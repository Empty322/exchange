<template>
    <div>
        <div class="home">
            <p v-if="isLoggedIn">User: {{ currentUser }}</p>
            <button class="btn" @click="login" v-if="!isLoggedIn">Login</button>
            <button class="btn" @click="logout" v-if="isLoggedIn">Logout</button>
            <button class="btn" @click="getProtectedApiData" v-if="isLoggedIn">Get API data</button>
        </div>
 
        <div>
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
        await this.$store.dispatch('getAccessToken')
        await this.$store.dispatch('getUser').then((user) => {
            this.currentUser = user.profile.name;
            this.accessTokenExpired = user.expired;

            this.isLoggedIn = (user !== null && !user.expired);
        });
    },
    methods: {
        async login() {
            await this.$store.dispatch('letsLogin');
        },
        async logout() {
            await this.$store.dispatch('logout');
        },
        getPublic() {

        },
        async getPrivate() {
            const authorizationHeader = 'Authorization';
            await this.$store.dispatch('getAccessToken').then((userToken) => {
                axios.defaults.headers.common[authorizationHeader] = `Bearer ${userToken}`;
 
                axios.get('https://localhost:44355/test/private-data/')
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