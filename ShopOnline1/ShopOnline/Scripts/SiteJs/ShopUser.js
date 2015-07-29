$(document).ready(function () {
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