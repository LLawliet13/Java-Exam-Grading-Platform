function Validate() {
    let oldPassword = document.getElementsByName("Password")[0].value;
    let newPassword = document.getElementsByName("newPassword")[0].value;
    let cfmPassword = document.getElementsByName("confirmPassword")[0].value;
    let email = document.getElementsByName("Email")[0].value;
    let AccountTypeID = document.getElementsByName("AccountTypeId")[0].value;
    let Username = document.getElementsByName("Username")[0].value;
    let AccountId = document.getElementsByName("AccountId")[0].value;
    console.log(oldPassword + "," + newPassword);
    if (oldPassword == newPassword) alert("oldPassword is same as newPassword")
    else
    if (newPassword !== cfmPassword) alert("newPassword is not same as cfmPassword")
    else
        $.ajax({

            url: '/Account/ChangePassword',
            type: 'Post',
            data: {
                
                'Password': oldPassword,
                'newPassword': newPassword,
                'email': email,
                'AccountTypeID': AccountTypeID,
                'Username': Username,
                'Id': AccountId
            },
            dataType: 'text',
            success: function (data) {
                if (data === "changed")
                    alert("change password sucessfully")
                else alert(data);

            },
            error: function (request, error) {
                alert("Request: " + JSON.stringify(request));
            }
        });
}