function validateEmail(email) {
	return email.match(
		/^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
	);
};
function isVietnamesePhoneNumberValid(number) {
    return /(((\+|)84)|0)(3|5|7|8|9)+([0-9]{8})\b/.test(number);
}
$(document).on("click", ".pass-show-btn", function () {
    if ($(this).attr("id") == "hide-newpass") {
        if ($(this).html() == "Hiện") {
            $("#newpassword").attr("type", "text")
            $(this).html("Ẩn")
        }
        else {
            $("#newpassword").attr("type", "password")
            $(this).html("Hiện")
        }
    }
    else {
        if ($(this).html() == "Hiện") {
            $("#c-password").attr("type", "text")
            $(this).html("Ẩn")
        }
        else {
            $("#c-password").attr("type", "password")
            $(this).html("Hiện")
        }
    }
});
$(document).on("click", ".btn-submit-register", function () {
    var name = $('#name').val();
    var email = $('#email').val();
    var password = $('#newpassword').val();
    var c_password = $('#c-password').val();
    var birthday = $('#birth').val();
    var phone = $('#phonenumber').val();
    var address = $('#address').val();
    var gender = $('[name="gender"]:radio:checked').val();
    if (name == "" || email == "" || password == "" || c_password == "" || birthday == "" || phone == "" || address == "" || gender == "") {
        alert("Vui lòng nhập đầy đủ thông tin")
    }
    else {
        if (!validateEmail(email)) {
            alert("email không hợp lệ")
        }
        else {
            if (password != c_password) {
                alert("mật khẩu không trùng nhau")
            }
            else {
                if (!isVietnamesePhoneNumberValid(phone)) {
                    alert("số điện thoại không đúng")
                }
                else {
                    var xhr = new XMLHttpRequest();
                    xhr.open("POST", '/User/Register_User?name=' + name + '&password=' + password + '&email=' + email + '&birthday=' + birthday + '&gender=' + gender + '&address=' + address + '&phoneNumber=' + phone, true);
                    xhr.Lineout = 30000;
                    xhr.onreadystatechange = function () {
                        if (xhr.readyState == 4 && xhr.status == 200) {
                            var r = xhr.responseText;
                            var j = JSON.parse(r);
                            if (j.message == "OK") {
                                alert("Đăng kí thành công vui lòng đăng nhập!!!");
                                window.location = "/dang-nhap";
                            } else {
                                alert("Email đã tồn tại. Vui lòng nhập email khác");
                            }
                        }
                    }
                    xhr.send();
                }
            }
        }
    }
});