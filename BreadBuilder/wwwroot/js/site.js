// Write your JavaScript code.

var counter = 1;
var formPopulate =

    '<div class="col-md-2">' +
    '<label asp-for="Ingredient.Name">Ingredient</label>' +
    '<input asp-for="Ingredient.Name" class="form-control" />' +
    '</div>' +
    '<div class="col-md-1">' +
    '<label asp-for="Measurement.Value">Quantity</label>' +
    '<input asp-for="Measurement.Value" class="form-control" />' +
    '</div>' +
    '<div class="col-md-1">' +
    '<label asp-for="Measurement.Unit">Unit</label>' +
    '<select asp-for="Measurement.Unit" class="dropdown"></select>' +
    '</div>' +
    '<br />' +
    '<br />';


function addInput()
{
    if (counter < 10) {
        document.getElementById("wrapper").innerHTML += formPopulate;
        counter++;

    }
    else
    {
        alert("Maximum Ingredients")
    }
   
}


             