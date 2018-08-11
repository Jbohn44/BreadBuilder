// Write your JavaScript code.


var formPopulate =
    '<span class="form-inline">' +
    '<label asp-for="Ingredient.Name">Ingredient</label>' +
    '<input asp-for="Ingredient.Name" class="form-control" />' +

    '<label asp-for="Measurement.Value">Measurement</label>' +
    '<input asp-for="Measurement.Value" class="form-control" />' +

    '<label asp-for="Measurement.Unit">Unit</label>' +
    '<select asp-for="Measurement.Unit" asp-items="Model.MeasurementUnits"></select>' +
    '<button onclick="addInput()">Click to add Stuff</button>' +
    '</span>';




function addInput()
{
    document.getElementById("wrapper").innerHTML = formPopulate;

}


             