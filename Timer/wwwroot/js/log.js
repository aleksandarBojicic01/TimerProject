$(document).ready(function () {
    $("#button-start").click(function () {
        var customerId = $("#cust-select").val(); 
        var categoryId = $("#cat-select").val();
        var taskId = $("#task-select").val();
        var billable = $("#TimeLog_Billable").is(":checked");
        var notes = $("#notesarea").val();
        var url = $(this).data("url");

        var dataToSend = {
            TimeLog: {
                CustomerId: customerId,
                CategoryId: categoryId,
                TaskId: taskId,
                Billable: billable,
                Notes: notes
            }
        };

        console.log('JSON Data:', JSON.stringify(dataToSend));

        $.ajax({
            url: url, 
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(dataToSend),
            success: function (response) {
                console.log('Timer started successfully.');
            },
            error: function (error) {
                console.error('Error starting the timer:', error);
            }
        });
    });
});