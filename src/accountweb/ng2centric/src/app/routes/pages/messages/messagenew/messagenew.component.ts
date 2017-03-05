import { Component, OnInit, ViewChild } from '@angular/core';
import { ModalDirective } from 'ng2-bootstrap';

@Component({
    selector: 'app-messagenew',
    templateUrl: './messagenew.component.html',
    styleUrls: ['./messagenew.component.scss']
})
export class MessagenewComponent implements OnInit {

    @ViewChild('msgNewModal') public msgNewModal: ModalDirective;

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
