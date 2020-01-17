<template>
    <div class="has-text-centered">
        <strong>Geen inhoud?!</strong>
        <br>
        <button class="button is-primary">Authn!</button>
    </div>
</template>

<script>
    import DependencyContainer from "../services/DependencyContainer";
    import WebAuthnService from "../services/WebAuthnService";

    export default {
        name: "LoginBox",
        props: {
            container: DependencyContainer
        },
        data() {
            return {
                webAuthnService: this.container.resolve(WebAuthnService.IName)
            }
        }
    }
</script>

<style scoped>

</style>