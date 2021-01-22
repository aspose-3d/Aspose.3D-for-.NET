import { Component, OnInit, Input } from '@angular/core';
import { ResourceENService } from 'src/app/services/resource-en.service';
@Component({
  selector: 'app-app-explication',
  templateUrl: './app-explication.component.html',
  styleUrls: ['./app-explication.component.scss']
})
export class AppExplicationComponent implements OnInit {
  @Input() productName: string;
  @Input() fnName: string;

  public AsposeProductTitle: string;
  public SupportedDocuments: string;
  public LiFeature: string;
  public LiFeature2: string;
  public LiFeature3: string;
  public LiFeature4: string;
  public productPicUrl: string;
  public Feature1: string;
  public Feature1Description: string;
  public Feature2: string;
  public Feature2Description: string;
  public Feature3: string;
  public PoweredBy: string;
  public QualityDescMetadata: string;
  public AsposeProduct: string;
  constructor(private resourceENService: ResourceENService) { }

  ngOnInit(): void {
    this.AsposeProductTitle = this.resourceENService.get("Aspose" + this.productName) + this.resourceENService.get(this.fnName + "APPName");
    this.SupportedDocuments = this.resourceENService.get("Supported") + " " + this.resourceENService.get("Documents") + ": " + this.resourceENService.get(this.productName + this.fnName + "ValidationExpression").replace(/\./g, " ").toUpperCase();
    this.LiFeature = this.resourceENService.get(this.productName + this.fnName + "LiFeature");
    this.LiFeature2 = this.resourceENService.get(this.productName + this.fnName + "LiFeature2");
    this.LiFeature3 = this.resourceENService.get(this.productName + this.fnName + "LiFeature3");
    this.LiFeature4 = this.resourceENService.get(this.productName + this.fnName + "LiFeature4");
    this.productPicUrl = "/3d/assets/images/aspose-3d-app.png";
    this.Feature1 = this.resourceENService.get(this.fnName + "Feature1");
    this.Feature1Description = this.resourceENService.get(this.fnName + "Feature1Description");
    this.Feature2 = this.resourceENService.get(this.fnName + "Feature2");
    this.Feature2Description = this.resourceENService.get(this.fnName + "Feature2Description");
    this.Feature3 = this.resourceENService.get(this.fnName + "Feature3");
    this.PoweredBy = this.resourceENService.get("PoweredBy");
    this.QualityDescMetadata = this.resourceENService.get("QualityDescMetadata");
    this.AsposeProduct = this.resourceENService.get("Aspose" + this.productName) + "."
  }

  shareApp(link: string) {
    switch (link) {
      case 'facebook':
        var a = document.createElement('a');
        a.href = 'https://www.facebook.com/sharer/sharer.php?u=#https://3d.aspose.app/3d/repairing'
        a.setAttribute('target', '_blank');
        a.click();
        break;
      case 'twitter':
        var a = document.createElement('a');
        a.href = 'https://twitter.com/intent/tweet?text=Repair 3D Online - Free Online 3D Repair &url=https://3d.aspose.app/3d/repairing'
        a.setAttribute('target', '_blank');
        a.click();
        break;
      case 'linkedin':
        var a = document.createElement('a');
        a.href = 'https://www.linkedin.com/sharing/share-offsite/?url=https://3d.aspose.app/3d/repairing'
        a.setAttribute('target', '_blank');
        a.click();
        break;
    }
  }
}
