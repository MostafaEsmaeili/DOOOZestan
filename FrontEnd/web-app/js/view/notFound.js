define([
    'jquery',
    'underscore',
    'backbone',
    '../../template/page/notFound.html',
    '../../template/section/notFound.html',
], function ($, _, Backbone, NotFoundPageTemplate, NotFoundSectionTemplate) {
    var body         = $('body'),
        notFoundView = Backbone.View.extend({
            tagName   : 'section',
            className : 'notFoundView',
            template  : {
                page   : _.template(NotFoundPageTemplate),
                section: _.template(NotFoundSectionTemplate)
            },
            initialize: function () {
                this.render();
            },
            render    : function () {
                body.removeClass().addClass('notFound');
                this.$el.html(this.template.page);
                body.prepend(this.$el);
                return this;
            }
        });
    return notFoundView;
});