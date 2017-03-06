import { Component, OnInit, Injector, ViewEncapsulation } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
declare var $: any;

import { MenuService } from '../../core/menu/menu.service';
import { SettingsService } from '../../shared/settings/settings.service';

@Component({
    selector: 'app-sidebar',
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class SidebarComponent implements OnInit {

    menu: Array<any>;
    router: Router;
    sidebarOffcanvasVisible: boolean;

    constructor(private menuService: MenuService, public settings: SettingsService, private injector: Injector) {

        this.menu = menuService.getMenuSorted();

    }

    ngOnInit() {
        this.router = this.injector.get(Router);
        this.router.events
            .filter(event => event instanceof NavigationEnd)
            .subscribe((event) => {
                this.settings.app.sidebar.visible = false;
            });
    }

    closeSidebar() {
        this.settings.app.sidebar.visible = false;
    }

    handleSidebar(event) {
        let item = this.getItemElement(event);
        // check click is on a tag
        if (!item) return;

        let ele = $(item),
            liparent = ele.parent()[0];

        let lis = ele.parent().parent().children(); // markup: ul > li > a
        // remove .active from childs
        lis.find('li').removeClass('active');
        // remove .active from siblings ()
        $.each(lis, function(key, li) {
            if (li !== liparent)
                $(li).removeClass('active');
        });
        let next = ele.next();
        if (next.length && next[0].tagName === 'UL') {
            ele.parent().toggleClass('active');
            event.preventDefault();
        }
    }

    // find the a element in click context
    // doesn't check deeply, asumens two levels only
    getItemElement(event) {
        let element = event.target,
            parent = element.parentNode;
        if (element.tagName.toLowerCase() === 'a') {
            return element;
        }
        if (parent.tagName.toLowerCase() === 'a') {
            return parent;
        }
        if (parent.parentNode.tagName.toLowerCase() === 'a') {
            return parent.parentNode;
        }
    }
}
