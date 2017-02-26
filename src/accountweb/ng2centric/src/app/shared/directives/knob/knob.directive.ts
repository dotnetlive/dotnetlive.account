import { OnInit, OnDestroy, OnChanges, Directive, Input, SimpleChange, ElementRef } from '@angular/core';
declare var $: any;

@Directive({
    selector: '[knob]'
})
export class KnobDirective implements OnInit {

    @Input() data;
    @Input() options;
    chart = null;

    constructor(private element: ElementRef) { }

    ngOnInit() {
        this.chart = $(this.element.nativeElement);
        this.chart.val(this.data).knob(this.options);
    }

    ngOnChanges(changes: { [propertyName: string]: SimpleChange }) {
        if (this.chart && changes['data']) {
            this.chart.val(this.data).trigger('change');
        }
        if (this.chart && changes['options']) {
            this.chart.trigger('configure', this.options);
        }
    }

}
