import { Component, OnInit, Inject, Input } from '@angular/core';
import { LocalStorageService } from 'src/app/services/local-storage.service';
import { ServicesModule, API_CONFIG } from 'src/app/services/services.module';
import { FileService } from 'src/app/services/file.service';
import { ActivatedRoute } from '@angular/router'
import { ResourceENService } from 'src/app/services/resource-en.service';
@Component({
    selector: 'app-convert-download',
    templateUrl: './convert-download.component.html',
    styleUrls: ['./convert-download.component.scss']
})
export class ConvertDownloadComponent implements OnInit {
    public emailAddress: any = "";
    public Msg: string = "";
    public issuccess: boolean = false;
    public isdanger: boolean = false;
    public showgif: boolean = false;

    @Input() sessionId: any;

    constructor(private localStorageService: LocalStorageService, @Inject(API_CONFIG) private uri: string, private fileService: FileService, public route: ActivatedRoute, private resourceENService: ResourceENService) { }

    ngOnInit(): void {
    }
    async download() {
        await this.fileService.downloadconversion('api/conversion/download', this.sessionId);
    }

    async send() {
        if (this.emailAddress == "" || this.emailAddress == null) {
            this.Msg = this.resourceENService.get("ThreedConvertMsg1");
            this.isdanger = true;
            this.issuccess = false;
            return;
        }

        var reg = /^\w+((.\w+)|(-\w+))@[A-Za-z0-9]+((.|-)[A-Za-z0-9]+).[A-Za-z0-9]+$/;
        if (!reg.test(this.emailAddress)) {
            this.Msg = this.resourceENService.get("ThreedConvertMsg2");
            this.isdanger = true;
            this.issuccess = false;
        } else {
            this.Msg = "";
            this.isdanger = false;
            this.issuccess = false;
            this.showgif = true
            let result: any = await this.fileService.sendEmail('api/conversion/sendEmail', this.emailAddress, this.sessionId);
            if (result.data.isSuccess) {
                this.Msg = this.resourceENService.get("ThreedConvertMsg3");
                this.issuccess = true;
                this.showgif = false;
            } else {
                this.Msg = this.resourceENService.get("ThreedConvertMsg4");
                this.isdanger = true;
                this.showgif = false;
            }
        }
    }

    enterEvent(e) {
        var keycode = window.event ? e.keyCode : e.which;
        if (keycode == 13) {
            this.send();
        }
    }

}
