var service = new WinRuntimes.Service();

function readXML() {
    service.readXML().then(function (xmlContent) {
        var jObj = JSON.parse(xmlContent);
        document.getElementById("userId").value = jObj.UserId;
        document.getElementById("userName").value = jObj.UserName;
    });
}