<script lang="ts">
import InGame from './components/InGame.vue'
import { defineComponent } from 'vue';

export default defineComponent({
  components: {
    InGame
  },
  data(){
    return {
      isInLobby: false,
      roomNumber: ""
    }
  }
})
</script>

<template>
  <template v-if="!isInLobby">
    <button @click="isInLobby = true">
      Enter Lobby
    </button>
    <input type="text" v-model="roomNumber" />
  </template>
  <in-game v-else :roomNumber="roomNumber" />
</template>

<style scoped>
</style>
