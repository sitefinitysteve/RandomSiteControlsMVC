const mix = require('laravel-mix');
require("laravel-mix-vue3");
const tailwindcss = require('tailwindcss');

mix.webpackConfig({
   resolve: {
      alias: {
          vue: "vue/dist/vue.esm-bundler.js"
      }
  }
});

mix.sass('assets/src/sass/app.scss', './assets/public/css')
   .options({
      processCssUrls: false,
      postCss: [ tailwindcss('./tailwind.config.js') ],
   });


mix.vue3('assets/src/js/app.js', './assets/public/js');
