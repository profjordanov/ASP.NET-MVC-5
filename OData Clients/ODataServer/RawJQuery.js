
    var taskServiceBaseAddress = "http://localhost:44301/odata/";
    var tasksBatch = [];

    function processResults(data) {
    
        //handle different return format for paging
        var results = data.d.results;
        if (results == null) {
            results = data.d;
        }

        //insert the tasks into the DOM
        applyTaskListTemplate(results);

        //if there are more results, get them now
        if (data.d.__next != null) {
            $.getJSON(data.d.__next, processResults);
        }
    }

    function applyTaskListTemplate(data) {
        $("#taskTemplate").tmpl(data).appendTo("#tasks");
    }


    function formatDate(rawDate) {
        evalDate = eval(rawDate.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"));
        return (evalDate.getMonth() + 1) + "/" + evalDate.getDate() + "/" + evalDate.getFullYear();
    }

    function formatDateForSubmit(inputDate) {
        var d = new Date(inputDate);

        return "\/Date(" + d.getTime() + ")\/";
    }

    function showStatus(status) {
        $("#statusMsg").html(status);

        $("#statusMsg").show(2000, function () {
            $("#statusMsg").hide(2000);
        });
    }

    function submitTask() {
        //{"Description":"matt sample","StartDate":"\/Date(1322719200000)\/","DueDate":"\/Date(1323928800000)\/","Priority":{"__metadata": {"uri":"http://localhost:44301/odata/Priorities(3)"}},"Status":{"__metadata":{"uri":"http://localhost:44301/odata/Statuses(1)"}},"AssignedTo":{"__metadata":{"uri":"http://localhost:44301/odata/Users(2)"}}}

        var newTask = {
            "Description": $("#taskDescription").val(),
            "StartDate": formatDateForSubmit($("#taskStartDate").val()),
            "DueDate": formatDateForSubmit($("#taskDueDate").val()),
            "Priority": { "__metadata": { "uri": taskServiceBaseAddress + "Priorities(" + $("#taskPriority").val() + ")"} },
            "Status": { "__metadata": { uri: taskServiceBaseAddress + "Statuses(" + $("#taskStatus").val() + ")"} },
            "AssignedTo": { "__metadata": { uri: taskServiceBaseAddress + "Users(" + $("#taskUser").val() + ")"} }
        };

        if ($("input[name='taskAddMode']:checked").val() == "single") {
            //single item to post

            $.ajax({
                url: "/odata/Tasks",
                type: "post",
                contentType: "application/json",
                dataType: "json",
                processData: false,
                data: JSON.stringify(newTask).replace(/[\/]/g, "\\/")
            }).error(function (data) {
                //showStatus(data.statusText);
               
                var errorObject = $.parseJSON(data.responseText);
                showStatus(errorObject.error.message.value);
            }).success(function (data) {
                showStatus("Successfully posted task with ID: " + data.d.ID);
            });
        } else {

            //add item to batch for submission
            tasksBatch.push(newTask);
            showStatus("Added task to batch");
            $("#batchSubmitButton").removeAttr("disabled");
           
        }

    }

    function submitBatch() {
        alert("Not implemented. :)");
    }

$(function () {
    //Load the status and priority drop downs


    $.getJSON(taskServiceBaseAddress + "Tasks?$expand=AssignedTo,Status,Priority",
               processResults);

    $.getJSON(taskServiceBaseAddress + "Priorities",
                function (data) {
                    $("#dropDownListTemplate").tmpl(data.d).appendTo("#taskPriority");
                });

    $.getJSON(taskServiceBaseAddress + "Statuses",
                function (data) {
                    $("#dropDownListTemplate").tmpl(data.d).appendTo("#taskStatus");
                });

    $.getJSON(taskServiceBaseAddress + "Users",
            function (data) {
                $("#dropDownListTemplate").tmpl(data.d).appendTo("#taskUser");
            });

    
});