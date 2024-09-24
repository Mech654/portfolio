async function hentBestillinger() {
    const response = await fetch('/bestillinger');
    const bestillinger = await response.json();

    bestillinger.forEach(bestilling => {
        console.log("Order_number: " + bestilling.id);
        console.log("Antal Sushi A typer: " + bestilling.sushi_type_A_Antal);
        console.log("Antal Sushi B typer: " + bestilling.sushi_type_B_Antal);
        console.log("Rabatbeløb: " + bestilling.RabatBeløb);
        console.log("Endelig pris: " + bestilling.EndeligPris);
    });
}

async function deleteOrder() {
    let orderId = prompt("Enter ID to delete: ");
    if (!orderId) {
        alert('Please enter an order ID.');
        return;
    }

    const response = await fetch(`/bestillinger/${orderId}`, {
        method: 'DELETE'
    });
}

while (true) {
    deleteOrder();
}
