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
          // 当 /user/:id/profile 匹配成功，
          // UserProfile 会被渲染在 User 的 <router-view> 中
          path: 'index',
          component: resolve => require(['./components/Index.vue'], resolve)
        },
        {
          // 当 /user/:id/posts 匹配成功
          // UserPosts 会被渲染在 User 的 <router-view> 中
          path: 'profile',
          component: resolve => require(['./components/Profile.vue'], resolve)
        }
      ]
    }
  ]
}

