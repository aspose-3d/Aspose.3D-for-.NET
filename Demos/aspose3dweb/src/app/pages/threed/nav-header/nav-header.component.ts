import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav-header',
  templateUrl: './nav-header.component.html',
  styleUrls: ['./nav-header.component.scss']
})
export class NavHeaderComponent implements OnInit {

  public flag: boolean = false;

  constructor() { }

  ngOnInit(): void {
  }

  public showheader() {
    this.flag = !this.flag;
  }
}
