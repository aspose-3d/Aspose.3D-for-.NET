import { Component, OnInit, Input, Injectable, Inject, Renderer2 } from '@angular/core';
import { FileService } from 'src/app/services/file.service';
import { ServicesModule, API_CONFIG } from 'src/app/services/services.module';
import { ResourceENService } from 'src/app/services/resource-en.service';
import { DOCUMENT } from '@angular/common';
@Injectable({
  providedIn: ServicesModule
})

@Component({
  selector: 'app-repair',
  templateUrl: './repair.component.html',
  styleUrls: ['./repair.component.scss']
})
export class RepairComponent implements OnInit {

  @Input() lists: any;
  @Input() sessionId: any;

  public listobj: any = [];
  public resultType: any = [];
  public showcheck: boolean;
  public showword: boolean;
  public file3d: any;

  public fileExtensions: string[];
  public saveAsExtension: string = "";
  public value: string;
  public isopen: boolean;

  constructor(
    @Inject(DOCUMENT) private _document: Document,
    private renderer2: Renderer2,
    private fileService: FileService,
    @Inject(API_CONFIG) private uri: string,
    private resourceENService: ResourceENService) { }

  ngOnInit(): void {
    window.onpopstate = function () {
      window.open("/", "_self");
    };
    const s = this.renderer2.createElement('script');
    s.onload = this.rendererLoaded.bind(this);
    s.type = 'text/javascript';
    s.src = '/3d/assets/js/aspose.3d-2.0.js'; // Defines someGlobalObject
    this.renderer2.appendChild(this._document.body, s);

    if (this.lists.length == 0) {
      this.showcheck = false;
      this.showword = true;
    } else {
      this.showcheck = true;
      this.showword = false;
      for (let i = 0; i < this.lists.length; i++) {
        var obj = { "type": "", "ck": "" };
        obj.type = this.lists[i];
        obj.ck = "true";
        this.listobj.push(obj);
      }
      this.objvalue();
    }

    this.fileExtensions = this.resourceENService.get("ThreedRepairingSaveAsExtensions").split(",");
    this.saveAsExtension = "STL";
  }
  rendererLoaded(): void {
    window.aspose3d({
      canvas: "canvas",
      features: ["menu", "selection", "grid"],
      movement: 'orbital',
      ruler: true,
      orientationBox: true,
      centerModel: true,
      url: this.uri + "api/repairing/review/" + this.sessionId
    });

  }

  AssignBtnToText(saveAsExtension: string) {
    this.saveAsExtension = saveAsExtension;
    this.isopen = !this.isopen
  }
  isOpen() {
    this.isopen = !this.isopen
  }

  choosecheck(cc) {
    for (let i = 0; i < this.listobj.length; i++) {
      if (cc == this.listobj[i].type) {
        if (this.listobj[i].ck == "true") {
          this.listobj[i].ck = "false";
        } else {
          this.listobj[i].ck = "true";
        }
      }
    }

  }

  download() {
    for (let i = 0; i < this.listobj.length; i++) {
      if (this.listobj[i].ck == "true") {
        this.resultType.push(this.listobj[i].type);
      }
    }
    for (let i = 0; i < this.resultType.length; i++) {
      switch (this.resultType[i]) {
        case "Scene contains multiple meshes.":
          this.resultType[i] = "1";
          break;
        case "Scene is not centered at the original plane.":
          this.resultType[i] = "2";
          break;
        case "Some parts have no normal data.":
          this.resultType[i] = "3";
          break;
        case "Some parts have holes.":
          this.resultType[i] = "4";
          break;
        case "Some parts have reversed normals.":
          this.resultType[i] = "5";
          break;
        case "Some parts is single-sided and have no thickness.":
          this.resultType[i] = "6";
          break;

      }
    }
    this.value = this.saveAsExtension.toLowerCase();
    this.fileService.download('api/repairing/result/', this.value, this.resultType, this.sessionId);
    this.resultType = [];

  }

  objvalue() {
    for (let i = 0; i < this.listobj.length; i++) {
      switch (this.listobj[i].type) {
        case "SceneIsNotMerged":
          this.listobj[i].type = "Scene contains multiple meshes.";
          break;
        case "SceneIsNotCentered":
          this.listobj[i].type = "Scene is not centered at the original plane.";
          break;
        case "PartHasNoNormal":
          this.listobj[i].type = "Some parts have no normal data.";
          break;
        case "PartHasHoles":
          this.listobj[i].type = "Some parts have holes."
          break;
        case "PartHasReversedNormals":
          this.listobj[i].type = "Some parts have reversed normals.";
          break;
        case "PartHasNoThickness":
          this.listobj[i].type = "Some parts is single-sided and have no thickness.";
          break;
      }
    }
  }

}
