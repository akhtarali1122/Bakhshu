
var siteBaseUrl = "";

$(document).ready(function () {

    siteBaseUrl = document.getElementById("frmSiteUrl").action;
});

if (document.getElementById("frmSiteUrl")) {
    siteBaseUrl = document.getElementById("frmSiteUrl").action;
}

function getAjaxLoaderImage() {
    return "<img src='" + siteBaseUrl + "Content/AdminContents/img/ajax-loader.gif' alt='loader' />";
}


function replace(fullString, text, by) {
    // Replaces text with by in string
    var strLength = fullString.length, txtLength = text.length;
    if ((strLength == 0) || (txtLength == 0)) return fullString;

    var i = fullString.indexOf(text);
    if ((!i) && (text != fullString.substring(0, txtLength))) return fullString;
    if (i == -1) return fullString;

    var newstr = fullString.substring(0, i) + by;

    if (i + txtLength < strLength)
        newstr += replace(fullString.substring(i + txtLength, strLength), text, by);

    return newstr;
}

function LTrim(str) {
    var whitespace = new String(" \t\n\r ");
    var s = new String(str);
    if (whitespace.indexOf(s.charAt(0)) != -1) {
        var j = 0, i = s.length;
        while (j < i && whitespace.indexOf(s.charAt(j)) != -1)
            j++;
        s = s.substring(j, i);
    }
    return s;
}
function RTrim(str) {
    var whitespace = new String(" \t\n\r ");
    var s = new String(str);
    if (whitespace.indexOf(s.charAt(s.length - 1)) != -1) {
        var i = s.length - 1;
        while (i >= 0 && whitespace.indexOf(s.charAt(i)) != -1)
            i--;
        s = s.substring(0, i + 1);
    }
    return s;
}
function Trim(str) {
    return RTrim(LTrim(str));
}

function GetWindowWidth() {
    if (document.documentElement && (document.documentElement.clientWidth)) //IE 6+ in 'standards compliant mode'
        return document.documentElement.clientWidth;
    else if (document.body && (document.body.clientWidth)) //IE 4 compatible
        return document.body.clientWidth;
    else if (typeof (window.innerWidth) == 'number')   //Non-IE
        return window.innerWidth;

    return screen.width;
}

function GetWindowHeight() {
    if (document.documentElement && (document.documentElement.clientHeight)) //IE 6+ in 'standards compliant mode'
        return document.documentElement.clientHeight;
    else if (document.body && (document.body.clientHeight)) //IE 4 compatible
        return document.body.clientHeight;
    else if (typeof (window.innerHeight) == 'number') //Non-IE
        return window.innerHeight;

    return screen.hight;
}

function getInternetExplorerVersion()
{
    var rv = -1; // Return value assumes failure.
    if (navigator.appName == 'Microsoft Internet Explorer') {
        var ua = navigator.userAgent;
        var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
        if (re.exec(ua) != null)
            rv = parseFloat(RegExp.$1);
    }
    return rv;
}

function toggleMe(elementId) {
    $("#" + elementId).slideToggle("slow");
}

function hideElement(elementId) {
    $("#" + elementId).hide("slow");
}
function showElement(elementId) {
    $("#" + elementId).show("slow");
}



function showProcessingPopup() {

  
    if (siteBaseUrl == "")
        siteBaseUrl = document.getElementById("frmSiteUrl").action;

    var screenWidth = GetWindowWidth();
    var screenHeight = GetWindowHeight();
    var leftMargin = (screenWidth / 2) - 100;
    var topMargin = (screenHeight / 2) - 30;

    document.getElementById("divProcessingPopup").style.display = "";
    document.getElementById("divProcessingPopup").style.left = leftMargin + "px";
    document.getElementById("divProcessingPopup").style.top = topMargin + "px";
}

function hideProcessingPopup() {
    document.getElementById("divProcessingPopup").style.display = "none";
}



function checkImageSize(imageElementId) {

var fileInput = $("#" + imageElementId)[0];
        var imgbytes = fileInput.files[0].fileSize;
   

}    


function setTextForAjax(txtStr) {
    var retParam = "";
    if (txtStr != null && txtStr != "undefined") {
        retParam = txtStr;
        retParam = retParam.replace("&nbsp;", "vs****Space****vs");
        retParam = retParam.replace("&", "vs****And****vs");
        retParam = retParam.replace("#", "vs****Hash****vs");
        retParam = retParam.replace("%", "vs****Percent****vs");
        retParam = retParam.replace("@", "vs****AtRateOf****vs");
        retParam = retParam.replace("~", "vs****Tiled****vs");
        retParam = retParam.replace("`", "vs****SmallTiled****vs");
        retParam = retParam.replace("!", "vs****Not****vs");
        retParam = retParam.replace("^", "vs****Power****vs");
        return retParam;
    }
    return txtStr;
}


function getOperationMessage(messageTitle, messageText, messageCssClass) {

    var retHtml = "<div id='responseMessage'>";
    retHtml = retHtml + "<div class='alert " + messageCssClass + "'>";
    retHtml = retHtml + "<button type='button' class='close' data-dismiss='alert'><i class='icon-remove'></i></button>";
    retHtml = retHtml + "<p><strong>" + messageTitle + "</strong>";
    retHtml = retHtml + "<br />" + messageText + "<br /></p>";
    retHtml = retHtml + "</div></div>";
    return retHtml;
}
function getErrorMessage(messageTitle,messageText)
{
    return getOperationMessage(messageTitle, messageText, "alert-danger")
}

function getNoticeMessage(messageTitle, messageText)
{
    return getOperationMessage(messageTitle, messageText, "alert-warning")
}
function getInformationMessage(messageTitle, messageText)
{
    return getOperationMessage(messageTitle, messageText, "alert-info")
}
function getSuccessMessage(messageTitle, messageText) {
    return getOperationMessage(messageTitle, messageText, "alert-success")
}

function checkUsernameAvailability(LoginId, MsgContainer) {

    if (LoginId != "") {
        $(MsgContainer).get()[0].innerHTML = getAjaxLoaderImage();
        $.ajax({
            type: "GET",
            url: siteBaseUrl + "Register/CheckUserAvailability?loginId=" + LoginId,
            success: function (response) {
                if (response == "1") {
                    $(MsgContainer).get()[0].innerHTML = "<span style='color:Green'>Available</span>";
                }
                else {
                    $(MsgContainer).get()[0].innerHTML = "<span style='color:Red'>Not Available</span>";
                }
            },
            error: function (ex) {
            }
        });
    }
}
