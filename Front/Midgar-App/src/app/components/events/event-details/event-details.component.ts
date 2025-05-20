import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-event-details',
  templateUrl: './event-details.component.html',
  styleUrls: ['./event-details.component.scss']
})
export class EventDetailsComponent implements OnInit {
  
  form: FormGroup;
  
  get formControl() : any {
    return this.form.controls;
  }
  
  constructor(private fb: FormBuilder) { }
  
  private validation() : void {
    this.form = this.fb.group({
      theme: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      
      local: ['', Validators.required],
      
      eventDate: ['', Validators.required],
      
      peopleCount: ['', [Validators.required, Validators.max(120000)]],
      
      phone: ['', Validators.required],
      
      email: ['', [Validators.required, Validators.email]],
      
      imageURL: ['', Validators.required]
    });
  }

  public resetForm(event: any) : void {
    event.preventDefault();
    this.form.reset();
  }
  
  blockInvalid(event: KeyboardEvent): void {
    if (['e', 'E', '+', '-'].includes(event.key)) {
      event.preventDefault();
    }
  }
  
  sanitizePeopleCount(): void {
    const control = this.form.get('peopleCount');
    const value = Number(control?.value);
    
    if (value < 0 || isNaN(value)) {
      control?.setValue('');
    }
  }
  
  ngOnInit(): void {
    this.validation();
  }
}