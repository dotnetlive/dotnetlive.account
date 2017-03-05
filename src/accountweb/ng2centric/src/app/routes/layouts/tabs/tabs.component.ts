import { Component, OnInit, Injector } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

import { PagetitleService } from '../../../core/pagetitle/pagetitle.service';

@Component({
    selector: 'app-tabs',
    templateUrl: './tabs.component.html',
    styleUrls: ['./tabs.component.scss']
})
export class TabsComponent implements OnInit {
    router: Router;
    tabs = [
        { heading: 'Home', route: 'layouts/tabs/home' },
        { heading: 'Profile', route: 'layouts/tabs/profile' },
        { heading: 'Messages', route: 'layouts/tabs/message' },
    ];

    constructor(pt: PagetitleService, private route: ActivatedRoute, private injector: Injector) {
        pt.setTitle('Tabs');
    }

    ngOnInit() {
        this.router = this.injector.get(Router);
    }

    isActive(_route: string) {
        return this.router.isActive(_route, true);
    }

    go(_route) {
        this.router.navigate([_route]);
    };

}
