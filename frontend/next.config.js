const withImages = require("next-images");

module.exports = withImages({
  images: {
    disableStaticImages: true,
  },
  webpack(config) {
    config.module.rules.push({
      test: /\.svg$/,
      use: [
        {
          loader: "@svgr/webpack",
          options: {
            svgoConfig: {},
          },
        },
      ],
    });
    return config;
  },
});
