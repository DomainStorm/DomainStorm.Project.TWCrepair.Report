import { r as registerInstance, a as getElement, h } from './index-BpF8IqPI.js';
import { S as SharedStyles } from './shared-styles-B0cWbvbo.js';

const stormHtmlViewCss = ":host{display:block;position:absolute;top:0;left:0;right:0;bottom:0}";

const StormHtmlView = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
    }
    get host() { return getElement(this); }
    url;
    width = '100%';
    height = '100%';
    viewTitle;
    omitInlineStyles = false;
    async componentWillLoad() {
        await SharedStyles.applyShadowRootStylesAsync(this.host, 'material');
    }
    render() {
        if (!this.url) {
            console.log('URL is not valid');
            return;
        }
        let style = {};
        if (!this.omitInlineStyles) {
            style.width = this.width;
            style.height = this.height;
            style.overflow = 'auto';
            style.border = 'none';
        }
        return h("iframe", { title: this.viewTitle, src: this.url, allow: "fullscreen", style: style });
    }
};
StormHtmlView.style = stormHtmlViewCss;

export { StormHtmlView as storm_html_view };
//# sourceMappingURL=storm-html-view.entry.esm.js.map

//# sourceMappingURL=storm-html-view.entry.js.map