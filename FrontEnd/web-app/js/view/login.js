define([
    'jquery',
    'underscore',
    'backbone',
    '../../template/page/login.html',
    '../../template/section/login.html',
], function ($, _, Backbone, LoginPageTemplate, LoginSectionTemplate) {
    var body      = $('body'),
        loginView = Backbone.View.extend({
            template  : {
                page   : _.template(LoginPageTemplate),
                section: _.template(LoginSectionTemplate)
            },
            initialize: function () {
                this.render();
            },
            render    : function () {
                body.removeClass().addClass('login');
                this.$el.html(this.template.page);
                return this;
            }
        });
    return loginView;
});