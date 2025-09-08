import { r as registerInstance, e as createEvent, a as getElement, h, d as Host } from './index-BpF8IqPI.js';
import { F as FnIcon } from './functional-Cx8laaX_.js';
import { g as generateUid } from './utils-BOoVSa4-.js';

const stormNavsCss = ":host{display:block}.nav-link:not(.active):hover{color:#e91e63 !important}.nav-link.disabled{color:#7b809a !important}";

const StormNavs = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.activeChanged = createEvent(this, "activeChanged", 7);
    }
    _resizeObserver;
    _navPillsRef;
    _navsRef;
    _movingDiv;
    get host() { return getElement(this); }
    value;
    dataSet;
    direction = 'horizontal';
    activeChanged;
    async watchDataSetHandler() {
        this.initDataSet();
    }
    async valueHandler() {
        this.moveNav();
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
    async setActive(id) {
        this.value = id;
    }
    handleItemClick(ev, id) {
        if (ev) {
            ev.preventDefault();
            ev.stopPropagation();
        }
        this.value = id;
    }
    checkSize() { }
    componentDidLoad() {
        if (!this.dataSet)
            return;
        const checkSize = () => {
            const activeLi = this._navPillsRef.querySelector('.nav-link.active');
            if (!activeLi.offsetWidth && !activeLi.offsetHeight) {
                return requestAnimationFrame(checkSize);
            }
            this.actionRender();
            this.bindMouseOver();
            this.observeResize();
        };
        checkSize();
        this.moveNav();
    }
    bindMouseOver() {
        this._navPillsRef.addEventListener('mouseover', (ev) => {
            const target = ev.target;
            const li = target.closest('li');
            if (li) {
                const nodes = Array.from(li.closest('ul')?.children || []);
                const index = nodes.indexOf(li) + 1;
                this.bindClickEvent(li, nodes, index);
            }
        });
    }
    bindClickEvent(li, nodes, index) {
        const navLink = this._navPillsRef.querySelector(`li:nth-child(${index}) .nav-link`);
        navLink?.addEventListener('click', () => {
            this.updateNavPosition(li, nodes, index);
        }, { once: true });
    }
    moveNav() {
        if (!this.value)
            return;
        this.dataSet.forEach(x => {
            x.active = x.id === this.value;
            const navElem = this._navsRef[x.id];
            navElem.classList.toggle('active', x.active);
        });
        this.updateNavPosition(this._navsRef[this.value].parentElement, Array.from(this._navsRef[this.value].closest('ul').children), Array.from(this._navsRef[this.value].closest('ul').children).indexOf(this._navsRef[this.value].parentElement) + 1);
        this.activeChanged.emit(this.value);
    }
    updateNavPosition(li, nodes, index) {
        if (!this._movingDiv) {
            return;
        }
        let sum = 0;
        if (this._navPillsRef.classList.contains('flex-column')) {
            for (let j = 1; j <= nodes.indexOf(li); j++) {
                sum += this._navPillsRef.querySelector(`li:nth-child(${j})`).offsetHeight;
            }
            this._movingDiv.style.transform = `translate3d(0px, ${sum}px, 0px)`;
            this._movingDiv.style.height = `${this._navPillsRef.querySelector(`li:nth-child(${index})`).offsetHeight}px`;
        }
        else {
            for (let j = 1; j <= nodes.indexOf(li); j++) {
                sum += this._navPillsRef.querySelector(`li:nth-child(${j})`).offsetWidth;
            }
            this._movingDiv.style.transform = `translate3d(${sum}px, 0px, 0px)`;
            this._movingDiv.style.width = `${this._navPillsRef.querySelector(`li:nth-child(${index})`).offsetWidth}px`;
        }
    }
    observeResize() {
        this._resizeObserver = new ResizeObserver(() => {
            this.actionRender();
        });
        this._resizeObserver.observe(this.host);
    }
    actionRender() {
        if (this._navPillsRef == null)
            return;
        const activeLink = this._navPillsRef.querySelector('.nav-link.active');
        if (activeLink == null)
            return;
        const li = activeLink.parentElement;
        const nodes = Array.from(li.closest('ul').children);
        this.createOrUpdateMovingNav(activeLink);
        if (li) {
            const isVertical = this._navPillsRef.classList.contains('flex-column');
            const index = nodes.indexOf(li) + 1;
            const sum = this.calculateNavOffset(index, isVertical);
            this.updateMovingNav(sum, isVertical, index);
        }
        this.adjustLayoutForScreenSize();
    }
    createOrUpdateMovingNav(activeLink) {
        if (!this._movingDiv) {
            this._movingDiv = document.createElement('div');
            const nav = activeLink.cloneNode();
            nav.textContent = '-';
            this._movingDiv.classList.add('moving-tab', 'position-absolute', 'nav-link');
            this._movingDiv.appendChild(nav);
            this._navPillsRef.appendChild(this._movingDiv);
        }
        this._movingDiv.style.padding = '0px';
        this._movingDiv.style.transition = '.5s ease';
        return this._movingDiv;
    }
    calculateNavOffset(index, isVertical) {
        let sum = 0;
        for (let j = 1; j < index; j++) {
            const dimension = isVertical ? 'offsetHeight' : 'offsetWidth';
            sum += this._navPillsRef.querySelector(`li:nth-child(${j})`)[dimension];
        }
        return sum;
    }
    updateMovingNav(sum, isVertical, index) {
        this._movingDiv.style.transform = isVertical ? `translate3d(0px, ${sum}px, 0px)` : `translate3d(${sum}px, 0px, 0px)`;
        this._movingDiv.style[`width`] = this._navPillsRef.querySelector(`li:nth-child(${index})`).offsetWidth + 'px';
        this._movingDiv.style[`height`] = this._navPillsRef.querySelector(`li:nth-child(${index})`).offsetHeight + 'px';
    }
    adjustLayoutForScreenSize() {
        if (window.innerWidth < 991 && !this._navPillsRef.classList.contains('flex-column')) {
            this.toggleLayout(true);
        }
        else if (window.innerWidth >= 991 && this._navPillsRef.classList.contains('on-resize')) {
            this.toggleLayout(false);
        }
    }
    toggleLayout(isVertical) {
        if (isVertical) {
            this._navPillsRef.classList.remove('flex-row');
            this._navPillsRef.classList.add('flex-column', 'on-resize');
        }
        else {
            this._navPillsRef.classList.remove('flex-column', 'on-resize');
            this._navPillsRef.classList.add('flex-row');
        }
        const li = this._navPillsRef.querySelector('.nav-link.active').parentElement;
        const nodes = Array.from(li.closest('ul').children);
        const sum = this.calculateNavOffset(nodes.indexOf(li) + 1, isVertical);
        this._movingDiv.style.transform = isVertical ? `translate3d(0px, ${sum}px, 0px)` : `translate3d(${sum}px, 0px, 0px)`;
    }
    componentWillRender() {
        this.initDataSet();
    }
    render() {
        this._navsRef = {};
        if (!this.dataSet)
            return;
        return (h(Host, null, h("div", { class: "nav-wrapper position-relative end-0 m-1" }, h("ul", { ref: elm => (this._navPillsRef = elm), class: {
                nav: true,
                'nav-pills': true,
                'nav-fill': true,
                'p-1': true,
                'flex-column': this.direction === 'vertical',
            } }, this.dataSet.map(x => (h("li", { class: "nav-item" }, h("a", { ref: elm => (this._navsRef[x.id] = elm), class: {
                'nav-link': true,
                'mb-0': true,
                'px-0': true,
                'py-1': true,
                active: x.active,
                disabled: x.disabled,
            }, onClick: ev => this.handleItemClick(ev, x.id), href: "#" }, h(FnIcon, null, x.icon), x.name))))))));
    }
    static get watchers() { return {
        "dataSet": ["watchDataSetHandler"],
        "value": ["valueHandler"]
    }; }
};
StormNavs.style = stormNavsCss;

export { StormNavs as storm_navs };
//# sourceMappingURL=storm-navs.entry.esm.js.map

//# sourceMappingURL=storm-navs.entry.js.map