const makeAdminButtons = document.querySelectorAll('.make-admin-button');
const removeAdminButtons = document.querySelectorAll('.make-user-button');

async function changeRole(btn, requestRoute) {
    const userId = btn.getAttribute('data-id');
        const response = await fetch(`${requestRoute}/${userId}`, {
            method: `POST`
        });
        if (response.ok) {
            alert("Роль пользователя успешно обновлена");
            location.reload();
        } else {
            alert("Произошла ошибка во время смены роли пользователя");
        }
}

makeAdminButtons.forEach((btn) => {
    btn.addEventListener('click', async () => {
        await changeRole(btn, '/role/admin');
    });
})

removeAdminButtons.forEach((btn) => {
    btn.addEventListener('click', async () => {
        await changeRole(btn, '/role/user');
    })
});