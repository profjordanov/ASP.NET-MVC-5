// myapp.js
(function(app) {
    app.isDebug = true;

    app.log = function(msg) {
        if (app.isDebug) {
            console.log(msg);
        }
    };

})(window.app = window.app || {}); //self executing function javascript