define([
    'jquery',
    'underscore',
    'backbone',
    '../../template/page/profile.html',
    '../../template/section/profile.html',
], function ($, _, Backbone, ProfilePageTemplate, ProfileSectionTemplate) {
    var body        = $('body'),
        profileView = Backbone.View.extend({
            tagName   : 'main',
            className : 'profileView',
            template  : {
                page   : _.template(ProfilePageTemplate),
                section: _.template(ProfileSectionTemplate)
            },
            initialize: function () {
                this.render();
            },
            render    : function () {
                body.removeClass().addClass('profile');
                this.$el.html(this.template.page);
                return this;
            }
        });
    return profileView;
});