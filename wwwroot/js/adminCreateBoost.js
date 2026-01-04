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


const createBoostButton = document.querySelector('.create-boost-button');
const createBoostForm = document.querySelector('#createBoostForm');

createBoostButton.addEventListener('click', () => {
    console.log('create boost btn click')
    const modal = new bootstrap.Modal(document.querySelector('#createBoostModal'));
    modal.show();
});


createBoostForm.addEventListener('submit', async function(e) {
    e.preventDefault();
    
    const title = document.getElementById('titleInput').value;
    const price = parseInt(document.getElementById('priceInput').value);
    const profit = parseInt(document.getElementById('profitInput').value);
    const isAuto = document.getElementById('isAutomaticInput').checked;
    const imageFile = document.getElementById('imageFileInput').files[0];
    
    try {
        if (!title || !price || !profit) {
            alert('Заполните название, цену и профит');
            return;
        }
        
        // Если нет изображения - отправляем пустой массив или null
        let imageBase64 = null;
        if (imageFile) {
            imageBase64 = await fileToBase64(imageFile);
        }
        const boostData = {
            title: title,
            price: price,
            profit: profit,
            isAuto: isAuto,
            image: imageBase64
        };
        
        const response = await fetch('/admin/boost', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(boostData)
        });
        
        if (response.ok) {
            alert('Буст создан успешно!');
            const modal = bootstrap.Modal.getInstance(document.querySelector('#createBoostModal'));
            if (modal) modal.hide();
            this.reset();
            location.reload();
        } else {
            const error = await response.text();
            alert('Ошибка создания: ' + error);
        }
    } catch (error) {
        console.error('Ошибка:', error);
        alert('Произошла ошибка при отправке');
    }
});