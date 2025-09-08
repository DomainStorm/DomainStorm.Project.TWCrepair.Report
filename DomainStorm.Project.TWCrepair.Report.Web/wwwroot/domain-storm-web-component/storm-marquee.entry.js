import { r as registerInstance, a as getElement, h, d as Host } from './index-BpF8IqPI.js';
import { S as SharedStyles } from './shared-styles-B0cWbvbo.js';

const stormMarqueeCss = ":host{--speed:30s}:host .marquee{white-space:nowrap;overflow:hidden;box-sizing:border-box}:host .inner{display:inline-block;padding-left:100%;animation:marquee var(--speed) linear infinite}@keyframes marquee{0%{transform:translate(0, 0)}100%{transform:translate(-100%, 0)}}:host .center{display:flex;justify-content:center;align-items:center}";

const StormMarquee = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
    }
    get host() { return getElement(this); }
    width = '100%';
    speed;
    async componentWillLoad() {
        await SharedStyles.applyShadowRootStylesAsync(this.host, 'material');
    }
    componentDidRender() {
        if (!this.speed)
            return;
        this.host.style.setProperty('--speed', this.speed);
    }
    render() {
        return (h(Host, { key: '9f90426ba1935d4caf6b0e93156522ca26829794' }, h("div", { key: 'dde4a14c1794337e575d58caa8a11fc4b6d93a9f', class: "container" }, h("div", { key: '7777b4a3e9b13ee791e2b30e3b4f3965651a8c60', class: "row" }, h("div", { key: '3341a76f2bbfe8d8e2ce9dfce65319523cacd488', class: "center" }, h("div", { key: '2c07ac69126f1e3b3c3b66c3711f395290e72642', class: "marquee", style: { width: this.width } }, h("div", { key: '676bd8f57bca4c950a54e2f6a88cbf842935dc91', class: "inner", innerHTML: this.host.innerHTML })))))));
    }
};
StormMarquee.style = stormMarqueeCss;

export { StormMarquee as storm_marquee };
//# sourceMappingURL=storm-marquee.entry.esm.js.map

//# sourceMappingURL=storm-marquee.entry.js.map