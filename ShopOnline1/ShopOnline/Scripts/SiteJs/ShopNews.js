$(document).ready(function () {
    if (document.getElementById("ebizlineTreeTable")) {
        var oTable1 = $('#ebizlineTreeTable').dataTable({
            "bJQueryUI": true,
            "bPaginate": false,
            "bSort": true,
            "bInfo": false,
        });

        $("#txtNewsHeadLines").keyup(function () {
            oTable1.fnFilter(this.value, 0);
        });
    }

    if (document.getElementById("btnCancelEditNews")) {
        $("#btnCancelEditNews").click(function () {
            canceleEditNews();
        });
    }
   
});
function EditNews(News_ID) {
    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
    document.getElementById("frmSiteUrl").action = siteBaseUrl + "ShopNews/AddNewsForm?hidNewsId=" + News_ID;
    showProcessingPopup();
    document.getElementById("frmSiteUrl").submit();
}
function deleteNews(News_ID) {

    bootbox.confirm("Are you sure, you want to de-activate this user ?", function (result) {
        if (result == true) {
            var siteBaseUrl = document.getElementById("frmSiteUrl").action;
            document.getElementById("frmSiteUrl").action = siteBaseUrl + "ShopNews/DeleteNews?NewsId=" + News_ID;
            showProcessingPopup();
            document.getElementById("frmSiteUrl").submit();
        }
    });
}
function canceleEditNews() {
    var siteBaseUrl = document.getElementById("frmSiteUrl").action;
    document.getElementById("frmSiteUrl").action = siteBaseUrl + "ShopNews/ManageShopNews";
    document.getElementById("frmSiteUrl").submit();
}