module.exports = {
  mode: "hash",
  routes: [
    {
      path: '/',
      component: resolve => require(['./view/Login.vue'], resolve)
    },
    {
      path: '/home',
      component: resolve => require(['./view/Home.vue'], resolve),
      children: [
        {
          path: 'index',
          component: resolve => require(['./components/Index.vue'], resolve)
        },
        {
          path: 'profile',
          component: resolve => require(['./components/Profile.vue'], resolve)
        }
      ]
    }
  ]
}

