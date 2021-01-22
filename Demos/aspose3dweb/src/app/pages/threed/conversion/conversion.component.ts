import { Component, OnInit, ViewChild, Injectable, Inject } from '@angular/core';
import { ResourceENService } from 'src/app/services/resource-en.service';
import { PlatformLocation } from '@angular/common';
import { ServicesModule, API_URL } from '../../../services/services.module';
import { Router } from '@angular/router';
import { Title, Meta } from '@angular/platform-browser';
@Injectable({
  providedIn: ServicesModule
})
@Component({
  selector: 'app-conversion',
  templateUrl: './conversion.component.html',
  styleUrls: ['./conversion.component.scss']
})
export class ConversionComponent implements OnInit {
  public productName = 'Threed';
  public fnName: string = "conversion";
  public threedConversionTitle: string;
  public threedConversionTitleSub: string;
  public AsposeProductTitle: string;
  public productPicUrl: string;
  public LiFeature: string;
  public LiFeature2: string;

  public readmore: boolean;
  public beforeTitle: string;
  public beforeContent: string;
  public afterTitle: string;
  public afterContent: string;
  public beforeSuffixLower: string;
  public afterSuffixLower: string;
  public beforeHref: string;
  public afterHref: string;

  public afterLink: string;
  public listobj: any = [];
  public linkshow: boolean;

  public beforeSuffix: any;
  public afterSuffix: any;
  public xialaValue: any = "FBX";
  public beforeValue: any = "";

  constructor(private resourceENService: ResourceENService, private location: PlatformLocation, private route: Router, private titleService: Title, private meta: Meta) { }

  ngOnInit(): void {
    this.meta.updateTag({ name: 'description', content: 'Convert 3D files such as FBX, STL, OBJ, 3DS, GLTF, DRC, RVM, PDF, X, JT, DXF, PLY, 3MF &amp; ASE to FBX, OBJ, 3DS &amp; DRC formats.' });
    this.meta.updateTag({ name: 'keywords', content: 'convert,3d,model,online,free,3ds,3mf,amf,a3dw,ase,rvm,pdms,collada,dae,draco,drc,dxf,fbx,gltf,glb,ifc,obj,pdf,ply,prc,jt,jt8,jt9,stp,step,stl,ifc,u3d,vrml,x,zip' });
    var locationUrl = this.route.url;
    var m = locationUrl.match(/conversion\/(\w+)-to-(\w+)/);
    if (m) {
      this.beforeSuffix = m[1].toUpperCase();
      this.afterSuffix = m[2].toUpperCase();
      this.beforeValue = this.beforeSuffix;
      this.xialaValue = this.afterSuffix;
      this.threedConversionTitle = this.resourceENService.get("ThreedFreeOnline") + this.beforeSuffix + this.resourceENService.get("ThreedConversionTo") + this.afterSuffix + this.resourceENService.get("ThreedConversionConverter");
      this.threedConversionTitleSub = this.resourceENService.get("ThreedConversionConvert") + this.beforeSuffix + this.resourceENService.get("ThreedConversionTo") + this.afterSuffix + this.resourceENService.get("ThreedConversionModernBrowser");
      this.titleService.setTitle("Free Online " + this.beforeSuffix + " to " + this.afterSuffix + " file format converter");

      this.readmore = true;
      this.beforeSuffixLower = this.beforeSuffix.toLowerCase();
      this.afterSuffixLower = this.afterSuffix.toLowerCase();
      this.beforeTitle = this.resourceENService.get(this.productName + this.beforeSuffix + "Title");
      this.beforeContent = this.resourceENService.get(this.productName + this.beforeSuffix + "Content");
      this.afterTitle = this.resourceENService.get(this.productName + this.afterSuffix + "Title");
      this.afterContent = this.resourceENService.get(this.productName + this.afterSuffix + "Content");
      this.beforeHref = this.resourceENService.get(this.productName + this.beforeSuffix + "Href");
      this.afterHref = this.resourceENService.get(this.productName + this.afterSuffix + "Href");

      this.linkshow = true;
      this.afterLink = this.resourceENService.get(this.productName + this.beforeSuffix + "Link");
      var afterLink = this.afterLink.split(",");
      for (var a in afterLink) {
        var obj = { "type": "", "describe": "", "typeLower": "" };
        obj.type = afterLink[a];
        obj.describe = this.resourceENService.get(this.productName + afterLink[a] + "Describe");
        obj.typeLower = afterLink[a].toLowerCase();
        this.listobj.push(obj);
      }
    } else {
      this.titleService.setTitle("Convert any 3D formats online");
      this.readmore = false;
      this.linkshow = false;
      this.threedConversionTitle = this.resourceENService.get("threedConversionTitle");
      this.threedConversionTitleSub = this.resourceENService.get("threedConversionTitleSub");
    }
    this.AsposeProductTitle = this.resourceENService.get("Aspose" + this.productName) + this.resourceENService.get(this.fnName + "APPName");
    this.LiFeature = this.resourceENService.get(this.productName + this.fnName + "LiFeature");
    this.LiFeature2 = this.resourceENService.get(this.productName + this.fnName + "LiFeature2");
    this.productPicUrl = "/3d/assets/images/aspose-3d-app.png";
  }

  shareApp(link: string) {
    switch (link) {
      case 'facebook':
        var a = document.createElement('a');
        a.href = 'https://www.facebook.com/sharer/sharer.php?u=#https://3d.aspose.app/3d/conversion'
        a.setAttribute('target', '_blank');
        a.click();
        break;
      case 'twitter':
        var a = document.createElement('a');
        a.href = 'https://twitter.com/intent/tweet?text=Convert 3D Online - Free Online 3D Converter&url=https://3d.aspose.app/3d/conversion'
        a.setAttribute('target', '_blank');
        a.click();
        break;
      case 'linkedin':
        var a = document.createElement('a');
        a.href = 'https://www.linkedin.com/sharing/share-offsite/?url=https://3d.aspose.app/3d/conversion'
        a.setAttribute('target', '_blank');
        a.click();
        break;
    }
  }

}
