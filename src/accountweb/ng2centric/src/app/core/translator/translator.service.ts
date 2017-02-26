import { Injectable } from '@angular/core';
import { TranslateService } from 'ng2-translate/ng2-translate';

@Injectable()
export class TranslatorService {

    defaultLanguage: string = 'en';
    availablelangs: any;
    currentLang: string = this.defaultLanguage;

    constructor(private translate: TranslateService) {
        // this language will be used as a fallback when a translation isn't found in the current language
        translate.setDefaultLang(this.defaultLanguage);

        this.availablelangs = [
            { code: 'en', text: 'English' },
            { code: 'es_AR', text: 'Spanish' }
        ];

        this.useLanguage();

    }

    useLanguage(lang: string = this.defaultLanguage) {
        this.translate.use(lang);
        this.currentLang = lang;
    }

    getAvailableLanguages() {
        return this.availablelangs;
    }

    getCurrentLang() {
        for(let i in this.availablelangs) {
            if(this.availablelangs[i].code === this.currentLang) {
                return this.availablelangs[i].text;
            }
        }
    }

}
