function confirmDelete(id) {
    // Confirm the deletion
    if (confirm('Are you sure that you want to delete this record?')) {
        // Send a POST request to delete the employee
        fetch(`/Employee/Delete/${id}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': '@Antiforgery.GetTokens(HttpContext).RequestToken' // CSRF token for security if needed
            }
        })
            .then(response => {
                if (response.ok) {
                    alert("Employee deleted successfully.");
                    window.location.reload(); // Reload the page to update the list
                } else {
                    alert("Error: Could not delete the employee.");
                }
            })
            .catch(error => console.error('Error:', error));
    }
}