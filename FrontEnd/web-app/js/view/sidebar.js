define([
    'jquery',
    'underscore',
    'backbone',
    'js-cookie',
    '../../template/section/sidebar.html'
], function ($, _, Backbone, JS_Cookie, SidebarSectionTemplate) {
    var body        = $('body'),
        main        = $('main'),
        sidebarView = Backbone.View.extend({
            tagName     : 'aside',
            className   : 'sidebarView',
            events      : {
                // 'change .inputFile'                : 'avatarUpload',
                'click  .logout': 'logout'
            },
            template    : {
                page: _.template(SidebarSectionTemplate)
            },
            initialize  : function () {
                this.render();
            },
            render      : function () {
                var _this = this;
                this.$el.html(this.template.page({
                    userIsLogin: JS_Cookie.get('userIsLogin')
                }));
                $('.sidebarView').remove();
                this.$el.insertBefore(main);
                $('a', this.$el).on('click', _this.closeSidebar);
                $('.holder', this.$el).on('click', _this.closeSidebar);
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
                JS_Cookie.set('userIsLogin', 'false');
                this.render();
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