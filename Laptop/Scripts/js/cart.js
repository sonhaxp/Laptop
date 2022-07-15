$('.qtybtn').on('click', function () {
    var $button = $(this);
    var oldValue = $button.parent().find('input').val();
    if ($button.hasClass('inc')) {
        var newVal = parseFloat(oldValue) + 1;
    } else {
        // Don't allow decrementing below zero
        if (oldValue > 0) {
            var newVal = parseFloat(oldValue) - 1;
        } else {
            newVal = 0;
        }
    }
    $button.parent().find('input').val(newVal);
    var id = $button.parent().find('input').attr("name");
    $.ajax({
        url: "/Cart/UpdateCart?id=" + id + "&quantity=" + newVal,
        method: "PUT",
        success: function (res) {
            $('#render-cart').html(res)
            var xhr = new XMLHttpRequest();
            xhr.open("POST", '/Cart/GetCountCart', true);
            xhr.Lineout = 30000;
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && xhr.status == 200) {
                    var r = xhr.responseText;
                    var j = JSON.parse(r);
                    $('#count-cart').html(j.message)
                }
            }
            xhr.send();
        },
        error: function (res) {
            alert('lỗi không xác định')
            window.location = '/trang-chu';
        }
    });
});
$('.btn-delete-cart').on("click", function () {
    var result = confirm("Bạn có muốn xóa không?");
    if (result) {
        var id = $(this).attr("name");
        $.ajax({
            url: "/Cart/DeleteCart?id=" + id,
            method: "DELETE",
            success: function (res) {
                $('#render-cart').html(res);
                var xhr = new XMLHttpRequest();
                xhr.open("POST", '/Cart/GetCountCart', true);
                xhr.Lineout = 30000;
                xhr.onreadystatechange = function () {
                    if (xhr.readyState == 4 && xhr.status == 200) {
                        var r = xhr.responseText;
                        var j = JSON.parse(r);
                        $('#count-cart').html(j.message)
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
});