import { r as registerInstance, e as createEvent, a as getElement, h } from './index-BpF8IqPI.js';

const stormSidenavCollapseCss = "@font-face{font-family:'Material Icons Round';font-style:normal;font-weight:400;src:url(../font/material-icons-round/v93/LDItaoyNOAY6Uewc665JcIzCKsKc_M9flwmP.woff2) format('woff2')}.material-icons-round{font-family:'Material Icons Round';font-weight:normal;font-style:normal;font-size:24px;line-height:1;letter-spacing:normal;text-transform:none;display:inline-block;white-space:nowrap;word-wrap:normal;direction:ltr;-webkit-font-feature-settings:'liga';-webkit-font-smoothing:antialiased}";

const StormSidenavCollapse = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.change = createEvent(this, "change", 7);
    }
    get host() { return getElement(this); }
    dataSet;
    value;
    change;
    dictionary = {};
    // private functionRefs: HTMLElement[]
    // componentWillRender() {
    //   this.functionRefs = []
    // }
    getClass(fun) {
        let isActive = fun.isActive;
        if (this.value == fun.id)
            isActive = true;
        return `nav-link text-white ${isActive ? 'active bg-gradient-primary' : ''}`;
    }
    onClick = event => {
        event.preventDefault();
        // this.host.querySelectorAll(".active").forEach(e => e.classList.remove('active'));
        // this.host.querySelectorAll(".bg-gradient-primary").forEach(e => e.classList.remove('bg-gradient-primary'));
        // event.target.classList.add('active');
        // event.target.classList.add('bg-gradient-primary');
        this.removeActiveAll();
        this.addActive(event.target);
        this.value = event.target.getAttribute('data-id');
        this.change.emit(this.dictionary[this.value]);
    };
    removeActiveAll() {
        // this.functionRefs.forEach(elem => elem.classList.remove('active','bg-gradient-primary'))
        this.host.querySelectorAll('.active').forEach(elem => elem.classList.remove('active', 'bg-gradient-primary'));
    }
    addActive(e) {
        e.classList.add('active', 'bg-gradient-primary');
    }
    async setLabel(fun) {
        this.dictionary[fun.id] = fun;
        const element = this.host.querySelector(`a[data-id="${fun.id}"] > span`);
        element.textContent = fun.name;
    }
    render() {
        // this.functionRefs = []
        return (h("ul", { key: '51b4896ffe2418ef583e74b8b6a317fc7d5a7c5a', class: "navbar-nav" }, this.dataSet?.map((fun) => {
            this.dictionary[fun.id] = fun;
            return (h("ul", { class: "nav " }, h("li", { class: "nav-item mt-3" }, h("h6", { class: "ps-4  ms-2 text-uppercase text-xs font-weight-bolder text-white" }, fun.name)), fun.children?.map((sub) => {
                this.dictionary[sub.id] = sub;
                return (h("li", { class: "nav-item" }, h("a", { class: this.getClass(sub), href: sub.href, onClick: e => this.onClick(e), "data-id": sub.id }, h("i", { class: "material-icons opacity-10" }, sub.icon), h("span", { class: "nav-link-text ms-2 ps-1" }, sub.name))));
            })));
        })));
    }
};
StormSidenavCollapse.style = stormSidenavCollapseCss;

export { StormSidenavCollapse as storm_sidenav_collapse };
//# sourceMappingURL=storm-sidenav-collapse.entry.esm.js.map

//# sourceMappingURL=storm-sidenav-collapse.entry.js.map