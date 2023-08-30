$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblUsers').DataTable({
        "ajax": { url: '/user/getall' },
        "columns": [
            { data: 'userName', "width": "30%" },
            { data: 'email', "width": "30%" }
        ]
    });
}
