<template>
  <div class="container">
    <div class="row card-wrapper">
      <form class="card col s12 m6 offset-m3" @submit.prevent="submitHandler">
        <div class="card-content">
          <span class="card-title">Регистрация</span>
          <div class="input-field">
            <input
              id="userName"
              type="text"
              v-model.trim="userName"
              :class="{ invalid: v$.userName.$dirty && v$.userName.$invalid }"
            />
            <label for="userName">Имя</label>
            <div v-if="v$.userName.$invalid">
              <small
                class="helper-text invalid"
                v-for="(error, index) of v$.userName.$errors"
                :key="index"
                >{{ error.$message }}</small
              >
            </div>
          </div>
          <div class="input-field">
            <input
              id="email"
              type="text"
              v-model.trim="email"
              :class="{ invalid: v$.email.$dirty && v$.email.$invalid }"
            />
            <label for="email">Email</label>
            <div v-if="v$.email.$invalid">
              <small
                class="helper-text invalid"
                v-for="(error, index) of v$.email.$errors"
                :key="index"
                >{{ error.$message }}</small
              >
            </div>
          </div>
          <div class="input-field">
            <input
              id="password"
              type="password"
              v-model.trim="password"
              :class="{ invalid: v$.password.$dirty && v$.password.$invalid }"
            />
            <div v-if="v$.password.$invalid">
              <small
                class="helper-text invalid"
                v-for="(error, index) of v$.password.$errors"
                :key="index"
                >{{ error.$message }}</small
              >
            </div>

            <label for="password">Пароль</label>
          </div>
          <div class="input-field">
            <input
              id="confirmPassword"
              type="password"
              v-model.trim="confirmPassword"
            />
            <label for="confirmPassword">Повторите пароль</label>
            <div>
              <small
                class="helper-text invalid"
                v-for="(error, index) of v$.confirmPassword.$errors"
                :key="index"
                >{{ error.$message }}</small
              >
            </div>
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
            <router-link to="#" @click.prevent="login">Войти</router-link>
          </p>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import useVuelidate from "@vuelidate/core";
import {
  email,
  required,
  minLength,
  alphaNum,
  // sameAs,
} from "@vuelidate/validators";
export default {
  setup() {
    return { v$: useVuelidate() };
  },
  data: () => ({
    userName: "",
    email: "",
    password: "",
    confirmPassword: "",
  }),
  validations() {
    return {
      userName: {
        required,
        minLength: minLength(6),
        $autoDirty: true,
      },
      email: { email, required, $autoDirty: true },
      password: {
        required,
        minLength: minLength(6),
        alphaNum,
        $autoDirty: true,
      },
      confirmPassword: {
        required,
        // sameAsPassword: sameAs(("password")),
      },
    };
  },
  methods: {
    submitHandler: async function () {
      this.v$.$touch();
      if (this.v$.$error) return;
      const formData = {
        userName: this.userName,
        email: this.email,
        password: this.password,
        confirmPassword: this.confirmPassword,
      };
      await this.$store.dispatch("register", formData);
    },
    async login() {
      await this.$store.dispatch("signinRedirect");
    },
  },
};
</script>

<style scoped>
.card-wrapper {
  margin-top: 20vh;
}
</style>
