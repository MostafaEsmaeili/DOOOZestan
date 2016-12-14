define([
    'jquery',
    'underscore',
    'backbone',
    '../../template/page/register.html',
    '../../template/section/register.html',
], function ($, _, Backbone, RegisterPageTemplate, RegisterSectionTemplate) {
    var body         = $('body'),
        registerView = Backbone.View.extend({
            tagName     : 'section',
            className   : 'registerView',
            events      : {
                'click .submit'    : 'submit',
                'change .inputfile': 'avatarUpload'
            },
            template    : {
                page   : _.template(RegisterPageTemplate),
                section: _.template(RegisterSectionTemplate)
            },
            initialize  : function () {
                this.render();
                $(window).on("resize", this.updateCSS);
            },
            render      : function () {
                var _this = this;
                body.removeClass().addClass('register');
                this.$el.html(this.template.page);
                this.$el.append(this.template.section).promise().done(function () {
                    _this.updateCSS();
                });
                return this;
            },
            updateCSS   : function () {
                if (window.innerHeight < 500) {
                    $('.registerSection', this.$el).removeClass('largeHeight').addClass('smallHeight');
                }
                else {
                    $('.registerSection', this.$el).removeClass('smallHeight').addClass('largeHeight');
                }
            },
            submit      : function () {
                // JS_Cookie.set('userName', $('.userName').val());
                // JS_Cookie.set('passWord', $('.passWord').val());
                Backbone.history.navigate('game/1', {trigger: true});
            },
            avatarUpload: function (e) {
                var thisEl = $(e.currentTarget),
                    regex  = /^([a-zA-Z0-9\s_\\.\-:])+(.jpg|.jpeg|.gif|.png|.bmp)$/;
                if (regex.test(thisEl.val().toLowerCase())) {
                    if (typeof (FileReader) != "undefined") {
                        var reader    = new FileReader();
                        reader.onload = function (e) {
                            $(".avatar").attr("src", e.target.result);
                        };
                        reader.readAsDataURL(thisEl[0].files[0]);
                    } else {
                        alert("This browser does not support FileReader.");
                    }
                } else {
                    alert("Please upload a valid image file.");
                }
            }
        });
    return registerView;
});