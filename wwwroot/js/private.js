
//create connection 
let socket = new WebSocket("ws://localhost:5042/ocpp");
let valueInput = document.getElementById("inputValue")
let newCountSpan = document.getElementById("messageSended");
socket.onopen = function (event) {
    console.log("WebSocket connection opened:", event);
   
};
socket.onmessage = function (event) {
    console.log("message ::: ",event.data)
    newCountSpan.innerText = event.data?.toString() || "value";
};


socket.onclose = function (event) {
    console.log("WebSocket connection closed:", event);
};
function sendMessage() {
    if (socket && socket.readyState === WebSocket.OPEN) {
        console.log("Sending message:", valueInput.value);
        
        socket.send(valueInput.value);
        valueInput.value = ''
    }else
    console.log("fail to send")
}
