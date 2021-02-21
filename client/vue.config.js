const { useCssModule } = require("vue");

module.exports = {
    configureWebpack: {
        optimization: {
            splitChunks: {
                minSize: 10000,
                maxSize: 250000,
            }
        }
    },
    outputDir: '../backend/wwwroot'
}