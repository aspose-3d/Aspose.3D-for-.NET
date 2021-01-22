import { Component, OnInit } from '@angular/core';
import { ResourceENService } from 'src/app/services/resource-en.service';

@Component({
  selector: 'app-threed',
  templateUrl: './threed.component.html',
  styleUrls: ['./threed.component.scss']
})
export class ThreedComponent implements OnInit {
  public CellsPageMainTitle: string;
  public CellsPageSubHeading: string;
  public AsposeThreed: string;
  public AsposeProductFamilyInclude: string;
  public SearchAPPName: string;
  public Threedsave: string;
  public Threedrepair: string;
  public CellsSearchDesc: string;
  public CellsConversionDesc: string;
  public CellsViewerDesc: string;

  constructor(private resourceENService: ResourceENService) { }

  ngOnInit(): void {
    this.CellsPageMainTitle = this.resourceENService.get("CellsPageMainTitle");
    this.CellsPageSubHeading = this.resourceENService.get("CellsPageSubHeading");
    this.AsposeThreed = this.resourceENService.get("AsposeThreed");
    this.AsposeProductFamilyInclude = this.resourceENService.get("AsposeProductFamilyInclude");
    this.SearchAPPName = this.resourceENService.get("SearchAPPName");
    this.Threedsave = this.resourceENService.get("Threedsave");
    this.Threedrepair = this.resourceENService.get("Threedrepair");
    this.CellsSearchDesc = this.resourceENService.get("CellsSearchDesc");
    this.CellsConversionDesc = this.resourceENService.get("CellsConversionDesc");
    this.CellsViewerDesc = this.resourceENService.get("CellsViewerDesc");
  }

}
