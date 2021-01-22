import { Component, OnInit, ViewChild } from '@angular/core';
import { ResourceENService } from 'src/app/services/resource-en.service';
import { FileService } from 'src/app/services/file.service';
import { ActivatedRoute } from "@angular/router";
import { Title, Meta } from '@angular/platform-browser';
import { Router } from '@angular/router';
@Component({
  selector: 'app-repairing',
  templateUrl: './repairing.component.html',
  styleUrls: ['./repairing.component.scss']
})
export class RepairingComponent implements OnInit {

  public productName = 'Threed';
  public fnName: string = "save";
  public ThreedTitle: string;
  public ThreedTitleSub: string;
  public uploadShow: boolean = true;
  public saveShow: boolean = false;
  public lists: any = [];
  public dback: boolean = false;
  public loading: boolean = false;
  public TypeErrorMsg: string;
  public acceptStr: string;
  public file: any;
  public fileSize: number = 0;
  public files: any;
  public filelist: any = [];
  public fileName: string;
  public state: boolean = false;
  public sessionId: any;
  public Suffix: any;
  public emptyMsg: string;
  public showEmptyMsg: boolean = false;
  public dialogFlag: boolean = true;
  public application: string = "repairing";

  @ViewChild('uploadinput') uploadinput: any;
  @ViewChild('showMsg') showMsg: any;
  @ViewChild('fileupload') fileupload: any;
  @ViewChild('dialogfile') dialogfile: any;

  constructor(private resourceENService: ResourceENService, private fileService: FileService, private routeInfo: ActivatedRoute, private titleService: Title, private meta: Meta, private route: Router) { }

  ngOnInit(): void {
    this.titleService.setTitle('Free online 3D model repair tool');
    this.meta.updateTag({ name: 'description', content: 'Repair your 3D files such as FBX, STL, OBJ, 3DS, GLTF, DRC, RVM, PDF, X, JT, DXF, PLY, 3MF, ASE to FBX, OBJ, 3DS, DRC for printing' });
    this.meta.updateTag({ name: 'keywords', content: 'repair,fix,3d,model,online,free,print,hole,printing,3ds,3mf,amf,a3dw,ase,rvm,pdms,collada,dae,draco,drc,dxf,fbx,gltf,glb,ifc,obj,pdf,ply,prc,jt,jt8,jt9,stp,step,stl,ifc,u3d,vrml,x,zip' });
    var locationUrl = this.route.url;
    var m = locationUrl.match(/repairing\/(\w+)/);
    if (m) {
      this.Suffix = m[1].toUpperCase();
      this.ThreedTitle = this.resourceENService.get("ThreedFreeOnline") + this.Suffix + this.resourceENService.get("ThreedrepairingPrinting");
      this.ThreedTitleSub = this.resourceENService.get("ThreedRepairSource") + this.Suffix + this.resourceENService.get("ThreedFilesPrinting");
      this.acceptStr = this.resourceENService.get(this.productName + this.Suffix + "ValidationExpression");
    } else {
      this.ThreedTitle = this.resourceENService.get("ThreedTitle");
      this.ThreedTitleSub = this.resourceENService.get("ThreedTitleSub");
      this.acceptStr = this.resourceENService.get(this.productName + this.fnName + "ValidationExpression");
    }
  }

  async repairfile() {
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

    this.sessionId = this.fileService.creatUuid();
    try {
      this.loading = true;
      let uploadResult: any = await this.fileService.upload('api/repairing/upload', this.filelist);
      if (uploadResult.data.state == "Success") {
        this.sessionId = uploadResult.data.sessionId;
        this.uploadShow = false;
        this.saveShow = true;
        this.dback = true;
        this.lists = uploadResult.data.types;
      } else {
        this.uploadShow = true;
        this.saveShow = false;
        this.dback = false;
        this.sessionId = uploadResult.data.sessionId;
        this.dialogfile.imgshow();
      }
      this.loading = false;
    } catch (error) {
      this.loading = false;
      this.uploadShow = true;
      this.saveShow = false;
      this.dback = false;
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
}
