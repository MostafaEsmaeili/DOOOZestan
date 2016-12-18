define([
    'jquery',
    'underscore',
    'backbone',
    'js-cookie',
    'sidebar',
    '../../template/page/login.html',
    '../../template/section/login.html'
], function ($, _, Backbone, JS_Cookie, SidebarView, LoginPageTemplate, LoginSectionTemplate) {
    var body          = $('body'),
        userNameValue = (JS_Cookie.get('userName')) ? JS_Cookie.get('userName') : false,
        passWordValue = (JS_Cookie.get('passWord')) ? JS_Cookie.get('passWord') : false,
        loginView     = Backbone.View.extend({
            tagName         : 'section',
            className       : 'loginView',
            events          : {
                'click .eye'       : 'showHidePassword',
                'submit form'      : 'submit'
                // 'change .inputfile': 'avatarUpload'
            },
            template        : {
                page   : _.template(LoginPageTemplate),
                section: _.template(LoginSectionTemplate)
            },
            initialize      : function () {
                $(window).on("resize", this.updateCSS);
                this.render();
            },
            render          : function () {
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
            updateCSS       : function () {
                if (window.innerHeight < 700) {
                    $('.loginSection', this.$el).removeClass('largeHeight').addClass('smallHeight');
                }
                else {
                    $('.loginSection', this.$el).removeClass('smallHeight').addClass('largeHeight');
                }
            },
            fillInputs      : function () {
                if (userNameValue) {
                    $('.userName').attr('placeholder', '').val(userNameValue);
                }
                if (passWordValue) {
                    $('.passWord').attr('placeholder', '').val(passWordValue);
                }
            },
            submit          : function () {
                JS_Cookie.set('userName', $('.userName').val());
                JS_Cookie.set('passWord', $('.passWord').val());
                JS_Cookie.set('userIsLogin', 'yes');
                $('.sidebarView').remove();
                var sidebarView = new SidebarView();
                Backbone.history.navigate('game/1', {trigger: true});
            },
            avatarUpload    : function (e) {
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
            },
            showHidePassword: function () {
                if ($('.password').attr('type') === 'password') {
                    $('.password').attr('type', 'text');
                    $('.eye').removeClass('fa-eye').addClass('fa-eye-slash');
                } else {
                    $('.password').attr('type', 'password');
                    $('.eye').removeClass('fa-eye-slash').addClass('fa-eye');
                }
            }
        });
    return loginView;
});