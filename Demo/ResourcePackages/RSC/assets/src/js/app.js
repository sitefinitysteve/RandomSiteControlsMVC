import { createApp } from 'vue'

const app = {
  data() {
    return {
      mobileMenu: {
        isOpen: false
      }
    }
  },
  methods: {
    openSidebar() {
      this.mobileMenu.isOpen = true;
    },
    closeSidebar() {
      this.mobileMenu.isOpen = false;
    }
  }
};


createApp(app).mount('#app');
