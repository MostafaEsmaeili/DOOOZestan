define([
    'jquery',
    'underscore',
    'backbone'
], function ($, _, Backbone) {
    var gameModel = Backbone.Model.extend({
        urlRoot   : 'http://192.168.1.103/Doozestan.WebApi/API/Game/3',
        initialize: function () {
        },
    });
    return gameModel;
});