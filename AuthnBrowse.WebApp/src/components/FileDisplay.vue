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
                    <i v-if="fileInfo.isDirectory" class="fas fa-folder"/>
                    {{ fileInfo.fileName }}
                </div>
                <div class="level-right has-text-right">
                    <small>@ {{ fileInfo.fileSize }}</small>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        name: "FileDisplay",
        props: {
            fileInfo: Object
        }
    }
</script>

<style scoped>

</style>