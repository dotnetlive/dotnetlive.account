module.exports = {
  mode: "hash",
  routes: [
    {
      path: '/',
<<<<<<< HEAD
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
=======
      component: resolve => require(['./view/Home.vue'], resolve)
    },
    {
      path: '/profile',
      component: resolve => require(['./view/Profile.vue'], resolve)
>>>>>>> add ui
    }
  ]
}

