import { r as registerInstance, a as getElement, h } from './index-BpF8IqPI.js';
import { S as SharedStyles } from './shared-styles-B0cWbvbo.js';

/**
 * High-performance singleton class for browser environment detection with lazy evaluation
 */
class BrowserEnv {
    // Cached detection results
    _isModernBrowser;
    _isSafariIOSDesktopMode;
    _isMobileDevice;
    _isSafariDesktop;
    // Static singleton and cached browser data
    static _instance = null;
    static _userAgent;
    static _navigator;
    static _isInitialized = false;
    // Compiled regex patterns for better performance
    static MOBILE_REGEX = /Mobi|Tablet|Android|iPad|iPhone/;
    static APPLE_VENDOR_REGEX = /Apple/;
    static SAFARI_REGEX = /Safari/;
    /**
     * Private constructor - use getCurrent() to get instance
     */
    constructor() {
        this.initializeStaticData();
    }
    /**
     * Initialize static browser data once
     */
    static initializeStaticData() {
        if (!this._isInitialized && typeof window !== 'undefined') {
            this._userAgent = window.navigator.userAgent;
            this._navigator = window.navigator;
            this._isInitialized = true;
        }
    }
    /**
     * Initialize static data for instance
     */
    initializeStaticData() {
        BrowserEnv.initializeStaticData();
    }
    /**
     * Detects if browser supports modern JavaScript features
     */
    get isModernBrowser() {
        if (this._isModernBrowser === undefined) {
            this._isModernBrowser = typeof window?.Promise === 'function';
        }
        return this._isModernBrowser;
    }
    /**
     * Detects Safari iOS in desktop mode (iPad requesting desktop site)
     */
    get isSafariIOSDesktopMode() {
        if (this._isSafariIOSDesktopMode === undefined) {
            const nav = BrowserEnv._navigator;
            this._isSafariIOSDesktopMode =
                nav.platform === 'MacIntel' &&
                    typeof nav.maxTouchPoints === 'number' &&
                    nav.maxTouchPoints > 1;
        }
        return this._isSafariIOSDesktopMode;
    }
    /**
     * Detects if device is mobile (includes tablets and iOS desktop mode)
     */
    get isMobileDevice() {
        if (this._isMobileDevice === undefined) {
            this._isMobileDevice =
                this.isSafariIOSDesktopMode ||
                    BrowserEnv.MOBILE_REGEX.test(BrowserEnv._userAgent);
        }
        return this._isMobileDevice;
    }
    /**
     * Detects desktop Safari browser (excludes mobile devices)
     */
    get isSafariDesktop() {
        if (this._isSafariDesktop === undefined) {
            const nav = BrowserEnv._navigator;
            this._isSafariDesktop =
                !this.isMobileDevice &&
                    typeof nav.vendor === 'string' &&
                    BrowserEnv.APPLE_VENDOR_REGEX.test(nav.vendor) &&
                    BrowserEnv.SAFARI_REGEX.test(BrowserEnv._userAgent);
        }
        return this._isSafariDesktop;
    }
    /**
     * Gets the current browser environment singleton instance
     * @returns {BrowserEnv} The browser environment singleton instance
     */
    static getCurrent() {
        if (!BrowserEnv._instance) {
            BrowserEnv._instance = new BrowserEnv();
        }
        return BrowserEnv._instance;
    }
    /**
     * Reset singleton instance (mainly for testing)
     */
    static reset() {
        BrowserEnv._instance = null;
        BrowserEnv._isInitialized = false;
    }
    /**
     * Get a summary of browser environment
     */
    getSummary() {
        return {
            isModernBrowser: this.isModernBrowser,
            isSafariIOSDesktopMode: this.isSafariIOSDesktopMode,
            isMobileDevice: this.isMobileDevice,
            isSafariDesktop: this.isSafariDesktop
        };
    }
}

const stormPdfViewCss = ":host{display:block;position:absolute;top:0;left:0;right:0;bottom:0}";

const StormPdfView = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
    }
    get host() { return getElement(this); }
    assumptionMode = true;
    supportRedirect = false;
    forceIframe = false;
    url;
    width = '100%';
    height = '100%';
    viewTitle = '';
    omitInlineStyles = false;
    async componentWillLoad() {
        await SharedStyles.applyShadowRootStylesAsync(this.host, 'material');
    }
    render() {
        if (!this.url) {
            console.log('URL is not valid');
            return;
        }
        if ((!BrowserEnv.getCurrent().isMobileDevice && BrowserEnv.getCurrent().isModernBrowser) || (this.assumptionMode && !BrowserEnv.getCurrent().isMobileDevice)) {
            if (this.forceIframe || this.supportRedirect || BrowserEnv.getCurrent().isSafariDesktop) {
                return h("storm-html-view", { url: this.url, width: this.width, height: this.height, viewTitle: this.viewTitle, omitInlineStyles: this.omitInlineStyles });
            }
            else {
                let style = {};
                if (!this.omitInlineStyles) {
                    style.width = this.width;
                    style.height = this.height;
                }
                return h("embed", { title: this.viewTitle, src: this.url, type: "application/pdf", style: style });
            }
        }
        throw new Error('This browser does not support embedded PDFs');
    }
};
StormPdfView.style = stormPdfViewCss;

export { StormPdfView as storm_pdf_view };
//# sourceMappingURL=storm-pdf-view.entry.esm.js.map

//# sourceMappingURL=storm-pdf-view.entry.js.map