define([
    'jquery',
    'underscore',
    'backbone'
], function ($, _, Backbone) {
    var gameModel = Backbone.Model.extend({
        idAttribute: "_id",
        // urlRoot   : 'http://192.168.1.103/Doozestan.WebApi/API/Game/3',
        urlRoot    : 'http://doozestan.css89iha.ir/api/game/3',
        initialize : function () {
        },
    });
    return gameModel;
});