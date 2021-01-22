import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AsposeUiModule } from './aspose-ui/aspose-ui.module';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    FormsModule,
    AsposeUiModule
  ],
  exports: [
    CommonModule,
    FormsModule,
    AsposeUiModule
  ]
})
export class ShareModule { }
