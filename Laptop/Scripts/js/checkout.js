function validateEmail(email) {
    return email.match(
        /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    );
};
function isVietnamesePhoneNumberValid(number) {
    return /(((\+|)84)|0)(3|5|7|8|9)+([0-9]{8})\b/.test(number);
}
$(document).on("click", ".btn-checkout", function () {
    var name = $('#first_name').val();
    var email = $('#email_address').val();
    var phone = $('#tel_number').val();
    var address = $('#p_address').val();
    if (name == '' || email == '' || phone == '' || address == '') {
        alert('Vui lòng nhập đầy đủ thông tin')
    }
    else {
        if (!validateEmail(email)) {
            alert('email sai định dạng')
        }
        else if (!isVietnamesePhoneNumberValid(phone)) {
            alert('số điện thoại không đúng')
        }
        else {
            $.ajax({
                url: "/Cart/Checkout_Order?name=" + name + "&email=" + email + "&phone=" + phone + "&address=" + address,
                method: "POST",
                contentType: "json",
                crossDomain: true,
                dataType: 'json',
                success: function (res) {
                    if (res.message == "Success") {
                        alert("Đã đặt hàng thành công");
                        window.location = "/trang-chu"
                    }
                    else {
                        alert("Đặt hàng thất bại");
                    }
                },
                error: function (res) {
                    alert("Lỗi không xác định")
                    window.location = '/trang-chu'
                }
            })
        }
    }
});