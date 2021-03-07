const fs = require("fs");

module.exports = {
  configureWebpack: {
    optimization: {
      splitChunks: {
        minSize: 10000,
        maxSize: 250000,
      },
    },
  },
  devServer: {
    https: {
      key: fs.readFileSync("./certs/localhost.key"),
      cert: fs.readFileSync("./certs/localhost.crt"),
    },
    port: 44357,
    public: "localhost:44357",
  },
};
