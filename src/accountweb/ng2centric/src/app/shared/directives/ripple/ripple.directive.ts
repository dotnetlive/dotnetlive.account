import { Directive, ElementRef, OnDestroy } from '@angular/core';

import { RippleEffect } from './ripple';

@Directive({
    selector: '.ripple'
})
export class RippleDirective implements OnDestroy {

    handler: RippleEffect;

    constructor(element: ElementRef) {
        this.handler = new RippleEffect(element.nativeElement);
    }

    ngOnDestroy() {
        this.handler.destroy();
    }

}
