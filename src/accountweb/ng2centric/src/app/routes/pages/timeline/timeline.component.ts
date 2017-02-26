import { Component, OnInit, ViewEncapsulation } from '@angular/core';

import { PagetitleService } from '../../../core/pagetitle/pagetitle.service';

@Component({
    selector: 'app-timeline',
    templateUrl: './timeline.component.html',
    styleUrls: ['./timeline.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class TimelineComponent implements OnInit {

    timelineModeAlt = false;

    constructor(pt: PagetitleService) {
        pt.setTitle('Timeline');
    }

    ngOnInit() {
    }

}
