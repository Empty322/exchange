<template>
  <div class="container">
    <div class="row card-wrapper">
      <form class="card col s12 m6 offset-m3" @submit.prevent="submitHandler">
        <div class="card-content">
          <span class="card-title">Регистрация</span>
          <div class="input-field">
            <label for="userName">Имя</label>
            <input
              id="userName"
              type="text"
              v-model.trim="userName"
              :class="{ invalid: v$.userName.$dirty && v$.userName.$invalid }"
            />
            <div v-if="v$.userName.$dirty && v$.userName.$invalid">
              <small class="helper-text invalid" v-if="v$.userName.required.$invalid">Необходимо заполнить имя</small>
              <small class="helper-text invalid" v-if="v$.userName.minLength.$invalid">Имя должно быть не менее {{ v$.userName.minLength.$params.min }} символов</small>
            </div>
          </div>
          <div class="input-field">
            <label for="email">Email</label>
            <input
              id="email"
              type="text"
              v-model.trim="email"
              :class="{ invalid: v$.email.$dirty && v$.email.$invalid }"
            />
            <div v-if="v$.email.$dirty && v$.email.$invalid">
              <small class="helper-text invalid" v-if="v$.email.required.$invalid">Необходимо заполнить email</small>
              <small class="helper-text invalid" v-if="v$.email.email.$invalid">Не верный формат email адреса</small>
            </div>
          </div>
          <div class="input-field">
            <label for="password">Пароль</label>
            <input
              id="password"
              type="password"
              v-model.trim="password"
              :class="{ invalid: v$.password.$dirty && v$.password.$invalid }"
            />
            <div v-if="v$.password.$dirty && v$.password.$invalid">
              <small class="helper-text invalid" v-if="v$.password.required.$invalid">Необходимо заполнить пароль</small>
              <small class="helper-text invalid" v-if="v$.password.minLength.$invalid">Пароль должен быть не менее {{ v$.password.minLength.$params.min }} символов длиной</small>
              <small class="helper-text invalid" v-if="v$.password.alphaNum.$invalid">Пароль должен содержать только цифры или буквы латинского алфавита</small>
            </div>
          </div>
          <div class="input-field">
            <label for="confirmPassword">Повторите пароль</label>
            <input
              id="confirmPassword"
              type="password"
              v-model.trim="confirmPassword"
              :class="{ invalid: v$.confirmPassword.$dirty && v$.confirmPassword.$invalid }"
            />
            <div v-if="v$.confirmPassword.$dirty && v$.confirmPassword.$invalid">
              <small class="helper-text invalid" v-if="v$.confirmPassword.sameAsPassword.$invalid">Пароли должны совпадать</small>
            </div>
          </div>
        </div>
        <div class="card-action">
          <div class="center">
            <button class="btn waves-effect waves-light auth-submit" type="submit">
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
  sameAs,
} from "@vuelidate/validators";


export default {
  name: "Register", // Чтобы во vue devtools у компонента было имя, а не просто anonimous component
  setup() {
    return { v$: useVuelidate() };
  },
  data() {
    return {
      userName: "",
      email: "",
      password: "",
      confirmPassword: "",
    }
  },
  validations() {
    return {
      userName: {
        required,
        minLength: minLength(6),
        $autoDirty: true,
      },
      email: { 
        required, 
        email, 
        $autoDirty: true 
      },
      password: {
        required,
        minLength: minLength(6),
        alphaNum,
        $autoDirty: true,
      },
      confirmPassword: {
        sameAsPassword: sameAs(this.password),
        $autoDirty: true,
      }
    }
  },
  methods: {
    async submitHandler() {
      this.v$.$touch();
      if (this.v$.$error) {
        this.$message("Необходимо корректро заполнить все поля");
        return;
      }
      const formData = {
        userName: this.userName,
        email: this.email,
        password: this.password,
        confirmPassword: this.confirmPassword,
      };
      try {
        await this.$store.dispatch("register", formData);
      }
      catch (e){
        console.log(e);
        this.$error("Не удачная регистрация");
      }
    },
    async login() {
      try {
        await this.$store.dispatch("signinRedirect");
      }
      catch (e) {
        this.$error("Ошибка сети");
      }
    },
  },
};
</script>

<style scoped>
  .card-wrapper {
    margin-top: 20vh;
  }
</style>
