function CheckCountryname() {
    var nameField = document.getElementById("countryname");  // Get the input element for country name
    var nameValue = nameField.value.trim();  // Get the trimmed value of the country name input
    var isName = /^[a-zA-Z]+$/;  // Regular expression to validate country name (only alphabets allowed)

    var errorField = document.getElementById("countryname-error");  // Get the error message element for country name

    if (nameValue === "") {  // Check if country name is empty
        nameField.classList.add("is-invalid");  // Add "is-invalid" class to highlight the input as invalid
        nameField.classList.remove("is-valid");  // Remove "is-valid" class if previously added
        errorField.textContent = "Country name is required";  // Display the error message for missing country name
    } else if (!isName.test(nameValue)) {  // Check if country name matches the required pattern
        nameField.classList.add("is-invalid");  // Add "is-invalid" class to highlight the input as invalid
        nameField.classList.remove("is-valid");  // Remove "is-valid" class if previously added
        errorField.textContent = "Country name cannot contain numbers or special characters";  // Display the error message for invalid country name format
    } else {
        nameField.classList.remove("is-invalid");  // Remove "is-invalid" class if previously added
        nameField.classList.add("is-valid");  // Add "is-valid" class to indicate the input is valid
        errorField.textContent = "";  // Clear the error message
    }
}




function CheckFormValidate() {
    CheckCountryname();  // Call the function to validate the country name input
}
