import { r as registerInstance, a as getElement, h, d as Host } from './index-BpF8IqPI.js';
import { S as SharedStyles } from './shared-styles-B0cWbvbo.js';

const stormDocumentListDetailCss = ":host{display:block}";

const StormDocumentListDetail = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
    }
    get host() { return getElement(this); }
    dataSet;
    columns;
    pageNumber = 1;
    pageSize = 10;
    searchText;
    sortName;
    sortOrder;
    totalRows;
    searchable = false;
    toolbar;
    splitPercent = 30;
    checkToggle = true;
    clickRowTo = 'select';
    sidePagination = 'stencil';
    _table;
    _splitView;
    _toggle = false;
    async componentWillLoad() {
        await SharedStyles.applyShadowRootStylesAsync(this.host, 'material');
    }
    render() {
        return (h(Host, { key: 'cc10fc77cb45d9e93593b639078ae3934804f1ad' }, h("storm-vertical-split-view", { key: '5bb05a333edb4baaa8364aa4e96dca8c5fde46fa', rightPaneVisible: false, splitPercent: this.splitPercent }, h("storm-horizontal-split-view", { key: '3bacb68bea5f14bd2498a0488db0ce6961ff1f16', slot: "left-pane", fixedPane: 'top', fixedSize: '56px' }, h("div", { key: '9ed90bd290baa190ac248cd65a5dddaa44e02575', slot: "top-pane" }, h("slot", { key: '67acaf29eb9c557ed2249b89998ca5f7f315a2b8', name: "top-pane" })), h("div", { key: '1775b87a8210979d2b96da59e9a57c9f150d23f5', slot: "bottom-pane" }, h("slot", { key: '815a1d1487db1d205faf53d6f32614fbf653f698', name: "bottom-pane" }))), h("div", { key: '51a25b4b7d0f37ac9d774662c24afaee6bedecd0', slot: "right-pane" }, h("slot", { key: '03b4846edb4d36298b7cd269c067ef7726a87fab', name: "right-pane" })))));
    }
    async componentDidRender() {
        this._table = this.host.shadowRoot.querySelector('storm-table');
        this._splitView = this.host.shadowRoot.querySelector('storm-vertical-split-view');
    }
    async handleSelectDidChange(value) {
        await this._splitView.viewToggle('right', value > 0);
    }
    async dataSetWatcher() {
        if (this._splitView)
            await this._splitView.viewToggle('right', false);
    }
    async viewToggle(force = null) {
        this._toggle = !this._toggle;
        await this._splitView.viewToggle('right', force != null ? force : this._toggle);
    }
    async getChecked(field) {
        return await this._table.getChecked(field);
    }
    async getSelected() {
        return await this._table.getSelected();
    }
    static get watchers() { return {
        "dataSet": ["dataSetWatcher"]
    }; }
};
StormDocumentListDetail.style = stormDocumentListDetailCss;

export { StormDocumentListDetail as storm_document_list_detail };
//# sourceMappingURL=storm-document-list-detail.entry.esm.js.map

//# sourceMappingURL=storm-document-list-detail.entry.js.map