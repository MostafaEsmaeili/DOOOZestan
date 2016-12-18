module.exports = {
    entry  : './js/main',
    output : {
        filename: 'application.js'
    },
    resolve: {
        modulesDirectories: ['.'],
        alias             : {
            'jquery'    : 'node_modules/jquery/dist/jquery.min',
            'underscore': 'node_modules/underscore/underscore-min',
            'backbone'  : 'node_modules/backbone/backbone',
            'bootstrap' : 'node_modules/bootstrap/dist/js/bootstrap.min',
            'js-cookie' : 'node_modules/js-cookie/src/js.cookie',
        }
    },
    module : {
        loaders: [
            {test: /\.html$/, loader: "raw"},
        ]
    }
};