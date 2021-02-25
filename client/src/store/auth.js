import { UserManager, WebStorageStateStore } from 'oidc-client'
import axios from 'axios'

const STS_DOMAIN = 'https://localhost:5001'
const settings = {
    userStore: new WebStorageStateStore({ store: window.localStorage }),
    authority: STS_DOMAIN,
    client_id: 'vuejs_code_client',
    redirect_uri: 'https://localhost:44357/callback',
    response_type: 'code',
    scope: 'openid profile email TestAPI',
    post_logout_redirect_uri: 'https://localhost:44357',
    filterProtocolClaims: true,
}

let userManager = new UserManager(settings)

export default {
    actions: {
        async getUser() {
            return await userManager.getUser();
        },
        async signinRedirect() {
            return await userManager.signinRedirect();
        },
        async login({ }, formData) {
            await axios.post('https://localhost:5001/auth/login', formData, { withCredentials: true }).then(async (response) => {
                if (response.data.isOk) {
                    window.location.href = response.data.redirectUrl
                }
                else
                  console.log(response)
              })
              .catch((error) => {
                  console.log(error);
              });
        },
        async signInCallback() {
            userManager.signinRedirectCallback().then(function (data) {
                console.log(data);
                window.location.href = "/";
            }).catch(function(e) {
                console.error(e);
            });
        },
        async logout() {
            return await userManager.signoutRedirect();
        },
        async getAccessToken() {
            return await userManager.getUser().then((data) => {
                return data.access_token
            })
        }
    }
}