$(document).on("click", ".btn-action", function () {
    var per = $(this).attr("per")
    var id = $(this).attr("name")
    if (per == 1) {
        window.location = "/Cart/AddCart/" + id;
    }
    else {
        var result = confirm("Bạn có muốn xóa không?");
        if (result) {
            $.ajax({
                url: "/WishList/DeleteWishList?id=" + id,
                method: "DELETE",
                success: function (res) {
                    $('#render-wishlist').html(res);
                    var xhr = new XMLHttpRequest();
                    xhr.open("POST", '/WishList/GetCountWishList', true);
                    xhr.Lineout = 30000;
                    xhr.onreadystatechange = function () {
                        if (xhr.readyState == 4 && xhr.status == 200) {
                            var r = xhr.responseText;
                            var j = JSON.parse(r);
                            $('#count-wishlist').html(j.message)
                        }
                    }
                    xhr.send();
                },
                error: function (res) {
                    alert('lỗi không xác định')
                    window.location = '/trang-chu';
                }
            });
        }
    }
})