import { Component, OnInit } from '@angular/core';

import { PagetitleService } from '../../../core/pagetitle/pagetitle.service';

@Component({
    selector: 'app-material',
    templateUrl: './material.component.html',
    styleUrls: ['./material.component.scss']
})
export class MaterialComponent implements OnInit {

    user = {};
    switch1 = true;
    switch2 = true;
    switch3 = true;
    switch01 = true;
    switch02 = true;
    switch03 = true;
    switch04 = true;
    switch05 = true;
    switch06 = true;
    switchw01 = true;
    switchw02 = true;
    switchw03 = true;
    switchw04 = true;
    switchw05 = true;
    switchw06 = true;

    constructor(pt: PagetitleService) {
        pt.setTitle('Material Inputs');
    }

    ngOnInit() { }

}
