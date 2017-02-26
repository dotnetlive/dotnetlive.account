import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpModule, Http } from '@angular/http';
import { Ng2BootstrapModule } from 'ng2-bootstrap/ng2-bootstrap';
import { TranslateService, TranslateModule, TranslateLoader, TranslateStaticLoader } from 'ng2-translate/ng2-translate';

import CENTRIC_DIRECTIVES from './directives';
import { SettingsService } from './settings/settings.service';
import { SettingsComponent } from './settings/settings.component';

// https://github.com/ocombe/ng2-translate/issues/218
export function createTranslateLoader(http: Http) {
    return new TranslateStaticLoader(http, './assets/i18n', '.json');
}

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        TranslateModule.forRoot({
            provide: TranslateLoader,
            useFactory: (createTranslateLoader),
            deps: [Http]
        }),
        HttpModule,
        Ng2BootstrapModule
    ],
    providers: [
        TranslateService,
        SettingsService
    ],
    declarations: [
        ...CENTRIC_DIRECTIVES,
        SettingsComponent
    ],
    exports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule,
        HttpModule,
        TranslateModule,
        Ng2BootstrapModule,
        ...CENTRIC_DIRECTIVES,
        SettingsComponent
    ]
})
export class SharedModule { }
