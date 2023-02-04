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
        console.log(unityClient)
    }
    CLIENTS.push(ws);
    
    ws.send(JSON.stringify(data))

    ws.on('message', (data)=>{
        console.log(data)
        unityClient.send(data)
    })
})
wss.on('listening', () => {
    console.log('listening on 8080')
})