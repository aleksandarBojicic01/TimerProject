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
                    RefreshTable();
                    toastr.success(data.message);
                }
            })
        }
    })
}

function RefreshTable() {
    $("#tblTasks").load(" #tblTasks");
}
