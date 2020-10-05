
$(document).ready(function () {
    $("#btnLoadReport").click(function () {

    });



});


var ReportManager = {

    LoadReport:function(){
        var jsonparam = "";
        var serviceurl = "../Employee/GenerateEmployeeReport";
        ReportManager.GetReport(serviceUrl, jsopParam, onFailed);
        function onFailed(error) {
            alert("Found error");

        }
    },


    GetReport: function (serviceUrl, jsopParams, errorCallback) {
        $.ajax({
            url: serviceUrl,
            async: false,
            type: "POST",
            data: "{" + jsopParams + "}",
            contentType: "application/json; charset=utf-8",
            success: function () {
                window.open('../Report/ReportViewer.aspx','_newtab');

            },
            error: errorCallback
        });
        
    }

}
