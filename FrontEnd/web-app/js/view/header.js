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
                'click .hamburgerMenu': 'showSidebar',
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
        });
    return headerView;
});