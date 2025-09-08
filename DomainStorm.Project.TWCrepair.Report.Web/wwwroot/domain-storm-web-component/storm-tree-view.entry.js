import { r as registerInstance, e as createEvent, a as getElement, f as forceUpdate, h, d as Host } from './index-BpF8IqPI.js';
import { C as CacheService } from './cache-service-pMD06zDP.js';
import { a as FnSearchInput } from './functional-Cx8laaX_.js';
import { S as SharedStyles } from './shared-styles-B0cWbvbo.js';
import { b as getFieldValue, s as setFieldValue, i as isModuleFunctionString, e as createFunctionFromString, d as debounce, l as appendHighlight, n as calculateObjectValue } from './utils-BOoVSa4-.js';

class IndexedDBCache {
    dbPromise = null;
    maxEntries;
    dbName;
    dbVersion;
    constructor(maxEntries = 1000, dbName = 'StencilCacheDB', dbVersion = 1) {
        this.maxEntries = Math.max(1, maxEntries); // Ensure at least 1 entry
        this.dbName = dbName;
        this.dbVersion = dbVersion;
    }
    openDB() {
        if (!this.dbPromise) {
            this.dbPromise = new Promise((resolve, reject) => {
                const request = window.indexedDB.open(this.dbName, this.dbVersion);
                request.onupgradeneeded = (event) => {
                    const db = event.target.result;
                    if (!db.objectStoreNames.contains('cache')) {
                        const store = db.createObjectStore('cache', { keyPath: 'key' });
                        store.createIndex('timestamp', 'timestamp', { unique: false });
                    }
                };
                request.onsuccess = (event) => {
                    resolve(event.target.result);
                };
                request.onerror = () => {
                    this.dbPromise = null;
                    reject(request.error);
                };
            });
        }
        return this.dbPromise;
    }
    async withTransaction(mode, operation) {
        const db = await this.openDB();
        const tx = db.transaction('cache', mode);
        return await operation(tx.objectStore('cache'));
    }
    async get(key) {
        return await this.withTransaction('readonly', (store) => {
            return new Promise((resolve, reject) => {
                const req = store.get(key);
                req.onsuccess = () => {
                    const record = req.result;
                    resolve(record ? {
                        value: record.value,
                        timestamp: record.timestamp,
                        ttlMillis: record.ttlMillis
                    } : null);
                };
                req.onerror = () => reject(req.error);
            });
        });
    }
    async set(key, entry) {
        await this.withTransaction('readwrite', (store) => {
            return new Promise((resolve, reject) => {
                // Check if key exists first
                const existsReq = store.get(key);
                existsReq.onsuccess = () => {
                    const exists = existsReq.result;
                    const processSet = () => {
                        const req = store.put({ key, ...entry });
                        req.onsuccess = () => resolve();
                        req.onerror = () => reject(req.error);
                    };
                    if (exists) {
                        // Key exists, just update
                        processSet();
                    }
                    else {
                        // New key, check size limit
                        const countReq = store.count();
                        countReq.onsuccess = () => {
                            if (countReq.result >= this.maxEntries) {
                                this.evictOldestEntry(store, processSet, reject);
                            }
                            else {
                                processSet();
                            }
                        };
                        countReq.onerror = () => reject(countReq.error);
                    }
                };
                existsReq.onerror = () => reject(existsReq.error);
            });
        });
    }
    evictOldestEntry(store, onComplete, onError) {
        const index = store.index('timestamp');
        const deleteReq = index.openCursor();
        deleteReq.onsuccess = (event) => {
            const cursor = event.target.result;
            if (cursor) {
                const delReq = cursor.delete();
                delReq.onsuccess = onComplete;
                delReq.onerror = () => onError(delReq.error);
            }
            else {
                onComplete();
            }
        };
        deleteReq.onerror = () => onError(deleteReq.error);
    }
    async delete(key) {
        await this.withTransaction('readwrite', (store) => {
            return new Promise((resolve, reject) => {
                const req = store.delete(key);
                req.onsuccess = () => resolve();
                req.onerror = () => reject(req.error);
            });
        });
    }
    async clear() {
        await this.withTransaction('readwrite', (store) => {
            return new Promise((resolve, reject) => {
                const req = store.clear();
                req.onsuccess = () => resolve();
                req.onerror = () => reject(req.error);
            });
        });
    }
    get defaultKeyPrefix() {
        return 'idb';
    }
    async size() {
        return await this.withTransaction('readonly', (store) => {
            return new Promise((resolve, reject) => {
                const req = store.count();
                req.onsuccess = () => resolve(req.result);
                req.onerror = () => reject(req.error);
            });
        });
    }
    setMaxEntries(maxEntries) {
        this.maxEntries = maxEntries;
    }
    getMaxEntries() {
        return this.maxEntries;
    }
    async destroy() {
        if (this.dbPromise) {
            const db = await this.dbPromise;
            db.close();
            this.dbPromise = null;
        }
        return new Promise((resolve, reject) => {
            const deleteReq = window.indexedDB.deleteDatabase(this.dbName);
            deleteReq.onsuccess = () => resolve();
            deleteReq.onerror = () => reject(deleteReq.error);
            deleteReq.onblocked = () => {
                console.warn('Database deletion blocked. Other connections may be open.');
                resolve();
            };
        });
    }
}

const cacheService = new CacheService(new IndexedDBCache(), {
    regionName: 'fetchDataSource',
});
async function fetchDataSource(options) {
    // 1. 合併預設值
    options = {
        method: 'POST',
        contentType: 'application/json',
        ...options,
    };
    // 2. 以 JSON.stringify(options) 當作單筆快取的 key
    const key = JSON.stringify(options);
    // 3. 嘗試從快取讀取 (使用預設 region)
    const cached = await cacheService.get(key);
    if (cached !== null) {
        return cached;
    }
    // 4. 快取不存在或已過期，真正執行 fetch
    const response = await fetch(options.url, {
        method: options.method,
        body: options.method !== 'GET' ? JSON.stringify(options.body ?? {}) : null,
        headers: new Headers({
            'Content-Type': options.contentType,
        }),
    });
    if (!response.ok) {
        throw new Error(`Network response was not ok: ${response.statusText}`);
    }
    const data = options.contentType === 'application/json'
        ? await response.json()
        : await response.text();
    // 5. 把新資料寫回快取 (使用預設 region)
    await cacheService.set(key, data);
    return data;
}

const stormTreeViewCss = ":host{display:block;background-color:#fff}:host .list-group-item{border:none !important;white-space:nowrap !important;cursor:pointer !important;margin-bottom:0 !important}:host .close.material-icons,:host .open.material-icons:hover{opacity:0.5}:host .open.material-icons,:host .close.material-icons:hover{opacity:1}:host .material-icons:empty{width:1.25rem}:host .indent{margin-left:0.575rem;margin-right:0.575rem}:host .leaf{margin-left:0.6rem}";

const StormTreeView = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.dateSetLoaded = createEvent(this, "dateSetLoaded", 7);
        this.valueChanged = createEvent(this, "valueChanged", 7);
        this.nodeSelect = createEvent(this, "nodeSelect", 7);
        this.nodeUnselect = createEvent(this, "nodeUnselect", 7);
        this.nodeClick = createEvent(this, "nodeClick", 7);
        this.nodeDblClick = createEvent(this, "nodeDblClick", 7);
        this.nodeCheck = createEvent(this, "nodeCheck", 7);
        this.nodeUncheck = createEvent(this, "nodeUncheck", 7);
        this.checkedChanged = createEvent(this, "checkedChanged", 7);
        this.searchChanged = createEvent(this, "searchChanged", 7);
        this.keydown = createEvent(this, "keydown", 7);
    }
    _searchText;
    _dataSet;
    _items;
    _dataSource;
    _searchRef;
    _nodeSelectableRule;
    _nodeCheckRule;
    get host() { return getElement(this); }
    partialLoad = false;
    loading = 'eager';
    value;
    checked = [];
    fieldId = 'id';
    fieldLabel = 'label';
    fieldChildren = 'children';
    fieldSubId = 'subId';
    fieldSubLabel = 'subLabel';
    fieldSubChildren = 'subChildren';
    fieldSelected = '_selected';
    fieldChecked = '_checked';
    fieldExpanded = '_expanded';
    fieldDisabled = '_disabled';
    fieldParent = '_parent';
    fieldSelectable = '_selectable';
    fieldCheckSelectable = '_checkSelectable';
    fieldIcon = '_icon';
    fieldHref = '_href';
    fieldLevel = '_level';
    fieldChildLoaded = '_loaded';
    fieldElement = '_element';
    expandIconAlign = 'left';
    expandIcon;
    collapseIcon;
    color;
    dataSet;
    dataSource;
    fetch;
    nullable = true;
    expandLevel = 2;
    structure = 'auto';
    searchable = false;
    selection = 'single';
    nodeSelectableRule;
    checkSelectable = false;
    nodeCheckRule;
    selectToCheck = true;
    dateSetLoaded;
    valueChanged;
    nodeSelect;
    nodeUnselect;
    nodeClick;
    nodeDblClick;
    nodeCheck;
    nodeUncheck;
    checkedChanged;
    searchChanged;
    keydown;
    async searchChangedHandler(ev) {
        this._searchText = ev.detail;
        if (this.partialLoad) {
            this.cleanNodes();
            await this.loadDataSet();
        }
        else {
            this.rerender();
        }
    }
    nodeSelectHandler(ev) {
        switch (this.selection) {
            case 'single':
                this.value = ev.detail;
                break;
            case 'multiple': {
                const currentValue = Array.isArray(this.value) ? this.value : this.value ? [this.value] : [];
                if (!currentValue.includes(ev.detail))
                    this.value = [...currentValue, ev.detail];
                break;
            }
            default:
                console.warn(`Unknown selection mode: ${this.selection}`);
        }
    }
    nodeUnselectHandler(ev) {
        switch (this.selection) {
            case 'single':
                if (this.value === ev.detail) {
                    this.value = null;
                }
                break;
            case 'multiple': {
                const currentValue = Array.isArray(this.value) ? this.value : [];
                if (currentValue.includes(ev.detail))
                    this.value = currentValue.filter(id => id !== ev.detail);
                break;
            }
            default:
                console.warn(`Unknown selection mode: ${this.selection}`);
        }
    }
    nodeCheckHandler(ev) {
        if (this.selectToCheck)
            return;
        if (!this.checked.includes(ev.detail))
            this.checked = [...this.checked, ev.detail];
    }
    nodeUncheckHandler(ev) {
        if (this.selectToCheck)
            return;
        if (this.checked.includes(ev.detail))
            this.checked = this.checked.filter(id => id !== ev.detail);
    }
    async valueChangedHandler() {
        if (!Array.isArray(this._items))
            return;
        const valueArray = Array.isArray(this.value) ? this.value : this.value ? [this.value] : [];
        await Promise.all(this._items.map(async (item) => {
            const id = String(getFieldValue(item, this.fieldId));
            const shouldSelect = valueArray.includes(id);
            const selected = getFieldValue(item, this.fieldSelected) === true;
            const promises = [];
            if (shouldSelect) {
                promises.push(this.setNodeExpanded(item));
            }
            if (selected !== shouldSelect) {
                promises.push(this.setNodeSelectionState(item, shouldSelect));
            }
            return Promise.all(promises);
        }));
        if (this.checkSelectable && this.selectToCheck) {
            this.checked = valueArray;
        }
    }
    async checkedChangedHandler() {
        if (!Array.isArray(this._items))
            return;
        await Promise.all(this._items.map(item => {
            const id = String(getFieldValue(item, this.fieldId));
            const checkedArray = Array.isArray(this.checked) ? this.checked : this.checked ? [this.checked] : [];
            const shouldChecked = checkedArray.includes(id);
            const checked = getFieldValue(item, this.fieldChecked) === true;
            if (checked !== shouldChecked) {
                return this.setNodeCheckedState(item, shouldChecked);
            }
            return Promise.resolve();
        }));
    }
    async watchValueHandler() {
        this.valueChanged.emit(this.value);
    }
    async watchCheckedHandler() {
        this.checkedChanged.emit(this.checked);
    }
    async setNodeSelectionState(item, shouldSelect) {
        const node = await this.getTreeNode(item);
        if (node) {
            await (shouldSelect ? node.select() : node.unselect());
        }
        else {
            setFieldValue(item, this.fieldSelected, shouldSelect);
        }
        if (this.checkSelectable && this.selectToCheck) {
            await this.setNodeCheckedState(item, shouldSelect);
        }
    }
    async setNodeCheckedState(item, shouldChecked) {
        const node = await this.getTreeNode(item);
        if (node) {
            await (shouldChecked ? node.check() : node.uncheck());
        }
        else {
            setFieldValue(item, this.fieldChecked, shouldChecked);
        }
    }
    watchDataSourceHandler() {
        this.cleanNodes();
        if (typeof this.dataSource === 'string') {
            if (isModuleFunctionString(this.dataSource)) {
                this._dataSource = createFunctionFromString(this.dataSource);
            }
            else if (this.dataSource.startsWith('{') && this.dataSource.endsWith('}')) {
                this._dataSource = JSON.parse(this.dataSource);
            }
            else {
                this._dataSource = { url: this.dataSource };
            }
        }
        else {
            this._dataSource = this.dataSource;
        }
    }
    watchNodeSelectableRuleHandler() {
        if (typeof this.nodeSelectableRule === 'string') {
            if (isModuleFunctionString(this.nodeSelectableRule)) {
                this._nodeSelectableRule = createFunctionFromString(this.nodeSelectableRule);
            }
        }
        else {
            this._nodeSelectableRule = this.nodeSelectableRule;
        }
    }
    watchNodeCheckRuleHandler() {
        if (typeof this.nodeCheckRule === 'string') {
            if (isModuleFunctionString(this.nodeCheckRule)) {
                this._nodeCheckRule = createFunctionFromString(this.nodeCheckRule);
            }
        }
        else {
            this._nodeCheckRule = this.nodeCheckRule;
        }
    }
    async watchDataSetHandler() {
        this.cleanNodes();
        await this.watchValueHandler();
        await this.watchCheckedHandler();
    }
    async getNodeById(id) {
        if (!this._items)
            return null;
        return this._items.find(x => getFieldValue(x, this.fieldId) === id);
    }
    async getTreeNode(item) {
        return getFieldValue(item, this.fieldElement);
    }
    async getLabel(item) {
        return getFieldValue(item, this.fieldLabel);
    }
    async unselectAll() {
        this.value = null;
    }
    async loadDataSet() {
        if (this._dataSet)
            return;
        if (this.dataSource || this.fetch) {
            try {
                const dataSet = await this.getDataSource();
                this.setDataSet(dataSet);
            }
            catch (error) {
                console.error('Error loading data:', error);
            }
        }
        else {
            this.setDataSet(this.dataSet);
        }
    }
    async getSelected() {
        return this._items.filter(node => getFieldValue(node, this.fieldSelected));
    }
    async getSelectedById() {
        return await this.getSelected().then(x => x.map(y => getFieldValue(y, this.fieldId)));
    }
    async setSearchFocus() {
        if (this.searchable && this._searchRef) {
            await this._searchRef.setFocus();
        }
    }
    rerender = debounce(async () => {
        if (!Array.isArray(this._items) || this._items.length === 0)
            return;
        await Promise.all(this._items.map(async (item) => {
            const node = await this.getTreeNode(item);
            if (node) {
                forceUpdate(node);
            }
        }));
        forceUpdate(this);
    }, 100);
    renderNode(item) {
        let content = getFieldValue(item, this.fieldLabel);
        if (content && this._searchText) {
            content = appendHighlight(content, this._searchText);
        }
        return h("storm-tree-node", { tree: this, dataSet: item, content: content, searchText: this._searchText });
    }
    async setNodeExpanded(item) {
        let parent = getFieldValue(item, this.fieldParent);
        while (parent) {
            const node = await this.getTreeNode(parent);
            if (node) {
                await node.expand().catch(error => {
                    console.error('Failed to expand tree node:', error);
                });
            }
            else {
                setFieldValue(parent, this.fieldExpanded, true);
            }
            parent = getFieldValue(parent, this.fieldParent);
        }
    }
    cleanNodes() {
        this._dataSet = undefined;
        this._items = undefined;
    }
    async getDataSource(treeNode = null) {
        if (this.fetch) {
            return this.fetch(treeNode?.dataSet);
        }
        else {
            const options = typeof this._dataSource === 'function' ? this._dataSource(treeNode?.dataSet, this._searchText) : this._dataSource;
            return await fetchDataSource(options);
        }
    }
    async loadChild(treeNode) {
        try {
            const dataSource = await this.getDataSource(treeNode);
            const dataSet = this.dataSetParse(dataSource);
            await this.mergeChild(dataSet, this.fieldChildren.split('&').map(x => x.trim()));
            setFieldValue(treeNode.dataSet, this.fieldChildLoaded, true);
            this._items = this.updateItems(this._dataSet);
        }
        catch (error) {
            console.error('Error loading data:', error);
        }
    }
    async mergeChild(source, fieldChildren) {
        for (const item of source) {
            const dest = await this.getNodeById(getFieldValue(item, this.fieldId));
            for (const part of fieldChildren) {
                const sourcePartValue = getFieldValue(item, part);
                const destPartValue = getFieldValue(dest, part);
                if (!!sourcePartValue && (!destPartValue || destPartValue.length == 0)) {
                    setFieldValue(dest, part, sourcePartValue);
                }
                else if (!!sourcePartValue && sourcePartValue.length > 0) {
                    await this.mergeChild(sourcePartValue, [part]);
                }
            }
        }
    }
    dataSetParse(value) {
        const clonedValue = JSON.parse(JSON.stringify(value));
        const structureType = this.structure === 'auto' ? (!Array.isArray(clonedValue) && !!this.fieldChildren ? 'tree' : 'array') : this.structure;
        switch (structureType) {
            case 'tree':
                return getFieldValue(clonedValue, this.fieldChildren);
            case 'array':
                return clonedValue;
            default:
                throw new Error(`Unsupported structure type: ${structureType}`);
        }
    }
    setDataSet(value) {
        if (!value)
            return;
        this._dataSet = this.dataSetParse(value);
        this._items = this.updateItems(this._dataSet);
        this.rerender();
    }
    updateItems(value, parent = null) {
        if (!value || value.length === 0)
            return [];
        const items = [];
        for (const item of value) {
            let id = getFieldValue(item, this.fieldId);
            if (id == null) {
                id = getFieldValue(item, this.fieldLabel);
                setFieldValue(item, this.fieldId, id);
            }
            if (this.selection != 'none' && item[this.fieldSelectable] == null) {
                setFieldValue(item, this.fieldSelectable, calculateObjectValue(this, this._nodeSelectableRule, item) ?? true);
            }
            if (this.checkSelectable && item[this.fieldCheckSelectable] == null) {
                setFieldValue(item, this.fieldCheckSelectable, calculateObjectValue(this, this._nodeCheckRule, item) ?? true);
            }
            let level = 1;
            if (parent) {
                setFieldValue(item, this.fieldParent, parent);
                level = getFieldValue(parent, this.fieldLevel) + 1;
            }
            setFieldValue(item, this.fieldLevel, level);
            if (!getFieldValue(item, this.fieldChildLoaded)) {
                setFieldValue(item, this.fieldExpanded, this.expandLevel > level);
                setFieldValue(item, this.fieldChildLoaded, !this.partialLoad || this.expandLevel > level);
            }
            items.push(item);
            const children = getFieldValue(item, this.fieldChildren);
            if (children && children.length > 0) {
                items.push(...this.updateItems(children, item));
            }
        }
        return items;
    }
    async componentWillLoad() {
        await SharedStyles.applyShadowRootStylesAsync(this.host, 'material');
        this.expandIcon = this.expandIcon ?? (this.expandIconAlign === 'right' ? 'add' : 'chevron_right');
        this.collapseIcon = this.collapseIcon ?? (this.expandIconAlign === 'right' ? 'remove' : 'expand_more');
        this.watchDataSourceHandler();
        this.watchNodeSelectableRuleHandler();
        this.watchNodeCheckRuleHandler();
    }
    async componentWillRender() {
        if (this.loading === 'eager' || !this.partialLoad) {
            await this.loadDataSet();
        }
        if (this.selection != 'none' && !this.nullable && !this.value && this._items?.length) {
            this.value = getFieldValue(this._items.find(item => getFieldValue(item, this.fieldSelectable)), this.fieldId);
        }
        await this.watchValueHandler();
        await this.watchCheckedHandler();
    }
    async componentDidLoad() {
        if (this._items?.length) {
            this.dateSetLoaded.emit();
        }
    }
    handleSearchChanged(ev) {
        ev.stopPropagation();
        const value = ev.detail?.trim() ?? '';
        if (this._searchText === value)
            return;
        if (value) {
            this.searchChanged.emit(value);
        }
    }
    renderSearch() {
        if (!this.searchable)
            return;
        return h(FnSearchInput, { ref: elm => (this._searchRef = elm), value: this._searchText, onValueChanged: ev => this.handleSearchChanged(ev) });
    }
    nodesRender() {
        let nodes = [];
        if (this.partialLoad || !this.searchable || this._searchText == null || !this._searchText.length) {
            nodes = this._dataSet;
        }
        else {
            nodes = this._items.filter(x => getFieldValue(x, this.fieldLabel).includes(this._searchText));
        }
        if (nodes.length === 0)
            return h("div", { class: "align-middle text-center p-4 h6" }, "\u6C92\u6709\u627E\u5230\u7B26\u5408\u7684\u7D50\u679C");
        return nodes.map(item => this.renderNode(item));
    }
    render() {
        if (!this._dataSet)
            return;
        return (h(Host, null, this.renderSearch(), this.nodesRender()));
    }
    static get watchers() { return {
        "value": ["watchValueHandler"],
        "checked": ["watchCheckedHandler"],
        "dataSource": ["watchDataSourceHandler"],
        "nodeSelectableRule": ["watchNodeSelectableRuleHandler"],
        "nodeCheckRule": ["watchNodeCheckRuleHandler"],
        "dataSet": ["watchDataSetHandler"]
    }; }
};
StormTreeView.style = stormTreeViewCss;

export { StormTreeView as storm_tree_view };
//# sourceMappingURL=storm-tree-view.entry.esm.js.map

//# sourceMappingURL=storm-tree-view.entry.js.map