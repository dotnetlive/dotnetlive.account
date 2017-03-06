import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Pipe, PipeTransform } from '@angular/core';
import * as _ from "lodash";

import { PagetitleService } from '../../../core/pagetitle/pagetitle.service';

@Pipe({
    name: 'dataFilter'
})
export class DataFilterPipe implements PipeTransform {

    transform(array: any[], query: string): any {
        if (query) {
            return _.filter(array, row => row.name.indexOf(query) > -1);
        }
        return array;
    }
}

@Component({
    selector: 'app-datatables',
    templateUrl: './datatables.component.html',
    styleUrls: ['./datatables.component.scss']
})
export class DatatablesComponent implements OnInit {

    public data = [];
    public filterQuery = '';
    public rowsOnPage = 10;
    public sortBy = 'email';
    public sortOrder = 'asc';

    constructor(pt: PagetitleService, private http: Http) {
        pt.setTitle('Data Table');
    }

    ngOnInit(): void {
        this.http.get('assets/datatable.json').subscribe((data) => this.data = data.json());
    }

    public toInt(num: string) {
        return +num;
    }

    public sortByWordLength = (a: any) => {
        return a.city.length;
    }

    remove(item) {
        var index = this.data.indexOf(item);
        this.data.splice(index, 1);
    }

}
