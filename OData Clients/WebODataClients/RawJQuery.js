
    var taskServiceBaseAddress = "http://localhost:44301/odata/";

    function processResults(data) {
        var results = data.d.results;
        if (results == null) {
            results = data.d;
        }

        //insert the tasks into the DOM
        applyTaskListTemplate(results);

        //if there are more results, get them now
        //using the format and callback parameters to get the result
        //requires enabling the JSONPSupportBehavior on the TaskDataService
        //in the ODataServer project
        if (data.d.__next != null) {
            $.getJSON(data.d.__next + "&$format=json&$callback=?", processResults);
        }
    }

    function applyTaskListTemplate(data) {
        $("#taskTemplate").tmpl(data).appendTo("#tasks");
    }


    function formatDate(rawDate) {
        evalDate = eval(rawDate.replace(/\/Date\((\d+)\)\//gi, "new Date($1)"));
        return (evalDate.getMonth() + 1) + "/" + evalDate.getDate() + "/" + evalDate.getFullYear();
    }

   

    function showStatus(status) {
        $("#statusMsg").html(status);

        $("#statusMsg").show(2000, function () {
            $("#statusMsg").hide(2000);
        });
    }

    function submitTask() {
        showStatus("not enabled for jsonp, cross-x-domain");
    }


    $(function () {
        //Load the status and priority drop downs
        $(document).ajaxError(function (e, jqxhr, settings, exception) {
            showStatus(e);
        });

        //note use of the callback and format parameters for x-domain support
        //requires enabling the JSONPSupportBehavior on the TaskDataService
        //in the ODataServer project
        $.getJSON(taskServiceBaseAddress + "Tasks?$expand=AssignedTo,Status,Priority&$format=json&$callback=?", processResults);

        $.getJSON(taskServiceBaseAddress + "Priorities?$format=json&$callback=?",
                function (data) {
                    $("#dropDownListTemplate").tmpl(data.d).appendTo("#taskPriority");
                });

        $.getJSON(taskServiceBaseAddress + "Statuses?$format=json&$callback=?",
                function (data) {
                    $("#dropDownListTemplate").tmpl(data.d).appendTo("#taskStatus");
                });

        $.getJSON(taskServiceBaseAddress + "Users?$format=json&$callback=?",
            function (data) {
                $("#dropDownListTemplate").tmpl(data.d).appendTo("#taskUser");
            });


    });