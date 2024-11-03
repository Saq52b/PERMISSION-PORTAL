<template>
    <div style="margin-bottom:0px" class="card" >
        <div class="card-body">
            <div class="col-lg-12">
                <div class="tab-content" id="nav-tabContent">
                    <div class="modal-header" >

                        <h5 class="modal-title" > Noble Permissions</h5>

                    </div>
                    <div class="">
                        <div class="card-body">
                            <div class="row ">
                                <div :key="render" class="form-group has-label col-sm-12 " >
                                    <label class="text  font-weight-bolder"> Group Name:<span class="LabelColour"> *</span></label>
                                    <input class="form-control"  v-model="groupName" type="text" />
                                   
                                </div>
                                
                                
                            </div>
                        </div>
                    </div>
                    
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import modules from '@/enums/modules'
    import permissions from '@/enums/permissions'
    export default {

        name: 'HelloWorld',
       
        data() {
            return {
                modules: modules,
                permissions: permissions,
                groupName: '',
                groupType:''
            }
        }
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    h3 {
        margin: 40px 0 0;
    }

    ul {
        list-style-type: none;
        padding: 0;
    }

    li {
        display: inline-block;
        margin: 0 10px;
    }

    a {
        color: #42b983;
    }
</style>