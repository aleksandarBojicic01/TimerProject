$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblOverview').DataTable({
        "ajax": { url: '/overview/getall' },
        "columnDefs": [
            {
                "targets": [7],
                "className": "text-center",
                "orderable": false,
            }
        ],

        "columns": [
            
            {
                data: 'date',
                "render": function (date, type, row) {
                    if (type === 'display' || type === 'filter') {
                        return moment(date).format('DD/MM/YYYY');
                    }
                    return date;
                },
                "width": "15%"
            },

            { data: 'customer.name', "width": "15%" },
            { data: 'category.name', "width": "15%" },
            { data: 'task.name', "width": "10%" },
            { data: 'startTime', "width": "10%" },
            { data: 'endTime', "width": "10%" },
            { data: 'duration', "width": "15%" },
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
        ],
    });    
        
}

 