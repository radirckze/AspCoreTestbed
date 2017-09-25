// Write your JavaScript code.
"using strict"

var ACT = {}; //AspCoreTestbad

$(document).ready(function () {

    ACT = ACT || {};

});

function addNewMovie() {
   
    $("#movietable tbody").append('<tr data-state="new"><td>id</td><td><input type="text" ' +
        'name="name" value= "name" ></input ></td > <td><input type="number" name="rating" ' +
        'value=3</input></td ></tr > ');
}

function saveNewMovies() {

    var newMovies = $("#movietable [data-state='new']");
    alert("number of new movies is: " + newMovies.length);
}

