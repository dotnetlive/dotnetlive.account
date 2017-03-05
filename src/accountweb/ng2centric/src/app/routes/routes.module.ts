import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { TreeModule } from 'angular2-tree-component';
import { FileUploadModule } from 'ng2-file-upload';
import { ColorPickerModule, ColorPickerService } from 'angular2-color-picker/lib';
import { SelectModule } from 'ng2-select';
import { AgmCoreModule } from 'angular2-google-maps/core';
import { DataTableModule } from "angular2-datatable";

import { SharedModule } from '../shared/shared.module';
import { MenuService } from '../core/menu/menu.service';

import * as DASHBOARD from './dashboard';
import * as CARDS from './cards';
import * as ELEMENTS from './elements';
import * as FORMS from './forms';
import * as LAYOUTS from './layouts';
import * as USER from './user';
import * as TABLES from './tables';
import * as PAGES from './pages';
import * as MAPS from './maps';
import * as CHARTS from './charts';

import appMenu from './menu';
import appRoutes from './routes';
import { MessageviewComponent } from './pages/messages/messageview/messageview.component';
import { MessagenewComponent } from './pages/messages/messagenew/messagenew.component';

// used to map object of imports into array so we can use
// the spread operator in the ngModule definition
const arr = obj => Object.keys(obj).map(name => obj[name]);


@NgModule({
    imports: [
        SharedModule,
        RouterModule.forRoot(appRoutes),
        TreeModule,
        FileUploadModule,
        ColorPickerModule,
        SelectModule,
        AgmCoreModule.forRoot({
            apiKey: 'AIzaSyBNs42Rt_CyxAqdbIBK0a5Ut83QiauESPA'
        }),
        DataTableModule
    ],
    declarations: [
        ...arr(DASHBOARD),
        ...arr(CARDS),
        ...arr(ELEMENTS),
        ...arr(CHARTS),
        ...arr(FORMS),
        ...arr(LAYOUTS),
        ...arr(USER),
        ...arr(TABLES),
        ...arr(PAGES),
        ...arr(MAPS),
        MessageviewComponent,
        MessagenewComponent
    ],
    exports: [
        RouterModule,
        TreeModule,
        FileUploadModule,
        ColorPickerModule,
        SelectModule,
        AgmCoreModule,
        DataTableModule,
        ...arr(DASHBOARD),
        ...arr(CARDS),
        ...arr(ELEMENTS),
        ...arr(CHARTS),
        ...arr(FORMS),
        ...arr(LAYOUTS),
        ...arr(USER),
        ...arr(TABLES),
        ...arr(PAGES),
        ...arr(MAPS)
    ]
})
export class RoutesModule {
    constructor(private menu: MenuService) {
        menu.addMenu(appMenu);
    }
}
