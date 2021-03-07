<template>
  <div>
    <router-view />
  </div>
</template>

<script>
import messages from "@/utils/messages";
export default {
  computed: {
    error() {
      return this.$store.getters.error;
    },
  },
  watch: {
    error(e) {
      this.$error(messages[e.response.status]);
    },
  },
};
</script>
