import { r as registerInstance, e as createEvent, a as getElement, h, d as Host } from './index-BpF8IqPI.js';
import { S as SharedStyles } from './shared-styles-B0cWbvbo.js';
import { p as parseClassNames } from './utils-BOoVSa4-.js';

const stormEditTableCss = "";

const StormEditTable = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.change = createEvent(this, "change", 7);
        this.rowView = createEvent(this, "rowView", 7);
        this.rowUpdate = createEvent(this, "rowUpdate", 7);
        this.rowDelete = createEvent(this, "rowDelete", 7);
    }
    get host() { return getElement(this); }
    dataSet;
    columns;
    classes = 'table-hover table-striped table-dark table-sm table-borderless';
    clickRowTo;
    pagination = false;
    searchable = false;
    viewable = true;
    editable = true;
    deletable = true;
    orderable = false;
    escape = true;
    // @Prop() actionColumn: ITableColumn
    change;
    rowView;
    rowUpdate;
    rowDelete;
    tableRef;
    tableResponsiveRef;
    viewAnchors;
    deleteAnchors;
    value;
    async componentWillLoad() {
        await SharedStyles.applyShadowRootStylesAsync(this.host, 'material');
    }
    componentWillRender() {
        if (!this.columns)
            return;
        this.addActionColumnRender();
    }
    addActionColumnRender() {
        const actionColumns = this.columns.filter(c => c.type == 'action');
        if (actionColumns.length == 0)
            return;
        for (var c of actionColumns) {
            c.buttons = [];
        }
        if (this.orderable) {
            actionColumns[0].buttons.push({
                label: '上移',
                icon: 'arrow_upward',
                action: (row, _action, table) => {
                    (async () => {
                        let from = row['__index'];
                        let to = row['__index'] - 1;
                        await table.moveRow(from, to);
                    })();
                },
                visible: row => {
                    return row['__index'] > 0;
                },
            }, {
                label: '下移',
                icon: 'arrow_downward',
                action: (row, _action, table) => {
                    (async () => {
                        let from = row['__index'];
                        let to = row['__index'] + 1;
                        await table.moveRow(from, to);
                    })();
                },
                visible: row => {
                    return row['__index'] < this.dataSet?.length - 1;
                },
            });
        }
        let i = actionColumns.length == 2 ? 1 : 0;
        if (this.viewable) {
            actionColumns[i].buttons.push({
                label: '觀看',
                icon: 'visibility',
                action: (row, _action, _table) => {
                    (async () => {
                        this.rowView.emit(row);
                    })();
                },
            });
        }
        if (this.editable) {
            actionColumns[i].buttons.push({
                label: '修改',
                icon: 'drive_file_rename_outline',
                action: (row, _action, _table) => {
                    (async () => {
                        this.rowUpdate.emit(row);
                    })();
                },
            });
        }
        if (this.deletable) {
            actionColumns[i].buttons.push({
                label: '刪除',
                icon: 'delete',
                action: (row, _action, _table) => {
                    (async () => {
                        this.rowDelete.emit(row);
                    })();
                },
            });
        }
        // const actionColumns = this.columns.filter(c => c.type == "action");
        // if(actionColumns.length == 0)
        //   return;
        // actionColumns[0].render = (value, row, index) => {
        //   let renderList = []
        //   if(this.viewable){
        //     renderList.push('<storm-tooltip title="觀看">',
        //     '<a role="button" class="mx-1 editor-view" data-index="'+index+'">',
        //     '<i class="material-icons">visibility</i>',
        //     '</a>',
        //     '</storm-tooltip>')
        //   }
        //   if(this.editable){
        //     renderList.push('<storm-tooltip title="修改">',
        //     '<a role="button" class="mx-1 editor-update" data-index="'+index+'">',
        //     '<i class="material-icons">drive_file_rename_outline</i>',
        //     '</a>',
        //     '</storm-tooltip>')
        //   }
        //   if(this.deletable){
        //     renderList.push('<storm-tooltip title="刪除">',
        //     '<a role="button" class="mx-1 editor-delete" data-index="'+index+'">',
        //     '<i class="material-icons">delete</i>',
        //     '</a>',
        //     '</storm-tooltip>')
        //   }
        //   return renderList.join('')};
    }
    // async componentDidRender() {
    //   this.tableRef.shadowRoot.querySelectorAll(".editor-view").forEach((element) => {
    //     element.addEventListener('click', (e) => {
    //       const rowIndex = parseInt((e.currentTarget as HTMLElement).getAttribute("data-index"))
    //       this.rowView.emit(this.dataSet[rowIndex]);
    //       e.stopPropagation();
    //     })
    //   });
    //   this.tableRef.shadowRoot.querySelectorAll(".editor-update").forEach((element) => {
    //     element.addEventListener('click', (e) => {
    //       const rowIndex = parseInt((e.currentTarget as HTMLElement).getAttribute("data-index"))
    //       this.rowUpdate.emit(this.dataSet[rowIndex]);
    //       e.stopPropagation();
    //     })
    //   });
    //   this.tableRef.shadowRoot.querySelectorAll(".editor-delete").forEach((element) => {
    //     element.addEventListener('click', (e) => {
    //       const rowIndex = parseInt((e.currentTarget as HTMLElement).getAttribute("data-index"))
    //       this.rowDelete.emit(this.dataSet[rowIndex]);
    //       e.stopPropagation();
    //     })
    //   });
    // }
    render() {
        return (h(Host, { key: '14994b1b43be37c4c742fa59919b54207ee92c87' }, h("storm-table", { key: 'd11c8f06a3b604d2760337821edd8cd98ae0b9ec', searchable: this.searchable, pagination: this.pagination, classes: parseClassNames(this.classes), columns: this.columns, dataSet: this.dataSet, clickRowTo: this.clickRowTo, escape: this.escape, ref: el => (this.tableRef = el) })));
    }
};
StormEditTable.style = stormEditTableCss;

export { StormEditTable as storm_edit_table };
//# sourceMappingURL=storm-edit-table.entry.esm.js.map

//# sourceMappingURL=storm-edit-table.entry.js.map