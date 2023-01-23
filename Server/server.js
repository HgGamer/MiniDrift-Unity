const https = require("https");
const http = require("http");
const fs = require("fs")
const WebSocket = require("ws");
const WebSocket2 = require("ws");
const express = require("express")
const ejs = require("ejs")

const app = express();
var unity;
app.use(express.static(__dirname + '/public'));
app.set('views', __dirname + '/views');
app.engine('html', require('ejs').renderFile);
app.set('view engine', 'ejs');
const options = {
    key: fs.readFileSync("./config/localhost.key"),
    cert: fs.readFileSync("./config/localhost.crt"),
  };

const server = https.createServer(options,app);

app.get('/', function(req, res) {
    res.render('index.html');
});

//controllers
const wss = new WebSocket.Server({ server });
wss.getUniqueID = function () {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
    }
    return s4() + s4() + '-' + s4();
};

wss.on('connection', (socket) => {
    socket.id = wss.getUniqueID();

    socket.on('message', (message) => {
        console.log('received: %s', message);
        let msg = socket.id + "|" + message;
     
        if(unity){
            unity.send(''+msg);
        }
       
    });
});

server.listen( 8080, () => {
    console.log(`Server started on port ${server.address().port} :)`);
});


//Unity "client"
const ws = new WebSocket2.Server({ port: 8000 });
ws.on('connection', (socket) => {
    unity = socket;
    socket.on('message', (message) => {
        console.log('received: %s', message);
        socket.send(`Hello, you sent -> ${message}`);
    });
  
});
