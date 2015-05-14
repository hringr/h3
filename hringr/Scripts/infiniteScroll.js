var page = 0,
    inCallback = false,
    hasReachedEndOfInfiniteScroll = false;

var scrollHandler = function (e) {
    if (hasReachedEndOfInfiniteScroll === false &&
            ($(window).scrollTop() >= ($(document).height() - $(window).height() - ($(window).height() * 0.1)))) {
        loadMoreToInfiniteScrollTable(e.data.address);
    }
}

function loadMoreToInfiniteScrollTable(loadMoreRowsUrl) {
    if (page > -1 && !inCallback) {
        inCallback = true;
        page++;
        $("div#loading").show();
        $.ajax({
            type: 'GET',
            url: loadMoreRowsUrl,
            data: "pageNumber=" + page,
            success: function (data, textstatus) {
                if (data != '') {
                    $("#posts-wrapper").append(data);
                }
                else {
                    page = -1;
                }

                inCallback = false;
                $("div#loading").hide();
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
            }
        });
    }
}
