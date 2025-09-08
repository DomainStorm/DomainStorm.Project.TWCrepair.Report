import { r as registerInstance, a as getElement, h, d as Host } from './index-BpF8IqPI.js';

const stormVerticalSplitViewCss = "";

const StormVerticalSplitView = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
    }
    _splitViewRef;
    get host() { return getElement(this); }
    splitPercent = 50;
    fixedSize;
    fixedPane;
    leftPaneVisible;
    rightPaneVisible;
    async viewToggle(token, force) {
        await this._splitViewRef.viewToggle(token === 'left' ? 'first' : 'second', force);
    }
    componentWillLoad() {
        this.leftPaneVisible = this.leftPaneVisible ?? this.host.querySelector(':scope>[slot="left-pane"]') != null;
        this.rightPaneVisible = this.rightPaneVisible ?? this.host.querySelector(':scope>[slot="right-pane"]') != null;
    }
    render() {
        return (h(Host, { key: '03b6b9520c96faf172071c2e2e971b3b87a0cc4f' }, h("storm-split-view", { key: '417b6450313b7c1029c4daf7978b630e997e5134', ref: elm => (this._splitViewRef = elm), splitPercent: this.splitPercent, fixedSize: this.fixedSize, fixedPane: this.fixedPane == null ? null : this.fixedPane == 'left' ? 'first' : 'second', firstPaneVisible: this.leftPaneVisible, secondPaneVisible: this.rightPaneVisible, direction: 'vertical', firstPaneName: 'left-pane', secondPaneName: 'right-pane' }, h("slot", { key: '65bd559439499eb96fa5580244e60d6d5518bb35', name: "left-pane" }), h("slot", { key: 'a1e7db477d742ca16c9b7131904d262b092dd79d', name: "right-pane" }))));
    }
};
StormVerticalSplitView.style = stormVerticalSplitViewCss;

export { StormVerticalSplitView as storm_vertical_split_view };
//# sourceMappingURL=storm-vertical-split-view.entry.esm.js.map

//# sourceMappingURL=storm-vertical-split-view.entry.js.map