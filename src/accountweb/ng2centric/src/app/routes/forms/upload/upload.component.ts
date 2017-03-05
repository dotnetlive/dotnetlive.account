import { Component, OnInit } from '@angular/core';
import { FileUploader } from 'ng2-file-upload';

import { PagetitleService } from '../../../core/pagetitle/pagetitle.service';

const URL = 'https://evening-anchorage-3159.herokuapp.com/api/';

@Component({
    selector: 'app-upload',
    templateUrl: './upload.component.html',
    styleUrls: ['./upload.component.scss']
})
export class UploadComponent implements OnInit {

    public uploader: FileUploader = new FileUploader({ url: URL });
    public hasBaseDropZoneOver: boolean = false;
    public hasAnotherDropZoneOver: boolean = false;

    public fileOverBase(e: any): void {
        this.hasBaseDropZoneOver = e;
    }

    public fileOverAnother(e: any): void {
        this.hasAnotherDropZoneOver = e;
    }

    constructor(pt: PagetitleService) {
        pt.setTitle('Upload');
    }

    ngOnInit() {
    }

}
