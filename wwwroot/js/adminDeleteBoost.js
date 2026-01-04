async function onBoostDelete(id) {
    const modalElement = document.getElementById('confirmDeleteModal');
    
    const modal = bootstrap.Modal.getInstance(modalElement) || new bootstrap.Modal(modalElement);

    modal.show()

    const confirmDeleteBtn = modalElement.querySelector('#confirmDeleteBtn');
    confirmDeleteBtn.addEventListener('click', async () => {
        try {
            await fetch(`/admin/boost/${id}`, {
                method: 'DELETE'
            });
        } catch (error) {
            console.log(error);
        }
        modal.hide();
        alert('Буст успешно удален!');
        location.reload();
        // Доделать, чтобы буст удалялся и визуально + сделать уведомление по красоте
    });
}


const deleteBoostButtons = document.querySelectorAll(`.delete-boost-btn`);

deleteBoostButtons.forEach((btn) => {
    btn.addEventListener('click', async () => {
        await onBoostDelete(parseInt(btn.getAttribute('data-id')));
    });
});