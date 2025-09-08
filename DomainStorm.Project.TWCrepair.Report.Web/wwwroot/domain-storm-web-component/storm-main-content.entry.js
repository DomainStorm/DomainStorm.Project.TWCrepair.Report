import { r as registerInstance, e as createEvent, a as getElement, h, d as Host } from './index-BpF8IqPI.js';
import { P as PerfectScrollbar } from './perfect-scrollbar.esm-hw42mw03.js';

const stormMainContentCss = ".main-content{margin-left:17.125rem}@media (max-width: 1199.98px){.main-content{margin-left:0 !important}}";

const StormMainContent = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.scroll = createEvent(this, "scroll", 7);
    }
    scroll;
    get host() { return getElement(this); }
    dataSet;
    navbarPosition;
    searchable = false;
    _dataSet;
    dataSetWatcher(newValue) {
        if (typeof newValue === 'string') {
            this._dataSet = JSON.parse(newValue);
        }
        else {
            this._dataSet = newValue;
        }
    }
    componentWillLoad() {
        if (!this.dataSet)
            return;
        this.dataSetWatcher(this.dataSet);
    }
    componentDidLoad = () => {
        const container = this.host.querySelector('.ps');
        new PerfectScrollbar(container);
        const scroll = this.scroll;
        let lastKnownScrollPosition = 0;
        let ticking = false;
        container.addEventListener('scroll', (e) => {
            // lastKnownScrollPosition = window.scrollY;
            lastKnownScrollPosition = container.scrollTop | e.detail;
            if (!ticking) {
                window.requestAnimationFrame(function () {
                    scroll.emit(lastKnownScrollPosition);
                    ticking = false;
                });
                ticking = true;
            }
            e.stopPropagation();
        });
    };
    render() {
        return (h(Host, { key: 'baf08dc0146426071d0a79e1ffbcfd511ba8b934', class: "position-absolute w-100 h-100" }, h("main", { key: '33a8fd44226ea064d0af2fc34c33ba396cc35e92', class: "main-content position-relative max-height-vh-100 h-100 border-radius-lg ps ps--active-y" }, h("storm-navbar", { key: 'f789ba4beeaffc2e1e923ce11abdd291786cfa9f', dataSet: this._dataSet, position: this.navbarPosition, searchable: this.searchable }), h("slot", { key: '4f85068ad1a7ac0ac0e4b1a49f0736c7adc56d5b', name: "item-container" }))));
    }
    static get watchers() { return {
        "dataSet": ["dataSetWatcher"]
    }; }
};
StormMainContent.style = stormMainContentCss;

export { StormMainContent as storm_main_content };
//# sourceMappingURL=storm-main-content.entry.esm.js.map

//# sourceMappingURL=storm-main-content.entry.js.map