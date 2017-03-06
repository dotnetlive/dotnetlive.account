<template>
    <aside class="sidebar-container">
        <div class="sidebar-header">
            <div class="pull-right pt-lg text-muted hidden"><em class="ion-close-round"></em></div>
            <a href="#" class="sidebar-header-logo"><img src="./../assets/images/logo.png" data-svg-replace="./../assets/images/logo.svg" alt="Logo"><span class="sidebar-header-logo-text">Centric</span></a>
        </div>
        <div class="sidebar-content">
            <div class="sidebar-toolbar text-center">
                <a href=""><img src="./../assets/images/user/01.jpg" alt="Profile" class="img-circle thumb64"></a>
                <div class="mt">Welcome, {{userName}}</div>
            </div>
            <nav class="sidebar-nav">
                <ul>
                    <li>
                        <router-link :to="{ path: '/home/index'}" class="ripple">
                            <span class="pull-right nav-label">
                                <span class="badge bg-success">2</span>
                            </span>
                            <span class="nav-icon">
                                <img src="" data-svg-replace="./../assets/images/icons/aperture.svg" alt="MenuItem" class="hidden">
                            </span>
                            <span>首页</span>
                        </router-link>
                    </li>
                    <li>
                        <router-link :to="{ path: '/home/profile'}" class="ripple">
                            <span class="pull-right nav-label"></span>
                            <span class="nav-icon">
                                <img src="" data-svg-replace="./../assets/images/icons/radio-waves.svg" alt="MenuItem" class="hidden">
                            </span>
                            <span>个人中心</span>
                        </router-link>
                    </li>
                </ul>
            </nav>
        </div>
    </aside>
</template>

<script>
    export default {
        data() {
            return {
                showMenus: '',
                userName: ''
            }
        },
        created() {
            this.userName = this.$store.state.user.userName;
        },
        methods: {
            menusNav() {
                this.showMenus = ''
            },
            showMenusNav(event) {
                event.stopPropagation();
                this.showMenus = 'open'
            }

        }
    }

</script>