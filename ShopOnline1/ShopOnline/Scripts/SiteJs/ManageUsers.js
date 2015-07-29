$(document).ready(function () {

    if (document.getElementById("ebizlineTreeTable")) {
        var oTable1 = $('#ebizlineTreeTable').dataTable({
            "bJQueryUI": true,
            "bPaginate": false,
            "bSort": true,
            "bInfo": false,

        });

        $("#txtLoginID").keyup(function () {
            oTable1.fnFilter(this.value, 0);
        });
        $("#txtName").keyup(function () {
            oTable1.fnFilter(this.value, 1);
        });

        $("#txtContactNos").keyup(function () {
            oTable1.fnFilter(this.value, 2);
        });

        $("#txtEmail").keyup(function () {
            oTable1.fnFilter(this.value, 3);
        });

        $("#selUserRole").change(function () {
            oTable1.fnFilter(this.value, 4);
        });

        $("#selUserStatus").change(function () {
            oTable1.fnFilter(this.value, 5);
        });

    }

    if (document.getElementById("txtLoginName")) {
        $("#txtLoginName").blur(function () {
             checkUsernameAvailability(document.getElementById("txtLoginName").value, "#divUserAvailabilityResult");
        });
    }


    if (document.getElementById("selRole")) {
        $("#selRole").change(function () {
            if (document.getElementById("selRole").value == "3") {

                document.getElementById("trTenantStaff").style.display = "none";
                document.getElementById("trTenant").style.display = "";

            }
            else {
                document.getElementById("trTenant").style.display = "none";
                document.getElementById("trTenantStaff").style.display = "";
    
            }
        });
    }

    if (document.getElementById("btnCreateUser")) {
        $("#btnCreateUser").click(function () {
            createUser();
        });
    }


    if (document.getElementById("btnLogin")) {
        $("#btnLogin").click(function () {
            doUserLogin();
        });
    }


    if (document.getElementById("btnRetreivePassword")) {
        $("#btnRetreivePassword").click(function () {
            retreivePassword();
        });
    }

    if (document.getElementById("btnSaveProfileInfo")) {
        $("#btnSaveProfileInfo").click(function () {
            SaveProfileInfo();
        });
    }
    
    if (document.getElementById("btnSaveProfileContactInfo")) {
        $("#btnSaveProfileContactInfo").click(function () {
            SaveProfileContactInfo();
        });
    }
    
    if (document.getElementById("btnEditProfileInfo")) {
        $("#btnEditProfileInfo").click(function () {
            hideElement('profileInfoViewMode'); showElement('profileInfoEditMode');
        });
    }
    if (document.getElementById("btnCancelProfileInfo")) {
        $("#btnCancelProfileInfo").click(function () {
            hideElement('profileInfoEditMode'); showElement('profileInfoViewMode');
        });
    }

    if (document.getElementById("btnEditProfileContactInfo")) {
        $("#btnEditProfileContactInfo").click(function () {
            hideElement('profileContactInfoViewMode'); showElement('profileContactInfoEditMode');
        });
    }
    
    if (document.getElementById("btnCancelProfileContactInfo")) {
        $("#btnCancelProfileContactInfo").click(function () {
            hideElement('profileContactInfoEditMode'); showElement('profileContactInfoViewMode');
        });
    }

    if (document.getElementById("btnChangeUserStatus")) {
        $("#btnChangeUserStatus").click(function () {
            changeUserStatus();
        });
    }

    if (document.getElementById("fileUserProfile")) {
        $("#fileUserProfile").change(function () {
            changeUserProfileAvatar();
        });
    }


    if (document.getElementById("btnUpdateProfileInfo")) {
        $("#btnUpdateProfileInfo").click(function () {
            UpdateUserProfileInfo();
        });
    }

    if (document.getElementById("btnUpdateUserProfileContactInfo")) {
        $("#btnUpdateUserProfileContactInfo").click(function () {
            UpdateUserContactInfo();
        });
    }

});

function changeUserStatus() {
    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
    document.getElementById("frmStatus").method = "Post";
    document.getElementById("frmStatus").action = siteBaseUrl+"ManageUsers/UpdateUserStatus";
    showProcessingPopup();
    document.getElementById("frmStatus").submit();
}

function createUser() {
    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
    document.getElementById("frmUser").method = "Post";
    document.getElementById("frmUser").action = siteBaseUrl + "ManageUsers/CreateUser";
    showProcessingPopup();
    document.getElementById("frmUser").submit();
}

function doUserLogin() {
    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
    document.getElementById("frmLogin").method = "Post";
    document.getElementById("frmLogin").action = siteBaseUrl + "Users/doLogin";
    showProcessingPopup();
    document.getElementById("frmLogin").submit();
}

function retreivePassword() {
    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
    document.getElementById("frmPassword").method = "Post";
    document.getElementById("frmPassword").action = siteBaseUrl+"Users/RetreivePassword";
    showProcessingPopup();
    document.getElementById("frmPassword").submit();
}


function SaveProfileInfo() {
    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
    document.getElementById("frmProfileInfo").action = siteBaseUrl+"Settings/SaveProfileInfo";
    showProcessingPopup();
    document.getElementById("frmProfileInfo").submit();
}

function SaveProfileContactInfo() {
    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
    document.getElementById("frmProfileContactInfo").action = siteBaseUrl + "Settings/SaveContactInfo";
    showProcessingPopup();
    document.getElementById("frmProfileContactInfo").submit();
}

function viewUser(userId) {
    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
    document.getElementById("frmSiteUrl").action = siteBaseUrl + "ManageUsers/ViewUserProfile?userId=" + userId;
    showProcessingPopup();
    document.getElementById("frmSiteUrl").submit();
}

function deleteUser(userId) {

    bootbox.confirm("Are you sure, you want to de-activate this user ?", function (result) {
        if (result == true) {
            var siteBaseUrl = document.getElementById("frmSiteUrl").action;
            document.getElementById("frmSiteUrl").action = siteBaseUrl + "ManageUsers/DeActivateUser?userId=" + userId;
            showProcessingPopup();
            document.getElementById("frmSiteUrl").submit();
        }

    });

}
function changeUserProfileAvatar()
{
    
    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
    document.getElementById("frmUserAvatar").action = siteBaseUrl + "Register/ChangeProfileAvatar";
    showProcessingPopup();
    document.getElementById("frmUserAvatar").submit();
   
}

function UpdateUserProfileInfo() {
    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
    document.getElementById("frmProfileInfo").action = siteBaseUrl + "ManageUsers/UpdateUserProfileInfo";
    showProcessingPopup();
    document.getElementById("frmProfileInfo").submit();
}

function UpdateUserContactInfo() {
    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
    document.getElementById("frmProfileContactInfo").action = siteBaseUrl + "ManageUsers/UpdateUserContactInfo";
    showProcessingPopup();
    document.getElementById("frmProfileContactInfo").submit();
}

function viewUserCameras(userId) {
    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
    document.getElementById("frmSiteUrl").action = siteBaseUrl + "ManageCamera/ViewUsersCamera?userId=" + userId;
    showProcessingPopup();
    document.getElementById("frmSiteUrl").submit();
}