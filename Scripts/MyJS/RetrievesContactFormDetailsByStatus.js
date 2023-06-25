// statusBackground.js

document.addEventListener("DOMContentLoaded", function () {
    const statusElements = document.querySelectorAll(".status");
    statusElements.forEach(function (status) {
        const statusValue = status.querySelector(".status-value").textContent.trim();
        if (statusValue === "Replied") {
            status.classList.add("status-Replied");
        } else if (statusValue === "Notreplied") {
            status.classList.add("status-Notreplied");
        }
    });
});


// Function to confirm the deletion of an "about" entry
function DeleteConfirm() {
    // Display an alert to confirm the deletion
    alert('Are you sure you want to delete this about?');
}