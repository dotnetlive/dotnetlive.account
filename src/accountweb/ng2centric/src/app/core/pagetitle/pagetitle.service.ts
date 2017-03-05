import { Injectable } from '@angular/core';

@Injectable()
export class PagetitleService {

    private page = {
        title: ''
    };

    constructor() {}

    setTitle(newTitle: string) {
        this.page.title = newTitle;
    }

    getTitle(): string {
        return this.page.title;
    }

}
