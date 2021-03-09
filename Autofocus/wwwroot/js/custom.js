var loadFile = function (event, img) {

    


    var reader = new FileReader();
    reader.onload = function () {
        var output = document.getElementById(img);
        output.src = reader.result;
        output.style.display = "block"; 
    };
    reader.readAsDataURL(event.target.files[0]);
};