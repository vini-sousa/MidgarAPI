import { Component, Input, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

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
  @Input() hide = false;
  
  constructor(private router: Router) { 
    router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.hide = event.url.includes('/events/detail');
      }
    });
  }

  ngOnInit() : void{ }

  list(): void {
    this.router.navigate(['/events/list']);
  }
}