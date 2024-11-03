<template>
    <div>
        <el-date-picker v-model="inputValue"
                        type="date"
                        placeholder="Pick a day"
                        style="width: 100%"
                        format='dd MMM yyyy'
                        :picker-options="pickerOptions">
        </el-date-picker>
    </div>
</template>
<script>
    import moment from 'moment'
    export default {
        props: ['value', 'dropdowndatecss'],
        data: function () {
            return {
                dropdownDatecss: "",
                inputValue: '',
                editField: '',
                pickerOptions: {
                    shortcuts: [{
                        text: 'Today',
                        onClick(picker) {
                            picker.$emit('pick', new Date());
                        }
                    }, {
                        text: 'Yesterday',
                        onClick(picker) {
                            const date = new Date();
                            date.setTime(date.getTime() - 3600 * 1000 * 24);
                            picker.$emit('pick', date);
                        }
                    }, {
                        text: 'A week ago',
                        onClick(picker) {
                            const date = new Date();
                            date.setTime(date.getTime() - 3600 * 1000 * 24 * 7);
                            picker.$emit('pick', date);
                        }
                    }]
                },
            }
        },
        mounted: function () {
            ; //eslint-disable-line
            this.dropdownDatecss = this.dropdowndatecss;
            this.inputValue = this.value;
        },
        updated: function () {
            ; //eslint-disable-line
            var input = moment(String(this.inputValue)).format('DD MMM YYYY');
            this.inputValue = input;
            this.$emit('input', this.inputValue);
        }
    }
</script>