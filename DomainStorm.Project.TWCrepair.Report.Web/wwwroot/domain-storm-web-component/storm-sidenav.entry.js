import { r as registerInstance, e as createEvent, a as getElement, h, d as Host } from './index-BpF8IqPI.js';
import { P as PerfectScrollbar } from './perfect-scrollbar.esm-hw42mw03.js';

const stormSidenavCss = "@font-face{font-family:'Material Icons Round';font-style:normal;font-weight:400;src:url(../font/material-icons-round/v93/LDItaoyNOAY6Uewc665JcIzCKsKc_M9flwmP.woff2) format('woff2')}.material-icons-round{font-family:'Material Icons Round';font-weight:normal;font-style:normal;font-size:24px;line-height:1;letter-spacing:normal;text-transform:none;display:inline-block;white-space:nowrap;word-wrap:normal;direction:ltr;-webkit-font-feature-settings:'liga';-webkit-font-smoothing:antialiased}.sidenav{position:absolute !important}@media (max-width: 1199.98px){.sidenav{transform:translateX(-17.125rem)}}";

const StormSideNav = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.change = createEvent(this, "change", 7);
    }
    get host() { return getElement(this); }
    name = '';
    image = 'logo-ct.png';
    basePath;
    dataSet;
    value;
    change;
    _collapse;
    selectedFunction;
    buildClasses() {
        return 'sidenav navbar navbar-vertical navbar-expand-xs border-0 border-radius-xl my-3 fixed-start ms-3 bg-gradient-dark ps';
    }
    onChange = (event) => {
        event.preventDefault();
        this.selectedFunction = event.detail;
        this.value = this.selectedFunction.id;
        this.change.emit(event.detail);
    };
    async getValue() {
        return this.selectedFunction;
    }
    async setLabel(fun) {
        await this._collapse.setLabel(fun);
    }
    componentDidLoad() {
        const container = this.host.querySelector('.ps');
        new PerfectScrollbar(container);
    }
    render() {
        return (h(Host, { key: '69aae2a495559d3b410033a70a390ebf729557fc' }, h("aside", { key: '31320f39d68e471e33a9db4fcf458c453a12b583', class: this.buildClasses() }, h("storm-sidenav-header", { key: '7c6f4cdfb058ac076fa0409c6fa0257c4da9125b', name: this.name, image: this.image, basePath: this.basePath }), h("slot", { key: '3d7d874f65e60f1a7bb7b8060a158b173aeb46e2' }), h("storm-sidenav-collapse", { key: '0fb28a7c918dc47eac788300d8102fd1972ad2a4', ref: el => (this._collapse = el), dataSet: this.dataSet, value: this.value, onChange: this.onChange }))));
    }
    static get assetsDirs() { return ["assets"]; }
};
StormSideNav.style = stormSidenavCss;

export { StormSideNav as storm_sidenav };
//# sourceMappingURL=storm-sidenav.entry.esm.js.map

//# sourceMappingURL=storm-sidenav.entry.js.map