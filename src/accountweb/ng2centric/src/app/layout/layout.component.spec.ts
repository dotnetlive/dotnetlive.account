/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { LayoutComponent } from './layout.component';
import { SettingsService } from '../shared/settings/settings.service';

describe('LayoutComponent', () => {
  it('should create', () => {
    let component = new LayoutComponent(new SettingsService());
    expect(component).toBeTruthy();
  });
});
