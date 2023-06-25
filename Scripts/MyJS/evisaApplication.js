// Function to check passport number
function checkPassportNumber() {
    // Get the value of the passport number input field
    var passportNumber = document.getElementById("passportnumber").value;
    // Get the error element to display error messages
    var errorElement = document.getElementById("passport-error");

    // Regular expression to validate passport number
    var regex = /^[A-Z0-9]{6,}$/;

    if (passportNumber.trim() === "") {
        // If passport number is empty, display an error message
        errorElement.textContent = "Passport number is required";
    } else if (!regex.test(passportNumber)) {
        // If passport number does not match the regular expression, display an error message
        errorElement.textContent = "Invalid passport number";
    } else {
        // If the passport number is valid, clear the error message
        errorElement.textContent = "";
    }
}

// Function to check entry date
function checkEntrydate() {
    // Get the value of the entry date input field
    var entrydate = document.getElementById('entrydate').value;
    // Get today's date
    var today = new Date().toISOString().split('T')[0];

    if (entrydate === '') {
        // If entry date is empty, display an error message
        document.getElementById('entry-error').textContent = 'Entry date is required.';
    } else if (entrydate < today) {
        // If entry date is in the past, display an error message
        document.getElementById('entry-error').textContent = 'Past dates are not allowed.';
    } else {
        // If the entry date is valid, clear the error message
        document.getElementById('entry-error').textContent = '';
    }
}

// Function to check departure date
function checkDeparturedate() {
    // Get the value of the departure date input field
    var departuredate = document.getElementById('departuredate').value;
    // Get today's date
    var today = new Date().toISOString().split('T')[0];

    if (departuredate === '') {
        // If departure date is empty, display an error message
        document.getElementById('departure-error').textContent = 'Departure date is required.';
    } else if (departuredate < today) {
        // If departure date is in the past, display an error message
        document.getElementById('departure-error').textContent = 'Past dates are not allowed.';
    } else {
        // If the departure date is valid, clear the error message
        document.getElementById('departure-error').textContent = '';
    }
}


function validateForm() {
    checkPassportNumber();
    checkEntrydate();
    checkDeparturedate();
}