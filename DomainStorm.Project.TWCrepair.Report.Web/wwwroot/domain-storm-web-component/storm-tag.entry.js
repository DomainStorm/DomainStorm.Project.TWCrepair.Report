import { r as registerInstance, e as createEvent, a as getElement, h, d as Host } from './index-BpF8IqPI.js';
import { C as Choices } from './choices-DE0ykQng.js';
import { S as SharedStyles } from './shared-styles-B0cWbvbo.js';
import './_commonjsHelpers-Cf5sKic0.js';

const stormTagCss = "";

const StormTag = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.change = createEvent(this, "change", 7);
    }
    get host() { return getElement(this); }
    class;
    label;
    value;
    change;
    async componentWillLoad() {
        await SharedStyles.applyShadowRootStylesAsync(this.host, 'material');
    }
    componentDidLoad() {
        const element = this.host.shadowRoot.querySelector('input');
        new Choices(element, {
            searchEnabled: false,
            removeItemButton: true,
            classNames: {
                containerOuter: 'choices',
                containerInner: 'choices__inner',
                input: 'choices__input',
                inputCloned: 'choices__input--cloned',
                list: 'choices__list',
                listItems: 'choices__list--multiple',
                listSingle: 'choices__list--single',
                listDropdown: 'choices__list--dropdown',
                item: 'choices__item',
                itemSelectable: 'choices__item--selectable',
                itemDisabled: 'choices__item--disabled',
                itemChoice: 'choices__item--choice',
                placeholder: 'choices__placeholder',
                group: 'choices__group',
                groupHeading: 'choices__heading',
                button: 'choices__button',
                activeState: 'is-active',
                focusState: 'is-focused',
                openState: 'is-open',
                disabledState: 'is-disabled',
                highlightedState: 'is-highlighted',
                selectedState: 'is-selected',
                flippedState: 'is-flipped',
                loadingState: 'is-loading',
                noResults: 'has-no-results',
                noChoices: 'has-no-choices',
            },
        });
        this.value?.push(element.value);
        this.change.emit(this.value);
    }
    handleChange = event => {
        this.value = event.target.value;
        this.change.emit(this.value);
        event.preventDefault();
    };
    render() {
        return (h(Host, { key: 'bd0ba28827ca62c238dd95fe02d075b632bf488f' }, h("div", { key: '9a65981619a075b600eca884c0dfeef1d4b32150', class: this.class }, h("label", { key: 'cb6ba08663a73723ea452809cc69cf6a1c6d653b', class: "input-group-static form-label" }, this.label), h("input", { key: '32e5df207185549723b622e00aa2b6d9cea1d853', class: "form-control", type: "text", "data-color": "dark", value: this.value, onChange: e => this.handleChange(e) }))));
    }
};
StormTag.style = stormTagCss;

export { StormTag as storm_tag };
//# sourceMappingURL=storm-tag.entry.esm.js.map

//# sourceMappingURL=storm-tag.entry.js.map