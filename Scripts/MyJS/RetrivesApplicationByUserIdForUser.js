// Wait for the DOM to finish loading before executing the code
document.addEventListener("DOMContentLoaded", function () {
    // Retrieve all elements with the class "status" and store them in the statusElements variable
    const statusElements = document.querySelectorAll(".status");

    // Iterate over each status element
    statusElements.forEach(function (status) {
        // Retrieve the text content of the child element with the class "status-value" and remove leading/trailing whitespace
        const statusValue = status.querySelector(".status-value").textContent.trim().toLowerCase();

        // Check the value of statusValue and add corresponding CSS classes to the status element
        if (statusValue === "unapproved") {
            status.classList.add("status-unapproved"); // Add "status-unapproved" class
        } else if (statusValue === "approved") {
            status.classList.add("status-approved"); // Add "status-approved" class
        } else if (statusValue === "waiting") {
            status.classList.add("status-waiting"); // Add "status-waiting" class
        }
    });
});
