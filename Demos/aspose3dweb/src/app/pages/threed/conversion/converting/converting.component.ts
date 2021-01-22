import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { FileService } from 'src/app/services/file.service';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { ResourceENService } from 'src/app/services/resource-en.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-converting',
  templateUrl: './converting.component.html',
  styleUrls: ['./converting.component.scss']
})
export class ConvertingComponent implements OnInit {
  public productName = 'Threed';
  public fnName: string = "conversion";
  public saveAsExtension: string = "";
  public fileExtensions: string[];
  public isopen: boolean;
  public emptyMsg: string;
  public showEmptyMsg: boolean = false;
  public showLoader: boolean;
  public showconversion: boolean = true;
  public showdown: boolean = false;
  public sessionId: string;
  public application: string = "conversion";
  public dialogFlag: boolean = true;

  @ViewChild('uploadfile') uploadfile: any;
  @Input() xialaValue: any;
  @Input() beforeValue: any;
  @ViewChild('dialogfile') dialogfile: any;

  constructor(
    private fileService: FileService,
    private router: Router,
    private localStorageService: LocalStorageService,
    private resourceENService: ResourceENService,
  ) { }

  ngOnInit(): void {
    this.fileExtensions = this.resourceENService.get("ThreedConversionSaveAsExtensions").split(",");
    this.saveAsExtension = this.xialaValue;
    if (this.beforeValue != "") {
      this.fnName = this.beforeValue;
    }
  }

  AssignBtnToText(saveAsExtension: string) {
    this.saveAsExtension = saveAsExtension;
    this.isopen = !this.isopen
  }
  isOpen() {
    this.isopen = !this.isopen
  }
  async convertfile() {
    if (this.uploadfile.filelist.length == 0 || this.uploadfile.filelist == undefined || this.uploadfile.filelist == null) {
      this.emptyMsg = this.resourceENService.get("FileSelectMessage");
      this.showEmptyMsg = true;
      return;
    }
    if (!this.uploadfile.state) {
      return;
    }

    var suffixlist: any = [];
    suffixlist = this.fileService.getFileSuffix(this.uploadfile.filelist);
    if (suffixlist.indexOf("bin") > -1 || suffixlist.indexOf("gltf") > -1) {
      if (suffixlist.indexOf("gltf") == -1) {
        this.emptyMsg = this.resourceENService.get("ThreedEmptyMsgGltf");
        this.showEmptyMsg = true;
        return;
      }
      if (suffixlist.indexOf("bin") == -1) {
        let Jres = this.fileService.judgeFile(this.uploadfile.filelist);
        if (!Jres) {
          this.emptyMsg = this.resourceENService.get("ThreedEmptyMsgDependent");
          this.showEmptyMsg = true;
          return;
        }
      }
    }

    this.showEmptyMsg = false;
    this.showLoader = true;
    //Upload file
    try {
      let uploadResult: any = await this.fileService.upload('api/conversion/files', this.uploadfile.filelist);
      if (uploadResult.data.isSuccess) {
        this.sessionId = uploadResult.data.data;
        //File conversion
        let convertResult: any = await this.fileService.convertFile(this.sessionId, this.saveAsExtension)
        if (convertResult.data.isSuccess) {
          this.showLoader = false;
          this.showconversion = false;
          this.showdown = true;
        } else {
          this.showLoader = false;
          this.sessionId = convertResult.data.sessionId;
          this.dialogfile.imgshow();
        }
      } else {
        this.showLoader = false;
        this.emptyMsg = uploadResult.data.message;
        this.showEmptyMsg = true;
        this.sessionId = uploadResult.data.sessionId;
        this.dialogfile.imgshow();
      }
    } catch (error) {
      this.showLoader = false;
      this.emptyMsg = error;
      this.showEmptyMsg = true;
    }
  }
}
