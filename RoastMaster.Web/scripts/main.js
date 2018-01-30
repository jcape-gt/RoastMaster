// Regex to calculate a valid hex color entry
var colorRegex = /^#[0-9a-f]{3}(?:[0-9a-f]{3})?$/i;

// holds gradient color array and start, stop values.
var roastGradient = {
    colorList: null,
    steps: 50,
    startColor: '#8c9361',
    endColor: '#2B2117'
}

var timer = {
    element: null,
    seconds: null,
    interval: null,
    frequency: 1000, // milliseconds
    paused: false,
    onRun: null,
    onEnd: null
}

// Use event listeners to ensure we don't overwrite previously
// set functionality
window.addEventListener('load', function () {
    initRoastGradient();

    $('#timerControl').click(toggleTimer);
    $('#resetControl').click(resetTimer);
});

function toggleTimer() {
    if (timer.interval == null) {
        var timerElement = $('#timer');
        var roastTimePercentage = $('#roastType').val();
        var maxRoastSeconds = $('#coffeeType').val();
        var time = maxRoastSeconds * roastTimePercentage;

        if (roastTimePercentage == null || maxRoastSeconds == null)
            alert('Please Select Coffee and Roast');
        else {
            initTimer(time, roastTimePercentage, timerElement);
            $('#resetControl').prop('disabled', false);
            $('#timer').css('visibility', 'inherit');
        }
    }   
    else if (timer.paused)
        resumeTimer();
    else
        pauseTimer();
}

// [TODO]: A more robust timer implementation could involve creating
// a moment instance (from moment.js) and allowing it to update based
// on the frequency.
// Argument roastEndPercent is based on the desired end roast, where "green" is
// 0, "charcoal" is 1, and other values fall in between.
function initTimer(totalSeconds, roastEndPercent, element) {
    timer.seconds = totalSeconds;
    timer.element = element;
    
    timer.onRun = function () {
        var percentComplete = (totalSeconds - timer.seconds) / totalSeconds;
        console.log(percentComplete + ' :: ' + roastEndPercent);
        var roastColor = getRoastColor(percentComplete * roastEndPercent);

        updateTimerDisplay(timer.seconds, element);
        console.log(percentComplete + ' ' + roastEndPercent);
        updateRoastColor(roastColor);
    }

    timer.onEnd = function () {
        resetTimer();
        alert('Roasted :)!');
        $('#timer').css('visibility', 'hidden');
        $('#timer').html('00:00');
    }

    timer.interval = setInterval(function () {
        if (!timer.paused) {
            timer.onRun();

            if (--timer.seconds < 0) {
                clearInterval(timer.interval);
                timer.onEnd();
                timer.interval = null;
            }
        }
    }, timer.frequency);

    resumeTimer();
}

// Handle pausing the timer
function pauseTimer() {
    timer.paused = true;
    $('#timerControl').html('Resume!');
}

// Handle resuming the timer
function resumeTimer() {
    timer.paused = false;
    $('#timerControl').html('Pause!');
}

// Reset the timer to it's initial state
function resetTimer() {
    timer.interval = null;
    timer.seconds = null;
    timer.onEnd = null;
    timer.onRun = null;
    timer.paused = false;

    $('#timerControl').html('Roast!');
    $(timer.element).html('')
        ;}

function initRoastGradient() {
    // Generate an array of colors representing the gradient
    // change in a bean's caramelization
    var gradiant = tinygradient([roastGradient.startColor, roastGradient.endColor]);
    roastGradient.colorList = gradiant.hsv(roastGradient.steps, 'short');
}

// Dry air makes a quicker roast
function roastTypeChanged(roastType) {
	// also take into account weather and humidity
    var roastTimePercentage = $(roastType).val();

    console.log(roastType + ' ' + roastTimePercentage);
	var roastColor = getRoastColor(roastTimePercentage);
	updateRoastColor(roastColor);
}

// Updates the roast color of all elements of class 'bean'
function updateRoastColor(roastColor) {
	var beans = $('.bean');

	beans.each(function() {
		$(this).css('fill', roastColor);
	});
}

// Get the bean color from the cacluated gradients
function getRoastColor(roastTimePercentage) {
	var roastColorIndex = Math.floor(roastTimePercentage * 
		(roastGradient.steps - 1));
	return roastGradient.colorList[roastColorIndex].toHexString()
	//alert (beanColorGradient[roastColorIndex]);
}

function updateTimerDisplay(seconds, element) {
	var minutes = parseInt(seconds/60);
	var seconds = parseInt(seconds % 60);
	$(element).html(padDigits(minutes, 2) + ':' + padDigits(seconds,2));
}

// Left-pad the input number with zeroes
function padDigits(number, digits) {
    return Array(Math.max(digits - String(number).length + 1, 0)).join(0) + number;
}

// Ensure input is a valid Hex color
function isHexColor(input) {
	return colorRegex.test(input);
}