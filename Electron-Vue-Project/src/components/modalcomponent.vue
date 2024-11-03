<template>
    <div>
        <transition name="modal">
            <div class="modal modal-mask" @click="close" v-show="show" style="display:block; overflow:auto" v-if="extraLarge==true">
                <div class="modal-dialog modal-notice" :class="{'modal-xl':modalLarge == true }">
                    <div class="modal-content" @click.stop>
                        <slot></slot>
                    </div>
                </div>
            </div>
            <div class="modal modal-mask" @click="close" v-show="show" style="display:block; overflow:auto" v-else>
                <div class="modal-dialog modal-notice" :class="{'modal-lg':modalLarge == true }">
                    <div class="modal-content" @click.stop>
                        <slot></slot>
                    </div>
                </div>
            </div>
        </transition>
    </div>
</template>
<script>
    export default {

        props: ['show', 'modalLarge','extraLarge'],
        data: function () {
            return {
                showModal: false
            }
        },
        methods: {
            close: function () {

                this.$emit('close');
            }
        },
        mounted: function () {

            this.showModal = this.show;
            document.addEventListener("keydown", function (e) {
                if (this.show && e.keyCode === 27) {
                    this.close();
                }
            });
        },
        watch: {
            showModal: function (value) {
                if (value) {
                    document.querySelector("html").classList.remove("perfect-scrollbar-on");
                    document.querySelector('body').style.overflow = 'hidden';
                }
            }
        },
        destroyed: function() {
            document.querySelector("html").classList.add("perfect-scrollbar-on");
            document.querySelector('body').style.overflow = 'auto';
        }
    }
</script>
<style scoped>
    .modal-mask {
        position: fixed;
        z-index: 1029;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.5);
        display: table;
        -ms-transition: opacity 2s ease !important;
        -o-transition: opacity 2s ease !important;
        -webkit-transition: opacity 2s ease !important;
        transition: opacity 2s ease !important;
    }
    .modal-xl {
        max-width: 1100px !important
    }
</style>
