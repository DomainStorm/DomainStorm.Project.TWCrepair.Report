import { r as registerInstance, e as createEvent, a as getElement, f as forceUpdate, h, d as Host } from './index-BpF8IqPI.js';
import { b as getFieldValue, p as parseClassNames } from './utils-BOoVSa4-.js';

const stormTableBodyCss = ":host{display:block}:host .group-item>th{cursor:auto}:host .group-item>th>div{line-height:1;margin:0.25rem}:host span.detail-title{display:inline-block;min-width:75px;font-weight:bold}:host .d-table-none{display:none !important}:host .d-table-cell{display:table-cell !important}:host .storm-table-4xs .d-table-4xs-none,:host .storm-table-3xs .d-table-4xs-none,:host .storm-table-xxs .d-table-4xs-none,:host .storm-table-xs .d-table-4xs-none,:host .storm-table-sm .d-table-4xs-none,:host .storm-table-md .d-table-4xs-none,:host .storm-table-lg .d-table-4xs-none,:host .storm-table-xl .d-table-4xs-none,:host .storm-table-xxl .d-table-4xs-none,:host .storm-table-3xl .d-table-4xs-none,:host .storm-table-4xl .d-table-4xs-none{display:none !important}:host .storm-table-4xs .d-table-4xs-cell,:host .storm-table-3xs .d-table-4xs-cell,:host .storm-table-xxs .d-table-4xs-cell,:host .storm-table-xs .d-table-4xs-cell,:host .storm-table-sm .d-table-4xs-cell,:host .storm-table-md .d-table-4xs-cell,:host .storm-table-lg .d-table-4xs-cell,:host .storm-table-xl .d-table-4xs-cell,:host .storm-table-xxl .d-table-4xs-cell,:host .storm-table-3xl .d-table-4xs-cell,:host .storm-table-4xl .d-table-4xs-cell{display:table-cell !important}:host .storm-table-3xs .d-table-3xs-none,:host .storm-table-xxs .d-table-3xs-none,:host .storm-table-xs .d-table-3xs-none,:host .storm-table-sm .d-table-3xs-none,:host .storm-table-md .d-table-3xs-none,:host .storm-table-lg .d-table-3xs-none,:host .storm-table-xl .d-table-3xs-none,:host .storm-table-xxl .d-table-3xs-none,:host .storm-table-3xl .d-table-3xs-none,:host .storm-table-4xl .d-table-3xs-none{display:none !important}:host .storm-table-3xs .d-table-3xs-cell,:host .storm-table-xxs .d-table-3xs-cell,:host .storm-table-xs .d-table-3xs-cell,:host .storm-table-sm .d-table-3xs-cell,:host .storm-table-md .d-table-3xs-cell,:host .storm-table-lg .d-table-3xs-cell,:host .storm-table-xl .d-table-3xs-cell,:host .storm-table-xxl .d-table-3xs-cell,:host .storm-table-3xl .d-table-3xs-cell,:host .storm-table-4xl .d-table-3xs-cell{display:table-cell !important}:host .storm-table-xxs .d-table-xxs-none,:host .storm-table-xs .d-table-xxs-none,:host .storm-table-sm .d-table-xxs-none,:host .storm-table-md .d-table-xxs-none,:host .storm-table-lg .d-table-xxs-none,:host .storm-table-xl .d-table-xxs-none,:host .storm-table-xxl .d-table-xxs-none,:host .storm-table-3xl .d-table-xxs-none,:host .storm-table-4xl .d-table-xxs-none{display:none !important}:host .storm-table-xxs .d-table-xxs-cell,:host .storm-table-xs .d-table-xxs-cell,:host .storm-table-sm .d-table-xxs-cell,:host .storm-table-md .d-table-xxs-cell,:host .storm-table-lg .d-table-xxs-cell,:host .storm-table-xl .d-table-xxs-cell,:host .storm-table-xxl .d-table-xxs-cell,:host .storm-table-3xl .d-table-xxs-cell,:host .storm-table-4xl .d-table-xxs-cell{display:table-cell !important}:host .storm-table-xs .d-table-xs-none,:host .storm-table-sm .d-table-xs-none,:host .storm-table-md .d-table-xs-none,:host .storm-table-lg .d-table-xs-none,:host .storm-table-xl .d-table-xs-none,:host .storm-table-xxl .d-table-xs-none,:host .storm-table-3xl .d-table-xs-none,:host .storm-table-4xl .d-table-xs-none{display:none !important}:host .storm-table-xs .d-table-xs-cell,:host .storm-table-sm .d-table-xs-cell,:host .storm-table-md .d-table-xs-cell,:host .storm-table-lg .d-table-xs-cell,:host .storm-table-xl .d-table-xs-cell,:host .storm-table-xxl .d-table-xs-cell,:host .storm-table-3xl .d-table-xs-cell,:host .storm-table-4xl .d-table-xs-cell{display:table-cell !important}:host .storm-table-sm .d-table-sm-none,:host .storm-table-md .d-table-sm-none,:host .storm-table-lg .d-table-sm-none,:host .storm-table-xl .d-table-sm-none,:host .storm-table-xxl .d-table-sm-none,:host .storm-table-3xl .d-table-sm-none,:host .storm-table-4xl .d-table-sm-none{display:none !important}:host .storm-table-sm .d-table-sm-cell,:host .storm-table-md .d-table-sm-cell,:host .storm-table-lg .d-table-sm-cell,:host .storm-table-xl .d-table-sm-cell,:host .storm-table-xxl .d-table-sm-cell,:host .storm-table-3xl .d-table-sm-cell,:host .storm-table-4xl .d-table-sm-cell{display:table-cell !important}:host .storm-table-md .d-table-md-none,:host .storm-table-lg .d-table-md-none,:host .storm-table-xl .d-table-md-none,:host .storm-table-xxl .d-table-md-none,:host .storm-table-3xl .d-table-md-none,:host .storm-table-4xl .d-table-md-none{display:none !important}:host .storm-table-md .d-table-md-cell,:host .storm-table-lg .d-table-md-cell,:host .storm-table-xl .d-table-md-cell,:host .storm-table-xxl .d-table-md-cell,:host .storm-table-3xl .d-table-md-cell,:host .storm-table-4xl .d-table-md-cell{display:table-cell !important}:host .storm-table-lg .d-table-lg-none,:host .storm-table-xl .d-table-lg-none,:host .storm-table-xxl .d-table-lg-none,:host .storm-table-3xl .d-table-lg-none,:host .storm-table-4xl .d-table-lg-none{display:none !important}:host .storm-table-lg .d-table-lg-cell,:host .storm-table-xl .d-table-lg-cell,:host .storm-table-xxl .d-table-lg-cell,:host .storm-table-3xl .d-table-lg-cell,:host .storm-table-4xl .d-table-lg-cell{display:table-cell !important}:host .storm-table-xl .d-table-xl-none,:host .storm-table-xxl .d-table-xl-none,:host .storm-table-3xl .d-table-xl-none,:host .storm-table-4xl .d-table-xl-none{display:none !important}:host .storm-table-xl .d-table-xl-cell,:host .storm-table-xxl .d-table-xl-cell,:host .storm-table-3xl .d-table-xl-cell,:host .storm-table-4xl .d-table-xl-cell{display:table-cell !important}:host .storm-table-xxl .d-table-xxl-none,:host .storm-table-3xl .d-table-xxl-none,:host .storm-table-4xl .d-table-xxl-none{display:none !important}:host .storm-table-xxl .d-table-xxl-cell,:host .storm-table-3xl .d-table-xxl-cell,:host .storm-table-4xl .d-table-xxl-cell{display:table-cell !important}:host .storm-table-3xl .d-table-3xl-none,:host .storm-table-4xl .d-table-3xl-none{display:none !important}:host .storm-table-3xl .d-table-3xl-cell,:host .storm-table-4xl .d-table-3xl-cell{display:table-cell !important}:host .storm-table-4xl .d-table-4xl-none{display:none !important}:host .storm-table-4xl .d-table-4xl-cell{display:table-cell !important}";

const StormTableBody = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.clickRow = createEvent(this, "clickRow", 7);
    }
    get host() { return getElement(this); }
    table;
    dataSet;
    timeStamp;
    clickRow;
    async updateColumnCheckbox(col) {
        if (!this.dataSet || col.type !== 'checkbox')
            return;
        const checkedCount = this.getTotalChecked(col.field);
        const stormCheckbox = this.host.querySelector('th[data-field="' + col.field + '"] storm-checkbox');
        stormCheckbox.checked = checkedCount === this.table.totalRows;
        stormCheckbox.indeterminate = checkedCount > 0 && checkedCount < this.table.totalRows;
    }
    async updateRowCheckbox(row, col) {
        const cell = this.host.querySelector('tr[data-id="' + getFieldValue(row, this.table.fieldId) + '"] storm-table-cell[data-field="' + col.field + '"]');
        if (cell) {
            forceUpdate(cell);
        }
        if (col.field === this.table.fieldSelected) {
            await this.updateRowSelection(row);
        }
    }
    async updateRowSelection(row) {
        const el = this.host.querySelector('tr[data-id="' + getFieldValue(row, this.table.fieldId) + '"]');
        if (el)
            el.classList.toggle('table-primary', getFieldValue(row, this.table.fieldSelected));
    }
    computeColumnClasses(col, classes) {
        return {
            'text-center': col.align === 'center',
            'text-start': !col.align || col.align === 'left',
            'text-end': col.align === 'right',
            'align-middle': !col.valign || col.valign === 'middle',
            'align-top': col.valign === 'top',
            'align-bottom': col.valign === 'bottom',
            'font-weight-bolder': true,
            'text-truncate': true,
            'table-sorter': col.sortable,
            asc: this.table.sortName === col.field && this.table.sortOrder !== 'desc',
            desc: this.table.sortName === col.field && this.table.sortOrder === 'desc',
            ...(classes || {}),
            ...(col.classes || {}),
        };
    }
    renderThead() {
        if (!this.table.columns)
            return;
        return (h("thead", null, h("tr", null, this.table.columns
            .filter(col => col.visible !== false)
            .map(col => {
            return this.renderColumn(col);
        }))));
    }
    renderColumn(col) {
        let content = col.title;
        const classes = {
            'thead-xxs': col.type === 'detail' || col.type === 'serialNumber' || col.type === 'radio' || col.type === 'checkbox',
            'detail-toggle': col.type === 'detail',
        };
        if (content == null && col.type === 'checkbox') {
            const checkedCount = this.getTotalChecked(col.field);
            content = (h("storm-checkbox", { type: "checkbox", indeterminate: checkedCount > 0 && checkedCount < this.table.totalRows, checked: checkedCount === this.table.totalRows, onValueChanged: ev => this.handleColumnCheckChange(ev, col) }));
        }
        return this.renderHeaderCell(content, col, classes);
    }
    renderHeaderCell(content, col, classes = {}) {
        return (h("th", { "data-field": col.field, scope: "col", class: this.computeColumnClasses(col, classes), onClick: () => this.handleColumnClick(col) }, h("div", { class: "cell" }, content), col.description && h("div", { class: "table-description" }, col.description)));
    }
    handleColumnClick(col) {
        if (!col.sortable)
            return;
        if (col.field == this.table.sortName) {
            if (this.table.sortOrder == 'desc')
                this.table.sortChange.emit({ sortName: undefined, sortOrder: undefined });
            else
                this.table.sortChange.emit({ sortName: this.table.sortName, sortOrder: 'desc' });
        }
        else
            this.table.sortChange.emit({ sortName: col.field, sortOrder: 'asc' });
    }
    async handleColumnCheckChange(ev, col) {
        const checked = ev.detail;
        if (checked)
            await this.table.checkAll(col.field);
        else
            await this.table.uncheckAll(col.field);
    }
    async initSelect() {
        if (!this.dataSet || !this.table.columns)
            return;
        for (const row of this.dataSet) {
            await this.updateRowSelection(row);
        }
    }
    getTotalChecked(field) {
        if (!this.table.totalChecked)
            return 0;
        return this.table.totalChecked[field];
    }
    computeCellClasses(col, classes) {
        return {
            'text-center': col.align === 'center',
            'text-start': !col.align || col.align === 'left',
            'text-end': col.align === 'right',
            'align-middle': !col.valign || col.valign === 'middle',
            'align-top': col.valign === 'top',
            'align-bottom': col.valign === 'bottom',
            ...(classes || {}),
            ...(col.classes || {}),
        };
    }
    renderCell(row, index, col) {
        const cellClasses = this.computeCellClasses(col, { 'text-xs': col.type === 'serialNumber', 'detail-toggle': col.type === 'detail' });
        return (h("td", { "data-field": col.field, class: cellClasses }, this.renderCellContent(row, index, col)));
    }
    renderCellContent(row, index, col) {
        return h("storm-table-cell", { table: this.table, row: row, index: index, column: col, "time-stamp": this.timeStamp, "data-field": col.field });
    }
    renderRow(row, index) {
        const rowId = getFieldValue(row, this.table.fieldId);
        const isSelected = getFieldValue(row, this.table.fieldSelected);
        const visibleColumns = this.table.columns.filter(col => col.visible !== false);
        const mainRow = (h("tr", { "data-id": rowId, class: { 'table-primary': isSelected }, onClick: () => this.clickRow.emit(rowId) }, visibleColumns.map(col => this.renderCell(row, index, col))));
        if (this.table.isColumnResponsive) {
            const detailCol = this.table.columns.find(col => col.type === 'detail');
            const detailClasses = detailCol?.classes ? { ...detailCol.classes, 'row-detail': true, 'd-none': true } : { 'row-detail': true, 'd-none': true };
            return [
                mainRow,
                h("tr", { class: detailClasses, "data-index": index }, h("td", { class: "full-cell", colSpan: this.table.expand.visibleColumnCount }, h("ul", { class: "list-group list-group-flush" }, this.renderDetail(row, index)))),
            ];
        }
        return mainRow;
    }
    renderDetail(row, index) {
        return this.table.columns
            .filter(col => col.responsive === true && col.visible !== false)
            .map(col => (h("li", { class: { 'list-group-item': true, ...(col.rwdClassReversal || {}) } }, h("span", { class: "detail-title" }, col.title), h("span", null, this.renderCellContent(row, index, col)))));
    }
    renderRows() {
        if (this.table.groupBy) {
            let groupItem = '';
            const result = [];
            this.dataSet.forEach((row, i) => {
                const groupByValue = getFieldValue(row, this.table.groupBy);
                if (groupByValue !== groupItem) {
                    groupItem = groupByValue;
                    result.push(h("tr", { class: "group-item table-info" }, h("th", { class: "align-middle full-cell", colSpan: this.table.expand.visibleColumnCount }, groupByValue)));
                }
                result.push(this.renderRow(row, i));
            });
            return result;
        }
        return this.dataSet.map((row, index) => this.renderRow(row, index));
    }
    renderBody() {
        if (!this.dataSet?.length) {
            const emptyClasses = {
                'align-middle': true,
                'text-center': true,
                'full-cell': true,
                ...(this.table.classes?.['table-sm'] ? { 'height-100': true } : { 'height-300': true }),
            };
            return (h("tbody", null, h("tr", null, h("td", { class: emptyClasses, colSpan: this.table.expand.visibleColumnCount }, h("p", { class: "p-4 h6" }, "\u6C92\u6709\u627E\u5230\u7B26\u5408\u7684\u7D50\u679C")))));
        }
        return h("tbody", null, this.renderRows());
    }
    render() {
        return (h(Host, { key: '2e99ed6ae3c0749917fa721462b20a4b04e69a90' }, h("table", { key: 'a555ef275610956179ebfea834568fc877ebb6f9', class: { ...{ table: true, 'mb-0': true, 'table-hover': !!this.table.clickRowTo }, ...parseClassNames(this.table.classes) } }, this.renderThead(), this.renderBody())));
    }
    async componentDidRender() {
        await this.initSelect();
    }
};
StormTableBody.style = stormTableBodyCss;

export { StormTableBody as storm_table_body };
//# sourceMappingURL=storm-table-body.entry.esm.js.map

//# sourceMappingURL=storm-table-body.entry.js.map