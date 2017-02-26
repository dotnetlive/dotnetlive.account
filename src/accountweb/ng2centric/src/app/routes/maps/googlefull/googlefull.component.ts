import { Component, OnInit } from '@angular/core';

import { PagetitleService } from '../../../core/pagetitle/pagetitle.service';

@Component({
    selector: 'app-googlefull',
    templateUrl: './googlefull.component.html',
    styleUrls: ['./googlefull.component.scss']
})
export class GooglefullComponent implements OnInit {

    myMarkers = [
        { id: 0, name: 'Canada', coords: { latitude: 56.130366, longitude: -106.346771 } },
        { id: 1, name: 'New York', coords: { latitude: 40.712784, longitude: -74.005941 } },
        { id: 2, name: 'Toronto', coords: { latitude: 43.653226, longitude: -79.383184 } },
        { id: 3, name: 'San Francisco', coords: { latitude: 37.774929, longitude: -122.419416 } },
        { id: 4, name: 'Utah', coords: { latitude: 39.320980, longitude: -111.093731 } }
    ];

    currLatitude = this.myMarkers[0].coords.latitude;
    currLongitude = this.myMarkers[0].coords.longitude;

    zoom: number = 4;
    scrollwheel = false;

    constructor(pt: PagetitleService) {
        pt.setTitle('Google Maps Full');
    }

    moveTo(coords) {
        this.currLatitude = coords.latitude;
        this.currLongitude = coords.longitude;
    }

    ngOnInit() {
    }

}
