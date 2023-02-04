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
            }
        },
        onOpen() {
            console.log(event);
            console.log("Successfully connected to the echo websocket server...");
        },
        sendMovementDirection() {
            const message = {
                id: this.playerNumber,
                command: 'MOVEMENT',
                direction: this.direction
            }
            console.log("Sending direction to server", message)
            this.connection.send(JSON.stringify(message));
        }
    }
})
</script>

<template>
    <div>
        <h1>In Game</h1>
        <input type="range" min="-1" max="1" step="0.1" v-model="direction">
    </div>
</template>

<style scoped>

</style>
