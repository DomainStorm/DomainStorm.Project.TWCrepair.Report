import { r as registerInstance, e as createEvent, h, d as Host } from './index-BpF8IqPI.js';
import { p as parseClassNames, g as generateUid } from './utils-BOoVSa4-.js';

const stormCheckboxCss = ":host{display:block}:host>.form-check{padding-left:0.25rem}:host>.form-check.form-switch{padding-left:2.75rem}:host>.form-check.form-switch .form-check-input{margin-top:.3rem}:host>.form-check.form-check-inline{margin-right:.75rem}:host>.form-check:not(.form-switch) .form-check-input[type=checkbox]{margin-top:0}:host .input-group.input-group-static label{margin-left:.25rem;margin-top:.15rem}:host label{cursor:pointer}";

const StormCheckbox = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.valueChanged = createEvent(this, "valueChanged", 7);
    }
    type = 'checkbox';
    label;
    labelClasses = 'custom-control-label';
    disabled;
    checked;
    indeterminate;
    isSwitch = false;
    isInline = false;
    valueChanged;
    _uid;
    async check() {
        if (this.checked)
            return;
        this.checked = true;
        this.valueChanged.emit(this.checked);
    }
    async uncheck() {
        if (!this.checked)
            return;
        this.checked = false;
        this.valueChanged.emit(this.checked);
    }
    renderLabel() {
        if (!this.label)
            return;
        return (h("label", { htmlFor: this._uid, class: parseClassNames(this.labelClasses) }, this.label));
    }
    render() {
        if (!this._uid)
            this._uid = generateUid('storm-checkbox');
        return (h(Host, { key: '20911f774c3ab182b2d12b5fe9bd3d9ee745534e' }, h("div", { key: 'eec8dfdc36ea732595edd2c6a1b84cdc42dd086b', class: { 'form-check': true, 'form-check-inline': this.isInline, 'form-switch': this.isSwitch } }, h("input", { key: 'ddf54f2cd01cbf539eb7d4d288e80b29ed387f05', class: { 'form-check-input': true }, id: this._uid, type: this.type, checked: this.checked, disabled: this.disabled, indeterminate: this.indeterminate, onChange: async (ev) => {
                ev.stopPropagation();
                if (ev.target['checked'])
                    await this.check();
                else
                    await this.uncheck();
            } }), this.renderLabel())));
    }
};
StormCheckbox.style = stormCheckboxCss;

export { StormCheckbox as storm_checkbox };
//# sourceMappingURL=storm-checkbox.entry.esm.js.map

//# sourceMappingURL=storm-checkbox.entry.js.map