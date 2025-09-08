import { r as registerInstance, e as createEvent, h } from './index-BpF8IqPI.js';
import { i as isModuleFunctionString, e as createFunctionFromString, p as parseClassNames } from './utils-BOoVSa4-.js';

const stormToolbarCss = ":host{display:block}";

const StormToolbar = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.toolbarAction = createEvent(this, "toolbarAction", 7);
    }
    dataSet;
    timeStamp;
    class;
    toolbarAction;
    renderToolbarItem(item) {
        if (typeof item.visible === 'function' && !item.visible())
            return;
        if (typeof item.visible === 'boolean' && !item.visible)
            return;
        if (item.type === 'group') {
            return this.renderDropdown(item);
        }
        else if (item.type === 'divider') {
            return (h("div", { class: "d-inline-flex align-middle mx-2" }, h("div", { class: "vr" })));
        }
        else {
            return (h("storm-toolbar-item", { dataSet: item, onToolbarAction: e => {
                    this.handleAction(e, item);
                } }));
        }
    }
    handleAction(_, item, dropdownItem = null) {
        if (!item.click)
            return;
        if (typeof item.click === 'string') {
            if (!isModuleFunctionString(item.click)) {
                throw new Error('Invalid function string');
            }
            item.click = createFunctionFromString(item.click);
        }
        item.click(item, dropdownItem);
        this.toolbarAction.emit(dropdownItem ? dropdownItem.id : item.id);
    }
    renderDropdown(item) {
        if (!item || !item.items || item.items.length === 0)
            return;
        return (h("div", { class: "d-inline-flex align-middle" }, h("storm-dropdown", { dataSet: item.items, trigger: "click", toggleIcon: "more_vert", type: "none", selection: "none", nullable: true, showSelected: false, onItemClick: e => {
                this.handleAction(e, item, e.detail);
            } })));
    }
    render() {
        if (!this.dataSet)
            return;
        return (h("div", { class: "w-100" }, h("div", { class: { 'd-flex': true, ...parseClassNames(this.class) } }, this.dataSet.map(x => {
            return this.renderToolbarItem(x);
        }))));
    }
};
StormToolbar.style = stormToolbarCss;

export { StormToolbar as storm_toolbar };
//# sourceMappingURL=storm-toolbar.entry.esm.js.map

//# sourceMappingURL=storm-toolbar.entry.js.map