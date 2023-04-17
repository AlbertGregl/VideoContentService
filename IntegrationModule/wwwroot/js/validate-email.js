// This file is used to validate the email address of a user
const urlParams = new URLSearchParams(window.location.search);
const username = urlParams.get('username');
const b64SecToken = urlParams.get('b64SecToken');

// Set the username and token in the form
document.querySelector('#username').value = username;
document.querySelector('#b64SecToken').value = b64SecToken;

// Add event listener to the confirm button
const confirmButton = document.querySelector('#confirm-button');
confirmButton.addEventListener('click', () => {
    const username = document.querySelector('#username').value;
    const b64SecToken = document.querySelector('#b64SecToken').value;
    fetch('/api/Users/ValidateEmail', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json' // Indicates the content
        },
        body: JSON.stringify({ username, b64SecToken })
    })
        .then(response => {
            if (response.ok) {
                alert('Email confirmed successfully');
            } else {
                alert('Error confirming email');
            }
        })
        .catch(error => {
            alert('Error confirming email');
        });
});
