import { NgModule, SkipSelf, Optional } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from '../app-routing.module';
// import { HttpClientModule } from '@angular/common/http';
import { ServicesModule } from '../services/services.module';
import { PagesModule } from '../pages/pages.module';
import { ShareModule } from '../share/share.module';

@NgModule({
  declarations: [],
  imports: [
    BrowserModule,
    // HttpClientModule,
    ServicesModule,
    PagesModule,
    ShareModule,
    AppRoutingModule
  ],
  exports: [
    ShareModule,
    AppRoutingModule
  ],
  providers: [],
})
export class CoreModule { 
  constructor(@SkipSelf() @Optional() parentModule: CoreModule){
    if(parentModule){
      throw new Error('CoreModule can only be imported by APPModule')
    }
  }
 }
