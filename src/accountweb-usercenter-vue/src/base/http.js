import axios from 'axios'
export default class Http {
    constructor() {
        this.baseApi = "http://api.account.dotnet.live/";
        // this.token = token;
    };
    get(url, data) {
        data = data || {};
        let urlParams = '';
        for (let key in data) {
            urlParams = urlParams + `&${key}=${data[key]}`;
        }
        if (urlParams) {
            url = url.indexOf('?') < 0 ? url + '?' + urlParams : url + '&' + urlParams;
        }
        return this.request({
            url: this.baseApi + url,
            method: 'get',
            headers: { 'Authorization': sessionStorage.getItem("token") || '' }
        });
    }
    post(url, data) {
        var formData = new FormData();
        for (var item in data) {
            formData.append(item, data[item]);
        }
        if (sessionStorage.getItem("token")) {
            formData.append('Authorization', sessionStorage.getItem("token"));
        }
        return this.request({
            url: this.baseApi + url,
            data: formData,
            method: 'post',
            headers: { 'Authorization': sessionStorage.getItem("token") || '' }
        });
    }
    put(url, data) {
        var formData = new FormData();
        for (var item in data) {
            formData.append(item, data[item]);
        }
        if (sessionStorage.getItem("token")) {
            formData.append('Authorization', sessionStorage.getItem("token"));
        }
        return this.request({
            url: this.baseApi + url,
            data: formData,
            method: 'put',
            headers: { 'Authorization': sessionStorage.getItem("token") || '' }
        });
    }
    delete(url, data) {
        var formData = new FormData();
        for (var item in data) {
            formData.append(item, data[item]);
        }
        if (sessionStorage.getItem("token")) {
            formData.append('Authorization', sessionStorage.getItem("token"));
        }
        return this.request({
            url: this.baseApi + url,
            data: formData,
            method: 'delete',
            headers: { 'Authorization': sessionStorage.getItem("token") || '' }
        });
    }
    request(options) {
        var promise = new Promise((resolve, reject) => {
            axios(options)
                .then(function (result) {
                    if (result.status == 200) {
                        return result.data;
                    } else {
                        reject(result.statusText);
                    }
                })
                .then((result) => {
                    resolve(result);
                })
                .catch(function (err) {
                    alert(err);
                    reject(err);
                });
        })
        return promise;
    }
}