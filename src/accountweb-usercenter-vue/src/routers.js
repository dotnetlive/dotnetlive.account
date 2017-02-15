module.exports = {
  mode: "hash",
  routes: [
    {
      path: '/',
      component: resolve => require(['./view/Home.vue'], resolve)
    },
    {
      path: '/profile',
      component: resolve => require(['./view/Profile.vue'], resolve)
    }
  ]
}

