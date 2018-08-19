// Write your JavaScript code.

var counter = 1;
var formPopulate =
    '<div class="row">' +
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
    '<select asp-for="Measurement.Unit" class="dropdown">' +
    '<option value="0">oz</option>' +
    '<option value="1">g</option>' +
    '<option value="2">tbsp</option>' +
    '<option value="3">tsp</option>' +
    '<option value="4">cup</option>' +
    '</select> ' +
    '</div>' +
    '</div>';
    


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


             