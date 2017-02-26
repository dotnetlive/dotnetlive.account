import { Component, OnInit } from '@angular/core';

import { PagetitleService } from '../../core/pagetitle/pagetitle.service';

@Component({
    selector: 'app-cards',
    templateUrl: './cards.component.html',
    styleUrls: ['./cards.component.scss']
})
export class CardsComponent implements OnInit {

    counter = 25;
    toggle = false;

    constructor(pt: PagetitleService) {
        pt.setTitle('Cards');
    }

    ngOnInit() {
    }

}
