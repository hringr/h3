$(document).ready(function () {
    GetAllComments();
});

function AddComment() {
    // alert("Hallo");
    var comment = $('#CommentText').val();
    var obj = new Object();
    obj.comment = comment;
    //post ajax
    $.ajax({
        type: "POST",
        url: "/Home/AddComment",
        data: obj,
        datatype: "json",
        success: function (data) {
            $("comment").remove();
            $("#CommentText").val("");
            //alert("Halló");
            //Kallar í GetAllComments();
            GetAllComments();

        },

    });
}

function GetAllComments() {
    //Ajax get kall sem nær í öll comment.
    //í success fallinu á ajax fyrirspurninni þá "smíðaru" html streng af commentum og attachar það við htmlið sem þú ert með efst
    //T.d $("#box").before(strengurinnSemÞúBjóstTil);
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "/Home/GetAllComments",
        data: "{}",
        datatype: "json",
        success: function (comments) {
            for (var i = 0; i < comments.length; i++) {
                comments[i].CommentDate = ConvertStringToJSDate(comments[i].CommentDate);
            }

            $("#commentList").loadTemplate($("#template"), comments);

            for (var i = 0; i < comments.length; i++) {
                GetLikes(comments[i].ID);
            }
        }
    })
}

function AddLike(x) {
    $.post("Posts/AddLike", { commentid: x }, function (data) {
        $("comment").remove();
        GetAllComments(x);
    });
}

function GetLikes(id) {
    $.ajax(
        {
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "Posts/GetLikes",
            data: { commentId: id },
            datatype: "json",
            success: function (likes) {
                for (var i = 0; i < likes.length; i++) {
                    likes[i].LikeDate = ConvertStringToJSDate(likes[i].LikeDate);
                }
                $("." + id).loadTemplate($("#templateForLikes"), likes);
            }
        });
}

function ConvertStringToJSDate(dt) {
    var dtE = /^\/Date\((-?[0-9]+)\)\/$/.exec(dt);
    if (dtE) {
        var dt = new Date(parseInt(dtE[1], 10));
        return dt;
    }
    return null;
}