import { r as registerInstance, a as getElement, h, d as Host } from './index-BpF8IqPI.js';
import { b as bootstrap_bundleExports } from './bootstrap.bundle-IsdR7fTW.js';
import { S as SharedStyles } from './shared-styles-B0cWbvbo.js';
import './_commonjsHelpers-Cf5sKic0.js';

const stormCarouselCss = ".storm-carousel .center{display:block;text-align:-webkit-center}";

const StormCarousel = class {
    constructor(hostRef) {
        registerInstance(this, hostRef);
    }
    get host() { return getElement(this); }
    dataSet;
    fade = false;
    // @Prop() interval: number | boolean = false   //在一個自動循環的輪播中，項目之間所延遲的時間。 如果為 false，輪播不會自動重播。
    wrap = true; //輪播是否應該連續循環，或是會停止。
    carousel;
    carouselRef;
    // videoRefs: Map<IMediaFileData,HTMLStormVideoElement>
    videoRefs;
    // @Listen('change')
    // listenVideoEnd() {
    //   debugger
    //   this.carousel.next();
    // }
    async componentWillLoad() {
        await SharedStyles.applyShadowRootStylesAsync(this.host, 'material');
    }
    componentDidRender() {
        if (!this.dataSet)
            return;
        // if(!this.carouselRef || this.carousel) return
        this.carousel = new bootstrap_bundleExports.Carousel(this.carouselRef, {
            // interval: true,
            wrap: this.wrap,
            ride: 'carousel',
        });
        // this.carousel = new Carousel(this.carouselRef, {
        //   interval: this.dataSet.playListItems[0].mimeType.startsWith('video/') ? this.interval : true,
        //   wrap: this.wrap,
        //   pause: this.pause
        //   ride: 'carousel'
        // });
        this.carouselRef.addEventListener('slid.bs.carousel', (ev) => {
            this.controlInterval(this.dataSet.playListItems[ev.to]);
        });
        if (this.dataSet && this.dataSet.playListItems?.length > 0) {
            this.controlInterval(this.dataSet.playListItems[0]);
        }
        this.videoRefs?.forEach(v => {
            v.addEventListener('ended', () => {
                this.carousel.next();
            });
        });
    }
    addItem() {
        if (!this.dataSet)
            return;
        // this.videoRefs = new Map<IMediaFileData,HTMLStormVideoElement>()
        this.videoRefs = new Map();
        return this.dataSet.playListItems.map((item, index) => {
            return (h("div", { class: index == 0 ? 'carousel-item center active' : 'carousel-item center', "data-bs-interval": item.playDuration > 0 ? item.playDuration * 1000 : false }, this.addItemElement(item)));
        });
    }
    addItemElement = (item) => {
        if (item.mimeType == 'text/html') {
            return h("div", { innerHTML: item.content });
        }
        else if (item.mimeType.startsWith('image/')) {
            return h("img", { class: "d-block", src: item.url });
        }
        else if (item.mimeType.startsWith('video/')) {
            return (h("video", { ref: el => this.videoRefs.set(item, el), controls: true, autoplay: true }, h("source", { src: item.url, type: item.mimeType })));
            // return <storm-video url={item.url} autoplay={true} reTime={true} ref={el => this.videoRefs.set(item,el)}/>
        }
    };
    controlInterval = (item) => {
        if (item.mimeType.startsWith('video/')) {
            this.carousel.pause();
            this.videoRefs.get(item).currentTime = 0;
            this.videoRefs.get(item).play();
        }
        else {
            this.carousel.cycle();
        }
    };
    render() {
        return (h(Host, { key: '370bc1563df8a6ec30d00c0642f782b715a70638' }, h("div", { key: '26d437c381ef2617a7b2558cefcb608be1e1a22a', id: "carouselControls", class: this.fade ? 'carousel slide carousel-fade' : 'carousel slide', ref: el => (this.carouselRef = el) }, h("div", { key: 'd243e1e528e3f197db00a1f54b67e5a7ade245fa', class: "carousel-inner" }, this.addItem()))));
    }
};
StormCarousel.style = stormCarouselCss;

export { StormCarousel as storm_carousel };
//# sourceMappingURL=storm-carousel.entry.esm.js.map

//# sourceMappingURL=storm-carousel.entry.js.map