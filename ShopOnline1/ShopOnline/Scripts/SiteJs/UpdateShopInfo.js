$(document).ready(function () {

    if (document.getElementById("UpdateShopAvatar")) {
        $("#UpdateShopAvatar").change(function () {
            UpdateShopProfileAvatar();
        });
    }

    //if (document.getElementById("btnUpdateShopInfo")) {
    //    $("#btnUpdateShopInfo").click(function () {
    //        UpdateShopProfile();
    //    });
    //}
    if (document.getElementById("btnEditShopProfile")) {
        $("#btnEditShopProfile").click(function () {
            hideElement('ShopInfoViewMode'); showElement('ShopInfoEditMode');
        });
    }
    
    if (document.getElementById("btnCancelShopInfo")) {
        $("#btnCancelShopInfo").click(function () {
            hideElement('ShopInfoEditMode'); showElement('ShopInfoViewMode');
        });
    }
});

//function UpdateShopProfile() {
//    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
//    document.getElementById("frmProfileInfo").action = siteBaseUrl + "UpdateProfiles/UpdateShopInfo";
//    showProcessingPopup();
//    document.getElementById("frmProfileInfo").submit();
//}

function UpdateShopProfileAvatar() {
    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
    document.getElementById("frmUserAvatar").action = siteBaseUrl + "UpdateProfiles/ChangeShopProfileAvatar";
    showProcessingPopup();
    document.getElementById("frmUserAvatar").submit();
}