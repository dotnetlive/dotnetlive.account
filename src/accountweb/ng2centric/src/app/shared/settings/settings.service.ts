import { Injectable } from '@angular/core';
declare var $: any;

@Injectable()
export class SettingsService {

    app: any;

    themes = [
        'theme-1',
        'theme-2',
        'theme-3',
        'theme-4',
        'theme-5',
        'theme-6',
        'theme-7',
        'theme-8',
        'theme-9',
    ]

    constructor() {

        // Global Settings
        // -----------------------------------
        this.app = {
            name: 'Centric',
            description: 'Bootstrap Admin Template',
            year: ((new Date()).getFullYear()),
            layout: {
                rtl: false
            },
            sidebar: {
                over: false,
                showheader: true,
                showtoolbar: true,
                visible: false,
                offcanvas: false,
                offcanvasVisible: false
            },
            header: {
                menulink: 'menu-link-slide'
            },
            footerHidden: false,
            viewAnimation: 'ng-fadeInLeftShort',
            theme: this.themes[0],
            currentTheme: 0
        };

    }

    getSetting(name) {
        return name ? this.app[name] : this.app;
    }

    setSetting(name, value) {
        if (typeof this.app[name] !== 'undefined') {
            this.app[name] = value;
        }
    }

    toggleSetting(name) {
        return this.setSetting(name, !this.getSetting(name));
    }

}
