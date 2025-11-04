
//let table;

$(document).ready(function () {
     loadDataTable();
});

function loadDataTable() {
    $('#tblData').DataTable({
        "ajax": { url: '/Admin/product/getall'},
    
        "columns": [
            { data: 'title', "width": "15%" },
            { data: 'author', "width": "10%" },
            { data: 'isbn', "width": "10%" },
            {
                data: 'listPrice',
                "render": function (data) {
                    return `<th scope="col">${data} Rial</th>`
                },
                "width": "5%"
            },
            { data: 'category.name', "width": "5%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                                <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2"> <i class="bi bi-pencil-square"></i> Edit </a>
                                <a href="/admin/product/delete?id=${data}" class="btn btn-danger mx-2"> <i class="bi bi-trash-fill"> </i> Delete </a>
                            </div>`
                },
                "width": "20%"
            },
        ]
    });
}
