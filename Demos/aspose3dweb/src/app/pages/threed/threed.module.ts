import { NgModule, InjectionToken } from '@angular/core';

import { ThreedRoutingModule } from './threed-routing.module';
import { ShareModule } from 'src/app/share/share.module';
import { ThreedComponent } from './threed.component';
import { RepairingComponent } from './repairing/repairing.component';
import { RepairComponent } from './repairing/repair/repair.component';
import { ConversionComponent } from './conversion/conversion.component';
import { ConvertingComponent } from './conversion/converting/converting.component';
import { ConvertDownloadComponent } from './conversion/convert-download/convert-download.component';
import { ErrorpageComponent } from './errorpage/errorpage.component';
import { ViewerComponent } from './viewer/viewer.component';
import { ViewerdownComponent } from './viewer/viewerdown/viewerdown.component';
import { NavHeaderComponent } from './nav-header/nav-header.component';
import { NavFooterComponent } from './nav-footer/nav-footer.component';
import { MeasurementComponent } from './measurement/measurement.component';
import { MeasurementdownComponent } from './measurement/measurementdown/measurementdown.component';

export const FILE_CONFIG = new InjectionToken('FileConfigToken');
@NgModule({
  declarations: [ThreedComponent, RepairingComponent, RepairComponent, ConversionComponent, ConvertingComponent, ConvertDownloadComponent, ErrorpageComponent, ViewerComponent, ViewerdownComponent, NavHeaderComponent, NavFooterComponent, MeasurementComponent, MeasurementdownComponent],
  imports: [
    ShareModule,
    ThreedRoutingModule
  ]
})
export class ThreedModule { }
