import { Component, OnInit } from '@angular/core';

import { PagetitleService } from '../../../core/pagetitle/pagetitle.service';

@Component({
    selector: 'app-wall',
    templateUrl: './wall.component.html',
    styleUrls: ['./wall.component.scss']
})
export class WallComponent implements OnInit {

    constructor(pt: PagetitleService) {
        pt.setTitle('Wall');
    }

    ngOnInit() {
    }

}
