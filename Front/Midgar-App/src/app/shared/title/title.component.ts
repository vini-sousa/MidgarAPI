import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-title',
  templateUrl: './title.component.html',
  styleUrls: ['./title.component.scss']
})
export class TitleComponent implements OnInit {

  @Input() title = '';
  @Input() subtitle = 'Since 2025';
  @Input() iconClass = 'fa fa-user';
  @Input() buttonList = false;

  constructor(private router: Router) { }

  ngOnInit() : void{ }

  list() : void {
    this.router.navigate([`/${this.title.toLowerCase()}/list`]);
  }
}