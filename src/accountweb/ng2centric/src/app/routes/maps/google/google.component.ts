import { Component, OnInit } from '@angular/core';

import { PagetitleService } from '../../../core/pagetitle/pagetitle.service';

@Component({
    selector: 'app-google',
    templateUrl: './google.component.html',
    styleUrls: ['./google.component.scss']
})
export class GoogleComponent implements OnInit {

    lat: number = 33.790807;
    lng: number = -117.835734;
    zoom: number = 14;
    scrollwheel = false;

    constructor(pt: PagetitleService) {
        pt.setTitle('Google Maps');
    }

    ngOnInit() {
    }

}
