import { Component, OnInit } from '@angular/core';

import { PagetitleService } from '../../../core/pagetitle/pagetitle.service';

@Component({
    selector: 'app-boxed',
    templateUrl: './boxed.component.html',
    styleUrls: ['./boxed.component.scss']
})
export class BoxedComponent implements OnInit {

    constructor(pt: PagetitleService) {
        pt.setTitle('Boxed');
    }

    ngOnInit() {
    }

}
