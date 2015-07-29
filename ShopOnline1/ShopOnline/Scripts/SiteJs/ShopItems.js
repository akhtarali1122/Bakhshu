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
        $("#txtCompany").keyup(function () {
            oTable1.fnFilter(this.value, 1);
        });
        $("#txtBrand").keyup(function () {
            oTable1.fnFilter(this.value, 2);
        });
    }
    if (document.getElementById("btnCancelEditItem")) {
        $("#btnCancelEditItem").click(function () {
            canceleEditItem();
        });
    }
});

function EditItem(Item_ID) {
    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
    document.getElementById("frmSiteUrl").action = siteBaseUrl + "ShopItems/Index?hidItemId=" + Item_ID;
    showProcessingPopup();
    document.getElementById("frmSiteUrl").submit();
}
function deleteItem(Item_ID) {

    bootbox.confirm("Are you sure, you want to de-activate this user ?", function (result) {
        if (result == true) {
            var siteBaseUrl = document.getElementById("frmSiteUrl").action;
            document.getElementById("frmSiteUrl").action = siteBaseUrl + "ShopItems/DeleteItem?ItemId=" + Item_ID;
            showProcessingPopup();
            document.getElementById("frmSiteUrl").submit();
        }
    });
}
function canceleEditItem() {
    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
    document.getElementById("frmSiteUrl").action = siteBaseUrl + "ShopItems/ManageShopItems";
    document.getElementById("frmSiteUrl").submit();
}