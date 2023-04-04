
const ws = new WebSocket('wss://s1.app.catfood.li:8432')
var permissionrequested = false;
ws.onopen = () => {
  console.log('ws opened on browser')
 
}

ws.onmessage = (message) => {
  console.log(`message received`, message.data)
}
function send(data) {
  ws.send(data)
}

function handleOrientation(event) {
  console.log(event.alpha+","+event.beta+","+event.gamma);
  ws.send(event.alpha+","+event.beta+","+event.gamma);
}
var currentCar = 1;
var maxcars = 8;
document.getElementById("button_carselector_left").addEventListener("touchstart", function(event) {
  event.preventDefault();
  if(currentCar>1){
    currentCar--;
  }
  document.getElementById("car").src = "car"+currentCar+".png";
}, false);
document.getElementById("button_carselector_right").addEventListener("touchstart", function(event) {
  event.preventDefault();
  if(currentCar<maxcars){
    currentCar++;
  }
  document.getElementById("car").src = "car"+currentCar+".png";
}, false);
document.getElementById("start").addEventListener("touchstart", function(event) {
  event.preventDefault();
  document.getElementById("carselector").style.display = "none";
  if(currentCar == 8){
    document.getElementById("pointercontroller").style.display = "block";
    ws.send("createCar|8");
  }else{
    ws.send("createCar|"+currentCar);
    document.getElementById("controller").style.display = "block";
  }

 
}, false);
var lasttime = new Date();
var lastOrientation;


function handleOrientation(event){
  console.log(event);
  let currentTime = new Date();
  if(currentTime-lasttime>=25){
    lasttime = new Date();
    ws.send(event.alpha+","+event.beta+","+event.gamma);
    lastOrientation = event;
  }
  
}
function onClick() {
  if (typeof DeviceMotionEvent.requestPermission === 'function') {
    // Handle iOS 13+ devices.
    DeviceMotionEvent.requestPermission()
      .then((state) => {
        if (state === 'granted') {
          window.addEventListener('deviceorientation', handleOrientation);
        } else {
          console.error('Request to access the orientation was rejected');
        }
      })
      .catch(console.error);
  } else {
    // Handle regular non iOS 13+ devices.
    window.addEventListener('deviceorientation', handleOrientation);
  }
  document.getElementById("pointerstartbutton").style.display = "none";
  document.getElementById("pointerbutton").style.display = "block";
}




document.getElementById("pointerbutton").addEventListener("touchstart",function(event){
  event.preventDefault();
  ws.send("pointerbutton_down")
  ws.send(lastOrientation.alpha+","+lastOrientation.beta+","+lastOrientation.gamma);
}, false)
document.getElementById("pointerbutton").addEventListener("touchend",function(event){
  event.preventDefault();
  ws.send("pointerbutton_up")
  ws.send(lastOrientation.alpha+","+lastOrientation.beta+","+lastOrientation.gamma);
}, false)
document.getElementById("buttonup").addEventListener("touchstart", function(event) {
  event.preventDefault();
  ws.send("buttonup_down")
}, false);
document.getElementById("buttonup").addEventListener("touchend", function(event) {
  ws.send("buttonup_up")
}, false);

document.getElementById("buttonleft").addEventListener("touchstart", function(event) {
  event.preventDefault();
  ws.send("buttonleft_down")
}, false);
document.getElementById("buttonleft").addEventListener("touchend", function(event) {
  ws.send("buttonleft_up")
}, false);

document.getElementById("buttonright").addEventListener("touchstart", function(event) {
  event.preventDefault();
  ws.send("buttonright_down")
}, false);
document.getElementById("buttonright").addEventListener("touchend", function(event) {
  ws.send("buttonright_up")
}, false);

document.getElementById("buttondown").addEventListener("touchstart", function(event) {
  event.preventDefault();
  ws.send("buttondown_down")
}, false);
document.getElementById("buttondown").addEventListener("touchend", function(event) {
  ws.send("buttondown_up")
}, false);
