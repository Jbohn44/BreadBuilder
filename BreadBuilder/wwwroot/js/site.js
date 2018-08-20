// Write your JavaScript code.

var counter = 0;
var divId = 0;

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
    '<select asp-for="Measurement.Unit" class="dropdown">' +
    '<option value="0">oz</option>' +
    '<option value="1">g</option>' +
    '<option value="2">tbsp</option>' +
    '<option value="3">tsp</option>' +
    '<option value="4">cup</option>' +
    '</select> ' +
    '</div>';
    


function addInput()
{
   
    if (counter < 10) {
        divId++;
        counter++;
        var div = document.createElement('DIV');
        div.className = "row";
        div.id = divId;
        div.innerHTML = formPopulate;
        document.getElementById("wrapper").appendChild(div);
        
        
       
    }
    else
    {
        alert("Maximum Ingredients")
    }
   
}

function removeInput()
{
    if (counter >= 1)
    {   
        counter--;
        var removeId = divId;
        var divToRemove = document.getElementById(removeId);
        divToRemove.parentNode.removeChild(divToRemove);
        
        divId--;
    }
    else
    {
       alert("no ingredients yet")
    }
}

             