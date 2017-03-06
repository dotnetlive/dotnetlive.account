import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl, ValidatorFn } from '@angular/forms';
import { CustomValidators } from 'ng2-validation';

import { PagetitleService } from '../../../core/pagetitle/pagetitle.service';

@Component({
    selector: 'app-validation',
    templateUrl: './validation.component.html',
    styleUrls: ['./validation.component.scss']
})
export class ValidationComponent implements OnInit {

    valForm: FormGroup;
    loginForm: FormGroup;
    blackList = ['bad@email.com', 'some@mail.com', 'wrong@email.co'];

    constructor(fb: FormBuilder, pt: PagetitleService) {

        pt.setTitle('Form Validation');

        let formPassword = new FormControl('', Validators.required);
        let formPasswordConfirm = new FormControl('', CustomValidators.equalTo(formPassword));

        // Model Driven validation
        this.valForm = fb.group({

            'sometext': [null, Validators.required],
            'checkbox': [null, Validators.required],
            'checkagree': [null, Validators.required],
            'radio': ['', Validators.required],
            'select': [null, Validators.required],
            'digits': [null, Validators.pattern('^[0-9]+$')],
            'minlen': [null, Validators.minLength(6)],
            'maxlen': [null, Validators.maxLength(10)],

            'email': [null, CustomValidators.email],
            'url': [null, CustomValidators.url],
            'date': [null, CustomValidators.date],
            'number': [null, Validators.compose([Validators.required, CustomValidators.number])],
            'alphanum': [null, Validators.pattern('^[a-zA-Z]+$')],
            'minvalue': [null, CustomValidators.min(6)],
            'maxvalue': [null, CustomValidators.max(10)],
            'minwords': [null, this.minWords(6)],
            'maxwords': [null, this.maxWords(10)],
            'minmaxlen': [null, CustomValidators.rangeLength([6, 10])],
            'range': [null, CustomValidators.range([10, 20])],
            'rangewords': [null, Validators.compose([this.minWords(6), this.maxWords(10)])],
            'email_bl': [null, this.checkBlackList(this.blackList) ],
            'password': formPassword,
            'confirmPassword': formPasswordConfirm
        });

        ///////

        let loginPassword = new FormControl('', Validators.required);
        let loginPasswordConfirm = new FormControl('', CustomValidators.equalTo(loginPassword));

        this.loginForm = fb.group({
            'email': [null, Validators.compose([Validators.required, CustomValidators.email])],
            'checkagree': [null, Validators.required],
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

    minWords(checkValue): ValidatorFn {
        return <ValidatorFn>((control: FormControl) => {
            return (control.value || '').split(' ').length >= checkValue ? null : { 'minWords': control.value };
        });
    }

    maxWords(checkValue): ValidatorFn {
        return <ValidatorFn>((control: FormControl) => {
            return (control.value || '').split(' ').length <= checkValue ? null : { 'maxWords': control.value };
        });
    }

    checkBlackList(list: Array<string>): ValidatorFn {
        return <ValidatorFn>((control: FormControl) => {
            return list.indexOf(control.value) < 0 ? null : { 'blacklist': control.value };
        });
    }

    ngOnInit() {
    }

}
