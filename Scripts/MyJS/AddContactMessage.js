// Function to validate the name field
function checkName() {
    var nameField = document.getElementById("name");  // Get the input element for name
    var nameValue = nameField.value.trim();  // Get the trimmed value of the name input
    var isName = /^[a-zA-Z]+$/;  // Regular expression to validate name (only alphabets allowed)

    var errorField = document.getElementById("name-error");  // Get the error message element for name

    if (nameValue === "") {  // Check if name is empty
        nameField.classList.add("is-invalid");  // Add "is-invalid" class to highlight the input as invalid
        nameField.classList.remove("is-valid");  // Remove "is-valid" class if previously added
        errorField.textContent = "Name is required";  // Display the error message for missing name
    } else if (!isName.test(nameValue)) {  // Check if name matches the required pattern
        nameField.classList.add("is-invalid");  // Add "is-invalid" class to highlight the input as invalid
        nameField.classList.remove("is-valid");  // Remove "is-valid" class if previously added
        errorField.textContent = "Name cannot contain numbers or special characters";  // Display the error message for invalid name format
    } else {
        nameField.classList.remove("is-invalid");  // Remove "is-invalid" class if previously added
        nameField.classList.add("is-valid");  // Add "is-valid" class to indicate the input is valid
        errorField.textContent = "";  // Clear the error message
    }
}



// Function to validate the email
function checkEmail() {
    var emailField = document.getElementById("email"); // Get the email input field element
    var emailValue = emailField.value.trim(); // Get the trimmed value of the email input
    var emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/; // Regular expression pattern for email validation

    var errorField = document.getElementById("email-error"); // Get the error message field element

    if (emailValue === "") { // Check if email value is empty
        emailField.classList.add("is-invalid"); // Add 'is-invalid' class to the email input field
        emailField.classList.remove("is-valid"); // Remove 'is-valid' class from the email input field
        errorField.textContent = "Email is required"; // Set the error message
    } else if (!emailPattern.test(emailValue)) { // Check if email value does not match the email pattern
        emailField.classList.add("is-invalid"); // Add 'is-invalid' class to the email input field
        emailField.classList.remove("is-valid"); // Remove 'is-valid' class from the email input field
        errorField.textContent = "Invalid email format"; // Set the error message
    } else {
        emailField.classList.remove("is-invalid"); // Remove 'is-invalid' class from the email input field
        emailField.classList.add("is-valid"); // Add 'is-valid' class to the email input field
        errorField.textContent = ""; // Clear the error message
    }
}




// Function to validate the message field
function checkMessage() {
    var messageField = document.getElementById("message");  // Get the textarea element for message
    var messageValue = messageField.value.trim();  // Get the trimmed value of the message textarea

    var errorField = document.getElementById("message-error");  // Get the error message element for message
    if (messageValue === "") {  // Check if message is empty
        messageField.classList.add("is-invalid");  // Add "is-invalid" class to highlight the textarea as invalid
        messageField.classList.remove("is-valid");  // Remove "is-valid" class if previously added
        errorField.textContent = "Message is required";  // Display the error message for missing message
    } else if (messageValue.length < 10) {  // Check if message length is within the specified range
        messageField.classList.add("is-invalid");  // Add "is-invalid" class to highlight the textarea as invalid
        messageField.classList.remove("is-valid");  // Remove "is-valid" class if previously added
        errorField.textContent = "Message should be minimum 10 characters";  // Display the error message for invalid message length
    } else {
        messageField.classList.remove("is-invalid");  // Remove "is-invalid" class if previously added
        messageField.classList.add("is-valid");  // Add "is-valid" class to indicate the textarea is valid
        errorField.textContent = "";  // Clear the error message
    }
}



function checkValidation() {
    checkName();
    checkEmail();
    checkMessage();
}