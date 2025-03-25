

$(document).ready(function () {

    $('#createProductForm').on('submit', function (e) {
        e.preventDefault();
        const productData = {
            name: $('#productName').val(),
            price: $('#productPrice').val(),
            stock: $('#productStock').val()
        };

        $.ajax({
            type: 'POST',
            url: '/Products/Create',
            data: productData,
            success: function (response) {
                alert('Producto creado exitosamente!');
            
            },
            error: function (error) {
                alert('Error al crear el producto: ' + error.responseText);
            }
        });
    });


    function loadProducts() {
        $.ajax({
            type: 'GET',
            url: '/Products',
            success: function (products) {
              
                console.log(products);
            },
            error: function (error) {
                alert('Error al cargar los productos: ' + error.responseText);
            }
        });
    }

    loadProducts();
});