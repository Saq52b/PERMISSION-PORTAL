<template>
    <modal :modalLarge="true"  :show="show">
        <div style="margin-bottom:0px" class="card">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="card-body">
                            <div class="row mb-2">
                                <div class="col-lg-12 text-center" v-if="isImage">
                                    <img :src="filePath" alt="Attachment Image" />
                                </div>

                                <div class="col-lg-12 text-center" v-if="isPdf">
                                    <embed :src="filePath" style="width:100%; height:1000px;" />
                                </div>

                            </div>
                        </div>
                            <div class="modal-footer row" >
                                <div class="col-lg-12 mt-3 " >                                    
                                    <button class="btn btn-danger   mr-2"
                                            v-on:click="close">
                                        Cancel
                                    </button>
                                </div>
                            </div>                       

                    </div>
                </div>
            </div>
        </div>
    </modal>
    <!--<acessdenied v-else :model=true></acessdenied>-->
</template>
<script>
  //  import clickMixin from '@/Mixins/clickMixin'
    export default {
        props: ['show', 'documentpath'],
    //    mixins: [clickMixin],
        data: function () {
            return {
                arabic: '',
                english: '',
                filePath: '',
                render: 0,
                loading: false,
                isImage: false,
                isPdf: false
            }
        },
        methods: {            
            close: function () {
                this.$emit('close');
            },

            getBase64Image: function (path) {
                var root = this;
                var token = '';
                if (root.$session.exists()) {
                    token = localStorage.getItem('token');
                }

                root.$https
                    .get('/NoblePermission/GetBaseImage?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                        if (response.data != null) {
                            if (root.isImage) {
                                root.filePath = 'data:image/png;base64,' + response.data; 
                            }

                            if (root.isPdf) {
                                root.filePath = 'data:application/pdf;base64,' + response.data; 
                            }                                                         
                        }
                    });
            },
        },

        created: function () {
            
            var fileExtension = this.documentpath.split('.').pop();

            if (fileExtension == 'png' || fileExtension == 'jpg' || fileExtension == 'jpeg' || fileExtension == 'svg') {
                this.getBase64Image(this.documentpath);     
                this.isImage = true;
            }

            if (fileExtension == 'pdf') {
                this.getBase64Image(this.documentpath);
                this.isPdf = true;
            }
        },

        mounted: function () {
            
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
        }
    }
</script>
