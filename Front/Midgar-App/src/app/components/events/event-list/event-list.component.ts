import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Events } from '@app/models/Events';
import { EventService } from '@app/services/event.service';
import { environment } from '@environments/environment';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
  styleUrls: ['./event-list.component.scss']
})
export class EventListComponent implements OnInit {

  modalRef?: BsModalRef;
  isCollapsed = false;

  public events: Events[] = [];
  public eventsFiltered: Events[] = [];
  public eventId: number;

  public widthImg = 150;
  public marginImg = 2;
  private listedFilter = '';

  public get filter() {
    return this.listedFilter;
  }

  public set filter(value: string) {
    this.listedFilter = value;
    this.eventsFiltered = this.filter ? this.filterEvents(this.filter) : this.events
  }

  public filterEvents(filterFor: string): Events[] {
    const filter = this.filter.toLowerCase();
  
    return this.events.filter(event => {
      const lotesAsText = event.lotes.map(l => l.name).join(' ').toLowerCase();
      return (
        event.theme.toLowerCase().includes(filter) ||
        event.local.toLowerCase().includes(filter) ||
        lotesAsText.includes(filter)
      );
    });
  }

  constructor(
    private eventService: EventService, 
    private modalService: BsModalService, 
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

    public getEvents(): void {

    this.eventService.getEvents().subscribe(
      (events: Events[]) => {
        this.events = events;
        this.eventsFiltered = events;
      },
      (error: any) => {
        console.error(error);
        this.toastr.error('Error loading events.', 'Error!');
      }
    ).add(() => this.spinner.hide());
  }

  openModal(confirmModal: TemplateRef<void>, eventId: number) {
    this.eventId = eventId;
    this.modalRef = this.modalService.show(confirmModal, { class: 'modal-sm' });
  }
 
  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.eventService.deleteEvent(this.eventId).subscribe(
      (result: any) => {
        if (result.message === "Deleted") {
          this.toastr.success('The event was successfully deleted.', 'Deleted!');
          this.getEvents();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Error when trying to delete the event ${this.eventId}`, 'Error');
      }
    ).add(() => this.spinner.hide());
  }
 
  decline(): void {
    this.modalRef?.hide();
  }

  detailEvent(id: number) : void {
    this.router.navigate([`events/detail/${id}`]);
  }

  public showImage(imageURL: string): string {
    return (imageURL != '') ? `${environment.apiURL}resources/images/${imageURL}` : 'assets/noimage.jpg';
  }

  public ngOnInit(): void {
    this.spinner.show();
    this.getEvents();
  }
}