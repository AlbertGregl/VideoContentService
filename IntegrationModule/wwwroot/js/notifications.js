document.addEventListener("DOMContentLoaded", function () {
    // Retrieve unsent notifications count and update the unsent-count div
    fetch("/api/Notifications/GetUnsentCount")
        .then(response => response.json())
        .then(count => {
            document.querySelector("#unsent-count").textContent = count;
        });

    // Retrieve all notifications and populate the notifications-list ul
    fetch("/api/Notifications/GetAll")
        .then(response => response.json())
        .then(notifications => {
            const notificationsList = document.querySelector("#notifications-list");
            notifications.forEach(notification => {
                const li = document.createElement("li");
                li.innerHTML = `
                <div>${notification.subject}</div>
                <div>${notification.receiverEmail}</div>
                <div>${notification.sentAt}</div>`;
                notificationsList.appendChild(li);
            });
        });

    // Invoke action to send notifications when the send-notifications-btn is clicked
    const sendNotificationsBtn = document.querySelector("#send-notifications-btn");
    sendNotificationsBtn.addEventListener("click", function () {
        fetch("/api/Notifications/SendAllNotifications", {
            method: "POST"
        })
            .then(response => {
                if (response.ok) {
                    alert("Notifications sent successfully");
                    // Retrieve unsent notifications count and update the unsent-count div
                    fetch("/api/Notifications/GetUnsentCount")
                        .then(response => response.json())
                        .then(count => {
                            document.querySelector("#unsent-count").textContent = count;
                        });
                } else {
                    alert("Error sending notifications");
                }
            })
            .catch(error => {
                alert("Error sending notifications");
            });
    });
});
