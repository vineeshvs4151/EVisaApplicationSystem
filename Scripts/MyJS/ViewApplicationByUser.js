// Wait for the HTML document to finish loading
document.addEventListener("DOMContentLoaded", function () {
    // Select all elements with class "status"
    const statusElements = document.querySelectorAll(".status");

    // Iterate through each status element
    statusElements.forEach(function (status) {
        // Get the text content of the element with class "status-value"
        const statusValue = status.querySelector(".status-value").textContent.trim().toLowerCase();

        // Add appropriate class based on the value of statusValue
        if (statusValue === "unapproved") {
            // If statusValue is "unapproved", add the class "status-unapproved" to the current status element
            status.classList.add("status-unapproved");
        } else if (statusValue === "approved") {
            // If statusValue is "approved", add the class "status-approved" to the current status element
            status.classList.add("status-approved");
        } else if (statusValue === "waiting") {
            // If statusValue is "waiting", add the class "status-waiting" to the current status element
            status.classList.add("status-waiting");
        }
    });
});
