import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'aspose-app';
  public flag: boolean = false;

  public showheader() {
    this.flag = !this.flag;
  }


}
