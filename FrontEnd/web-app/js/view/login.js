define([
    'jquery',
    'underscore',
    'backbone',
    '../../template/page/login.html',
    '../../template/section/login.html',
], function ($, _, Backbone, LoginPageTemplate, LoginSectionTemplate) {
    var body      = $('body'),
        loginView = Backbone.View.extend({
            className : 'loginView',
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
                this.$el.append(this.template.section);
                return this;
            }
        });
    return loginView;
});