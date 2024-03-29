////$(document).ready(function () {
////    $('#userTable').DataTable();
////});

//var _$table = $('#userTable');

//var _$userTable = _$table.DataTable({
//    "language": {
//        "url": "//cdn.datatables.net/plug-ins/1.10.24/i18n/Portuguese-Brasil.json"
//    },
//    "processing": true,
//    "serverSide": true,
//    "ajax": {
//        "url": "/User/GetAllUsers", 
//        "type": "POST", 
//        "dataType": "json",
//        "data": function (data) {
//            data.page = data.start / data.length + 1;
//            data.pageSize = data.length; 

//            console.log(data);
//            return data;
//        }
//    },
//    "columns": [
//        { "data": "1" }, 
//        //{ "data": "Id" }, 
//        { "data": "fullName" }, 
//        { "data": "cpf" }, 
//        { "data": "email" },
//        {
//            "data": null,
//            "render": function (data, type, row, meta) {
//                return '<button onclick="editUser(' + data.Id + ')">Editar</button>' +
//                    '<button onclick="deleteUser(' + data.Id + ')">Excluir</button>';
//            }
//        }
//    ]
//});
