
function Køber() {                     
    let Sushi_A_antal;
    let Sushi_B_antal;
    let Pris;
    let Rabat;

    while (true) { 
       
        console.log("Vil du købe 'Sushi_A' eller 'Sushi_B? ('Exit' Hvis færdig): ");


        let item = prompt("Sushi_A/Sushi_B: ");

     
        if (item.toLowerCase() === 'exit') {
            console.log("Gå videre til betaling!");
            Chart();
        }


        console.log("Hvor mange vil du købe?: ");
        let antal = prompt("Antal: ");


        if (item == "Sushi_A"){
            Sushi_A_antal += antal
        }

        if (item == "Sushi_B"){
            Sushi_B_antal += antal
        }


        console.log("Done! Added to chart");

        
        
    }
}

function Chart(){
    GetPrice()
    console.log("Amount of Sushi A: " +  Sushi_A_antal)
    console.log("Amount of Sushi B: " +  Sushi_A_antal)
    console.log("The discount is: " + rabat)
    console.log("The final price is: " + pris)
    
    console.log("Do you confirm to buy for" + pris + "?: ")
    let confirm = prompt("Yes/No: ");

    if(confirm == "Yes"){
        SaveToDatabase()
        
    }

    if(confirm == "No"){
        return
    }

}


async function SaveToDatabase(){
    fetch('/SendToDatabase', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ sushiTypeAAntal, sushiTypeBAntal, Pris, Rabat })
    });
}




async function GetPrice(){
    const respons = await fetch('/beregnPris', {  //referencere til methoden 'beregnPris' i py filen
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ sushiTypeAAntal, sushiTypeBAntal })
    });
    const data = await response.json(); 

    pris = data.endeligPris.toFixed(2)
    rabat = pris * data.rabat_procent.toFixed(2)
}


Buying();