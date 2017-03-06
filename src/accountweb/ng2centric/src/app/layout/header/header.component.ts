import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ModalDirective } from 'ng2-bootstrap';

import { SettingsService } from '../../shared/settings/settings.service';
import { PagetitleService } from '../../core/pagetitle/pagetitle.service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss', './header.menu-links.scss'],
    encapsulation: ViewEncapsulation.None
})
export class HeaderComponent implements OnInit {

    sidebarVisible: true;
    sidebarOffcanvasVisible: boolean;

    constructor(private settings: SettingsService, private pt: PagetitleService) { }


    ngOnInit() {
    }

    toggleSidebarOffcanvasVisible() {
        this.settings.app.sidebar.offcanvasVisible = !this.settings.app.sidebar.offcanvasVisible;
    }

    toggleSidebar(state) {
        //  state === true -> open
        //  state === false -> close
        //  state === undefined -> toggle
        this.settings.app.sidebar.visible = typeof state !== 'undefined' ? state : !this.settings.app.sidebar.visible;
    }

    openModalSearch() {

    }

    openModalBar() {

    }

}
