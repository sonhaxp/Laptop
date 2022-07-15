function validateEmail(email) {
    return email.match(
        /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    );
};
$(document).on("click", ".pass-show-btn", function () {
        if ($(this).html() == "Hiện") {
            $("#password").attr("type", "text")
            $(this).html("Ẩn")
        }
        else {
            $("#password").attr("type", "password")
            $(this).html("Hiện")
        }
});
$(document).on("click", ".btn-submit-login", function () {
    var email = $('#email').val();
    var password = $('#password').val();
    if (email == "" || password == "") {
        alert("Vui lòng nhập đầy đủ thông tin")
    }
    else {
        if (!validateEmail(email)) {
            alert("email không hợp lệ")
        }
        else {
            var form = new FormData();
            form.append("email", email);
            form.append("password", password);
            var xhr = new XMLHttpRequest();
            xhr.open("POST", '/User/checkLogin', true);
            xhr.Lineout = 30000;
            xhr.onreadystatechange = function () {
                if (xhr.readyState == 4 && xhr.status == 200) {
                    var r = xhr.responseText;
                    var j = JSON.parse(r);
                    if (j.Data.status == "OK") {
                        alert("Đăng nhập thành công");
                        window.location = "/trang-chu";
                    } else {
                        alert("Sai tên đăng nhập hoặc mật khẩu");
                    }
                }
            }
            xhr.send(form);
        }
    }
});