import { r as registerInstance, e as createEvent, a as getElement, f as forceUpdate, h, d as Host } from './index-BpF8IqPI.js';
import { F as FnIcon } from './functional-Cx8laaX_.js';
import { b as getFieldValue, s as setFieldValue, d as debounce, l as appendHighlight } from './utils-BOoVSa4-.js';

const stormTreeNodeCss = ":host{display:block;-webkit-user-select:none;user-select:none}.list-group-item.active .form-check:not(.form-switch) .form-check-input[type=\"checkbox\"]:checked{background:#fff !important;border-color:#fff !important}.list-group-item.active .form-check:not(.form-switch) .form-check-input[type=\"checkbox\"]::after{color:#e91e63 !important}.list-group-item .form-check{margin-bottom:0 !important}";

const StormTreeNode = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.nodeSelect = createEvent(this, "nodeSelect", 7);
        this.nodeUnselect = createEvent(this, "nodeUnselect", 7);
        this.nodeClick = createEvent(this, "nodeClick", 7);
        this.nodeDblClick = createEvent(this, "nodeDblClick", 7);
        this.nodeCheck = createEvent(this, "nodeCheck", 7);
        this.nodeUncheck = createEvent(this, "nodeUncheck", 7);
    }
    _child;
    get host() { return getElement(this); }
    href = 'javascript:void(0);';
    dataSet;
    tree;
    parent;
    content;
    searchText;
    nodeSelect;
    nodeUnselect;
    nodeClick;
    nodeDblClick;
    nodeCheck;
    nodeUncheck;
    async handleNodeClick(ev) {
        if (ev.detail !== this.getId())
            return;
        if (this.getSelectable()) {
            if (!this.getSelected()) {
                await this.select();
            }
            else if (this.tree.nullable) {
                await this.unselect();
            }
        }
        else {
            ev.stopPropagation();
            if (this.hasChild()) {
                this.expandToggle();
            }
        }
    }
    expandToggle() {
        this.getExpanded() ? this.collapse() : this.expand();
    }
    getId() {
        return getFieldValue(this.dataSet, this.tree.fieldId);
    }
    getHref() {
        const href = getFieldValue(this.dataSet, this.tree.fieldHref);
        return href ?? this.href;
    }
    getChild() {
        return getFieldValue(this.dataSet, this.tree.fieldChildren) || [];
    }
    getSelectable() {
        const selectable = getFieldValue(this.dataSet, this.tree.fieldSelectable);
        return selectable ?? this.tree.selection != 'none';
    }
    getCheckSelectable() {
        return getFieldValue(this.dataSet, this.tree.fieldCheckSelectable) === true;
    }
    getSelected() {
        return getFieldValue(this.dataSet, this.tree.fieldSelected) === true;
    }
    getChecked() {
        return getFieldValue(this.dataSet, this.tree.fieldChecked) === true;
    }
    getDisabled() {
        return getFieldValue(this.dataSet, this.tree.fieldDisabled) === true;
    }
    setSelected(value) {
        if (getFieldValue(this.dataSet, this.tree.fieldSelected) === value)
            return;
        setFieldValue(this.dataSet, this.tree.fieldSelected, value);
        this.rerender();
    }
    setChecked(value) {
        if (getFieldValue(this.dataSet, this.tree.fieldChecked) === value)
            return;
        setFieldValue(this.dataSet, this.tree.fieldChecked, value);
        this.rerender();
    }
    getExpanded() {
        return getFieldValue(this.dataSet, this.tree.fieldExpanded) === true;
    }
    async setExpanded(value) {
        if (this.getExpanded() === value)
            return;
        setFieldValue(this.dataSet, this.tree.fieldExpanded, value);
        if (value && !getFieldValue(this.dataSet, this.tree.fieldChildLoaded)) {
            await this.tree.loadChild(this);
        }
        this.rerender();
    }
    rerender = debounce(() => {
        forceUpdate(this);
    }, 100);
    async select() {
        if (this.getSelected()) {
            return;
        }
        this.setSelected(true);
        this.nodeSelect.emit(this.getId());
    }
    async unselect() {
        if (!this.getSelected()) {
            return;
        }
        this.setSelected(false);
        this.nodeUnselect.emit(this.getId());
    }
    async check() {
        if (this.getChecked()) {
            return;
        }
        this.setChecked(true);
        this.nodeCheck.emit(this.getId());
    }
    async uncheck() {
        if (!this.getChecked()) {
            return;
        }
        this.setChecked(false);
        this.nodeUncheck.emit(this.getId());
    }
    async expand() {
        await this.setExpanded(true);
    }
    async collapse() {
        await this.setExpanded(false);
    }
    hasChild() {
        return !!this.getChild()?.length;
    }
    renderExpandIcon() {
        if (this.searchText)
            return;
        if (!this.hasChild() || (this.getExpanded() && this.tree.collapseIcon === '') || (!this.getExpanded() && this.tree.expandIcon === '')) {
            return this.tree.expandIconAlign === 'left' ? h("span", { class: "indent" }) : undefined;
        }
        const classes = {
            'mb-1': true,
            'mx-1': true,
            'position-absolute': this.tree.expandIconAlign === 'right',
            'end-0': this.tree.expandIconAlign === 'right',
        };
        return (h("span", { class: classes, onClick: ev => {
                ev.stopPropagation();
                this.expandToggle();
            } }, h(FnIcon, null, this.getExpanded() ? this.tree.collapseIcon : this.tree.expandIcon)));
    }
    renderIndent() {
        if (this.searchText)
            return;
        const indent = [];
        const level = getFieldValue(this.dataSet, this.tree.fieldLevel);
        for (let i = 1; i < level; i++) {
            indent.push(h("span", { class: "indent" }));
        }
        if (!this.hasChild() && this.tree.expandIconAlign === 'left') {
            indent.push(h("span", { class: "leaf" }));
        }
        return indent;
    }
    renderChild() {
        if (this.searchText)
            return;
        if (!this.hasChild() || !this.getExpanded())
            return;
        this._child = this.getChild().map(item => {
            return this.renderNodeItem(item);
        });
        return h("div", { class: "list-group" }, this._child);
    }
    renderNodeItem(item) {
        let content = getFieldValue(item, this.tree.fieldLabel);
        if (content && this.searchText) {
            content = appendHighlight(content, this.searchText);
        }
        const node = h("storm-tree-node", { tree: this.tree, dataSet: item, content: content, searchText: this.searchText });
        return node;
    }
    renderLabel() {
        return h("span", { innerHTML: this.content });
    }
    componentWillLoad() {
        setFieldValue(this.dataSet, this.tree.fieldElement, this);
    }
    renderCheckbox() {
        if (!this.getCheckSelectable())
            return;
        return (h("storm-checkbox", { type: "checkbox", isInline: true, checked: this.getChecked(), onValueChanged: ev => {
                ev.stopPropagation();
            }, onClick: async (ev) => {
                ev.stopPropagation();
                if (this.tree.selectToCheck) {
                    this.nodeClick.emit(this.getId());
                }
                else if (!this.getChecked()) {
                    await this.check();
                }
                else {
                    await this.uncheck();
                }
            } }));
    }
    render() {
        if (!this.dataSet)
            return;
        return (h(Host, null, h("a", { href: this.getHref(), class: {
                'py-1': true,
                'px-1': true,
                'list-group-item': true,
                'list-group-item-action': getFieldValue(this.dataSet, this.tree.fieldSelectable),
                'list-group-item-primary': this.tree.color == 'primary',
                'list-group-item-secondary': this.tree.color == 'secondary',
                'list-group-item-success': this.tree.color == 'success',
                'list-group-item-danger': this.tree.color == 'danger',
                'list-group-item-warning': this.tree.color == 'warning',
                'list-group-item-info': this.tree.color == 'info',
                'list-group-item-light': this.tree.color == 'light',
                'list-group-item-dark': this.tree.color == 'dark',
                'd-flex': true,
                'align-items-end': true,
                active: this.getSelected(),
                disabled: this.getDisabled(),
            }, onClick: ev => {
                ev.stopPropagation();
                this.nodeClick.emit(this.getId());
            }, onDblClick: ev => {
                ev.stopPropagation();
                this.nodeDblClick.emit(this.getId());
            } }, this.renderIndent(), this.renderExpandIcon(), h(FnIcon, null, getFieldValue(this.dataSet, this.tree.fieldIcon)), this.renderCheckbox(), this.renderLabel()), this.renderChild()));
    }
    disconnectedCallback() {
        setFieldValue(this.dataSet, this.tree.fieldElement, undefined);
    }
};
StormTreeNode.style = stormTreeNodeCss;

export { StormTreeNode as storm_tree_node };
//# sourceMappingURL=storm-tree-node.entry.esm.js.map

//# sourceMappingURL=storm-tree-node.entry.js.map