import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import messages from './utils/message.plugin'
import 'materialize-css/dist/js/materialize.min'

createApp(App)
    .use(messages)
    .use(store)
    .use(router)
    .mount('#app')
