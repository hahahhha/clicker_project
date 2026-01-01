async function testDelete(id) {
    const toDeleteBoostId = id;

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
        // Доделать, чтобы буст удалялся и визуально + сделать уведомление по красоте
    });
}

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

function updateBoostsTable(boost) {
    const tbody = document.querySelector('.boosts-table tbody');
    const row = tbody.insertRow(-1);

    row.setAttribute('data-boost-id', boost.id);
    row.innerHTML = `
        <td class="text-center fw-bold">${boost.id}</td>
        <td>
            <div class="d-flex align-items-center">
                <i class="fas fa-bolt text-warning me-2"></i>
                <span>${boost.title}</span>
            </div>
        </td>
        <td class="text-center">
            <span class="badge bg-success">${boost.price} золота</span>
        </td>
        <td>
            <div class="d-flex justify-content-center gap-2">
                <button class="btn btn-sm btn-outline-danger" onclick="deleteBoost(${boost.id})">
                    <i class="fas fa-trash me-1"></i> Удалить
                </button>
                <button class="btn btn-sm btn-outline-primary">
                    <i class="fas fa-edit me-1"></i> Изменить
                </button>
            </div>
        </td>
    `;

    // anim
    row.style.opacity = '0';
    row.style.transform = 'translateY(-10px)';
    setTimeout(() => {
        row.style.transition = 'all 0.3s ease';
        row.style.opacity = '1';
        row.style.transform = 'translateY(0)';
    }, 10);
}


const createBoostButton = document.querySelector('.create-boost-button');
const deleteBoostButtons = document.querySelectorAll(`.delete-boost-btn`);

deleteBoostButtons.forEach((btn) => {
    btn.addEventListener('click', async () => {
        await testDelete(parseInt(btn.getAttribute('data-id')))
    });
});

createBoostButton.addEventListener('click', () => {
    const modal = new bootstrap.Modal(document.querySelector('#createBoostModal'));
    modal.show();
});

document.querySelector('#createBoostForm').addEventListener('submit', async function(e) {
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
            image: imageBase64  // base64 строка или null
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
            updateBoostsTable(boostData);
        } else {
            const error = await response.text();
            alert('Ошибка создания: ' + error);
        }
    } catch (error) {
        console.error('Ошибка:', error);
        alert('Произошла ошибка при отправке');
    }
});