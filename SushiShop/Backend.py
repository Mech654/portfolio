import datetime

def rabatBeregning():  #Methode for at beregne rabat
    rabat_Procent = 0
    if Antal_Sushi >= 20:
        rabat_Procent += 0.2
    elif Antal_Sushi >= 10:
        rabat_Procent += 0.1


    bestillingstidspunkt = datetime.now()
    if 11 <= bestillingstidspunkt.hour <= 14:
        rabat_Procent += 0.20

    rabat = Pris * rabat_Procent
    endelig_pris = Pris - rabat


def BeregnPris(sushi_A_antal, sushi_B_antal):  #Methode for at beregne pris
    Sushi_A_pris = 3
    Sushi_B_pris = 4

    Antal_Sushi = sushi_A_antal + sushi_B_antal
    Pris = (sushi_A_antal * Sushi_A_pris) + (sushi_B_antal*Sushi_B_pris)

    rabatBeregning()

    return{
        "endelig_pris": endelig_pris,
        "rabat_Procent": rabat_Procent,
    }


def SendToDatabase(Sushi_A, Sushi_B, Rabat, Pris):          #Methode til at sende data til databasen
    sql_format = f"""
    INSERT INTO bestillinger (sushi_type_A_Antal, sushi_type_B_Antal, RabatBeløb, EndeligPris)
    VALUES ({Sushi_A}, {Sushi_B}, {Rabat}, {Pris});
    """
    database.execute(sql_format)
