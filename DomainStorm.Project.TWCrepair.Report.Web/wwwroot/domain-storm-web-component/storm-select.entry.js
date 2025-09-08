import { r as registerInstance, e as createEvent, a as getElement, h, d as Host } from './index-BpF8IqPI.js';
import { C as Choices } from './choices-DE0ykQng.js';
import { a as arrayEqual, d as debounce } from './utils-BOoVSa4-.js';
import './_commonjsHelpers-Cf5sKic0.js';

const stormSelectCss = ".choices__list--dropdown,.choices__list[aria-expanded]{z-index:1030}";

const StormSelect = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.valueChanged = createEvent(this, "valueChanged", 7);
        this.change = createEvent(this, "change", 7);
        this.searchChange = createEvent(this, "searchChange", 7);
    }
    _choices;
    _dataSet;
    _selectRef;
    get host() { return getElement(this); }
    label;
    value;
    name;
    dataSet;
    shouldSort = false;
    selection = 'single';
    removeItemButton = false;
    duplicateItemsAllowed = false;
    searchable;
    searchTimeOut = 500;
    searchFilter = true;
    valueChanged;
    change;
    searchChange;
    dataSetWatcher() {
        this.initDataSet();
        if (this._selectRef?.getAttribute('data-choice') === 'active' && this._dataSet) {
            if (this._dataSet.length) {
                this._choices.setChoices(this._dataSet, 'value', 'label', true);
            }
            else {
                this._choices.clearStore();
            }
        }
    }
    valueWatcher() {
        if (this._choices) {
            const newValue = this._choices.getValue(true);
            if (Array.isArray(newValue) && Array.isArray(this.value)) {
                if (!arrayEqual(newValue, this.value)) {
                    this._choices.setChoiceByValue(this.value);
                }
            }
            else if (newValue !== this.value) {
                this._choices.setChoiceByValue(this.value);
            }
        }
        this.change.emit(this.value);
        this.valueChanged.emit(this.value);
    }
    setSearch = debounce((value) => {
        this.searchChange.emit(value);
    }, this.searchTimeOut);
    initSelection() {
        if (this._dataSet) {
            if (this.value) {
                this._dataSet.forEach(item => {
                    item.selected = Array.isArray(this.value) ? this.value.includes(item.value) : this.value == item.value;
                });
            }
            else if (this._dataSet.some(item => item.selected)) {
                const selectedItems = this._dataSet.filter(item => item.selected);
                this.value = this.selection === 'single' ? selectedItems[0].value : selectedItems.map(item => item.value);
            }
            else if (this.selection === 'single') {
                this._dataSet[0].selected = true;
                this.value = this._dataSet[0].value;
            }
        }
        else if (this.selection === 'multiple') {
            this.value = Array.from(this.host.querySelectorAll('option:checked')).map(o => o.value);
        }
        else {
            this.value = this.host.querySelector('option:checked').value;
        }
    }
    initDataSet() {
        if (!this.dataSet) {
            this._dataSet = null;
        }
        else if (typeof this.dataSet === 'string') {
            this._dataSet = JSON.parse(this.dataSet);
        }
        else if (Array.isArray(this.dataSet) && this.dataSet.length) {
            this._dataSet = this.dataSet.map(item => ({
                value: item.id,
                label: item.title,
                selected: item.selected,
            }));
        }
    }
    handleFocused = async (e) => {
        e.target.parentElement.classList.add('is-focused');
    };
    handleFocusout = async (e) => {
        const target = e.target;
        if (target.value) {
            target.parentElement.classList.add('is-filled');
        }
        target.parentElement.classList.remove('is-focused');
    };
    handleKeyUp = async (e) => {
        const target = e.target;
        if (target.value) {
            target.parentElement.classList.add('is-filled');
        }
        else {
            target.parentElement.classList.remove('is-filled');
        }
    };
    componentWillLoad() {
        this.initDataSet();
        this.initSelection();
    }
    labelRender() {
        if (this.label) {
            return h("label", { class: "form-label ms-0" }, this.label);
        }
    }
    render() {
        return (h(Host, { key: 'cb28ec79e7ecdccf7ce430a7230fd7d983a0b5c8' }, this.labelRender(), h("select", { key: '28d6c9214ef896d71cf2e4ce35bbff6a0ca1c49c', class: "form-control", name: this.name, onFocus: e => this.handleFocused(e), onFocusout: e => this.handleFocusout(e), onKeyUp: e => this.handleKeyUp(e), ref: el => (this._selectRef = el), multiple: this.selection === 'multiple' }, h("slot", { key: 'f427511b3562ffe024f0c849dc687fdf6dd7f292', name: "option" }))));
    }
    componentDidRender() {
        if (this._selectRef.getAttribute('data-choice') !== 'active') {
            this._choices = new Choices(this._selectRef, {
                searchEnabled: this.searchable ?? this.selection === 'multiple',
                allowHTML: true,
                removeItemButton: this.removeItemButton,
                noChoicesText: '沒有可供選擇的選項',
                noResultsText: '找不到任何結果',
                duplicateItemsAllowed: this.duplicateItemsAllowed,
                shouldSort: this.shouldSort,
                searchPlaceholderValue: '搜尋',
                searchChoices: this.searchFilter,
                searchFloor: -1,
            });
            if (this._dataSet) {
                this._choices.setChoices(this._dataSet, 'value', 'label', true);
            }
            this._choices.passedElement.element.addEventListener('change', (e) => {
                e.preventDefault();
                e.stopPropagation();
                this.value = this._choices.getValue(true);
            }, false);
            if (!this.searchFilter)
                this._selectRef.addEventListener('search', e => {
                    const event = e;
                    this.setSearch(event.detail.value);
                });
        }
    }
    static get watchers() { return {
        "dataSet": ["dataSetWatcher"],
        "value": ["valueWatcher"]
    }; }
};
StormSelect.style = stormSelectCss;

export { StormSelect as storm_select };
//# sourceMappingURL=storm-select.entry.esm.js.map

//# sourceMappingURL=storm-select.entry.js.map