module.exports = {
  mode: "hash",
  routes: [
    {
      path: '/',
<<<<<<< HEAD
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
=======
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
>>>>>>> 99514e3e0a214704904fcb3c912370caf5d195a7
    }
  ]
}

