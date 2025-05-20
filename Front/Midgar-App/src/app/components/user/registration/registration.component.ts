import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from '@app/helpers/ValidatorField';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  
  form!: FormGroup;

  get formControl() : any {
    return this.form.controls;
  }
  
  constructor(private fb: FormBuilder) { }
  
  private validation() : void {

    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('password', 'confirmPassword')
    };

    this.form = this.fb.group({
      firstName: ['', Validators.required],
      
      lastName: ['', Validators.required],
      
      email: ['', [Validators.required, Validators.email]],
      
      userName: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(16)]],
      
      password: ['', [Validators.required, Validators.minLength(6)]],
      
      confirmPassword: ['', Validators.required],

      termsAccepted: [false, Validators.requiredTrue],
    }, formOptions);
  }
  
  ngOnInit(): void {
    this.validation();
  }
  
}