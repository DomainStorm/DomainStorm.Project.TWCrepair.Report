import { r as registerInstance, e as createEvent, a as getElement, h, d as Host } from './index-BpF8IqPI.js';
import { P as PerfectScrollbar } from './perfect-scrollbar.esm-hw42mw03.js';

const stormSplitViewCss = ".ps{overflow:hidden !important;overflow-anchor:none;-ms-overflow-style:none;touch-action:auto;-ms-touch-action:auto}.ps__rail-x{display:none;opacity:0;transition:background-color .2s linear, opacity .2s linear;-webkit-transition:background-color .2s linear, opacity .2s linear;height:15px;bottom:0px;position:absolute}.ps__rail-y{display:none;opacity:0;transition:background-color .2s linear, opacity .2s linear;-webkit-transition:background-color .2s linear, opacity .2s linear;width:15px;right:0;position:absolute}.ps--active-x>.ps__rail-x,.ps--active-y>.ps__rail-y{display:block;background-color:transparent}.ps:hover>.ps__rail-x,.ps:hover>.ps__rail-y,.ps--focus>.ps__rail-x,.ps--focus>.ps__rail-y,.ps--scrolling-x>.ps__rail-x,.ps--scrolling-y>.ps__rail-y{opacity:0.6}.ps .ps__rail-x:hover,.ps .ps__rail-y:hover,.ps .ps__rail-x:focus,.ps .ps__rail-y:focus,.ps .ps__rail-x.ps--clicking,.ps .ps__rail-y.ps--clicking{background-color:#eee;opacity:0.9}.ps__thumb-x{background-color:#aaa;border-radius:6px;transition:background-color .2s linear, height .2s ease-in-out;-webkit-transition:background-color .2s linear, height .2s ease-in-out;height:6px;bottom:2px;position:absolute}.ps__thumb-y{background-color:#aaa;border-radius:6px;transition:background-color .2s linear, width .2s ease-in-out;-webkit-transition:background-color .2s linear, width .2s ease-in-out;width:6px;right:2px;position:absolute}.ps__rail-x:hover>.ps__thumb-x,.ps__rail-x:focus>.ps__thumb-x,.ps__rail-x.ps--clicking .ps__thumb-x{background-color:#999;height:11px}.ps__rail-y:hover>.ps__thumb-y,.ps__rail-y:focus>.ps__thumb-y,.ps__rail-y.ps--clicking .ps__thumb-y{background-color:#999;width:11px}@supports (-ms-overflow-style: none){.ps{overflow:auto !important}}@media screen and (-ms-high-contrast: active), (-ms-high-contrast: none){.ps{overflow:auto !important}}:host{display:block}.storm-container,.storm-vertical-static-backdrop,.storm-horizontal-static-backdrop{position:absolute;top:0;left:0;right:0;bottom:0}.storm-first-pane-d-none>.first-second-pane,.storm-second-pane-d-none>.storm-second-pane{display:none !important}.storm-pane{position:absolute;top:0;left:0;right:0;bottom:0;overflow:auto;}.storm-split-percent:has(.storm-first-pane-AD-none,.storm-second-pane-d-none)>.split-bar{display:none}.storm-split-percent:not(.storm-first-pane-d-none):not(.storm-second-pane-d-none)>.split-bar{-webkit-background-clip:padding-box;background-clip:padding-box;position:absolute;z-index:9998}.split-bar.active,.split-bar:hover{background-color:#e91e63}.storm-vertical-static-backdrop,.storm-horizontal-static-backdrop{z-index:9999}.storm-vertical-split>.storm-first-pane,.storm-vertical-split>.storm-second-pane{position:absolute;top:0;bottom:0}.storm-vertical-split>.storm-first-pane{left:0}.storm-vertical-split>.storm-second-pane{right:0}.storm-vertical-split.storm-split-percent>.storm-first-pane{right:auto;width:var(--split-percent)}.storm-vertical-split.storm-split-percent>.storm-second-pane{left:var(--split-percent);width:auto}.storm-vertical-split.storm-fixed-first-pane>.storm-first-pane{right:auto;width:var(--fixed-size)}.storm-vertical-split.storm-fixed-first-pane>.storm-second-pane{left:var(--fixed-size);width:auto}.storm-vertical-split.storm-fixed-second-pane>.storm-first-pane{right:var(--fixed-size);width:auto}.storm-vertical-split.storm-fixed-second-pane>.storm-second-pane{left:auto;width:var(--fixed-size)}.storm-vertical-split.storm-first-pane-d-none>.storm-second-pane,.storm-vertical-split.storm-second-pane-d-none>.storm-first-pane{left:0 !important;right:0 !important;width:auto !important}.storm-vertical-split.storm-split-percent:not(.storm-first-pane-d-none):not(.storm-second-pane-d-none):not(.storm-fixed-first-pane):not(.storm-fixed-second-pane)>.storm-first-pane>.storm-pane{right:2px !important}.storm-vertical-split.storm-split-percent:not(.storm-first-pane-d-none):not(.storm-second-pane-d-none):not(.storm-fixed-first-pane):not(.storm-fixed-second-pane)>.storm-second-pane>.storm-pane{left:2px !important}.storm-vertical-split.storm-split-percent:not(.storm-first-pane-d-none):not(.storm-second-pane-d-none)>.split-bar{border-left:1px solid rgba(0, 0, 0, 0);border-right:1px solid rgba(0, 0, 0, 0);top:0;bottom:0;left:var(--split-percent);margin-left:-2px;width:4px}.storm-vertical-static-backdrop,.storm-vertical-split .split-bar:hover{cursor:col-resize}.storm-horizontal-split>.storm-first-pane,.storm-horizontal-split>.storm-second-pane{position:absolute;left:0;right:0}.storm-horizontal-split>.storm-first-pane{top:0}.storm-horizontal-split>.storm-second-pane{bottom:0}.storm-horizontal-split.storm-split-percent>.storm-first-pane{bottom:auto;height:var(--split-percent)}.storm-horizontal-split.storm-split-percent>.storm-second-pane{top:var(--split-percent);height:auto}.storm-horizontal-split.storm-fixed-first-pane>.storm-first-pane{bottom:auto;height:var(--fixed-size)}.storm-horizontal-split.storm-fixed-first-pane>.storm-second-pane{top:var(--fixed-size);height:auto}.storm-horizontal-split.storm-fixed-second-pane>.storm-first-pane{bottom:var(--fixed-size);height:auto}.storm-horizontal-split.storm-fixed-second-pane>.storm-second-pane{top:auto;height:var(--fixed-size)}.storm-horizontal-split.storm-first-pane-d-none>.storm-second-pane,.storm-horizontal-split.storm-second-pane-d-none>.storm-first-pane{top:0 !important;bottom:0 !important;height:auto !important}.storm-horizontal-split.storm-split-percent:not(.storm-first-pane-d-none):not(.storm-second-pane-d-none):not(.storm-fixed-first-pane):not(.storm-fixed-second-pane)>.storm-first-pane>.storm-pane{bottom:2px !important}.storm-horizontal-split.storm-split-percent:not(.storm-first-pane-d-none):not(.storm-second-pane-d-none):not(.storm-fixed-first-pane):not(.storm-fixed-second-pane)>.storm-second-pane>.storm-pane{top:2px !important}.storm-horizontal-split.storm-split-percent:not(.storm-first-pane-d-none):not(.storm-second-pane-d-none)>.split-bar{border-top:1px solid rgba(0, 0, 0, 0);border-bottom:1px solid rgba(0, 0, 0, 0);left:0;right:0;top:var(--split-percent);margin-top:-2px;height:4px}.storm-horizontal-static-backdrop,.storm-horizontal-split>.split-bar:hover{cursor:row-resize}";

const StormSplitView = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.innerScroll = createEvent(this, "innerScroll", 7);
    }
    _splitBarMove = false;
    _splitBarRef;
    _stormContainerRef;
    _secondPaneRef;
    get host() { return getElement(this); }
    splitPercent = 50;
    fixedSize;
    fixedPane;
    direction = 'vertical';
    firstPaneVisible = true;
    secondPaneVisible = true;
    firstPaneName = 'first-pane';
    secondPaneName = 'second-pane';
    innerScroll;
    async viewToggle(token, force) {
        this._stormContainerRef.classList.toggle('storm-' + token + '-pane-d-none', !force);
    }
    renderPane(token) {
        const classes = {};
        classes['storm-' + token + '-pane'] = true;
        return (h("div", { ref: elm => {
                if (token === 'second')
                    this._secondPaneRef = elm;
            }, class: classes }, h("div", { class: { 'storm-pane': true } }, h("slot", { name: token === 'first' ? this.firstPaneName : this.secondPaneName }))));
    }
    render() {
        const styles = {};
        if (!this.fixedSize || !this.fixedPane)
            styles['--split-percent'] = this.splitPercent + '%';
        else
            styles['--fixed-size'] = this.fixedSize;
        return (h(Host, { key: '4ea80a7eda3f1f9484029b174feaa212d19a5ba2', style: styles }, h("div", { key: '4a5823dc67398e9107a3c07a1065e0d7e0d6d3fe', ref: elm => (this._stormContainerRef = elm), class: {
                'storm-container': true,
                'storm-first-pane-d-none': !this.firstPaneVisible,
                'storm-second-pane-d-none': !this.secondPaneVisible,
                'storm-split-percent': !this.fixedSize || !this.fixedPane,
                'storm-fixed-first-pane': this.fixedPane === 'first',
                'storm-fixed-second-pane': this.fixedPane === 'second',
                'storm-horizontal-split': this.direction === 'horizontal',
                'storm-vertical-split': this.direction === 'vertical',
            } }, this.renderPane('first'), this.renderPane('second'), h("div", { key: '4af541c5feeb2cd3f9838753b10850798babd636', ref: elm => {
                this._splitBarRef = elm;
            }, class: { 'split-bar': true } }))));
    }
    componentDidLoad() {
        this._splitBarRef.addEventListener('mousedown', () => {
            this._splitBarRef.classList.add('active');
            this._splitBarMove = true;
            const staticBackdrop = document.createElement('div');
            staticBackdrop.classList.add('storm-' + this.direction + '-static-backdrop');
            staticBackdrop.addEventListener('mousemove', (e) => {
                if (this._splitBarMove) {
                    if (this.direction === 'vertical')
                        this._splitBarRef.style.setProperty('left', e.offsetX + 'px');
                    else
                        this._splitBarRef.style.setProperty('top', e.offsetY + 'px');
                }
            });
            staticBackdrop.addEventListener('mouseup', e => {
                this._splitBarRef.classList.remove('active');
                if (this._splitBarMove) {
                    let percent = 0;
                    if (this.direction === 'vertical')
                        percent = (e.offsetX / staticBackdrop.offsetWidth) * 100;
                    else
                        percent = (e.offsetY / staticBackdrop.offsetHeight) * 100;
                    if (percent < 10)
                        percent = 10;
                    if (percent > 90)
                        percent = 90;
                    this.host.style.setProperty('--split-percent', percent + '%');
                    this._splitBarMove = false;
                    if (this.direction === 'vertical')
                        this._splitBarRef.style.removeProperty('left');
                    else
                        this._splitBarRef.style.removeProperty('top');
                    this.host.removeChild(staticBackdrop);
                }
            });
            this.host.appendChild(staticBackdrop);
        });
        const container = this._secondPaneRef.firstElementChild;
        if (container) {
            new PerfectScrollbar(container);
            const innerScroll = this.innerScroll;
            let lastKnownScrollPosition = 0;
            let ticking = false;
            container.addEventListener('scroll', (e) => {
                lastKnownScrollPosition = container.scrollTop;
                if (!ticking) {
                    window.requestAnimationFrame(function () {
                        innerScroll.emit(lastKnownScrollPosition);
                        ticking = false;
                    });
                    ticking = true;
                }
                e.stopPropagation();
            });
        }
    }
};
StormSplitView.style = stormSplitViewCss;

export { StormSplitView as storm_split_view };
//# sourceMappingURL=storm-split-view.entry.esm.js.map

//# sourceMappingURL=storm-split-view.entry.js.map