import { UserManager, WebStorageStateStore } from 'oidc-client'
import axios from 'axios'

const STS_DOMAIN = 'https://localhost:5001'

const settings = {
    userStore: new WebStorageStateStore({ store: window.localStorage }),
    authority: STS_DOMAIN,
    client_id: 'vuejs_code_client',
    redirect_uri: 'https://localhost:44357/callback.html',
    automaticSilentRenew: true,
    silent_redirect_uri: 'https://localhost:44357/silent-renew.html',
    response_type: 'code',
    scope: 'openid profile',
    post_logout_redirect_uri: 'https://localhost:44357/',
    filterProtocolClaims: true,
}

let userManager = new UserManager(settings)

export default {
    
    actions: {
        async getUser() {
            return userManager.getUser();
        },
        async login({ dispatch }, formData) {
            dispatch
            axios.post('https://localhost:5001/auth/login', formData)
                    .then((response) => {
                        console.log(response)
                    })
                    .catch((error) => {
                        console.log(error);
                    });
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