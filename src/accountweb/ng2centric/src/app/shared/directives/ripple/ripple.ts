declare var $: any;

const TRANSITION_END = 'transitionend webkitTransitionEnd';

/**
 * Ripple effect for common components
 */
export class RippleEffect {

    element;
    rippleElement;
    $rippleElement;
    clickEv;

    /**
     * @param [element] dom element
     */
    constructor(element) {
        let jq = $;

        this.element = jq(element);
        this.rippleElement = this.getElement();
        this.$rippleElement = jq(this.rippleElement);

        this.clickEv = this.detectClickEvent();

        this.element.on(this.clickEv, () => {
            // remove animation on click
            this.$rippleElement.removeClass('md-ripple-animate');
            // Set ripple size and position
            this.calcSizeAndPos();
            // start to animate
            this.$rippleElement
                .addClass('md-ripple-animate');
        });

        this.$rippleElement.on(TRANSITION_END, () => {
            this.$rippleElement
                .removeClass('md-ripple-animate');
            // avoid weird affect when ripple is not active
            this.rippleElement.style.width = 0;
            this.rippleElement.style.height = 0;
        });
    }

    /**
     * Returns the elements used to generate the effect
     * If not exists, it is created by appending a new
     * dom element
     */
    getElement() {
        let dom = this.element[0];
        let rippleElement = dom.querySelector('.md-ripple');

        if (rippleElement === null) {
            // Create ripple
            rippleElement = document.createElement('span');
            rippleElement.className = 'md-ripple';
            // Add ripple to element
            this.element.append(rippleElement);
        }
        return rippleElement;
    }

    /**
     * Determines the better size for the ripple element
     * based on the element attached and calculates the
     * position be fully centered
     */
    calcSizeAndPos() {
        let size = Math.max(this.element.width(), this.element.height());
        this.rippleElement.style.width = size + 'px';
        this.rippleElement.style.height = size + 'px';
        // autocenter (requires css)
        this.rippleElement.style.marginTop = -(size / 2) + 'px';
        this.rippleElement.style.marginLeft = -(size / 2) + 'px';
    }

    detectClickEvent() {
        let isIOS = ((/iphone|ipad/gi).test(navigator.appVersion));
        return isIOS ? 'touchstart' : 'click';
    }

    destroy() {
        this.element.off(this.clickEv);
        this.$rippleElement.off(TRANSITION_END);
    }
}
