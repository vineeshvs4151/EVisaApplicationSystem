// Function to validate the firstname field
function checkFirstName() {
    var nameField = document.getElementById("fname");  // Get the input element for firstname
    var nameValue = nameField.value.trim();  // Get the trimmed value of the firstname input
    var isName = /^[a-zA-Z]+$/;  // Regular expression to validate firstname (only alphabets allowed)

    var errorField = document.getElementById("fname-error");  // Get the error message element for firstname

    if (nameValue === "") {  // Check if firstname is empty
        nameField.classList.add("is-invalid");  // Add "is-invalid" class to highlight the input as invalid
        nameField.classList.remove("is-valid");  // Remove "is-valid" class if previously added
        errorField.textContent = "Firstname is required";  // Display the error message for missing firstname
    } else if (!isName.test(nameValue)) {  // Check if firstname matches the required pattern
        nameField.classList.add("is-invalid");  // Add "is-invalid" class to highlight the input as invalid
        nameField.classList.remove("is-valid");  // Remove "is-valid" class if previously added
        errorField.textContent = "Firstname cannot contain numbers or special characters";  // Display the error message for invalid firstname format
    } else {
        nameField.classList.remove("is-invalid");  // Remove "is-invalid" class if previously added
        nameField.classList.add("is-valid");  // Add "is-valid" class to indicate the input is valid
        errorField.textContent = "";  // Clear the error message
    }
}

// Function to validate the lastname field
function checkLastName() {
    var nameField = document.getElementById("lname");  // Get the input element for lastname
    var nameValue = nameField.value.trim();  // Get the trimmed value of the lastname input
    var isName = /^[a-zA-Z-' ]{1,}$/;  // Regular expression to validate lastname (only alphabets, hyphens, and spaces allowed)

    var errorField = document.getElementById("lname-error");  // Get the error message element for lastname

    if (nameValue === "") {  // Check if lastname is empty
        nameField.classList.add("is-invalid");  // Add "is-invalid" class to highlight the input as invalid
        nameField.classList.remove("is-valid");  // Remove "is-valid" class if previously added
        errorField.textContent = "Lastname is required";  // Display the error message for missing lastname
    } else if (!isName.test(nameValue)) {  // Check if lastname matches the required pattern
        nameField.classList.add("is-invalid");  // Add "is-invalid" class to highlight the input as invalid
        nameField.classList.remove("is-valid");  // Remove "is-valid" class if previously added
        errorField.textContent = "Lastname cannot contain numbers or special characters";  // Display the error message for invalid lastname format
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


// Function to validate the title field
function checkTitle() {
    var titleField = document.getElementById("title");  // Get the input element for title
    var titleValue = titleField.value.trim();  // Get the trimmed value of the title input
    var isName = /^[a-zA-Z-' ]{1,}$/;  // Regular expression to validate title (only alphabets, hyphens, and spaces allowed)

    var errorField = document.getElementById("title-error");  // Get the error message element for title

    if (titleValue === "") {  // Check if title is empty
        titleField.classList.add("is-invalid");  // Add "is-invalid" class to highlight the input as invalid
        titleField.classList.remove("is-valid");  // Remove "is-valid" class if previously added
        errorField.textContent = "Title is required";  // Display the error message for missing title
    } else if (!isName.test(titleValue)) {  // Check if title matches the required pattern
        titleField.classList.add("is-invalid");  // Add "is-invalid" class to highlight the input as invalid
        titleField.classList.remove("is-valid");  // Remove "is-valid" class if previously added
        errorField.textContent = "Title cannot contain numbers or special characters";  // Display the error message for invalid title format
    } else {
        titleField.classList.remove("is-invalid");  // Remove "is-invalid" class if previously added
        titleField.classList.add("is-valid");  // Add "is-valid" class to indicate the input is valid
        errorField.textContent = "";  // Clear the error message
    }
}


// Function to validate the description field
function checkDescription() {
    var messageField = document.getElementById("description");  // Get the textarea element for description
    var messageValue = messageField.value.trim();  // Get the trimmed value of the description textarea

    var errorField = document.getElementById("description-error");  // Get the error message element for description

    if (messageValue === "") {  // Check if description is empty
        messageField.classList.add("is-invalid");  // Add "is-invalid" class to highlight the textarea as invalid
        messageField.classList.remove("is-valid");  // Remove "is-valid" class if previously added
        errorField.textContent = "Description is required";  // Display the error message for missing description
    } else if (messageValue.length < 50) {  // Check if description length is within the specified range
        messageField.classList.add("is-invalid");  // Add "is-invalid" class to highlight the textarea as invalid
        messageField.classList.remove("is-valid");  // Remove "is-valid" class if previously added
        errorField.textContent = "Description should be minimum 50 characters";  // Display the error message for invalid description length
    } else {
        messageField.classList.remove("is-invalid");  // Remove "is-invalid" class if previously added
        messageField.classList.add("is-valid");  // Add "is-valid" class to indicate the textarea is valid
        errorField.textContent = "";  // Clear the error message
    }
}

// Function to validate the image field
function checkImage() {
    var imageField = document.getElementById("image");  // Get the input element for image
    var imageValue = imageField.value;  // Get the value of the image input

    var errorField = document.getElementById("image-error");  // Get the error message element for image

    if (imageValue === "") {  // Check if image is not selected
        imageField.classList.add("is-invalid");  // Add "is-invalid" class to highlight the input as invalid
        imageField.classList.remove("is-valid");  // Remove "is-valid" class if previously added
        errorField.textContent = "Please select an image";  // Display the error message for missing image
    } else {
        imageField.classList.remove("is-invalid");  // Remove "is-invalid" class if previously added
        imageField.classList.add("is-valid");  // Add "is-valid" class to indicate the input is valid
        errorField.textContent = "";  // Clear the error message
    }
}


