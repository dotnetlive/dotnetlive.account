import { Component, OnInit } from '@angular/core';
declare var $: any;

@Component({
    selector: 'app-spinners',
    templateUrl: './spinners.component.html',
    styleUrls: ['./spinners.component.scss']
})
export class SpinnersComponent implements OnInit {

    constructor() { }

    ngOnInit() {
        $('.loader-inner').loaders();
    }

}
