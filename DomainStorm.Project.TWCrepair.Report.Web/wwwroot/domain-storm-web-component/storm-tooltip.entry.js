import { r as registerInstance, a as getElement, h, d as Host } from './index-BpF8IqPI.js';
import { b as bootstrap_bundleExports } from './bootstrap.bundle-IsdR7fTW.js';
import './_commonjsHelpers-Cf5sKic0.js';

const stormTooltipCss = "";

const StormTooltip = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
    }
    _tooltip;
    get host() { return getElement(this); }
    heading;
    placement = 'bottom';
    trigger = 'hover';
    componentDidRender() {
        this._tooltip?.dispose();
        if (this.heading) {
            this._tooltip = new bootstrap_bundleExports.Tooltip(this.host.querySelector('div'), {
                title: this.heading,
                placement: this.placement,
                trigger: this.trigger,
            });
            this.host.querySelector('div').addEventListener('click', () => {
                this._tooltip.hide();
            });
        }
    }
    render() {
        return (h(Host, { key: '28be6a6780300925c330aa9d49b1ddbf26b934fd' }, h("div", { key: 'dd6fdcbeee0a6bbfac1f2ec0c2136cd5fa290193', class: "d-inline-block" }, h("slot", { key: '0ae56a188125db465ae40a4e7fe03efa55aa2891' }))));
    }
    disconnectedCallback() {
        this._tooltip?.dispose();
    }
};
StormTooltip.style = stormTooltipCss;

export { StormTooltip as storm_tooltip };
//# sourceMappingURL=storm-tooltip.entry.esm.js.map

//# sourceMappingURL=storm-tooltip.entry.js.map