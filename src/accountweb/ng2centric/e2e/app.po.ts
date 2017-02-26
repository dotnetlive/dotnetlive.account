import { browser, element, by } from 'protractor';

export class Ng2centricPage {
    navigateTo() {
        return browser.get('/');
    }

    getRootElement() {
        return element.all(by.css('app-root'));
    }
}
