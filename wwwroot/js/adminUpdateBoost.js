function fileToBase64(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.readAsDataURL(file);
        reader.onload = () => {
            // Убираем префикс data:image/...;base64,
            const base64 = reader.result.split(',')[1];
            resolve(base64);
        };
        reader.onerror = error => reject(error);
    });
}

let originalImage = '';
let currentImage = '';

let currentBoostId = -1;

async function onBoostChange(id) {
    const modalElement = document.querySelector('#editBoostModal');
    const modal = new bootstrap.Modal(modalElement);

    const response = await fetch(`/admin/boost/${id}`, {
        method: "GET",
        headers: { 'Content-Type': 'application/json' }
    });
    const editForm = document.querySelector('#edit-boost-form');
    if (response.ok) {
        const boostData = await response.json();

        originalImage = boostData.image;
        currentImage = originalImage;
        currentBoostId = boostData.id;

        editForm.querySelector('#titleInput').value = boostData.title;
        editForm.querySelector('#priceInput').value = boostData.price;
        editForm.querySelector('#profitInput').value = boostData.profit;
        editForm.querySelector('#isAutomaticInput').checked = boostData.isAuto;
    }
    modal.show();
}


const editBoostButtons = document.querySelectorAll(`.edit-boost-btn`);
editBoostButtons.forEach((btn) => {
    btn.addEventListener('click', async () => {
        await onBoostChange(parseInt(btn.getAttribute('data-id')))
    });
})


const labelChangeImage = document.querySelector('#change-image-label');
const changeImageButton = document.querySelector('#change-boost-img-btn');
const cancelChangeImageButton = document.querySelector('#cancel-change-boost-img-btn');
const boostChangeImageInput = document.querySelector('#boost-change-image-file');

const cancelBoostChange = document.querySelector('#cancel-boost-change');

const editBoostForm = document.querySelector('#edit-boost-form');

let isImageChanged = false;

editBoostForm.addEventListener('submit', async (evt) => {
    evt.preventDefault();
    const boostData = {
        title: editBoostForm.querySelector('#titleInput').value,
        price: parseInt(editBoostForm.querySelector('#priceInput').value),
        profit: parseInt(editBoostForm.querySelector('#profitInput').value),
        isAuto: editBoostForm.querySelector('#isAutomaticInput').checked,
        image: currentImage
    };
    
    const response = await fetch(`/admin/boost/${currentBoostId}`, {
        method: "PUT",
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(boostData)
    })

    if (response.ok) {
        alert('Буст успешно обновлен');
        location.reload();
    } else {
        alert('Не удалось обновить буст');
    }
});

changeImageButton.addEventListener('click', () => {
    boostChangeImageInput.classList.remove('d-none');
    changeImageButton.classList.add('d-none');
    cancelChangeImageButton.classList.remove('d-none');
}); 

boostChangeImageInput.addEventListener('change', async () => {
    isImageChanged = true;
    const imageFile = boostChangeImageInput.files[0];
    currentImage = await fileToBase64(imageFile);
    // console.log(currentImage);
});

cancelChangeImageButton.addEventListener('click', () => {
    boostChangeImageInput.classList.add('d-none');
    changeImageButton.classList.remove('d-none');
    cancelChangeImageButton.classList.add('d-none');
    isImageChanged = false;
    currentImage = originalImage;
});

cancelBoostChange.addEventListener('click', () => {
    if (!boostChangeImageInput.classList.contains('d-none')) {
        boostChangeImageInput.classList.add('d-none');
    }
    if (changeImageButton.classList.contains('d-none')) {
        changeImageButton.classList.remove('d-none');
    }
    if (!cancelChangeImageButton.classList.contains('d-none')) {
        cancelChangeImageButton.classList.add('d-none');
    }
});