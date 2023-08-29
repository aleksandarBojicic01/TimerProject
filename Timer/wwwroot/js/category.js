$(document).ready(function() {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblCategories').DataTable({
        "ajax": { url: '/category/getall' },
        "columns": [
            { data: 'name', "width": "15%" },
            { data: 'description', "width": "30%" },
            { data: 'billable', "width": "10%" },
            { data: 'active', "width": "10%" },
            { data: 'vat', "width": "10%" },
            {
                data: 'id',
                "render": function(data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/category/edit?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit</a>
                    <a onClick=Delete('/category/delete/${data}') class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                },
                "width": "25%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#2c3e50',
        confirmButtonText: 'Delete Category'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    })
}