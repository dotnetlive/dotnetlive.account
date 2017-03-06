import { Injectable } from '@angular/core';

var materialColors = require('material-colors');

@Injectable()
export class ColorsService {

    APP_COLORS = {
        'gray-darker':            '#263238',
        'gray-dark':              '#455A64',
        'gray':                   '#607D8B',
        'gray-light':             '#90A4AE',
        'gray-lighter':           '#ECEFF1',
        'primary':                '#448AFF',
        'success':                '#4CAF50',
        'info':                   '#03A9F4',
        'warning':                '#FFB300',
        'danger':                 '#F44336'
    };

    constructor() { }

    byName(name: string) {
        var color = this.APP_COLORS[name];
        if (!color && materialColors) {
            var c = name.split('-'); // red-500, blue-a100, deepPurple-500, etc
            if (c.length)
                color = (materialColors[c[0]] || {})[c[1]];
        }
        return (color || '#fff');
    }

}
