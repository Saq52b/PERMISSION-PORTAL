<template>
    <div class="rounded">
        <div class="col-12" id="second">
            <h2>Login to your account</h2>
        </div>
        <div class=" col-xl-4 col-lg-5 col-md-6 col-sm-9 container box">
            <div class=" col-xl-12 col-lg-12 col-md-12 col-sm-12 col-12 container">
                <form class="justify-content-center">
                    <div class="form-group col-md-12" v-bind:class="{'has-danger' : $v.loginVm.email.$error}">
                        <p class="text-center">
                            <img src="NobleLogin.png"  class="img-fluid imgCss" height="150" width="150" />
                        </p>
                        <label class="h6" for="exampleInputEmail1" >Email/ User Name</label>
                        <input type="email" v-model="$v.loginVm.email.$model" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp" placeholder=" Type email here">
                        <span v-if="$v.loginVm.email.$error" class="error">
                            <span v-if="!$v.loginVm.email.required">Email is required</span>
                        </span>
                    </div>
                    <div class="form-group col-md-12" v-bind:class="{'has-danger' : $v.loginVm.password.$error}">
                        <label class="h6" for="exampleInputPassword1">Password</label>
                        <input type="password" v-model="$v.loginVm.password.$model" class="form-control" id="exampleInputPassword1" placeholder=" Type password here">
                        <span v-if="$v.loginVm.password.$error" class="error">
                            <span v-if="!$v.loginVm.password.required">Password is required</span>
                            <span v-if="!$v.loginVm.password.minLength">Password required minimum 8 length</span>
                        </span>
                    </div>
                    <button class="btn col-md-7 btn1" @click="$event.target.disabled = true" v-bind:disabled="$v.loginVm.$invalid" v-on:click="btnLogin">LOGIN</button>
                </form>
            </div>
        </div>
    </div>
</template>

<script>
    import image from '../assets/NobleLogin.png';
    import { required, minLength } from "vuelidate/lib/validators";
    /*import axios from "axios";*/
    export default {
        name: 'Login',
        data() {
            return {
                loginVm: {
                    email: '',
                    password: ''
                },
                image: '',
                prefix: '',
                result: ''
            }
        },
        validations: {
            loginVm: {
                email: {
                    required
                },
                password: {
                    required,
                    minLength: minLength(8)
                }
            }
        },
        methods: {
            btnLogin: function () {
                var root = this;

                this.$https.post('/NoblePermission/UserLogin', this.loginVm)
                    .then(function (res) {
                        if (res.data.substring(0, 7) != "success") {
                            root.$swal({
                                title: 'Error!',
                                text: res.data,
                                type: 'error',
                                confirmButtonClass: "btn btn-info",
                                buttonsStyling: false
                            });
                        }
                        else {
                            localStorage.setItem('CanLogin', true)
                            root.$router.push('/company')
                            
                            
                        }
                    }, function () {
                        root.$swal({
                            title: 'Error!',
                            text: "Network error",
                            type: 'error',
                            confirmButtonClass: "btn btn-info",
                            buttonsStyling: false
                        })
                    });
            },
           
        },
        created() {
            this.image = image;
           
        }
    }
</script>
<style scoped>
    @import url('https://fonts.googleapis.com/css2?family=Roboto&display=swap');

    .imgCss {
        margin: 0px !important;
    }

    .rounded {
        margin: 0%;
        padding: 0%;
        box-sizing: border-box;
        font-family: 'Roboto', sans-serif;
    }

    #second {
        background-color: rgb(241, 238, 238);
        text-align: center;
        margin-bottom: 20px;
    }

        #second h2 {
            padding-bottom: 20px;
            padding-top: 20px;
        }

    .box {
        border-radius: 18px;
        text-align: center;
        border: black solid 0.3pt;
        box-shadow: 0px 1px 4px 1px rgba(173, 173, 173, 0.48);
        color: black;
        padding: 20px;
        justify-content: center;
    }

    .form-control {
        border-radius: 50px !important;
        padding: 20px;
    }

    input {
        font-style: italic;
    }

    .btn1 {
        border-radius: 50px !important;
        color: white !important;
        box-shadow: 0px 1px 9px 1px rgba(190, 187, 187, 0.48) !important;
        background-color: #3178F6 !important;
        padding: 10px 40px;
    }

    a {
        color: black !important;
    }
</style>