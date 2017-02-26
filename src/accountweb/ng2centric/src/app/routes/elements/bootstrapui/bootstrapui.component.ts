import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';

@Component({
    selector: 'app-bootstrapui',
    templateUrl: './bootstrapui.component.html',
    styleUrls: ['./bootstrapui.component.scss']
})
export class BootstrapuiComponent implements OnInit {

    // ALERTs
    public alerts: Array<Object> = [
        {
            type: 'danger',
            msg: 'Oh snap! Change a few things up and try submitting again.'
        },
        {
            type: 'success',
            msg: 'Well done! You successfully read this important alert message.',
            closable: true
        }
    ];
    // PROGRESSBAR
    public max: number = 200;
    public showWarning: boolean;
    public dynamic: number;
    public type: string;
    public stacked: any[] = [];

    // TOOLTIPS
    public dynamicTooltip: string = 'Hello, World!';
    public dynamicTooltipText: string = 'dynamic';
    public htmlTooltip: string = 'I\'ve been made <b>bold</b>!';
    public tooltipModel: any = { text: 'foo', index: 1 };

    // RATINGS
    public x: number = 5;
    public y: number = 2;
    public maxRat: number = 10;
    public rate: number = 7;
    public isReadonly: boolean = false;
    public overStar: number;
    public percent: number;
    public ratingStates: any = [{
        stateOn: 'icon-2x ion-android-radio-button-on',
        stateOff: 'icon-2x ion-android-radio-button-off'
    }, {
        stateOn: 'icon-2x ion-android-star',
        stateOff: 'icon-2x ion-android-star-outline'
    }, {
        stateOn: 'icon-2x ion-android-folder',
        stateOff: 'icon-2x ion-android-folder-open'
    }];

    // CAROUSEL PROPS
    public myInterval: number = 5000;
    public noWrapSlides: boolean = false;
    public slides: Array<any> = [];

    // TYPEAHEAD PROPS
    public stateCtrl: FormControl = new FormControl();

    public myForm: FormGroup = new FormGroup({
        state: this.stateCtrl
    });

    public customSelected: string = '';
    public selected: string = '';
    public dataSource: Observable<any>;
    public asyncSelected: string = '';
    public typeaheadLoading: boolean = false;
    public typeaheadNoResults: boolean = false;
    public states: Array<string> = ['Alabama', 'Alaska', 'Arizona', 'Arkansas',
        'California', 'Colorado',
        'Connecticut', 'Delaware', 'Florida', 'Georgia', 'Hawaii', 'Idaho',
        'Illinois', 'Indiana', 'Iowa',
        'Kansas', 'Kentucky', 'Louisiana', 'Maine', 'Maryland', 'Massachusetts',
        'Michigan', 'Minnesota',
        'Mississippi', 'Missouri', 'Montana', 'Nebraska', 'Nevada', 'New Hampshire',
        'New Jersey', 'New Mexico',
        'New York', 'North Dakota', 'North Carolina', 'Ohio', 'Oklahoma', 'Oregon',
        'Pennsylvania', 'Rhode Island',
        'South Carolina', 'South Dakota', 'Tennessee', 'Texas', 'Utah', 'Vermont',
        'Virginia', 'Washington',
        'West Virginia', 'Wisconsin', 'Wyoming'];
    public statesComplex: Array<any> = [
        { id: 1, name: 'Alabama' }, { id: 2, name: 'Alaska' }, { id: 3, name: 'Arizona' },
        { id: 4, name: 'Arkansas' }, { id: 5, name: 'California' },
        { id: 6, name: 'Colorado' }, { id: 7, name: 'Connecticut' },
        { id: 8, name: 'Delaware' }, { id: 9, name: 'Florida' },
        { id: 10, name: 'Georgia' }, { id: 11, name: 'Hawaii' },
        { id: 12, name: 'Idaho' }, { id: 13, name: 'Illinois' },
        { id: 14, name: 'Indiana' }, { id: 15, name: 'Iowa' },
        { id: 16, name: 'Kansas' }, { id: 17, name: 'Kentucky' },
        { id: 18, name: 'Louisiana' }, { id: 19, name: 'Maine' },
        { id: 21, name: 'Maryland' }, { id: 22, name: 'Massachusetts' },
        { id: 23, name: 'Michigan' }, { id: 24, name: 'Minnesota' },
        { id: 25, name: 'Mississippi' }, { id: 26, name: 'Missouri' },
        { id: 27, name: 'Montana' }, { id: 28, name: 'Nebraska' },
        { id: 29, name: 'Nevada' }, { id: 30, name: 'New Hampshire' },
        { id: 31, name: 'New Jersey' }, { id: 32, name: 'New Mexico' },
        { id: 33, name: 'New York' }, { id: 34, name: 'North Dakota' },
        { id: 35, name: 'North Carolina' }, { id: 36, name: 'Ohio' },
        { id: 37, name: 'Oklahoma' }, { id: 38, name: 'Oregon' },
        { id: 39, name: 'Pennsylvania' }, { id: 40, name: 'Rhode Island' },
        { id: 41, name: 'South Carolina' }, { id: 42, name: 'South Dakota' },
        { id: 43, name: 'Tennessee' }, { id: 44, name: 'Texas' },
        { id: 45, name: 'Utah' }, { id: 46, name: 'Vermont' },
        { id: 47, name: 'Virginia' }, { id: 48, name: 'Washington' },
        { id: 49, name: 'West Virginia' }, { id: 50, name: 'Wisconsin' },
        { id: 51, name: 'Wyoming' }];

    oneAtATime: false;

    // pagination/pager
    public totalItems: number = 64;
    public currentPage: number = 4;

    public maxSize: number = 5;
    public bigTotalItems: number = 175;
    public bigCurrentPage: number = 1;

    // buttons
    public singleModel: boolean = true;
    public radioModel: string = 'Middle';
    public checkModel: any = { left: false, middle: true, right: false };

    /*
        NG2BOOTSTRAP MODAL backdrop fix
        https://github.com/valor-software/ng2-bootstrap/issues/1235
    */
    showBackdrop() {
        let el = document.createElement('div');
        el.className = 'modal-backdrop fade in';
        document.body.appendChild(el);
    }
    hideBackdrop() {
        document.body.removeChild(document.querySelector('.modal-backdrop'));
    }

    constructor() {
        // init carousel
        this.addSlide(1);
        this.addSlide(2);
        this.addSlide(3);
        // progressbar
        this.random();
        this.randomStacked();
    }

    // pagination/pager
    public setPage(pageNo: number): void {
        this.currentPage = pageNo;
    };

    public pageChanged(event: any): void {
        console.log('Page changed to: ' + event.page);
        console.log('Number items per page: ' + event.itemsPerPage);
    };

    // CAROUSEL METHODS
    public addSlide(id = 5): void {
        this.slides.push({
            image: 'assets/img/pic' + id + '.jpg',
            text: ['More', 'Extra', 'Lots of', 'Surplus'][this.slides.length % 2] + ' ' +
                ['Trees', 'Mountains', 'Clouds', 'Space'][this.slides.length % 2]
        });
    }

    // TYPEAHEAD METHODS
    public getStatesAsObservable(token: string): Observable<any> {
        let query = new RegExp(token, 'ig');

        return Observable.of(
            this.statesComplex.filter((state: any) => {
                return query.test(state.name);
            })
        );
    }

    public changeTypeaheadLoading(e: boolean): void {
        this.typeaheadLoading = e;
    }

    public changeTypeaheadNoResults(e: boolean): void {
        this.typeaheadNoResults = e;
    }

    public typeaheadOnSelect(e: any): void {
        console.log('Selected value: ', e.item);
    }

    // ALERT METHOD
    public closeAlert(i: number): void {
        this.alerts.splice(i, 1);
    }

    public addAlert(): void {
        this.alerts.push({ msg: 'Another alert!', type: 'warning', closable: true });
    }

    // PROGRESSBAR METHODS
    public random(): void {
        let value = Math.floor((Math.random() * 100) + 1);
        let type: string;

        if (value < 25) {
            type = 'success';
        } else if (value < 50) {
            type = 'info';
        } else if (value < 75) {
            type = 'warning';
        } else {
            type = 'danger';
        }

        this.showWarning = (type === 'danger' || type === 'warning');
        this.dynamic = value;
        this.type = type;
    };

    public randomStacked(): void {
        let types = ['success', 'info', 'warning', 'danger'];

        this.stacked = [];
        let total = 0;
        let n = Math.floor((Math.random() * 4) + 1);
        for (let i = 0; i < n; i++) {
            let index = Math.floor((Math.random() * 4));
            let value = Math.floor((Math.random() * 30) + 1);
            total += value;
            this.stacked.push({
                value: value,
                max: value, // i !== (n - 1) ? value : 100,
                type: types[index]
            });
        }
    };

    // TOOLTIPS METHODS
    public tooltipStateChanged(state: boolean): void {
        console.log(`Tooltip is open: ${state}`);
    }

    // RATINGS METHODS
    public hoveringOver(value: number): void {
        this.overStar = value;
        this.percent = 100 * (value / this.maxRat);
    };

    public resetStar(): void {
        this.overStar = void 0;
    }



    ngOnInit() {
    }

}
