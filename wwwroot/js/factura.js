$(document).ready(function () {
    loadCustomers();
    loadProducts();
        function loadCustomers() {
        $.ajax({
            type: 'GET',
            url: '/Clientes', 
            success: function (customers) {
                customers.forEach(function (customer) {
                    $('#customerSelect').append(new Option(customer.Nombre, customer.ClienteId));
                });
            },
            error: function (error) {
                alert('Error loading customers: ' + error.responseText);
            }
        });
    }

    function loadProducts() {
        $.ajax({
            type: 'GET',
            url: '/Productos', 
            success: function (products) {
                products.forEach(function (product) {
                    $('#productSelect').append(new Option(product.Nombre, product.ProductoId));
                });
            },
            error: function (error) {
                alert('Error loading products: ' + error.responseText);
            }
        });
    }

 
    $('#addProduct').on('click', function () {
        const productId = $('#productSelect').val();
        const quantity = parseInt($('#productQuantity').val());

        if (!productId || quantity <= 0) {
            alert('Please select a product and enter a valid quantity.');
            return;
        }

       
        $.ajax({
            type: 'GET',
            url: '/Productos/' + productId, y
            success: function (product) {
                if (product.Stock < quantity) {
                    alert('Not enough stock available.');
                    return;
                }

                const subtotal = product.Precio * quantity;
                const iva = subtotal * 0.15; 
                const total = subtotal + iva;


                $('#invoiceTable tbody').append(`
                    <tr>
                        <td>${product.Codigo}</td>
                        <td>${product.Nombre}</td>
                        <td>${quantity}</td>
                        <td>${product.Precio.toFixed(2)}</td>
                        <td>${subtotal.toFixed(2)}</td>
                        <td>${iva.toFixed(2)}</td>
                        <td>${total.toFixed(2)}</td>
                        <td><button class="btn btn-danger remove-product">Remove</button></td>
                    </tr>
                `);

              
                updateInvoiceSummary();
            },
            error: function (error) {
                alert('Error getting product details: ' + error.responseText);
            }
        });
    });


    function updateInvoiceSummary() {
        let subtotal = 0;
        let iva = 0;
        let total = 0;

        $('#invoiceTable tbody tr').each(function () {
            const rowSubtotal = parseFloat($(this).find('td:nth-child(5)').text());
            const rowIVA = parseFloat($(this).find('td:nth-child(6)').text());
            const rowTotal = parseFloat($(this).find('td:nth-child(7)').text());

            subtotal += rowSubtotal;
            iva += rowIVA;
            total += rowTotal;
        });

        $('#subtotal').text(subtotal.toFixed(2));
        $('#iva').text(iva.toFixed(2));
        $('#total').text(total.toFixed(2));
    }


    $('#invoiceTable').on('click', '.remove-product', function () {
        $(this).closest('tr').remove();
        updateInvoiceSummary();
    });


    $('#generateInvoice').on('click', function () {
        const invoiceDetails = {
            ClienteId: $('#customerSelect').val(),
            Fecha: $('#issueDate').text(),
            DetalleFacturas: []
        };

        $('#invoiceTable tbody tr').each(function () {
            const productId = $(this).find('td:nth-child(1)').text();
            const quantity = parseInt($(this).find('td:nth-child(3)').text());
            const price = parseFloat($(this).find('td:nth-child(5)').text());

            invoiceDetails.DetalleFacturas.push({
                ProductoId: productId,
                Cantidad: quantity,
                PrecioUnitario: price
            });
        });

      
        $.ajax({
            type: 'POST',
            url: '/Facturas/Create', 
            data: JSON.stringify(invoiceDetails),
            contentType: 'application/json',
            success: function (response) {
                alert('Invoice generated successfully.');
               clearForm();
            },
            error: function (error) {
                alert('Error generating invoice: ' + error.responseText);
            }
        });
    });


    function clearForm() {
        $('#customerSelect').val('');
        $('#productSelect').val('');
        $('#productQuantity').val('');
        $('#invoiceTable tbody').empty();
        updateInvoiceSummary();
    }

 
    $('#printInvoice').on('click', function () {
        window.print(); 
    });
});