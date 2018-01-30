// Regex to calculate a valid hex color entry
var colorRegex = /^#[0-9a-f]{3}(?:[0-9a-f]{3})?$/i;

// holds gradient color array and start, stop values.
var roastColorGradient;
var startColor = '#8c9361';
var endColor = '#2B2117';

// number of steps (and thus colors) between start and 
// end gradient colors.
var gradientSteps = 50;

// Use event listeners to ensure we don't overwrite previously
// set functionality
window.addEventListener("load", function(){
	roastColorGradient = generateRoastColorGradient();

	$('#start').click(start);

	getCoffee();
});

// Dry air makes a quicker roast
function roastTypeChanged(roastType) {
	// also take into account weather and humidity
	var roastTimePercentage = $(roastType).val();
	var roastColor = getRoastColor(roastTimePercentage);
	UpdateRoastColor(roastColor);
}

// Updates the roast color of all elements of class 'bean'
function UpdateRoastColor(roastColor) {
	var beans = $('.bean');

	beans.each(function() {
		$(this).css("fill", roastColor);
	});
}

// Get the bean color from the cacluated gradients
function getRoastColor(roastTimePercentage) {
	var roastColorIndex = Math.floor(roastTimePercentage * 
		(gradientSteps - 1));
	return roastColorGradient[roastColorIndex].toHexString()
}

// Generate an array of colors representing the gradient
// change in a bean's caramelization
function generateRoastColorGradient() {
	result = new Array();

	if(isHexColor(startColor) && isHexColor(endColor)) {
		var grad = tinygradient([startColor, endColor]);
		result = grad.hsv(gradientSteps, 'short');
	}

	return result;
}

function gradientTest() {
	var grad = tinygradient(['#8c9361', '#2B2117']);
	//var colorsHsv = grad.hsv(50, 'long');
	var html = '<section class="col-md-12">';

	alert(grad);

	grad.hsv(50, 'short').forEach(function(color) {
        html += '<span style="background:' + color.toRgbString() + ';" title="' + color.toHexString() + '">' +color.toHexString() +'</span><br/>';
    });
	html += '</section>';
    document.querySelector('.container').innerHTML += html;
}

function startTimer() {
	var timer = $('.timer')[0];
	createTimer(60, timer)
}

// [TODO]: A more robust timer implementation could involve creating
// a moment instance (from moment.js) and allowing it to update based
// on the frequency.
// Argument roastStopPercent is based on the desired end roast, where "green" is
// 0, "charcoal" is 1, and other values fall in between.
function startRoast(totalSeconds, roastEndPercent, element) {
	// Frequency (in ms) at which the interval is processed.
	var frequency = 1000;
	var secondsRemaining = totalSeconds;
	var minutes;
	var seconds;

	// Ensure minutes are of correct type and are unsigned
	// (the second part is important for efficient disposal)
	if(typeof secondsRemaining === 'number' && secondsRemaining > 0)
	{
		var timerInterval = setInterval(function() {
			var percentComplete = (totalSeconds-secondsRemaining)/totalSeconds;

			updateTimer(secondsRemaining, element);
			updateRoastColor(percentComplete, roastEndPercent);

			if(--secondsRemaining < 0) {
				clearInterval(timerInterval);
			}
		}, frequency);
	}
}

function updateTimer(seconds, element) {
	var minutes = parseInt(seconds/60);
	var seconds = parseInt(seconds % 60);
	$(element).html(minutes + "min" + seconds + "sec");
}

// need a max value as well (always goes to final roast color)
function updateRoastColor(percentComplete, roastEndPercent) {
	// Scale the percentage relevant to number of gradient steps

	
	var colorValue = getRoastColor(percentComplete * roastEndPercent);
	UpdateRoastColor(colorValue);
}

function isHexColor(input) {
	return colorRegex.test(input);
}