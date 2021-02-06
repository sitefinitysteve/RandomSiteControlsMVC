const colors = require('tailwindcss/colors')

module.exports = {
  purge: [
    //Theme purge
    './MVC/Views/**/*.cshtml',
    './MVC/GridSystem/**/*.cshtml',
    './MVC/Views/**/*.js',

    //Widget purge
    '../../MVC/Views/**/*.cshtml',
  ],
  darkMode: false, // or 'media' or 'class'
  theme: {
    extend: {
      colors: {
        teal: colors.teal,
        cyan: colors.cyan,
      }
    },
  },
  variants: {
    extend: {
    },
  },
  plugins: [
    require('@tailwindcss/forms'),
    require('@tailwindcss/aspect-ratio'),
  ],
}
