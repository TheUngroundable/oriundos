const WebSocket = require('ws')
const wss = new WebSocket.Server({ port: 8080 }, () => {
    console.log('server started')
})

const CLIENTS = []
let unityClient = undefined

wss.on('connection', function connection(ws) {

    const data = {
        id: CLIENTS.length,
        command: "CONNECTION"
    }
    if(!unityClient){
        unityClient = ws
    }
    CLIENTS.push(ws);
    
    ws.send(JSON.stringify(data))

    ws.on('message', (data)=>{
        const json = JSON.parse(data)
         const message = {
            id: json.id,
            command: json.command,
            direction: json.direction
         }
        unityClient.send(JSON.stringify(message))
    })
})
wss.on('listening', () => {
    console.log('listening on 8080')
})