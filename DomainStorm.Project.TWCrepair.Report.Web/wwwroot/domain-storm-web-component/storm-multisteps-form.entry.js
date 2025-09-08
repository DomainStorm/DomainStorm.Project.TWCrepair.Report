import { r as registerInstance, e as createEvent, a as getElement, h, d as Host } from './index-BpF8IqPI.js';

const stormMultistepsFormCss = ".multisteps-form__panel{position:relative !important}";

const StormMultistepsForm = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.formSubmit = createEvent(this, "formSubmit", 7);
    }
    get host() { return getElement(this); }
    stepTitle;
    stepTitleDetail;
    stepButton;
    formSubmit;
    _stepButton;
    dataSetWatcher(newValue) {
        if (typeof newValue === 'string') {
            this._stepButton = JSON.parse(newValue);
        }
        else {
            this._stepButton = newValue;
        }
    }
    //DOM elements
    DOMstrings;
    // @Method()
    // async resize() {
    //   this.setFormHeight()
    // }
    componentWillLoad() {
        if (!this.stepButton)
            return;
        this.dataSetWatcher(this.stepButton);
    }
    componentDidRender() {
        if (!this._stepButton && this.DOMstrings)
            return;
        this.DOMstrings = {
            stepsBtnClass: 'multisteps-form__progress-btn',
            stepsBtns: this.host.querySelectorAll(`.multisteps-form__progress-btn`),
            stepsBar: this.host.querySelector('.multisteps-form__progress'),
            stepsForm: this.host.querySelector('.multisteps-form__form'),
            stepsFormTextareas: this.host.querySelectorAll('.multisteps-form__textarea'),
            stepFormPanelClass: 'multisteps-form__panel',
            stepFormPanels: this.host.querySelectorAll('.multisteps-form__panel'),
            stepPrevBtnClass: 'js-btn-prev',
            stepNextBtnClass: 'js-btn-next',
        };
        //trigger stepbtn[0] by mao
        if (this.DOMstrings.stepsBtns.length > 0)
            this.DOMstrings.stepsBtns[0].click();
        // this.setFormHeight();
        //SETTING PROPER FORM HEIGHT ONLOAD
        // window.addEventListener('load', () => this.setFormHeight(), false);
        //SETTING PROPER FORM HEIGHT ONRESIZE
        // window.addEventListener('resize', () => this.setFormHeight(), false);
    }
    //remove class from a set of items
    removeClasses = (elemSet, className) => {
        elemSet.forEach(elem => {
            elem.classList.remove(className);
        });
    };
    //return exect parent node of the element
    findParent = (elem, parentClass) => {
        let currentNode = elem;
        while (!currentNode.classList.contains(parentClass)) {
            currentNode = currentNode.parentNode;
        }
        return currentNode;
    };
    //get active button step number
    getActiveStep = elem => {
        return Array.from(this.DOMstrings.stepsBtns).indexOf(elem);
    };
    //set all steps before clicked (and clicked too) to active
    setActiveStep = activeStepNum => {
        //remove active state from all the state
        this.removeClasses(this.DOMstrings.stepsBtns, 'js-active');
        //set picked items to active
        this.DOMstrings.stepsBtns.forEach((elem, index) => {
            if (index <= activeStepNum) {
                elem.classList.add('js-active');
            }
        });
    };
    //get active panel
    getActivePanel = () => {
        let activePanel;
        this.DOMstrings.stepFormPanels.forEach(elem => {
            if (elem.classList.contains('js-active')) {
                activePanel = elem;
            }
        });
        return activePanel;
    };
    //open active panel (and close unactive panels)
    setActivePanel = activePanelNum => {
        //remove active class from all the panels
        this.removeClasses(this.DOMstrings.stepFormPanels, 'js-active');
        //show active panel
        this.DOMstrings.stepFormPanels.forEach((elem, index) => {
            if (index === activePanelNum) {
                elem.classList.add('js-active');
                // this.setFormHeight(/*elem*/);
            }
        });
    };
    //從css設定，取消設定高度
    //set form height equal to current panel height
    // formHeight = activePanel => {
    //   const activePanelHeight = activePanel.offsetHeight;
    //   this.DOMstrings.stepsForm.style.height = `${activePanelHeight}px`;
    // };
    //從css設定，取消設定高度
    // setFormHeight = () => {
    //   const activePanel = this.getActivePanel();
    //   this.formHeight(activePanel);
    // };
    stepBarClick = async (e) => {
        //check if click target is a step button
        const eventTarget = e.target;
        if (!eventTarget.classList.contains(`${this.DOMstrings.stepsBtnClass}`)) {
            return;
        }
        //get active button step number
        const activeStep = this.getActiveStep(eventTarget);
        //set all steps before clicked (and clicked too) to active
        this.setActiveStep(activeStep);
        //open active panel
        this.setActivePanel(activeStep);
    };
    prevNextClick = async (e) => {
        const eventTarget = e.target;
        //check if we clicked on `PREV` or NEXT` buttons
        if (!(eventTarget.classList.contains(`${this.DOMstrings.stepPrevBtnClass}`) || eventTarget.classList.contains(`${this.DOMstrings.stepNextBtnClass}`))) {
            return;
        }
        //find active panel
        const activePanel = this.findParent(eventTarget, `${this.DOMstrings.stepFormPanelClass}`);
        let activePanelNum = Array.from(this.DOMstrings.stepFormPanels).indexOf(activePanel);
        //set active step and active panel onclick
        if (eventTarget.classList.contains(`${this.DOMstrings.stepPrevBtnClass}`)) {
            activePanelNum--;
        }
        else {
            activePanelNum++;
        }
        this.setActiveStep(activePanelNum);
        this.setActivePanel(activePanelNum);
    };
    sendClick = async (e) => {
        e.stopPropagation();
        this.formSubmit.emit();
    };
    //add by mao
    setStepBtn = () => {
        return this._stepButton?.map(element => (h("button", { class: "multisteps-form__progress-btn", type: "button", title: element, onClick: e => this.stepBarClick(e) }, element)));
    };
    //add by mao
    setPanel = () => {
        return this._stepButton?.map(element => {
            let index = this._stepButton.indexOf(element);
            return (h("div", { class: "multisteps-form__panel border-radius-xl bg-white", "data-animation": "FadeIn" }, h("div", { class: "multisteps-form__content" }, h("slot", { name: /*"multisteps"+*/ index.toString() }), this.setPrevNextSendBtn(index))));
        });
    };
    //add by mao
    setPrevNextSendBtn = (index) => {
        if (index == 0) {
            return (h("div", { class: "button-row d-flex mt-4" }, h("button", { class: "btn bg-gradient-dark ms-auto mb-0 js-btn-next", type: "button", title: "Next", onClick: e => this.prevNextClick(e) }, "\u4E0B\u4E00\u9801")));
        }
        else if (this._stepButton.length - 1 == index) {
            return (h("div", { class: "button-row d-flex mt-0 mt-md-4" }, h("button", { class: "btn bg-gradient-light mb-0 js-btn-prev", type: "button", title: "Prev", onClick: e => this.prevNextClick(e) }, "\u4E0A\u4E00\u9801"), h("button", { class: "btn bg-gradient-dark ms-auto mb-0", type: "button", title: "Send", onClick: e => this.sendClick(e) }, "\u9001\u51FA")));
        }
        else {
            return (h("div", { class: "row" }, h("div", { class: "button-row d-flex mt-4 col-12" }, h("button", { class: "btn bg-gradient-light mb-0 js-btn-prev", type: "button", title: "Prev", onClick: e => this.prevNextClick(e) }, "\u4E0A\u4E00\u9801"), h("button", { class: "btn bg-gradient-dark ms-auto mb-0 js-btn-next", type: "button", title: "Next", onClick: e => this.prevNextClick(e) }, "\u4E0B\u4E00\u9801"))));
        }
    };
    render() {
        return (h(Host, { key: '452ca989d4d72bc264ce1918c6c948d030469131' }, h("div", { key: '96317e57ba1cc1d63d81946c8c92c6cae685a349', class: "row" }, ' ', h("div", { key: 'c21b4a45c3d28592062a6cf33a7c6336f74b27e1', class: "row" }, ' ', h("div", { key: '13f60d7baf69bc66524734e60cdcda8b76bad80d', class: "col-12 col-lg-8 mx-auto my-5" })), h("div", { key: '397db116aa221e5b95153714b5bcbe7683678460', class: "col-lg-8 col-md-10 col-12 m-auto" }, this.stepTitle ? h("h3", { class: "mt-3 mb-0 text-center" }, this.stepTitle) : '', this.stepTitleDetail ? h("p", { class: "lead font-weight-normal opacity-8 mb-7 text-center" }, this.stepTitleDetail) : '', h("div", { key: 'b49ffcc42b0c825ad5696dd363b3f1b3ebb7bda9', class: "card" }, h("div", { key: '4a532f29ced79d10572a67e9d302e70d8d5dd69e', class: "card-header p-0 position-relative mt-n5 mx-3 z-index-2" }, h("div", { key: 'db818360523c13115f95932a2b6e57ed6dfb68a5', class: "bg-gradient-primary shadow-primary border-radius-lg pt-4 pb-3" }, h("div", { key: '9cc88f88383b454e49426de367b4959f15c90662', class: "multisteps-form__progress" }, this.setStepBtn()))), h("div", { key: '30133013e04a899dd9eb2220e23c62dd19df137b', class: "card-body" }, h("form", { key: '2e1200b0786b583e1935cff437f19c8a89e9b26a', class: "multisteps-form__form" }, this.setPanel())))))));
    }
    static get watchers() { return {
        "stepButton": ["dataSetWatcher"]
    }; }
};
StormMultistepsForm.style = stormMultistepsFormCss;

export { StormMultistepsForm as storm_multisteps_form };
//# sourceMappingURL=storm-multisteps-form.entry.esm.js.map

//# sourceMappingURL=storm-multisteps-form.entry.js.map