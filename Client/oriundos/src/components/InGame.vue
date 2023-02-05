<script>
import { defineComponent } from 'vue'
import OriundosWebsocket from '../libs/OriundosWebsocket.ts'

const PROTOCL = "ws://"
const SERVER_IP = "192.168.1.191"
const PORT = "8080"
export default defineComponent({
    name: 'in-game',
    props: {
        roomNumber: {
            type: String,
            default: ''
        }
    },
    data() {
        return {
            connection: undefined,
            playerNumber: 0,
            direction: 0,
            isPlaying: false,
            ready: false,
            finished: false
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
            this.connection = OriundosWebsocket.createWebsocket(PROTOCOL+SERVER_IP+":"+PORT)
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
                case 'FINISHED':
                    this.isPlaying = false
                    this.finished = true
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
                type: "PLAYER",
                roomNumber: this.roomNumber
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
        <h2>You are Player {{ playerNumber }} in room number {{ roomNumber }}</h2>
        <hr />
        <h3 v-if="ready">Ready</h3>
        <h3 v-if="isPlaying && !finished">Game is playing</h3>
        <h3 v-if="finished">You Suck</h3>
        <button v-if="!isPlaying" @click="readyHandler">I'm Ready</button>
        <input type="range" min="-1" max="1" step="0.1" :disabled="!isPlaying || finished" v-model="direction">

    </div>
</template>

<style scoped>
input {
    width: 100%;
}
</style>
