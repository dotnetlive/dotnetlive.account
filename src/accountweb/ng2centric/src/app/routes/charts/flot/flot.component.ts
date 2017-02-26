import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Http } from '@angular/http';
declare var $: any;

import { PagetitleService } from '../../../core/pagetitle/pagetitle.service';
import { ColorsService } from '../../../core/colors/colors.service';

@Component({
    selector: 'app-flot',
    templateUrl: './flot.component.html',
    styleUrls: ['./flot.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class FlotComponent implements OnInit {

    // BAR
    // -----------------------------------
    barData: any;
    barOptions = {
        series: {
            bars: {
                align: 'center',
                lineWidth: 0,
                show: true,
                barWidth: 0.6,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.8
                    }, {
                        opacity: 0.5
                    }]
                }
            }
        },
        grid: {
            borderColor: 'rgba(162,162,162,.26)',
            borderWidth: 1,
            hoverable: true,
            backgroundColor: 'transparent'
        },
        tooltip: true,
        tooltipOpts: {
            content: (label, x, y) => x + ' : ' + y
        },
        xaxis: {
            tickColor: 'rgba(162,162,162,.26)',
            font: { color: this.colors.byName('blueGrey-200') },
            mode: 'categories'
        },
        yaxis: {
            // position: 'right' or 'left',
            tickColor: 'rgba(162,162,162,.26)',
            font: { color: this.colors.byName('blueGrey-200') }
        },
        shadowSize: 0
    };

    // BAR STACKED
    // -----------------------------------
    barStackeData: any;
    barStackedOptions = {
        series: {
            stack: true,
            bars: {
                align: 'center',
                lineWidth: 0,
                show: true,
                barWidth: 0.6,
                fill: 0.9
            }
        },
        grid: {
            borderColor: 'rgba(162,162,162,.26)',
            borderWidth: 1,
            hoverable: true,
            backgroundColor: 'transparent'
        },
        tooltip: true,
        tooltipOpts: {
            content: (label, x, y) => x + ' : ' + y
        },
        xaxis: {
            tickColor: 'rgba(162,162,162,.26)',
            font: { color: this.colors.byName('blueGrey-200') },
            mode: 'categories'
        },
        yaxis: {
            min: 0,
            max: 200, // optional: use it for a clear represetation
            // position: 'right' or 'left',
            tickColor: 'rgba(162,162,162,.26)',
            font: { color: this.colors.byName('blueGrey-200') }
        },
        shadowSize: 0
    };

    // SPLINE
    // -----------------------------------
    splineData: any;
    splineOptions = {
        series: {
            lines: {
                show: false
            },
            points: {
                show: true,
                radius: 2
            },
            splines: {
                show: true,
                tension: 0.4,
                lineWidth: 1,
                fill: 1
            }
        },
        grid: {
            borderColor: 'rgba(162,162,162,.26)',
            borderWidth: 1,
            hoverable: true,
            backgroundColor: 'transparent'
        },
        tooltip: true,
        tooltipOpts: {
            content: (label, x, y) => x + ' : ' + y
        },
        xaxis: {
            tickColor: 'rgba(162,162,162,.26)',
            font: { color: this.colors.byName('blueGrey-200') },
            mode: 'categories'
        },
        yaxis: {
            min: 0,
            max: 150, // optional: use it for a clear represetation
            tickColor: 'rgba(162,162,162,.26)',
            font: { color: this.colors.byName('blueGrey-200') },
            // position: 'right' or 'left',
            tickFormatter: (v) => v
        },
        shadowSize: 0
    };

    // AREA
    // -----------------------------------
    areaData: any;
    areaOptions = {
        series: {
            lines: {
                show: true,
                fill: true,
                fillColor: {
                    colors: [{
                        opacity: 0.5
                    }, {
                        opacity: 0.9
                    }]
                }
            },
            points: {
                show: false
            }
        },
        grid: {
            borderColor: 'rgba(162,162,162,.26)',
            borderWidth: 1,
            hoverable: true,
            backgroundColor: 'transparent'
        },
        tooltip: true,
        tooltipOpts: {
            content: (label, x, y) => x + ' : ' + y
        },
        xaxis: {
            tickColor: 'rgba(162,162,162,.26)',
            font: { color: this.colors.byName('blueGrey-200') },
            mode: 'categories'
        },
        yaxis: {
            min: 0,
            max: 150,
            tickColor: 'rgba(162,162,162,.26)',
            font: { color: this.colors.byName('blueGrey-200') },
            // position: 'right' or 'left'
        },
        shadowSize: 0
    };

    // LINE
    // -----------------------------------
    lineData: any;
    lineOptions = {
        series: {
            lines: {
                show: true,
                fill: 0.01
            },
            points: {
                show: true,
                radius: 4
            }
        },
        grid: {
            borderColor: 'rgba(162,162,162,.26)',
            borderWidth: 1,
            hoverable: true,
            backgroundColor: 'transparent'
        },
        tooltip: true,
        tooltipOpts: {
            content: (label, x, y) => x + ' : ' + y
        },
        xaxis: {
            tickColor: 'rgba(162,162,162,.26)',
            font: { color: this.colors.byName('blueGrey-200') },
            mode: 'categories'
        },
        yaxis: {
            // position: 'right' or 'left',
            tickColor: 'rgba(162,162,162,.26)',
            font: { color: this.colors.byName('blueGrey-200') }
        },
        shadowSize: 0
    };

    // PIE
    // -----------------------------------
    pieData = [{
        'label': 'CSS',
        'color': '#009688',
        'data': 40
    }, {
        'label': 'LESS',
        'color': '#FFC107',
        'data': 90
    }, {
        'label': 'SASS',
        'color': '#FF7043',
        'data': 75
    }];
    pieOptions = {
        series: {
            pie: {
                show: true,
                innerRadius: 0,
                label: {
                    show: true,
                    radius: 0.8,
                    formatter: (label, series) => {
                        return '<div class="flot-pie-label">' +
                            //label + ' : ' +
                            Math.round(series.percent) +
                            '%</div>';
                    },
                    background: {
                        opacity: 0.8,
                        color: '#222'
                    }
                }
            }
        }
    };

    // DONUT
    // -----------------------------------
    donutData = [{
        'color': '#4CAF50',
        'data': 60,
        'label': 'Coffee'
    }, {
        'color': '#009688',
        'data': 90,
        'label': 'CSS'
    }, {
        'color': '#FFC107',
        'data': 50,
        'label': 'LESS'
    }, {
        'color': '#FF7043',
        'data': 80,
        'label': 'Jade'
    }, {
        'color': '#3949AB',
        'data': 116,
        'label': 'AngularJS'
    }];
    donutOptions = {
        series: {
            pie: {
                show: true,
                innerRadius: 0.5 // This makes the donut shape
            }
        }
    };

    // REALTIME
    // -----------------------------------
    private rtAuxData = [];
    realTimeData: any;
    realTimeOptions = {
        series: {
            lines: {
                show: true,
                fill: true,
                fillColor: {
                    colors: ['#3F51B5', '#3F51B5']
                }
            },
            shadowSize: 0 // Drawing is faster without shadows
        },
        grid: {
            show: false,
            borderWidth: 0,
            minBorderMargin: 20,
            labelMargin: 10
        },
        xaxis: {
            tickFormatter: () => ''
        },
        yaxis: {
            min: 0,
            max: 110
        },
        legend: {
            show: true
        },
        colors: ['#3F51B5']
    };
    constructor(private http: Http, pt: PagetitleService, private colors: ColorsService) {

        pt.setTitle('Flot Charts');

        this.getChartData('assets/chart/bar.json').subscribe(data => this.barData = data);
        this.getChartData('assets/chart/barstacked.json').subscribe(data => this.barStackeData = data);
        this.getChartData('assets/chart/spline.json').subscribe(data => this.splineData = data);
        this.getChartData('assets/chart/area.json').subscribe(data => this.areaData = data);
        this.getChartData('assets/chart/line.json').subscribe(data => this.lineData = data);

    }

    getChartData(url) {
        return this.http.get(url).map(data => data.json());
    }

    ngOnInit() {
        // Generate random data for realtime demo
        this.update();
    }

    // REALTIME demo
    // -----------------------------------
    getRandomData() {
        let totalPoints = 300;
        if (this.rtAuxData.length > 0) {
            this.rtAuxData = this.rtAuxData.slice(1);
        }
        // Do a random walk
        while (this.rtAuxData.length < totalPoints) {
            let prev = this.rtAuxData.length > 0 ? this.rtAuxData[this.rtAuxData.length - 1] : 50,
                y = prev + Math.random() * 10 - 5;
            if (y < 0) {
                y = 0;
            } else if (y > 100) {
                y = 100;
            }
            this.rtAuxData.push(y);
        }
        // Zip the generated y values with the x values
        let res = [];
        for (let i = 0; i < this.rtAuxData.length; ++i) {
            res.push([i, this.rtAuxData[i]]);
        }
        return [res];
    }

    update() {
        this.realTimeData = this.getRandomData();
        setTimeout(this.update.bind(this), 30);
    }

}
