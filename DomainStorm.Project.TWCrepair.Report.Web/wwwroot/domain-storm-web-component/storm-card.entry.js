import { r as registerInstance, e as createEvent, a as getElement, h, d as Host } from './index-BpF8IqPI.js';
import { P as PerfectScrollbar } from './perfect-scrollbar.esm-hw42mw03.js';
import { F as FnIcon, d as FnHr } from './functional-Cx8laaX_.js';
import { o as mergeClasses } from './utils-BOoVSa4-.js';

const stormCardCss = ":host{display:block}.vh-75{height:75vh !important}.card-header:first-child{border-radius:.75rem !important}";

const StormCard = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.innerScroll = createEvent(this, "innerScroll", 7);
    }
    _cardBodyRef;
    _slotCardHeader = false;
    _slotTitle = false;
    _slotHeader = false;
    _slotFooter = false;
    _slotBody = false;
    _perfectScrollbar;
    _scrollListener = null;
    get host() { return getElement(this); }
    class;
    icon;
    headline;
    bodyClass;
    scrollable = false;
    cardStyle = 'info';
    innerScroll;
    _observer = new MutationObserver(() => {
        this.checkSlots();
    });
    checkSlots = () => {
        const slots = { cardHeader: this._slotCardHeader, title: this._slotTitle, header: this._slotHeader, footer: this._slotFooter, body: this._slotBody };
        const children = this.host.children;
        const targetSlots = new Set(['card-header', 'title', 'header', 'body', 'footer']);
        let foundCount = 0;
        for (let i = 0; i < children.length; i++) {
            const child = children[i];
            const slot = child.getAttribute('slot');
            if (slot && targetSlots.has(slot)) {
                switch (slot) {
                    case 'card-header':
                        if (!slots.cardHeader) {
                            slots.cardHeader = true;
                            foundCount++;
                        }
                        break;
                    case 'title':
                        if (!slots.title) {
                            slots.title = true;
                            foundCount++;
                        }
                        break;
                    case 'header':
                        if (!slots.header) {
                            slots.header = true;
                            foundCount++;
                        }
                        break;
                    case 'footer':
                        if (!slots.footer) {
                            slots.footer = true;
                            foundCount++;
                        }
                        break;
                }
            }
            else if (!slot && child.nodeType === Node.ELEMENT_NODE && !slots.body) {
                slots.body = true;
                foundCount++;
            }
            if (foundCount === 5)
                break;
        }
        this._slotCardHeader = slots.cardHeader;
        this._slotTitle = slots.title;
        this._slotHeader = slots.header;
        this._slotFooter = slots.footer;
        this._slotBody = slots.body;
    };
    componentWillRender() {
        this._observer.observe(this.host, {
            childList: true, // 偵測子節點新增或刪除
            subtree: false, // 若要監聽所有後代節點，設為 true
        });
    }
    componentDidRender() {
        if (!this._cardBodyRef)
            return;
        this.scrollable ? this.initScrollbar() : this.disposeScrollbar();
    }
    _cardClassesCache = null;
    _lastCardProps = '';
    buildCardClasses() {
        const currentProps = `${this.class || ''}-${this.icon || ''}`;
        if (this._cardClassesCache && this._lastCardProps === currentProps) {
            return this._cardClassesCache;
        }
        const baseClasses = { card: true };
        const classes = this.class ? mergeClasses(baseClasses, this.class) : baseClasses;
        const hasMarginTop = Object.keys(classes).some(key => /^mt-[0-5]|mt-auto$/.test(key));
        if (!hasMarginTop) {
            classes[this.icon ? 'mt-4' : 'mt-3'] = true;
        }
        this._cardClassesCache = classes;
        this._lastCardProps = currentProps;
        return classes;
    }
    _bodyClassesCache = null;
    _lastBodyProps = '';
    buildBodyClasses() {
        const currentProps = `${this.bodyClass || ''}-${this.scrollable}`;
        if (this._bodyClassesCache && this._lastBodyProps === currentProps) {
            return this._bodyClassesCache;
        }
        const baseClasses = {
            'card-body': true,
            'py-2': true,
            'overflow-auto': this.scrollable,
        };
        const classes = this.bodyClass ? mergeClasses(baseClasses, this.bodyClass) : baseClasses;
        this._bodyClassesCache = classes;
        this._lastBodyProps = currentProps;
        return classes;
    }
    initScrollbar() {
        this.disposeScrollbar();
        this._perfectScrollbar = new PerfectScrollbar(this._cardBodyRef);
        let lastKnownScrollPosition = 0;
        let frameId = null;
        this._scrollListener = (ev) => {
            ev.stopPropagation();
            const currentScrollPosition = this._cardBodyRef.scrollTop;
            if (Math.abs(currentScrollPosition - lastKnownScrollPosition) < 1)
                return;
            lastKnownScrollPosition = currentScrollPosition;
            if (frameId) {
                cancelAnimationFrame(frameId);
            }
            frameId = requestAnimationFrame(() => {
                this.innerScroll.emit(lastKnownScrollPosition);
                frameId = null;
            });
        };
        this._cardBodyRef?.addEventListener('scroll', this._scrollListener, { passive: true });
    }
    disposeScrollbar() {
        if (this._scrollListener) {
            this._cardBodyRef?.removeEventListener('scroll', this._scrollListener);
            this._scrollListener = null;
        }
        if (this._perfectScrollbar) {
            this._perfectScrollbar.destroy();
            this._perfectScrollbar = null;
        }
    }
    disconnectedCallback() {
        this.disposeScrollbar();
        this._cardClassesCache = null;
        this._bodyClassesCache = null;
        this._iconCache = null;
        this._titleCache = null;
        this._observer.disconnect();
    }
    _iconCache = null;
    _lastIconProps = '';
    renderIcon() {
        if (!this.icon)
            return;
        const currentProps = `${this.icon}-${this.cardStyle}`;
        if (this._iconCache && this._lastIconProps === currentProps) {
            return this._iconCache;
        }
        const classes = {
            icon: true,
            'icon-lg': true,
            'icon-shape': true,
            'text-center': true,
            'border-radius-xl': true,
            'mt-n4': true,
            'me-3': true,
            'mb-2': true,
        };
        classes['bg-gradient-' + this.cardStyle] = true;
        classes['shadow-' + this.cardStyle] = true;
        this._iconCache = (h("div", { class: classes }, h(FnIcon, { class: "opacity-10" }, this.icon)));
        this._lastIconProps = currentProps;
        return this._iconCache;
    }
    _titleCache = null;
    _lastHeadline = '';
    renderCardTitle() {
        if (!this.headline)
            return;
        if (this._titleCache && this._lastHeadline === this.headline) {
            return this._titleCache;
        }
        this._titleCache = h("h5", { class: "mt-3 me-auto" }, this.headline);
        this._lastHeadline = this.headline;
        return this._titleCache;
    }
    renderTitleSlot() {
        if (!this._slotTitle)
            return;
        return h("slot", { name: "title" });
    }
    renderHeader() {
        if (this._slotCardHeader)
            return h("slot", { name: "card-header" });
        if (!this._slotHeader && !this._slotTitle && !this.icon && !this.headline)
            return;
        return (h("div", { class: {
                'card-header': true,
                'py-2': true,
            } }, h("div", { class: "title-header d-flex" }, this.renderIcon(), this.renderCardTitle(), this.renderTitleSlot()), this.renderHeaderSlot()));
    }
    renderHeaderSlot() {
        if (!this._slotHeader)
            return;
        return h("slot", { name: "header" });
    }
    renderBody() {
        if (!this._slotBody)
            return;
        return [
            h(FnHr, null),
            h("div", { class: this.buildBodyClasses(), ref: elm => (this._cardBodyRef = elm) }, h("slot", { name: "body" })),
        ];
    }
    renderFooterSlot() {
        if (!this._slotFooter)
            return;
        return [
            h(FnHr, null),
            h("div", { class: "card-footer py-2" }, h("slot", { name: "footer" })),
        ];
    }
    render() {
        return (h(Host, { key: 'fdddcfb730d43fbd4d53da186ac406f712522eba' }, h("div", { key: '5e53d9e883eba619de0bb5cc07b9144e4dc6ee0d', class: this.buildCardClasses() }, this.renderHeader(), this.renderBody(), this.renderFooterSlot())));
    }
};
StormCard.style = stormCardCss;

export { StormCard as storm_card };
//# sourceMappingURL=storm-card.entry.esm.js.map

//# sourceMappingURL=storm-card.entry.js.map