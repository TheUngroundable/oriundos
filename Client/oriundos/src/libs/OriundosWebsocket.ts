class OriundosWebsocket {
    createWebsocket = () => {
        return new WebSocket("ws://192.168.1.191:8080");
    }

    connect = (connection: WebSocket, onMessage: () => void, onOpen: () => void) => {
        console.log("Starting connection to WebSocket Server");

        connection.onmessage = onMessage

        connection.onopen = onOpen
    }
}

export default new OriundosWebsocket()
