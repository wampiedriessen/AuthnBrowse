<template>
    <div class="card">
        <div class="card-image">
            <figure class="image is-4by3">
                <img src="https://bulma.io/images/placeholders/1280x960.png" alt="Placeholder image">
            </figure>
        </div>
        <div class="card-content">
            <div class="level">
                <div class="level-left">
                    <i v-if="fileinfo.isDirectory" class="fas fa-folder"></i>
                    {{ fileinfo.fileName }}
                </div>
                <div class="level-right has-text-right">
                    <small>@ {{ fileinfo.fileSize }}</small>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: "FileDisplay",
        props: {
            fileinfo: Object
        }
    }
</script>

<style scoped>

</style>