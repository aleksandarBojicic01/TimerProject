$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblTasks').DataTable({
        "ajax": { url: '/task/getall' },
        
        "columns": [
            { data: 'sequentialNumber', "width": "5%" },
            { data: 'name', "width": "10%" },
            { data: 'customer.name', "width": "10%" },
            { data: 'category.name', "width": "10%" },
            { data: 'identityUser.userName', "width": "15%" },
            { data: 'estimatedHours', "width": "5%" },
            {
                "data": "startDate",
                "render": function (data, type, row) {
                    if (type === 'display' || type === 'filter') {
                        // Format date for display and filtering
                        return moment(data).format('DD/MM/YYYY');
                    }
                    // Use the original data for sorting and other operations
                    return data;
                },
                "width": "10%"
            },
            {
                "data": "endDate",
                "render": function (data, type, row) {
                    if (type === 'display' || type === 'filter') {
                        // Format date for display and filtering
                        return moment(data).format('DD/MM/YYYY');
                    }
                    // Use the original data for sorting and other operations
                    return data;
                },
                "width": "10%"
            },
            { data: 'priority', "width": "5%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/task/edit?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                    <a onClick=Delete('/task/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "20%"
            }
        ]
    });
}

const table = document.querySelector('#tblTasks'); 
function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#2c3e50',
        confirmButtonText: 'Delete Task'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                  // RefreshTable();
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}

function RefreshTable() {
    $("#tblTasks").load(" #tblTasks");
}
