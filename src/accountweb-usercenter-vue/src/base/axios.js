import axios from 'axios'
var baseAjax = {
    baseApi: "http://api.account.dotnet.live/",
    get: function (url, data) {
        data = data || {};
        let urlParams = '';
        for (let key in data) {
            urlParams = urlParams + `&${key}=${data[key]}`;
        }
        if (urlParams)
            url = url.indexOf('?') < 0 ? url + '?' + urlParams : url + '&' + urlParams;
        var options = {
            url: this.baseApi + url,
            method: 'get',
        };
        this.request(options);
    },
    post: function (url, data) {
        var formData = new FormData();
        for (var item in data) {
            formData.append(item, data[item]);
        }
        var options = {
            url: this.baseApi + url,
            data: formData,
            method: 'post',
        };
        return this.request(options);
    },
    request: function (options) {
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
module.exports = baseAjax;