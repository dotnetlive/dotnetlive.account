import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, ValidatorFn } from '@angular/forms';
import { CustomValidators } from 'ng2-validation';

@Component({
    selector: 'app-signup',
    templateUrl: './signup.component.html',
    styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {

    regForm: FormGroup;

    constructor(fb: FormBuilder) {

        let loginPassword = new FormControl('', Validators.required);
        let loginPasswordConfirm = new FormControl('', CustomValidators.equalTo(loginPassword));

        this.regForm = fb.group({
            'email': [null, Validators.compose([Validators.required, CustomValidators.email])],
            'password': loginPassword,
            'confirmPassword': loginPasswordConfirm
        });
    }

    submitForm($ev, form: FormGroup) {
        $ev.preventDefault();
        let value = form.value;
        for (let c in form.controls) {
            form.controls[c].markAsTouched();
        }
        if (form.valid) {
            console.log('Valid!');
        }
        console.log(value);
    }

    ngOnInit() {
    }

}
