import { r as registerInstance, e as createEvent, a as getElement, h, d as Host } from './index-BpF8IqPI.js';
import { g as generateUid } from './utils-BOoVSa4-.js';

const stormTabsCss = ":host{display:block}.tab-content{padding:.25rem .5rem .5rem .5rem}";

const StormTabs = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.activeChanged = createEvent(this, "activeChanged", 7);
    }
    _tabRefs = {};
    get host() { return getElement(this); }
    value;
    dataSet;
    activeChanged;
    async watchDataSetHandler() {
        this.initDataSet();
    }
    async valueHandler() {
        this.updateActiveTab();
    }
    async getTabPane(id) {
        return this._tabRefs[id];
    }
    initDataSet() {
        if (!this.dataSet || this.dataSet.length === 0) {
            return;
        }
        this.dataSet.forEach(x => {
            if (!x.id) {
                x.id = generateUid('storm-nav');
            }
        });
        if (this.value) {
            this.dataSet.forEach(x => {
                x.active = x.id === this.value;
            });
        }
        else if (!this.dataSet.some(x => x.active)) {
            this.dataSet[0].active = true;
            this.value = this.dataSet[0].id;
        }
    }
    renderTabPane(item) {
        if (this.host.querySelector(`:scope>[slot="${item.id}"]`)) {
            const tabPane = this.host.querySelector(`:scope>[slot="${item.id}"]`);
            this._tabRefs[item.id] = tabPane;
            tabPane.classList.add('tab-pane', 'fade');
            if (item.active) {
                tabPane.classList.add('show', 'active');
            }
            else {
                tabPane.classList.remove('show', 'active');
            }
            return h("slot", { name: item.id });
        }
        return h("div", { ref: elm => (this._tabRefs[item.id] = elm), class: { 'tab-pane': true, fade: true, show: item.active, active: item.active } });
    }
    updateActiveTab() {
        if (!this.value)
            return;
        this.dataSet.forEach(x => {
            x.active = x.id === this.value;
            const tabPane = this._tabRefs[x.id];
            if (tabPane) {
                tabPane.classList.toggle('show', x.active);
                tabPane.classList.toggle('active', x.active);
            }
        });
        this.activeChanged.emit(this.value);
    }
    componentWillRender() {
        this._tabRefs = {};
        this.initDataSet();
    }
    render() {
        if (!this.dataSet)
            return;
        return (h(Host, null, h("storm-navs", { value: this.value, dataSet: this.dataSet, onActiveChanged: async (ev) => {
                ev.preventDefault();
                ev.stopPropagation();
                this.value = ev.detail;
            } }), h("div", { class: "tab-content" }, this.dataSet.map(item => {
            return this.renderTabPane(item);
        }))));
    }
    componentDidLoad() {
        this.updateActiveTab();
    }
    static get watchers() { return {
        "dataSet": ["watchDataSetHandler"],
        "value": ["valueHandler"]
    }; }
};
StormTabs.style = stormTabsCss;

export { StormTabs as storm_tabs };
//# sourceMappingURL=storm-tabs.entry.esm.js.map

//# sourceMappingURL=storm-tabs.entry.js.map