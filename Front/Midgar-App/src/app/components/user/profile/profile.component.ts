import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ValidatorField } from '@app/helpers/ValidatorField';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  form!: UntypedFormGroup;

  get formControl() : any {
    return this.form.controls;
  }
    
  constructor(private fb: UntypedFormBuilder) { }

  private validation() : void {

    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('password', 'confirmPassword')
    };

    this.form = this.fb.group({
      title: ['', Validators.required],

      firstName: ['', Validators.required],
      
      lastName: ['', Validators.required],
      
      email: ['', [Validators.required, Validators.email]],

      phone: ['', Validators.required],

      role: ['', Validators.required],
      
      description: ['', Validators.required],
            
      password: ['', [Validators.required, Validators.minLength(6)]],
      
      confirmPassword: ['', Validators.required],
    }, formOptions);
  }

  public resetForm(event: any) : void {
    event.preventDefault();
    this.form.reset();
  }

  ngOnInit() {
    this.validation();
  }

}