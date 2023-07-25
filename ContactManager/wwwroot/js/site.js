// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
let dataTable;
$(document).ready(function () {
    initTable();
});
function initTable() {
    dataTable = new DataTable('#userTable', {
        order: [[1, 'desc']],
        info: false         
    });

}