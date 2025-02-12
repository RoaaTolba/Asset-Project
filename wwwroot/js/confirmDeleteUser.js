function confirmDeleteUser(id) {
    if (confirm('Are you sure that you want to delete this record?')) {
        const csrfToken = document.querySelector('input[name="__RequestVerificationToken"]').value; // Get token

        fetch(`/User/Delete/${id}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': csrfToken // Pass the token
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
