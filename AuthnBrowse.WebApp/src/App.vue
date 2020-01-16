<template>
  <div id="app">
    <!-- <navbar /> -->
    <section class="hero is-info">
      <div class="hero-body">
        <div class="container">
          <h1 class="title">
            AuthnBrowse
          </h1>
          <p class="subtitle">
            For Browsing your Authns
          </p>
        </div>
      </div>
    </section>
    <hello-world msg="Hello there!"></hello-world>
  </div>
</template>

<script>
import HelloWorld from './components/HelloWorld.vue'

export default {
  name: 'app',
  components: {
    HelloWorld
  }
}
</script>

<style lang="scss">
  @import 'assets/sass/site';
</style>
