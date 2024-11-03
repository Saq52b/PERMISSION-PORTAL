<template>
    <modal :show="show" :modalLarge="true">
        <div class="modal-lg">
            <div style="margin-bottom:0px" class="card">
                <div class="card-header">
                    <h5>License History: ({{companyName}})</h5>
                </div>
                <div class="card-body ">
                    <div class=" table-responsive">
                        <table class="table ">
                            <thead class="m-0">
                                <tr>
                                    <th>#</th>
                                    <th>License Type</th>
                                    <th>From date</th>
                                    <th>To Date</th>
                                    <th>Technical Support</th>
                                    <th>From date (TS)</th>
                                    <th>To Date (TS)</th>
                                    <th>Active </th>
                                    <th>Block</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr v-for="(company,index) in companyLicenceList" v-bind:key="index">
                                    <td>
                                        {{index+1}}
                                    </td>
                                    <td>{{company.licenseType}}</td>
                                    <td v-if="!company.isTechnicalSupport && !company.isUpdateTechnicalSupport">{{getDate(company.fromDate)}}</td>
                                    <td v-else>-</td>
                                    <td v-if="!company.isTechnicalSupport && !company.isUpdateTechnicalSupport">{{getDate(company.toDate)}}</td>
                                    <td v-else>-</td>
                                    <td v-if="company.isTechnicalSupport || company.isUpdateTechnicalSupport">True</td>
                                    <td v-else>False</td>
                                    <td v-if="company.isTechnicalSupport || company.isUpdateTechnicalSupport">{{getDate(company.fromDate)}}</td>
                                    <td v-else>-</td>
                                    <td v-if="company.isTechnicalSupport || company.isUpdateTechnicalSupport">{{getDate(company.toDate)}}</td>
                                    <td v-else>-</td>

                                    <td>{{company.isActive}}</td>
                                    <td>{{company.isBlock}}</td>
                                </tr>
                            </tbody>
                        </table>
                        <div class="modal-footer justify-content-right">
                            <button type="button" class="btn btn-secondary  mr-3 " v-on:click="$emit('close')">Close</button>

                        </div>
                    </div>
                </div>
            </div>
        </div>


    </modal>
</template>
<script>
    import moment from "moment";

    export default {
        props: ['show', 'companyLicenceList', 'companyName'],
        data: function () {
            return {
                render: 0,
                types: ['Trail', 'Base', 'Standard', 'Advanced']
            }
        },

        methods: {
            getDate: function (date) {
                return moment(date).format('DD/MM/YYYY');
            },
            close: function () {
                this.$emit('close');
            },
        },

        mounted: function () {
            this.render++;

        }
    }
</script>
