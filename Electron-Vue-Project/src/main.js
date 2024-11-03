import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import Vuelidate from 'vuelidate'
import json from '../public/Config.json';
import axios from 'axios'
import VueSweetalert2 from 'vue-sweetalert2';
import 'sweetalert2/dist/sweetalert2.min.css';
import VueSession from 'vue-session'
import { BootstrapVue, BPagination } from 'bootstrap-vue'
import ElementUI from 'element-ui'
import locale from 'element-ui/lib/locale/lang/en'
import 'element-ui/lib/theme-chalk/index.css';
import ToggleButton from 'vue-js-toggle-button';
Vue.use(ToggleButton);
Vue.use(ElementUI, { locale });
Vue.use(BootstrapVue, BPagination);
Vue.use(VueSession)
Vue.config.productionTip = false;
Vue.use(Vuelidate);
Vue.use(VueSweetalert2);
Vue.component('grouping', require('./components/Grouping.vue').default);
Vue.component('modal', require('./components/modalcomponent.vue').default);
Vue.component('licensing', require('./components/Licensing.vue').default);
Vue.component('datepicker', require('./components/DatePicker.vue').default);
Vue.component('login-history-model', require('./components/LicenseHistoryModel.vue').default);
Vue.component('payment-limit-model', require('./components/PaymentLimitModel.vue').default);
Vue.component('loginForm', require('./components/LoginForm.vue').default);
Vue.component('ftp-account-detail', require('./components/FtpAccountDetail.vue').default);
Vue.component('bulk-attachment', require('./components/Attachment/ImportAttachment.vue').default);
Vue.component('attachment-view', require('./components/Attachment/AttachmentView.vue').default);
Vue.component('color-picker', require('./components/ColorPicker.vue').default);

Vue.prototype.$https = axios;
axios.defaults.baseURL = json.ServerIP;

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
