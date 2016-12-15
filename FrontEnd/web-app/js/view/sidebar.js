define([
    'jquery',
    'underscore',
    'backbone',
    'js-cookie',
    '../../template/section/sidebar.html',
], function ($, _, Backbone, JS_Cookie, SidebarSectionTemplate) {
    var body        = $('body'),
        sidebarView = Backbone.View.extend({
            tagName     : 'aside',
            className   : 'sidebarView',
            events      : {
                'click .settings'                  : 'preventClick',
                'change .inputFile'                : 'avatarUpload',
                'click  .login, .register, .logout': 'closeSidebar',
                'click  .logout'                   : 'logout',
            },
            template    : {
                page: _.template(SidebarSectionTemplate),
            },
            initialize  : function () {
                this.render();
            },
            render      : function () {
                this.$el.html(this.template.page({
                    userIsLogin: JS_Cookie.get('userIsLogin')
                }));
                body.prepend(this.$el);
                this.$el.on('click', this.closeSidebar);
                return this;
            },
            closeSidebar: function () {
                $('.sidebarView').removeClass('visible');
                body.removeClass('noScroll');
            },
            preventClick: function (e) {
                e.stopPropagation();
            },
            logout      : function () {
                JS_Cookie.set('userIsLogin', false);
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
    return sidebarView;
});