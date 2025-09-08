import { r as registerInstance, e as createEvent, a as getElement, h, d as Host } from './index-BpF8IqPI.js';
import { F as FnIcon } from './functional-Cx8laaX_.js';
import { S as SharedStyles } from './shared-styles-B0cWbvbo.js';
import './utils-BOoVSa4-.js';

const stormPaginationCss = ":host{display:block}:host dl,:host ol,:host ul{margin-bottom:0}";

const StormPagination = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
        this.pageChange = createEvent(this, "pageChange", 7);
    }
    get host() { return getElement(this); }
    pageNumber = 1;
    totalPages;
    paginationSuccessivelySize = 5;
    paginationPagesBySide = 1;
    pageChange;
    pageFrom = -1;
    pageTo = -1;
    cachedPaginationItems = null;
    lastCalculationKey = '';
    setPage(newPageNumber) {
        // 邊界檢查和驗證
        if (newPageNumber < 1 || newPageNumber > this.totalPages || newPageNumber === this.pageNumber) {
            return;
        }
        this.pageNumber = newPageNumber;
        // 清除快取，因為頁碼已改變
        this.cachedPaginationItems = null;
        this.lastCalculationKey = '';
        this.pageChange.emit(this.pageNumber);
    }
    async componentWillLoad() {
        await SharedStyles.applyShadowRootStylesAsync(this.host, 'material');
    }
    disconnectedCallback() {
        // 清理快取
        this.cachedPaginationItems = null;
        this.lastCalculationKey = '';
    }
    calculatePageRange() {
        // 提早返回避免不必要的計算
        if (this.totalPages <= 0) {
            this.pageFrom = 1;
            this.pageTo = 0;
            return;
        }
        // 邊界檢查和自動修正
        if (this.pageNumber > this.totalPages) {
            this.pageNumber = this.totalPages;
        }
        if (this.pageNumber < 1) {
            this.pageNumber = 1;
        }
        // 簡化計算邏輯
        if (this.totalPages <= this.paginationSuccessivelySize) {
            this.pageFrom = 1;
            this.pageTo = this.totalPages;
            return;
        }
        // 計算顯示範圍
        const halfSize = Math.floor(this.paginationSuccessivelySize / 2);
        this.pageFrom = Math.max(1, this.pageNumber - halfSize);
        this.pageTo = Math.min(this.totalPages, this.pageFrom + this.paginationSuccessivelySize - 1);
        // 調整起始位置以保持固定數量的頁面
        if (this.pageTo - this.pageFrom + 1 < this.paginationSuccessivelySize) {
            this.pageFrom = Math.max(1, this.pageTo - this.paginationSuccessivelySize + 1);
        }
    }
    componentWillRender() {
        this.calculatePageRange();
    }
    getPaginationItems() {
        // 使用快取避免重複計算
        const currentKey = `${this.pageNumber}-${this.totalPages}-${this.paginationSuccessivelySize}-${this.paginationPagesBySide}`;
        if (this.lastCalculationKey === currentKey && this.cachedPaginationItems) {
            return this.cachedPaginationItems;
        }
        const paginationItems = [];
        // 左側頁面
        if (this.pageFrom > 1) {
            let max = this.paginationPagesBySide;
            if (max >= this.pageFrom)
                max = this.pageFrom - 1;
            for (let i = 1; i <= max; i++) {
                paginationItems.push({ page: i });
            }
            if (this.pageFrom - 1 === max + 1) {
                paginationItems.push({ page: this.pageFrom - 1 });
            }
            else if (this.pageFrom - 1 > max) {
                paginationItems.push({ title: '...' });
            }
        }
        // 中間頁面
        for (let i = this.pageFrom; i <= this.pageTo; i++) {
            paginationItems.push({ page: i });
        }
        // 右側頁面
        if (this.totalPages > this.pageTo) {
            let min = this.totalPages - (this.paginationPagesBySide - 1);
            if (this.pageTo >= min)
                min = this.pageTo + 1;
            if (this.pageTo + 1 === min - 1) {
                paginationItems.push({ page: this.pageTo + 1 });
            }
            else {
                paginationItems.push({ title: '...' });
            }
            for (let i = min; i <= this.totalPages; i++) {
                paginationItems.push({ page: i });
            }
        }
        // 快取結果
        this.cachedPaginationItems = paginationItems;
        this.lastCalculationKey = currentKey;
        return paginationItems;
    }
    render() {
        const paginationItems = this.getPaginationItems();
        return (h(Host, { key: '8f02f100d6cb1215a01e1bee4efa3d571d2bce41' }, h("ul", { key: 'c667679227e22ce5ee51bbb0c2fe69329fcaabc6', class: "pagination pagination-primary" }, h("li", { key: '8c8170d47b7e9f6ea9bdb6661b5a628e00fb1edd', class: "page-item" }, h("a", { key: '9dae5170d55c7f542a1248d757d36e397898bd19', class: "page-link", href: "javascript:;", "aria-label": "\u4E0A\u4E00\u9801", onClick: () => {
                const newPage = this.pageNumber === 1 ? this.totalPages : this.pageNumber - 1;
                this.setPage(newPage);
            } }, h("span", { key: 'f56ae430949fc776f0268363cd3b9368e70df63e', "aria-hidden": "true" }, h(FnIcon, { key: '133083a8c049c069130032ce25b57aa9ace094df' }, "keyboard_arrow_left")))), paginationItems.map((x, index) => {
            const isActive = x.page === this.pageNumber;
            const isDisabled = x.page == null;
            return (h("li", { key: x.page ? `page-${x.page}` : `ellipsis-${index}`, class: { 'page-item': true, active: isActive, disabled: isDisabled }, onClick: () => {
                    if (x.page && !isActive)
                        this.setPage(x.page);
                } }, h("a", { class: "page-link", href: "javascript:;", "aria-current": isActive ? 'page' : undefined, "aria-label": x.page ? `第 ${x.page} 頁` : undefined }, x.title || x.page?.toString())));
        }), h("li", { key: '3a31eb9d1800289526e041ca678c67eb9659f763', class: "page-item" }, h("a", { key: '24a4083d162a40ab6e2117c57ccdce027f704092', class: "page-link", href: "javascript:;", "aria-label": "\u4E0B\u4E00\u9801", onClick: () => {
                const newPage = this.pageNumber === this.totalPages ? 1 : this.pageNumber + 1;
                this.setPage(newPage);
            } }, h("span", { key: 'a755283e0f11f9d5bace86321e150fbba5c213e0', "aria-hidden": "true" }, h(FnIcon, { key: '4a5762f30b9911f3bfe64bf178bd4bb4ed811e96' }, "keyboard_arrow_right")))))));
    }
};
StormPagination.style = stormPaginationCss;

export { StormPagination as storm_pagination };
//# sourceMappingURL=storm-pagination.entry.esm.js.map

//# sourceMappingURL=storm-pagination.entry.js.map