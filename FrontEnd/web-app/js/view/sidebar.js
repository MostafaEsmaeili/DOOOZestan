define([
    'jquery',
    'underscore',
    'backbone',
    '../../template/section/sidebar.html',
], function ($, _, Backbone, SidebarSectionTemplate) {
    var body        = $('body'),
        sidebarView = Backbone.View.extend({
            tagName     : 'aside',
            className   : 'sidebarView',
            events      : {},
            template    : {
                page: _.template(SidebarSectionTemplate),
            },
            initialize  : function () {
                this.render();
            },
            render      : function () {
                this.$el.html(this.template.page);
                body.prepend(this.$el);
                this.$el.on('click', this.closeSidebar);
                return this;
            },
            closeSidebar: function () {
                $('.sidebarView').removeClass('visible');
            }
        });
    return sidebarView;
});