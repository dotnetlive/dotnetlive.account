import { Component, OnInit } from '@angular/core';

import { PagetitleService } from '../../../core/pagetitle/pagetitle.service';

@Component({
    selector: 'app-classic',
    templateUrl: './classic.component.html',
    styleUrls: ['./classic.component.scss']
})
export class ClassicComponent implements OnInit {

    constructor(pt: PagetitleService) {
        pt.setTitle('Tables Classic');
    }
    ngOnInit() {
    }

}
