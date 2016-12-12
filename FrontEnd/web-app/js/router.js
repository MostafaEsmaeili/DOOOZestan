define([
    'jquery',
    'underscore',
    'backbone',
    'view/home',
    'view/login',
    'view/register',
    'view/profile',
    'view/game',
    'view/notFound',
], function ($, _, Backbone, HomeView, LoginView, RegisterView, ProfileView, GameView, NotFoundView) {
    var body      = $('body'),
        main      = $('main'),
        appRouter = Backbone.Router.extend({
            initialize: function (options) {
            },
            routes    : {
                ''           : 'home',
                // ''           : 'login',
                'login'      : 'login',
                'register'   : 'register',
                'profile/:id': 'profile',
                'game/:id'   : 'game',
                '*path'      : 'notFound',
            },
            home      : function () {
                var homeView = new HomeView();
                main.html(homeView.$el);
            },
            login     : function () {
                var loginView = new LoginView();
                main.html(loginView.$el);
            },
            register  : function () {
                var registerView = new RegisterView();
                main.html(registerView.$el);
            },
            profile   : function (id) {
                var profileView = new ProfileView();
                main.html(profileView.$el);
            },
            game      : function (id) {
                var gameView = new GameView(id);
                main.html(gameView.$el);
            },
            notFound  : function (path) {
                var notFoundView = new NotFoundView(path);
                main.html(notFoundView.$el);
            }
        });
    return appRouter;
});