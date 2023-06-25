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
function CheckLastName() {
    let lastName = document.getElementById("lname");
    let lastNameValue = lastName.value.trim();
    let isName = /^[a-zA-Z\s']+$/;

    if (lastNameValue === "") {
        setErrorFor(lastName, "Last name is required");
    } else if (!isName.test(lastNameValue)) {
        setErrorFor(lastName, "Last name can only contain letters");
    } else {
        setSuccessFor(lastName);
    }
}


// Date of birth validation
const dobInput = document.getElementById("dateofbirth");
const today = new Date().toISOString().split("T")[0];
dobInput.setAttribute("max", today);
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

    if (phoneNumberValue === "") {
        setErrorFor(phoneNumber, "Phone number is required");
    } else if (phoneNumberValue.length !== 10) {
        setErrorFor(phoneNumber, "Phone number should be exactly 10 digits long");
    } else if (!/^\d{10}$/.test(phoneNumberValue)) {
        setErrorFor(phoneNumber, "Phone number should contain only digits");
    } else {
        setSuccessFor(phoneNumber);
    }
}


function CheckEmail() {
    let emailval = document.getElementById("email");
    var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (emailval.value.trim() === "") {
        setErrorFor(emailval, "Email is required");
    } else if (!emailPattern.test(emailval.value.trim())) {
        setErrorFor(emailval, "Invalid email address!");
    } else {
        setSuccessFor(emailval);
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

// Password validation
function CheckPassword() {
    let password = document.getElementById("password");
    let passwordValue = password.value.trim();

    if (passwordValue === "") {
        setErrorFor(password, "Password is required");
    } else if (
        passwordValue.length < 8 ||
        !containsUpperCase(passwordValue) ||
        !containsLowerCase(passwordValue) ||
        !containsNumber(passwordValue) ||
        !containsSpecialCharacter(passwordValue)
    ) {
        setErrorFor(password, "Password should be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character");
    } else {
        setSuccessFor(password);
    }
}


function containsUpperCase(password) {
    return /[A-Z]/.test(password);
}

function containsLowerCase(password) {
    return /[a-z]/.test(password);
}

function containsNumber(password) {
    return /[0-9]/.test(password);
}

function containsSpecialCharacter(password) {
    return /[!@#$%^&*]/.test(password);
}

// Confirm password validation
function CheckConfirmPassword() {
    let confirmPassword = document.getElementById("confirmpassword");
    let password = document.getElementById("password");
    if (confirmPassword.value.trim() === "") {
        setErrorFor(confirmPassword, "Confirm password is required");
    } else if (confirmPassword.value !== password.value) {
        setErrorFor(confirmPassword, "Passwords do not match");
    } else {
        setSuccessFor(confirmPassword);
    }
}

function validateForm() {
    CheckFirstName(),
        CheckLastName(),
        CheckDateOfBirth(),
        CheckGender(),
        CheckPhoneNumber(),
        CheckEmail(),
        CheckAddress(),
        CheckUsername(),
        CheckPassword(),
        CheckConformPassword()

}