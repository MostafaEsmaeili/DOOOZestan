require([
    'jquery',
    'underscore',
    'backbone',
    'router',
    'view/header',
], function ($, _, Backbone, Router, HeaderView) {
    this.router = new Router();
    Backbone.history.start({});
    var headerView = new HeaderView();
});