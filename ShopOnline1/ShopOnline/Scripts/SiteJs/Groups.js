$(document).ready(function () {
    if (document.getElementById("ebizlineTreeTable")) {
        var oTable1 = $('#ebizlineTreeTable').dataTable({
            "bJQueryUI": true,
            "bPaginate": false,
            "bSort": true,
            "bInfo": false,
        });

        $("#txtName").keyup(function () {
            oTable1.fnFilter(this.value, 0);
        });
     
    }
    if (document.getElementById("btnCancelEditItem")) {
        $("#btnCancelEditItem").click(function () {
            canceleEditItem();
        });
    }
});
function EditGroup(Group_ID) {
    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
    document.getElementById("frmSiteUrl").action = siteBaseUrl + "Groups/CreateGroup?groupId=" + Group_ID;
    showProcessingPopup();
    document.getElementById("frmSiteUrl").submit();
}