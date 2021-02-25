<template>
    <div class="container">
        <div class="card" style="padding: 2rem">
            <p v-if="isLoggedIn">User: {{ currentUser }}</p>
            <div class="row">
                <button class="btn" @click="login" v-if="!isLoggedIn">Login</button>
                <button class="btn" @click="logout" v-if="isLoggedIn">Logout</button>
                <button class="btn" @click="getPrivate" v-if="isLoggedIn">getPublic</button>
                <button class="btn" @click="getPrivate" v-if="isLoggedIn">getPrivate</button>
            </div>
            {{message}}

        </div>
    </div>
</template>

<script>
import axios from 'axios'

export default {
    data: () => ({
        message: 'no message',
        currentUser: '',
        isLoggedIn: false,
        accessTokenExpired: false
    }),
    async mounted() {
        await this.$store.dispatch('getUser').then((user) => {
            if (user) {
                this.currentUser = user.profile.name;
                this.accessTokenExpired = user.expired;
                this.isLoggedIn = (user !== null && !user.expired);
            }
        });
    },
    methods: {
        async login() {
            await this.$store.dispatch('signinRedirect');
        },
        async logout() {
            await this.$store.dispatch('logout');
        },
        async getPublic() {
            const authorizationHeader = 'Authorization';
            await this.$store.dispatch('getAccessToken').then((userToken) => {
                axios.defaults.headers.common[authorizationHeader] = `Bearer ${userToken}`;
 
                axios.get('https://localhost:6001/test/public-data/')
                    .then((response) => {
                        console.log(response)
                        this.message = response.data;
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            });
        },
        async getPrivate() {
            const authorizationHeader = 'Authorization';
            await this.$store.dispatch('getAccessToken').then((userToken) => {
                axios.defaults.headers.common[authorizationHeader] = `Bearer ${userToken}`;
 
                axios.get('https://localhost:6001/test/private-data/')
                    .then((response) => {
                        console.log(response)
                        this.message = response.data;
                    })
                    .catch((error) => {
                        console.log(error);
                    });
            });
        }
    }
}
</script>

