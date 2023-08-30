$(document).ready(function() {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblCategories').DataTable({
        "ajax": { url: '/category/getall' },
        "columnDefs" : [
            {
                "targets": [2, 3],
                "className": "text-center",
                "orderable": false,
            }
        ],
        "columns": [
            { data: 'name', "width": "15%" },
            { data: 'description', "width": "30%" },
            {
                data: 'billable',
                "render": function (data) {
                    if (data === true) {
                        return '<input type="checkbox" checked name="id" onclick="return false" class="m-auto" style="vertical-align: middle; position: relative; ">'
                    }
                    return '<input type="checkbox" name="id" onclick="return false" class="m-auto">'
                
            },
                "width": "10%"
            },
          {
              data: 'active',
              "render": function (data) {
                  if (data === true) {
                      return '<input type="checkbox" checked name="id" onclick="return false" class="m-auto" style="vertical-align: middle; position: relative; ">'
                  }
                  return '<input type="checkbox" name="id" onclick="return false" class="m-auto">'

              },
              "width": "10%"
          },
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