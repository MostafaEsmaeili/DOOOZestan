define([
    'jquery',
    'underscore',
    'backbone',
    '../../template/page/login.html',
    '../../template/section/login.html',
], function ($, _, Backbone, LoginPageTemplate, LoginSectionTemplate) {
    var body      = $('body'),
        loginView = Backbone.View.extend({
            tagName   : 'main',
            className : 'loginView',
            events    : {
                'click .submit': 'submit'
            },
            template  : {
                page   : _.template(LoginPageTemplate),
                section: _.template(LoginSectionTemplate)
            },
            initialize: function () {
                this.render();
                $(window).on("resize", this.updateCSS);
            },
            render    : function () {
                var _this = this;
                body.removeClass().addClass('login');
                this.$el.html(this.template.page);
                this.$el.append(this.template.section).promise().done(function () {
                    _this.updateCSS();
                });
                return this;
            },
            updateCSS : function () {
                if (window.innerHeight < 500) {
                    $('.loginSection', this.$el).removeClass('largeHeight').addClass('smallHeight');
                }
                else {
                    $('.loginSection', this.$el).removeClass('smallHeight').addClass('largeHeight');
                }
            },
            submit    : function () {
                console.log("submit login");
            }
        });
    return loginView;
});