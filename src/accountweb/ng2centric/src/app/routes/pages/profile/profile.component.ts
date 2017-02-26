import { Component, OnInit } from '@angular/core';

import { PagetitleService } from '../../../core/pagetitle/pagetitle.service';

@Component({
    selector: 'app-profile',
    templateUrl: './profile.component.html',
    styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

    genders = [
        { value: 0, text: 'Female' },
        { value: 1, text: 'Male' }
    ];

    user = {
        data: {
            area: 'Research & development',
            birthday: '10/11/2000',
            membersince: '05/11/2015',
            gender: 0,
            address: 'Some street, 123',
            email: 'user@mail.com',
            phone: '13-123-46578',
            about: 'Pellentesque porta tincidunt justo, non fringilla erat iaculis in. Sed nisi erat, ornare eu pellentesque quis, pellentesque non nulla. Proin rutrum, est pellentesque commodo mattis, sem justo porttitor odio, id aliquet lacus augue nec nisl.'
        }
    };

    constructor(pt: PagetitleService) {
        pt.setTitle('Profile');
    }

    ngOnInit() {
    }

}
