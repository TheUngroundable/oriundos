const WebSocket = require('ws')
const wss = new WebSocket.Server({ port: 8080 }, () => {
    console.log('server started')
})

const rooms = {}

const numberOfPlayers = 2

const COMMANDS = {
    CONNECTION: "CONNECTION",
    JOINED: 'JOINED',
    MOVEMENT: 'MOVEMENT',
    READY: "READY",
    FINISHED: 'FINISHED',
    STARTED: 'STARTED'
}

wss.on('connection', function connection(ws) {

    const data = {
        command: COMMANDS.CONNECTION,
    }

    ws.send(JSON.stringify(data))

    ws.on('message', (data) => {
        const json = JSON.parse(data)

        if (json.command === COMMANDS.JOINED) {
            if (json.type === 'UNITY' && !rooms[json.roomNumber]) {
                const room = {
                    unity: ws,
                    players: [],
                    roomNumber: json.roomNumber,
                    numberOfReady: 0,
                    maxPlayers: numberOfPlayers,
                    isPlaying: false,
                }
                rooms[json.roomNumber] = room
                console.log("Unity has created a game for room " + json.roomNumber)
            } else if (json.type === 'PLAYER' && rooms[json.roomNumber]) {
                console.log(json.roomNumber)
                const room = rooms[json.roomNumber]
                const playerNumber = room.players.length
                const player = {
                    id: playerNumber,
                    ws,
                    isReady: false,
                }
                room.players.push(player)

                const data = {
                    id: playerNumber,
                    command: COMMANDS.CONNECTION,
                    roomNumber: json.roomNumber,
                    isPlaying: false,
                }

                ws.send(JSON.stringify(data))
                console.log("Player "+playerNumber+" has joined the room")


                const unity = room.unity
                const userJoinedNotify = {
                    id: playerNumber,
                    command: COMMANDS.JOINED,
                    roomNumber: json.roomNumber,
                    value: 1,
                    isPlaying: false,
                }
                unity.send(JSON.stringify(userJoinedNotify))
            }
        }

        if (json.command === COMMANDS.MOVEMENT) {

            console.log(json.roomNumber + " | Player " + json.id + " has moved to " + json.value)
            const message = {
                id: json.id,
                command: json.command,
                value: json.value,
                roomNumber: json.roomNumber,
                isPlaying: true
            }
            const unityWs = rooms[json.roomNumber].unity
            unityWs.send(JSON.stringify(message))
        }

        if (json.command === COMMANDS.READY) {

            const room = rooms[json.roomNumber]
            const player = room.players.find(player => player.id === json.id)

            player.isReady = json.value

            if (json.value) {
                console.log("Player " + json.id + " is ready")
            } else {
                console.log("Player " + json.id + " is not ready")
            }
        }

        if (json.command === COMMANDS.FINISHED) {
            const room = rooms[json.roomNumber]
            room.isPlaying = false
            const message = {
                id: json.id,
                command: json.command,
                value: json.value,
                roomNumber: json.roomNumber,
                isPlaying: false
            }
            const encodedMessage = JSON.stringify(message)
            console.log("game over")
            rooms[json.roomNumber].players.forEach(player => player.ws.send(encodedMessage))
        }

        if (rooms[json.roomNumber]) {
            const room = rooms[json.roomNumber]
            const allPlayersAreReady = room.players.length > 0 && room.players.every(player => player.isReady)
            console.log("All players are ready: "+allPlayersAreReady)
            if (allPlayersAreReady && room) {
                const message = {
                    id: -1,
                    command: COMMANDS.STARTED,
                    value: 1,
                    roomNumber: json.roomNumber,
                    isPlaying: true,
                }
                room.isPlaying = true
                const encodedMessage = JSON.stringify(message)
                room.players.forEach(player => player.ws.send(encodedMessage))
                room.unity.send(encodedMessage)
                console.log("Game has started")
            }
        }


    })
})
wss.on('listening', () => {
    console.log('Node server started on on 8080')
})