document.addEventListener("DOMContentLoaded", function () {
    const deleteBtns = document.querySelectorAll(".deleteBtn");
    const confirmationPopup = document.getElementById("confirmationPopup");
    const deleteUserIdInput = document.getElementById("deleteUserId");
    const cancelDelete = document.getElementById("cancelDelete");

    deleteBtns.forEach(btn => {
        btn.addEventListener("click", function () {
            const userId = btn.getAttribute("data-user-id");
            deleteUserIdInput.value = userId;
            confirmationPopup.classList.add("active");
        });
    });

    cancelDelete.addEventListener("click", function () {
        confirmationPopup.classList.remove("active");
    });

    // Close the popup if user clicks outside the popup-content
    window.addEventListener("click", function (event) {
        if (event.target == confirmationPopup) {
            confirmationPopup.classList.remove("active");
        }
    });
});