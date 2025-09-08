import { r as registerInstance, e as createEvent, a as getElement, f as forceUpdate, h, d as Host } from './index-BpF8IqPI.js';
import { C as CacheService } from './cache-service-pMD06zDP.js';
import { b as FnIconSwitch } from './functional-Cx8laaX_.js';
import { b as getFieldValue, s as setFieldValue, d as debounce, f as isConditionGroups, h as isConditions, j as evaluateConditions, k as escapeHtml, l as appendHighlight, m as replacePlaceholders, r as resolvePropertyValue } from './utils-BOoVSa4-.js';

/**
 * 判斷輸入是否為有效命令格式，例如 @Invoke:DotNet('GetUser')
 * @param value 任意輸入
 * @returns 是否符合命令格式
 */
function isCommandString(value) {
    if (typeof value !== 'string')
        return false;
    // 檢查型別與正則，確保格式正確
    // 僅允許單層括號內容，且括號內可為任意字元
    return (typeof value === 'string' &&
        /^@[a-zA-Z0-9:-]+\('.*'\)$/.test(value.trim()));
}
/**
 * 解析命令字串，回傳 ParsedCommand 或 null
 * 格式範例：@Invoke:DotNet('GetUser')
 * @param value 命令字串
 * @returns 解析後的命令物件，若格式不符則回傳 null
 */
function parseCommandString(value) {
    const trimmed = value.trim();
    // 強化正則表達式，避免括號內為空或出現多行情況
    const commandRegex = /^@([a-zA-Z0-9:-]+)\('([^']*)'\)$/;
    const match = commandRegex.exec(trimmed);
    if (!match)
        return null;
    return {
        command: match[1],
        value: match[2]
    };
}
/**
 * 檢查解析後命令是否符合指定 path
 * @param parsed 解析後的命令物件
 * @param expectedPath 預期的 path 字串，例如 "Invoke:DotNet"
 * @returns 是否符合預期 path
 */
function isPath(parsed, expectedPath) {
    return !!parsed && parsed.command === expectedPath;
}

class MemoryCache {
    store = new Map();
    maxEntries;
    constructor(maxEntries = 1000) {
        this.maxEntries = maxEntries;
    }
    async get(key) {
        return this.store.get(key) || null;
    }
    async set(key, entry) {
        // If updating existing key, no need to check size
        if (!this.store.has(key) && this.store.size >= this.maxEntries) {
            this.evictOldestEntry();
        }
        this.store.set(key, entry);
    }
    evictOldestEntry() {
        const iterator = this.store.keys();
        const firstKey = iterator.next().value;
        if (firstKey !== undefined) {
            this.store.delete(firstKey);
        }
    }
    async delete(key) {
        this.store.delete(key);
    }
    async clear() {
        this.store.clear();
    }
    get defaultKeyPrefix() {
        return 'mem';
    }
    async size() {
        return this.store.size;
    }
    setMaxEntries(maxEntries) {
        this.maxEntries = maxEntries;
        this.enforceMaxEntries();
    }
    enforceMaxEntries() {
        const excessCount = this.store.size - this.maxEntries;
        if (excessCount <= 0)
            return;
        const iterator = this.store.keys();
        for (let i = 0; i < excessCount; i++) {
            const key = iterator.next().value;
            if (key === undefined)
                break;
            this.store.delete(key);
        }
    }
    getMaxEntries() {
        return this.maxEntries;
    }
    async destroy() {
        this.store.clear();
    }
}

const cacheService = new CacheService(new MemoryCache());
/**
 * 呼叫 .NET Interop 方法，支援快取與錯誤保護
 * @param value    字串，格式可為 "Invoke:DotNet('methodName')" 或僅 methodName
 * @param args     傳給 .NET 方法的參數陣列
 * @param settings 可選設定：
 *                 - cache: 是否啟用快取，預設 true
 *                 - regionName: 快取區域名稱，預設 'dotnetInterop'
 * @return Promise<any> 回傳 .NET 方法的結果
 */
async function invokeDotNetMethodAsync(methodName, args = [], settings = {}) {
    if (typeof methodName !== 'string') {
        throw new TypeError('methodName 必須是字串');
    }
    settings = { cache: true, regionName: 'dotnetInterop', ...settings };
    // 組成 key：methodName + args
    const key = `${methodName}:${JSON.stringify(args)}`;
    const useCache = settings.cache ?? true;
    if (useCache) {
        const cachedValue = await cacheService.get(key);
        if (cachedValue !== null) {
            return cachedValue;
        }
    }
    try {
        const result = await window.counterInterop?.invokeMethodAsync(methodName, ...args);
        if (useCache) {
            await cacheService.set(key, result);
        }
        return result;
    }
    catch (err) {
        console.error(`[Interop] 無法呼叫 '${methodName}'：`, err);
        return 'unknown';
    }
}
/**
 * 清除 .NET Interop 方法的快取
 * @param regionName (可選) 指定要清除的 region，預設為 'dotnetInterop'
 * @return Promise<void>
 */
async function clearDotNetMethodCache() {
    await cacheService.clear();
}

const stormTableCellCss = ":host{display:block}.input-group.input-group-static .form-control{background-color:white;line-height:1.25rem;padding:.25rem}.detail-button{cursor:pointer}";

const StormTableCell = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.clickCell = createEvent(this, "clickCell", 7);
        this.clickAction = createEvent(this, "clickAction", 7);
        this.valueChanged = createEvent(this, "valueChanged", 7);
        this.cellWillValueChanged = createEvent(this, "cellWillValueChanged", 7);
        this.cellValueChanged = createEvent(this, "cellValueChanged", 7);
        this.cellDidValueChanged = createEvent(this, "cellDidValueChanged", 7);
        this.cellValueChangedOnBlur = createEvent(this, "cellValueChangedOnBlur", 7);
    }
    cellData;
    cellType = 'text';
    isDisabled = false;
    prependText;
    appendText;
    __cellValueEditingEvent;
    get host() { return getElement(this); }
    table;
    row;
    index;
    column;
    timeStamp;
    clickCell;
    clickAction;
    valueChanged;
    cellWillValueChanged;
    cellValueChanged;
    cellDidValueChanged;
    cellValueChangedOnBlur;
    valueChangedHandler(ev) {
        if (!this.column?.field)
            return;
        const eventDetail = {
            id: getFieldValue(this.row, this.table.fieldId),
            field: this.column.field,
            value: ev.detail,
        };
        this.cellWillValueChanged.emit(eventDetail);
    }
    cellWillValueChangedHandler(ev) {
        if (this.table.sidePagination === 'stencil') {
            this.cellValueChanged.emit(ev.detail);
        }
    }
    cellValueChangedHandler(ev) {
        setFieldValue(this.row, this.column.field, ev.detail.value);
        if (this.table.sidePagination === 'stencil') {
            this.cellDidValueChanged.emit(ev.detail);
        }
    }
    cellDidValueChangedHandler(ev) {
        this.__cellValueEditingEvent = ev.detail;
        this.rerender();
    }
    focusoutHandler(_) {
        if (this.__cellValueEditingEvent) {
            this.cellValueChangedOnBlur.emit(this.__cellValueEditingEvent);
            this.__cellValueEditingEvent = undefined;
        }
    }
    rerender = debounce(() => {
        forceUpdate(this);
    }, 100);
    async dispatchCustomEvent(type, detail) {
        this[type].emit(detail);
    }
    renderSerialNumber() {
        return (this.table.pageNumber - 1) * this.table.pageSize + this.index + 1;
    }
    renderDetail() {
        return (h(FnIconSwitch, { onToggle: on => {
                const detailRow = this.table.host.shadowRoot.querySelector('.row-detail[data-index="' + this.index + '"]');
                if (detailRow) {
                    detailRow.classList.toggle('d-none', !on);
                }
            } }));
    }
    renderActionControl() {
        return (h("storm-table-toolbar", { table: this.table, column: this.column, row: this.row, timeStamp: this.timeStamp, onClickAction: ev => {
                ev.stopPropagation();
                this.clickAction.emit(ev.detail);
            } }));
    }
    renderInputControl() {
        return (h("div", { class: "input-group input-group-static" }, h("input", { class: "form-control", type: "text", "aria-label": this.column.title, disabled: this.isDisabled, value: getFieldValue(this.row, this.column.field), onClick: ev => ev.stopPropagation(), onChange: ev => {
                ev.stopPropagation();
                this.valueChanged.emit(ev.target.value);
            } })));
    }
    renderTextareaControl() {
        const option = this.cellData;
        return (h("div", { class: "input-group input-group-static" }, h("textarea", { class: "form-control", disabled: this.isDisabled, cols: option.cols, rows: option.rows, title: this.column.title, value: getFieldValue(this.row, this.column.field), onClick: ev => ev.stopPropagation(), onChange: ev => {
                ev.stopPropagation();
                this.valueChanged.emit(ev.target.value);
            } })));
    }
    renderDropdownControl() {
        const option = this.cellData;
        const dataSet = option.dataSet;
        return (h("storm-dropdown", { disabled: this.isDisabled, label: option.label, value: getFieldValue(this.row, this.column.field), dataSet: dataSet, nullable: option.nullable ?? false, searchable: option.searchable ?? true, selection: 'single', timeStamp: this.timeStamp, type: 'select', onValueChanged: ev => {
                ev.stopPropagation();
                this.valueChanged.emit(ev.detail);
            } }));
    }
    renderDropdownTreeControl() {
        const option = this.cellData;
        let dataSet = option.dataSet;
        if (option.dataSets && isConditionGroups(option.dataSets)) {
            const matchingConditionGroup = option.dataSets.find(conditionGroup => isConditions(conditionGroup.conditions) && evaluateConditions(conditionGroup.conditions, this.row));
            if (matchingConditionGroup) {
                dataSet = matchingConditionGroup.value;
            }
        }
        return (h("storm-dropdown-tree", { disabled: this.isDisabled, loading: option.loading, partialLoad: option.partialLoad, label: option.label, value: getFieldValue(this.row, this.column.field), text: option.fieldText ? getFieldValue(this.row, option.fieldText) : null, dataSet: dataSet, dataSource: option.dataSource, fieldId: option.fieldId ?? 'id', fieldLabel: option.fieldLabel ?? 'label', fieldChildren: option.fieldChildren ?? 'children', expandLevel: option.expandLevel ?? 2, nodeSelectableRule: option.nodeSelectableRule, nullable: option.nullable ?? false, searchable: option.searchable ?? true, selection: 'single', timeStamp: this.timeStamp, onValueChanged: ev => {
                ev.stopPropagation();
                this.valueChanged.emit(ev.detail);
            }, onTextChanged: ev => {
                ev.stopPropagation();
                if (option.fieldText)
                    setFieldValue(this.row, option.fieldText, ev.detail);
            }, onSearchChanged: ev => {
                ev.stopPropagation();
            } }));
    }
    renderCheckControl() {
        return (h("storm-checkbox", { type: this.cellType === 'checkbox' ? 'checkbox' : 'radio', disabled: this.isDisabled, checked: getFieldValue(this.row, this.column.field) }));
    }
    renderCheckboxGroupControl() {
        const option = this.cellData;
        const value = getFieldValue(this.row, this.column.field);
        const dataSet = option.dataSet
            .filter(x => {
            if (x.visible === undefined)
                return true;
            return typeof x.visible === 'function' ? x.visible(this.row) : x.visible;
        })
            .map(x => ({
            id: x.id,
            label: x.label,
            labelClasses: x.labelClasses,
            checked: x.checked,
            disabled: x.disabled,
        }));
        return (h("div", { class: "input-group input-group-static" }, h("storm-checkbox-group", { disabled: this.isDisabled, type: option.groupType, value: value, dataSet: dataSet, isSwitch: option.isSwitch ?? false, isInline: option.isInline ?? true, onValueChanged: ev => {
                ev.stopPropagation();
                this.valueChanged.emit(ev.detail);
            } })));
    }
    renderCellContent() {
        switch (this.cellType) {
            case 'action':
                return this.renderActionControl();
            case 'input':
                return this.renderInputControl();
            case 'checkbox':
            case 'radio':
                return this.renderCheckControl();
            case 'textarea':
                return this.renderTextareaControl();
            case 'dropdown':
                return this.renderDropdownControl();
            case 'dropdownTree':
                return this.renderDropdownTreeControl();
            case 'checkboxGroup':
                return this.renderCheckboxGroupControl();
            case 'serialNumber':
                return this.renderSerialNumber();
            case 'detail':
                return this.renderDetail();
            case 'text':
                return this.renderCellValue();
            default:
                return null;
        }
    }
    renderCell() {
        if (!this.prependText && !this.appendText) {
            return this.renderCellContent();
        }
        else {
            return (h("div", { class: "d-flex flex-row gap-1 align-items-center" }, this.prependText && h("span", null, this.prependText), this.renderCellContent(), this.appendText && h("span", null, this.appendText)));
        }
    }
    renderCellValue() {
        let content = getFieldValue(this.row, this.column.field);
        if (typeof this.column.render === 'function') {
            content = this.column.render(content, this.row, this.index);
        }
        else {
            if (Array.isArray(content)) {
                content = content.filter(item => item != null && item !== '').join('、');
            }
            if (this.table.escape && typeof content !== 'boolean' && content != null) {
                content = escapeHtml(content);
            }
            if (content != null && this.column.transform && this.column.transform[content.toString()]) {
                content = this.column.transform[content];
            }
            else if (content && this.table.searchable && this.table.searchText) {
                content = appendHighlight(content, this.table.searchText);
            }
        }
        const template = this.column.cellTemplate || this.table.cellTemplate;
        const displayValue = content != null && content.toString().trim().length > 0 ? content : '-';
        return (h("span", { innerHTML: replacePlaceholders(template, {
                value: displayValue,
                row: this.row,
                index: this.index,
            }) }));
    }
    async componentWillRender() {
        if (!this.column) {
            this.resetCellState();
            return;
        }
        const [cellType, cellData] = await Promise.all([this.resolvePropertyValueAsync(this.column, 'type'), this.resolvePropertyValueAsync(this.column, 'data')]);
        this.cellType = cellType || 'text';
        this.cellData = cellData;
        if (cellData && typeof cellData === 'object' && !Array.isArray(cellData)) {
            const [disabled, prependText, appendText] = await Promise.all([
                this.resolvePropertyValueAsync(cellData, 'disabled'),
                this.resolvePropertyValueAsync(cellData, 'prependText'),
                this.resolvePropertyValueAsync(cellData, 'appendText'),
            ]);
            this.isDisabled = disabled ?? false;
            this.prependText = prependText;
            this.appendText = appendText;
            if (this.isDisabled && (cellType === 'input' || cellType === 'textarea')) {
                this.cellType = 'text';
            }
        }
        else {
            this.resetControlState();
        }
    }
    resetCellState() {
        this.cellType = 'text';
        this.cellData = undefined;
        this.resetControlState();
    }
    resetControlState() {
        this.isDisabled = false;
        this.prependText = undefined;
        this.appendText = undefined;
        this.__cellValueEditingEvent = undefined;
    }
    async resolvePropertyValueAsync(targetObject, propertyName) {
        const result = resolvePropertyValue(targetObject, propertyName, [this.row, this.index, this.column]);
        if (isCommandString(result)) {
            const parsed = parseCommandString(result);
            if (parsed.command === 'Invoke:DotNet') {
                const rowId = getFieldValue(this.row, this.table?.fieldId);
                try {
                    return await invokeDotNetMethodAsync(parsed.value, [rowId, this.index, this.column.field], { cache: true });
                }
                catch (error) {
                    console.error('InvokeDotNetMethod 執行失敗', error);
                    throw error;
                }
            }
            if (parsed.command === 'Get:Field') {
                return getFieldValue(this.row, parsed.value);
            }
        }
        if (isConditions(result)) {
            return evaluateConditions(result, this.row);
        }
        if (isConditionGroups(result)) {
            for (const conditionGroup of result) {
                if (isConditions(conditionGroup.conditions) && evaluateConditions(conditionGroup.conditions, this.row)) {
                    return conditionGroup.value;
                }
            }
            return undefined;
        }
        if (result instanceof Promise) {
            try {
                const resolved = await result;
                return resolved;
            }
            catch (error) {
                console.error('屬性 Promise 解析失敗', error);
                throw error;
            }
        }
        return result;
    }
    render() {
        return (h(Host, { key: 'fd24e3ec8802a3866244dcc1f73d5fa8dc805121', onClick: ev => {
                if (this.cellType === 'checkbox' || this.cellType === 'radio') {
                    const option = this.cellData;
                    if (option && option.disabled) {
                        ev.stopPropagation();
                        ev.preventDefault();
                        return;
                    }
                }
                this.clickCell.emit({ id: getFieldValue(this.row, this.table.fieldId), field: this.column.field });
            } }, this.renderCell()));
    }
};
StormTableCell.style = stormTableCellCss;

export { StormTableCell as storm_table_cell };
//# sourceMappingURL=storm-table-cell.entry.esm.js.map

//# sourceMappingURL=storm-table-cell.entry.js.map