const ws = new WebSocket('wss://localhost:8080')
ws.onopen = () => {
  console.log('ws opened on browser')

}
function getRandomName(){
  return Math.random().toString(20).substr(2, 6)
}
ws.onmessage = (message) => {
  console.log(`message received`);
  console.log(`message received`, message.data)
}
function send(data) {
  ws.send(data)
}

function handleOrientation(event) {
  console.log(event.alpha+","+event.beta+","+event.gamma);
  ws.send(event.alpha+","+event.beta+","+event.gamma);
}
document.getElementById("start").addEventListener("click", function(event){
  console.log("start");
  event.preventDefault();
  if (
    DeviceMotionEvent &&
    typeof DeviceMotionEvent.requestPermission === "function"
  ) {
    DeviceMotionEvent.requestPermission();
  }
  window.addEventListener("deviceorientation", handleOrientation);
  document.getElementById("start").style.display = "none";
  

});


document.getElementById("buttonup").addEventListener("mousedown", function(event) {
  event.preventDefault();
  ws.send("buttonup_down")
}, false);
document.getElementById("buttonup").addEventListener("mouseup", function(event) {
  ws.send("buttonup_up")
}, false);

document.getElementById("buttonleft").addEventListener("mousedown", function(event) {
  event.preventDefault();
  ws.send("buttonleft_down")
}, false);
document.getElementById("buttonleft").addEventListener("mouseup", function(event) {
  ws.send("buttonleft_up")
}, false);

document.getElementById("buttonright").addEventListener("mousedown", function(event) {
  event.preventDefault();
  ws.send("buttonright_down")
}, false);
document.getElementById("buttonright").addEventListener("mouseup", function(event) {
  ws.send("buttonright_up")
}, false);

document.getElementById("buttondown").addEventListener("mousedown", function(event) {
  event.preventDefault();
  ws.send("buttondown_down")
}, false);
document.getElementById("buttondown").addEventListener("mouseup", function(event) {
  ws.send("buttondown_up")
}, false);




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