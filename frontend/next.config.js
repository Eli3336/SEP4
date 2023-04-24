const withImages = require("next-images");

module.exports = withImages({
  webpack(config, options) {
    config.module.rules.push({
      test: /\.svg$/,
      use: ["@svgr/webpack", "react-svg-loader"],
    });

    return config;
  },
});
