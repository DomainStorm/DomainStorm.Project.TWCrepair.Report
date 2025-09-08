import { h } from './index-BpF8IqPI.js';
import { p as parseClassNames, d as debounce } from './utils-BOoVSa4-.js';

/**
 * 圖示
 * @param ref 元素的引用函式
 * @param class
 * @param children 圖示名稱
 * @returns JSX 元素
 */
const FnIcon = ({ class: classes, onClick, ref }, children) => {
    if (!children || !Array.isArray(children) || children.length !== 1 || typeof children[0] !== 'string' || !String(children[0]).trim())
        return;
    const iconClass = {
        'material-icons': true,
        'align-middle': true,
        ...parseClassNames(classes),
    };
    return (h("i", { class: iconClass, ref: ref, onClick: ev => {
            if (!onClick)
                return;
            ev.stopPropagation();
            onClick(ev);
        } }, children));
};
/**
 * 切換圖示元件
 * @param class 類別名稱
 * @param on 是否開啟狀態
 * @param onToggle 切換狀態的事件處理函式
 * @param disabled 是否禁用
 * @returns JSX 元素
 */
const FnIconSwitch = ({ class: classes, on = false, onToggle, toggleOnIcon = 'remove', toggleOffIcon = 'add' }) => {
    let onIconRef = null;
    let offIconRef = null;
    let currentOn = on;
    const toggleIcons = () => {
        currentOn = !currentOn;
        if (onIconRef && offIconRef) {
            onIconRef.classList.toggle('d-none', !currentOn);
            offIconRef.classList.toggle('d-none', currentOn);
        }
        onToggle && onToggle(currentOn);
    };
    const handleClick = (ev) => {
        ev.stopPropagation();
        toggleIcons();
    };
    return (h("span", { class: {
            'cursor-pointer': true,
            ...parseClassNames(classes),
        }, onClick: handleClick },
        h(FnIcon, { ref: el => (onIconRef = el), class: { 'd-none': !on } }, toggleOnIcon),
        h(FnIcon, { ref: el => (offIconRef = el), class: { 'd-none': on } }, toggleOffIcon)));
};
/**
 * 搜尋框元件
 *
 * @param ref 對應的 HTMLStormInputGroupElement 元素的引用
 * @param value 輸入框目前的值
 * @param onValueChanged 當值變更時觸發的事件處理函式
 * @param placeholder 輸入框預設文字，預設為 '搜尋'
 *
 * @returns JSX 元素
 */
const FnSearchInput = ({ ref, value, onValueChanged, placeholder }) => {
    const handleValueChanged = debounce((ev) => {
        if (!onValueChanged)
            return;
        onValueChanged(ev);
    }, 1000);
    return (h("div", { class: "mb-1" },
        h("storm-input-group", { ref: ref, placeholder: placeholder ?? '搜尋', "input-style": "outline", "prepend-icon": "search", value: value, onValueChanged: (ev) => {
                ev.stopPropagation();
                handleValueChanged(ev);
            }, onChange: (ev) => {
                if (ev)
                    ev.stopPropagation();
            } })));
};
const FnHr = ({ class: classes }) => (h("hr", { class: {
        dark: true,
        horizontal: true,
        'mx-2': true,
        'my-0': true,
        ...parseClassNames(classes),
    } }));
const FnTag = ({ lable, onClickCancel }) => (h("span", { class: {
        'storm-tag': true,
        badge: true,
        'bg-primary': true,
        'rounded-pill': true,
        'me-1': true,
        'px-2': true,
        'py-1': true,
    } },
    lable,
    onClickCancel != null ? h(FnIcon, { onClick: onClickCancel }, "cancel") : undefined));

export { FnIcon as F, FnSearchInput as a, FnIconSwitch as b, FnTag as c, FnHr as d };
//# sourceMappingURL=functional-Cx8laaX_.js.map

//# sourceMappingURL=functional-Cx8laaX_.js.map