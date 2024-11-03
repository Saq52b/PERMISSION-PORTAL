<template>
    <div class="row">
        <div class="col-lg-9 col-sm-9 ml-auto mr-auto">

            <div class="card ml-auto mr-auto mt-5">
                <div class="row m-0">

                    <div class="BorderBottom col-9">
                        <span class=" DayHeading">List Of Companies</span>
                    </div>
                    <div class=" col-3 text-right">
                        <button type="button" class="btn btn-outline-primary" v-on:click="signOut"><img src="SignOut.png" /></button>
                    </div>
                    <!--<div class="modal-header col-9 p-0">

                        <h5 class="modal-title">Companies</h5>

                    </div>
                    <div class="modal-header col-3 p-0 text-right">

                        <button type="button" class="btn btn-outline-primary" v-on:click="signOut"><img src="SignOut.png" /></button>

                    </div>-->
                </div>

                <div class="card-body">
                    <div class="row">
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6">
                            <!--<div class="form-group">
                                <label>Serach</label>
                                <div>
                                    <input type="text" class="form-control search_input" v-model="search" name="search" id="search" placeholder="Search by name, Vat, Reg No" />
                                    <span class="fas fa-search search_icon"></span>
                                </div>
                            </div>-->
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-6 text-right">

                            <a href="javascript:void(0)" class="btn btn-primary " style="margin-top:27px;" v-on:click="gotoWLForm">White Label Form</a>

                            <a href="javascript:void(0)" class="btn btn-primary " style="margin-top:27px;" v-on:click="AddNewGroup"><i class="fa fa-plus"></i>Create Group </a>

                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">

                            <div v-for="(company, index) in companies" :key="index+3">

                                <div class="accordion" role="tablist">
                                    <b-card no-body class="mb-1">
                                        <b-card-header header-tag="header" class="p-1" role="tab">
                                            <table class="table table-striped table-hover table_list_bg" style="margin:0;">
                                                <tbody>
                                                    <tr>
                                                        <td style="width:3%">
                                                            {{index+1}}
                                                        </td>
                                                        <td style="width: 23%; text-align: left">
                                                            <strong>

                                                                <a href="javascript:void(0)" v-b-toggle.accordion-1 v-on:click="makeActiveCollapse(index,company.id)" style="color: #219653">{{company.nameEnglish}}</a>

                                                            </strong>
                                                            <div>
                                                                <strong>
                                                                    {{company.nameArabic}}
                                                                </strong>

                                                            </div>
                                                        </td>

                                                        <td style="width: 23%; text-align: right">

                                                            CR({{company.companyRegNo}})
                                                            <div>
                                                                Vat({{company.vatRegistrationNo}})
                                                            </div>
                                                        </td>

                                                        <td style="width: 15%; text-align: left;padding-left:8px;">
                                                            Ph({{company.phoneNo}})
                                                        </td>
                                                        <td style="width: 15%; text-align: left">
                                                            Client NO({{company.clientNo}})
                                                        </td>
                                                        <td style="width: 20%; text-align: right">
                                                            <button type="button" class="btn btn-primary " style="background-color: #219653; border-color: #219653" v-on:click="DisActiveLicense(company.id, true)"> Activation </button>
                                                        </td>

                                                    </tr>
                                                </tbody>
                                            </table>
                                            <!--<a href="javascript:void(0)" v-b-toggle.accordion-1 v-on:click="makeActiveCollapse(index)">{{company.nameEnglish}}</a>-->
                                        </b-card-header>
                                        <b-collapse id="accordion-1" accordion="my-accordion" role="tabpanel" v-if="index==collpase">
                                            <b-card-body style="padding-right:0 !important">
                                                <div v-for="(business, busIndex) in businesses" :key="busIndex+3">
                                                    <div class="accordionchild" role="tablist">
                                                        <b-card no-body class="mb-1">
                                                            <b-card-header header-tag="header" class="p-1" role="tab">
                                                                <table class="table table-striped table-hover table_list_bg" style="margin:0;">
                                                                    <tbody>
                                                                        <tr>
                                                                            <td style="width:4%">
                                                                                {{(index + 1) + '.' + (busIndex+1)}}
                                                                            </td>
                                                                            <td style="width:25%; text-align:left">
                                                                                <strong>

                                                                                    <a href="javascript:void(0)" v-b-toggle.accordion-11 v-on:click="makeActiveBusCollapse(busIndex, business.id)" style="color: darkseagreen">{{business.nameEnglish}}</a>
                                                                                </strong>
                                                                                <div>
                                                                                    <strong>
                                                                                        {{business.nameArabic}}
                                                                                    </strong>
                                                                                </div>
                                                                            </td>

                                                                            <td style="width: 30%; text-align: left">
                                                                                {{business.categoryInEnglish}}
                                                                                <div>
                                                                                    {{business.categoryInArabic}}
                                                                                </div>


                                                                            </td>

                                                                            <td style="width: 25%; text-align: left">
                                                                                {{business.addressEnglish}}
                                                                                <div>
                                                                                    {{business.addressArabic}}
                                                                                </div>
                                                                            </td>
                                                                            <td style="width: 20%; text-align: right">
                                                                                <button type="button" class="btn btn-primary " style="background-color: darkseagreen; border-color: darkseagreen " v-on:click="DisActiveLicense(business.id, false)"> Activation </button>
                                                                            </td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </b-card-header>
                                                            <b-collapse id="accordion-11" accordion="my-accordionchild" role="tabpanel" v-if="busIndex==busCollapse">
                                                                <b-card-body style="padding-right:0 !important">
                                                                    <div v-for="(location, locIndex) in locations" :key="locIndex+3">
                                                                        <div class="accordionInnerchild" role="tablist">
                                                                            <b-card no-body class="mb-1">
                                                                                <b-card-header header-tag="header" class="p-1" role="tab">
                                                                                    <table class="table table-striped table-hover table_list_bg" style="margin:0;">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <td style="width: 5%">
                                                                                                    {{(index + 1) + '.' + (busIndex+1) + '.' + (locIndex+1)}}
                                                                                                </td>
                                                                                                <td v-if="location.nobleGroupId != null" style="width: 20%; text-align: left">
                                                                                                    <strong>

                                                                                                        <a href="javascript:void(0)" v-on:click="EditCompanyGroup(location.id)">{{location.nameEnglish}}</a>
                                                                                                    </strong>
                                                                                                    <div>
                                                                                                        <strong>
                                                                                                            {{location.nameArabic}}
                                                                                                        </strong>
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td v-else style="width: 20%; text-align: left">
                                                                                                    <strong>

                                                                                                        {{location.nameEnglish}}
                                                                                                    </strong>
                                                                                                    <div>
                                                                                                        <strong>
                                                                                                            {{location.nameArabic}}
                                                                                                        </strong>
                                                                                                    </div>
                                                                                                </td>

                                                                                                <td class="text-center" style="width: 10%; text-align: left">
                                                                                                    Ph({{location.phoneNo}})
                                                                                                </td>
                                                                                                <td class="text-center" style="width: 35%; text-align: left">
                                                                                                    {{location.addressEnglish}}
                                                                                                    <div>
                                                                                                        {{location.addressArabic}}
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td class="text-center" style="width: 10%; text-align: left">
                                                                                                    {{location.groupName}}
                                                                                                    <div>
                                                                                                        {{location.licenseType}}
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td class="text-center" style="width: 10%; text-align: left" v-if="location.technicalSupportPeriod != 'UnLimited' && !location.isTechnicalSupport">
                                                                                                    {{location.endDate}}
                                                                                                    <div>
                                                                                                        (End Date)
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td class="text-center" style="width: 10%; text-align: left" v-else-if="location.technicalSupportPeriod != 'UnLimited' && location.isTechnicalSupport">
                                                                                                    {{location.endDate}}
                                                                                                    <div>
                                                                                                        (Technical End Date)
                                                                                                    </div>
                                                                                                </td>
                                                                                                <td class="text-center" style="width: 10%; text-align: left" v-else>
                                                                                                    -
                                                                                                </td>

                                                                                                <td class="text-center dropdown" style="width: 10%">
                                                                                                    <button class="dropdown-toggle btn btn-primary  btn-block" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                                                                        Actions
                                                                                                    </button>
                                                                                                    <div class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenuButton">
                                                                                                        <a class="dropdown-item" href="javascript:void(0);" v-if="location.nobleGroupId != null" v-on:click="UpdateLicensing(location.id)">Update License</a>
                                                                                                        <a class="dropdown-item" href="javascript:void(0);" v-else v-on:click="AddLicensing(location.id)">Add License</a>
                                                                                                        <a class="dropdown-item" href="javascript:void(0);" v-on:click="showLicenceHistory(location.nameEnglish, location.companyLicenseLookUps)">License History</a>


                                                                                                        <a class="dropdown-item" href="javascript:void(0);" v-on:click="AddUpdatePaymentLimit(location.id)">Payment Limit</a>
                                                                                                        <a class="dropdown-item" href="javascript:void(0);" v-on:click="AddUpdateFtpDetail(location.id)">Add Ftp Detail</a>
                                                                                                    </div>

                                                                                                </td>

                                                                                            </tr>
                                                                                        </tbody>
                                                                                    </table>
                                                                                    <!--<b-button block v-b-toggle.accordion-111 variant="primary" v-on:click="makeActiveLocCollapse(locIndex)">{{location}}</b-button>-->
                                                                                </b-card-header>

                                                                            </b-card>

                                                                        </div>
                                                                    </div>

                                                                </b-card-body>
                                                            </b-collapse>
                                                        </b-card>

                                                    </div>
                                                </div>


                                            </b-card-body>
                                        </b-collapse>
                                    </b-card>

                                </div>

                            </div>



                        </div>
                    </div>

                </div>
            </div>
        </div>
        <licensing :license="newLicense"
                   :show="show"
                   v-if="show"
                   @close="IsSave"
                   :type="type" />

        <login-history-model :show="showHistory"
                             :companyName="companyName"
                             :companyLicenceList="companyLicenceList"
                             v-if="showHistory"
                             @close="showHistory = false">

        </login-history-model>
        <payment-limit-model :show="showPaymentLimit"
                             :paymentLimit="newPaymentLimit"
                             v-if="showPaymentLimit"
                             @close="showPaymentLimit = false">

        </payment-limit-model>
        <ftp-account-detail :show="showFtpDetail"
                            :ftpDetail="newFtpDetail"
                            v-if="showFtpDetail"
                            @close="showFtpDetail = false">

        </ftp-account-detail>

    </div>
</template>


<script>
    export default {
        data: function () {
            return {
                searchQuery: '',
                show: false,
                companyList: [],
                type: '',
                search: '',
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                companyLicenceList: [],
                newPaymentLimit: {
                    id: '00000000-0000-0000-0000-000000000000',
                    fromDate: '',
                    toDate: '',
                    message: '',
                    isActive: '',
                    companyId: '00000000-0000-0000-0000-000000000000'
                },
                newFtpDetail: {
                    id: '00000000-0000-0000-0000-000000000000',
                    host: '',
                    port: '',
                    username: '',
                    password: '',
                    systemType: '',
                    companyId: '00000000-0000-0000-0000-000000000000'
                },
                showHistory: false,
                showPaymentLimit: false,
                showFtpDetail: false,
                newLicense: {
                    nobleGroupId: '',
                    fromDate: '',
                    toDate: '',
                    isActive: false,
                    isBlock: false,
                    companyId: '',
                    licenseType: '',
                    gracePeriod: false,
                    paymentFrequency: '',
                    isTechnicalSupport: false,
                    isUpdateTechnicalSupport: false,
                    technicalSupportPeriod: '',
                    activationPlatform: ''
                },
                companies: [],
                businesses: [],
                locations: [],
                collpase: '',
                busCollapse: '',
            }
        },
        watch: {
            search: function (val) {
                this.GetCompanyData(val, 1);
            }
        },
        methods: {
            DisActiveLicense: function (companyId, isCompany) {
                var root = this;
                this.$https.get('/NoblePermission/DisActiveLicense?companyId=' + companyId + '&isCompany=' + isCompany).then(function (response) {
                    if (response.data.isSuccess) {
                        root.$swal({
                            icon: 'success',
                            title: 'Saved Successfully!',
                            showConfirmButton: false,
                            timer: 1500,
                            timerProgressBar: true,
                        });
                        root.$router.push('/company');


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

                }).catch(error => {
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

                });
            },
            makeActiveCollapse: function (item, companyId) {

                this.collpase = item;
                this.businesses = []
                var root = this;
                this.companyList.businesses.forEach(function (x) {

                    if (x.clientParentId === companyId) {
                        root.businesses.push(x)
                    }
                })
                //this.businesses.push(.find(x => x.clientParentId == companyId))
            },
            makeActiveBusCollapse: function (item, busId) {
                this.busCollapse = item;
                this.locations = []
                var root = this;
                this.companyList.locations.forEach(function (x) {

                    if (x.businessParentId === busId) {
                        root.locations.push(x)
                    }
                })
            },
            signOut: function () {
                localStorage.setItem('CanLogin', false)
                this.$router.push('/')
            },
            EditCompanyGroup: function (companyId) {
                var root = this;
                this.$https.get('/NoblePermission/GetCompanyPermissionById?id=' + companyId).then(function (response) {
                    if (response.data != null) {
                        //this.$router.push({ path: '/permission', query: { data: response.data.result, type: 'Edit' }});
                        root.$router.push({
                            path: '/permission',
                            query: {
                                data: response.data.result,
                                type: 'Edit'
                            }
                        })
                    }

                }).catch(error => {
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

                });
            },
            AddLicensing: function (companyId) {
                this.newLicense = {
                    nobleGroupId: '00000000-0000-0000-0000-000000000000',
                    fromDate: '',
                    toDate: '',
                    isActive: false,
                    isBlock: false,
                    companyId: companyId,
                    licenseType: '',
                    gracePeriod: false,
                    paymentFrequency: '',
                    isTechnicalSupport: false,
                    isUpdateTechnicalSupport: false,
                    technicalSupportPeriod: '',
                    activationPlatform: ''
                }
                this.show = !this.show;
                this.type = "Add";
            },
            UpdateLicensing: function (companyId) {
                var root = this;
                root.$https.get('/NoblePermission/GetLicenseDetail?companyId=' + companyId).then(function (response) {
                    if (response.data.isSuccess) {
                        root.newLicense.nobleGroupId = response.data.message.nobleGroupId
                        root.newLicense.fromDate = response.data.message.fromDate
                        root.newLicense.toDate = response.data.message.toDate
                        root.newLicense.isActive = response.data.message.isActive
                        root.newLicense.isBlock = response.data.message.isBlock
                        root.newLicense.companyId = response.data.message.companyId
                        root.newLicense.licenseType = response.data.message.licenseType
                        root.newLicense.gracePeriod = response.data.message.gracePeriod
                        root.newLicense.paymentFrequency = response.data.message.paymentFrequency
                        root.newLicense.isTechnicalSupport = response.data.message.isTechnicalSupport
                        root.newLicense.isUpdateTechnicalSupport = response.data.message.isUpdateTechnicalSupport
                        root.newLicense.technicalSupportPeriod = response.data.message.technicalSupportPeriod,
                            root.newLicense.activationPlatform = response.data.message.activationPlatform,
                            root.show = !root.show;
                        root.type = "Edit";
                    }

                });

            },
            AddUpdatePaymentLimit: function (companyId) {
                var root = this;
                root.$https.get('/NoblePermission/GetLastPaymentLimit?companyId=' + companyId).then(function (response) {
                    if (response.data.isSuccess) {
                        root.newPaymentLimit.id = response.data.message.id
                        root.newPaymentLimit.fromDate = response.data.message.fromDate
                        root.newPaymentLimit.toDate = response.data.message.toDate
                        root.newPaymentLimit.isActive = response.data.message.isActive
                        root.newPaymentLimit.message = response.data.message.message
                        root.newPaymentLimit.companyId = companyId

                        root.showPaymentLimit = !root.showPaymentLimit;

                    }

                });

            },
            AddUpdateFtpDetail: function (companyId) {
                var root = this;
                //root.$https.get('/NoblePermission/GetLastPaymentLimit?companyId=' + companyId).then(function (response) {
                //    if (response.data.isSuccess) {
                //        root.newFtpDetail.id = response.data.message.id
                //        root.newFtpDetail.host = response.data.message.host
                //        root.newFtpDetail.port = response.data.message.port
                //        root.newFtpDetail.username = response.data.message.username
                //        root.newFtpDetail.password = response.data.message.password
                //        root.newFtpDetail.systemType = response.data.message.systemType
                //        root.newFtpDetail.companyId = companyId

                //        root.showFtpDetail = !root.showFtpDetail;

                //    }

                //});
                root.newFtpDetail.companyId = companyId;
                root.showFtpDetail = !root.showFtpDetail;

            },
            AddNewGroup: function () {
                this.$router.push('/permission')
            },
            IsSave: function () {

                this.show = false;

                this.$router.go('company')
            },
            getPage: function () {
                this.GetCompanyData(this.search, this.currentPage);
            },

            GetCompanyData: function () {
                var root = this;
                root.$https.get('/NoblePermission/GetCompanyList?pageNumber=' + this.currentPage + '&searchTerm=' + this.search).then(function (response) {
                    if (response.data != null) {

                        console.log(response.data.message)
                        root.companies = response.data.message.companies
                        root.companyList = response.data.message
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },
            showLicenceHistory: function (name, licenceList) {

                this.companyLicenceList = [];
                this.companyName = name;
                this.showHistory = !this.showHistory;
                this.companyLicenceList = licenceList;
            },
            gotoWLForm() {
                var root = this;
                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                root.$https.get('/NoblePermission/GetFormData', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    var data = ''
                    if (response.data != null) {

                        data = {
                            id: response.data.id,
                            heading: response.data.heading,
                            description: response.data.description,
                            addressLine1: response.data.addressLine1,
                            addressLine2: response.data.addressLine2,
                            addressLine3: response.data.addressLine3,
                            backgroundImageName: response.data.backgroundImageName,
                            backgroundImagePath: response.data.backgroundImagePath,
                            companyName: response.data.companyName,
                            applicationName: response.data.applicationName,
                            loginLogoName: response.data.loginLogoName,
                            loginLogoPath: response.data.loginLogoPath,
                            loginScreenImageName: response.data.loginScreenImageName,
                            loginScreenImagePath: response.data.loginScreenImagePath,
                            sidebarImageName: response.data.sidebarImageName,
                            sidebarImagePath: response.data.sidebarImagePath,
                            tagImage1Name: response.data.tagImage1Name,
                            tagImage1Path: response.data.tagImage1Path,
                            tagImage2Name: response.data.tagImage2Name,
                            tagImage2Path: response.data.tagImage2Path,
                            favName: response.data.favName,
                            favIconName: response.data.favIconName,
                            favIconPath: response.data.favIconPath,
                            email: response.data.email,
                            sideMenuColor: response.data.sideMenuColor,
                            sideMenuBtnColor: response.data.sideMenuBtnColor,
                            sideMenuBtnClickColor: response.data.sideMenuBtnClickColor,
                            saveBtnBgColor: response.data.saveBtnBgColor,
                            saveBtnColor: response.data.saveBtnColor,
                            cancelBgBtnColor: response.data.cancelBgBtnColor,
                            cancelBtnColor: response.data.cancelBtnColor,
                            headingColor: response.data.headingColor,
                            tableHeaderBgColor: response.data.tableHeaderBgColor,
                            tableHeaderColor: response.data.tableHeaderColor,
                            invoiceTitleBgColor: response.data.invoiceTitleBgColor,
                            invoiceTitleColor: response.data.invoiceTitleColor,

                            applicationBgColor: response.data.applicationBgColor,
                            applicationTextColor: response.data.applicationTextColor,
                            cardBgColor: response.data.cardBgColor,
                            cardTextColor: response.data.cardTextColor,
                            setupBgColor: response.data.setupBgColor,
                            setupTextColor: response.data.setupTextColor,
                            setupCssFilter: response.data.setupCssFilter,
                            sideMenuBtnColorFilter: response.data.sideMenuBtnColorFilter,
                            sideMenuBtnClickColorFilter: response.data.sideMenuBtnClickColorFilter,
                            sideMenuBtnClickBgColor: response.data.sideMenuBtnClickBgColor,

                        }


                    }
                    root.$router.push({
                        path: '/LoginForm',
                        query: { data: data }
                    })
                });
            }

        },
        created: function () {
            this.$emit('input', this.$route.name);
        },
        mounted: function () {
            this.GetCompanyData(this.search, 1);

        }
    }
</script>