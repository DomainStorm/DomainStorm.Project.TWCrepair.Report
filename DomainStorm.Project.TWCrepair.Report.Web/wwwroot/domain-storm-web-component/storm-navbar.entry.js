import { r as registerInstance, e as createEvent, h } from './index-BpF8IqPI.js';

const stormNavbarCss = "@font-face{font-family:'Material Icons Round';font-style:normal;font-weight:400;src:url(../font/material-icons-round/v93/LDItaoyNOAY6Uewc665JcIzCKsKc_M9flwmP.woff2) format('woff2')}.material-icons-round{font-family:'Material Icons Round';font-weight:normal;font-style:normal;font-size:24px;line-height:1;letter-spacing:normal;text-transform:none;display:inline-block;white-space:nowrap;word-wrap:normal;direction:ltr;-webkit-font-feature-settings:'liga';-webkit-font-smoothing:antialiased}.pinned .sidenav-toggler-inner .sidenav-toggler-line:first-child,.pinned .sidenav-toggler-inner .sidenav-toggler-line:last-child{width:13px;transform:translateX(5px)}.navbar .sidenav-toggler-inner{width:18px}";

const StormNavbar = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.toggleClick = createEvent(this, "toggleClick", 7);
        this.navbarClick = createEvent(this, "navbarClick", 7);
    }
    navbarRef;
    toggleInnerRef;
    // private checked: boolean = false
    ticking = false;
    scrollY;
    position;
    searchable;
    dataSet;
    checked = false;
    // @Event() toggleClick: EventEmitter<boolean>
    toggleClick;
    navbarClick;
    async toggle() {
        this.checked = !this.checked;
        this.toggleInnerRef.classList.toggle('pinned');
        const sidenavShow = document.getElementsByClassName('g-sidenav-show')[0];
        if (this.checked) {
            sidenavShow.classList.remove('g-sidenav-pinned');
            sidenavShow.classList.add('g-sidenav-hidden');
        }
        else {
            sidenavShow.classList.remove('g-sidenav-hidden');
            sidenavShow.classList.add('g-sidenav-pinned');
        }
    }
    // @Method()
    // async getChecked() {
    //   return this.checked
    // }
    listenScroll(e) {
        this.scrollY = e.detail | window.scrollY;
        if (!this.ticking) {
            requestAnimationFrame(() => {
                if (this.scrollY > 5) {
                    if (this.navbarRef.classList.contains('shadow-none')) {
                        this.navbarRef.classList.remove('shadow-none');
                        this.navbarRef.classList.add('blur', 'shadow-blur', 'left-auto');
                    }
                }
                else {
                    this.navbarRef.classList.remove('blur', 'shadow-blur', 'left-auto');
                    this.navbarRef.classList.add('shadow-none');
                }
                this.ticking = false;
            });
            this.ticking = true;
        }
        e.stopPropagation();
    }
    SetPosition = () => {
        return this.position?.map(elem => {
            return (h("li", { class: "breadcrumb-item text-sm text-dark active", "aria-current": "page" }, elem));
        });
    };
    SetSearch = () => {
        if (!this.searchable)
            return;
        return h("storm-input-group", { class: "me-3", label: "\u641C\u5C0B", outline: true });
    };
    SetIcon = () => {
        return this.dataSet?.map(elem => {
            if (elem.href) {
                return (h("li", { class: "nav-item" }, h("storm-tooltip", { title: elem.title, placement: "bottom" }, h("a", { role: "button", href: elem.href, class: "nav-link text-body font-weight-bold px-2" }, h("i", { class: "material-icons me-sm-1" }, elem.icon)))));
            }
            else {
                return (h("li", { class: "nav-item" }, h("storm-tooltip", { title: elem.title, placement: "bottom" }, h("a", { role: "button", class: "nav-link text-body font-weight-bold px-2", onClick: e => this.handleClick(e, elem.id) }, h("i", { class: "material-icons me-sm-1" }, elem.icon)))));
            }
        });
    };
    handleToggleClick = (e) => {
        e.preventDefault();
        // this.toggle()
        // this.toggleClick.emit(this.checked)
        this.toggleClick.emit();
    };
    handleClick = (e, id) => {
        e.preventDefault();
        e.stopPropagation();
        this.navbarClick.emit(id);
    };
    render() {
        return (h("nav", { key: 'e29ebade4f8f5a0a932e12009e0077a8bbabe251', class: "navbar navbar-main navbar-expand-lg position-sticky mt-4 top-1 px-0 mx-4 border-radius-xl z-index-sticky shadow-none",
            /*id="navbarBlur" data-scroll="true"*/ ref: e => (this.navbarRef = e) }, h("div", { key: 'a546489e7c12498441a9946a34f93b3b3e5db848', class: "container-fluid py-1 px-3" }, h("nav", { key: '1acd17f2396c3a2740314b0d487fac3aff8fc6fc', "aria-label": "breadcrumb" }, h("ol", { key: '429991ed67a31a3cbad98091f01727b114eb4702', class: "breadcrumb bg-transparent mb-0 pb-0 pt-1 px-0 me-sm-6 me-5" }, h("li", { key: 'de29e749a33399c7896d728d36655cbfad057bd3', class: "breadcrumb-item text-sm" }, h("a", { key: '0067ba4e62c57f2d5fab20aa8743ec20866c37d2', class: "opacity-5 text-dark", href: "/" }, h("i", { key: '4c39c98e5689c7969714ed0966e84022de86e2b6', class: "material-icons opacity-10" }, "home"))), this.SetPosition()), h("h6", { key: 'f054b894f351cec96ac06b570376b29fb1470dfc', class: "font-weight-bolder mb-0" }, this.position ? this.position[this.position?.length - 1] : '')), h("div", { key: '7ed34012bf368ae6fae32798f6844c2c05e3aa2d', class: "sidenav-toggler sidenav-toggler-inner pinned", ref: e => (this.toggleInnerRef = e) }, h("a", { key: '0048af7d6914cfb2df67f2458a58bfef1f71a28d', href: "javascript:;", class: "nav-link text-body p-0", onClick: e => this.handleToggleClick(e) }, h("div", { key: '2f360759eeb71628935a667172f35aad792edb0d', class: "sidenav-toggler-inner" }, h("i", { key: 'af87c899676c5da44f732969294e691dec96cf7f', class: "sidenav-toggler-line" }), h("i", { key: '48a1a80aa0bb08dc5483ce8487530b0fe6b35d5c', class: "sidenav-toggler-line" }), h("i", { key: 'e8ad018d623bcbcfa75026e8b4f11e72c073f49e', class: "sidenav-toggler-line" })))), h("div", { key: '29998fc4c86bc4ae8d6ab68eac293169917c9a75', class: "collapse navbar-collapse mt-sm-0 mt-2 me-md-0 me-sm-4", id: "navbar" }, h("div", { key: '04e8658d184c9b23d44845847264c478531f8b4e', class: "ms-md-auto d-flex align-items-center" }, this.SetSearch(), h("ul", { key: '4ec6ea9a600497d4ccbd035416bc0b102f411e30', class: "navbar-nav  justify-content-end" }, this.SetIcon()))))));
    }
};
StormNavbar.style = stormNavbarCss;

export { StormNavbar as storm_navbar };
//# sourceMappingURL=storm-navbar.entry.esm.js.map

//# sourceMappingURL=storm-navbar.entry.js.map