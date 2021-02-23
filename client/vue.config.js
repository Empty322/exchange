const CopyWebpackPlugin = require('copy-webpack-plugin');

module.exports = {
    configureWebpack: {
        optimization: {
            splitChunks: {
                minSize: 10000,
                maxSize: 250000,
            }
        },
        plugins: [
            new CopyWebpackPlugin([
                { from: 'node_modules/oidc-client/dist/oidc-client.min.js', to: 'js' }
            ])
        ]
    }
}