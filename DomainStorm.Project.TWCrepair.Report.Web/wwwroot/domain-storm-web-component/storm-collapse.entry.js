import { r as registerInstance, a as getElement, h, d as Host } from './index-BpF8IqPI.js';

const stormCollapseCss = "";

const StormCollapse = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
    }
    get host() { return getElement(this); }
    aClass = 'btn btn-primary';
    buttonName;
    for = 'collapseExample';
    getHref = () => {
        let s = '#';
        s += this.for;
        return s;
    };
    render() {
        return (h(Host, { key: 'c48c0c604a2dfb27dd0517e18eebeb64bb938f15' }, h("a", { key: '3cea307a67fe0539c35d966fb502a1f336f0be8c', class: this.aClass, "data-bs-toggle": "collapse", href: this.getHref(), role: "button", "aria-expanded": "false", "aria-controls": this.for }, this.buttonName)));
    }
};
StormCollapse.style = stormCollapseCss;

export { StormCollapse as storm_collapse };
//# sourceMappingURL=storm-collapse.entry.esm.js.map

//# sourceMappingURL=storm-collapse.entry.js.map