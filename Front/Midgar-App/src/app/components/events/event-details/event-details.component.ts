import { Component, OnInit } from '@angular/core';
import { AbstractControl, UntypedFormBuilder, UntypedFormGroup, ValidationErrors, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { Events } from '@app/models/Events';
import { EventService } from '@app/services/event.service';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-event-details',
  templateUrl: './event-details.component.html',
  styleUrls: ['./event-details.component.scss']
})
export class EventDetailsComponent implements OnInit {
  
  events = {} as Events;
  form: UntypedFormGroup;
  saveState = 'post';

  title = '';
  subtitle= '';
  
  get formControl() : any {
    return this.form.controls;
  }

  bsConfig(): Partial<BsDatepickerConfig> {
    return {
      dateInputFormat: 'MM/DD/YYYY HH:mm',
      showWeekNumbers: false,
      containerClass: 'theme-default',
      isAnimated: true
    };
  }
  
  constructor(private fb: UntypedFormBuilder, 
              private router: ActivatedRoute, 
              private eventService: EventService, 
              private spinner: NgxSpinnerService, 
              private toastr: ToastrService) { }
  
  
  imageExtensionValidator(control: AbstractControl): ValidationErrors | null {
    const file = control.value;
    if (!file) return null;

    const pattern = /\.(gif|jpe?g|bmp|png)$/i;
    return pattern.test(file) ? null : { invalidImageExtension: true };
  }

  private validation() : void {
    this.form = this.fb.group({
      theme: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      
      local: ['', Validators.required],
      
      eventDate: ['', Validators.required],
      
      peopleCount: ['', [Validators.required, Validators.max(120000)]],
      
      phone: ['', Validators.required],
      
      email: ['', [Validators.required, Validators.email]],
      
      imageURL: ['', [Validators.required, this.imageExtensionValidator]]
    });
  }

  public resetForm(event: any) : void {
    event.preventDefault();
    this.form.reset();
  }
  
  blockInvalid(event: KeyboardEvent) : void {
    if (['e', 'E', '+', '-'].includes(event.key)) {
      event.preventDefault();
    }
  }
  
  sanitizePeopleCount() : void {
    const control = this.form.get('peopleCount');
    const value = Number(control?.value);
    
    if (value < 0 || isNaN(value)) {
      control?.setValue('');
    }
  }

  cssValidator(formField) : any {
    return {'is-invalid': formField.errors && formField.touched }
  }
  
  public getEvent() : void {
    const eventId = this.router.snapshot.paramMap.get('id')

    if (eventId != null) {
      this.spinner.show();

      this.title = `Event: ${eventId}`;
      this.subtitle = 'Here you can update your event information';

      this.saveState = 'put';

      this.eventService.getEventById(+eventId).subscribe(
        (events: Events) => {
          this.events = {... events};
          this.form.patchValue(this.events)
        },
        (error: any) => {
          this.toastr.error('Error trying to load the event');
          console.error(error);
        }
      ).add(() => this.spinner.hide());
    }
  }

  public saveChanges() : void {
    this.spinner.show();

    if(this.form.valid) {

      this.events = (this.saveState === 'post') ? { ... this.form.value} : {id: this.events.id, ... this.form.value}

      this.eventService[this.saveState](this.events).subscribe(
        () => this.toastr.success('The event was successfully saved', 'Success'),
        (error: any) => {
          console.error(error);
          this.toastr.error('Error when trying to save the event', 'Error');
        }
      ).add(() => this.spinner.hide());
    }
  }

  ngOnInit() : void {
    this.title = 'Event';
    this.subtitle = 'Here you could create a new event';
    this.getEvent();
    this.validation();
  }
}