import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { UploadFileComponent } from './upload-file/upload-file.component';
import { AppExplicationComponent } from './app-explication/app-explication.component';
import { DialogComponent } from './dialog/dialog.component';



@NgModule({
  declarations: [UploadFileComponent, AppExplicationComponent, DialogComponent],
  imports: [
    BrowserModule,
    FormsModule
  ],
  exports: [
    UploadFileComponent,
    AppExplicationComponent,
    DialogComponent
  ]
})
export class AsposeUiModule { }
