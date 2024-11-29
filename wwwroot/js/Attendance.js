    // JavaScript for temporary storage, edit, delete, and final submission

    let tempRecords = [];

    function loadRecordsFromLocalStorage() {
        const savedRecords = localStorage.getItem("attendanceRecords");
    if (savedRecords) {
        tempRecords = JSON.parse(savedRecords);
    displayTempRecords();
        }
    }


    // Function to save the tempRecords array to local storage
    function saveRecordsToLocalStorage() {
        localStorage.setItem("attendanceRecords", JSON.stringify(tempRecords));
    }

    function setDateTimeDefaults() {
        const today = new Date();
    const formattedDate = today.toISOString().split('T')[0];
    document.querySelector("input[name='date']").value = formattedDate;
    }

    document.addEventListener('DOMContentLoaded', () => {
        setDateTimeDefaults();
    loadRecordsFromLocalStorage(); // Load records from local storage on page load
    });

    function convertTo24HourFormat(time) {
        if (!time) return '';
    const [hourPart, minutePart] = time.split(':');
    const period = time.slice(-2);
    let hours = parseInt(hourPart, 10);
    if (period === 'PM' && hours !== 12) hours += 12;
    if (period === 'AM' && hours === 12) hours = 0;
    return `${String(hours).padStart(2, '0')}:${minutePart.slice(0, 2)}`;
    }


    function addTemporaryRecord() {
        const empSelect = document.querySelector("select[name='Emp_Id']");
    const empId = parseInt(empSelect.value);
    const empName = empSelect.options[empSelect.selectedIndex].text;

    // Format date input to ensure compatibility
    const date = document.querySelector("input[name='date']").value;
        // const dateParts = dateInput.split('/');
        // const formattedDate = `${dateParts[2]}-${dateParts[0]}-${dateParts[1]}`; Convert MM/dd/yyyy to yyyy-MM-dd

    const startTime = document.querySelector("input[name='starttime']").value;
    const endTime = document.querySelector("input[name='endtime']").value || null;

    // Convert time values to TimeSpan strings (HH:mm:ss format)
    const formattedStartTime = startTime ? convertTo24HourFormat(startTime) : null;
    const formattedEndTime = endTime ? convertTo24HourFormat(endTime) : null;

    // Validate employee ID
    if (!empId) {
        alert("Please select a valid employee.");
    return;
        }
        const duplicate = tempRecords.some(record => record.empId === empId && record.date === date);
    if (duplicate) {
        alert("This employee is already in the attendance records for today.");
    return; // Exit the function without adding a duplicate
        }
    tempRecords.push({
        empId,
        empName,
        date,
        startTime: formattedStartTime,
    endTime: formattedEndTime
        });
    displayTempRecords();
    saveRecordsToLocalStorage();
    clearForm();
    }

    function formatDateMMDDYYYY(date) {
        const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are zero-based
    const day = String(date.getDate()).padStart(2, '0');
    const year = date.getFullYear();
    return `${month}/${day}/${year}`; // Format as MM/dd/yyyy
    }

    function displayTempRecords() {
        const tbody = document.querySelector("table tbody");
    tbody.innerHTML = '';
        tempRecords.forEach((record, index) => {

            const displayStartTime = record.startTime ? convertTo24HourFormat(record.startTime) : '';
    const displayEndTime = record.endTime ? convertTo24HourFormat(record.endTime) : '';

    const displayDate = record.date.includes('-')
    ? formatDateMMDDYYYY(new Date(record.date))
    : record.date;

    const row = `<tr>
        <td class="text-center">${index + 1}</td>
        <td class="text-center">${record.empName}</td> <!-- Display employee name here -->
        <td class="text-center">${displayDate}</td>
        <td class="text-center">${displayStartTime}</td>
        <td class="text-center">${displayEndTime}</td>
        <td class="text-center">
            <a href="javascript:void(0);" onclick="editRecord(${index})">Edit</a> |
            <a href="javascript:void(0);" style="color:darkred;" onclick="deleteRecord(${index})">Delete</a>
        </td>
    </tr>`;
    tbody.innerHTML += row;
        });
    }

    function clearForm() {
        document.querySelector("select[name='Emp_Id']").value = '';
    document.querySelector("input[name='date']").value = '';
    document.querySelector("input[name='starttime']").value = '';
    document.querySelector("input[name='endtime']").value = '';
    setDateTimeDefaults();
    }

    function editRecord(index) {
        const record = tempRecords[index];
    document.querySelector("select[name='Emp_Id']").value = record.empId;
    // Ensure date is in YYYY-MM-DD format for the date input
    document.querySelector("input[name='date']").value = record.date.includes('/')
    ? new Date(record.date).toISOString().split('T')[0]
    : record.date;

    // Convert time to HH:mm format if needed
    document.querySelector("input[name='starttime']").value = convertTo24HourFormat(record.startTime);
    document.querySelector("input[name='endtime']").value = convertTo24HourFormat(record.endTime);
    deleteRecord(index);
    }

    function deleteRecord(index) {
        tempRecords.splice(index, 1);
    displayTempRecords();
    saveRecordsToLocalStorage();
    }

    function finishDay() {
        const incompleteRecords = tempRecords.filter(record => !record.startTime || !record.endTime);
        if (incompleteRecords.length > 0) {
        alert("Some records are incomplete. Please complete them before finishing the day.");
    return;
        } 
        const transformedRecords = tempRecords.map(record => ({
        Id: record.empId, // Map empId to Id
    DateTime: `${record.date}T${record.startTime}`, // Combine date and startTime into DateTime
    StartTime: record.startTime, // Direct mapping
    EndTime: record.endTime, // Direct mapping
    Emp_Id: record.empId // Map empId to Emp_Id
        }));

    saveRecords(transformedRecords);
    }

    function saveRecords(transformedRecords) {
        console.log("Data being sent:", transformedRecords);
        #tr.value = value_record

    fetch('/Attendance/SaveRecords', {
        method: 'POST',
    headers: {'Content-Type': 'application/json' },
    body: JSON.stringify(transformedRecords)
        })
            .then(response => {
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
    return response.json();
            })
            .then(data => {
                if (data.success) {
        alert("Attendance records saved successfully!");
    tempRecords = [];
    transformedRecords = [];
    displayTempRecords();
    localStorage.removeItem("attendanceRecords");
                } else {
        console.log(transformedRecords);
    alert("Failed to save records. Please check the input data.");
                }
            })
            .catch(error => {
        console.error("Error saving records:", error);
    alert("An error occurred while saving records.");
            });
    }


