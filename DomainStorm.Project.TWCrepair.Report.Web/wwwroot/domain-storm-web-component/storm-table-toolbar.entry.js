import { r as registerInstance, e as createEvent, a as getElement, h, d as Host } from './index-BpF8IqPI.js';
import { b as getFieldValue, h as isConditions, j as evaluateConditions, n as calculateObjectValue } from './utils-BOoVSa4-.js';

const stormTableToolbarCss = ":host{display:block}:host storm-button,:host storm-dropdown,:host div{vertical-align:middle}:host .btn:hover:not(.active){color:white}:host .btn.btn-outline-primary:hover{background-color:#e91e63}:host .btn.disabled{color:#7b809a;border-color:#7b809a;cursor:not-allowed}";

const StormTableToolbar = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.clickAction = createEvent(this, "clickAction", 7);
    }
    __dateSet;
    get host() { return getElement(this); }
    table;
    column;
    row;
    isDynamicLabel = true;
    timeStamp;
    clickAction;
    componentWillRender() {
        this.__dateSet = this.column.buttons?.map(x => {
            const item = {
                id: x.id,
                type: x.type,
                label: x.label,
                tooltip: x.tooltip,
                icon: x.icon,
                size: 'sm',
                buttonStyle: 'outline-primary',
                rounded: true,
                visible: () => {
                    if (isConditions(x.visible)) {
                        return evaluateConditions(x.visible, this.row);
                    }
                    return calculateObjectValue(this, x.visible, this.row, this.table, getFieldValue(this.row, this.table.fieldIndex)) ?? true;
                },
                disabled: () => {
                    if (isConditions(x.disabled)) {
                        return evaluateConditions(x.disabled, this.row);
                    }
                    return calculateObjectValue(this, x.disabled, this.row, this.table, getFieldValue(this.row, this.table.fieldIndex)) ?? false;
                },
                items: x.items
                    ? x.items.map(y => ({
                        id: y.id,
                        title: y.title,
                        icon: y.icon,
                        selected: y.selected,
                        visible: () => {
                            if (isConditions(y.visible)) {
                                return evaluateConditions(y.visible, this.row);
                            }
                            return calculateObjectValue(this, y.visible, this.row, this.table, getFieldValue(this.row, this.table.fieldIndex)) ?? true;
                        },
                        disabled: () => {
                            if (isConditions(y.disabled)) {
                                return evaluateConditions(y.disabled, this.row);
                            }
                            return calculateObjectValue(this, y.disabled, this.row, this.table, getFieldValue(this.row, this.table.fieldIndex)) ?? false;
                        },
                    }))
                    : null,
                click: (_, dropdownItem = null) => {
                    if (x.action)
                        x.action(this.row, x, this.table, dropdownItem);
                    this.clickAction.emit({
                        id: getFieldValue(this.row, this.table.fieldId),
                        field: this.column.field,
                        actionId: x.id,
                        dropdownItemId: dropdownItem ? dropdownItem.id : null,
                    });
                },
            };
            return item;
        });
    }
    render() {
        return (h(Host, { key: 'c44e6c510dd40fe17a5a7c043c8e41fd586516bb' }, h("storm-toolbar", { key: '14ed24aff46abd9c4c60f666510508d21f80da64', class: {
                'storm-dynamic-label': this.isDynamicLabel,
                'justify-content-start': this.column.align === 'left',
                'justify-content-center': this.column.align === 'center',
                'justify-content-end': this.column.align === 'right',
            }, dataSet: this.__dateSet, timeStamp: this.timeStamp })));
    }
};
StormTableToolbar.style = stormTableToolbarCss;

export { StormTableToolbar as storm_table_toolbar };
//# sourceMappingURL=storm-table-toolbar.entry.esm.js.map

//# sourceMappingURL=storm-table-toolbar.entry.js.map