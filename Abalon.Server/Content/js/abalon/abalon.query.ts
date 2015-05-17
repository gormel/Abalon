/// <reference path="../jquery/jquery.d.ts"/>

module Abalon {
    export class Query {
        constructor(private apiPath: string) { }

        login(username: string, password: string): JQueryXHR {
            return $.ajax({
                method: "post",
                url: this.apiPath + "login",
                data: { name: username, pass: password }
            });
        }

        logout(): JQueryXHR {
            return $.ajax({
                method: "post",
                url: this.apiPath + "logout"
            });
        }

        list(handler: (players: { name: string }[]) => void): JQueryXHR {
            return $.ajax({
                method: "get",
                url: this.apiPath + "list",
                dataType: "json",
                success: handler
            });
        }
    }
}
