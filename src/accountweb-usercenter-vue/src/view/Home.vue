<template>
    <div class="theme-1">
        <div class="layout-container">
            <!-- top navbar-->
            <MyHead></MyHead>
            <!-- sidebar-->
            <Menus></Menus>
            <div class="sidebar-layout-obfuscator"></div>
            <!-- Main section-->
            <router-view></router-view>
        </div>
        <!-- Search template-->
        <div tabindex="-1" role="dialog" class="modal modal-top fade modal-search">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-body">
                        <div class="pull-left">
                            <button type="button" data-dismiss="modal" class="btn btn-flat"><em class="ion-arrow-left-c icon-24"></em></button>
                        </div>
                        <div class="pull-right">
                            <button type="button" class="btn btn-flat"><em class="ion-android-microphone icon-24"></em></button>
                        </div>
                        <form action="#" class="oh">
                            <div class="mda-form-control pt0">
                                <input type="text" placeholder="Search.." data-localize="header.SEARCH" class="form-control header-input-search">
                                <div class="mda-form-control-line"></div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
        <!-- End Search template-->
        <!-- Settings template-->
        <div tabindex="-1" role="dialog" class="modal-settings modal modal-right fade">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <div data-dismiss="modal" class="pull-right clickable"><em class="ion-close-round text-soft"></em></div>
                        <h4 class="modal-title"><span>Settings</span></h4>
                    </div>
                    <div class="modal-body">
                        <div class="clearfix mb">
                            <div class="pull-left wd-tiny mb">
                                <div class="setting-color">
                                    <label class="preview-theme-1">
                    <input type="radio" checked="checked" name="setting-theme" value="0"><span class="ion-checkmark-round"></span>
                    <div class="t-grid">
                      <div class="t-row">
                        <div class="t-col preview-header"></div>
                        <div class="t-col preview-sidebar"></div>
                        <div class="t-col preview-content"></div>
                      </div>
                    </div>
                  </label>
                                </div>
                            </div>
                            <div class="pull-left wd-tiny mb">
                                <div class="setting-color">
                                    <label class="preview-theme-2">
                    <input type="radio" name="setting-theme" value="1"><span class="ion-checkmark-round"></span>
                    <div class="t-grid">
                      <div class="t-row">
                        <div class="t-col preview-header"></div>
                        <div class="t-col preview-sidebar"></div>
                        <div class="t-col preview-content"></div>
                      </div>
                    </div>
                  </label>
                                </div>
                            </div>
                            <div class="pull-left wd-tiny mb">
                                <div class="setting-color">
                                    <label class="preview-theme-3">
                    <input type="radio" name="setting-theme" value="2"><span class="ion-checkmark-round"></span>
                    <div class="t-grid">
                      <div class="t-row">
                        <div class="t-col preview-header"></div>
                        <div class="t-col preview-sidebar"></div>
                        <div class="t-col preview-content"></div>
                      </div>
                    </div>
                  </label>
                                </div>
                            </div>
                            <div class="pull-left wd-tiny mb">
                                <div class="setting-color">
                                    <label class="preview-theme-4">
                    <input type="radio" name="setting-theme" value="3"><span class="ion-checkmark-round"></span>
                    <div class="t-grid">
                      <div class="t-row">
                        <div class="t-col preview-header"></div>
                        <div class="t-col preview-sidebar"></div>
                        <div class="t-col preview-content"></div>
                      </div>
                    </div>
                  </label>
                                </div>
                            </div>
                            <div class="pull-left wd-tiny mb">
                                <div class="setting-color">
                                    <label class="preview-theme-5">
                    <input type="radio" name="setting-theme" value="4"><span class="ion-checkmark-round"></span>
                    <div class="t-grid">
                      <div class="t-row">
                        <div class="t-col preview-header"></div>
                        <div class="t-col preview-sidebar"></div>
                        <div class="t-col preview-content"></div>
                      </div>
                    </div>
                  </label>
                                </div>
                            </div>
                            <div class="pull-left wd-tiny mb">
                                <div class="setting-color">
                                    <label class="preview-theme-6">
                    <input type="radio" name="setting-theme" value="5"><span class="ion-checkmark-round"></span>
                    <div class="t-grid">
                      <div class="t-row">
                        <div class="t-col preview-header"></div>
                        <div class="t-col preview-sidebar"></div>
                        <div class="t-col preview-content"></div>
                      </div>
                    </div>
                  </label>
                                </div>
                            </div>
                            <div class="pull-left wd-tiny mb">
                                <div class="setting-color">
                                    <label class="preview-theme-7">
                    <input type="radio" name="setting-theme" value="6"><span class="ion-checkmark-round"></span>
                    <div class="t-grid">
                      <div class="t-row">
                        <div class="t-col preview-header"></div>
                        <div class="t-col preview-sidebar"></div>
                        <div class="t-col preview-content"></div>
                      </div>
                    </div>
                  </label>
                                </div>
                            </div>
                            <div class="pull-left wd-tiny mb">
                                <div class="setting-color">
                                    <label class="preview-theme-8">
                    <input type="radio" name="setting-theme" value="7"><span class="ion-checkmark-round"></span>
                    <div class="t-grid">
                      <div class="t-row">
                        <div class="t-col preview-header"></div>
                        <div class="t-col preview-sidebar"></div>
                        <div class="t-col preview-content"></div>
                      </div>
                    </div>
                  </label>
                                </div>
                            </div>
                            <div class="pull-left wd-tiny mb">
                                <div class="setting-color">
                                    <label class="preview-theme-9">
                    <input type="radio" name="setting-theme" value="8"><span class="ion-checkmark-round"></span>
                    <div class="t-grid">
                      <div class="t-row">
                        <div class="t-col preview-header"></div>
                        <div class="t-col preview-sidebar"></div>
                        <div class="t-col preview-content"></div>
                      </div>
                    </div>
                  </label>
                                </div>
                            </div>
                        </div>
                        <hr>
                        <p>
                            <label class="mda-checkbox">
                <input id="sidebar-showheader" type="checkbox" checked=""><em class="bg-indigo-500"></em>Sidebar header
              </label>
                        </p>
                        <p>
                            <label class="mda-checkbox">
                <input id="sidebar-showtoolbar" type="checkbox" checked=""><em class="bg-indigo-500"></em>Sidebar toolbar
              </label>
                        </p>
                        <p>
                            <label class="mda-checkbox">
                <input id="sidebar-offcanvas" type="checkbox"><em class="bg-indigo-500"></em>Sidebar offcanvas
              </label>
                        </p>
                        <hr>
                        <p>Navigation icon</p>
                        <p>
                            <label class="mda-radio">
                <input type="radio" name="headerMenulink" value="menu-link-close"><em class="bg-indigo-500"></em>Close icon
              </label>
                        </p>
                        <p>
                            <label class="mda-radio">
                <input type="radio" checked="" name="headerMenulink" value="menu-link-slide"><em class="bg-indigo-500"></em>Slide arrow
              </label>
                        </p>
                        <p>
                            <label class="mda-radio">
                <input type="radio" name="headerMenulink" value="menu-link-arrow"><em class="bg-indigo-500"></em>Big arrow
              </label>
                        </p>
                        <hr>
                        <button type="button" data-toggle-fullscreen="" class="btn btn-default btn-raised">Toggle fullscreen</button>
                        <hr>
                        <p>Change language</p>
                        <!-- START Language list-->
                        <div class="btn-group">
                            <button type="button" data-toggle="dropdown" class="btn btn-default btn-block btn-raised">English</button>
                            <ul role="menu" class="dropdown-menu dropdown-menu-right animated fadeInUpShort">
                                <li><a href="#" data-set-lang="en">English</a></li>
                                <li><a href="#" data-set-lang="es">Spanish</a></li>
                            </ul>
                        </div>
                        <!-- END Language list-->
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import MyHead from './../components/Header'
    import Menus from './../components/Menus'
    import MyFoot from './../components/Footer'
    export default {
        components: {
            MyHead,
            Menus,
            MyFoot
        },
        data() {
            return {
                msg: 'Welcome to Your Vue.js App'
            }
        },
        created() {
        }
    }

</script>