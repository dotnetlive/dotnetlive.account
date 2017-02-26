import { Component, OnInit, ViewChild, ElementRef, ViewEncapsulation} from '@angular/core';
declare var $: any;

import { PagetitleService } from '../../../core/pagetitle/pagetitle.service';

@Component({
    selector: 'app-editors',
    templateUrl: './editors.component.html',
    styleUrls: ['./editors.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class EditorsComponent implements OnInit {

    @ViewChild('summernote') summernote: ElementRef;
    @ViewChild('summernoteAir') summernoteAir: ElementRef;

    contents: string;
    contents2: string;

    constructor(pt: PagetitleService) {
        pt.setTitle('Editor');
    }

    ngOnInit() {

        $(this.summernote.nativeElement).summernote({
            height: 280,
            dialogsInBody: true,
            callbacks: {
                onChange: (contents, $editable) => {
                    this.contents = contents;
                }
            }
        });

        $(this.summernoteAir.nativeElement).summernote({
            airMode: true,
            dialogsInBody: true,
            callbacks: {
                onChange: (contents, $editable) => {
                    this.contents2 = contents;
                }
            }
        });
    }

}
