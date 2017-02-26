import { Injectable } from '@angular/core';

@Injectable()
export class MenuService {

    menuItems: Array<any>;

    constructor() {
        this.menuItems = [];
    }

    addMenu(items: Array<{
        name: string,
        link?: string,
        href?: string,
        imgpath?: string,
        order?: number,
        iconclass?: string,
        label?: any,
        subitems?: Array<any>
    }>) {
        items.forEach((item) => {
            this.menuItems.push(item);
        });
    }

    getMenu() {
        return this.menuItems;
    }

    getMenuSorted() {
        return this.menuItems.sort((a, b) => {
            return a.order - b.order;
        });
    }

}
