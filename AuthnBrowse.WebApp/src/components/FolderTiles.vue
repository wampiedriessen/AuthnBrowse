<template>
  <div>
    <div v-if="!files" class="container is-fluid">
      <div class="notification">
        <i class="fas fa-spinner fa-pulse"/> <strong>Loading your files....</strong>
      </div>
    </div>
    <div v-else class="columns">
      <div class="column is-half-mobile is-one-third-desktop is-one-quarter-widescreen is-one-fifth-fullhd" v-for="(file, i) in files" v-bind:key="i">
        <file-display :file-info="file"/>
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
