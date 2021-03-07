<template>
  <div class="container">
    <div class="row card-wrapper">
      <form class="card col s12 m6 offset-m3" @submit.prevent="submitHandler">
        <div class="card-content">
          <span class="card-title">Войти</span>
          <div class="input-field">
            <input id="userName" type="text" v-model.trim="userName" />
            <label for="userName">Имя пользователя</label>
          </div>
          <div class="input-field">
            <input id="password" type="password" v-model.trim="password" />
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
    userName: "",
    password: "",
  }),
  methods: {
    submitHandler: async function () {
      const formData = {
        userName: this.userName,
        password: this.password,
        returnUrl: this.$route.query.ReturnUrl,
      };

      await this.$store.dispatch("login", formData);
    },
  },
};
</script>

<style scoped>
.card-wrapper {
  margin-top: 20vh;
}
</style>
