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

    } else {
        setSuccessFor(password);
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


function validateForm() {
    CheckPassword();
    CheckUsername();
}