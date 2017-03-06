<template>
    <div class="layout-container">
        <div class="page-container bg-blue-grey-900">
            <div class="container-full">
                <div class="container container-xs"><img src="./../assets/images/logo.png" class="mv-lg block-center img-responsive thumb64">
                    <div class="card b0 form-validate">
                        <div class="card-offset pb0">
                            <div class="card-offset-item text-right"><a href="signup.html" class="btn-raised btn btn-info btn-circle btn-lg"><em class="ion-person-add"></em></a></div>
                            <div class="card-offset-item text-right hidden">
                                <div class="btn btn-success btn-circle btn-lg"><em class="ion-checkmark-round"></em></div>
                            </div>
                        </div>
                        <div class="card-heading">
                            <div class="card-title text-center">Login</div>
                        </div>
                        <div class="card-body">
                            <div class="mda-form-group float-label mda-input-group">
                                <div class="mda-form-control">
                                    <input type="email" name="accountName" v-model="userName" class="form-control">
                                    <div class="mda-form-control-line"></div>
                                    <label>Email address</label>
                                </div><span class="mda-input-group-addon"><em class="ion-ios-email-outline icon-lg"></em></span>
                            </div>
                            <div class="mda-form-group float-label mda-input-group">
                                <div class="mda-form-control">
                                    <input type="password" name="accountPassword" v-model="password" class="form-control">
                                    <div class="mda-form-control-line"></div>
                                    <label>Password</label>
                                </div><span class="mda-input-group-addon"><em class="ion-ios-locked-outline icon-lg"></em></span>
                            </div>
                        </div>
                        <button @click='loginIn' class="btn btn-primary btn-flat">Authenticate</button>
                    </div>
                    <div class="text-center text-sm"><a href="recover.html" class="text-inherit">Forgot password?</a></div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                userName: '',
                password: ''
            }
        },
        created() {

        },
        methods: {
            loginIn() {
                this.$http.get('api/account/login', { email: this.userName, passwordHash: this.password, withBearerPrefix: true }).then((result) => {
                    sessionStorage.setItem("token", result.token)
                    this.$store.dispatch('inUser', result.loginUser);
                    this.$router.push({ path: '/home/index' });
                })
            }
        }
    }

</script>