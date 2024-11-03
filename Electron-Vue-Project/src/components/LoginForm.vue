<template>
    <div class="row m-0">

        <div class="col-sm-12 col-lg-12 mr-auto ml-auto ">

            <div class="card">
                <div class="row m-0">

                    <div class="BorderBottom col-12 mt-3">
                        <span class=" DayHeading">White Labeling</span>
                    </div>

                </div>

                <div class="card-body">
                    <div class="row">
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <label>Slogo Heading :</label>

                            <input class="form-control" style="height:42px;"
                                   v-model="whiteLabeling.heading" />

                        </div>

                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <label>Slogo Description :</label>

                            <input class="form-control" style="height:42px;"
                                   v-model="whiteLabeling.description" />

                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 "></div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 "></div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <label>Address Line 1 :</label>

                            <textarea class="form-control" style="height:42px;"
                                      v-model="whiteLabeling.addressLine1" />

                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <label>Address Line 2 :</label>
                            <textarea class="form-control" style="height:42px;"
                                      v-model="whiteLabeling.addressLine2" />
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <label>Address Line 2 :</label>
                            <textarea class="form-control" style="height:42px;"
                                      v-model="whiteLabeling.addressLine3" />

                        </div>



                    </div>
                    <h4>Terms and Conditions</h4>
                    <div class="row">

                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <label>Company Name :</label>

                            <input class="form-control" style="height:42px;"
                                   v-model="whiteLabeling.companyName" />

                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <label>Email :</label>

                            <input class="form-control" style="height:42px;"
                                   v-model="whiteLabeling.email" />

                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <label>Application Name :</label>

                            <input class="form-control" style="height:42px;"
                                   v-model="whiteLabeling.applicationName" />

                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <label>Fav Name :</label>

                            <input class="form-control" style="height:42px;"
                                   v-model="whiteLabeling.favName" />

                        </div>
                    </div>
                    <h4>Theme Color Scheme</h4>
                    <div class="row">
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <span style="float:left;"> Application Background Color: </span>
                            <span title="Application background color (background color of all input field when focus out)" style="font-size: 20px; font-weight: bold; color: green; float: right; padding-right: 8px;"> <i class="fa fa-info-circle" aria-hidden="true"></i></span>
                            <br />
                            <color-picker :color="whiteLabeling.applicationBgColor" v-model="whiteLabeling.applicationBgColor">

                            </color-picker>


                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <span style="float:left;">Application Text Color: </span>
                            <span title="Application text color (text color of all input field when focus out)" style="font-size:20px; font-weight:bold;color:green; float:right; padding-right:8px;"> <i class="fa fa-info-circle" aria-hidden="true"></i></span>
                            <br />   
                            <color-picker :color="whiteLabeling.applicationTextColor" v-model="whiteLabeling.applicationTextColor">

                            </color-picker>


                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <span style="float:left;">Card Background Color: </span>
                            <span title="Card background color (background color of all input field when focus in)" style="font-size: 20px; font-weight: bold; color: green; float: right; padding-right: 8px;"> <i class="fa fa-info-circle" aria-hidden="true"></i></span>
                            <br />
                            <color-picker :color="whiteLabeling.cardBgColor" v-model="whiteLabeling.cardBgColor">

                            </color-picker>


                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <span style="float:left;">Card Text Color: </span>
                            <span title="Card text color (text color of all input field when focus in)" style="font-size: 20px; font-weight: bold; color: green; float: right; padding-right: 8px;"> <i class="fa fa-info-circle" aria-hidden="true"></i></span>
                            <br />      
                            <color-picker :color="whiteLabeling.cardTextColor" v-model="whiteLabeling.cardTextColor">

                            </color-picker>


                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <span style="float:left;"> MenuBar: </span>
                            <span title="Main menu background color before click" style="font-size: 20px; font-weight: bold; color: green; float: right; padding-right: 8px;"> <i class="fa fa-info-circle" aria-hidden="true"></i></span>
                            <br />        
                            <color-picker :color="whiteLabeling.sideMenuColor" v-model="whiteLabeling.sideMenuColor">

                            </color-picker>


                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <span style="float:left;"> Menu Button color: </span>
                            <span title="Main menu text/icon color before click" style="font-size: 20px; font-weight: bold; color: green; float: right; padding-right: 8px;"> <i class="fa fa-info-circle" aria-hidden="true"></i></span>
                            <br />       
                            <color-picker :color="whiteLabeling.sideMenuBtnColor" v-model="whiteLabeling.sideMenuBtnColor">

                            </color-picker>


                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <span style="float:left;"> Menu Button Background Click: </span>
                            <span title="Main menu background color after click" style="font-size: 20px; font-weight: bold; color: green; float: right; padding-right: 8px;"> <i class="fa fa-info-circle" aria-hidden="true"></i></span>
                            <br />     
                            <color-picker :color="whiteLabeling.sideMenuBtnClickBgColor" v-model="whiteLabeling.sideMenuBtnClickBgColor">

                            </color-picker>


                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <span style="float:left;">Menu Button Text Click: </span>
                            <span title="Main menu text/icon color after click" style="font-size: 20px; font-weight: bold; color: green; float: right; padding-right: 8px;"> <i class="fa fa-info-circle" aria-hidden="true"></i></span>
                            <br />     
                            <color-picker :color="whiteLabeling.sideMenuBtnClickColor" v-model="whiteLabeling.sideMenuBtnClickColor">

                            </color-picker>


                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <span style="float:left;">Setup Menu Background Color : </span>
                            <span title="Setup menu every small card color(All Setup form menu like financial setup or item setup)" style="font-size: 20px; font-weight: bold; color: green; float: right; padding-right: 8px;"> <i class="fa fa-info-circle" aria-hidden="true"></i></span>
                            <br />        
                            <color-picker :color="whiteLabeling.setupBgColor" v-model="whiteLabeling.setupBgColor">

                            </color-picker>


                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <span style="float:left;">Setup Menu Text Color: </span>
                            <span title="Setup menu text/icon color(All Setup form menu like financial setup or item setup)" style="font-size: 20px; font-weight: bold; color: green; float: right; padding-right: 8px;"> <i class="fa fa-info-circle" aria-hidden="true"></i></span>
                            <br />       
                            <color-picker :color="whiteLabeling.setupTextColor" v-model="whiteLabeling.setupTextColor">

                            </color-picker>


                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <span style="float:left;"> Save Background Color: </span>
                            <span title="Application save button background color" style="font-size: 20px; font-weight: bold; color: green; float: right; padding-right: 8px;"> <i class="fa fa-info-circle" aria-hidden="true"></i></span>
                            <br />   
                            <color-picker :color="whiteLabeling.saveBtnBgColor" v-model="whiteLabeling.saveBtnBgColor">

                            </color-picker>


                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <span style="float:left;"> Save Text Color: </span>
                            <span title="Application save button text color" style="font-size: 20px; font-weight: bold; color: green; float: right; padding-right: 8px;"> <i class="fa fa-info-circle" aria-hidden="true"></i></span>
                            <br />    
                            <color-picker :color="whiteLabeling.saveBtnColor" v-model="whiteLabeling.saveBtnColor">

                            </color-picker>


                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <span style="float:left;">Cancel Background Color: </span>
                            <span title="Application cancel button background color" style="font-size: 20px; font-weight: bold; color: green; float: right; padding-right: 8px;"> <i class="fa fa-info-circle" aria-hidden="true"></i></span>
                            <br />   
                            <color-picker :color="whiteLabeling.cancelBgBtnColor" v-model="whiteLabeling.cancelBgBtnColor">

                            </color-picker>


                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <span style="float:left;">Cancel Text Color: </span>
                            <span title="Application cancel button text color" style="font-size: 20px; font-weight: bold; color: green; float: right; padding-right: 8px;"> <i class="fa fa-info-circle" aria-hidden="true"></i></span>
                            <br />  
                            <color-picker :color="whiteLabeling.cancelBtnColor" v-model="whiteLabeling.cancelBtnColor">

                            </color-picker>


                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <span style="float:left;">Heading: </span>
                            <span title="Application heading color" style="font-size: 20px; font-weight: bold; color: green; float: right; padding-right: 8px;"> <i class="fa fa-info-circle" aria-hidden="true"></i></span>
                            <br />  
                            <color-picker :color="whiteLabeling.headingColor" v-model="whiteLabeling.headingColor">

                            </color-picker>



                        </div>

                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <span style="float:left;">Table Header Background Color: </span>
                            <span title="Application table header background color" style="font-size: 20px; font-weight: bold; color: green; float: right; padding-right: 8px;"> <i class="fa fa-info-circle" aria-hidden="true"></i></span>
                            <br />     
                            <color-picker :color="whiteLabeling.tableHeaderBgColor" v-model="whiteLabeling.tableHeaderBgColor">

                            </color-picker>


                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <span style="float:left;">Table Header Text Color: </span>
                            <span title="Application table header text color" style="font-size: 20px; font-weight: bold; color: green; float: right; padding-right: 8px;"> <i class="fa fa-info-circle" aria-hidden="true"></i></span>
                            <br />     
                            <color-picker :color="whiteLabeling.tableHeaderColor" v-model="whiteLabeling.tableHeaderColor">

                            </color-picker>


                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <span style="float:left;">Invoice Title Background Color: </span>
                            <span title="On Every Purchase/Sale Invoice header box background color" style="font-size: 20px; font-weight: bold; color: green; float: right; padding-right: 8px;"> <i class="fa fa-info-circle" aria-hidden="true"></i></span>
                            <br />  
                            <color-picker :color="whiteLabeling.invoiceTitleBgColor" v-model="whiteLabeling.invoiceTitleBgColor">

                            </color-picker>


                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <span style="float:left;"> Invoice Title Text Color: </span>
                            <span title="On Every Purchase/Sale Invoice header box text color" style="font-size: 20px; font-weight: bold; color: green; float: right; padding-right: 8px;"> <i class="fa fa-info-circle" aria-hidden="true"></i></span>
                            <br />
                            <color-picker :color="whiteLabeling.invoiceTitleColor" v-model="whiteLabeling.invoiceTitleColor">

                            </color-picker>


                        </div>
                    </div>
                    <h4>Images</h4>
                    <div class="row">

                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <label> Tag Image :</label>
                            <div>
                                <b-form-file v-model="tagimage"
                                             @input="uploadFile('tagImage')"
                                             id="uplaodfile"
                                             ref="imgupload1"
                                             accept=".jpg, .png, .jpeg"
                                             :no-drop="true"
                                             :state="Boolean(whiteLabeling.tagImage1Path)"
                                             placeholder="Click here to choose a file"></b-form-file>

                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <label> Login Screen Image :</label>
                            <div>
                                <b-form-file v-model="loginscimage"
                                             @input="uploadFile('loginScreenImage')"
                                             id="uplaodfile2"
                                             ref="imgupload2"
                                             accept=".jpg, .png, .jpeg"
                                             :no-drop="true"
                                             :state="Boolean(whiteLabeling.loginScreenImagePath)"
                                             placeholder="Click here to choose a file"></b-form-file>

                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <label> Login Logo :</label>
                            <div>
                                <b-form-file v-model="loginlogo"
                                             @input="uploadFile('loginLogo')"
                                             id="uplaodfile3"
                                             ref="imgupload3"
                                             accept=".jpg, .png, .jpeg"
                                             :no-drop="true"
                                             :state="Boolean(whiteLabeling.loginlogoPath)"
                                             placeholder="Click here to choose a file"></b-form-file>

                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <label> Background Image :</label>
                            <div>
                                <b-form-file v-model="bgimage"
                                             @input="uploadFile('bgImage')"
                                             id="uplaodfile4"
                                             ref="imgupload4"
                                             accept=".jpg, .png, .jpeg"
                                             :no-drop="true"
                                             :state="Boolean(whiteLabeling.backgroundImagePath)"
                                             placeholder="Click here to choose a file"></b-form-file>

                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <label> Sidebar Image :</label>
                            <div>
                                <b-form-file v-model="sidebarlogo"
                                             @input="uploadFile('sidebarImage')"
                                             id="uplaodfile5"
                                             ref="imgupload5"
                                             accept=".svg"
                                             :no-drop="true"
                                             :state="Boolean(whiteLabeling.sidebarImagePath)"
                                             placeholder="Click here to choose a file"></b-form-file>

                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 ">
                            <label> Fav Icon :</label>
                            <div>
                                <b-form-file v-model="favIcon"
                                             @input="uploadFile('favIcon')"
                                             id="uplaodfile6"
                                             ref="imgupload6"
                                             accept=".jpg, .png, .jpeg"
                                             :no-drop="true"
                                             :state="Boolean(whiteLabeling.favIconPath)"
                                             placeholder="Click here to choose a file"></b-form-file>

                            </div>
                        </div>

                    </div>

                    <div class="row mb-2 mt-3" v-if="attachementList.length > 0">
                        <table class="table">
                            <tr>
                                <th class="text-center" style="width:5%;">#</th>
                                <th style="width:25%;">Description</th>
                                <th style="width:40%;">Document</th>
                                <th class="text-center" style="width:5%;">View</th>
                                <th class="text-center" style="width:5%;">Download</th>
                                <th class="text-center" style="width:5%;"></th>
                            </tr>
                            <tr v-for="(item,index) in attachementList" :key="index">
                                <td class="text-center">{{index+1}}</td>
                                <td>
                                    {{item.description}}
                                </td>
                                <td>
                                    {{item.fileName}}
                                </td>
                                <td class="text-center">
                                    <button @click="ViewAttachment(item.path)"
                                            title="Remove Item"
                                            class="btn btn-primary btn-round btn-sm  btn-icon">
                                        <i class="fas fa-eye"></i>
                                    </button>
                                </td>
                                <td class="text-center">
                                    <button class="btn btn-primary btn-round btn-sm  btn-icon" v-on:click="DownloadAttachment(item.path)">
                                        <i class="fa fa-download"></i>
                                    </button>
                                </td>
                                <td class="text-center">
                                    <button @click="RemoveItem(index)"
                                            title="Add Attachement"
                                            class="btn btn-danger btn-round btn-sm  btn-icon">
                                        <i class="nc-icon nc-simple-remove"></i>
                                    </button>
                                </td>
                            </tr>

                        </table>
                    </div>

                    <div class="row mb-2 mt-3">
                        <div class="col-12 text-right">

                            <a href="javascript:void(0)" class="btn btn-primary " style="margin-top:27px;" v-on:click="Savebutton">Save</a>

                            <a href="javascript:void(0)" class="btn btn-danger " style="margin-top:27px;" v-on:click="gotoCompany">Cancel </a>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <attachment-view :documentpath="documentpath" :show="showView" v-if="showView" @close="CloseModel" />
    </div>

</template>

<script>
    import { BFormFile } from 'bootstrap-vue';
    import { hexToCSSFilter } from 'hex-to-css-filter';
    export default {
        components: {
            BFormFile
        },
        data() {
            return {
                tagimage: null,
                loginscimage: null,
                loginlogo: null,
                sidebarlogo: null,
                bgimage: null,
                path: '',
                filename: '',
                favIcon: null,
                showView: false,
                documentpath: '',
                whiteLabeling: {
                    heading: '',
                    description: '',
                    tagImage1Name: '',
                    tagImage1Path: null,
                    companyName: '',
                    applicationName: '',
                    addressLine1: '',
                    addressLine2: '',
                    addressLine3: '',
                    loginScreenImageName: '',
                    loginScreenImagePath: null,
                    loginlogoName: '',
                    loginlogoPath: null,
                    backgroundImageName: '',
                    backgroundImagePath: null,
                    sidebarImageName: '',
                    sidebarImagePath: null,
                    email: '',
                    favIconName: '',
                    favIconPath: null,
                    favName: '',
                    sideMenuColor: '',
                    sideMenuBtnColor: '',
                    sideMenuBtnClickColor: '',

                    saveBtnBgColor: '',
                    saveBtnColor: '',

                    cancelBgBtnColor: '',
                    cancelBtnColor: '',
                    headingColor: '',

                    tableHeaderBgColor: '',
                    tableHeaderColor: '',

                    invoiceTitleBgColor: '',
                    invoiceTitleColor: '',

                    applicationBgColor: '',
                    applicationTextColor: '',
                    cardBgColor: '',
                    cardTextColor: '',
                    setupBgColor: '',
                    setupTextColor: '',
                    setupCssFilter: '',
                    sideMenuBtnColorFilter: '',
                    sideMenuBtnClickColorFilter: '',
                    sideMenuBtnClickBgColor: '',
                },
                attachementList: []

            }
        },
        methods: {
            Savebutton() {
                var root = this;

                var token = '';
                if (this.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                var cssFilter = '';
                if (this.whiteLabeling.sideMenuBtnColor != null && this.whiteLabeling.sideMenuBtnColor != '') {
                    cssFilter = hexToCSSFilter(this.whiteLabeling.sideMenuBtnColor)
                    this.whiteLabeling.sideMenuBtnColorFilter = cssFilter.filter
                }
                if (this.whiteLabeling.sideMenuBtnClickColor != null && this.whiteLabeling.sideMenuBtnClickColor != '') {
                    cssFilter = hexToCSSFilter(this.whiteLabeling.sideMenuBtnClickColor)
                    this.whiteLabeling.sideMenuBtnClickColorFilter = cssFilter.filter
                }
                if (this.whiteLabeling.setupTextColor != null && this.whiteLabeling.setupTextColor != '') {
                    cssFilter = hexToCSSFilter(this.whiteLabeling.setupTextColor)
                    this.whiteLabeling.setupCssFilter = cssFilter.filter
                }
                root.$https
                    .post('/NoblePermission/SaveWhiteLabelInfo', root.whiteLabeling, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(response => {
                        this.loading = false
                        this.info = response.data.bpi
                        this.$swal.fire({
                            icon: 'success',
                            title: 'Saved Successfully',
                            showConfirmButton: false,
                            timer: 1000,
                            timerProgressBar: true,

                        });
                        this.$router.push('/company');
                    })
            },
            gotoCompany() {
                this.$router.push('/company');
            },

            uploadFile(fileName) {
                console.log(fileName)
                var root = this;
                var token = '';
                var imageName = '';
                if (root.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                var file = '';
                if (fileName === 'tagImage') {
                    file = this.$refs.imgupload1.files
                    imageName = 'VatScreen.png'

                }
                else if (fileName === 'loginScreenImage') {
                    file = this.$refs.imgupload2.files
                    imageName = 'invoiceImage.png'
                }
                else if (fileName === 'loginLogo') {
                    file = this.$refs.imgupload3.files
                    imageName = 'NobleLogin.png'
                }
                else if (fileName === 'bgImage') {
                    file = this.$refs.imgupload4.files
                    imageName = 'Sale Order.jpg'
                }
                else if (fileName === 'sidebarImage') {
                    file = this.$refs.imgupload5.files
                    imageName = 'logo-mini.svg'
                }
                else if (fileName === 'favIcon') {
                    file = this.$refs.imgupload6.files
                    imageName = 'fav-icon.png'
                }
                if (file.length <= 0)
                    return

                var fileData = new FormData();
                for (var k = 0; k < file.length; k++) {
                    fileData.append("files", file[k]);
                    root.fileName = file[k].name;
                }

                root.$https.post('/NoblePermission/UploadFilesAsync?imageName=' + imageName, fileData, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            var isAttachmentExist = -9;
                            if (fileName === 'tagImage') {

                                root.whiteLabeling.tagImage1Path = response.data;
                                root.whiteLabeling.tagImage1Name = root.fileName;
                                if (root.attachementList.length > 0) {
                                    isAttachmentExist = root.attachementList.findIndex(x => x.description === 'Tag Image')
                                    if (isAttachmentExist >= 0) {
                                        root.attachementList[isAttachmentExist].path = response.data
                                        root.attachementList[isAttachmentExist].fileName = root.fileName
                                    }
                                }

                            }
                            else if (fileName === 'loginScreenImage') {
                                root.whiteLabeling.loginScreenImagePath = response.data;
                                root.whiteLabeling.loginScreenImageName = root.fileName;
                                if (root.attachementList.length > 0) {
                                    isAttachmentExist = root.attachementList.findIndex(x => x.description === 'Login Screen Image')
                                    if (isAttachmentExist >= 0) {
                                        root.attachementList[isAttachmentExist].path = response.data
                                        root.attachementList[isAttachmentExist].fileName = root.fileName
                                    }
                                }

                            }
                            else if (fileName === 'loginLogo') {
                                root.whiteLabeling.loginlogoPath = response.data;
                                root.whiteLabeling.loginlogoName = root.fileName;
                                if (root.attachementList.length > 0) {
                                    isAttachmentExist = root.attachementList.findIndex(x => x.description === 'Login Logo')
                                    if (isAttachmentExist >= 0) {
                                        root.attachementList[isAttachmentExist].path = response.data
                                        root.attachementList[isAttachmentExist].fileName = root.fileName
                                    }
                                }

                            }
                            else if (fileName === 'bgImage') {

                                root.whiteLabeling.backgroundImagePath = response.data;
                                root.whiteLabeling.backgroundImageName = root.fileName;
                                if (root.attachementList.length > 0) {
                                    isAttachmentExist = root.attachementList.findIndex(x => x.description === 'Background Image')
                                    if (isAttachmentExist >= 0) {
                                        root.attachementList[isAttachmentExist].path = response.data
                                        root.attachementList[isAttachmentExist].fileName = root.fileName
                                    }
                                }

                            }
                            else if (fileName === 'sidebarImage') {
                                root.whiteLabeling.sidebarImagePath = response.data;
                                root.whiteLabeling.sidebarImageName = root.fileName;
                                if (root.attachementList.length > 0) {
                                    isAttachmentExist = root.attachementList.findIndex(x => x.description === 'Sidebar Image')
                                    if (isAttachmentExist >= 0) {
                                        root.attachementList[isAttachmentExist].path = response.data
                                        root.attachementList[isAttachmentExist].fileName = root.fileName
                                    }
                                }

                            }
                            else if (fileName === 'favIcon') {
                                root.whiteLabeling.favIconPath = response.data;
                                root.whiteLabeling.favIconName = root.fileName;
                                if (root.attachementList.length > 0) {
                                    isAttachmentExist = root.attachementList.findIndex(x => x.description === 'Fav Icon')
                                    if (isAttachmentExist >= 0) {
                                        root.attachementList[isAttachmentExist].path = response.data
                                        root.attachementList[isAttachmentExist].fileName = root.fileName
                                    }
                                }

                            }

                        }
                    },
                        function () {
                            root.loading = false;
                            root.$swal({
                                title: "Error!",
                                text: "Something went wrong",
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                buttonsStyling: false
                            });
                        });
            },


            RemoveItem: function (index) {
                if (this.attachementList[index].description === 'Tag Image') {
                    this.whiteLabeling.tagImage1Path = null;
                    this.whiteLabeling.tagImage1Name = '';
                    this.tagimage = null;
                    this.path = '';
                    this.filename = '';
                    this.attachementList.splice(index, 1);

                }
                else if (this.attachementList[index].description === 'Login Screen Image') {
                    this.whiteLabeling.loginScreenImagePath = null;
                    this.whiteLabeling.loginScreenImageName = '';
                    this.loginscimage = null;
                    this.attachementList.splice(index, 1);

                    this.path = '';
                    this.filename = '';
                }
                else if (this.attachementList[index].description === 'Login Logo') {
                    this.whiteLabeling.loginlogoPath = null;
                    this.whiteLabeling.loginlogoName = '';
                    this.loginlogo = null;
                    this.attachementList.splice(index, 1);
                    this.path = '';
                    this.filename = '';
                }
                else if (this.attachementList[index].description === 'Background Image') {
                    this.whiteLabeling.backgroundImagePath = null;
                    this.whiteLabeling.backgroundImageName = '';
                    this.bgimage = null;
                    this.attachementList.splice(index, 1);
                    this.path = '';
                    this.filename = '';
                }
                else if (this.attachementList[index].description === 'Sidebar Image') {
                    this.whiteLabeling.sidebarImagePath = null;
                    this.whiteLabeling.sidebarImageName = '';
                    this.sidebarlogo = null;
                    this.attachementList.splice(index, 1);
                    this.path = '';
                    this.filename = '';
                }
                else if (this.attachementList[index].description === 'Fav Icon') {
                    this.whiteLabeling.favIconPath = null;
                    this.whiteLabeling.favIconName = '';
                    this.favIcon = null;
                    this.attachementList.splice(index, 1);
                    this.path = '';
                    this.filename = '';
                }



            },

            ViewAttachment: function (path) {
                if (path != '' && path != undefined && path != null) {
                    this.documentpath = path;
                    this.showView = true;
                }
            },

            DownloadAttachment(path) {

                var root = this;
                var token = '';
                if (root.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                var ext = path.split('.')[1];
                root.$https.get('/NoblePermission/DownloadFile?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                    .then(function (response) {
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        link.setAttribute('download', 'file.' + ext);
                        document.body.appendChild(link);
                        link.click();
                    });
            },

            CloseModel: function () {
                this.showView = false;
            },
        },
        created() {
            if (this.$route.query.data != '' && this.$route.query.data != null && this.$route.query.data != undefined) {

                this.whiteLabeling = this.$route.query.data;
                this.attachementList.push({ description: 'Tag Image', path: this.whiteLabeling.tagImage1Path, fileName: this.whiteLabeling.tagImage1Name });
                this.attachementList.push({ description: 'Login Screen Image', path: this.whiteLabeling.loginScreenImagePath, fileName: this.whiteLabeling.loginScreenImageName });
                this.attachementList.push({ description: 'Login Logo', path: this.whiteLabeling.loginLogoPath, fileName: this.whiteLabeling.loginLogoName });
                this.attachementList.push({ description: 'Background Image', path: this.whiteLabeling.backgroundImagePath, fileName: this.whiteLabeling.backgroundImageName });
                this.attachementList.push({ description: 'Sidebar Image', path: this.whiteLabeling.sidebarImagePath, fileName: this.whiteLabeling.sidebarImageName });
                this.attachementList.push({ description: 'Fav Icon', path: this.whiteLabeling.favIconPath, fileName: this.whiteLabeling.favIconName });

            }
        }
    }
</script>