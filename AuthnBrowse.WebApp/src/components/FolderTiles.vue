<template>
  <div class="container is-fluid">
    <div v-if="!files" class="notification">
        <i class="fas fa-spinner fa-pulse"></i> <strong>Loading your files....</strong>
    </div>
    <div v-else class="columns">
      <div class="column is-half-desktop is-one-third-widescreen is-one-quarter-fullhd" v-for="(file, i) in files" v-bind:key="i">
        <file-display v-bind:fileinfo="file"></file-display>
      </div>
    </div>
  </div>
</template>

<script>
import DependencyContainer from "../services/DependencyContainer";
import FileApiService from '../services/FileApiService';

import FileDisplay from '../components/FileDisplay';

export default {
  name: 'FolderTiles',
  props: {
    container: DependencyContainer
  },
  components: {
    FileDisplay,
  },
  methods: {
    getFiles: async function () {
      this.files = await this.fileApiService.GetFiles();
    },
  },
  data: function () {
    return {
      files: null,
      fileApiService: this.container.resolve(FileApiService.IName),
    }
  },
  mounted() {
    this.getFiles(); // F&F
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
