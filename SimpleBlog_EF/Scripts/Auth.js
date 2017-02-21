$(document).ready(function () {

    //$.validator.unobtrusive.parse("#login-form");

    $(".login-btn").on("click", function (e) {
        if ($(".login-btn").text().trim() == "Logout") {
            $("#logout-modal").modal("show");
            setTimeout(
              function () {
                  $(".login-submit").click();
              }, 2000);
        }
        else {
            $("#login-modal").modal("show");
        }
    });

    //login submit
    $(".login-submit").on("click", function (e) {

        e.preventDefault();

        var loginOrLogout = $(".login-btn").text().trim();
        var Username = $("<input>").attr("name", "Username").val($("#username").val());
        var Password = $("<input>").attr("name", "Password").val($("#password").val());

        if (!Username || !Password) {
            //$(this).closest("form")
            //.submit();
            //return;
        }
        else {
            //build new login form and post
            var antiForgeryToken = $("#anti-forgery-form input");
            var antiForgeryInput = $("<input type='hidden'>").attr("name", antiForgeryToken.attr("name")).val(antiForgeryToken.val());

            switch (loginOrLogout) {
                case "Login":
                    $("<form>")
                        .attr("method", "post")
                        .attr("action", "/login")
                        .append(Username)
                        .append(Password)
                        .append(antiForgeryInput)
                        .appendTo(document.body)
                        .submit();
                    break;

                default:
                    $("<form>")
                        .attr("method", "get")
                        .attr("action", "/logout")
                        .appendTo(document.body)
                        .submit();
                    break;
            }


        }
    });

    //$(".login-modal").on("submit", function (e) {

    //    e.preventDefault();

    //    var Username = $("#username").val();
    //    var Password = $("#password").val();

    //    if (!Username || !Password) {
    //        //$(this).closest("form")
    //        //.submit();
    //        //return;
    //    }
    //});
    //$("a[data-post]").click(function (e) {
    //    e.preventDefault();
    //    //alert("got here");

    //    var $this = $(this);

    //    var message = $this.data("post");

    //    if (message && !confirm(message))
    //        return;

    //    var antiForgeryToken = $("#anti-forgery-form input");
    //    var antiForgeryInput = $("<input type='hidden'>").attr("name", antiForgeryToken.attr("name")).val(antiForgeryToken.val());

    //    $("<form>")
    //        .attr("method", "post")
    //        .attr("action", $this.attr("href"))
    //        .append(antiForgeryInput)
    //        .appendTo(document.body)
    //        .submit();
    //});
});

