<script>
import { defineComponent } from 'vue'
import OriundosWebsocket from '../libs/OriundosWebsocket.ts'
export default defineComponent({
    name: 'in-game',
    data() {
        return {
            connection: undefined,
            playerNumber: 0,
            direction: 0,
            isPlaying: false,
            ready: false
        }
    },
    mounted() {
        this.connect()
    },
    watch: {
        direction(prev, next) {
            if (prev !== next) {
                this.sendMovementDirection()
            }
        }
    },
    methods: {
        connect() {
            this.connection = OriundosWebsocket.createWebsocket()
            OriundosWebsocket.connect(this.connection, this.onMessage, this.onOpen)
        },
        onMessage() {
            const message = JSON.parse(event.data)
            console.log(message)
            if (!message.command) {
                return
            }
            switch (message.command) {
                case 'CONNECTION':
                    if (message.id) {
                        this.playerNumber = message.id
                    }
                    break;
                case 'STARTED':
                    this.isPlaying = true
                    break;
                case 'STOPPED':
                    this.isPlaying = false
                    break;
            }
        },
        onOpen() {
            console.log(event);
            console.log("Successfully connected to the echo websocket server...");
            this.sendMessage('JOINED', !this.ready)
        },
        readyHandler(){
            this.sendMessage('READY', !this.ready)
            this.ready = !this.ready
        },
        sendMovementDirection() {
            if (!this.isPlaying) {
                return
            }
            this.sendMessage('MOVEMENT', this.direction)
        },
        sendMessage(command, value){
            const message = {
                id: this.playerNumber,
                command,
                value,
                type: "PLAYER"
            };
            console.log("Player "+this.playerNumber+" said "+message);
            this.connection.send(JSON.stringify(message));
        }
    }
})
</script>

<template>
    <div>
        <h1>Oriundos Controller</h1>
        <h2>You are Player {{ playerNumber }}</h2>
        <hr />
        <h3 v-if="ready">Ready</h3>
        <h3 v-if="isPlaying">Game is playing</h3>
        <input type="range" min="-1" max="1" step="0.1" :disabled="!isPlaying" v-model="direction">
        <button v-if="!isPlaying" @click="readyHandler">I'm Ready</button>
    </div>
</template>

<style scoped>
input {
    width: 100%;
}
</style>
