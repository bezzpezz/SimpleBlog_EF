$(document).ready(function () {
    $("a[data-post]").click(function (e) {
        e.preventDefault();
        //alert("got here");

        var $this = $(this);

        var message = $this.data("post");

        if (message && !confirm(message))
            return;

        var antiForgeryToken = $("#anti-forgery-form input");
        var antiForgeryInput = $("<input type='hidden'>").attr("name", antiForgeryToken.attr("name")).val(antiForgeryToken.val());

        $("<form>")
            .attr("method", "post")
            .attr("action", $this.attr("href"))
            .append(antiForgeryInput)
            .appendTo(document.body)
            .submit();

        //$("<form> <button> type=submit")
        //    .attr("method", "post")
        //    .attr("action", $this.attr("href"))
        //    .append(antiForgeryInput)
        //    .appendTo(document.body)
        //    .submit();
    });

    // For button elements - not "<a>" elements - the code above is used with the data-post attribute and the "<a>" html element
    // boostrap can stuff up styling, so instread of overriding bootstrap css, add "<button>"s inside of a "<form>" tag and use a Get request with the form-submit class
    $(".form-submit button").on("click", function (e) {

        e.preventDefault();
        var $this = $(this);
        var form = $(this).closest('form');
        var antiForgeryToken = $("#anti-forgery-form input");
        var antiForgeryInput = $("<input type='hidden'>").attr("name", antiForgeryToken.attr("name")).val(antiForgeryToken.val());

        $("<form>")
            .attr("method", "get")
            .attr("action", $this.attr("formaction"))
            .append(antiForgeryInput)
            .appendTo(document.body)
            .submit();
    });

});
