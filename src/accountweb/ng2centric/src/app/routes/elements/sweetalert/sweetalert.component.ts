import { Component, OnInit } from '@angular/core';
var swal = require('sweetalert');

@Component({
    selector: 'app-sweetalert',
    templateUrl: './sweetalert.component.html',
    styleUrls: ['./sweetalert.component.scss']
})
export class SweetalertComponent implements OnInit {

    constructor() { }

    ngOnInit() { }

    swalDemo1() {
        swal('Here\'s a message!');
    }

    swalDemo2() {
        swal('Here\'s a message!', 'It\'s pretty, isn\'t it?');
    }

    swalDemo3() {
        swal('Good job!', 'You clicked the button!', 'success');
    }

    swalDemo4() {
        swal({
            title: 'Are you sure?',
            text: 'You will not be able to recover this imaginary file!',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Yes, delete it!',
            closeOnConfirm: false
        },
            () => {
                swal('Deleted!', 'Your imaginary file has been deleted.', 'success');
            });
    }

    swalDemo5() {
        swal({
            title: 'Are you sure?',
            text: 'You will not be able to recover this imaginary file!',
            type: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'No, cancel plx!',
            closeOnConfirm: false,
            closeOnCancel: false
        }, (isConfirm) => {
            if (isConfirm) {
                swal('Deleted!', 'Your imaginary file has been deleted.', 'success');
            } else {
                swal('Cancelled', 'Your imaginary file is safe :)', 'error');
            }
        });
    }
}
