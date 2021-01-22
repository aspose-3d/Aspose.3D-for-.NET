import { Component, OnInit, ViewChild } from '@angular/core';
import { ResourceENService } from 'src/app/services/resource-en.service';
import { FileService } from 'src/app/services/file.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-measurement',
  templateUrl: './measurement.component.html',
  styleUrls: ['./measurement.component.scss']
})
export class MeasurementComponent implements OnInit {
  public productName = 'Threed';
  public fnName: string = "measurement";
  public threedMeasurementTitle: string;
  public threedMeasurementTitleSub: string;
  public AsposeProductTitle: string;
  public LiFeature: string;
  public LiFeature2: string;
  public productPicUrl: string;
  public flag: boolean = false;
  public firstshow: boolean = true;
  public downshow: boolean = false;
  public acceptStr: string;
  public file: any;
  public fileSize: number = 0;
  public files: any;
  public filelist: any = [];
  public fileName: string;
  public state: boolean = false;
  public TypeErrorMsg: string;
  public showLoader: boolean = false;
  public filePath: any;
  public sessionId: any;
  public emptyMsg: string;
  public showEmptyMsg: boolean = false;
  public Suffix: any;
  public dialogFlag: boolean = true;
  public application: string = "measurement";

  @ViewChild('uploadinput') uploadinput: any;
  @ViewChild('fileupload') fileupload: any;
  @ViewChild('showMsg') showMsg: any;
  @ViewChild('dialogfile') dialogfile: any;

  constructor(private resourceENService: ResourceENService, private fileService: FileService, private route: Router) { }

  ngOnInit(): void {
    var locationUrl = this.route.url;

    var specification = locationUrl.match(/[\?&](session)=([\w\-\.]*)/g);
    if (specification != null) {
      var specificationArry = specification[0].split("=");
      this.sessionId = specificationArry[1];
      this.firstshow = false;
      this.downshow = true;
      return;
    }

    var m = locationUrl.match(/measurement\/(\w+)/);
    if (m) {
      this.Suffix = m[1].toUpperCase();
      this.threedMeasurementTitle = this.resourceENService.get("ThreedFreeOnline") + this.Suffix + this.resourceENService.get("ThreedMeasurementPrinting");
      this.threedMeasurementTitleSub = this.resourceENService.get("ThreedMeasurementViewing") + this.Suffix + this.resourceENService.get("ThreedViewerDevice");
      this.acceptStr = this.resourceENService.get(this.productName + this.Suffix + "ValidationExpression");
    } else {
      this.threedMeasurementTitle = this.resourceENService.get("threedMeasurementTitle");
      this.threedMeasurementTitleSub = this.resourceENService.get("threedMeasurementTitleSub");
      this.acceptStr = this.resourceENService.get(this.productName + this.fnName + "ValidationExpression");
    }
    this.AsposeProductTitle = this.resourceENService.get("Aspose" + this.productName) + this.resourceENService.get(this.fnName + "APPName");
    this.LiFeature = this.resourceENService.get(this.productName + this.fnName + "LiFeature");
    this.LiFeature2 = this.resourceENService.get(this.productName + this.fnName + "LiFeature2");
    this.productPicUrl = "/3d/assets/images/aspose-3d-app.png";
  }

  async measurementfile() {
    if (this.filelist.length == 0 || this.filelist == undefined || this.filelist == null) {
      this.emptyMsg = this.resourceENService.get("FileSelectMessage");
      this.showEmptyMsg = true;
      return;
    }

    if (!this.state) {
      return;
    }

    var suffixlist: any = [];
    suffixlist = this.fileService.getFileSuffix(this.filelist);
    if (suffixlist.indexOf("bin") > -1 || suffixlist.indexOf("gltf") > -1) {
      if (suffixlist.indexOf("gltf") == -1) {
        this.emptyMsg = this.resourceENService.get("ThreedEmptyMsgGltf");
        this.showEmptyMsg = true;
        return;
      }
      if (suffixlist.indexOf("bin") == -1) {
        let Jres = this.fileService.judgeFile(this.filelist);
        if (!Jres) {
          this.emptyMsg = this.resourceENService.get("ThreedEmptyMsgDependent");
          this.showEmptyMsg = true;
          return;
        }
      }
    }

    try {
      this.showLoader = true;
      let uploadResult: any = await this.fileService.upload('api/measurement/upload', this.filelist);
      if (uploadResult.data.state == "Success") {
        this.sessionId = uploadResult.data.sessionId;
        this.route.navigate(['measurement'], { queryParams: { session: this.sessionId } });
        this.firstshow = false;
        this.downshow = true;
      } else {
        this.sessionId = uploadResult.data.sessionId;
        this.dialogfile.imgshow();
      }
      this.showLoader = false;
    } catch (error) {
      this.showLoader = false;
      this.emptyMsg = this.resourceENService.get("ThreedEmptyMsgCatch");
      this.showEmptyMsg = true;
    }
  }

  hideError() {
    this.showMsg.nativeElement.style.display = 'none';
    this.showEmptyMsg = false;
    this.state = true;
  }
  upload(): void {
    this.TypeErrorMsg = this.resourceENService.get("InvalidFileExtension") + this.acceptStr.replace(/\./g, " ").toUpperCase();
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
    if (!this.acceptStr.includes(str)) {
      this.state = false;
      this.showMsg.nativeElement.style.display = 'block';
    } else {
      if (str == ".obj") {
        this.showMsg.nativeElement.style.display = 'block';
        this.TypeErrorMsg = this.resourceENService.get("ThreedUploadObj");
      } else {
        this.showMsg.nativeElement.style.display = 'none';
      }
      this.state = true;
    }
    this.fileupload.nativeElement.style.display = 'inline-block';
    this.showEmptyMsg = false;
  }

  public removefile(count: any) {
    this.hideError();
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
      this.emptyMsg = this.resourceENService.get("ThreedUploadOnlySupport");
      this.showEmptyMsg = true;
      this.state = false;
      return false;
    } else {
      return true;
    }
  }
  shareApp(link: string) {
    switch (link) {
      case 'facebook':
        var a = document.createElement('a');
        a.href = 'https://www.facebook.com/sharer/sharer.php?u=#https://3d.aspose.app/3d/viewer'
        a.setAttribute('target', '_blank');
        a.click();
        break;
      case 'twitter':
        var a = document.createElement('a');
        a.href = 'https://twitter.com/intent/tweet?text=Viewer 3D Online - Free Online 3D Viewer &url=https://3d.aspose.app/3d/viewer'
        a.setAttribute('target', '_blank');
        a.click();
        break;
      case 'linkedin':
        var a = document.createElement('a');
        a.href = 'https://www.linkedin.com/sharing/share-offsite/?url=https://3d.aspose.app/3d/viewer'
        a.setAttribute('target', '_blank');
        a.click();
        break;
    }
  }
}
