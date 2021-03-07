import { UserManager, WebStorageStateStore } from "oidc-client";
import axios from "axios";

const STS_DOMAIN = "https://localhost:5001";
const settings = {
  userStore: new WebStorageStateStore({ store: window.localStorage }),
  authority: STS_DOMAIN,
  client_id: "vuejs_code_client",
  redirect_uri: "https://localhost:44357/signin-callback",
  response_type: "code",
  scope: "openid profile email TestAPI",
  post_logout_redirect_uri: "https://localhost:44357",
};
let userManager = new UserManager(settings);

export default {
  actions: {
    async getUser() {
      return await userManager.getUser();
    },
    async signinRedirect() {
      return await userManager.signinRedirect();
    },
    async login({ commit }, formData) {
      try {
        await axios
          .post(`${STS_DOMAIN}/auth/login`, formData, { withCredentials: true })
          .then((response) => {
            if (response.data.isOk)
              window.location.href = response.data.redirectUrl;
          });
      } catch (e) {
        commit("setError", e);
      }
    },
    async register({ commit }, formData) {
      try {
        await axios
          .post(`${STS_DOMAIN}/auth/register`, formData, {
            withCredentials: true,
          })
          .then((response) => {
            if (response) window.location.href = "/";
          });
      } catch (e) {
        commit("setError", e);
      }
    },
    async signInCallback() {
      userManager.signinRedirectCallback().then(() => {
        window.location.href = "/";
      });
    },
    async logout() {
      return await userManager.signoutRedirect();
    },
    async removeUser() {
      return await userManager.removeUser();
    },
    async getAccessToken() {
      return await userManager.getUser().then((data) => {
        return data.access_token;
      });
    },
  },
};
