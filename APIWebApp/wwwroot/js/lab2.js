const uri = 'api/Authors';
let authors = [];

function getAuthors() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayAuthors(data))
        .catch(error => console.error('Не вдалося отримати автори.', error));
}

function addAuthor() {
    var addFullNameTextbox = document.getElementById('add-fullName');
    var addCountryTextbox = document.getElementById('add-country');
    var addBirthYearTextbox = document.getElementById('add-birthYear');

    var errorMessage = '';

    if (!addFullNameTextbox.value.trim()) {
        errorMessage = 'Поле "Повне ім\'я" обов\'язкове для заповнення.';
    }

    if (!addCountryTextbox.value.trim()) {
        errorMessage += '\nПоле "Країна" обов\'язкове для заповнення.';
    }

    var birthYear = parseInt(addBirthYearTextbox.value);
    if (!addBirthYearTextbox.value.trim()) {
        errorMessage += '\nПоле "Рік народження" обов\'язкове для заповнення.';
    }
    if (birthYear > 2024) {
        errorMessage += '\nРік народження не може бути більше 2024.';
    }

    if (birthYear < 0) {
        errorMessage += '\nРік народження не може від\'ємним числом.';
    }

    if (errorMessage !== '') {
        alert(errorMessage);
        return false;
    }

    const author = {
        fullName: addFullNameTextbox.value.trim(),
        country: addCountryTextbox.value.trim(),
        birthYear: addBirthYearTextbox.value.trim()
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(author)
    })
        .then(response => response.json())
        .then(() => {
            getAuthors();
            addFullNameTextbox.value = '';
            addCountryTextbox.value = '';
            addBirthYearTextbox.value = '';
            addDeathYearTextbox.value = '';
        })
        .catch(error => console.error('Не вдалося додати автора.', error));
}

function deleteAuthor(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getAuthors())
        .catch(error => console.error('Не вдалося видалити автора.', error));
}

function displayEditForm(id) {
    document.getElementById('edit-fullName').disabled = false;
    document.getElementById('edit-country').disabled = false;
    document.getElementById('edit-birthYear').disabled = false;
    const author = authors.find(author => author.id === id);
    document.getElementById('edit-id').value = author.id;
    document.getElementById('edit-fullName').value = author.fullName;
    document.getElementById('edit-country').value = author.country;
    document.getElementById('edit-birthYear').value = author.birthYear;
    document.getElementById('editForm').style.display = 'block';
}

function updateAuthor() {
    const authorId = document.getElementById('edit-id').value;
    const fullName = document.getElementById('edit-fullName').value.trim();
    const country = document.getElementById('edit-country').value.trim();
    const birthYear = document.getElementById('edit-birthYear').value.trim();

    var errorMessage = '';

    if (!fullName) {
        errorMessage = 'Поле "Повне ім\'я" обов\'язкове для заповнення.';
    }

    if (!country) {
        errorMessage += '\nПоле "Країна" обов\'язкове для заповнення.';
    }

    if (!birthYear) {
        errorMessage += '\nПоле "Рік народження" обов\'язкове для заповнення.';
    }

    const parsedBirthYear = parseInt(birthYear);
    if (parsedBirthYear > 2024) {
        errorMessage += '\nРік народження не може бути більше 2024.';
    }

    if (parsedBirthYear < 0) {
        errorMessage += '\nРік народження не може бути від\'ємним числом.';
    }

    if (errorMessage !== '') {
        alert(errorMessage);
        return false;
    }

    const author = {
        id: parseInt(authorId, 10),
        fullName: fullName,
        country: country,
        birthYear: birthYear
    };

    fetch(`${uri}/${authorId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(author)
    })
        .then(() => getAuthors())
        .catch(error => console.error('Не вдалося оновити автора.', error));

    return false;
}

function _displayAuthors(data) {
    const tBody = document.getElementById('authors');
    tBody.innerHTML = '';


    const button = document.createElement('button');

    data.forEach(author => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Редагувати';
        editButton.setAttribute('onclick', `displayEditForm(${author.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Видалити';
        deleteButton.setAttribute('onclick', `deleteAuthor(${author.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let textNodeFullName = document.createTextNode(author.fullName);
        td1.appendChild(textNodeFullName);

        let td2 = tr.insertCell(1);
        let textNodeCountry = document.createTextNode(author.country);
        td2.appendChild(textNodeCountry);

        let td3 = tr.insertCell(2);
        let textNodeBirthYear = document.createTextNode(author.birthYear);
        td3.appendChild(textNodeBirthYear);

        let td5 = tr.insertCell(3);
        td5.appendChild(editButton);

        let td6 = tr.insertCell(4);
        td6.appendChild(deleteButton);

    });

    authors = data;
}
