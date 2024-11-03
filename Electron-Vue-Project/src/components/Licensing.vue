<template>
    <div>
        <modal :show="show" v-if="show">

            <div style="margin-bottom:0px" class="card">
                <div class="card-body">
                    <div class="col-lg-12">
                        <div class="tab-content" id="nav-tabContent">
                            <div class="modal-header">

                                <h5 class="modal-title" id="myModalLabel"> Licensing </h5>

                            </div>
                            <div class="">
                                <div class="card-body ">
                                    <div class="row ">

                                        <div class="form-group has-label col-sm-12 ">
                                            <label class="text  font-weight-bolder"> Group Name :</label>
                                            <grouping v-model="license.nobleGroupId" :values="license.nobleGroupId"></grouping>
                                        </div>
                                        <div class="form-group has-label col-sm-12 ">
                                            <label class="text  font-weight-bolder"> License Type:</label>
                                            <multiselect :options="licenseTypeList" v-model="onLicenseSelect" :show-labels="false" placeholder="Select License Type">
                                            </multiselect>

                                        </div>
                                        <div class="form-group has-label col-sm-12 ">
                                            <label class="text  font-weight-bolder"> Activation Platform:</label>
                                            <multiselect :options="platformList" v-model="license.activationPlatform" :show-labels="false" placeholder="Select License Type">
                                            </multiselect>

                                        </div>

                                        <div class="col-sm-12" v-if="license.licenseType != '' && !license.isActive">
                                            <label>Grace Period: </label><span style="margin-left:5px;"><toggle-button v-model="license.gracePeriod" v-on:change="SetDate" :key="render" color="#3178F6" /></span>
                                        </div>

                                        <!--<div class="col-sm-6" v-if="license.gracePeriod && !license.isActive">
        <label>From Date</label>
        <div>
            <datepicker v-model="license.fromDate"></datepicker>
        </div>
    </div>-->
                                        <div class="col-sm-12" v-if="license.gracePeriod && !license.isActive">
                                            <label>To Date</label>
                                            <div>
                                                <datepicker v-model="license.toDate"></datepicker>
                                            </div>
                                        </div>


                                        <div class="form-group col-md-12" v-if="license.licenseType === 'Subscription' || license.licenseType === 'Unlimited'">
                                            <label style="margin: 7px;text-align:right">Active: </label>
                                            <span style="margin-left:30px;">
                                                <toggle-button v-model="license.isActive" v-on:change="SetDate" color="#3178F6" />
                                            </span>
                                        </div>
                                        <div  class="col-sm-12"  v-if="license.licenseType === 'Subscription' && license.isActive">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <label>From Date</label>
                                                    <div>
                                                        <datepicker v-model="license.fromDate"></datepicker>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6" >
                                                    <label>To Date</label>
                                                    <div>
                                                        <datepicker v-model="license.toDate"></datepicker>
                                                    </div>
                                                </div>
                                                <div class="form-group has-label col-sm-12 " >
                                                    <label class="text  font-weight-bolder"> Payment Frequency:</label>
                                                    <multiselect :options="paymentFrequencyList" v-model="license.paymentFrequency" :show-labels="false" placeholder="Select License Type">
                                                    </multiselect>

                                                </div>
                                            </div>
                                        </div>
                                        <div  class="col-sm-12"  v-if="license.licenseType === 'Unlimited' && license.isActive">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <label>Technical Support: </label>
                                                    <div>
                                                        <toggle-button v-model="license.isTechnicalSupport" :key="supportRender" v-on:change="SetTechnicalSupport(true)" color="#3178F6" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <label>Technical Support With Update: </label>
                                                    <div>
                                                        <toggle-button v-model="license.isUpdateTechnicalSupport" :key="supportRender" v-on:change="SetTechnicalSupport(false)" color="#3178F6" />
                                                    </div>
                                                </div>
                                                <div class="form-group has-label col-sm-12 " v-if="license.isTechnicalSupport || license.isUpdateTechnicalSupport">
                                                    <label class="text  font-weight-bolder"> Technical Support Period:</label>
                                                    <multiselect :options="technicalSupportPeriodList" v-model="license.technicalSupportPeriod" :show-labels="false" placeholder="Select License Type">
                                                    </multiselect>

                                                </div>
                                                <div class="col-sm-6" v-if="license.technicalSupportPeriod === 'Limited' || license.technicalSupportPeriod === 'Free'">
                                                    <label>From Date</label>
                                                    <div>
                                                        <datepicker v-model="license.fromDate"></datepicker>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6"  v-if="license.technicalSupportPeriod === 'Limited' || license.technicalSupportPeriod === 'Free'">
                                                    <label>To Date</label>
                                                    <div>
                                                        <datepicker v-model="license.toDate"></datepicker>
                                                    </div>
                                                </div>
                                                <div class="form-group has-label col-sm-12 " v-if="license.technicalSupportPeriod === 'Limited' || license.technicalSupportPeriod === 'Free'">
                                                    <label class="text  font-weight-bolder"> Payment Frequency:</label>
                                                    <multiselect :options="paymentFrequencyList" v-model="license.paymentFrequency" :show-labels="false" placeholder="Select License Type">
                                                    </multiselect>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer justify-content-right">
                                <button type="button" class="btn btn-primary " v-on:click="SaveLicense"> Save </button>
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
        <loading :active.sync="loading"
                 :can-cancel="true"
                 :is-full-page="true"></loading>
    </div>
</template>
<script>
    //import { required } from "vuelidate/lib/validators"
    import moment from "moment";
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/vue-loading.css';
    import Multiselect from 'vue-multiselect'
    export default {
        props: ['show', 'license', 'type'],
        components: {
            Loading,
            Multiselect
        },
        data: function () {
            return {
                options: [],
                value: '',
                licenseTypeList: ['Demo', 'Subscription', 'Unlimited'],
                paymentFrequencyList: ['3 Month', '6 Month', '12 Month'],
                technicalSupportPeriodList: ['Limited', 'UnLimited', 'Free'],
                platformList: ['Offline', 'Online', 'Both'],
                render: 0,
                supportRender: 0,
                loading: false,

            }
        },
        validations: {
            //group: {
            //    groupName: {
            //        required
            //    },
            //    groupType: {
            //        required
            //    },
            //}
        },
        computed: {
            
            onLicenseSelect: {
                get: function () {

                    return this.license.licenseType;
                },
                set: function (modifiedValue) {

                    if (modifiedValue != null) {
                        this.license.licenseType = modifiedValue;
                        if (modifiedValue === 'Demo') {
                            this.license.gracePeriod = true;
                            this.license.isActive = false;
                            this.license.gracePeriod = true;
                            this.render++
                        }
                        else {
                            this.license.gracePeriod = false;
                            this.license.isActive = false;
                            this.license.gracePeriod = false;
                            this.render++
                        }
                        
                    }
                    else {
                        this.license.licenseType = '';
                        this.license.gracePeriod = false;
                        this.license.paymentFrequency = '';
                        this.license.isActive = false;
                        this.render++
                    }
                    this.license.fromDate = moment().format('llll');
                    this.license.toDate = moment().format('llll');
                    this.license.isUpdateTechnicalSupport = false;
                    this.license.isTechnicalSupport = false;

                    this.license.paymentFrequency = '';
                    this.license.technicalSupportPeriod = '';
                    this.supportRender++
                }
            }

        },
        methods: {
            SetTechnicalSupport: function (isTechnicalSupport) {
                if (isTechnicalSupport) {
                    this.license.isUpdateTechnicalSupport = false
                    this.supportRender++
                }
                else {
                    this.license.isTechnicalSupport = false
                    this.supportRender++
                }
            },
            SetDate: function () {
                //if (!isGracePeriod) {
                //    this.license.gracePeriod = this.license.licenseType === 'Demo'? true:false;
                //    this.license.paymentFrequency = '';
                //    this.render++
                //}
                this.license.fromDate = moment().format('llll');
                this.license.toDate = moment().format('llll');
            },
            
            close: function () {
                this.$emit('close', true);
            },

            SaveLicense: function () {
                var root = this;
                root.loading = true
                this.license.fromDate = moment(this.license.fromDate).format();
                this.license.toDate = moment(this.license.toDate).format();

                this.$https.post('/NoblePermission/CompanyLicensing', this.license)
                    .then(function (response) {
                        if (response.data.isSuccess) {


                            root.$swal({
                                icon: 'success',
                                title: 'Saved Successfully!',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                            root.close()

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
                        root.loading = false
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


        created: function () {
            if (this.type === 'Add') {
                this.license.fromDate = moment().format('llll');
                this.license.toDate = moment().format('llll');
            }
            else {
                this.license.fromDate = moment(this.license.fromDate).format('llll');
                this.license.toDate = moment(this.license.toDate).format('llll');
            }
            this.supportRender++;
        },
    }
</script>