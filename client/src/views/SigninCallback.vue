<template>
  <div>
    SigninCallback page
  </div>
</template>

<script>
export default {
  mounted() {
    this.$store.dispatch('signInCallback');
  }
}
</script>
