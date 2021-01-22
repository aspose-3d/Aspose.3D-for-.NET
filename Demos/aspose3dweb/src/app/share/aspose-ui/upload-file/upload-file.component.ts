import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { ResourceENService } from 'src/app/services/resource-en.service';
import { FileService } from 'src/app/services/file.service';
@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.scss']
})
export class UploadFileComponent implements OnInit {
  @Input() productName: string;
  @Input() fnName: string;
  @Input() parent: any;

  public file: any;
  public fileSize: number = 0;
  public files: any;
  public filelist: any = [];
  public fileName: string;
  public state: boolean = false;
  public TypeErrorMsg: string;
  public acceptStr: string;
  public tipsMsg: string;
  @ViewChild('uploadinput') uploadinput: any;
  @ViewChild('fileupload') fileupload: any;
  @ViewChild('showMsg') showMsg: any;
  @ViewChild('tips') tips: any;

  constructor(private resourceENService: ResourceENService, private fileService: FileService) { }

  ngOnInit(): void {
    this.acceptStr = this.resourceENService.get(this.productName + this.fnName + "ValidationExpression") || "";
    this.TypeErrorMsg = this.resourceENService.get("InvalidFileExtension") + this.acceptStr.replace(/\./g, " ").toUpperCase();
  }
  ngAfterViewInit(): void {
    this.fileupload.nativeElement.style.display = 'none';
  }
  showError(msg: string) {
    this.tips.nativeElement.style.display = 'block';
    this.showMsg.nativeElement.style.display = 'none';
    this.tipsMsg = msg;
    this.state = false;
  }
  hideError() {
    this.tips.nativeElement.style.display = 'none';
    this.showMsg.nativeElement.style.display = 'none';
    this.state = true;
    this.parent.showEmptyMsg = false;
  }
  upload(): void {
    this.file = this.uploadinput.nativeElement.files[0];
    this.files = this.uploadinput.nativeElement.files;
    this.fileService.judgeFile(this.files);
    for (let i = 0; i < this.files.length; i++) {
      this.filelist.push(this.files[i]);
      this.fileSize += this.files[i].size;
    }
    if (!this.fileSizeLimit()) {
      this.state = false;
      return;
    }
    this.fileName = this.file.name;
    let str = this.fileName.substring(this.fileName.lastIndexOf(".")).toLowerCase();
    if (this.acceptStr == "") {
      if (str == ".obj") {
        this.showError(this.resourceENService.get("ThreedUploadObj"));
        this.state = true;
      } else {
        this.hideError();
      }

    } else {
      if (!this.acceptStr.includes(str)) {
        this.tips.nativeElement.style.display = 'none';
        this.showMsg.nativeElement.style.display = 'block';
        this.state = false;
      } else {
        this.hideError();
      }
    }
    this.fileupload.nativeElement.style.display = 'inline-block';
  }
  public removefile(count: any) {
    let element = document.getElementById(count);
    element.style.display = 'none';
    let file0 = this.filelist[count];
    this.fileSize = this.fileSize - file0.size;
    this.fileSizeLimit();
    this.filelist.splice(count, 1);
    this.uploadinput.nativeElement.style.display = 'block';
  }

  fileSizeLimit() {
    var maximumSize = 100 * 1024 * 1024;//Maximum upload size is 100MB
    if (this.fileSize > maximumSize) {
      this.fileupload.nativeElement.style.display = 'inline-block';
      this.uploadinput.nativeElement.style.display = 'block';
      this.showError(this.resourceENService.get("ThreedUploadOnlySupport"));
      return false;
    } else {
      return true;
    }
  }
}
