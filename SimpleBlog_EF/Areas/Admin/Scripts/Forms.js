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
    });

    $("[data-slug]").each(function () {
        var $this = $(this);
        var $sendSlugFrom = $($this.data("slug"));

        $sendSlugFrom.keyup(function () {
            var slug = $sendSlugFrom.val();
            slug = slug.replace(/[^a-zA-Z0-9\s]/g, "");
            slug = slug.toLowerCase();
            slug = slug.replace(/\s+/g, "-");

            if (slug.charAt(slug.length - 1 == "-"))
                slug = slug.substr(0, slug.length - 1);

            $this.val(slug);
        });
    });

    // For button elements - not "<a>" elements - the code above is used with the data-post attribute and the "<a>" html element
    // boostrap can stuff up styling, so instread of overriding bootstrap css, add "<button>"s inside of a "<form>" tag and use a Get request with the form-submit class
    $(".form-submit button").on("click", function (e) {

        e.preventDefault();
        var $this = $(this);
        var form = $(this).closest('form');
        var antiForgeryToken = $("#anti-forgery-form input");
        var antiForgeryInput = $("<input type='hidden'>").attr("name", antiForgeryToken.attr("name")).val(antiForgeryToken.val());

        if ($this.attr('id') == "users-delete-btn") {

            var message = $this.data("post");
            if (message && !confirm(message))
                return;

            $("<form>")
                .attr("method", "post")
                .attr("action", $this.attr("formaction"))
                .append(antiForgeryInput)
                .appendTo(document.body)
                .submit();
        }
        else {
            $("<form>")
                .attr("method", "get")
                .attr("action", $this.attr("formaction"))
                .append(antiForgeryInput)
                .appendTo(document.body)
                .submit();
        }
    });
});
