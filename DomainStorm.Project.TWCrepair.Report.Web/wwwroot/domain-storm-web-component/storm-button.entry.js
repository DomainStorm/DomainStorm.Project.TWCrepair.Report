import { r as registerInstance, e as createEvent, h } from './index-BpF8IqPI.js';
import { F as FnIcon } from './functional-Cx8laaX_.js';
import { o as mergeClasses, p as parseClassNames } from './utils-BOoVSa4-.js';

const stormButtonCss = ":host{display:block}:host .btn:focus{outline:none !important;box-shadow:none !important}:host .btn-group-sm>.btn i,:host .btn.btn-sm i{font-size:1rem !important;margin-top:-0.2rem !important;margin-bottom:-0.15rem !important;margin-left:-0.2rem !important;margin-right:-0.15rem !important}:host .btn i{padding-left:0.25rem;padding-right:0.25rem}";

const StormButton = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.buttonClick = createEvent(this, "buttonClick", 7);
    }
    buttonStyle;
    active;
    disabled;
    size = '';
    icon;
    label;
    type = 'button';
    form;
    rounded;
    tooltip;
    buttonData;
    class;
    buttonClick;
    handleClick = (e) => {
        if (this.type === 'button') {
            e.stopPropagation();
            this.buttonClick.emit();
        }
    };
    iconRender() {
        if (!this.icon)
            return;
        return (h("span", { class: "btn-inner--icon" }, h(FnIcon, null, this.icon)));
    }
    labelRender() {
        if (!this.label)
            return;
        return (h("span", { class: {
                'btn-inner--text': !!this.icon,
                'storm-button-label': true,
            } }, this.label));
    }
    buttonRender() {
        const classes = mergeClasses({
            'm-1': true,
            btn: true,
            active: this.active === true,
            disabled: this.disabled === true,
            'btn-icon-only': !!this.icon && !this.label,
            'btn-rounded': this.rounded === true,
            'd-inline-flex': true,
            'align-items-center': true,
            'justify-content-center': true,
        }, parseClassNames(this.class));
        if (this.size)
            classes['btn-' + this.size] = true;
        if (this.buttonStyle && this.buttonStyle.startsWith('gradient-'))
            classes['bg-' + this.buttonStyle] = true;
        else
            classes['btn-' + (this.buttonStyle ?? 'primary')] = true;
        return (h("button", { type: this.type, class: classes, form: this.form, onClick: this.handleClick }, this.iconRender(), this.labelRender(), h("slot", null)));
    }
    render() {
        if (this.tooltip) {
            return h("storm-tooltip", { heading: this.tooltip }, this.buttonRender());
        }
        else {
            return this.buttonRender();
        }
    }
};
StormButton.style = stormButtonCss;

export { StormButton as storm_button };
//# sourceMappingURL=storm-button.entry.esm.js.map

//# sourceMappingURL=storm-button.entry.js.map