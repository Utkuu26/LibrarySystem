[System.Serializable]
public class Kitap
{
    public string Baslik { get; set; }
    public string Yazar { get; set; }
    public string ISBN { get; set; }
    public int KopyaSayisi { get; set; }
    public int OduncAlinanKopyalar { get; set; }

    //Kitap kitap1 = new Kitap { Baslik = "Vahsetin Cagrisi", Yazar = "Jack London", ISBN = "111", KopyaSayisi = 5 };
    //    Kitap kitap2 = new Kitap { Baslik = "Deniz Kurdu", Yazar = "Jack London", ISBN = "222", KopyaSayisi = 3 };
    //    Kitap kitap3 = new Kitap { Baslik = "Olaganustu Bir Gece", Yazar = "Stefan Zweig", ISBN = "333", KopyaSayisi = 2 };

}
