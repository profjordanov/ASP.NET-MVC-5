
var taskServiceBaseAddress = "http://localhost:44301/odata/";
var tasksBatch = [];

function processResults(data) {
    var results = data.results;
//    if (results == null) {
//        results = data.d;
//    }

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
    //{"Description":"matt sample","StartDate":"\/Date(1322719200000)\/","DueDate":"\/Date(1323928800000)\/","Priority":{"__metadata": {"uri":"http://localhost:44301/odata/Priorities(3)"}},"Status":{"__metadata":{"uri":"http://localhost:44301/odata/Statuses(1)"}},"AssignedTo":{"__metadata":{"uri":"http://localhost:44301/odata/Users(2)"}}}

    var newTask = {
        "Description": $("#taskDescription").val(),
        "StartDate": $("#taskStartDate").val(),
        "DueDate": $("#taskDueDate").val(),
        "Priority": { "__metadata": { "uri": taskServiceBaseAddress + "Priorities(" + $("#taskPriority").val() + ")"} },
        "Status": { "__metadata": { uri: taskServiceBaseAddress + "Statuses(" + $("#taskStatus").val() + ")"} },
        "AssignedTo": { "__metadata": { uri: taskServiceBaseAddress + "Users(" + $("#taskUser").val() + ")"} }
    };

    if ($("input[name='taskAddMode']:checked").val() == "single") {
        //single task - submit with POST
        OData.request({
            requestUri: "/odata/Tasks",
            method: "POST",
            data: newTask
        },
        function (insertedItem) {

            showStatus("Added task with ID: " + insertedItem.ID);
        },
        function (error) {
            //showStatus(error.message);
            var errorObject = $.parseJSON(error.response.body);
            showStatus(errorObject.error.message.value);
        });
    } else {
        //add item to the batch collection
        tasksBatch.push(newTask);
        showStatus("Task added to batch");
        $("#batchSubmitButton").removeAttr("disabled");
    }

}

function submitBatch() {
    var changeRequests = [];
    //create a change request for each task
    for (var i = 0; i < tasksBatch.length; i++) {
        changeRequests.push(
            {
                requestUri: "Tasks",
                method: "POST",
                data: tasksBatch[i]
            });
        }
    var requestData = { __batchRequests: [{ __changeRequests: changeRequests}]};

    OData.request({
        requestUri: taskServiceBaseAddress + "$batch",
        method: "POST",
        data: requestData
    }, function (data) {
        // build up any error messages
        var errorMessages = [];

        var errorsFound = false;
        for (var i = 0; i < data.__batchResponses.length; i++) {
            var batchResponse = data.__batchResponses[i];
            for (var j = 0; j < batchResponse.__changeResponses.length; j++) {
                var changeResponse = batchResponse.__changeResponses[j];
                if (changeResponse.message) {
                    errorsFound = true;
                    errorMessages.push(changeResponse.message);
                }
            }
        }

        if (errorsFound) {
            showStatus(errorMessages.join(","));
        }
        else {
            showStatus("Batch submitted");
        }
    }, function (err) {
        showStatus(err.message);
    }, OData.batchHandler);

}

$(function () {
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