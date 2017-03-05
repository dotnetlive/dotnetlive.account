import { NgModule, Optional, SkipSelf } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { MenuService } from './menu/menu.service';
import { PagetitleService } from './pagetitle/pagetitle.service';
import { TranslatorService } from './translator/translator.service';
import { ColorsService } from './colors/colors.service';

import { throwIfAlreadyLoaded } from './module-import-guard';

@NgModule({
    imports: [
        SharedModule
    ],
    providers: [
        MenuService,
        PagetitleService,
        TranslatorService,
        ColorsService
    ]
})
export class CoreModule {
    constructor( @Optional() @SkipSelf() parentModule: CoreModule) {
        throwIfAlreadyLoaded(parentModule, 'CoreModule');
    }
}
