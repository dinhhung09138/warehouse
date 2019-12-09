import { Component, OnInit } from '@angular/core';
import { SharedResource } from 'src/app/shared/shared.message';

@Component({
  selector: 'app-left-sidebar',
  templateUrl: './left-sidebar.component.html',
  styleUrls: ['./left-sidebar.component.css']
})
export class LeftSidebarComponent implements OnInit {

  leftSidebarText = SharedResource.leftSidebar;

  constructor() { }

  ngOnInit() {
  }

}
