/// <reference path="../jquery/jquery.d.ts"/>
var Abalon;
(function (Abalon) {
    var Query = (function () {
        function Query(apiPath) {
            this.apiPath = apiPath;
        }
        Query.prototype.login = function (username, password) {
            return $.ajax({
                method: "post",
                url: this.apiPath + "login",
                data: { name: username, pass: password }
            });
        };

        Query.prototype.logout = function () {
            return $.ajax({
                method: "post",
                url: this.apiPath + "logout"
            });
        };

        Query.prototype.list = function (handler) {
            return $.ajax({
                method: "get",
                url: this.apiPath + "list",
                dataType: "json",
                success: handler
            });
        };
        return Query;
    })();
    Abalon.Query = Query;
})(Abalon || (Abalon = {}));
