function updateTotalPrice() {
    const unitPrice = parseFloat(document.getElementById('unitPrice').textContent);
    const quantity = parseInt(document.getElementById('quantity').value, 10);
    const totalPrice = unitPrice * quantity;

    document.getElementById('totalPrice').textContent = totalPrice.toFixed(2);
}

function addToCart() {
    const formData = {
        ArticleNumber: document.querySelector('input[name="ArticleNumber"]').value,
        ProductName: document.querySelector('input[name="ProductName"]').value,
        Price: parseFloat(document.querySelector('input[name="Price"]').value),
        Quantity: parseInt(document.getElementById('quantity').value, 10)
    };

    fetch('/cart/addtocart', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(formData)
    })
        .then(response => {
            if (!response.ok) {
                alert("Something went wrong, please try again.");
            }
            return response.json();
        })
        .then(data => {
            updateCartCountViewComponent();
        })
        .catch(error => {
            console.error('Error adding product to cart:', error);
            alert("Error adding product to cart.");
        });
}


function updateCartCountViewComponent() {
    fetch('/cart/updatecartcount', {
        method: 'GET',
        headers: {
            'Content-Type': 'text/html'
        }
    })
        .then(response => {
            if (!response.ok) {
                alert("Something went wrong, please try again.");
            }
            return response.text();
        })
        .then(data => {
            document.getElementById('cartCountContainer').innerHTML = data;
        })
        .catch(error => {
            console.error('Error updating cart count view component:', error);
        });
}