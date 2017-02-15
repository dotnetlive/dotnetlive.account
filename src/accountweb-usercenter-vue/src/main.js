// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import VueRouter from 'vue-router'
import App from './App'
import routers from './routers'
import './assets/bootstrap/css/bootstrap.css'
import './assets/content/animate.css'
import './assets/content/style.css'
/* eslint-disable no-new */
Vue.use(VueRouter)
const router = new VueRouter(routers)
new Vue({
    el: '#app',
    router,
    template: '<App/>',
    components: { App }
})