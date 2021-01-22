import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    CoreModule,
    BrowserModule.withServerTransition({ appId: 'serverApp' })
  ],

  bootstrap: [AppComponent]
})
export class AppModule { }
