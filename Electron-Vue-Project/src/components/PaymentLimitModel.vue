<template>
    <div>
        <modal :show="show" v-if="show">

            <div style="margin-bottom:0px" class="card">
                <div class="card-body">
                    <div class="col-lg-12">
                        <div class="tab-content" id="nav-tabContent">
                            <div class="modal-header">

                                <h5 class="modal-title" id="myModalLabel"> Payment Limits </h5>

                            </div>
                            <div class="">
                                <div class="card-body ">
                                    <div class="row ">



                                        <div class="col-sm-12">
                                            <label>From Date</label>
                                            <div>
                                                <datepicker v-model="paymentLimit.fromDate"></datepicker>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <label>To Date</label>
                                            <div>
                                                <datepicker v-model="paymentLimit.toDate"></datepicker>
                                            </div>
                                        </div>
                                        <div class="form-group has-label col-sm-12 ">
                                            <label class="text  font-weight-bolder"> Message </label>
                                            <input class="form-control" v-model="paymentLimit.message" type="text" />

                                        </div>
                                        <div class="col-sm-6">
                                            <label>Active: </label>
                                            <div>
                                                <toggle-button v-model="paymentLimit.isActive" color="#3178F6" />
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                           
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer justify-content-right">
                                <button type="button" class="btn btn-primary " v-on:click="SaveLicense"> Save </button>
                                <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">Close</button>

                            </div>
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
    export default {
        props: ['show', 'paymentLimit'],
        components: {
            Loading,
        },
        data: function () {
            return {
               
                loading: false,

            }
        },
        validations: {
            
        },
        methods: {
           
            close: function () {
                this.$emit('close', true);
            },

            SaveLicense: function () {
                var root = this;
                root.loading = true
                this.paymentLimit.fromDate = moment(this.paymentLimit.fromDate).format();
                this.paymentLimit.toDate = moment(this.paymentLimit.toDate).format();

                this.$https.post('/NoblePermission/AddUpdatePaymentLimit', this.paymentLimit)
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
            debugger;
            if (this.paymentLimit.id === '00000000-0000-0000-0000-000000000000') {
                this.paymentLimit.fromDate = moment().format('llll');
                this.paymentLimit.toDate = moment().format('llll');
            }
            else {
                this.paymentLimit.fromDate = moment(this.paymentLimit.fromDate).format('llll');
                this.paymentLimit.toDate = moment(this.paymentLimit.toDate).format('llll');
            }
        },
    }
</script>