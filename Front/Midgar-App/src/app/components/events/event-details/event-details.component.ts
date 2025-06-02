import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormArray, FormControl, FormGroup, UntypedFormBuilder, UntypedFormGroup, ValidationErrors, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Events } from '@app/models/Events';
import { Lote } from '@app/models/Lote';
import { EventService } from '@app/services/event.service';
import { LoteService } from '@app/services/lote.service';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-event-details',
  templateUrl: './event-details.component.html',
  styleUrls: ['./event-details.component.scss']
})
export class EventDetailsComponent implements OnInit {
  
  modalRef: BsModalRef;
  eventId: number;
  events = {} as Events;
  form: UntypedFormGroup;
  saveState = 'post';
  currentLote = {id: 0, name: '', index: 0};

  title = '';
  subtitle= '';
  
  get editMode(): boolean {
    return this.saveState === 'put';
  }

  get lotes(): FormArray {
    return this.form.get('lotes') as FormArray;
  }

  get formControl() : any {
    return this.form.controls;
  }

  bsConfig(): any {
    return {
      dateInputFormat: 'MM/DD/YYYY HH:mm',
      adaptivePosition: true,
      showWeekNumbers: false,
      containerClass: 'theme-default',
      isAnimated: true,
      withTimepicker: true
    };
  }

  bsConfigLote(): Partial<BsDatepickerConfig> {
    return {
      dateInputFormat: 'MM/DD/YYYY',
      adaptivePosition: true,
      showWeekNumbers: false,
      containerClass: 'theme-default',
      isAnimated: true
    };
  }
  
  constructor(private fb: UntypedFormBuilder, 
              private activatedRoute: ActivatedRoute, 
              private eventService: EventService, 
              private spinner: NgxSpinnerService, 
              private toastr: ToastrService,
              private router: Router,
              private loteService: LoteService,
              private modalService: BsModalService) { }
  
  
  imageExtensionValidator(control: AbstractControl): ValidationErrors | null {
    const file = control.value;
    if (!file) return null;

    const pattern = /\.(gif|jpe?g|bmp|png)$/i;
    return pattern.test(file) ? null : { invalidImageExtension: true };
  }

   private parseCustomFormatDateString(dateInput: string | Date | null | undefined): Date | null {
    if (dateInput instanceof Date)
      return dateInput;

    if (!dateInput || typeof dateInput !== 'string')
      return null;

    let parts = dateInput.match(/^(\d{2})\/(\d{2})\/(\d{4})(?: (\d{2}):(\d{2})(?::(\d{2}))?)?$/);
    let dayIndex = 1, monthIndex = 2;

    if (parts) {
      const year = parseInt(parts[3], 10);
      const month = parseInt(parts[monthIndex], 10) - 1;
      const day = parseInt(parts[dayIndex], 10);

      const hours = parts[4] ? parseInt(parts[4], 10) : 0;
      const minutes = parts[5] ? parseInt(parts[5], 10) : 0;
      const seconds = parts[6] ? parseInt(parts[6], 10) : 0;

      const parsedDate = new Date(year, month, day, hours, minutes, seconds);

      if (parsedDate.getFullYear() === year && parsedDate.getMonth() === month && parsedDate.getDate() === day)
          return parsedDate;

      console.warn('The date string resulted in an invalid date:', dateInput);
      return null;
    }

    const fallbackDate = new Date(dateInput);
    if (!isNaN(fallbackDate.getTime()))
      return fallbackDate;

    console.warn('The date string could not be parsed:', dateInput);
    return null;
  }

  private validation() : void {
    this.form = this.fb.group({
      theme: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      
      local: ['', Validators.required],
      
      eventDate: ['', Validators.required],
      
      peopleCount: ['', [Validators.required, Validators.max(120000)]],
      
      phone: ['', Validators.required],
      
      email: ['', [Validators.required, Validators.email]],
      
      imageURL: ['', [Validators.required, this.imageExtensionValidator]],

      lotes: this.fb.array([])
    });
  }

  addLote(): void {
    this.lotes.push(this.createLote({id: 0} as Lote));
  }

  createLote(lote: Lote): FormGroup {
    return this.fb.group({
        id: [lote.id],
        name: [lote.name, Validators.required],
        quantity: [lote.quantity, Validators.required],
        price: [lote.price, Validators.required],
        initialDate: [this.parseCustomFormatDateString(lote.initialDate)],
        finalDate: [this.parseCustomFormatDateString(lote.finalDate)]
    });
  }

  public changeDateValue(value: Date, index: number, field: string): void {
    const lotesArray = this.form.get('lotes') as FormArray;
    const loteControl = lotesArray.at(index);
    
    if (loteControl)
      loteControl.get(field)?.setValue(value, { emitEvent: true });
  }

  public returnLoteTitle(name: string): string {
    return name === null || name === '' ? 'ㅤ' : name;
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

  cssValidator(formField: FormControl | AbstractControl) : any {
    return {'is-invalid': formField.errors && formField.touched }
  }
  
  public loadEvent() : void {
    this.eventId = +this.activatedRoute.snapshot.paramMap.get('id')

    if (this.eventId != null && this.eventId != 0) {
      this.spinner.show();

      this.title = `Event: ${this.eventId}`;
      this.subtitle = 'Here you can update your event information';

      this.saveState = 'put';

      this.eventService.getEventById(this.eventId).subscribe(
        (events: Events) => {
          this.events = {... events};
          this.form.patchValue(events);
          this.loadLotes();
        },
        (error: any) => {
          this.toastr.error('Error trying to load the event');
          console.error(error);
        }
      ).add(() => this.spinner.hide());
    }
  }

  public loadLotes() : void {

    if (!this.eventId && this.eventId !== 0) 
      return;

    this.loteService.getLotesByEventId(this.eventId!).subscribe(
      (returnLotes: Lote[]) => {
        returnLotes.forEach((lote) => {
          this.lotes.push(this.createLote(lote));
        });
      },
      (error: any) => {
        this.toastr.error('Error trying to load the lote', 'Error');
        console.error(error);
      }
    ).add(() => this.spinner.hide());
  }

  public saveEvent() : void {
    this.spinner.show();

    if(this.form.valid) {

      this.events = (this.saveState === 'post') ? { ... this.form.value} : {id: this.events.id, ... this.form.value}

      this.eventService[this.saveState](this.events).subscribe(
        (returnEvent: Events) => {
          this.toastr.success('The event was successfully saved', 'Success');
          this.router.navigate([`events/detail/${returnEvent.id}`]);
        },
        (error: any) => {
          this.toastr.error('Error when trying to save the event', 'Error');
          console.error(error);
        }
      ).add(() => this.spinner.hide());
    }
  }

  public saveLote(): void {
    if (this.form.controls.lotes.valid) {
      this.spinner.show();
      this.loteService.saveLote(this.eventId, this.form.value.lotes)
        .subscribe(
          () => {
            this.toastr.success('The lote(s) was successfully saved', 'Success');
          },
          (error: any) => {
            this.toastr.error('Error when trying to save the lote(s)', 'Error');
            console.error(error);
          },
        ).add(() => this.spinner.hide());
    }
  }

  public deleteLote(template: TemplateRef<any>, index: number): void {
    this.currentLote.id = this.lotes.get(index + '.id').value;
    this.currentLote.name = this.lotes.get(index + '.name').value;
    this.currentLote.index = index;

    this.modalRef = this.modalService.show(template, {class:'modal-sm'});
  }

  public confirmDeleteLote(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.loteService.deleteLote(this.eventId, this.currentLote.id).subscribe(
      () => {
        this.toastr.success('The lote was successfully deleted', 'Success');
        this.lotes.removeAt(this.currentLote.index);
      },
      (error: any) => {
        this.toastr.error(`Error when trying to delete the lote ${this.currentLote.id}`, 'Error');
        console.error(error);
      }
    ).add(() => this.spinner.hide());
  }

  public declineDeleteLote(): void {
    this.modalRef?.hide();
  }

  ngOnInit() : void {
    this.title = 'Event';
    this.subtitle = 'Here you could create a new event';
    this.loadEvent();
    this.validation();
  }
}