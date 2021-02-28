export default {
    install(app) {
        app.config.globalProperties.$message = text => window.M.toast({ html: text })
        app.config.globalProperties.$error = text => window.M.toast({ html: `[Ошибка]: ${text}` })
    }
}
  