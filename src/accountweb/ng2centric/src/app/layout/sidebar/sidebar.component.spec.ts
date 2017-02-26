/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement, Injector } from '@angular/core';
import { RouterModule, Router } from '@angular/router';

import { MenuService } from '../../core/menu/menu.service';
import { SidebarComponent } from './sidebar.component';
import { SettingsService } from '../../shared/settings/settings.service';

describe('SidebarComponent', () => {
    let mockRouter = {
        navigate: jasmine.createSpy('navigate')
    };
    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [
                MenuService,
                SettingsService,
                { provide: Router, useValue: mockRouter }
            ]
        }).compileComponents();
    });

    it('should create an instance', async(inject([MenuService, SettingsService, Injector], (menuService, settingsService, injector) => {
        let component = new SidebarComponent(menuService, settingsService, injector);
        expect(component).toBeTruthy();
    })));
});
