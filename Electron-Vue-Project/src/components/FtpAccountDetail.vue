<template>
    <div>
        <modal :show="show" :modalLarge="true" v-if="show">

            <div style="margin-bottom:0px" class="card">
                <div class="card-body">
                    <div class="col-lg-12">
                        <div class="tab-content mt-2" id="nav-tabContent">
                            <div class="modal-header">

                                <h5 class="modal-title" id="myModalLabel"> Ftp Account Details </h5>

                            </div>
                            <div class="row ">

                                <div class="col-sm-6" v-bind:class="{'has-danger' : $v.ftpDetail.host.$error}">
                                    <label>Ftp Server *</label>
                                    <input class="form-control" v-model="$v.ftpDetail.host.$model" type="text" />
                                    <span v-if="$v.ftpDetail.host.$error" class="error">
                                        <span v-if="!$v.ftpDetail.host.required"> Host is required</span>
                                    </span>
                                </div>
                                <div class="form-group has-label col-sm-6 ">
                                    <label class="text  font-weight-bolder"> Ftp Port </label>
                                    <input class="form-control" v-model="ftpDetail.port" type="text" />

                                </div>
                                <div class="col-sm-6">
                                    <label>Ftp UserName</label>
                                    <input class="form-control" v-model="ftpDetail.username" type="text" />
                                </div>
                                <div class="form-group has-label col-sm-6 ">
                                    <label class="text  font-weight-bolder"> Ftp Password </label>
                                    <input class="form-control" v-model="ftpDetail.password" type="text" />
                                    
                                </div>
                                <div class="form-group has-label col-sm-6 " v-if="!isEdit" v-bind:class="{'has-danger' : $v.ftpDetail.systemType.$error}" :key="rander">
                                    <label class="text  font-weight-bolder"> System Type *:</label>
                                    <multiselect :options="systemTypeList" v-model="$v.ftpDetail.systemType.$model" :show-labels="false" placeholder="Select System Type">
                                    </multiselect>
                                    <span v-if="$v.ftpDetail.systemType.$error" class="error">
                                        <span v-if="!$v.ftpDetail.systemType.required"> System Type is required</span>
                                    </span>

                                </div>
                            </div>
                            <div class="modal-footer justify-content-right">
                                <button type="button" class="btn btn-primary " v-bind:disabled="$v.ftpDetail.$invalid" v-on:click="SaveLicense"> Save </button>
                                <button type="button" class="btn btn-danger   " v-on:click="close()">Close</button>
                                <button type="button" class="btn btn-danger" v-on:click="ClearFtpDetail">Clear</button>

                            </div>
                        </div>
                    </div>

                    <div class="mt-2">
                        <div class=" table-responsive">
                            <table class="table table-striped table-hover table_list_bg">
                                <thead class="m-0">
                                    <tr>
                                        <th style="width:5%">
                                            #
                                        </th>
                                        <th style="width:30%">
                                            Ftp Host
                                        </th>
                                        <th style="width:10%">
                                            Ftp Port
                                        </th>
                                        <th style="width:20%">
                                            User Name
                                        </th>
                                        <th style="width:20%">
                                            Password
                                        </th>
                                        <th style="width:20%">
                                            System Type
                                        </th>

                                    </tr>
                                </thead>
                                <tbody>
                                    <tr v-for="(details,index) in ftpDetailList" v-bind:key="details.id">
                                        <td>
                                            {{index+1}}
                                        </td>
                                        <td>
                                            <strong>
                                                <a href="javascript:void(0)" v-on:click="EditFtpDetail(details)">{{details.host}}</a>
                                            </strong>
                                        </td>
                                        <td>
                                            {{details.port}}
                                        </td>
                                        <td>{{details.username}}</td>
                                        <td>{{details.password}}</td>
                                        <td>{{details.systemType}}</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>

            </div>
        </modal>
        <loading :active.sync="loading"
                 :can-cancel="true"
                 :is-full-page="true"></loading>
    </div>
</template>
<script>
    //import { required } from "vuelidate/lib/validators"
    import Multiselect from 'vue-multiselect'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/vue-loading.css';
    import { required } from "vuelidate/lib/validators"
    export default {
        props: ['show', 'ftpDetail'],
        components: {
            Loading,
            Multiselect,
        },
        data: function () {
            return {

                loading: false,
                systemTypeList: ['Local', 'Live', 'Backup'],
                ftpDetailList: [],
                isEdit: false,
                rander : 0
            }
        },
        validations: {
            ftpDetail: {
                host: {
                    required,
                },
                systemType: {
                    required,
                },
            }
        },
        methods: {

            close: function () {
                this.$emit('close', true);
            },

            SaveLicense: function () {
                var root = this;
                root.$https.post('/NoblePermission/SaveFtpAccountDetail', this.ftpDetail).then(function (response) {
                    if (response.data.isSuccess) {
                        root.$swal({
                            icon: 'success',
                            title: 'Saved Successfully!',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                        root.GetFtpAccount()
                    }

                });
                this.ftpDetail.id = '00000000-0000-0000-0000-000000000000';
                this.ftpDetail.host = '';
                this.ftpDetail.port = '';
                this.ftpDetail.username = '';
                this.ftpDetail.password = '';
                this.ftpDetail.systemType = '';

            },
            GetFtpAccount: function () {
                var root = this;
                root.$https.get('/NoblePermission/GetFtpAccountList?companyId=' + this.ftpDetail.companyId).then(function (response) {
                    if (response.data.isSuccess) {
                        root.ftpDetailList = []
                        response.data.message.forEach(function (x) {
                            root.ftpDetailList.push({
                                id: x.id,
                                host: x.host,
                                port: x.port,
                                username: x.username,
                                password: x.password,
                                systemType: x.systemType,
                            })

                            var index = root.systemTypeList.findIndex(function (y) {
                                return y == x.systemType
                            });
                            root.systemTypeList.splice(index, 1)
                        })
                        this.rander++
                    }

                });



            },
            EditFtpDetail: function (details) {
                this.ftpDetail.id = details.id
                this.ftpDetail.host = details.host
                this.ftpDetail.port = details.port
                this.ftpDetail.username = details.username
                this.ftpDetail.password = details.password
                this.ftpDetail.systemType = details.systemType
                this.isEdit = true;
            },
            ClearFtpDetail: function () {
                this.ftpDetail.id = '00000000-0000-0000-0000-000000000000';
                this.ftpDetail.host = '';
                this.ftpDetail.port = '';
                this.ftpDetail.username = '';
                this.ftpDetail.password = '';
                this.ftpDetail.systemType = '';
                this.isEdit = false
            }
        },


        created: function () {
            this.GetFtpAccount()
        },
    }
</script>