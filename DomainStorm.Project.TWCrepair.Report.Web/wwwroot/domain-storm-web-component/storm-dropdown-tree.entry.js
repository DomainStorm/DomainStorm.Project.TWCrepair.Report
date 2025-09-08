import { r as registerInstance, e as createEvent, a as getElement, h, d as Host } from './index-BpF8IqPI.js';
import { a as arrayEqual } from './utils-BOoVSa4-.js';

const stormDropdownTreeCss = ":host{display:block}";

const StormDropdownTree = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.valueChanged = createEvent(this, "valueChanged", 7);
        this.textChanged = createEvent(this, "textChanged", 7);
        this.searchChanged = createEvent(this, "searchChanged", 7);
    }
    _dropdownRef;
    _treeRef;
    _text;
    get host() { return getElement(this); }
    label;
    heading;
    text;
    value;
    dataSet;
    dataSource;
    disabled;
    nullable = false;
    fieldId = 'id';
    fieldLabel = 'label';
    fieldChildren = 'children';
    expandLevel = 2;
    nodeSelectableRule;
    size = 'Default';
    partialLoad = false;
    loading = 'lazy';
    class;
    valid;
    required;
    searchText;
    searchable = false;
    selection = 'single';
    timeStamp;
    valueChanged;
    textChanged;
    searchChanged;
    valueWatcher(newValue, oldValue) {
        if (arrayEqual(newValue, oldValue))
            return;
        this.updateValue();
        this.valueChanged.emit(this.value);
    }
    textWatcher() {
        this.textChanged.emit(this.text);
    }
    dataSetWatcher() {
        this._treeRef.dataSet = this.dataSet;
    }
    updateValue() {
        if (this._treeRef.value != this.value)
            this._treeRef.value = this.value;
        if (this._dropdownRef.value != this.value)
            this._dropdownRef.value = this.value;
    }
    componentDidLoad() {
        this.updateValue();
    }
    renderTree() {
        return (h("storm-tree-view", { ref: elm => (this._treeRef = elm), partialLoad: this.partialLoad, loading: this.loading, value: this.value, class: this.class, dataSet: this.dataSet, dataSource: this.dataSource, fieldId: this.fieldId, fieldLabel: this.fieldLabel, fieldChildren: this.fieldChildren, nullable: this.nullable, selection: this.selection, expandLevel: this.expandLevel, nodeSelectableRule: this.nodeSelectableRule, searchable: this.searchable, onValueChanged: ev => {
                ev.stopPropagation();
                this.value = ev.detail;
            }, onNodeClick: async (ev) => {
                ev.stopPropagation();
                await this._dropdownRef.hide();
            }, onSearchChanged: ev => {
                ev.stopPropagation();
                this.searchChanged.emit(ev.detail);
            } }));
    }
    async componentWillRender() {
        this._text = this.text;
        if (this._text == null) {
            if (this._treeRef && this.value) {
                const values = Array.isArray(this.value) ? this.value : [this.value];
                const texts = [];
                for (const value of values) {
                    const node = await this._treeRef.getNodeById(value);
                    if (node) {
                        texts.push(await this._treeRef.getLabel(node));
                    }
                }
                this._text = texts.join('、');
            }
            else {
                this._text = undefined;
            }
        }
    }
    render() {
        return (h(Host, { key: 'ac8ce1d7ccb9fa46ec648b5b20f1ce8f21d909a6' }, h("storm-dropdown", { key: '5f46d016a740d82c5300f0d769f8169654963f4a', ref: elm => (this._dropdownRef = elm), nullable: this.nullable, heading: this.heading, label: this.label, text: this._text, value: this.value, showSelected: true, valid: this.valid, required: this.required, disabled: this.disabled, timeStamp: this.timeStamp, type: "select", onValueChanged: ev => {
                ev.stopPropagation();
                this.value = ev.detail;
                if (this.value == null) {
                    this.text = undefined;
                }
            }, onUnselectedAll: async (ev) => {
                ev.stopPropagation();
                await this._treeRef.unselectAll();
            }, onShowMenu: async (ev) => {
                ev.stopPropagation();
                await this._treeRef.loadDataSet();
                if (this.searchable && this._treeRef) {
                    requestAnimationFrame(async () => {
                        await this._treeRef.setSearchFocus();
                    });
                }
            } }, this.renderTree())));
    }
    static get watchers() { return {
        "value": ["valueWatcher"],
        "text": ["textWatcher"],
        "dataSet": ["dataSetWatcher"]
    }; }
};
StormDropdownTree.style = stormDropdownTreeCss;

export { StormDropdownTree as storm_dropdown_tree };
//# sourceMappingURL=storm-dropdown-tree.entry.esm.js.map

//# sourceMappingURL=storm-dropdown-tree.entry.js.map