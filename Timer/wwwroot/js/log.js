$(document).ready(function () {
    $("#button-start").click(function () {
        var customerId = $("#cust-select").val(); // Assuming cust-select is the ID of the customer select element
        var categoryId = $("#cat-select").val(); // Assuming cat-select is the ID of the category select element
        var taskId = $("#task-select").val(); // Assuming task-select is the ID of the task select element
        var billable = $("#TimeLog_Billable").is(":checked");
        var notes = $("#notesarea").val();
        var url = $(this).data("url");

        // Prepare the data to send in the request body
        var dataToSend = {
            TimeLog: {
                CustomerId: customerId,
                CategoryId: categoryId,
                TaskId: taskId,
                Billable: billable,
                Notes: notes
                // Add other properties here
            }
        };

        console.log('JSON Data:', JSON.stringify(dataToSend));

        $.ajax({
            url: url, // Replace with the actual URL
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(dataToSend),
            success: function (response) {
                // Handle success response, if needed
                console.log('Timer started successfully.');
                // Optionally, update the UI to reflect the timer has started
            },
            error: function (error) {
                // Handle error, if needed
                console.error('Error starting the timer:', error);
            }
        });
    });

    // Other JavaScript code...
});