import { r as registerInstance, e as createEvent, h, d as Host } from './index-BpF8IqPI.js';

const stormCheckboxGroupCss = ":host{display:block}";

const StormCheckboxGroup = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.valueChanged = createEvent(this, "valueChanged", 7);
    }
    _dataSet;
    type = 'checkbox';
    labelClasses = 'custom-control-label';
    isSwitch = false;
    isInline = false;
    dataSet;
    value;
    disabled;
    valueChanged;
    dataSetWatcher(newValue) {
        try {
            this._dataSet = typeof newValue === 'string' ? JSON.parse(newValue) : newValue;
        }
        catch (error) {
            console.error('Invalid JSON format:', error);
            return;
        }
        if (this.value) {
            this.valueWatcher(this.value);
        }
        else
            this.value = this.type === 'checkbox' ? this._dataSet.find(x => x.checked)?.id : this._dataSet.filter(x => x.checked).map(x => x.id);
        if (this.type === 'radio') {
            const checkedItems = this._dataSet.filter(x => x.checked);
            if (checkedItems.length === 0) {
                this._dataSet[0].checked = true;
            }
            else if (checkedItems.length > 1) {
                checkedItems
                    .reverse()
                    .slice(1)
                    .forEach(item => (item.checked = false));
            }
        }
    }
    valueWatcher(newValue) {
        if (Array.isArray(newValue))
            this._dataSet.forEach(x => (x.checked = newValue.includes(x.id)));
        else
            this._dataSet.forEach(x => (x.checked = x.id == newValue));
    }
    check(item) {
        if (this.type === 'radio') {
            this.uncheckAll();
        }
        item.checked = true;
        if (this.type === 'radio') {
            this.value = item.id;
        }
        else if (Array.isArray(this.value)) {
            if (!this.value.includes(item.id))
                this.value = [...this.value, item.id];
        }
        else {
            this.value = [item.id];
        }
    }
    uncheck(item) {
        item.checked = false;
        if (this.type === 'radio') {
            if (this.value === item.id) {
                this.value = null;
            }
        }
        else if (Array.isArray(this.value)) {
            this.value = this.value.filter(id => id !== item.id);
        }
    }
    uncheckAll() {
        if (this._dataSet) {
            this._dataSet.filter(x => x.checked).forEach(x => (x.checked = false));
        }
    }
    componentWillLoad() {
        if (this.dataSet) {
            this.dataSetWatcher(this.dataSet);
        }
    }
    dataSetRender() {
        return this._dataSet.map(item => {
            if (!item.id)
                item.id = item.label;
            return (h("storm-checkbox", { type: this.type, label: item.label, labelClasses: item.labelClasses ?? this.labelClasses, disabled: this.disabled || item.disabled, checked: item.checked, isSwitch: this.isSwitch, isInline: this.isInline, onValueChanged: ev => {
                    if (ev.detail) {
                        this.check(item);
                    }
                    else {
                        this.uncheck(item);
                    }
                } }));
        });
    }
    render() {
        return h(Host, { key: 'f965177356115f1631386a8f879d73d6878d7420' }, this.dataSetRender());
    }
    static get watchers() { return {
        "dataSet": ["dataSetWatcher"],
        "value": ["valueWatcher"]
    }; }
};
StormCheckboxGroup.style = stormCheckboxGroupCss;

export { StormCheckboxGroup as storm_checkbox_group };
//# sourceMappingURL=storm-checkbox-group.entry.esm.js.map

//# sourceMappingURL=storm-checkbox-group.entry.js.map