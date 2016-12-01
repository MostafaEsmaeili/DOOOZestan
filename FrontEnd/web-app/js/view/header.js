define([
    'jquery',
    'underscore',
    'backbone',
    '../../template/section/header.html',
], function ($, _, Backbone, HaederSectionTemplate) {
    var body       = $('body'),
        haederView = Backbone.View.extend({
            className : 'headerView',
            template  : {
                page: _.template(HaederSectionTemplate)
            },
            initialize: function () {
                this.render();
            },
            render    : function () {
                this.$el.html(this.template.page);
                body.prepend(this.$el);
                return this;
            }
        });
    return haederView;
});