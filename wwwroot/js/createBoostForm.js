export function createForm(title='', price='', profit='', image='', isAuto=false){
    `<form id="createBoostForm">
        <div class="mb-3">
            <label for="titleInput" class="form-label">Название</label>
            <input name="Title" type="text" class="form-control" id="titleInput" placeholder="Придумайте название"
            value="${title}">
        </div>
        <div class="mb-3">
            <label for="priceInput" class="form-label">Цена</label>
            <input name="Price" type="number" class="form-control" id="priceInput" placeholder="Установите цену"
            value="${price}">
        </div>
        <div class="mb-3">
            <label for="profitInput" class="form-label">Профит</label>
            <input name="Profit" type="number" class="form-control" id="profitInput" placeholder="Установите профит буста"
            value="${profit}">
        </div>
        <div class="mb-3 mt-3">
            <label for="formFile" class="form-label">Изображение</label>
            <input name="Image" class="form-control" type="file" id="imageFileInput"
            value="${image}">
        </div>
        <div class="form-check mb-3">
            <input name="IsAuto" class="form-check-input" type="checkbox" value="" id="isAutomaticInput"
            ${isAuto ? 'checked' : ''}>
            <label class="form-check-label" for="isAutomaticInput">
                Автоматический буст
            </label>
        </div>
        
        <div class="modal-footer">
            <button type="submit" class="btn btn-primary">Создать</button>
            <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Отмена</button>
        </div>
    </form>`
} 