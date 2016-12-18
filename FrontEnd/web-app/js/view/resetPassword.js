define([
    'jquery',
    'underscore',
    'backbone',
    '../../template/page/resetPassword.html',
    '../../template/section/resetPassword.html'
], function ($, _, Backbone, ResetPasswordPageTemplate, ResetPasswordSectionTemplate) {
    var body              = $('body'),
        resetPasswordView = Backbone.View.extend({
            tagName   : 'section',
            className : 'resetPasswordView',
            events    : {
                'submit form': 'submit'
            },
            template  : {
                page   : _.template(ResetPasswordPageTemplate),
                section: _.template(ResetPasswordSectionTemplate)
            },
            initialize: function () {
                $(window).on("resize", this.updateCSS);
                this.render();
            },
            render    : function () {
                var _this = this;
                body.removeClass().addClass('resetPassword');
                this.$el.html(this.template.page);
                $.when(
                    this.$el.append(this.template.section)
                ).then(function () {
                    _this.updateCSS();
                });
                return this;
            },
            updateCSS : function () {
                if (window.innerHeight < 500) {
                    $('.resetPasswordSection', this.$el).removeClass('largeHeight').addClass('smallHeight');
                }
                else {
                    $('.resetPasswordSection', this.$el).removeClass('smallHeight').addClass('largeHeight');
                }
            },
            submit    : function () {
                Backbone.history.navigate('game/1', {trigger: true});
            }
        });
    return resetPasswordView;
});