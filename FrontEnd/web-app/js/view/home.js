define([
    'jquery',
    'underscore',
    'backbone',
    '../../template/page/home.html',
    '../../template/section/home.html',
], function ($, _, Backbone, HomePageTemplate, HomeSectionTemplate) {
    var body     = $('body'),
        homeView = Backbone.View.extend({
            className : 'homeView',
            template  : {
                page   : _.template(HomePageTemplate),
                section: _.template(HomeSectionTemplate)
            },
            initialize: function () {
                this.render();
            },
            render    : function () {
                body.removeClass().addClass('home');
                this.$el.html(this.template.page);
                return this;
            }
        });
    return homeView;
});