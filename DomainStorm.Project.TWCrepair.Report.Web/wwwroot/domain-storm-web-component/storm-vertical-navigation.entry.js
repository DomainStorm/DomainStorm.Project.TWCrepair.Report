import { r as registerInstance, e as createEvent, a as getElement, f as forceUpdate, h, d as Host } from './index-BpF8IqPI.js';
import { S as SharedStyles } from './shared-styles-B0cWbvbo.js';

const stormVerticalNavigationCss = "";

const StormVerticalNavigation = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.scroll = createEvent(this, "scroll", 7);
        this.checkedChanged = createEvent(this, "checkedChanged", 7);
    }
    _dataSet;
    verticalSLotRef;
    treeViewRef;
    verticalList;
    navigationList;
    verticalListReverse;
    verticals;
    isDebouncing = false;
    ticking = false;
    get host() { return getElement(this); }
    treeViewable = true;
    dataSet;
    fixTop = 10;
    scrollY;
    selectToCheck = false;
    checkSelectable = false;
    scroll;
    checked;
    checkedChanged;
    fieldIcon = 'icon';
    fieldSelected = '_selected';
    value;
    dataSetWatcher(newValue) {
        if (typeof newValue === 'string') {
            this._dataSet = JSON.parse(newValue);
        }
        else {
            this._dataSet = newValue;
        }
    }
    scrollHandler(e) {
        if (this.isDebouncing)
            return;
        const newScrollY = e.detail | window.scrollY;
        if (this.scrollY === newScrollY)
            return;
        this.scrollY = newScrollY;
        this.requestFrame();
        e.stopPropagation();
    }
    innerScrollHandler(e) {
        if (this.isDebouncing)
            return;
        const newScrollY = e.detail | window.scrollY;
        if (this.scrollY === newScrollY)
            return;
        this.scrollY = newScrollY;
        this.requestFrame();
        e.stopPropagation();
    }
    requestFrame() {
        if (this.ticking)
            return;
        this.ticking = true;
        requestAnimationFrame(() => {
            this.ticking = false;
            this.updateFrame();
        });
    }
    updateFrame() {
        this.refresh();
        this.triggerActive();
        this.scroll.emit(this.scrollY);
    }
    async refresh() {
        this.setVerticalData();
    }
    async getChecked() {
        return this.treeViewRef.checked;
    }
    async componentWillLoad() {
        await SharedStyles.applyShadowRootStylesAsync(this.host, 'material');
        if (!this.dataSet)
            return;
        this.dataSetWatcher(this.dataSet);
    }
    componentDidRender() {
        if (this.verticalList && this.navigationList)
            return;
        this.getVertical();
        this.setNavigationData();
    }
    componentDidLoad() {
        this.treeViewRef.addEventListener('nodeSelect', async (e) => {
            const node = await this.treeViewRef.getNodeById(e.detail);
            window.location.replace(window.location.origin + window.location.pathname + window.location.search + node['href']);
            this.isDebouncing = true;
            setTimeout(() => {
                this.isDebouncing = false;
            }, 1);
        });
    }
    getVertical() {
        this.verticals = this.verticalSLotRef.assignedElements();
        if (this.verticals.length == 0)
            return;
        this.setVerticalData();
    }
    getScrollContainer(elem) {
        while (elem) {
            if (elem.classList.contains('ps')) {
                break;
            }
            elem = elem.parentElement;
        }
        return elem;
    }
    setVerticalData() {
        this.verticalList = [];
        this.verticals.map(elem => {
            this.verticalList.push({
                href: '#' + elem.getAttribute('id'),
                top: elem.getBoundingClientRect().top + this.scrollY,
            });
        });
        this.verticalListReverse = this.verticalList.slice().reverse();
    }
    setNavigationData() {
        let treeNodes = this.treeViewRef.shadowRoot.querySelectorAll('storm-tree-node');
        if (treeNodes.length == 0)
            return;
        this.navigationList = [];
        treeNodes.forEach(elem => {
            this.navigationList.push(elem);
            if (elem.children) {
                elem.querySelectorAll('storm-tree-node').forEach(elemChild => this.navigationList.push(elemChild));
            }
        });
    }
    triggerActive() {
        const viewportMidpoint = this.scrollY + window.innerHeight / 2;
        const activeElem = this.verticalListReverse.find(elem => elem.top <= viewportMidpoint);
        this.navigationList.forEach(navElem => {
            const isActive = navElem.dataSet['href'] === activeElem?.href;
            if (isActive && !navElem.dataSet[this.fieldSelected]) {
                this.addActive(navElem);
            }
            else if (!isActive && navElem.dataSet[this.fieldSelected]) {
                this.removeActive(navElem);
            }
        });
    }
    addActive(elem) {
        //let listItem = elem.querySelector('a');
        //listItem.classList.add('active');
        elem.dataSet[this.fieldSelected] = true;
        forceUpdate(elem);
    }
    removeActive(elem) {
        //let listItem = elem.querySelector('a');
        //listItem.classList.remove('active');
        elem.dataSet[this.fieldSelected] = false;
        forceUpdate(elem);
    }
    treeViewRender() {
        if (!this.treeViewable)
            return;
        return (h("storm-tree-view", { class: "p-3", selection: "single", dataSet: this._dataSet, selectToCheck: this.selectToCheck, checkSelectable: this.checkSelectable, checked: this.checked, fieldIcon: this.fieldIcon, fieldSelected: this.fieldSelected, value: this.value, onCheckedChanged: ev => this.handleCheckedChanged(ev), ref: e => (this.treeViewRef = e) }));
    }
    handleCheckedChanged(ev) {
        ev.stopPropagation();
        this.checkedChanged.emit(ev.detail);
    }
    render() {
        return (h(Host, { key: '9fbc72910d15dacd6918c57499a856315f998b85' }, h("div", { key: '4aa546d139ffc5a57c9ff296ec59025452946f43', class: "container-fluid my-3 py-3" }, h("div", { key: '44e09e79de158ebe74d1c688d0e90295b94e4a8a', class: "row mb-5" }, h("div", { key: '8380deb4235916d40a270fd67003ddaa2d69769a', class: "col-lg-9 mt-lg-0 mt-4" }, h("slot", { key: 'd4af3e1b00ebaae114144078733c6054d764f1e3', name: "vertical", ref: (e) => (this.verticalSLotRef = e) })), h("div", { key: '367289166f9ded6798b9cd46df6dae16dcc8e372', class: "col-lg-3" }, h("div", { key: '895c744fc347ef2e020a5571f64390a9cc21aea3', class: 'position-sticky ' + 'top-' + this.fixTop }, h("slot", { key: 'e5853c316ddca690d2a0c41deb60e3cb400dece4', name: "navigation-header" }), h("div", { key: '0c8f0593755de703da1d716900bd77e51013224f', class: "card" }, this.treeViewRender())))))));
    }
    static get watchers() { return {
        "dataSet": ["dataSetWatcher"]
    }; }
};
StormVerticalNavigation.style = stormVerticalNavigationCss;

export { StormVerticalNavigation as storm_vertical_navigation };
//# sourceMappingURL=storm-vertical-navigation.entry.esm.js.map

//# sourceMappingURL=storm-vertical-navigation.entry.js.map