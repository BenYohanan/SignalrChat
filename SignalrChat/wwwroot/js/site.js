
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();
var username = "";
    
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&gt;").replace(/ > /g, "&gt;");
    var encodedMsg = msg;
    var li = document.createElement("p");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function () {
    var message = document.getElementById("message").value;
    connection.invoke("SendMessage", username, message).then(function () {
        document.getElementById("message").value = "";
    }).catch(function (err) {
        return console.error(err.toString());
    });
});

function SetUsername() {
    var usernameinput = document.getElementById("username").value;
    if (usernameinput === "") {
        alert("Please enter your name");
        return;
    }
    username = usernameinput;

    document.getElementById("userinfo").style.display = 'none';
    document.getElementById("messagearea").style.display = 'block';
    document.getElementById("username").innerHTML = usernameinput;
}
