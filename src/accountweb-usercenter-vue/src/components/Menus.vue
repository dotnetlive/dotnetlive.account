<template>
    <nav class="navbar-default navbar-static-side" role="navigation" @click="menusNav()">
        <div class="sidebar-collapse">
            <ul class="nav metismenu" id="side-menu">
                <li class="nav-header">
                    <div class="dropdown profile-element" v-bind:class="showMenus">
                        <span><img alt="image" class="img-circle" src="./../assets/images/profile_small.jpg" />
                             </span>
                        <a data-toggle="dropdown" class="dropdown-toggle">
                            <span class="clear"> 
                                <span class="block m-t-xs"> 
                                <strong class="font-bold">David Williams</strong>
                             </span>
                            <span class="text-muted text-xs block" @click="showMenusNav($event)">Art Director <b class="caret"></b></span>
                            </span>
                        </a>
                        <ul class="dropdown-menu animated fadeInRight m-t-xs">
                            <li>
                                <router-link :to="{ path: 'profile'}">Profile</router-link>
                            </li>
                            <li><a href="contacts.html">Contacts</a></li>
                            <li><a href="mailbox.html">Mailbox</a></li>
                            <li class="divider"></li>
                            <li><a href="login.html">Logout</a></li>
                        </ul>
                    </div>
                    <div class="logo-element">
                        IN+
                    </div>
                </li>
                <li class="active">
                    <router-link :to="{ path: '/home/index'}"><i class="fa fa-th-large"></i> <span class="nav-label">首页</span><span class="fa arrow"></span></router-link>
                </li>
                <li>
                    <router-link :to="{ path: '/home/profile'}"><i class="fa fa-diamond"></i> <span class="nav-label">个人中心</span></router-link>
                </li>
            </ul>

        </div>
    </nav>
</template>

<script>
    export default {
        data() {
            return {
                showMenus: ''
            }
        },
        created() {
            // document.onclick = (e) => {
            //     this.showMenus = false;
            // };
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