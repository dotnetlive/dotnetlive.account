import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ng2-bootstrap';

@Component({
    selector: 'app-messageview',
    templateUrl: './messageview.component.html',
    styleUrls: ['./messageview.component.scss']
})
export class MessageviewComponent implements OnInit {

    @ViewChild('msgViewModal') public msgViewModal: ModalDirective;

    constructor() { }

    ngOnInit() {
    }

    showBackdrop() {
        let el = document.createElement('div');
        el.className = 'modal-backdrop fade in';
        document.body.appendChild(el);
    }
    hideBackdrop() {
        document.body.removeChild(document.querySelector('.modal-backdrop'));
    }
}
