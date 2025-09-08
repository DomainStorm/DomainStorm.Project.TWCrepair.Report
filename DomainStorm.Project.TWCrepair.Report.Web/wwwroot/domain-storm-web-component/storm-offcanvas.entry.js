import { r as registerInstance, a as getElement, h } from './index-BpF8IqPI.js';
import { b as bootstrap_bundleExports } from './bootstrap.bundle-IsdR7fTW.js';
import { S as SharedStyles } from './shared-styles-B0cWbvbo.js';
import './_commonjsHelpers-Cf5sKic0.js';

const stormOffcanvasCss = "";

const StormOffCanvas = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
    }
    get host() { return getElement(this); }
    placement = 'end';
    backdrop = false;
    scrollable = false;
    offCanvas;
    offCanvasRef;
    async componentWillLoad() {
        await SharedStyles.applyShadowRootStylesAsync(this.host, 'material');
    }
    componentDidLoad() {
        this.offCanvas = new bootstrap_bundleExports.Offcanvas(this.offCanvasRef, {
            backdrop: this.backdrop,
            scroll: this.scrollable,
        });
    }
    async toggle() {
        await this.offCanvas.toggle();
    }
    async show() {
        await this.offCanvas.show();
    }
    async hide() {
        await this.offCanvas.hide();
    }
    render() {
        return (h("div", { key: '4a9f8481bfc3088fa7753f3f117d63f53839b84c', class: 'offcanvas offcanvas-' + this.placement, ref: e => (this.offCanvasRef = e) }, h("div", { key: '9cf88f6fa523febfda02a2cf03f23cfdf4efa93c', class: "offcanvas-header pb-0 pt-3" }, h("div", { key: '0d79dba4c050c06e20fc06637e104a339ab5eb7e', class: "float-start ms-4" }, h("slot", { key: '26603676e4cd69e60397ada2b6e79dd86fb6a570', name: "header" })), h("button", { key: 'f3e7fd1bf9cba81fabfeba50928eb5d39c015c0c', class: "btn btn-link text-dark p-0 fixed-plugin-close-button me-2", onClick: () => this.hide() }, h("i", { key: '9b6e3696b9cbb1e5a6cb9f4cdfdb6a9afca881cd', class: "material-icons" }, "clear"))), h("hr", { key: '0d11776a4de710d93c0063326a24971a01a8ad27', class: "horizontal dark my-1" }), h("div", { key: '65a3806ae722976e9a4fa52c6bd9b60135743eee', class: "offcanvas-body" }, h("slot", { key: '911fd979af3ee0ef1fe9a82d96a10a20e1edcab7', name: "body" }))));
    }
};
StormOffCanvas.style = stormOffcanvasCss;

export { StormOffCanvas as storm_offcanvas };
//# sourceMappingURL=storm-offcanvas.entry.esm.js.map

//# sourceMappingURL=storm-offcanvas.entry.js.map