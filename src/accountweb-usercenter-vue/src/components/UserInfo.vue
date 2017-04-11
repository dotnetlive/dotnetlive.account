<template>
    <main class="main-container">
        <section>
            <div class="container-fluid">
                <!-- DATATABLE DEMO 1-->
                <div class="card">
                    <div class="card-heading">用户列表</div>
                    <div class="card-body">
                        <div id="datatable1_wrapper" class="dataTables_wrapper no-footer">
                            <!--<div id="datatable1_filter" class="dataTables_filter"><label><em class="ion-search"></em>
                            <input type="search" class="" placeholder="" aria-controls="datatable1"></label>
                            </div>-->
                            <table id="datatable1" class="table-datatable table table-striped table-hover mv-lg dataTable no-footer" role="grid" aria-describedby="datatable1_info">
                                <thead>
                                    <tr role="row">
                                        <th class="sorting_asc" tabindex="0" aria-controls="datatable1" rowspan="1" colspan="1" aria-sort="ascending" aria-label="Rendering engine: activate to sort column descending"
                                            style="width: 173px;">姓名</th>
                                        <th class="sorting" tabindex="0" aria-controls="datatable1" rowspan="1" colspan="1" aria-label="Browser: activate to sort column ascending"
                                            style="width: 296px;">邮箱</th>
                                        <th class="sorting" tabindex="0" aria-controls="datatable1" rowspan="1" colspan="1" aria-label="Platform(s): activate to sort column ascending"
                                            style="width: 273px;">电话号码</th>
                                        <th class="sort-numeric sorting" tabindex="0" aria-controls="datatable1" rowspan="1" colspan="1" aria-label="Engine version: activate to sort column ascending"
                                            style="width: 151px;">创建时间</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(item,index) in list" class="gradeA" v-bind:class="index%2==0?'even':'odd'" role="row">
                                        <td class="sorting_1">{{item.userName}}</td>
                                        <td>{{item.email}}</td>
                                        <td>{{item.phoneNumber}}</td>
                                        <td>{{item.createdOn}}</td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="dataTables_info" id="datatable1_info" role="status" aria-live="polite">Showing 1 to 10 of 57 entries</div>
                            <div class="dataTables_paginate paging_simple_numbers" id="datatable1_paginate">
                                <a class="paginate_button previous disabled" aria-controls="datatable1" data-dt-idx="0" tabindex="0" id="datatable1_previous">
                                    <em class="ion-ios-arrow-left"></em></a>
                                <span>
                                        <a class="paginate_button current" aria-controls="datatable1" data-dt-idx="1" tabindex="0">1</a>
                                        </span>
                                <a class="paginate_button next" aria-controls="datatable1" data-dt-idx="7" tabindex="0" id="datatable1_next">
                                    <em class="ion-ios-arrow-right"></em></a>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- DATATABLE DEMO 2-->
            </div>
        </section>
        <footer><span>2017 - Centric app.</span></footer>
    </main>
</template>

<script>
    export default {
        data() {
            return {
                list: []
            }
        },
        created() {
            this.$http.get('api/user/search').then((result) => {
                this.list = result;
            })
        },
        methods: {

        }
    }

</script>