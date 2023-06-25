// Execute the function when the DOM content has finished loading
document.addEventListener("DOMContentLoaded", function () {
    // Get all elements with the class "status"
    const statusElements = document.querySelectorAll(".status");

    // Iterate over each status element
    statusElements.forEach(function (status) {
        // Get the text content of the element with class "status-value" and remove any leading or trailing white spaces
        const statusValue = status.querySelector(".status-value").textContent.trim().toLowerCase();

        // Add appropriate CSS class based on the status value
        if (statusValue === "unapproved") {
            // If the status is "unapproved", add the CSS class "status-unapproved" to the parent status element
            status.classList.add("status-unapproved");
        } else if (statusValue === "approved") {
            // If the status is "approved", add the CSS class "status-approved" to the parent status element
            status.classList.add("status-approved");
        } else if (statusValue === "waiting") {
            // If the status is "waiting", add the CSS class "status-waiting" to the parent status element
            status.classList.add("status-waiting");
        }
    });
});
