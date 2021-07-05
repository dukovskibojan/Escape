$(function () {
    let startingText = $("#startingText").text();
    $("#startingText").remove();
    let newElement = $("<p style='color:lightgrey;'></p>");
    newElement.appendTo("#textHere")
    let i = 0;
    function addElement() {
        newElement.html(newElement.html() + startingText[i]);
        if (i == startingText.length - 1)
            clearInterval(iD);
        else {
            i = parseInt(i) + 1;
        }
    }
    var iD = setInterval(addElement, 0);
})