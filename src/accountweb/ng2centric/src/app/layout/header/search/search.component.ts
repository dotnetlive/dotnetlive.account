import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { ModalDirective } from 'ng2-bootstrap';

@Component({
    selector: 'header-search',
    templateUrl: './search.component.html',
    styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {

    @ViewChild('searchModal') public searchModal:ModalDirective;

    constructor(private element: ElementRef) { }

    ngOnInit() { }

    onModalShown() {
        let input = this.element.nativeElement.querySelector('.header-input-search')
        if(input) input.focus();
    }
}
