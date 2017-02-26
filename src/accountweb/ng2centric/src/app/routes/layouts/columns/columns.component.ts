import { Component, OnInit } from '@angular/core';

import { PagetitleService } from '../../../core/pagetitle/pagetitle.service';

@Component({
    selector: 'app-columns',
    templateUrl: './columns.component.html',
    styleUrls: ['./columns.component.scss']
})
export class ColumnsComponent implements OnInit {

    constructor(pt: PagetitleService) {
        pt.setTitle('Columns');
    }

    ngOnInit() {
    }

}
