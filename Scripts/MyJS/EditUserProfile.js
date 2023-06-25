// First name validation
function CheckFirstName() {
    var isName = /^[a-zA-Z]+$/;
    let fnameValue = document.getElementById("fname");
    if (fnameValue.value.trim() === "") {
        setErrorFor(fnameValue, "First name is required");
    } else if (!isName.test(fnameValue.value.trim())) {
        setErrorFor(fnameValue, "First name cannot contain numbers or special characters");
    } else {
        setSuccessFor(fnameValue);
    }
}




// Last name validation
const dobInput = document.getElementById("dateofbirth");
const today = new Date().toISOString().split("T")[0];
dobInput.setAttribute("max", today);

function CheckLastName() {
    var isName = /^[a-zA-Z]+$/;
    let lnameValue = document.getElementById("lname");
    if (lnameValue.value.trim() === "") {
        setErrorFor(lnameValue, "Last name is required");
    } else if (!isName.test(lnameValue.value.trim())) {
        setErrorFor(lnameValue, "Last name cannot contain numbers or special characters");
    } else {
        setSuccessFor(lnameValue);
    }
}

// Date of birth validation
function CheckDateOfBirth() {
    let dobValue = document.getElementById("dateofbirth");
    if (dobValue.value.trim() === "") {
        setErrorFor(dobValue, "Date of birth is required");
    } else {
        setSuccessFor(dobValue);
    }
}


// Address validation
function CheckAddress() {
    let address = document.getElementById("address");

    if (address.value.trim() === "") {
        setErrorFor(address, "Address is required");
    } else {
        setSuccessFor(address);
    }
}



// Phone number validation
function CheckPhoneNumber() {
    let phoneNumber = document.getElementById("phonenumberid");
    let phoneNumberValue = phoneNumber.value.trim();
    let validRegex = /^\d{10}$/; // Regular expression for a 10-digit phone number

    if (phoneNumberValue === "") {
        setErrorFor(phoneNumber, "Phone number is required");
    } else if (!validRegex.test(phoneNumberValue)) {
        setErrorFor(phoneNumber, "Invalid phone number");
    } else {
        setSuccessFor(phoneNumber);
    }
}


function CheckEmail() {
    let emailInput = document.getElementById("emailid");
    let emailValue = emailInput.value.trim();
    let validRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

    if (emailValue === "") {
        setErrorFor(email, "Email is required");
    } else if (!validRegex.test(emailValue)) {
        setErrorFor(email, "Invalid email address!");
    } else if (emailValue.length > 100) {
        setErrorFor(email, "Email address is too long");
    } else {
        setSuccessFor(email);
    }
}



// Shows the error message
function setErrorFor(input, message) {
    const formControl = input.parentElement;
    const small = formControl.querySelector("small");
    small.classList.add('form-error');
    small.textContent = message;
}

// Clears the error message
function setSuccessFor(input) {
    const formControl = input.parentElement;
    const small = formControl.querySelector("small");
    small.classList.remove('form-error');
    small.textContent = ""; // Use textContent instead of innerHTML
}




// Username validation
function CheckUsername() {
    let username = document.getElementById("username");
    if (username.value.trim() === "") {
        setErrorFor(username, "Username is required");
    } else {
        setSuccessFor(username);
    }
}

