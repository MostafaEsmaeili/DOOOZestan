define([
    'jquery',
    'underscore',
    'backbone',
    '../model/game',
], function ($, _, Backbone, GameModel) {
    var gameCollection = Backbone.Collection.extend({
        url       : 'http://192.168.1.103/Doozestan.WebApi/API/Game/',
        model     : GameModel,
        initialize: function () {
        }
    });
    return gameCollection;
});