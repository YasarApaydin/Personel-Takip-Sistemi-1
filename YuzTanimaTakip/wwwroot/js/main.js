let editingRow = null;

function openModal() {
    document.getElementById('modalTitle').innerText = 'Yeni Personel Ekle';
    document.getElementById('newFirstName').value = '';
    document.getElementById('newLastName').value = '';
    document.getElementById('newEmail').value = '';
    document.getElementById('newDepartment').value = '';
    editingRow = null;
    document.getElementById('personnelModal').style.display = 'flex';
}

function closeModal() {
    if (editingRow && (
        document.getElementById('newFirstName').value !== editingRow.cells[1].innerText ||
        document.getElementById('newLastName').value !== editingRow.cells[2].innerText ||
        document.getElementById('newEmail').value !== editingRow.cells[3].innerText ||
        document.getElementById('newDepartment').value !== editingRow.cells[4].innerText)) {
        if (!confirm("Kaydedilmemiş değişiklikleriniz var. Kaydetmeden çıkmak istediğinizden emin misiniz?")) {
            return;
        }
    }
    document.getElementById('personnelModal').style.display = 'none';
}

function savePersonnel() {
    let firstName = document.getElementById('newFirstName').value;
    let lastName = document.getElementById('newLastName').value;
    let email = document.getElementById('newEmail').value;
    let department = document.getElementById('newDepartment').value;

    if (!firstName || !lastName || !email || !department) {
        alert("Tüm alanların doldurulması zorunludur!");
        return;
    }

    if (editingRow) {
        if (confirm("Değişiklikleri kaydetmek istiyor musunuz?")) {
            editingRow.cells[1].innerText = firstName;
            editingRow.cells[2].innerText = lastName;
            editingRow.cells[3].innerText = email;
            editingRow.cells[4].innerText = department;
        }
    } else {
        let table = document.getElementById('personnelTable');
        let newId = generateNewId();

        let row = table.insertRow();
        row.insertCell(0).innerText = newId;
        row.insertCell(1).innerText = firstName;
        row.insertCell(2).innerText = lastName;
        row.insertCell(3).innerText = email;
        row.insertCell(4).innerText = department;

        let actionsCell = row.insertCell(5);
        actionsCell.className = 'action-buttons';
        actionsCell.innerHTML = `
            <button class="edit" onclick="editPersonnel(this)">
                <i class="fas fa-edit"></i>
            </button>
            <button class="delete" onclick="confirmDelete(this)">
                <i class="fas fa-trash"></i>
            </button>
        `;
    }
    closeModal();
}

function generateNewId() {
    let table = document.getElementById('personnelTable');
    let rows = table.getElementsByTagName('tr');
    let maxId = 0;

    for (let row of rows) {
        let cells = row.getElementsByTagName('td');
        if (cells.length > 0) {
            let id = parseInt(cells[0].innerText);
            if (!isNaN(id) && id > maxId) {
                maxId = id;
            }
        }
    }
    return maxId + 1;
}

function editPersonnel(btn) {
    editingRow = btn.closest('tr');
    document.getElementById('modalTitle').innerText = 'Personeli Düzenle';
    document.getElementById('newFirstName').value = editingRow.cells[1].innerText;
    document.getElementById('newLastName').value = editingRow.cells[2].innerText;
    document.getElementById('newEmail').value = editingRow.cells[3].innerText;
    document.getElementById('newDepartment').value = editingRow.cells[4].innerText;
    document.getElementById('personnelModal').style.display = 'flex';
}

function confirmDelete(btn) {
    if (confirm("Bu kaydı silmek istediğinizden emin misiniz?")) {
        let row = btn.closest('tr');
        row.remove();
    }
}

function applyFilter() {
    let filterId = document.getElementById('filterId').value.toLowerCase();
    let filterFirstName = document.getElementById('filterFirstName').value.toLowerCase();
    let filterLastName = document.getElementById('filterLastName').value.toLowerCase();
    let filterEmail = document.getElementById('filterEmail').value.toLowerCase();
    let filterDepartment = document.getElementById('filterDepartment').value.toLowerCase();

    let rows = document.getElementById('personnelTable').getElementsByTagName('tr');

    for (let row of rows) {
        let cells = row.getElementsByTagName('td');
        if (cells.length > 0) {
            let id = cells[0].innerText.toLowerCase();
            let firstName = cells[1].innerText.toLowerCase();
            let lastName = cells[2].innerText.toLowerCase();
            let email = cells[3].innerText.toLowerCase();
            let department = cells[4].innerText.toLowerCase();

            if (id.includes(filterId) &&
                firstName.includes(filterFirstName) &&
                lastName.includes(filterLastName) &&
                email.includes(filterEmail) &&
                department.includes(filterDepartment)) {
                row.style.display = '';
            } else {
                row.style.display = 'none';
            }
        }
    }
}

// Modal dışına tıklandığında kapatma
window.onclick = function (event) {
    let modal = document.getElementById('personnelModal');
    if (event.target === modal) {
        closeModal();
    }
}