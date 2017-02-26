import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

import { PagetitleService } from '../../../core/pagetitle/pagetitle.service';

@Component({
    selector: 'app-advanced',
    templateUrl: './advanced.component.html',
    styleUrls: ['./advanced.component.scss']
})
export class AdvancedComponent implements OnInit {

    // Color Picker
    colorDemo1 = '#555555';
    colorDemo2 = '#555555';
    colorDemo3 = '#555555';
    colorDemo4 = '#555555';

    // ng2Select
    public items: Array<string> = ['Amsterdam', 'Antwerp', 'Athens', 'Barcelona',
        'Berlin', 'Birmingham', 'Bradford', 'Bremen', 'Brussels', 'Bucharest',
        'Budapest', 'Cologne', 'Copenhagen', 'Dortmund', 'Dresden', 'Dublin',
        'Düsseldorf', 'Essen', 'Frankfurt', 'Genoa', 'Glasgow', 'Gothenburg',
        'Hamburg', 'Hannover', 'Helsinki', 'Kraków', 'Leeds', 'Leipzig', 'Lisbon',
        'London', 'Madrid', 'Manchester', 'Marseille', 'Milan', 'Munich', 'Málaga',
        'Naples', 'Palermo', 'Paris', 'Poznań', 'Prague', 'Riga', 'Rome',
        'Rotterdam', 'Seville', 'Sheffield', 'Sofia', 'Stockholm', 'Stuttgart',
        'The Hague', 'Turin', 'Valencia', 'Vienna', 'Vilnius', 'Warsaw', 'Wrocław',
        'Zagreb', 'Zaragoza', 'Łódź'];

    private value: any = {};
    private _disabledV: string = '0';
    private disabled: boolean = false;

    // timepicker
    public hstep: number = 1;
    public mstep: number = 15;
    public ismeridian: boolean = true;
    public isEnabled: boolean = true;

    public mytime: Date = new Date();
    public options: any = {
        hstep: [1, 2, 3],
        mstep: [1, 5, 10, 15, 25, 30]
    };


    constructor(pt: PagetitleService) {

        pt.setTitle('Advanced');

    }

    //ng2 select
    private get disabledV(): string {
        return this._disabledV;
    }

    private set disabledV(value: string) {
        this._disabledV = value;
        this.disabled = this._disabledV === '1';
    }

    public selected(value: any): void {
        console.log('Selected value is: ', value);
    }

    public removed(value: any): void {
        console.log('Removed value is: ', value);
    }

    public typed(value: any): void {
        console.log('New search input: ', value);
    }

    public refreshValue(value: any): void {
        this.value = value;
    }

    // timepicker
    public toggleMode(): void {
        this.ismeridian = !this.ismeridian;
    }

    public update(): void {
        let d = new Date();
        d.setHours(14);
        d.setMinutes(0);
        this.mytime = d;
    }

    public changed(): void {
        console.log('Time changed to: ' + this.mytime);
    }

    public clear(): void {
        this.mytime = void 0;
    }

    ngOnInit() {
    }

}
