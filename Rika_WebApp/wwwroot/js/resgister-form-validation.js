document.addEventListener("DOMContentLoaded", function () {
    const form = document.querySelector("form");
    const firstName = document.getElementById("firstname");
    const lastName = document.getElementById("lastname");
    const email = document.getElementById("email");
    const password = document.getElementById("password");
    const confirmPassword = document.getElementById("confirm-password");
    const terms = document.getElementById("terms");

    form.addEventListener("submit", function (event) {
        let isValid = true;

        // Reset error messages
        const errorMessages = document.querySelectorAll(".error-message");
        errorMessages.forEach(msg => msg.remove());

        // Helper function to show error
        function showError(input, message) {
            const error = document.createElement("small");
            error.className = "error-message text-danger";
            error.textContent = message;
            input.parentElement.appendChild(error);
            isValid = false;
        }

        // Validate First Name
        if (firstName.value.trim() === "") {
            showError(firstName, "First Name is required.");
        }

        // Validate Last Name
        if (lastName.value.trim() === "") {
            showError(lastName, "Last Name is required.");
        }

        // Validate Email
        const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
        if (!emailRegex.test(email.value.trim())) {
            showError(email, "Please enter a valid email.");
        }

        // Validate Password
        if (password.value.length < 8) {
            showError(password, "Password must be at least 8 characters long.");
        }

        // Validate Confirm Password
        if (password.value !== confirmPassword.value) {
            showError(confirmPassword, "Passwords do not match.");
        }

        // Validate Terms & Conditions
        if (!terms.checked) {
            showError(terms, "You must agree to the terms and conditions.");
        }

        // Prevent form submission if invalid
        if (!isValid) {
            event.preventDefault();
        }
    });
});
