// Write your JavaScript code.

var counter = 0;
var divId = 0;
var i = 5;

    


function addInput()
{
    var formPopulate =

        '<div class="col-md-2">' +
        '<label asp-for="RecipeItems[' + i +'].RecipeIngredient.Name">Ingredient</label>' +
        '<input asp-for="RecipeItems[' + i + '].RecipeIngredient.Name" class="form-control" />' +
        '</div>' +
        '<div class="col-md-1">' +
        '<label asp-for="RecipeItems[' + i + '].RecipeMeasurment.Value">Quantity</label>' +
        '<input asp-for="RecipeItems[' + i + '].RecipeMeasurment.Value" class="form-control" />' +
        '</div>' +
        '<div class="col-md-1">' +
        '<label asp-for="RecipeItems[' + i + '].RecipeMeasurement.Unit">Unit</label>' +
        '<select asp-for="RecipeItems[' + i + '].RecipeMeasurement.Unit" class="dropdown">' +
        '<option value="0">oz</option>' +
        '<option value="1">g</option>' +
        '<option value="2">cup</option>' +
        '</select> ' +
        '</div>';

    var formInput =
        '<div class="col-md-2">' +
        '<label for="RecipeItems[' + i + '].RecipeIngredient.Name">Ingredient</label>' +
        '<input name="RecipeItems[' + i + '].RecipeIngredient.Name" class="form-control" />' +
        '</div>' +
        '<div class="col-md-1">' +
        '<label for="RecipeItems[' + i + '].RecipeMeasurment.Value">Quantity</label>' +
        '<input name="RecipeItems[' + i + '].RecipeMeasurment.Value" class="form-control" />' +
        '</div>' +
        '<div class="col-md-1">' +
        '<label for="RecipeItems[' + i + '].RecipeMeasurement.Unit">Unit</label>' +
        '<select name="RecipeItems[' + i + '].RecipeMeasurement.Unit" class="dropdown">' +
        '<option value="0">oz</option>' +
        '<option value="1">g</option>' +
        '<option value="2">cup</option>' +
        '</select> ' +
        '</div>';
   
    if (counter < 10)
    {
        divId++;
        counter++;
        var div = document.createElement('DIV');
        div.className = "row";
        div.id = divId;
        div.innerHTML = formInput;
        document.getElementById("wrapper").appendChild(div);
        i++;
        
       
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
        i--;
        divId--;
    }
    else
    {
       alert("no ingredients yet")
    }
}

             