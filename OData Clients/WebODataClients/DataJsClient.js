
var taskServiceBaseAddress = "http://localhost:44301/odata/";

var tasksBatch = [];

function processResults(data) {
    var results = data.results;

    //insert the tasks into the DOM
    applyTaskListTemplate(results);

    //if there are more results, get them now
    if (data.__next != null) {
        OData.read(data.__next, processResults);
    }
}



function applyTaskListTemplate(data) {
    $("#taskTemplate").tmpl(data).appendTo("#tasks");
}

function formatDate(rawDate) {
    return (rawDate.getMonth() + 1) + "/" + rawDate.getDate() + "/" + rawDate.getFullYear();
}

function showStatus(status) {
    $("#statusMsg").html(status);

    $("#statusMsg").show(1000, function () {
        $("#statusMsg").hide(3000);
    });
}

function submitTask() {
    showStatus("not enabled for jsonp, cross-x-domain");

}

function submitBatch() {
    showStatus("not enabled for jsonp, cross-x-domain");

}

$(function () {

    
    //enable jsonp functionality for queries
    OData.defaultHttpClient.formatQueryString = "$format=json";
    OData.defaultHttpClient.callbackParameterName = "$callback";
    OData.defaultHttpClient.enableJsonpCallback = true;

    //let the client library know about the date types on task
    OData.defaultMetadata.push(
    { namespace: "ODataServer.Models",
        entityType: [{
            name: "Task", property: [
                { name: "DueDate", type: "Edm.DateTime" },
                { name: "StartDate", type: "Edm.DateTime" }
            ]
        }]

    });


    //load the tasks
    OData.read(taskServiceBaseAddress + "Tasks?$expand=AssignedTo,Status,Priority",
               processResults);

    //Load the status, user and priority drop downs
    OData.read(taskServiceBaseAddress + "Priorities",
        function (data) {
            $("#dropDownListTemplate").tmpl(data.results).appendTo("#taskPriority");
        });

    OData.read(taskServiceBaseAddress + "Statuses",
        function (data) {
            $("#dropDownListTemplate").tmpl(data.results).appendTo("#taskStatus");
        });

    OData.read(taskServiceBaseAddress + "Users",
        function (data) {
            $("#dropDownListTemplate").tmpl(data.results).appendTo("#taskUser");
        });



});