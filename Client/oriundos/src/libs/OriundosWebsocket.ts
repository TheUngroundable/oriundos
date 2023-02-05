class OriundosWebsocket {
    createWebsocket = (serverIp: string) => {
        return new WebSocket(serverIp);
    }

    connect = (connection: WebSocket, onMessage: () => void, onOpen: () => void) => {
        console.log("Starting connection to WebSocket Server");

        connection.onmessage = onMessage

        connection.onopen = onOpen
    }
}

export default new OriundosWebsocket()
