import { r as registerInstance, a as getElement, h, d as Host } from './index-BpF8IqPI.js';
import { S as SharedStyles } from './shared-styles-B0cWbvbo.js';

const stormCanvasCss = "";

const StormCanvas = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
    }
    get host() { return getElement(this); }
    width = 700;
    height = 350;
    async componentWillLoad() {
        await SharedStyles.applyShadowRootStylesAsync(this.host, 'material');
    }
    componentDidLoad() {
        var canvas = this.host.shadowRoot.querySelector('canvas');
        var ctx = canvas.getContext('2d');
        ctx.fillStyle = '#E0E0E0';
        ctx.fillRect(0, 0, this.width, this.height);
    }
    render() {
        return (h(Host, { key: '190a286466900228d65051b81e53567c958babcc' }, h("canvas", { key: '087f61db61b8e6401211010c81ef0c847844971b', width: this.width, height: this.height })));
    }
};
StormCanvas.style = stormCanvasCss;

export { StormCanvas as storm_canvas };
//# sourceMappingURL=storm-canvas.entry.esm.js.map

//# sourceMappingURL=storm-canvas.entry.js.map