import { NgModule, Injectable, InjectionToken } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ResourceENService } from './resource-en.service';
import { environment } from 'src/environments/environment';

export const API_CONFIG = new InjectionToken('ApiConfigToken');
export const API_URL = new InjectionToken('ApiUrlToken');
@NgModule({
  declarations: [],
  imports: [
    CommonModule
  ],
  providers: [
    { provide: API_CONFIG, useValue: environment.api }
  ]
})
export class ServicesModule { }
