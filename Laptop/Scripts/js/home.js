$(document).on("click", ".btn-cart", function () {
    var id = $(this).attr("name")
    window.location = "/Cart/AddCart/" + id;
});
$(document).on("click", ".btn-wishlist", function () {
    var id = $(this).attr("name");
    $.ajax({
        url: '/WishList/AddorDeleteWishList/' + id,
        method: 'post',
        success: function (res) {
            if (res.message == "add") {
                $('.wish-list-' + id).addClass("btn-wishlist--active");
            }
            else {
                $('.wish-list-' + id).removeClass("btn-wishlist--active");
            }
            $.ajax({
                url: '/WishList/GetCountWishList',
                method: 'post',
                success: function (res) {
                    $('#count-wishlist').html(res.message);
                }
            });
        }
    });
});
loadwishlist = function () {
    $.ajax({
        url: '/WishList/GetWish',
        method: 'GET',
        contentType: "json",
        crossDomain: true,
        dataType: 'json',
        success: function (res) {
            length = res.length;
            var wishlist = $('.btn-wishlist');
            for (var i = 0; i < length; i++) {
                $('.wish-list-' + res[i]).addClass("btn-wishlist--active");
            }
        }
    });
}
loadwishlist();