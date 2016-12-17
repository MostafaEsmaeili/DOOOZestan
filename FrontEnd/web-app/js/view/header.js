define([
    'jquery',
    'underscore',
    'backbone',
    'sidebar',
    '../../template/section/header.html',
], function ($, _, Backbone, SidebarView, HeaderSectionTemplate) {
    var body        = $('body'),
        sidebarView = new SidebarView(),
        headerView  = Backbone.View.extend({
            tagName    : 'header',
            className  : 'headerView',
            events     : {
                'click .hamburgerMenu'       : 'showSidebar',
                'click .hamburgerMenu, .logo': 'vibrate'
            },
            template   : {
                page: _.template(HeaderSectionTemplate),
            },
            initialize : function () {
                this.render();
            },
            render     : function () {
                this.$el.html(this.template.page);
                body.prepend(this.$el);
                return this;
            },
            showSidebar: function () {
                $('.sidebarView').addClass('visible');
                body.addClass('noScroll');
            },
            vibrate    : function () {
                navigator.vibrate = navigator.vibrate || navigator.webkitVibrate || navigator.mozVibrate || navigator.msVibrate;
                if (navigator.vibrate) {
                    // vibration API supported
                    navigator.vibrate(5);
                }
            }
        });
    return headerView;
});