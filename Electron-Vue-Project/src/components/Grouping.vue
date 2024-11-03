<template>
    <div>
        <multiselect v-model="DisplayValue" :options="options" :multiple="false" v-if="disable" disabled  track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
            <p slot="noResult" class="text-danger"> Oops! No Group found.</p>
            <span slot="noResult" class="btn btn-primary " v-on:click="AddGroup('Add')">Create Group</span><br />

        </multiselect>
        <multiselect v-model="DisplayValue" :options="options" v-else :multiple="false"  track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
            <p slot="noResult" class="text-danger"> Oops! No Group found.</p>
            <span slot="noResult" class="btn btn-primary " v-on:click="AddGroup('Add')">Create Group</span><br />

        </multiselect>
        <modal :show="show" v-if="show">

            <div style="margin-bottom:0px" class="card">
                <div class="card-body">
                    <div class="col-lg-12">
                        <div class="tab-content" id="nav-tabContent">
                            <div class="modal-header" >

                                <h5 class="modal-title" id="myModalLabel"> Add Group</h5>

                            </div>
                            <div class="">
                                <div class="card-body ">
                                    <div class="row ">
                                        
                                        <div  class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : $v.group.groupName.$error}">
                                            <label class="text  font-weight-bolder"> Group Name: * </label>
                                            <input class="form-control"  v-model="$v.group.groupName.$model" type="text" />
                                            <span v-if="$v.group.groupName.$error" class="error">
                                                <span v-if="!$v.group.groupName.required"> Group Name is required</span>
                                            </span>
                                        </div>
                                        <div class="form-group has-label col-sm-12 " v-bind:class="{'has-danger' : $v.group.groupType.$error}">
                                            <label class="text  font-weight-bolder"> Group Type*:</label>
                                            <multiselect :options="groupTypeList" v-model="$v.group.groupType.$model" :show-labels="false" placeholder="Select Group Type">
                                            </multiselect>
                                            <span v-if="$v.group.groupType.$error" class="error">
                                                <span v-if="!$v.group.groupType.required"> Group Type is required</span>
                                            </span>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer justify-content-right">
                                    <button type="button" class="btn btn-primary " v-on:click="SaveGroup" v-bind:disabled="$v.group.$invalid"> Save </button>
                                    <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">Close</button>
                                
                            </div>
                            <!--<div v-else>
                                <loading :active.sync="loading" :can-cancel="false" :is-full-page="true"></loading>
                            </div>-->
                        </div>
                    </div>
                </div>
            </div>
        </modal>
    </div>
</template>
<script>
    import Multiselect from 'vue-multiselect'
   // import clickMixin from '@/Mixins/clickMixin'
    //import Loading from 'vue-loading-overlay';
    //import 'vue-loading-overlay/dist/vue-loading.css';
    import { required } from "vuelidate/lib/validators"
    export default {
        name: 'branddropdown',
        props: ["values", "disable"],

        components: {
            Multiselect,
            //Loading
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                options: [],
                value: '',
                show: false,
                type: '',
                group: {
                    id: '00000000-0000-0000-0000-000000000000',
                    groupName: '',
                    groupType: '',
                },
                groupTypeList: ['Basic', 'Advance', 'Premium', 'Customize'],
                render: 0,
                loading: false,
            }
        },
        validations: {
            group: {
                groupName: {
                    required
                },
                groupType: {
                    required
                },
            }
        },
        methods: {
            getData: function () {
                var root = this;
               
                this.$https.get('/NoblePermission/GetNobleGroup').then(function (response) {
                    if (response.data != null) {
                        response.data.forEach(function (result) {
                            root.options.push({
                                id: result.id,
                                name: result.groupName + '-' + result.groupType,
                            })
                        })
                    }
                }).then(function () {
                    root.value = root.options.find(function (x) {
                        return x.id == root.values;
                    })
                    root.$emit('input', root.value.id);
                });
            },
            AddGroup: function (type) {
                debugger;
                this.group = {
                    id: '00000000-0000-0000-0000-000000000000',
                    groupName: '',
                    groupType: '',
                }

                this.show = !this.show;
                this.type = type;
            },
            close: function () {
                this.show = false;
            },
            
            SaveGroup: function () {
                var root = this;
                
                this.$https.post('/NoblePermission/AddNobleGroup', this.group)
                    .then(function (response) {
                        debugger;
                        if (response.data.isSuccess) {
                           

                                
                            root.options.push({
                                id: response.data.nobleGroup.id,
                                name: response.data.nobleGroup.groupName + '-' + response.data.nobleGroup.groupType,
                            });
                           
                            root.values = response.data.nobleGroup.id
                            root.getData()
                            root.$swal({
                                icon: 'success',
                                title: 'Saved Successfully!',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.show = false;
                           
                        }
                        else {
                            root.$swal({
                                title: "Error!",
                                text: response.data.message,
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: 'Something Went Wrong!',
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false);
            }
        },
        computed: {
            DisplayValue: {
                get: function () {
                    if (this.value != "" || this.value != undefined) {
                        return this.value;
                    }
                    return this.values;
                },
                set: function (value) {
                    this.value = value;
                    if (this.value == null) {
                        this.$emit('input', '');
                    }
                    else {
                        this.$emit('input', value.id);
                    }
                    
                }
            }
        },
        mounted: function () {
           
            this.getData();
        },
    }
</script>