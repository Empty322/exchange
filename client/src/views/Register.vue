<template>
  <div class="container">
    <div class="row card-wrapper">
      <form class="card col s12 m6 offset-m3" @submit.prevent="submitHandler">
          <div class="card-content">
              <span class="card-title">Регистрация</span>
              <div class="input-field">
                  <input
                      id="fistName"
                      type="text"
                      v-model.trim="fistName"
                  >
                  <label for="email">Имя</label>
              </div>
              <div class="input-field">
                  <input
                      id="lastName"
                      type="text"
                      v-model.trim="lastName"
                  >
                  <label for="email">Фамилия</label>
              </div>
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
                  Зарегистрироваться
                  <i class="material-icons right">send</i>
              </button>
              </div>

              <p class="center">
                  Уже зарегистрированы?
                  <router-link to="/login">Войти</router-link>
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
