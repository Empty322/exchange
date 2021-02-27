<template>
  <div class="container">
    <div class="row card-wrapper">
      <div class="card col s12 m6 offset-m3">
        <div class="center">
          <Loader/>
		</div>
        <div class="center">
          Идет вход в приложение...
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Loader from '@/components/Loader'

export default {
  mounted() {
    this.$store.dispatch('signInCallback');
  },
  components: {
    Loader
  }
}
</script>

<style scoped>
	.card-wrapper {
		margin-top: 20vh;
	}

	.card {
		padding: 3rem;
	}
</style>