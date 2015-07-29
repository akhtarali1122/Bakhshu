$(document).ready(function () {


    if (document.getElementById("UpdateUserAvatar")) {
        $("#UpdateUserAvatar").change(function () {
            UpdateUserProfileAvatar();
        });
    }
    //if (document.getElementById("btnUpdateProfileInfo")) {
    //    $("#btnUpdateProfileInfo").click(function () {
    //        UpdateUserProfile();
    //    });
    //}
    if (document.getElementById("btnEditUserProfile")) {
        $("#btnEditUserProfile").click(function () {
            hideElement('profileInfoViewMode'); showElement('profileInfoEditMode');
        });
    }
    
    if (document.getElementById("btnCancelProfileInfo")) {
        $("#btnCancelProfileInfo").click(function () {
            hideElement('profileInfoEditMode'); showElement('profileInfoViewMode');
        });
    }
   
     
});

function UpdateUserProfileAvatar() {
    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
    document.getElementById("frmUserAvatar").action = siteBaseUrl + "UpdateProfiles/UpdateProfileAvatar";
    showProcessingPopup();
    document.getElementById("frmUserAvatar").submit();
}

//function UpdateUserProfile() {
//    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
//    document.getElementById("frmProfileInfo").action = siteBaseUrl + "UpdateProfiles/UpdateUser";
//    showProcessingPopup();
//    document.getElementById("frmProfileInfo").submit();
//}
