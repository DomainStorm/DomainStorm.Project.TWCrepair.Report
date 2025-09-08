import { r as registerInstance, e as createEvent, h, d as Host } from './index-BpF8IqPI.js';
import { g as generateUid, r as resolvePropertyValue, p as parseClassNames } from './utils-BOoVSa4-.js';

const stormToolbarItemCss = ":host{display:block}";

const StormToolbarItem = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.toolbarAction = createEvent(this, "toolbarAction", 7);
    }
    uid;
    dataSet;
    toolbarAction;
    _active;
    _disabled;
    handleToolbarAction(e) {
        e.stopPropagation();
        this.toolbarAction.emit(this.dataSet.id);
    }
    initDataSet() {
        if (this.dataSet.id) {
            this.uid = this.dataSet.id;
        }
        else if (!this.uid) {
            this.dataSet.id = this.uid = generateUid('storm-toolbar-item');
        }
    }
    componentWillLoad() {
        this.initDataSet();
    }
    async componentWillRender() {
        const promises = [];
        if (this.dataSet.active) {
            promises.push(this.resolveActive().then(result => (this._active = result)));
        }
        if (this.dataSet.disabled) {
            promises.push(this.resolveDisabled().then(result => (this._disabled = result)));
        }
        if (promises.length > 0) {
            await Promise.all(promises);
        }
    }
    async resolveActive() {
        const result = resolvePropertyValue(this.dataSet, 'active');
        return result instanceof Promise ? await result : result;
    }
    async resolveDisabled() {
        const result = resolvePropertyValue(this.dataSet, 'disabled');
        return result instanceof Promise ? await result : result;
    }
    render() {
        return (h(Host, { key: '0a3ad1590d80a175f241c5bf32ee3440bfb4d48b', class: parseClassNames(this.dataSet.class) }, h("storm-button", { key: '3c00e16ecd970ec528ecbeda2f70a5da671ba32b', type: this.dataSet.buttonType, label: this.dataSet.label, tooltip: this.dataSet.tooltip, icon: this.dataSet.icon, buttonStyle: this.dataSet.buttonStyle, size: this.dataSet.size, active: this._active, disabled: this._disabled, onButtonClick: event => this.handleToolbarAction(event), rounded: this.dataSet.rounded })));
    }
};
StormToolbarItem.style = stormToolbarItemCss;

export { StormToolbarItem as storm_toolbar_item };
//# sourceMappingURL=storm-toolbar-item.entry.esm.js.map

//# sourceMappingURL=storm-toolbar-item.entry.js.map