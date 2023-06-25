
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
    checkMessage();
}