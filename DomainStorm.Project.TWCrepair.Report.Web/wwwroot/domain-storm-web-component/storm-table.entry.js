import { r as registerInstance, e as createEvent, a as getElement, f as forceUpdate, h, d as Host } from './index-BpF8IqPI.js';
import { a as FnSearchInput } from './functional-Cx8laaX_.js';
import { S as SharedStyles } from './shared-styles-B0cWbvbo.js';
import { s as setFieldValue, b as getFieldValue, c as arrayMove, r as resolvePropertyValue, d as debounce, g as generateUid, i as isModuleFunctionString, e as createFunctionFromString, p as parseClassNames } from './utils-BOoVSa4-.js';

const stormTableCss = ":host{display:block;line-height:1}:host .table td,:host .table th{white-space:pre-wrap}:host .table thead{z-index:1}:host .table.table-sm thead th{padding:0.5rem 0.25rem}:host .table thead th{padding:0.75rem 0.5rem}:host .table thead th.thead-xxs{width:2.3rem}:host .table-sm thead th.thead-xxs{padding:0.25rem}:host .form-check{padding:0}:host .table-sorter{cursor:pointer}:host .table-sorter>.cell{display:inline-block;height:100%;position:relative;width:100%;line-height:1.1}:host .table-sorter>.cell:before{border-top:5px solid #000;bottom:.15rem}:host .table-sorter>.cell:after{border-bottom:5px solid #000;border-top:5px solid transparent;top:-.2rem}:host .table-sorter>.cell:after,:host .table-sorter>.cell:before{content:\"\";height:0;width:0;position:absolute;right:5px;border-left:5px solid transparent;border-right:5px solid transparent;opacity:.3}:host .table-sorter.asc>.cell:after,:host .table-sorter.desc>.cell:before{opacity:.7}:host .table-sorter.asc>.cell:before,:host .table-sorter.desc>.cell:after{opacity:0}:host table{table-layout:fixed}:host .no-records-found{height:30vh}:host .fixed-table-header th{position:sticky;top:0}:host .fixed-table-header thead{position:sticky;top:0;background:white}:host .fixed-table-header thead tr{border-top-width:0}:host .table-description{font-size:0.5rem;font-weight:normal !important}:host .table-top,:host .table-bottom{padding:0.5rem}:host .table-top label{margin-bottom:0}:host .table-selectedInfo,:host .table-pageSizeMenu,:host .table-pageInfo{float:left;color:#7b809a;font-size:.875rem}:host .table-selectedInfo,:host .table-pageInfo{margin:0.7rem 0;padding-right:2rem}:host .table-search,:host .table-pagination{float:right}:host .table .form-check>.form-check-input[type=radio]{background-color:#fff;cursor:pointer}:host .table-bottom:after,:host .table-top:after{clear:both;content:\" \";display:table}:host .table-search input{font-size:.875rem;color:#495057;border:1px solid #f0f2f5;border-radius:0.375rem;padding:6px 12px}:host .table-top .table-pageSizeMenu label select{border-color:#f0f2f5;border-radius:0.375rem;padding:6px}:host .tr.table-sorter{cursor:pointer}:host tr storm-toolbar storm-button .btn{width:1.5rem;height:1.5rem;padding:0.3rem 0.3rem}:host tr:hover storm-toolbar.storm-dynamic-label storm-button .btn:not(.btn-icon-only){width:auto;height:auto;padding:.06rem .5rem .06rem .2rem}:host tr storm-toolbar.storm-dynamic-label storm-button .storm-button-label{display:none}:host tr:hover storm-toolbar.storm-dynamic-label storm-button .storm-button-label{display:block}";

const StormTable = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.pageNumberChange = createEvent(this, "pageNumberChange", 7);
        this.pageSizeChange = createEvent(this, "pageSizeChange", 7);
        this.sortChange = createEvent(this, "sortChange", 7);
        this.searchChanged = createEvent(this, "searchChanged", 7);
        this.tableWillRender = createEvent(this, "tableWillRender", 7);
        this.rowMove = createEvent(this, "rowMove", 7);
        this.rowWillSelect = createEvent(this, "rowWillSelect", 7);
        this.rowWillUnselect = createEvent(this, "rowWillUnselect", 7);
        this.rowSelect = createEvent(this, "rowSelect", 7);
        this.rowUnselect = createEvent(this, "rowUnselect", 7);
        this.rowDidSelect = createEvent(this, "rowDidSelect", 7);
        this.rowDidUnselect = createEvent(this, "rowDidUnselect", 7);
        this.rowSelectChange = createEvent(this, "rowSelectChange", 7);
        this.selectWillChange = createEvent(this, "selectWillChange", 7);
        this.selectChange = createEvent(this, "selectChange", 7);
        this.selectDidChange = createEvent(this, "selectDidChange", 7);
        this.cellWillCheck = createEvent(this, "cellWillCheck", 7);
        this.cellWillUncheck = createEvent(this, "cellWillUncheck", 7);
        this.cellCheck = createEvent(this, "cellCheck", 7);
        this.cellUncheck = createEvent(this, "cellUncheck", 7);
        this.cellDidCheck = createEvent(this, "cellDidCheck", 7);
        this.cellDidUncheck = createEvent(this, "cellDidUncheck", 7);
        this.cellCheckChange = createEvent(this, "cellCheckChange", 7);
        this.checkWillChange = createEvent(this, "checkWillChange", 7);
        this.checkChange = createEvent(this, "checkChange", 7);
        this.checkDidChange = createEvent(this, "checkDidChange", 7);
        this.clickRow = createEvent(this, "clickRow", 7);
    }
    __pageDate;
    __cellCheckChangeTimeoutRafId = {};
    __bodyRef;
    __selectedInfoRef;
    __breakpoints = [
        { size: '4xs', width: 239 },
        { size: '3xs', width: 359 },
        { size: 'xxs', width: 479 },
        { size: 'xs', width: 576 },
        { size: 'sm', width: 768 },
        { size: 'md', width: 992 },
        { size: 'lg', width: 1200 },
        { size: 'xl', width: 1400 },
        { size: 'xxl', width: 1600 },
        { size: '3xl', width: 1920 },
        { size: '4xl', width: Infinity },
    ];
    get host() { return getElement(this); }
    fieldId = 'id';
    fieldSelected = '__selected';
    fieldIndex = '__index';
    pagination = false;
    sidePagination = 'stencil';
    searchable = false;
    pageSizeMenu = true;
    pageInfo = true;
    selectedInfo = true;
    escape = true;
    classes;
    cellTemplate = '{value}';
    clickRowTo;
    selection = 'single';
    pageList = [5, 10, 15, 20, 25];
    tableHeight;
    columns;
    dataSet;
    pageNumber = 1;
    pageSize = 10;
    searchText;
    sortName;
    sortOrder;
    groupBy;
    totalRows;
    totalSelected;
    totalChecked;
    isColumnResponsive = false;
    expand = { visibleColumnCount: 0 };
    pageNumberChange;
    pageSizeChange;
    sortChange;
    searchChanged;
    tableWillRender;
    rowMove;
    rowWillSelect;
    rowWillUnselect;
    rowSelect;
    rowUnselect;
    rowDidSelect;
    rowDidUnselect;
    rowSelectChange;
    selectWillChange;
    selectChange;
    selectDidChange;
    cellWillCheck;
    cellWillUncheck;
    cellCheck;
    cellUncheck;
    cellDidCheck;
    cellDidUncheck;
    cellCheckChange;
    checkWillChange;
    checkChange;
    checkDidChange;
    clickRow;
    watchDataSetHandler() {
        this.initDataSet();
        this.rerender();
    }
    async watchColumnsHandler() {
        this.initColumns();
    }
    watchTotalSelectedHandler(_newValue, _oldValue) {
        this.debouncedDisplaySelectedCount();
    }
    searchChangedHandler(ev) {
        if (this.sidePagination === 'stencil') {
            this.searchText = ev.detail;
        }
        else {
            this.tableWillRender.emit({
                pageNumber: this.pageNumber,
                pageSize: this.pageSize,
                sortName: this.sortName,
                sortOrder: this.sortOrder,
                searchText: ev.detail,
            });
        }
    }
    pageNumberChangeHandler(ev) {
        if (this.sidePagination === 'stencil') {
            this.pageNumber = ev.detail;
        }
        else {
            this.tableWillRender.emit({ pageNumber: ev.detail, pageSize: this.pageSize, sortName: this.sortName, sortOrder: this.sortOrder, searchText: this.searchText });
        }
    }
    pageSizeChangeHandler(ev) {
        if (this.sidePagination === 'stencil') {
            this.pageSize = ev.detail;
        }
        else {
            this.tableWillRender.emit({ pageNumber: this.pageNumber, pageSize: ev.detail, sortName: this.sortName, sortOrder: this.sortOrder, searchText: this.searchText });
        }
    }
    sortChangeHandler(ev) {
        if (this.sidePagination === 'stencil') {
            this.sortName = ev.detail.sortName;
            this.sortOrder = ev.detail.sortOrder;
        }
        else {
            this.tableWillRender.emit({
                pageNumber: this.pageNumber,
                pageSize: this.pageSize,
                sortName: ev.detail.sortName,
                sortOrder: ev.detail.sortOrder,
                searchText: this.searchText,
            });
        }
    }
    async rowWillSelectHandler(ev) {
        if (this.sidePagination === 'stencil') {
            if (this.selection === 'single') {
                await this.unselectAll();
            }
            this.rowSelect.emit(ev.detail);
        }
    }
    async rowSelectHandler(ev) {
        const row = await this.getRowById(ev.detail);
        setFieldValue(row, this.fieldSelected, true);
        this.__bodyRef.updateRowSelection(row);
        this.rowDidSelect.emit(row);
    }
    rowDidSelectHandler(ev) {
        this.rowSelectChange.emit(ev.detail);
    }
    rowWillUnselectHandler(ev) {
        if (this.sidePagination === 'stencil') {
            this.rowUnselect.emit(ev.detail);
        }
    }
    async rowUnselectHandler(ev) {
        const row = await this.getRowById(ev.detail);
        setFieldValue(row, this.fieldSelected, false);
        this.__bodyRef.updateRowSelection(row);
        this.rowDidSelect.emit(row);
    }
    rowDidUnselectHandler(ev) {
        this.rowSelectChange.emit(ev.detail);
    }
    rowSelectChangeHandler(_) {
        this.debouncedRowSelectionUpdate();
    }
    async selectWillChangeHandler(_) {
        if (this.sidePagination === 'stencil') {
            const selected = await this.getSelected();
            this.selectChange.emit(selected.length);
        }
    }
    selectChangeHandler(ev) {
        this.totalSelected = ev.detail;
        this.debouncedDisplaySelectedCount();
        this.selectDidChange.emit(ev.detail);
    }
    async cellWillCheckHandler(ev) {
        if (this.sidePagination === 'stencil') {
            const col = await this.getColumnByField(ev.detail.field);
            if (col.type === 'radio') {
                await this.uncheckAll(col.field);
            }
            this.cellCheck.emit(ev.detail);
        }
    }
    async cellCheckHandler(ev) {
        const col = await this.getColumnByField(ev.detail.field);
        const row = await this.getRowById(ev.detail.id);
        setFieldValue(row, col.field, true);
        this.__bodyRef.updateRowCheckbox(row, col);
        this.cellDidCheck.emit({ row: row, column: col });
    }
    cellDidCheckHandler(ev) {
        this.cellCheckChange.emit(ev.detail);
    }
    cellWillUncheckHandler(ev) {
        if (this.sidePagination === 'stencil') {
            this.cellUncheck.emit(ev.detail);
        }
    }
    async cellUncheckHandler(ev) {
        const col = await this.getColumnByField(ev.detail.field);
        const row = await this.getRowById(ev.detail.id);
        setFieldValue(row, col.field, false);
        this.__bodyRef.updateRowCheckbox(row, col);
        this.cellDidCheck.emit({ row: row, column: col });
    }
    cellDidUncheckHandler(ev) {
        this.cellCheckChange.emit(ev.detail);
    }
    cellCheckChangeHandler(ev) {
        const col = ev.detail.column;
        const rafId = this.__cellCheckChangeTimeoutRafId[col.field];
        if (rafId) {
            clearTimeout(rafId);
            delete this.__cellCheckChangeTimeoutRafId[col.field];
        }
        this.__cellCheckChangeTimeoutRafId[col.field] = setTimeout(() => {
            delete this.__cellCheckChangeTimeoutRafId[col.field];
            this.checkWillChange.emit(col.field);
        }, 100);
    }
    async checkWillChangeHandler(ev) {
        if (this.sidePagination === 'stencil') {
            const checked = await this.getChecked(ev.detail);
            this.checkChange.emit({ total: checked.length, field: ev.detail });
        }
    }
    async checkChangeHandler(ev) {
        this.setTotalChecked(ev.detail.field, ev.detail.total);
        if (ev.detail.field === this.fieldSelected) {
            this.totalSelected = ev.detail.total;
        }
        const col = await this.getColumnByField(ev.detail.field);
        await this.__bodyRef.updateColumnCheckbox(col);
        this.checkDidChange.emit({ total: ev.detail.total, column: col });
    }
    async clickCellHandler(ev) {
        const col = await this.getColumnByField(ev.detail.field);
        if (col.type === 'checkbox' || col.type === 'radio') {
            await this.checkClick(ev.detail.id, ev.detail.field);
        }
        else {
            this.clickRow.emit(ev.detail.id);
        }
    }
    async clickRowHandler(ev) {
        if (this.clickRowTo === 'check') {
            await this.checkClick(ev.detail, this.fieldSelected);
        }
        else if (this.clickRowTo === 'select') {
            const row = await this.getRowById(ev.detail);
            getFieldValue(row, this.fieldSelected) ? this.unselect(ev.detail) : this.select(ev.detail);
        }
    }
    async moveRow(from, to) {
        arrayMove(this.dataSet, from, to);
        this.dataSet.forEach((row, index) => {
            const rowIndex = getFieldValue(row, this.fieldIndex);
            if (rowIndex !== index) {
                this.rowMove.emit({
                    row: row,
                    from: rowIndex,
                    to: index,
                });
                setFieldValue(row, this.fieldIndex, index);
            }
        });
        this.rerender();
    }
    async dispatchCustomEvent(type, detail) {
        this[type].emit(detail);
    }
    async dispatchCellCustomEvent(type, detail) {
        const cell = this.host.shadowRoot?.querySelector('tr[data-id="' + detail['id'] + '"] storm-table-cell[data-field="' + detail['field'] + '"]');
        await cell.dispatchCustomEvent(type, detail);
    }
    async getRowAt(index) {
        return this.dataSet.find(row => getFieldValue(row, this.fieldIndex) === index);
    }
    async getRowById(id) {
        if (typeof id === 'object')
            return id;
        return this.dataSet.find(row => getFieldValue(row, this.fieldId) === id);
    }
    async getColumnByField(field) {
        if (typeof field === 'object')
            return field;
        return this.columns.find(col => col.field === field);
    }
    async getSelected() {
        if (this.sidePagination === 'stencil') {
            return this.dataSet.filter(row => getFieldValue(row, this.fieldSelected));
        }
        return null;
    }
    async getChecked(field) {
        if (this.sidePagination === 'stencil') {
            const col = await this.getColumnByField(field);
            return this.dataSet.filter(row => getFieldValue(row, col.field) !== null && getFieldValue(row, col.field));
        }
        return null;
    }
    async select(id) {
        this.rowWillSelect.emit(id);
    }
    async selectAll() {
        this.dataSet
            .filter(row => !getFieldValue(row, this.fieldSelected))
            .forEach(row => {
            this.select(getFieldValue(row, this.fieldId));
        });
    }
    async unselect(id) {
        this.rowWillUnselect.emit(id);
    }
    async unselectAll() {
        this.dataSet
            .filter(row => getFieldValue(row, this.fieldSelected))
            .forEach(row => {
            this.unselect(getFieldValue(row, this.fieldId));
        });
    }
    async check(id, field) {
        const row = await this.getRowById(id);
        if (getFieldValue(row, field))
            return;
        this.cellWillCheck.emit({ id: id, field: field });
    }
    async uncheck(id, field) {
        const row = await this.getRowById(id);
        if (!getFieldValue(row, field))
            return;
        this.cellWillUncheck.emit({ id: id, field: field });
    }
    async checkAll(field) {
        const col = await this.getColumnByField(field);
        const rowsToCheck = this.dataSet.filter(row => !getFieldValue(row, field));
        for (const row of rowsToCheck) {
            const option = await this.resolveColumnData(col, [row, getFieldValue(row, this.fieldIndex), col]);
            if (option && option.disabled)
                continue;
            this.check(getFieldValue(row, this.fieldId), field);
        }
    }
    async uncheckAll(field) {
        const col = await this.getColumnByField(field);
        const rowsToUncheck = this.dataSet.filter(row => getFieldValue(row, field));
        for (const row of rowsToUncheck) {
            const option = await this.resolveColumnData(col, [row, getFieldValue(row, this.fieldIndex), col]);
            if (option && option.disabled)
                continue;
            this.uncheck(getFieldValue(row, this.fieldId), field);
        }
    }
    async resolveColumnData(col, args) {
        const result = resolvePropertyValue(col, 'data', args);
        return result instanceof Promise ? await result : result;
    }
    rerender = debounce(async () => {
        forceUpdate(this);
    }, 100);
    debouncedDisplaySelectedCount = debounce(async () => {
        if (!this.isSelectedInfoVisible() || !this.__selectedInfoRef)
            return;
        this.__selectedInfoRef.innerHTML = this.totalSelected ? `已選取 ${this.totalSelected} 筆` : '';
    }, 100);
    debouncedRowSelectionUpdate = debounce(async () => {
        this.selectWillChange.emit();
    }, 100);
    async checkClick(id, field) {
        const col = await this.getColumnByField(field);
        const row = await this.getRowById(id);
        if (getFieldValue(row, col.field)) {
            if (col.type === 'radio' && (!col.data || !col.data.unchecked))
                return;
            this.uncheck(id, col.field);
        }
        else
            this.check(id, col.field);
    }
    isPaginationVisible() {
        return this.columns && this.pageInfo && this.pagination && this.__pageDate && this.__pageDate.length > 0;
    }
    isPageSizeMenuVisible() {
        return this.pageSizeMenu && this.isPaginationVisible();
    }
    isSelectedInfoVisible() {
        return this.selectedInfo && this.clickRowTo != null;
    }
    getTotalChecked(field) {
        if (!this.totalChecked)
            return 0;
        return this.totalChecked[field];
    }
    handleSearchChanged(ev) {
        ev.stopPropagation();
        const value = ev.detail?.trim();
        if (this.searchText === value)
            return;
        this.searchChanged.emit(value);
    }
    handleColumnCheckChange(ev, col) {
        const checked = ev.target['checked'];
        if (checked)
            this.checkAll(col.field);
        else
            this.uncheckAll(col.field);
    }
    handleColumnClick(col) {
        if (!col.sortable)
            return;
        if (col.field == this.sortName) {
            if (this.sortOrder == 'desc')
                this.sortChange.emit({ sortName: undefined, sortOrder: undefined });
            else
                this.sortChange.emit({ sortName: this.sortName, sortOrder: 'desc' });
        }
        else
            this.sortChange.emit({ sortName: col.field, sortOrder: 'asc' });
    }
    renderPagination() {
        if (!this.isPaginationVisible())
            return;
        return (h("div", { class: "table-pagination" }, h("storm-pagination", { "page-number": this.pageNumber, "total-pages": Math.ceil(this.totalRows / this.pageSize), onPageChange: ev => {
                this.pageNumberChange.emit(ev.detail);
            } })));
    }
    renderPageSizeMenu() {
        if (!this.isPageSizeMenuVisible())
            return;
        return (h("div", { class: "table-pageSizeMenu" }, h("label", null, "\u986F\u793A", ' ', h("select", { class: "table-selector", onChange: ev => this.pageSizeChange.emit(Number(ev.currentTarget.value)) }, this.pageList.map(page => {
            return (h("option", { value: page, selected: this.pageSize == page }, page));
        })), ' ', "\u7B46\u7D50\u679C")));
    }
    renderSearch() {
        if (!this.searchable)
            return;
        return (h("div", { class: "table-search" }, h(FnSearchInput, { placeholder: "\u8ACB\u8F38\u5165\u95DC\u9375\u5B57", onValueChanged: ev => this.handleSearchChanged(ev) })));
    }
    renderPageInfo() {
        if (!this.pageInfo)
            return;
        return (h("div", { class: "table-pageInfo" }, this.searchable && this.searchText ? `篩選 ${this.__pageDate.length} 筆，` : '', this.pagination && this.__pageDate.length > 0
            ? `顯示第 ${(this.pageNumber - 1) * this.pageSize + 1} 至 ${this.totalRows < this.pageNumber * this.pageSize ? this.totalRows : this.pageNumber * this.pageSize} 筆，`
            : '', "\u5171 ", this.totalRows, " \u7B46"));
    }
    renderSelectedInfo() {
        if (!this.isSelectedInfoVisible())
            return;
        return h("div", { ref: elm => (this.__selectedInfoRef = elm), class: "table-selectedInfo" });
    }
    renderTableTop() {
        if (!this.isPageSizeMenuVisible() && !this.searchable)
            return;
        return (h("div", { class: "table-top p-1" }, this.renderPageSizeMenu(), this.renderSearch()));
    }
    renderTableBottom() {
        if (!this.isSelectedInfoVisible() && !this.isPaginationVisible())
            return;
        return (h("div", { class: "table-bottom p-1" }, this.renderPageInfo(), this.renderSelectedInfo(), this.renderPagination()));
    }
    initDataSet() {
        if (!this.dataSet?.length || !this.columns?.length)
            return;
        const checkboxRadioColumns = this.columns.filter(column => column.type === 'checkbox' || column.type === 'radio');
        const checkboxRadioColumnsLength = checkboxRadioColumns.length;
        const dataSetLength = this.dataSet.length;
        const isStencilPagination = this.sidePagination === 'stencil';
        const columnCheckedCounts = new Map();
        checkboxRadioColumns.forEach(column => {
            columnCheckedCounts.set(column.field, 0);
        });
        let selectedCount = 0;
        this.sortData(this.dataSet);
        for (let index = 0; index < dataSetLength; index++) {
            const row = this.dataSet[index];
            const fieldId = getFieldValue(row, this.fieldId);
            const fieldIndexValue = getFieldValue(row, this.fieldIndex);
            const fieldSelectedValue = getFieldValue(row, this.fieldSelected);
            if (fieldId == null) {
                setFieldValue(row, this.fieldId, generateUid('row'));
            }
            if (fieldIndexValue == null) {
                setFieldValue(row, this.fieldIndex, index);
            }
            if (fieldSelectedValue == null) {
                setFieldValue(row, this.fieldSelected, false);
            }
            for (let i = 0; i < checkboxRadioColumnsLength; i++) {
                const column = checkboxRadioColumns[i];
                const fieldValue = getFieldValue(row, column.field);
                if (fieldValue == null) {
                    setFieldValue(row, column.field, false);
                }
                else if (isStencilPagination && fieldValue) {
                    columnCheckedCounts.set(column.field, columnCheckedCounts.get(column.field) + 1);
                }
            }
            if (isStencilPagination && (fieldSelectedValue || getFieldValue(row, this.fieldSelected))) {
                selectedCount++;
            }
        }
        if (isStencilPagination) {
            this.totalSelected = selectedCount;
            this.totalRows = dataSetLength;
            for (const [field, count] of columnCheckedCounts) {
                this.setTotalChecked(field, count);
            }
        }
    }
    initColumns() {
        if (!this.columns)
            return;
        this.columns.forEach((col, index) => {
            if (col.type == null) {
                col.type = 'text';
            }
            else if (typeof col.type === 'string' && isModuleFunctionString(col.type)) {
                col.type = createFunctionFromString(col.type);
            }
            if (col.type !== 'action') {
                col.field ??= ['checkbox', 'radio'].includes(col.type) ? generateUid(col.type, index.toString()) : index;
                col.searchable ??= this.searchable;
            }
            const classes = col.classes ?? {};
            const responsiveClassPairs = Object.entries(classes).filter(([className, value]) => className.startsWith('d-table-') && className !== 'd-table-none' && className !== 'd-table-cell' && value === true);
            if (responsiveClassPairs.length > 0) {
                col.responsive = true;
                col.rwdClassReversal = {};
                const hasNone = responsiveClassPairs.some(([cls]) => cls.endsWith('-none'));
                const hasCell = responsiveClassPairs.some(([cls]) => cls.endsWith('-cell'));
                if (hasNone && hasCell) {
                    col.rwdClassReversal['d-table-none'] = classes['d-table-none'] !== true;
                }
                else if (hasNone && !hasCell) {
                    col.rwdClassReversal['d-table-none'] = classes['d-table-none'] !== true;
                }
                for (const [className] of responsiveClassPairs) {
                    if (className.endsWith('-cell')) {
                        const noneClass = className.replace(/-cell$/, '-none');
                        col.rwdClassReversal[noneClass] = true;
                    }
                    else if (className.endsWith('-none')) {
                        const cellClass = className.replace(/-none$/, '-cell');
                        col.rwdClassReversal[cellClass] = true;
                    }
                }
            }
            if (typeof col.data === 'string') {
                if (isModuleFunctionString(col.data)) {
                    col.data = createFunctionFromString(col.data);
                }
                else if (col.data.startsWith('{') && col.data.endsWith('}')) {
                    try {
                        col.data = JSON.parse(col.data);
                    }
                    catch (error) {
                        console.warn('無法解析欄位 data JSON：', col.data);
                    }
                }
            }
        });
        if (this.columns.some(col => col.responsive === true)) {
            this.isColumnResponsive = true;
            this.columns.unshift({
                type: 'detail',
            });
        }
        this.expand.visibleColumnCount = this.columns.filter(column => column.visible !== false).length;
        if (this.clickRowTo === 'check') {
            const checkColumn = this.columns.find(col => col.type === 'checkbox' || col.type === 'radio');
            if (checkColumn && (!this.fieldSelected || this.fieldSelected === '__selected')) {
                this.fieldSelected = checkColumn.field?.toString();
            }
        }
        if (!this.dataSet) {
            this.tableWillRender.emit({
                pageNumber: this.pageNumber,
                pageSize: this.pageSize,
                sortName: this.sortName,
                sortOrder: this.sortOrder,
                searchText: this.searchText,
            });
        }
        else {
            this.initDataSet();
        }
    }
    _resizeObserver;
    responsiveResize() {
        if (this._resizeObserver)
            return;
        this._resizeObserver = new ResizeObserver(() => {
            this.refreshColumnVisibility();
        });
        this._resizeObserver.observe(this.host);
    }
    _breakpointIndex;
    refreshColumnVisibility = debounce(() => {
        const tableElem = this.host.shadowRoot?.querySelector('table');
        if (!tableElem)
            return;
        const tableWidth = tableElem.offsetWidth;
        if (this._breakpointIndex != null) {
            const currentBreakpoint = this.__breakpoints[this._breakpointIndex];
            const prevBreakpoint = this.__breakpoints[this._breakpointIndex - 1];
            if (tableWidth >= currentBreakpoint.width && (!prevBreakpoint || tableWidth < prevBreakpoint.width)) {
                return;
            }
        }
        let newBreakpointIndex;
        const breakpointsLength = this.__breakpoints.length;
        const classesToAdd = [];
        const classesToRemove = [];
        for (let index = 0; index < breakpointsLength; index++) {
            const { size, width: bpWidth } = this.__breakpoints[index];
            const isLast = index === breakpointsLength - 1;
            const isMatch = tableWidth >= bpWidth && (isLast || tableWidth < this.__breakpoints[index + 1].width);
            const className = `storm-table-${size}`;
            if (isMatch) {
                classesToAdd.push(className);
                newBreakpointIndex = index;
            }
            else {
                classesToRemove.push(className);
            }
        }
        classesToAdd.forEach(cls => tableElem.classList.add(cls));
        classesToRemove.forEach(cls => tableElem.classList.remove(cls));
        this._breakpointIndex = newBreakpointIndex;
        const visibleColumns = [];
        let hasResponsiveHidden = false;
        const columnsLength = this.columns.length;
        for (let i = 0; i < columnsLength; i++) {
            const col = this.columns[i];
            const isVisible = this.isColumnVisible(col);
            col['__columnVisible'] = isVisible;
            if (isVisible) {
                visibleColumns.push(col);
            }
            else if (col.responsive === true) {
                hasResponsiveHidden = true;
            }
        }
        const elems = this.__bodyRef?.querySelectorAll('.detail-toggle, .row-detail');
        const elemsArray = elems ? Array.from(elems) : [];
        if (hasResponsiveHidden) {
            this.expand.visibleColumnCount = visibleColumns.length;
            elemsArray.forEach(elem => {
                elem.classList.remove('d-table-none');
            });
        }
        else {
            this.expand.visibleColumnCount = visibleColumns.filter(col => col.type !== 'detail').length;
            elemsArray.forEach(elem => {
                elem.classList.add('d-table-none');
            });
        }
        const colspanValue = this.expand.visibleColumnCount.toString();
        const fullCellElems = this.__bodyRef?.querySelectorAll('.full-cell[colspan]');
        if (fullCellElems) {
            Array.from(fullCellElems).forEach(elem => {
                elem.setAttribute('colspan', colspanValue);
            });
        }
    }, 100);
    isColumnVisible(column) {
        if (column.visible === false)
            return false;
        if (!column.classes || column.responsive !== true)
            return true;
        let isVisible = true;
        if (column.classes['d-table-none'] === true)
            isVisible = false;
        if (column.classes['d-table-cell'] === true)
            isVisible = true;
        if (typeof this._breakpointIndex === 'number' && Array.isArray(this.__breakpoints)) {
            for (let i = 0; i <= this._breakpointIndex; i++) {
                const size = this.__breakpoints[i]?.size;
                if (!size)
                    continue;
                if (column.classes[`d-table-${size}-none`] === true)
                    isVisible = false;
                if (column.classes[`d-table-${size}-cell`] === true)
                    isVisible = true;
            }
        }
        return isVisible;
    }
    setTotalChecked(field, total) {
        this.totalChecked ??= {};
        this.totalChecked[field] = total;
    }
    async componentWillLoad() {
        await SharedStyles.applyShadowRootStylesAsync(this.host, 'material');
        this.initColumns();
    }
    disconnectedCallback() {
        Object.values(this.__cellCheckChangeTimeoutRafId).forEach(id => {
            if (id)
                clearTimeout(id);
        });
        this.__cellCheckChangeTimeoutRafId = {};
        if (this._resizeObserver) {
            this._resizeObserver.disconnect();
            this._resizeObserver = undefined;
        }
    }
    componentWillRender() {
        if (!this.dataSet || !this.columns)
            return;
        this.classes = parseClassNames(this.classes);
        this.__pageDate = this.dataSet;
        if (this.sidePagination === 'stencil') {
            if (this.searchable && this.searchText) {
                const searchText = this.searchText.toLowerCase();
                const searchableColumns = this.columns.filter(col => col.searchable);
                const searchableColumnsLength = searchableColumns.length;
                this.__pageDate = this.__pageDate.filter(row => {
                    for (let i = 0; i < searchableColumnsLength; i++) {
                        const col = searchableColumns[i];
                        const value = getFieldValue(row, col.field);
                        if (String(value).toLowerCase().includes(searchText)) {
                            return true;
                        }
                    }
                    return false;
                });
            }
            this.sortData(this.__pageDate);
        }
    }
    sortData(data) {
        const sortKeys = [];
        if (this.groupBy)
            sortKeys.push(this.groupBy);
        if (this.sortName != null)
            sortKeys.push(this.sortName);
        if (this.fieldIndex !== undefined)
            sortKeys.push(this.fieldIndex);
        data.sort((a, b) => {
            for (const key of sortKeys) {
                const aValue = getFieldValue(a, key);
                const bValue = getFieldValue(b, key);
                if (aValue == null && bValue == null)
                    continue;
                if (aValue == null)
                    return -1;
                if (bValue == null)
                    return 1;
                const isDesc = key === this.sortName && this.sortOrder === 'desc';
                if (typeof aValue === 'string' && typeof bValue === 'string') {
                    const result = aValue.localeCompare(bValue);
                    if (result !== 0)
                        return isDesc ? -result : result;
                }
                else if (typeof aValue === 'number' && typeof bValue === 'number') {
                    if (aValue !== bValue)
                        return isDesc ? bValue - aValue : aValue - bValue;
                }
                else {
                    const result = String(aValue).localeCompare(String(bValue));
                    if (result !== 0)
                        return isDesc ? -result : result;
                }
            }
            return 0;
        });
    }
    render() {
        if (!this.__pageDate)
            return;
        let data = this.__pageDate;
        if (this.pagination && this.sidePagination == 'stencil') {
            const from = (this.pageNumber - 1) * this.pageSize + 1;
            const to = this.pageNumber * this.pageSize;
            data = data.slice(from - 1, to);
        }
        return (h(Host, null, this.renderTableTop(), h("div", { class: { 'text-sm': true, 'table-responsive': true, border: this.classes && !!this.classes['table-bordered'] } }, h("div", { class: { 'table-container': true, 'fixed-table-header': !!this.tableHeight }, style: { height: !!this.tableHeight ? this.tableHeight : 'inherit' } }, h("storm-table-body", { ref: elm => (this.__bodyRef = elm), table: this, dataSet: data, timeStamp: Date.now() }))), this.renderTableBottom()));
    }
    componentDidRender() {
        this.debouncedDisplaySelectedCount();
        if (this.isColumnResponsive) {
            this.responsiveResize();
        }
    }
    static get watchers() { return {
        "dataSet": ["watchDataSetHandler"],
        "columns": ["watchColumnsHandler"],
        "totalSelected": ["watchTotalSelectedHandler"]
    }; }
};
StormTable.style = stormTableCss;

export { StormTable as storm_table };
//# sourceMappingURL=storm-table.entry.esm.js.map

//# sourceMappingURL=storm-table.entry.js.map