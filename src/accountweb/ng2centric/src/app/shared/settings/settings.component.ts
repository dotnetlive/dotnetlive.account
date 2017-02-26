import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { ModalDirective } from 'ng2-bootstrap';

const screenfull = require('screenfull');
const browser = require('jquery.browser');
declare var $: any;

import { SettingsService } from './settings.service';
import { TranslatorService } from '../../core/translator/translator.service';

@Component({
    selector: 'app-settings',
    templateUrl: './settings.component.html',
    styleUrls: ['./settings.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class SettingsComponent implements OnInit {

    @ViewChild('fsbutton') fsbutton;
    @ViewChild('settingsModal') public settingsModal: ModalDirective;

    constructor(private settings: SettingsService, private translator: TranslatorService) { }

    ngOnInit() { }

    updateTheme(theme) {
        $('body')
            .removeClass((index, css) => (css.match(/(^|\s)theme-\S+/g) || []).join(' '))
            .addClass(this.settings.getSetting('theme'));
    }

    toggleFullScreen(event) {

        if (screenfull.enabled) {
            screenfull.toggle();
        }

    }

}
