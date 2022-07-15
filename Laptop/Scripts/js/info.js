// information
const information = document.getElementById('js-information')
const modalInformation = document.querySelector('.member-body-wrap')
const modalInformationBoder = document.querySelector('.tab-member__link--info')
const modalInformationPointer = document.querySelector('.tab-member__link--info')
// Hàm hiện modal
function showInformation() {
    modalInformation.classList.remove('hide')
    modalTransaction.classList.remove('open')

    modalTransactionBoder.classList.remove('tab-member-color-active')
    modalInformationBoder.classList.add('tab-member-color-active')

    modalInformationPointer.classList.remove('cursor-pointer')
    modalTransactionPointer.classList.add('cursor-pointer')
}
// xử lý hành vi click hiện
information.addEventListener('click', showInformation)

// transaction
const transaction = document.getElementById('js-transaction')
const modalTransaction = document.querySelector('.modal-transaction')
const modalTransactionBoder = document.querySelector('.tab-member__link--trans')
const modalTransactionPointer = document.querySelector('.tab-member__link--trans')
// Hàm hiện modal
function showTransaction() {
    modalTransaction.classList.add('open')
    modalInformation.classList.add('hide')

    modalTransactionBoder.classList.add('tab-member-color-active')
    modalInformationBoder.classList.remove('tab-member-color-active')

    modalInformationPointer.classList.add('cursor-pointer')
    modalTransactionPointer.classList.remove('cursor-pointer')
    load_hd();
}
// xử lý hành vi click hiện
transaction.addEventListener('click', showTransaction)
dateFormat = function (str) {
    var s = str.split('T')
    var t = s[0].split('-')
    var output = t[2] + '/' + t[1] + '/' + t[0];
    return output
}
load_hd = function () {
    $.ajax({
        url: "/User/Order?datefrom=" + document.getElementById('date_from').value + "&dateto=" + document.getElementById('date_to').value,
        method: "GET",
        success: function (res) {
            $("#data-table").html(res);
        },
        error: function () {
            console.log("Load api get thất bại");
        }
    });
}
Save_ThanhVien = function () {
    var a = $('#changepass__checkbox').is(':checked');
    if (a == false) {
        $.ajax({
            url: "/user/ChangeInfo?address=" + $('#address').val() + "&oldpass=&newpass=",
            method: "PUT",
            contentType: "json",
            crossDomain: true,
            dataType: 'json',
            success: function (res) {
                if (res.message == 'ok')
                    alert('Cập nhật thành công');
                else {
                    alert('Cập nhật thất bại');
                }
            },
            error: function () {
                console.log("Load api thất bại");
            }
        });
    }
    else {
        if ($('#pass_older').val() == "" || $('#pass_new').val() == "" || $('#pass_new2').val() == "") {
            alert('Không được bỏ trống dữ liệu')
        }
            else if ($('#pass_new').val() != $('#pass_new2').val()) {
            alert('Mật khẩu mới không khớp')
        }
        else {
            $.ajax({
                url: "/user/ChangeInfo?address=" + $('#address').val() + "&oldpass=" + $('#pass_older').val() + "&newpass=" + $('#pass_new').val(),
                method: "PUT",
                contentType: "json",
                crossDomain: true,
                dataType: 'json',
                success: function (res) {
                    if (res.message == "error") {
                        alert('Mật khẩu cũ không đúng')
                    }
                    else {
                        alert('Cập nhật thành công');
                    }
                },
                error: function () {
                    console.log("Load api thất bại");
                }
            });
        }
    }
}
$(document).on("click", "#show-transaction", function () {
    var id = $(this).attr("name");
    $.ajax({
        url: "/User/GetOrderDetail?order_id=" + id,
        method: "GET",
        success: function (res) {
            $(".modal-body").html(res);
        },
        error: function () {
            console.log("Load api get thất bại");
        }
    });
});


