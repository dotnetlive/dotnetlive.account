import { Component, OnInit } from '@angular/core';

import { PagetitleService } from '../../../core/pagetitle/pagetitle.service';

@Component({
    selector: 'app-overlap',
    templateUrl: './overlap.component.html',
    styleUrls: ['./overlap.component.scss']
})
export class OverlapComponent implements OnInit {

    constructor(pt: PagetitleService) {
        pt.setTitle('Overlap');
    }
    ngOnInit() {
    }

}
