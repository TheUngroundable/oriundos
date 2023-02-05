
# Oriundos



A brief description of what this project does and who it's for


## IMPORTANT
You must retrieve your private ip address before you start playing

Run this command in terminal to find your private ip address
```bash
    ipconfig

    # example of private ip address
    # 192.168.1.67

    # !!!!IMPORTANT!!!!!!!
    # do not use the ip address that ends by 254 since it is the gateway and the game could not work 
```

once you have it, edit in the follwing files the address that the clients have to point to

 - WebSocketClient.cs edit serverIp variable at the beginning of the file
 - InGame.vue you can edit serverIp constant at the beginning of the file

by now on, when we refer to "server ip", we are talking about the private ip address you previously found

## Folders Structure

 - UNITY: has unity client
 - CLIENT: has Vue.js web client
 - NODE: has server node

## Installation

How Install Oriundos with npm


Install node dependencies


Open terminal in Node folder and run
```bash
    npm install
```

Open terminal in Client/oriundos folder and run

```bash
    npm install
    npm run serve
```

Now Vue.js client will run on port 5173 and node.js will run on port 8080

For opening Vue.js client, go to 
```bash
    http://{COMPUTER_LOCAL_IP_ADDRESS}:5173

    #example
    # http://192.168.1.81:5173
```

When you run Unity project it will connect to server and will display the room number that the users must instert in the input in the Vue.js client

Once all joined players are ready, game starts
