import { Directive, OnInit, ElementRef } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/map';

@Directive({
    selector: '[svgreplace]'
})
export class SvgreplaceDirective implements OnInit {

    constructor(private element: ElementRef, private http: Http) { }

    ngOnInit() {
        setTimeout(() => {

            let src = this.element.nativeElement.src;

            if (!src || src.indexOf('.svg') < 0) {
                throw "only support for SVG images"; // return /*only support for SVG images*/;
            }

            this.http.get(src)
                .map(data => data.text())
                .subscribe(data => {
                    this.element.nativeElement.outerHTML = data;
                });

        });
    }

}
