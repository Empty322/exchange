<template>
  <div class="container">
    <div class="row card-wrapper">
      <form class="card col s12 m6 offset-m3" @submit.prevent="submitHandler">
          <div class="card-content">
              <span class="card-title">Войти</span>
              <div class="input-field">
                <input
                    id="email"
                    type="text"
                    v-model.trim="email"
                >
                <label for="email">Email</label>
              </div>
              <div class="input-field">
                <input
                    id="password"
                    type="password"
                    v-model.trim="password"
                >
                <label for="password">Пароль</label>
              </div>
          </div>
          <div class="card-action">
              <div class="center">
              <button
                  class="btn waves-effect waves-light auth-submit"
                  type="submit"
              >
                  Войти
                  <i class="material-icons right">send</i>
              </button>
              </div>

              <p class="center">
                Нет аккаунта?
                <router-link to="/register">Зарегистрироваться</router-link>
              </p>
          </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  data: () => ({
    email: '',
    password: ''
  }),
  methods: {
    submitHandler: async function () {
      const formData = {
        email: this.email,
        password: this.password
      }
      try {
        await this.$store.dispatch('login', formData)
        this.$router.push('/')
      } catch (e) {
        console.log(e)
      }
    }
  }
}
</script>

<style scoped>
  .card-wrapper {
    margin-top: 20vh;
  }
</style>
