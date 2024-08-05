
const observer = new IntersectionObserver((entries) => {
    entries.forEach((entry) => {
        console.log(entry)
        if (entry.isIntersecting) {
            entry.target.classList.add('show');
        } else {
            entry.target.classList.remove('show');
        }
    });
});

const hiddenElements = document.querySelectorAll('.hidden');
hiddenElements.forEach((el) => observer.observe(el));


const observerG = new IntersectionObserver((entries) => {
    entries.forEach((entry) => {
        console.log(entry)
        if (entry.isIntersecting) {
            entry.target.classList.add('showG');
        } else {
            entry.target.classList.remove('showG');
        }
    });
});

const hiddenElementsg = document.querySelectorAll('.hiddenG');
hiddenElementsg.forEach((el) => observerG.observe(el));

document.addEventListener("DOMContentLoaded", function () {
    var title = document.querySelector('h1').textContent;

    if (title === "Cryptocurrency") {
        document.body.classList.add('custom-background');
    }
});





$(document).ready(function () {
    var table = $('#userRoleTable').DataTable({
        responsive: true,
        "columnDefs": [
            { "orderable": false, "targets": 2 },

        ],
         "columns": [
            { "width": "20vw" }, 
            { "width": "20vw" }, 
            { "width": "20vw" }
             
        ]
    });
    $('td').each(function () {
        var cell = $(this);
        var text = cell.text().trim();
        if (text.startsWith('Customer')) {
            cell.addClass('positive');
        } else if (text.startsWith('Admin')) {
            cell.addClass('negative');
        }
    });
});
$(document).ready(function () {
    var table = $('#myTable').DataTable({

    });

    function fetchData() {
        $.ajax({
            url: '/Customer/Home/GetCryptoData', // Ensure this path is correct
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                table.clear();
                data.forEach(item => {
                    table.row.add([
                        item.symbol,
                        item.name,
                        item.price,
                        item.change,
                        item.changeInProcentige,
                        item.marketCap,
                        item.volumeInCurrency,
                        item.volumeOutCurrency24Hr,
                        item.totalVolumeAllCurrencies24Hr,
                        item.circulatingSupply
                    ]);
                });
                table.draw(); // Redraw the table with the new data
                $('td').each(function () {
                    var cell = $(this);
                    var text = cell.text().trim();
                    if (text.startsWith('+')) {
                        cell.addClass('positive');
                    } else if (text.startsWith('-')) {
                        cell.addClass('negative');
                    }
                });
            },
            error: function (xhr, status, error) {
                console.error('Error fetching data:', error);
                console.log('XHR:', xhr);
                console.log('Status:', status);
                console.log('Error:', error);
            }
        });
    }

    fetchData(); // Initial fetch
    setInterval(fetchData, 3000); 
});

document.addEventListener("DOMContentLoaded", function () {
    var cells = document.querySelectorAll('td');

    cells.forEach(function (cell) {
        var text = cell.textContent.trim();
        console.log("Cell content:", text); // Debugging output

        if (text.startsWith('+'|| 'Admin')) {
            cell.classList.add('positive');
            console.log("Added class 'positive'"); // Debugging output
        } else if (text.startsWith('-' || 'Customer')) {
            cell.classList.add('negative');
            console.log("Added class 'negative'"); // Debugging output
        }
    });
});


// Function to update the margin-left of .ContentManageAcc
function updateMargin() {
    // Select the element(s) with the class .ContentManageAcc
    var elements = document.querySelectorAll('.ContentManageAcc');

    // Iterate over selected elements and update the margin
    elements.forEach(function (element) {
        element.style.marginLeft = '0';
    });
}

// Check if the specific page is loaded, e.g., using URL
if (window.location.pathname.includes('/TwoFactorAuthentication')) {
    updateMargin();
} 
if (window.location.pathname.includes('/EnableAuthenticator')) {
    updateMargin();
} 
if (window.location.pathname.includes('/GenerateRecoveryCodes')) {
    updateMargin();
} 
if (window.location.pathname.includes('/ShowRecoveryCodes')) {
    updateMargin();
}
if (window.location.pathname.includes('/Disable2fa')) {
    updateMargin();
}
if (window.location.pathname.includes('/ResetAuthenticator')) {
    updateMargin();
}

document.addEventListener("DOMContentLoaded", function () {
    var title = document.querySelector('h1').textContent;

    if (title === "Stocks") {
        document.body.classList.add('custom-backgroundStocks');
    }
});


$(document).ready(function () {
    $('.dropdown').hover(
        function () {
            $('.hero-sectionAbout h1').hide();
        }, function () {
            $('.hero-sectionAbout h1').show();
        }
    );
});


document.addEventListener('DOMContentLoaded', function () {
    const toggler = document.getElementById('navbar-toggler');
    const body = document.querySelector('body');

    toggler.addEventListener('click', function () {
        if (body.classList.contains('menu-open')) {
            body.classList.remove('menu-open');
        } else {
            body.classList.add('menu-open');
        }
    });
});

$(document).ready(function () {
    $('.navEl').on('click', function () {
        var target = $(this).data('target');
        $('.ContentManageAcc').load(target);
    });
});

document.addEventListener('DOMContentLoaded', function () {
    // Scroll to the ContentManageAcc div
    var targetElement = document.getElementById('content-manage-acc');
    if (targetElement) {
        targetElement.scrollIntoView({
            behavior: 'smooth'
        });
    }
});