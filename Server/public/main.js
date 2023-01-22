const ws = new WebSocket('wss://127.0.0.1:8080/')
ws.onopen = () => {
  console.log('ws opened on browser')
    ws.send("hello");
}