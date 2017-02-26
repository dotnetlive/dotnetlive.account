import { Ng2centricPage } from './app.po';
import { browser } from 'protractor';

describe('ng2centric App', function() {
    let page: Ng2centricPage;

    browser.ignoreSynchronization = true;

    beforeEach(() => {
        page = new Ng2centricPage();
    });

    it('should perform a basic test', () => {
        page.navigateTo();
        let root = page.getRootElement();
        expect(root.count()).toEqual(1);
    });
});
