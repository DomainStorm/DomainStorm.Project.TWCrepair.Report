import { r as registerInstance, e as createEvent, a as getElement, h, d as Host } from './index-BpF8IqPI.js';
import { F as FnIcon, a as FnSearchInput } from './functional-Cx8laaX_.js';
import { a as arrayEqual, g as generateUid, n as calculateObjectValue, o as mergeClasses, l as appendHighlight, t as getSelectedItemIds } from './utils-BOoVSa4-.js';

const stormListGroupCss = ":host{display:block}";

const StormListGroup = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.valueChanged = createEvent(this, "valueChanged", 7);
        this.itemClick = createEvent(this, "itemClick", 7);
        this.selectChange = createEvent(this, "selectChange", 7);
        this.searchChanged = createEvent(this, "searchChanged", 7);
    }
    _itemRefs;
    _searchRef;
    get host() { return getElement(this); }
    dataSet;
    headline;
    searchText;
    searchable = false;
    selection = 'single';
    nullable = false;
    value;
    class;
    itemClass;
    valueChanged;
    itemClick;
    selectChange;
    searchChanged;
    valueWatcher(newValue, oldValue) {
        if (arrayEqual(newValue, oldValue))
            return;
        this.setSelectedItems();
        this.valueChanged.emit(newValue);
    }
    dataSetWatcher() {
        this.initDataSet();
        this.setSelectedItems();
    }
    itemClickHandler(event) {
        if (this.selection === 'none')
            return;
        const item = event.detail;
        item.selected ? this.unselect(item) : this.select(item);
    }
    searchChangedHandler(ev) {
        this.searchText = ev.detail;
    }
    async setSearchFocus() {
        if (this.searchable && this._searchRef) {
            requestAnimationFrame(async () => {
                await this._searchRef.setFocus();
            });
        }
    }
    handleSearchChanged(ev) {
        ev.stopPropagation();
        const value = ev.detail?.trim();
        if (this.searchText === value)
            return;
        this.searchChanged.emit(value);
    }
    select(item) {
        if (item.selected)
            return;
        switch (this.selection) {
            case 'single':
                this.value = item.id;
                break;
            case 'multiple': {
                const currentValue = this.getCurrentValueAsArray();
                if (!currentValue.includes(item.id)) {
                    this.value = [...currentValue, item.id];
                }
                break;
            }
            default:
                console.warn(`Unknown selection mode: ${this.selection}`);
        }
        this.selectChange.emit(item);
    }
    unselect(item) {
        if (!item.selected)
            return;
        switch (this.selection) {
            case 'single':
                if (!this.nullable)
                    return;
                this.value = null;
                break;
            case 'multiple': {
                const currentValue = this.getCurrentValueAsArray();
                if (currentValue.length <= 1)
                    return;
                if (currentValue.includes(item.id)) {
                    this.value = currentValue.filter(id => id !== item.id);
                }
                break;
            }
            default:
                console.warn(`Unknown selection mode: ${this.selection}`);
        }
        this.selectChange.emit(item);
    }
    getCurrentValueAsArray() {
        return Array.isArray(this.value) ? this.value : this.value ? [this.value] : [];
    }
    initDataSet() {
        if (!Array.isArray(this.dataSet))
            return;
        this.dataSet.forEach(item => {
            item.id = item.id || generateUid('ilist-item');
        });
    }
    setSelectedItems() {
        if (!Array.isArray(this.dataSet))
            return;
        const valueSet = new Set(this.getCurrentValueAsArray());
        this.dataSet.forEach(item => {
            item.selected = valueSet.has(item.id);
        });
    }
    render() {
        const listGroup = this.renderListGroup();
        if (!this.headline) {
            return h(Host, null, listGroup);
        }
        return (h(Host, null, h("storm-card", { scrollable: true, class: "mt-4", bodyClass: "p-0" }, h("div", { slot: "card-header" }, h("div", { class: "border-radius-xl mt-n4 mx-4 mb-2 bg-gradient-dark shadow-dark p-1" }, h("h6", { class: "text-white font-weight-bolder text-center m-1" }, this.headline))), h("div", { slot: "body" }, listGroup))));
    }
    renderItems() {
        this._itemRefs = {};
        if (!Array.isArray(this.dataSet) || this.dataSet.length === 0) {
            return h("div", { class: "disabled list-group-item align-middle text-center p-4 h6" }, "\u6C92\u6709\u627E\u5230\u7B26\u5408\u7684\u7D50\u679C");
        }
        const filteredItems = this.getFilteredItems();
        return filteredItems.map(item => this.renderItem(item));
    }
    getFilteredItems() {
        let items = this.dataSet.filter(item => calculateObjectValue(this, item.visible) ?? true);
        if (this.searchable && this.searchText?.trim()) {
            const searchTerm = this.searchText.toLowerCase();
            items = items.filter(item => item.title?.toLowerCase().includes(searchTerm));
        }
        return items;
    }
    renderItem(item) {
        const disabled = (calculateObjectValue(this, item.disabled) ?? false);
        return (h("a", { href: "javascript:void(0);", class: mergeClasses({
                'list-group-item': true,
                'list-group-item-action': true,
                disabled: disabled,
                active: item.selected,
                'px-2': true,
                'py-1': true,
            }, this.itemClass), onClick: ev => {
                ev.stopPropagation();
                this.itemClick.emit(item);
            }, ref: elm => (this._itemRefs[item.id] = elm) }, h(FnIcon, null, item.icon), h(FnIcon, { class: { 'float-end': true } }, item.selected && this.selection === 'multiple' ? 'check_circle' : ''), this.renderItemLabel(item.title)));
    }
    renderListGroup() {
        return (h("div", { class: mergeClasses({ 'list-group': true, 'list-group-flush': !!this.headline }, this.class) }, this.renderSearch(), this.renderItems()));
    }
    renderItemLabel(value) {
        const shouldHighlight = this.searchText?.trim() && value;
        const displayValue = shouldHighlight ? appendHighlight(value, this.searchText) : value;
        return h("span", { class: { 'nav-link-text': true }, innerHTML: displayValue });
    }
    renderSearch() {
        if (!this.searchable)
            return;
        return (h("div", { class: "search" }, h(FnSearchInput, { ref: elm => (this._searchRef = elm), value: this.searchText, onValueChanged: ev => this.handleSearchChanged(ev) })));
    }
    componentWillLoad() {
        this.initDataSet();
        const hasValue = this.value && ((typeof this.value === 'string' && this.value.trim()) || (Array.isArray(this.value) && this.value.length > 0));
        if (!hasValue) {
            this.value = getSelectedItemIds(this.dataSet, this.nullable, this.selection);
        }
        this.setSelectedItems();
    }
    componentDidLoad() {
        const hasValidValue = (typeof this.value === 'string' && this.value.trim() !== '') || (Array.isArray(this.value) && this.value.length > 0);
        if (hasValidValue) {
            this.valueChanged.emit(this.value);
        }
    }
    static get watchers() { return {
        "value": ["valueWatcher"],
        "dataSet": ["dataSetWatcher"]
    }; }
};
StormListGroup.style = stormListGroupCss;

export { StormListGroup as storm_list_group };
//# sourceMappingURL=storm-list-group.entry.esm.js.map

//# sourceMappingURL=storm-list-group.entry.js.map