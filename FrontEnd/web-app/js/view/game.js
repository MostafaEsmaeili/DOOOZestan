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
                        gameCollection.each(function (model) {
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
                return this;
            },
            select    : function (e) {
                $('.action').removeClass('selected');
                $(e.target).addClass('selected');
            }
        });
    return gameView;
});