/*$(document).ready(function () {
    GetAllPosts();
});*/

/*function AddComment() {
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
}*/

function AddLike(x) {
    $.post("/Posts/AddLike?postingID=" + x);
}

function RemoveLike(x) {
    $.post("/Posts/RemoveLike?postingID=" + x);
}

function AddDislike(x) {
    $.post("/Posts/AddDislike?postingID=" + x);
}
function RemoveDislike(x) {
    $.post("/Posts/RemoveDislike?postingID=" + x);
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
