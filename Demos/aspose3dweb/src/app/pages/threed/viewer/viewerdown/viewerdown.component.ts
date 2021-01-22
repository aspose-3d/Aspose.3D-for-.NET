import { Renderer2, Component, OnInit, Input, Inject } from '@angular/core';
import { API_CONFIG } from 'src/app/services/services.module';
import { FileService } from 'src/app/services/file.service';
import { DOCUMENT } from '@angular/common';

@Component({
  selector: 'app-viewerdown',
  templateUrl: './viewerdown.component.html',
  styleUrls: ['./viewerdown.component.scss']
})
export class ViewerdownComponent implements OnInit {

  @Input() fileName: any;
  @Input() sessionId: any;

  constructor(
    @Inject(DOCUMENT) private _document: Document,
    private renderer2: Renderer2,
    @Inject(API_CONFIG) private uri: string,
    private fileService: FileService) { }

  ngOnInit(): void {
    window.onpopstate = function () {
      window.open("/", "_self");
    };
    this.rendererReady();
  }

  async rendererReady() {
    try {
      let uploadResult: any = await this.fileService.judgeA3dw('api/viewer/verify/', this.sessionId);
      if (uploadResult.data.state == "Success") {
        const s = this.renderer2.createElement('script');
        s.onload = this.rendererLoaded.bind(this);
        s.type = 'text/javascript';
        s.src = '/3d/assets/js/aspose.3d-2.0.js';  // Defines someGlobalObject
        this.renderer2.appendChild(this._document.body, s);
      }
    } catch (error) {

    }
  }

  rendererLoaded(): void {
    window.aspose3d({
      canvas: "canvas",
      features: ["menu", "selection", "summary", "grid", "property-grid"],
      movement: 'orbital',
      ruler: true,
      orientationBox: true,
      centerModel: true,
      url: this.uri + "api/viewer/review/" + this.sessionId
    });

  }

  async downloadFile() {
    await this.fileService.downloadconversion('api/viewer/download', this.sessionId);
  }
}
