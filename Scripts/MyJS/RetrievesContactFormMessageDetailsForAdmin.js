// statusBackground.js

// Wait for the HTML document to finish loading
document.addEventListener("DOMContentLoaded", function () {
    // Select all elements with class "status"
    const statusElements = document.querySelectorAll(".status");

    // Iterate through each status element
    statusElements.forEach(function (status) {
        // Get the text content of the element with class "status-value"
        const statusValue = status.querySelector(".status-value").textContent.trim();

        // Add appropriate class based on the value of statusValue
        if (statusValue === "Replied") {
            // If statusValue is "Replied", add the class "status-Replied" to the current status element
            status.classList.add("status-Replied");
        } else if (statusValue === "Notreplied") {
            // If statusValue is "Notreplied", add the class "status-Notreplied" to the current status element
            status.classList.add("status-Notreplied");
        }
    });
});


// Function to confirm the deletion of an "about" entry
function DeleteConfirm() {
    // Display an alert to confirm the deletion
    alert('Are you sure you want to delete this about?');
}