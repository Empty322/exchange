<template>
  <component :is="layout">
      <router-view />
    </component>
</template>

<script>
import EmptyLayout from '@/layout/EmptyLayout'

export default {
  computed: {
    layout () {
      return (this.$route.meta.layout || 'empty') + '-layout'
    }
  },
  components: {
    EmptyLayout
  }
}
</script>

<style lang="scss">
  @import '~materialize-css/dist/css/materialize.min.css';
  body {
    background: #B4B4B4;
  }
</style>