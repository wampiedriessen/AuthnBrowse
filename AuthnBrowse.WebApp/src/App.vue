<template>
  <div id="app">
    <!-- <navbar /> -->
    <section class="hero is-info">
      <div class="hero-body">
        <div class="container">
          <div class="columns">
            <div class="column is-three-quarters">
              <div class="container">
                <h1 class="title">
                  AuthnBrowse
                </h1>
                <h2 class="subtitle">
                  For Browsing your Authns
                </h2>
              </div>
            </div>
            <div class="column">
              <div class="box">
                <login-box :container="container"></login-box>
              </div>
            </div>
          </div>
        </div>
      </div>
    </section>
    <section style="margin-top: 10px">
      <folder-tiles :container="container"></folder-tiles>
    </section>
  </div>
</template>

<script>
import DependencyContainer from "./services/DependencyContainer";
import FolderTiles from './components/FolderTiles.vue';
import LoginBox from './components/LoginBox.vue';
import FileApiService from "./services/FileApiService";
import WebAuthnService from "./services/WebAuthnService";

const dependencyContainer = new DependencyContainer();
dependencyContainer.registerContainer(FileApiService.IName, new FileApiService({
  ApiBaseUrl: "https://localhost:5001/files"
}));
dependencyContainer.registerContainer(WebAuthnService.IName, new WebAuthnService());

export default {
  name: 'app',
  components: {
    FolderTiles,
    LoginBox
  },
  data() {
    return {
      container: dependencyContainer,
    };
  },
}
</script>

<style lang="scss">
  @import 'assets/sass/site';
</style>