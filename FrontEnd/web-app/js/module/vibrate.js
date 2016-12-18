define([
    'jquery'
], function ($) {
    return function () {
        // enable vibration support
        navigator.vibrate = navigator.vibrate || navigator.webkitVibrate || navigator.mozVibrate || navigator.msVibrate;
        if (navigator.vibrate) {
            // vibration API supported
            navigator.vibrate(3);
        }
    }
});