define([
    'jquery',
    'underscore',
    'backbone',
    'js-cookie',
    '../../template/page/login.html',
    '../../template/section/login.html',
], function ($, _, Backbone, JS_Cookie, LoginPageTemplate, LoginSectionTemplate) {
    var body      = $('body'),
        loginView = Backbone.View.extend({
            tagName    : 'main',
            className  : 'loginView',
            events     : {
                'click .submit': 'submit',
            },
            template   : {
                page   : _.template(LoginPageTemplate),
                section: _.template(LoginSectionTemplate)
            },
            initialize : function () {
                $(window).on("resize", this.updateCSS);
                this.render();
            },
            render     : function () {
                var _this = this;
                body.removeClass().addClass('login');
                this.$el.html(this.template.page);
                $.when(
                    this.$el.append(this.template.section)
                ).then(function () {
                    _this.updateCSS();
                    _this.fillInputs();
                });
                return this;
            },
            afterRender: function () {
                this.fillInputs();
            },
            updateCSS  : function () {
                if (window.innerHeight < 500) {
                    $('.loginSection', this.$el).removeClass('largeHeight').addClass('smallHeight');
                }
                else {
                    $('.loginSection', this.$el).removeClass('smallHeight').addClass('largeHeight');
                }
            },
            fillInputs : function () {
                var userNameValue = (JS_Cookie.get('userName')) ? JS_Cookie.get('userName') : false,
                    passWordValue = (JS_Cookie.get('passWord')) ? JS_Cookie.get('passWord') : false;
                if (userNameValue) {
                    $('.userName').attr('placeholder', '').val(userNameValue);
                }
                if (passWordValue) {
                    $('.passWord').attr('placeholder', '').val(passWordValue);
                }
            },
            submit     : function () {
                var userName = $('.userName').val(),
                    passWord = $('.passWord').val();
                JS_Cookie.set('userName', userName);
                JS_Cookie.set('passWord', passWord);
            }
        });
    return loginView;
});