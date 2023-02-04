const WebSocket = require('ws')
const wss = new WebSocket.Server({ port: 8080 }, () => {
    console.log('server started')
})

const CLIENTS = []
let unityClient = undefined

const numberOfPlayers = 2
let numberOfReady = 0


wss.on('connection', function connection(ws) {

    const data = {
        id: unityClient ? CLIENTS.length : 0,
        command: "CONNECTION"
    }

    CLIENTS.push(ws);
    
    ws.send(JSON.stringify(data))
    
    if(!unityClient){
        unityClient = ws
    }

    

    ws.on('message', (data)=>{
        const json = JSON.parse(data)
        console.log(json)
        if(json.command === 'JOINED'){
            if(json.type === 'UNITY'){
                unityClient = ws
                console.log("Unity has joined the game")
            } else if(json.type === 'PLAYER'){
                console.log("Player has joined the room")
            }
        }

        if(json.command === 'MOVEMENT'){
            console.log("Player "+json.id+" has moved to "+json.value)
            const message = {
                id: json.id,
                command: json.command,
                value: json.value
            }
            unityClient.send(JSON.stringify(message))
        }

        if(json.command === 'READY'){
            if(json.value){
                numberOfReady++;
                console.log("Player "+json.id+" is ready")
            } else {
                numberOfReady--;
                console.log("Player "+json.id+" is not ready")
            }
        }

        

        if(numberOfReady == numberOfPlayers+1){
            const message = {
                id: -1,
                command: 'STARTED',
                value: 1
            }
            CLIENTS.forEach(client => client.send(JSON.stringify(message)))
            console.log("Game has started")
        }
    })
})
wss.on('listening', () => {
    console.log('listening on 8080')
})