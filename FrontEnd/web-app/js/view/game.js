define([
    'jquery',
    'underscore',
    'backbone',
    '../collection/game',
    '../model/game',
    '../../template/page/game.html',
    '../../template/section/game.html'
], function ($, _, Backbone, GameCollection, GameModel, GamePageTemplate, GameSectionTemplate) {
    var body           = $('body'),
        gameCollection = new GameCollection(),
        gameModel      = new GameModel(),
        gameView       = Backbone.View.extend({
            tagName   : 'section',
            className : 'gameView',
            template  : {
                page   : _.template(GamePageTemplate),
                section: _.template(GameSectionTemplate)
            },
            events    : {
                'click .action': 'select'
            },
            initialize: function (id) {
                this.render();
            },
            render    : function () {
                var _this = this;
                body.removeClass().addClass('game');
                gameCollection.fetch({
                    success: function () {
                        console.log('gameCollection: ', gameCollection);
                        console.log('gameCollection.models: ', gameCollection.models);
                        console.log('gameCollection.models[0]: ', gameCollection.models[0]);
                        console.log("gameCollection.models[0].get('Data'): ", gameCollection.models[0].get('Data'));
                        gameCollection.each(function (model, index) {
                            console.log("model: ", model);
                            console.log("index: ", index);
                            _this.$el.html(_this.template.page({
                                model: model
                            }));
                        });
                        _this.$el.append(_this.template.section);
                    }
                });
                gameModel.fetch({
                    success: function (model) {
                    }
                });
                _this.$el.html(_this.template.page({}));
                _this.$el.append(_this.template.section);
                return this;
            },
            select    : function (e) {
                this.vibrate();
                $('.action').removeClass('selected');
                $(e.target).addClass('selected');
            },
            vibrate   : function () {
                navigator.vibrate = navigator.vibrate || navigator.webkitVibrate || navigator.mozVibrate || navigator.msVibrate;
                if (navigator.vibrate) {
                    // vibration API supported
                    navigator.vibrate(5);
                }
            }
        });
    return gameView;
});