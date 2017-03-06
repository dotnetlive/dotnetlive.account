import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ViewContainerRef } from '@angular/core';

import { ComponentsHelper } from 'ng2-bootstrap/ng2-bootstrap'

import { AppComponent } from './app.component';

import { CoreModule } from './core/core.module';
import { LayoutModule } from './layout/layout.module';
import { SharedModule } from './shared/shared.module';
import { RoutesModule } from './routes/routes.module';

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        BrowserModule,
        CoreModule,
        LayoutModule,
        SharedModule,
        RoutesModule
    ],
    providers: [{ provide: ComponentsHelper, useClass: ComponentsHelper }],
    bootstrap: [AppComponent]
})
export class AppModule {

}
