// employee.js

$(document).ready(function() {
    // Handle row click to select a row
    $('.clickable-row').on('click', function() {
        var rowId = $(this).data('id');
        $('#selected-row-id').val(rowId);  // Store selected row ID
        $('#edit-button').prop('disabled', false);  // Enable the Edit button
        
        // Dynamically set the href attribute for the Edit button with the selected employee ID
        $('#edit-button').attr('href', '/Employee/Edit/' + rowId);

        // Highlight the selected row
        $('#data-table tbody tr').removeClass('table-active');  // Remove highlight from all rows
        $(this).addClass('table-active');  // Add highlight to selected row
    });

    // Function to open the modal with the selected row's data
    $('#edit-button').on('click', function (event) {
        event.preventDefault();
        var selectedId = $('#selected-row-id').val();
        if (!selectedId) {
            alert('Please select a row first.');
            return;
        }

        // Make an AJAX call to get the row data
        $.ajax({
            url: '/Employee/GetEmployeeData',  // Call your controller action
            type: 'GET',
            data: { id: selectedId },
            success: function (employee) {
                // Populate the form fields with employee data
                //$('#edit-id').val(employee.ID);
                $('#edit-name').val(employee.Name);
                $('#edit-Address').val(employee.Address);
                $('#edit-Email').val(employee.Email);
                $('#edit-ContactNumber').val(employee.ContactNumber);
                $('#edit-BirthDate').val(employee.BirthDate);
                $('#edit-Gender_id').val(employee.Gender_id);
                $('#edit-date_of_contract').val(employee.date_of_contract);
                $('#edit-start_time').val(employee.start_time);
                $('#edit-end_time').val(employee.end_time);
                $('#edit-Salary').val(employee.Salary);
                $('#edit-SSN').val(employee.SSN);
                $('#edit-Nationality').val(employee.Nationality);
                $('#edit-Note').val(employee.Note);
                
                // Show the modal
                $('#editModal').modal('show');
            },
            error: function () {
                alert('Failed to load employee data.');
            }
        });
    });
});

function saveChanges() {
    var formData = $('#editForm').serialize(); // Serialize the form data

    $.ajax({
        url: '/Employee/Edit',  // Adjust the URL to your controller action
        type: 'POST',
        data: formData,
        success: function (response) {
            if (response.success) {
                $('#editModal').modal('hide');
                location.reload();  // Reload the page to reflect changes (or update dynamically)
            } else {
                alert('Failed to save changes.');
            }
        },
        error: function () {
            alert('Error saving the employee data.');
        }
    });
}
