import { Component, OnInit, ViewEncapsulation } from '@angular/core';

import { SettingsService } from '../shared/settings/settings.service';

@Component({
    selector: 'app-layout',
    templateUrl: './layout.component.html',
    styleUrls: ['./layout.component.scss', './layout-variants.scss'],
    encapsulation: ViewEncapsulation.None
})
export class LayoutComponent implements OnInit {

    constructor(private settings: SettingsService) { }

    ngOnInit() { }

    layout() {
        return [

            this.settings.app.sidebar.visible ? 'sidebar-visible' : '',
            this.settings.app.sidebar.offcanvas ? 'sidebar-offcanvas' : '',
            this.settings.app.sidebar.offcanvasVisible ? 'offcanvas-visible' : ''

        ].join(' ');
    }

    closeSidebar() {
        this.settings.app.sidebar.visible = false;
    }
}
