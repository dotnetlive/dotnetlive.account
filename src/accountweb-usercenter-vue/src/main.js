// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import VueRouter from 'vue-router'
import App from './App'
import routers from './routers'
import store from './store'

// import 'bootstrap'
import './assets/less/index.less'
import './assets/less/style.less'
/* eslint-disable no-new */

import Http from './base/http'
Vue.prototype.$http = new Http();

Vue.use(VueRouter)
const router = new VueRouter(routers)
router.beforeEach((to, from, next) => {
  if (to.path != "/") {
    if (_.isNull(sessionStorage.getItem("token"))) {
      next({
        path: '/'
      })
    } else {
      next();
    }
  } else {
    next();
  }
})


new Vue({
  el: '#app',
  router,
  store,
  template: '<App/>',
  components: { App }
})
