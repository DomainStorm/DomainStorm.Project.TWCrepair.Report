import { r as registerInstance, a as getElement, h, d as Host } from './index-BpF8IqPI.js';
import { S as SharedStyles } from './shared-styles-B0cWbvbo.js';

const stormAccordionCss = ":host{display:block}";

const StormAccordion = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
    }
    get host() { return getElement(this); }
    async componentWillLoad() {
        await SharedStyles.applyShadowRootStylesAsync(this.host, 'material');
    }
    render() {
        return (h(Host, { key: '688529fd185bccbedeb9c5953276024ab8745235' }, h("div", { key: '1fbf163bf4c5b7d56fd7863e1f8b9bdcded1144a', class: "card m-1" }, h("div", { key: '71a6a6aa72596c5c014ca8dc51d2bbcd6631ed71', class: "card-body px-1 py-2" }, h("div", { key: '51812038976d7e66ea52ab78d8a573c6861e3465', class: "accordion" }, h("slot", { key: '1044c02546c51c30806941c235a0aed94ebcbc9f' }))))));
    }
};
StormAccordion.style = stormAccordionCss;

export { StormAccordion as storm_accordion };
//# sourceMappingURL=storm-accordion.entry.esm.js.map

//# sourceMappingURL=storm-accordion.entry.js.map