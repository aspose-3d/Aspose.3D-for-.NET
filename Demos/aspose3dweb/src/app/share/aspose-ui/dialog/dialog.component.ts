import { Component, Input, OnInit } from '@angular/core';
import { FileService } from 'src/app/services/file.service';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss']
})
export class DialogComponent implements OnInit {

  public showFlag: boolean = false;
  public showSuccessFlag: boolean = false;
  public maskFlag: boolean = false;
  public emailAddress: any = "";
  public showLoader: boolean = false;
  public link: string;

  @Input() sessionId: any;
  @Input() application: any;

  constructor(private fileService: FileService) { }

  ngOnInit(): void {
  }

  imgdelete() {
    this.showFlag = false;
    this.maskFlag = false;
    this.emailAddress = "";
  }

  imgSuccessdelete() {
    this.showSuccessFlag = false;
    this.maskFlag = false;
    this.emailAddress = "";
  }

  imgshow() {
    this.showFlag = true;
    this.maskFlag = true;
  }

  async report() {
    console.log(this.application);
    this.showLoader = true;
    let Result: any = await this.fileService.uploadErrorReport(this.emailAddress, this.sessionId, this.application);
    if (Result.data.isSuccess) {
      this.showFlag = false;
      this.showSuccessFlag = true;
      this.link = Result.data.data;
    }
    this.showLoader = false;
  }

}
