<template>
    <modal :modalLarge="true" :extraLarge="true" :show="show">
        <div style="margin-bottom:0px" class="card" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="card-body">
                            <div class="row mb-2">
                                <table class="table">
                                    <tr>
                                        <th class="text-center" style="width:5%;">#</th>
                                        <th style="width:10%;">{{ $t('ImportAttachment.Date') }}</th>
                                        <th style="width:35%;">{{ $t('ImportAttachment.Description') }}</th>
                                        <th style="width:30%;">{{ $t('ImportAttachment.Document') }}</th>
                                        <th class="text-center" style="width:5%;">{{ $t('ImportAttachment.View') }}</th>
                                        <th class="text-center" style="width:5%;">{{ $t('ImportAttachment.Print') }}</th>
                                        <th class="text-center" style="width:5%;">{{ $t('ImportAttachment.Download') }}</th>
                                        <th class="text-center" style="width:5%;"></th>
                                    </tr>
                                    <tr v-for="(item,index) in attachementList" :key="index">
                                        <td>{{index+1}}</td>
                                        <td>
                                            {{item.date}}
                                        </td>
                                        <td>
                                            <input class="form-control" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'" v-model="item.description" type="text" />
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
                                            <button class="btn btn-primary btn-round btn-sm  btn-icon" @click="ViewAttachment(item.path)">
                                                <i class="fa fa-print"></i>
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

                                    <tr>
                                        <td class="text-center"></td>
                                        <td>{{date}}</td>
                                        <td>
                                            <input class="form-control" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'" v-model="description" type="text" />
                                        </td>
                                        <td>
                                            <b-form-file v-model="file1"
                                                         @input="uploadFile()"
                                                         id="uplaodfile"
                                                         ref="imgupload1"
                                                         :no-drop="true"
                                                         :state="Boolean(file1)"
                                                         v-bind:placeholder="$t('ImportAttachment.ChooseFile')"></b-form-file>
                                        </td>
                                        <td class="text-center"></td>
                                        <td class="text-center"></td>
                                        <td class="text-center"></td>
                                        <td class="text-center">
                                            <button @click="AddAttachement()"
                                                    v-bind:disabled="path==''"
                                                    title="Add Attachement"
                                                    class="btn btn-primary btn-round btn-sm  btn-icon">
                                                <i class="fa fa-plus"></i>
                                            </button>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="modal-footer row">
                            <div class="col-lg-12 mt-3 " v-bind:class="$i18n.locale == 'en' ? 'text-right' : 'text-left'">
                                <button class="btn btn-primary  "
                                        v-bind:disabled="attachementList.length==0"
                                        @click="SaveAttachement">
                                    <i class="nc-icon nc-cloud-upload-94"></i> {{ $t('ImportAttachment.Upload') }}
                                </button>
                                <button class="btn btn-danger   mr-2"
                                        v-on:click="close">
                                    {{ $t('ImportAttachment.Cancel') }}
                                </button>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <attachment-view :documentpath="documentpath" :show="showView" v-if="showView" @close="CloseModel" />
        </div>
    </modal>
</template>
<script>
  //  import clickMixin from '@/Mixins/clickMixin'
    import moment from "moment";
    import { BFormFile } from 'bootstrap-vue';
    export default {
        components: {
            BFormFile
        },
        props: ['show', 'documentid', 'attachmentList'],
       // mixins: [clickMixin],
        data: function () {
            return {
                arabic: '',
                english: '',
                date: '',
                path: '',
                fileName: '',
                description: '',
                file1: null,
                render: 0,
                loading: false,
                showView: false,
                documentpath: '',
                attachementList: []
            }
        },
        methods: {
            RemoveItem: function (index) {
                this.attachementList.splice(index, 1);
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
                root.$https.get('/Contact/DownloadFile?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
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

            AddAttachement: function () {
                
                this.date = moment().format('l');
                this.attachementList.push({ date: this.date, documentId: this.documentid, description: this.description, path: this.path, fileName: this.fileName });

                this.description = '';
                this.file1 = null;
                this.path = '';
                this.fileName = '';
            },

            close: function () {
                
                this.$emit("close", this.attachementList);
            },

            SaveAttachement() {
                if (this.documentid != undefined) {
                    var root = this;
                    var token = '';
                    if (root.$session.exists()) {
                        token = localStorage.getItem('token');
                    }

                    root.$https.post('/Company/SaveAttachment', this.attachementList, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(function (response) {
                            if (response.data.isSuccess) {
                                root.$swal({
                                    title: "Saved!",
                                    text: 'Saved Successfuly',
                                    type: 'success',
                                    confirmButtonClass: "btn btn-success",
                                    buttonStyling: false,
                                    icon: 'success',
                                    timer: 1500,
                                    timerProgressBar: true
                                });
                                root.close();
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
                }
                else {
                    this.close();
                }
                
            },

            uploadFile() {
                debugger;
                var root = this;
                var token = '';
                if (root.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                
                var file = this.$refs.imgupload1.files;

                var fileData = new FormData();
                for (var k = 0; k < file.length; k++) {
                    fileData.append("files", file[k]);
                    root.fileName = file[k].name;
                }

                root.$https.post('/Company/UploadFilesAsync', fileData, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            root.path = response.data;

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
            GetAttachmentList() {

                var root = this;
                var token = '';
                if (root.$session.exists()) {
                    token = localStorage.getItem('token');
                }
                
                root.$https.get('/Company/AttachmentList?id=' + this.documentid + '&prop=' + this.document, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            response.data.forEach(function (x) {
                                root.attachementList.push({ date: moment(x.date).format('l'), documentId: x.documentId, description: x.description, path: x.path, fileName: x.fileName });
                            });
                        }
                        
                    });
            },
        },

        created: function () {
            
            var root = this;
            if (this.documentid != undefined) {
                this.GetAttachmentList();
            }
            else {
                this.attachmentList.forEach(function (x) {
                    root.attachementList.push({ date: moment(x.date).format('l'), documentId: x.documentId, description: x.description, path: x.path, fileName: x.fileName });
                });
            }
        },

        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.date = moment().format('l');
        }
    }
</script>
