const WebSocket = require('ws')
const wss = new WebSocket.Server({ port: 8080 }, () => {
    console.log('server started')
})

const rooms = {}

const numberOfPlayers = 2

wss.on('connection', function connection(ws) {

    const data = {
        command: "CONNECTION",
    }
    
    ws.send(JSON.stringify(data))

    ws.on('message', (data)=>{
        const json = JSON.parse(data)

        if(json.command === 'JOINED'){
            if(json.type === 'UNITY' && !rooms[json.value]){
                const room = {
                    unity: ws,
                    players: [],
                    roomNumber: json.roomNumber,
                    numberOfReady: 0,
                    maxPlayers: numberOfPlayers,
                    isPlaying: false,
                }
                rooms[json.roomNumber] = room
                console.log("Unity has created a game for room "+json.roomNumber)
            } else if(json.type === 'PLAYER'){
                console.log(json.roomNumber)
                let playerNumber = rooms[json.roomNumber].players.length
                playerNumber++;
                const player = {
                    id: playerNumber,
                    ws,
                }
                rooms[json.roomNumber].players.push(player)

                const data = {
                    id: playerNumber,
                    command: "CONNECTION",
                    roomNumber: json.roomNumber,
                }

                ws.send(JSON.stringify(data))            
                console.log("Player has joined the room")
            }
        }

        if(json.command === 'MOVEMENT' && json.isPlaying){

            console.log(json.roomNumber+" | Player "+json.id+" has moved to "+json.value)
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

        if(json.command === 'READY'){

            const room = rooms[json.roomNumber]

            if(json.value){
                room.numberOfReady++;
                console.log("Player "+json.id+" is ready")
            } else {
                room.numberOfReady--;
                console.log("Player "+json.id+" is not ready")
            }
        }

        if(json.command === 'FINISHED'){
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
            rooms[json.roomNumber].players.forEach(player => player.ws.send(encodedMessage))
        }

        if(rooms[json.roomNumber] && rooms[json.roomNumber].numberOfReady == rooms[json.roomNumber].maxPlayers){
            const room = rooms[json.roomNumber]
            const message = {
                id: -1,
                command: 'STARTED',
                value: 1,
                roomNumber: json.roomNumber
            }
            room.isPlaying = true
            const encodedMessage = JSON.stringify(message)
            room.players.forEach(player => player.ws.send(encodedMessage))
            room.unity.send(encodedMessage)
            console.log("Game has started")
        }
    })
})
wss.on('listening', () => {
    console.log('listening on 8080')
})