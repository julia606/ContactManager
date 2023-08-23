let dataTable;
$(document).ready(function () {
    initTable();

    $('#userTable').on('click', '.edit-btn', function () {
        var userId = $(this).data('id');

        $.get('/UserData/GetUserData/' + userId, function (data) {

            $('#userId').val(data.Id);
            $('#name').val(data.Name);
            $('#dob').val(data.DateOfBirth);
            $("#married").prop("checked", data.Married);
            $('#phone').val(data.Phone);
            $('#salary').val(data.Salary);
            $('#contactModal').modal('show');
        });
    });

    $('#saveChangesBtn').on('click', function () {
        var userId = $('#userId').val();
        var name = $('#name').val();
        var dob = $('#dob').val();
        var married = $("#married").prop("checked");
        var phone = $('#phone').val();
        var salary = $('#salary').val();

        $.ajax({
            url: '/UserData/UpdateUser/' + userId,
            type: 'POST',
            data: {
                Id: userId,
                Name: name,
                DateOfBirth: dob,
                Married: married,
                Phone: phone,
                Salary: salary,
            },
            success: function (data) {
                $('#contactModal').modal('hide');

                var updatedData = {
                    "Id": data.Id,
                    "Name": data.Name,
                    "DateOfBirth": data.DateOfBirth,
                    "Married": data.Married ? "Yes" : "No",
                    "Phone": data.Phone,
                    "Salary": data.Salary
                };

                dataTable.row('#rowId').data(updatedData).draw(false);
                window.location.reload();
            },
            error: function (error) {
                console.log(error);
            }
        });
    });


    $('#userTable').on('click', '.remove-btn', function () {
        var userId = $(this).data('id');

        $.ajax({
            url: '/UserData/DeleteUser/' + userId,
            type: 'DELETE',
            success: function (data) {
                dataTable.row('#rowId').remove().draw(false);
                window.location.reload();
            },
            error: function (error) {
                console.log(error);
                // Обробка помилки (якщо необхідно)
            }
        });

    });
});
function initTable() {
    dataTable = new DataTable('#userTable', {
        order: [[1, 'desc']],
        info: false
    });
}


