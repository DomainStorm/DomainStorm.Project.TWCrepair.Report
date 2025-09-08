import { r as registerInstance, a as getElement, h, d as Host } from './index-BpF8IqPI.js';
import { F as FnIcon } from './functional-Cx8laaX_.js';
import { S as SharedStyles } from './shared-styles-B0cWbvbo.js';
import './utils-BOoVSa4-.js';

const stormAccondionItemCss = ":host{display:block}";

const StormAccondionItem = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
    }
    get host() { return getElement(this); }
    expanded = true;
    expandIconAlign = 'right';
    expandIcon;
    collapseIcon;
    handleToggle(e) {
        if (e) {
            e.preventDefault();
            e.stopPropagation();
        }
        this.expanded = !this.expanded;
    }
    async componentWillLoad() {
        await SharedStyles.applyShadowRootStylesAsync(this.host, 'material');
        if (this.expandIcon == null) {
            this.expandIcon = this.expandIconAlign === 'right' ? 'add' : 'chevron_right';
        }
        if (this.collapseIcon == null) {
            this.collapseIcon = this.expandIconAlign === 'right' ? 'remove' : 'expand_more';
        }
    }
    render() {
        return (h(Host, { key: '8660e10272b9f327e97cfcbaa41ffde99e652655' }, h("div", { key: '185e2016e7fe61395a2e2c3bf9d8617e28a9d683', class: "accordion-item" }, h("h5", { key: '68d43c0faf6432222e77ca2e0bb5f85b4aec318e', class: "accordion-header" }, h("button", { key: 'ca15728fd98aef2427c29e1677ba78667eaec3ea', class: {
                'p-2': true,
                'accordion-button': true,
                'font-weight-bold': true,
                collapsed: !this.expanded,
                primary: true,
            }, type: "button", onClick: e => this.handleToggle(e) }, h(FnIcon, { key: 'a3e73873e9bd005bf4d6a7f75ee1a15c3fb7a3bc', class: {
                'mb-1': true,
                'mx-1': true,
                'position-absolute': this.expandIconAlign === 'right',
                'end-0': this.expandIconAlign === 'right',
            } }, this.expanded ? this.collapseIcon : this.expandIcon), h("slot", { key: 'e7a7283b9d0e8a7903986f31aa3b25c8df792aea', name: "storm-accordion-header" }))), h("div", { key: 'f7b5e54fa24e10d3e14ca447bca658bf6e8536b6', class: {
                'accordion-collapse': true,
                'border-top': true,
                collapse: !this.expanded,
            } }, h("div", { key: 'f079bf5040684869f2a83e23bc1ff40eb8d6cdbd', class: "accordion-body p-2" }, h("slot", { key: '78b6c141117b5e0471c62ca425be9f901494c57f', name: "storm-accordion-body" }))))));
    }
};
StormAccondionItem.style = stormAccondionItemCss;

export { StormAccondionItem as storm_accondion_item };
//# sourceMappingURL=storm-accondion-item.entry.esm.js.map

//# sourceMappingURL=storm-accondion-item.entry.js.map