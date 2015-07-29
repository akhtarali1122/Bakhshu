$(document).ready(function () {

    if (document.getElementById("fileShopProfile")) {
        $("#fileShopProfile").change(function () {
            changeShopProfileAvatar();
        });
    }

    if (document.getElementById("fileUserProfile")) {
        $("#fileUserProfile").change(function () {
            changeUserProfileAvatar();
        });
    }



});

function changeUserProfileAvatar() {
    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
    document.getElementById("frmUserAvatar").action = siteBaseUrl + "ShopImages/ChangeProfileAvatar";
    showProcessingPopup();
    document.getElementById("frmUserAvatar").submit();

}
function changeShopProfileAvatar() {

    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
    document.getElementById("frmShopAvatar").action = siteBaseUrl + "ShopImages/ChangeShopProfileAvatar";
    showProcessingPopup();
    document.getElementById("frmShopAvatar").submit();
}
function RemoveShopImage(USER_ID) {

    bootbox.confirm("Are you sure, you want to remove the image ?", function (result) {
        if (result == true) {
            var siteBaseUrl = document.getElementById("frmSiteUrl").action;
            document.getElementById("frmSiteUrl").action = siteBaseUrl + "ShopImages/DeleteShopImage?Shop_Image_Id=" + USER_ID;
            document.getElementById("frmSiteUrl").submit();
        }

    });

}