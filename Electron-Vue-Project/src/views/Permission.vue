<template>
    <div style="margin-bottom:0px" class="card">
        <div class="card-body">
            <div class="col-lg-12">
                <div class="tab-content" id="nav-tabContent">
                    <div class="row">
                        <div class="modal-header col-9 p-0">

                            <h5 class="modal-title"> Noble Permissions</h5>

                        </div>
                        <div class="modal-header col-3 p-0 text-right">

                            <button type="button" class="btn btn-outline-primary" v-on:click="signOut"><img src="SignOut.png" /></button>

                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row ">
                            <div class="form-group  col-6 " v-bind:class="{'has-danger' : $v.noblePermission.groupId.$error}">
                                <label class="text  font-weight-bolder"> Select and Create Group *:</label>
                                <grouping v-model="noblePermission.groupId" v-if="isEdit" :values="noblePermission.groupId" v-bind:disable="true"></grouping>
                                <grouping v-model="noblePermission.groupId" v-on:input="GetSelectedtGroupData" v-else></grouping>

                                <span v-if="$v.noblePermission.groupId.$error" class="error">
                                    <span v-if="!$v.noblePermission.groupId.required"> Group Name is required</span>
                                </span>


                            </div>

                            <div class="col-12 ">
                                <h5 style="border-bottom:1px red solid">Modules</h5>
                            </div>
                            <div class="form-group has-label col-12 ">
                                <div class="row">
                                    <div class="col-3" v-for="module of noblePermission.modules" :key='module.name + 3'>
                                        <h6 class="btn btn-block" type="button" style="background-color: #219653 !important " v-on:click="ShowOptions(module)" v-if="noblePermission.permissions.filter(x => x.checked===true && x.moduleId === module.id).length <= 0">
                                            {{module.name}}
                                        </h6>
                                        <h6 class="btn btn-block" style="background-color: #3178F6 !important" type="button" v-on:click="ShowOptions(module)" v-else>
                                            {{module.name}}
                                        </h6>
                                    </div>
                                </div>



                            </div>

                            <div class="col-12">
                                <div class="row" v-if="show">
                                    <div class="col-12 ">
                                        <h2 style="margin-bottom:5px">{{moduleName}} Module</h2>
                                    </div>
                                    <div class="col-12 ">
                                        <h5 style="border-bottom:1px red solid">
                                            <input type="checkbox" v-model="isChecked" v-on:change="onClickCheckBox(moduleId)" /><span style="margin-left:5px;">Select All Permission Of {{moduleName}} Module</span>
                                        </h5>
                                    </div>
                                    <template v-for="perType of permissionType">
                                        <div class="col-12" v-if="perType.moduleId === moduleId" :key='perType.value + 3'>

                                            <h4 style="margin-bottom:4px;margin-top:2px;">
                                                {{perType.value}}:
                                            </h4>
                                            <div class="row">
                                                <template v-for="per of noblePermission.permissions">
                                                    <div class="col-3" v-if="per.moduleId === moduleId && per.typeId === perType.id" :key='per.value + 3'>

                                                        <span>
                                                            <input type="checkbox" v-model="per.checked" v-on:change="updateAllCheckStatuc(moduleId, per)" /><span style="margin-left:5px;">{{per.permissionName}}</span>
                                                        </span>
                                                        <span v-if="per.key === '7dc50e60-d5a2-419a-b12a-200ac71d7cb6' && per.checked === true">
                                                            <input class="form-control" v-model="customerLimit" type="number" @change="onLimitedCustomer(per)" @focus="$event.target.select()" />

                                                        </span>
                                                        <span v-else-if=" per.key === '3d1f65f1-3f72-4898-a175-1b6ab42b2b9d'&& per.checked === true">
                                                            <input class="form-control" v-model="supplierLimit" type="number" @change="onLimitedCustomer(per)" @focus="$event.target.select()" />

                                                        </span>
                                                        <span v-else-if=" per.key === '8cb9768b-f76c-4614-a8a8-c22c7f1a0c81'&& per.checked === true">
                                                            <input class="form-control" v-model="openBatch" type="number" @change="onLimitedCustomer(per)" @focus="$event.target.select()" />

                                                        </span>
                                                        <span v-else-if=" per.key === '7a6930e8-876c-4344-b14e-80b961c14f96'&& per.checked === true">
                                                            <input class="form-control" v-model="attachmentLimit" type="number" @change="onLimitedCustomer(per)" @focus="$event.target.select()" />
                                                        </span>
                                                        <span v-else-if=" per.key === '671b5fb5-94d5-4edd-89fa-58cb08da4783'&& per.checked === true">
                                                            <input class="form-control" v-model="attachmentSize" type="number" @change="onLimitedCustomer(per)" @focus="$event.target.select()" />
                                                        </span>
                                                    </div>
                                                </template>

                                            </div>
                                            <hr style="margin-bottom:4px;margin-top:2px;background-color:black"/>
                                        </div>
                                    </template>

                                </div>
                            </div>

                        </div>
                        <div class="modal-footer justify-content-right">
                            <button type="button" class="btn btn-primary  " v-bind:disabled="$v.noblePermission.$invalid || noblePermission.permissions.filter(x => x.checked==true).length <= 0" v-if="isEdit" v-on:click="SaveCompanyPermissions"> Save </button>
                            <button type="button" class="btn btn-primary  " v-bind:disabled="$v.noblePermission.$invalid || noblePermission.permissions.filter(x => x.checked==true).length <= 0" v-else v-on:click="SavePermissions"> Save </button>
                            <button type="button" class="btn btn-danger  mr-3 " v-on:click="onCancel">Cancel</button>
                        </div>
                    </div>


                </div>
            </div>
        </div>
        <loading :active.sync="loading"
                 :can-cancel="true"
                 :is-full-page="true"></loading>
    </div>
</template>

<script>
    // @ is an alias to /src
    //import NoblePermission from '@/components/NoblePermission.vue'
    
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/vue-loading.css';
    import modules from '@/enums/modules'
    import permissions from '@/enums/permissions'
    import permissionType from '@/enums/permissiontype'
    import { required } from "vuelidate/lib/validators"
    export default {
        name: 'Home',
        components: {
            Loading
        },
        data() {
            return {
                loading:false,
                rander:0,
                customerLimit:0,
                attachmentLimit:0,
                attachmentSize:0,
                supplierLimit:0,
                openBatch:1,
                noblePermission: {
                    modules: [],
                    permissions: [],
                    groupId: '',
                    companyId:'00000000-0000-0000-0000-000000000000'
                },
                moduleId: '',
                moduleName: '',
                modules: modules,
                isChecked:false,
                permissions: permissions,
                companyPermissionList:[],
                permissionType: permissionType,
                show: false,
                groupList: ['ERP', 'Retail', 'WholeSale'],
                groupTypeList: ['Basic', 'Advance', 'Premium', 'Customize'],
                isEdit:false
            }
        },
        validations: {
            noblePermission: {
                groupId: {
                    required,
                },
            }
        },
        methods: {
            onCancel: function () {
                this.$router.push('/company');
            },
            onLimitedCustomer: function (per) {
               
                if (per.key === '7dc50e60-d5a2-419a-b12a-200ac71d7cb6') {
                    per.value = this.customerLimit
                }
                else if (per.key === '3d1f65f1-3f72-4898-a175-1b6ab42b2b9d') {
                    per.value = this.supplierLimit
                }
                else if (per.key === '8cb9768b-f76c-4614-a8a8-c22c7f1a0c81') {
                    per.value = this.openBatch
                }
                else if (per.key === '7a6930e8-876c-4344-b14e-80b961c14f96') {
                    per.value = this.attachmentLimit
                }
                else if (per.key === '671b5fb5-94d5-4edd-89fa-58cb08da4783') {
                    per.value = this.attachmentSize
                }
                
            },
            signOut: function () {
                localStorage.setItem('CanLogin', false)
                this.$router.push('/')
            },
            updateAllCheckStatuc: function (moduleId, per) {
                var allPermissionSelected = this.noblePermission.permissions.findIndex((y => !y.checked && y.moduleId === moduleId));
                if (allPermissionSelected < 0) {
                    this.isChecked = true
                }
                else {
                    this.isChecked = false
                }
                var perm = -1
                
                if (per.value === 'CanSendSaleEmailAsLink') {
                    perm = this.noblePermission.permissions.findIndex((y => y.value === 'CanSendSaleEmailAsLink'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm + 1].checked = false
                    }
                    
                }
                else if (per.value === 'CanSendSaleEmailAsPdf') {
                    perm = this.noblePermission.permissions.findIndex((y => y.value === 'CanSendSaleEmailAsPdf'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm - 1].checked = false
                    }
                    
                }
                else if (per.value === 'BasicMail') {
                    perm = this.noblePermission.permissions.findIndex((y => y.value === 'BasicMail'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm + 1].checked = false
                    }
                    
                }
                else if (per.value === 'StandardMail') {
                    perm = this.noblePermission.permissions.findIndex((y => y.value === 'StandardMail'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm - 1].checked = false
                    }
                    
                }
                else if (per.value === 'BasicOptions') {
                    perm = this.noblePermission.permissions.findIndex((y => y.value === 'BasicOptions'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm + 1].checked = false
                        this.noblePermission.permissions[perm + 2].checked = false
                    }
                }
                else if (per.value === 'StandardOptions') {
                    perm = this.noblePermission.permissions.findIndex((y => y.value === 'StandardOptions'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm + 1].checked = false
                    }
                }
                else if (per.value === 'AdvanceOptions') {
                    perm = this.noblePermission.permissions.findIndex((y => y.value === 'AdvanceOptions'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm - 2].checked = false
                    }
                }
                else if (per.value === 'MultiUnit') {
                    perm = this.noblePermission.permissions.findIndex((y => y.value === 'DecimalQuantity'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm].checked = false
                    }
                    
                }
                else if (per.value === 'DecimalQuantity') {
                    perm = this.noblePermission.permissions.findIndex((y => y.value === 'MultiUnit'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm].checked = false
                    }
                    
                }
                else if (per.value === 'IsFifo') {
                   
                    perm = this.noblePermission.permissions.findIndex((y => y.value === 'IsSerial'));
                    if (perm >= 0) {
                       
                        this.noblePermission.permissions[perm].checked = false
                        this.noblePermission.permissions[perm + 2].checked = !this.noblePermission.permissions[perm + 2].checked
                    }
                    
                }
                else if (per.value === 'IsSerial') {
                    perm = this.noblePermission.permissions.findIndex((y => y.value === 'IsFifo'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm].checked = false
                        this.noblePermission.permissions[perm + 1].checked = false
                    }
                    
                }
                else if (per.permissionName === 'Open Batch') {
                    perm = this.noblePermission.permissions.findIndex((y => y.value === 'IsFifo'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm].checked = !this.noblePermission.permissions[perm].checked
                        this.noblePermission.permissions[perm - 1].checked = false
                    }
                    
                }
                else if (per.value === 'CanChooseA4InvoiceType') {
                    perm = this.noblePermission.permissions.findIndex((y => y.value === 'DefaultTemplate'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm].checked = !this.noblePermission.permissions[perm].checked
                        this.noblePermission.permissions[perm + 1].checked = !this.noblePermission.permissions[perm + 1].checked
                        this.noblePermission.permissions[perm + 2].checked = !this.noblePermission.permissions[perm + 2].checked
                    }
                    
                }
                else if (per.value === 'CanChooseThermalInvoiceType') {
                    perm = this.noblePermission.permissions.findIndex((y => y.value === 'PKTemplate1Thermal'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm].checked = !this.noblePermission.permissions[perm].checked
                        this.noblePermission.permissions[perm + 1].checked = !this.noblePermission.permissions[perm + 1].checked
                        this.noblePermission.permissions[perm + 2].checked = !this.noblePermission.permissions[perm + 2].checked
                        this.noblePermission.permissions[perm + 3].checked = !this.noblePermission.permissions[perm + 3].checked
                    }
                    
                }
                else if (per.value === 'DefaultSaleVat') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === 'f12f90ad-4a83-423c-b264-c77e0d41f48e'));
                    if (perm >= 0) {
                        //this.noblePermission.permissions[perm].checked = !this.noblePermission.permissions[perm].checked
                        this.noblePermission.permissions[perm + 1].checked = false
                        this.noblePermission.permissions[perm + 2].checked = false
                        this.noblePermission.permissions[perm + 3].checked = false
                    }
                    
                }
                else if (per.value === 'DefaultSaleVatHead') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === 'e24f86dd-bf78-46dd-84b4-ebcf19b8abec'));
                    if (perm >= 0) {
                        //this.noblePermission.permissions[perm].checked = !this.noblePermission.permissions[perm].checked
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm + 1].checked = false
                        this.noblePermission.permissions[perm + 2].checked = false
                    }
                    
                }
                else if (per.value === 'DefaultSaleVatItem') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === 'd5ce4f2c-46be-45ce-8ab1-09bfa4d6c545'));
                    if (perm >= 0) {
                        //this.noblePermission.permissions[perm].checked = !this.noblePermission.permissions[perm].checked
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm - 2].checked = false
                        this.noblePermission.permissions[perm + 1].checked = false
                    }
                    
                }
                else if (per.value === 'DefaultSaleVatHeadItem') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '76cb1219-715a-4565-8b6f-ccf971a9f6f6'));
                    if (perm >= 0) {
                        //this.noblePermission.permissions[perm].checked = !this.noblePermission.permissions[perm].checked
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm - 2].checked = false
                        this.noblePermission.permissions[perm - 3].checked = false
                    }
                    
                }
                else if (per.value === 'DefaultPurchaseVat') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '21796b13-add1-4f59-8489-253fd1cf069c'));
                    if (perm >= 0) {
                        //this.noblePermission.permissions[perm].checked = !this.noblePermission.permissions[perm].checked
                        this.noblePermission.permissions[perm + 1].checked = false
                        this.noblePermission.permissions[perm + 2].checked = false
                        this.noblePermission.permissions[perm + 3].checked = false
                    }
                    
                }
                else if (per.value === 'DefaultPurchaseVatHead') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '024a0466-4cf4-45e0-8e66-ab7f8a2b18f6'));
                    if (perm >= 0) {
                        //this.noblePermission.permissions[perm].checked = !this.noblePermission.permissions[perm].checked
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm + 1].checked = false
                        this.noblePermission.permissions[perm + 2].checked = false
                    }
                    
                }
                else if (per.value === 'DefaultPurchaseVatItem') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '55b1234e-5d1b-435b-b8fe-d2f7fa0b19cb'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm - 2].checked = false
                        this.noblePermission.permissions[perm + 1].checked = false
                    }
                    
                }
                else if (per.value === 'DefaultPurchaseVatHeadItem') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === 'ac9c31b6-ab92-44e3-9a14-e9f5d5e42793'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm - 2].checked = false
                        this.noblePermission.permissions[perm - 3].checked = false
                    }
                    
                }
                else if (per.value === 'OnlineUser') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === 'ff294a85-6338-4334-afe1-71f86f6772b9'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm + 1].checked = false
                        this.noblePermission.permissions[perm + 2].checked = false
                    }
                    
                }
                else if (per.value === 'OfflineUser') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '37a627b8-f17c-40d7-a9f9-4eeb611b0c5a'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm + 1].checked = false
                    }
                    
                }
                else if (per.value === 'BothUser') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === 'caa4c724-9855-472b-a3a6-448ddbeb5fc7'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm - 2].checked = false
                    }
                    
                }
                else if (per.value === 'MachineWisePrefix') {
                    
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '15f6f624-01e5-47ba-a713-6d2d28ddef54'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm + 1].checked = false
                        this.noblePermission.permissions[perm + 2].checked = false
                    }
                    
                }
                else if (per.value === 'EmployeeWisePrefix') {
                    
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '07d57ea1c-6b21-4663-9525-51718b3c55d9'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm + 1].checked = false
                    }
                    
                }
                else if (per.value === 'NormalPrefix') {
                    
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '78d37d30-5a55-467d-8e7c-161e5fdb3e35'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm - 2].checked = false
                        this.noblePermission.permissions[perm - 1].checked = false
                    }
                    
                }
                else if (per.value === 'SimpleSaleInvoice') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '8836edc3-2112-40cc-927c-5ce4d1a223e1'));
                    if (perm >= 0) {
                        
                        this.noblePermission.permissions[perm + 1].checked = false
                        this.noblePermission.permissions[perm + 2].checked = false
                    }
                    
                }
                else if (per.value === 'SaleServiceInvoice') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '063dc416-1b25-4ea8-a8e9-0119d7d9cad0'));
                    if (perm >= 0) {
                        
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm + 1].checked = false
                    }
                    
                }
                else if (per.value === 'AllSale') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '5d1ff61a-be9e-4f3b-9a60-8c8548cbd23b'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm - 2].checked = false
                        this.noblePermission.permissions[perm - 1].checked = false
                    }

                }
                else if (per.value === 'SimpleAdvancePayment') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '3800cdc1-5d60-4cb1-a7aa-7c8b34331dfb'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm + 1].checked = false
                        this.noblePermission.permissions[perm + 2].checked = false
                        this.noblePermission.permissions[perm + 3].checked = false
                    }
                }
                else if (per.value === 'StandardAdvancePayment') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === 'ebcd1f5e-8be6-483f-abf2-8cca087e3e1c'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm + 1].checked = false
                        this.noblePermission.permissions[perm + 2].checked = false
                    }
                }
                else if (per.value === 'PremiumAdvancePayment') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '8f564728-6b49-404b-97da-2ea3b9c285e8'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm - 2].checked = false
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm + 1].checked = false
                    }
                }
                else if (per.value === 'MultipleAdvancePayment') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '5a315f24-182d-4103-a2c8-c5112bb273e2'));
                    if (perm >= 0) {
                        this.noblePermission.permissions[perm - 3].checked = false
                        this.noblePermission.permissions[perm - 2].checked = false
                        this.noblePermission.permissions[perm - 1].checked = false
                    }
                }
                else if (per.value === 'SimpleARAdvancePayment') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '52d11419-c095-491f-85db-12cfb10f7572'));
                    if (perm >= 0) {
                        
                        this.noblePermission.permissions[perm + 1].checked = false
                        this.noblePermission.permissions[perm + 2].checked = false
                        this.noblePermission.permissions[perm + 3].checked = false
                    }
                }
                else if (per.value === 'StandardARAdvancePayment') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '08d8b10c-4805-4fd8-a5cc-6858886b9381'));
                    if (perm >= 0) {
                        
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm + 1].checked = false
                        this.noblePermission.permissions[perm + 2].checked = false
                    }
                }
                else if (per.value === 'PremiumARAdvancePayment') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '3599e4d0-b3d7-4348-814c-4ee2c078e424'));
                    if (perm >= 0) {
                        
                        this.noblePermission.permissions[perm - 2].checked = false
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm + 1].checked = false
                    }
                }
                else if (per.value === 'MultipleARAdvancePayment') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '3c44beb7-721b-4b09-8548-4ab99c97529e'));
                    if (perm >= 0) {
                        
                        this.noblePermission.permissions[perm - 3].checked = false
                        this.noblePermission.permissions[perm - 2].checked = false
                        this.noblePermission.permissions[perm - 1].checked = false
                    }
                }
                else if (per.value === 'SimpleSupplierAdvancePayment') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '8e316c16-0a4e-436e-9508-5ad40901f56f'));
                    if (perm >= 0) {
                        
                        this.noblePermission.permissions[perm + 1].checked = false
                        this.noblePermission.permissions[perm + 2].checked = false
                        this.noblePermission.permissions[perm + 3].checked = false
                    }
                }
                else if (per.value === 'StandardSupplierAdvancePayment') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === 'f99cd1ad-ab7f-4780-99de-0e949c6fcfd3'));
                    if (perm >= 0) {
                        
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm + 1].checked = false
                        this.noblePermission.permissions[perm + 2].checked = false
                    }
                }
                else if (per.value === 'PremiumSupplierAdvancePayment') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '0786459b-bedc-40a6-937e-529e1a0a2955'));
                    if (perm >= 0) {
                        
                        this.noblePermission.permissions[perm - 2].checked = false
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm + 1].checked = false
                    }
                }
                else if (per.value === 'MultipleSupplierAdvancePayment') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '47c8c6fc-2b83-4935-8502-e8a9ee5cce43'));
                    if (perm >= 0) {
                        
                        this.noblePermission.permissions[perm - 3].checked = false
                        this.noblePermission.permissions[perm - 2].checked = false
                        this.noblePermission.permissions[perm - 1].checked = false
                    }
                }
                else if (per.value === 'MultiplePayment') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '96d32b95-273d-4e18-afa5-a0bb5d4ff606'));
                    if (perm >= 0) {
                        
                        this.noblePermission.permissions[perm + 1].checked = true
                        this.noblePermission.permissions[perm + 2].checked = false
                    }

                }
                else if (per.value === 'UnFollowAging') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === 'aa0a072e-c25c-4038-a91e-2b66fdbdede2'));
                    if (perm >= 0) {
                        
                        this.noblePermission.permissions[perm - 1].checked = false
                    }

                }
                else if (per.value === 'FollowAging') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === 'de6c8a1a-eab3-47f1-b03f-79d25397bcfc'));
                    if (perm >= 0) {
                        
                        this.noblePermission.permissions[perm + 1].checked = false
                    }
                }
                else if (per.value === 'Simple') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === 'f9b34159-89b7-4f92-b961-5feeea08c22f'));
                    if (perm >= 0) {
                        
                        this.noblePermission.permissions[perm + 1].checked = false
                        this.noblePermission.permissions[perm + 2].checked = false
                    }
                }
                else if (per.value === 'Standard') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '25885941-2aff-4392-bc8d-652b67367fe5'));
                    if (perm >= 0) {
                        
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm + 1].checked = false
                    }
                }
                else if (per.value === 'Premium') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === 'fcef83ba-b79e-4ecd-8afb-9fd5d2ab88ec'));
                    if (perm >= 0) {
                        
                        this.noblePermission.permissions[perm - 2].checked = false
                        this.noblePermission.permissions[perm - 1].checked = false
                    }
                }
                else if (per.value === 'PurchaseInvoiceInventoryControl') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '4d8e7f91-4553-4d28-9faa-5c2dc9c9991f'));
                    if (perm >= 0) {
                        
                        this.noblePermission.permissions[perm + 1].checked = false
                        this.noblePermission.permissions[perm + 2].checked = false
                        this.noblePermission.permissions[perm + 3].checked = false
                    }
                }
                else if (per.value === 'GoodsReceiveNoteInventoryManagement') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '4656c6cd-53c9-4f6e-bd8a-eab0de09adfe'));
                    if (perm >= 0) {
                        
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm + 1].checked = false
                        this.noblePermission.permissions[perm + 2].checked = false
                    }
                }
                else if (per.value === 'GRNInventoryAndRequiredInvoiceProcess') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === '0ceb6a79-0c54-400a-8281-66cb36e43740'));
                    if (perm >= 0) {
                        
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm - 2].checked = false
                        this.noblePermission.permissions[perm + 1].checked = false
                    }
                }
                else if (per.value === 'MissAndMatchGoodsReceiveAndPurchaseInvoice') {
                    perm = this.noblePermission.permissions.findIndex((y => y.key === 'b3fa4771-73fe-4891-a96c-ddd8a135488f'));
                    if (perm >= 0) {
                        
                        this.noblePermission.permissions[perm - 1].checked = false
                        this.noblePermission.permissions[perm - 2].checked = false
                        this.noblePermission.permissions[perm - 3].checked = false
                    }
                }
            },
            onClickCheckBox: function (moduleId) {
               // var root = this;
                var checkPermissionUpdate = false
                this.noblePermission.modules.forEach(function (x) {
                    if (x.id == moduleId) {
                        x.checked = !x.checked
                        checkPermissionUpdate = x.checked
                    }
                })
                this.noblePermission.permissions.forEach(function (x) {
                    if (x.moduleId == moduleId && checkPermissionUpdate) {
                        x.checked = true
                    }
                    else if (x.moduleId == moduleId && !checkPermissionUpdate){
                        x.checked = false
                    }
                    if (x.value === 'OpenSaleOrder' || x.value === 'SaleOrderToInvoice' || x.value === 'QuotationToSaleOrder'
                        || x.value === 'CanSendSaleEmailAsLink' || x.value === 'CanSendSaleEmailAsPdf' || x.value === 'MultiUnit'
                        || x.value === 'DecimalQuantity' || x.value === 'IsSerial'
                        || x.value === 'IsFifo' || x.permissionName === 'Open Batch') {
                        x.checked = false
                    }
                })
            },
            GetSelectedtGroupData: function () {
                var root = this;
                if (this.$route.query.type != 'Edit') {
                    this.ConvertEnumToList()
                    if (this.noblePermission.groupId != "") {
                        this.$https.get('/NoblePermission/GetNoblePermissionByGroupId?id=' + this.noblePermission.groupId).then(function (response) {
                            if (response.data != null) {
                                response.data.result.forEach(function (x) {
                                    var index = root.noblePermission.permissions.findIndex((y => y.key == x.key));
                                    if (index >= 0) {
                                        root.noblePermission.permissions[index].checked = true;
                                        if (x.key == '7dc50e60-d5a2-419a-b12a-200ac71d7cb6') {
                                            
                                            root.noblePermission.permissions[index].value = x.value;
                                            root.customerLimit = x.value;
                                        }
                                        else if (x.key == '3d1f65f1-3f72-4898-a175-1b6ab42b2b9d') {
                                            
                                            root.noblePermission.permissions[index].value = x.value;
                                            root.supplierLimit = x.value;
                                        }
                                        else if (x.key == '8cb9768b-f76c-4614-a8a8-c22c7f1a0c81') {
                                            
                                            root.noblePermission.permissions[index].value = x.value;
                                            root.openBatch = x.value;
                                        }
                                        else if (x.key == '7a6930e8-876c-4344-b14e-80b961c14f96') {
                                            
                                            root.noblePermission.permissions[index].value = x.value;
                                            root.attachmentLimit = x.value;
                                        }
                                        else if (x.key == '671b5fb5-94d5-4edd-89fa-58cb08da4783') {
                                            
                                            root.noblePermission.permissions[index].value = x.value;
                                            root.attachmentSize = x.value;
                                        }
                                    }
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

                    }
                }
               
            },
            SavePermissions: function () {
                var root = this;
                root.loading = true
                this.$https.post('/NoblePermission/SaveNoblePermission', this.noblePermission).then(function (response) {
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
                    root.loading = false
                });
            },
            SaveCompanyPermissions: function () {
                var root = this;
                this.loading = true;
                this.$https.post('/NoblePermission/SaveCompanyPermissions', this.noblePermission).then(function (response) {
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
                    root.loading = false
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
                    root.loading=false
                });
            },
            ShowOptions: function (module) {
                this.moduleId = module.id;
                this.moduleName = module.name;
                var root = this;
                var allPermissionSelected = root.noblePermission.permissions.findIndex((y => !y.checked  && y.moduleId === module.id));
                if (allPermissionSelected < 0) {
                    var moduleIndex = root.noblePermission.modules.findIndex((y=>y.id === module.id));
                    root.noblePermission.modules[moduleIndex].checked = true
                    root.isChecked = true
                }
                else {
                    root.isChecked = false
                }
                this.show = true
            },
            ConvertEnumToList: function () {
                this.noblePermission.modules = []
                this.noblePermission.permissions = []
                for (let item in this.modules) {
                    this.noblePermission.modules.push({
                        id: this.modules[item].id,
                        name: this.modules[item].value,
                        checked:false
                    });


                }
                for (let item in this.permissions) {
                    this.noblePermission.permissions.push({
                        permissionName: this.permissions[item].permissionName,
                        key: this.permissions[item].key,
                        value: this.permissions[item].value,
                        moduleId: this.permissions[item].moduleId,
                        typeId: this.permissions[item].typeId,
                        checked: this.permissions[item].checked,
                    });


                }
                this.show = false;
            },
            SetCompanyPermissionData: function () {
                var root = this;
                
                root.noblePermission.groupId = root.companyPermissionList[0].nobleGroupId;
                this.noblePermission.companyId = root.companyPermissionList[0].companyId;
                root.companyPermissionList.forEach(function (x) {
                    var index = root.noblePermission.permissions.findIndex((y => y.key == x.key));
                    if (index >= 0) {
                        root.noblePermission.permissions[index].checked = true;
                        
                        if (x.key == '7dc50e60-d5a2-419a-b12a-200ac71d7cb6') {
                            
                            root.noblePermission.permissions[index].value = x.value;
                            root.customerLimit = x.value;
                        }
                        else if (x.key == '3d1f65f1-3f72-4898-a175-1b6ab42b2b9d') {
                            
                            root.noblePermission.permissions[index].value = x.value;
                            root.supplierLimit = x.value;
                        }
                        else if (x.key == '8cb9768b-f76c-4614-a8a8-c22c7f1a0c81') {
                            
                            root.noblePermission.permissions[index].value = x.value;
                            root.openBatch = x.value;
                        }
                        else if (x.key == '7a6930e8-876c-4344-b14e-80b961c14f96') {
                            
                            root.noblePermission.permissions[index].value = x.value;
                            root.attachmentLimit = x.value;
                        }
                        else if (x.key == '671b5fb5-94d5-4edd-89fa-58cb08da4783') {
                            
                            root.noblePermission.permissions[index].value = x.value;
                            root.attachmentSize = x.value;
                        }
                    }
                });
            }
        },
        mounted() {
            
            this.ConvertEnumToList();
            if (this.$route.query.type === 'Edit') {
                this.isEdit = true
                this.companyPermissionList = this.$route.query.data;
                this.SetCompanyPermissionData()
            }
           
        }
    }
</script>
