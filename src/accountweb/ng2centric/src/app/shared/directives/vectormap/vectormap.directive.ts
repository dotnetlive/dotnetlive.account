import { OnInit, Directive, Input, ElementRef, DoCheck } from '@angular/core';
declare var $: any;

@Directive({
    selector: '[vectormap]'
})
export class VectormapDirective implements OnInit, DoCheck {

    @Input() mapHeight: number;
    @Input() mapOptions: any;
    @Input() mapSeries: any;
    @Input() mapMarkers: any;

    $element: any;

    _markersLen = 0; // change detection

    constructor(private element: ElementRef) { }

    ngOnInit() {

        this.$element = $(this.element.nativeElement);
        this.$element.css('height', this.mapHeight);

        if (!this.$element.vectorMap) {
            return;
        }
        if( this.mapMarkers ) {
            this.mapOptions.markers = this.mapMarkers;
        }
        if( this.mapSeries ) {
            this.mapOptions.series = this.mapSeries;
        }

        this.$element.vectorMap(this.mapOptions);

    }

    // compare length of markers, if they change reload the map
    // and store the current value for next check
    ngDoCheck() {
        if (this._markersLen !== this.mapMarkers.length) {
            this.mapOptions.markers = this.mapMarkers;
            this.$element.empty().vectorMap(this.mapOptions);
            this._markersLen = this.mapMarkers.length;
        }
    }

}
