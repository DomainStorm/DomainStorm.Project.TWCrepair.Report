import { r as registerInstance, h, i as getAssetPath } from './index-BpF8IqPI.js';

const stormSidenavHeaderCss = "@media (min-width: 1200px){.g-sidenav-hidden .navbar-vertical .nav-item .nav-link .nav-link-text,.g-sidenav-hidden .navbar-vertical .nav-item .nav-link .sidenav-normal{height:0}}";

const StormSidenavHeader = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
    }
    name;
    image;
    basePath = "/";
    render() {
        return (h("div", { key: 'e7c1e85d9134a32a2fbda2f9bbbd164144ad006a', class: "sidenav-header" }, h("i", { key: 'b2336a44f6011ae91986257159b90ccd3cdccbe3', class: "fas fa-times p-3 cursor-pointer text-white opacity-5 position-absolute end-0 top-0 d-none d-xl-none", "aria-hidden": "true", id: "iconSidenav" }), h("a", { key: '9e60b766e81eb09ed3dea9049f1c0f65aa69d6db', class: "navbar-brand m-0", href: `${this.basePath}` }, h("img", { key: '270a60f6ee50dcbfba77924db272733ca3974294', src: getAssetPath(`../../img/${this.image}` /*`./assets/${this.image}`*/), class: "navbar-brand-img h-100", alt: "main_logo" }), h("span", { key: 'fdbf5e2334cfb8ae224cd76b0477b2922adf9f17', class: "ms-1 font-weight-bold text-white" }, this.name))));
    }
    static get assetsDirs() { return ["assets"]; }
};
StormSidenavHeader.style = stormSidenavHeaderCss;

export { StormSidenavHeader as storm_sidenav_header };
//# sourceMappingURL=storm-sidenav-header.entry.esm.js.map

//# sourceMappingURL=storm-sidenav-header.entry.js.map