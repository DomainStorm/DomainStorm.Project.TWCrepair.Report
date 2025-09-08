import { r as registerInstance, e as createEvent, a as getElement, h, d as Host } from './index-BpF8IqPI.js';
import { S as SharedStyles } from './shared-styles-B0cWbvbo.js';

const stormButtonGroupCss = ":host .main-content{margin-left:17.125rem !important}";

const StormButtonGroup = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.itemClick = createEvent(this, "itemClick", 7);
    }
    get host() { return getElement(this); }
    dataSet;
    itemClick;
    onClick = async (data) => {
        this.itemClick.emit(data);
    };
    async componentWillLoad() {
        await SharedStyles.applyShadowRootStylesAsync(this.host, 'material');
    }
    render() {
        return (h(Host, { key: '279c1e94bb6e8c4b720898249a32857dbab24ce5' }, h("div", { key: '45e17cdee28be4e875d39b00a618b546d5ea2aa1', class: "d-sm-flex justify-content-between" }, h("div", { key: 'bfecd6149ae397806ec539a83df23a883770ebd4', class: "d-flex" }, this.dataSet.map(data => (h("button", { class: `${data.class}`, type: "button", onClick: () => this.onClick(data) }, data.label)))))));
    }
};
StormButtonGroup.style = stormButtonGroupCss;

export { StormButtonGroup as storm_button_group };
//# sourceMappingURL=storm-button-group.entry.esm.js.map

//# sourceMappingURL=storm-button-group.entry.js.map