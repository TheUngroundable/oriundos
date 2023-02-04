<script lang="ts">
import InGame from './components/InGame.vue'
import { defineComponent } from 'vue';

export default defineComponent({
  components: {
    InGame
  },
  data(){
    return {
      isInLobby: false
    }
  }
})
</script>

<template>
  <button v-if="!isInLobby" @click="isInLobby = true">
    Enter Lobby
  </button>
  <in-game v-else />
</template>

<style scoped>
</style>
