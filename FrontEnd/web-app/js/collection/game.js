define([
    'jquery',
    'underscore',
    'backbone',
    '../model/game',
], function ($, _, Backbone, GameModel) {
    var gameCollection = Backbone.Collection.extend({
        // url       : 'http://192.168.1.103/Doozestan.WebApi/API/Game/',
        url       : 'http://doozestan.css89iha.ir/api/game/',
        model     : GameModel,
        initialize: function () {
        }
    });
    return gameCollection;
});