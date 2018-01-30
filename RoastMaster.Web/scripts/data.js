var serviceUrl = 'http://localhost:8008/roastmaster';

var coffeeList = null;
var speciesList = null;

function apiGet(endpoint, successFn, errorFn) {
	$.ajax({
		url: endpoint,
		type: 'GET',
		dataType: 'jsonp',
        success: successFn,
		error: function (jqXHR, textStatus, errorThrown) {
            alert(endpoint + " : " + jqXHR.responseText + " : "  + textStatus + ' : ' + errorThrown);
        }
	});
}

function getCoffee() {
    var endpoint = serviceUrl + '/coffee';
    var success = function (resultList) {
        coffeeList = resultList;
    };
    var error = apiError;

    apiGet(endpoint, success, error);
}

function getSpecies() {
    var endpoint = serviceUrl + '/species';
    var success = function (resultList) {
        speciesList = resultList;
    };
    var error = apiError;

    apiGet(endpoint, success, error);
}

function load() {
    $.when(getCoffee(), getSpecies()).done
}

function initCoffeeDropDown() {
    var speciesMapping = {};

    getCoffee();
    getSpecies();

    console.log(coffeeList + ' ' + speciesList);
    if (coffeeList != null && speciesList != null) {
        $.each(speciesList, function () {
            speciesMapping[this.id] = new {
                name: this.name, coffeeList: new Array()
            }
            console.log()
        });

        $.each(coffeeList, function () {
            speciesMapping[this.speciesId].coffeeList.push(this.name);
        });
    }

    console.log(speciesMapping);
}

function updateCoffeeDropDown(coffeeList) {
	var select = $('#coffeeType');

	$.each(coffeeList, function() {
		var coffeeOption = $( '<option>' + this.name + '</option>' );

        $(select).append(coffeeOption)
	});
}

function apiError(method, endpoint) {
	alert('Failed ' + method + ' call for ' + endpoint);
}