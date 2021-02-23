import { UserManager, WebStorageStateStore } from 'oidc-client'
import axios from 'axios'

const STS_DOMAIN = 'https://localhost:5001'

const settings = {
    userStore: new WebStorageStateStore({ store: window.localStorage }),
    authority: STS_DOMAIN,
    client_id: 'vuejs_code_client',
    redirect_uri: 'https://localhost:44357/login',
    response_type: 'code',
    scope: 'openid profile email',
    post_logout_redirect_uri: 'https://localhost:44357',
    filterProtocolClaims: true,
}

let userManager = new UserManager(settings)

export default {
    
    actions: {
        async getUser() {
            return await userManager.getUser();
        },
        async letsLogin() {
            return await userManager.signinRedirect();
        },
        async login({ dispatch }, formData) {
            dispatch
            formData = {
                UserName: "Empty",
                Password: "Vbrhjcthdbc98@",
                ReturnUrl: formData.returnUrl
            }
            return axios.post('https://localhost:5001/auth/login', formData)
        },
        async logout() {
            return userManager.signoutRedirect();
        },
        async getAccessToken() {
            return userManager.getUser().then((data) => {
                return data.access_token
            })
        }
    }
}