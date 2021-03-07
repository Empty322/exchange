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
      await userManager.signinRedirect();
    },
    async login({ }, formData) {
      const response = await axios.post(`${STS_DOMAIN}/auth/login`, formData, { withCredentials: true })
      if (response.data.isOk)
        window.location.href = response.data.redirectUrl;
    },
    async register({ }, formData) {
      let response = await axios.post(`${STS_DOMAIN}/auth/register`, formData, { withCredentials: true })
      if (response.data.isOk)
        window.location.href = "/";
    },
    async signInCallback() {
      await userManager.signinRedirectCallback()
      window.location.href = "/";
    },
    async logout() {
      await userManager.signoutRedirect();
    },
    async removeUser() {
      await userManager.removeUser();
    },
    async getAccessToken() {
      return await userManager.getUser().then((data) => {
        return data.access_token;
      });
    },
  },
};
