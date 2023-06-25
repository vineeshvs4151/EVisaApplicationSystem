// Function to prevent navigating back to the previous page
function preback() {
    window.history.forward();
}

// Set a timeout to execute the preback function after 0 milliseconds
setTimeout("preback()", 0);

// Clear the onunload event handler to prevent page caching
window.onunload = function () {
    null;
};




















// Get the element with id "navLinks"
var navLinksValue = document.getElementById("navLinks");

// Function to show the menu by setting the right CSS property to "0"
function showMenu() {
    navLinksValue.style.right = "0";
}

// Function to hide the menu by setting the right CSS property to "-200px"
function hideMenu() {
    navLinksValue.style.right = "-200px";
}
