import { r as registerInstance, a as getElement, h, d as Host } from './index-BpF8IqPI.js';

const stormHorizontalSplitViewCss = ":host{display:block}";

const StormHorizontalSplitView = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
    }
    _splitViewRef;
    get host() { return getElement(this); }
    splitPercent = 50;
    fixedSize;
    fixedPane;
    topPaneVisible;
    bottomPaneVisible;
    async viewToggle(token, force) {
        await this._splitViewRef.viewToggle(token === 'top' ? 'first' : 'second', force);
    }
    componentWillLoad() {
        this.topPaneVisible = this.topPaneVisible ?? this.host.querySelector(':scope>[slot="top-pane"]') != null;
        this.bottomPaneVisible = this.bottomPaneVisible ?? this.host.querySelector(':scope>[slot="bottom-pane"]') != null;
    }
    render() {
        return (h(Host, { key: 'c6245fcfed4bce7a7b22f8696a8ea9e2b78711d2' }, h("storm-split-view", { key: '05a4317b92260c76bffdeae362153ef9bb303564', ref: elm => (this._splitViewRef = elm), splitPercent: this.splitPercent, fixedSize: this.fixedSize, fixedPane: this.fixedPane == null ? null : this.fixedPane == 'top' ? 'first' : 'second', firstPaneVisible: this.topPaneVisible, secondPaneVisible: this.bottomPaneVisible, direction: 'horizontal', firstPaneName: 'top-pane', secondPaneName: 'bottom-pane' }, h("slot", { key: 'c30f7e0a7d709b870fd0b157723ac1887037066d', name: "top-pane" }), h("slot", { key: 'fe7999e2bcb4950081ca9f4aa99ae395342b9709', name: "bottom-pane" }))));
    }
};
StormHorizontalSplitView.style = stormHorizontalSplitViewCss;

export { StormHorizontalSplitView as storm_horizontal_split_view };
//# sourceMappingURL=storm-horizontal-split-view.entry.esm.js.map

//# sourceMappingURL=storm-horizontal-split-view.entry.js.map