<template>
    <header class="header-container">
        <nav>
            <ul class="visible-xs visible-sm">
                <li><a id="sidebar-toggler" href="#" class="menu-link menu-link-slide"><span><em></em></span></a></li>
            </ul>
            <ul class="hidden-xs">
                <li><a id="offcanvas-toggler" href="#" class="menu-link menu-link-slide"><span><em></em></span></a></li>
            </ul>
            <h2 class="header-title">Dashboard</h2>
            <ul class="pull-right">
                <li><a id="header-search" href="#" class="ripple"><em class="ion-ios-search-strong"></em></a></li>
                <li class="dropdown" v-bind:class="isShowTags?'open':''">
                    <a @click="showTags" data-toggle="dropdown" class="dropdown-toggle has-badge ripple">
                        <em class="ion-person"></em>
                        <sup class="badge bg-danger">3</sup>
                    </a>
                    <ul class="dropdown-menu dropdown-menu-right md-dropdown-menu">
                        <li><a><em class="ion-home icon-fw"></em>Profile</a></li>
                        <li><a><em class="ion-gear-a icon-fw"></em>Messages</a></li>
                        <li role="presentation" class="divider"></li>
                        <li><a @click="logout"><em class="ion-log-out icon-fw"></em>Logout</a></li>
                    </ul>
                </li>
                <li><a id="header-settings" href="#" class="ripple"><em class="ion-gear-b"></em></a></li>
            </ul>
        </nav>
    </header>
</template>

<script>
    export default {
        data() {
            return {
                isShowTags: false,
            }
        },
        methods: {
            showTags() {
                if (this.isShowTags) {
                    this.isShowTags = false;
                } else {
                    this.isShowTags = true;
                }
            },
            logout() {
                this.$http.get('api/account/logoff').then((result) => {
                    sessionStorage.clear();
                    this.$store.dispatch('inUser', {});
                    this.$router.push({ path: '/' });
                })
            }
        }
    }

</script>