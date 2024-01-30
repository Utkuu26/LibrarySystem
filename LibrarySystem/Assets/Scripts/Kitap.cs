[System.Serializable]
public class Kitap
{
    public string Baslik { get; set; }
    public string Yazar { get; set; }
    public string ISBN { get; set; }
    public int KopyaSayisi { get; set; }
    public int OduncAlinanKopyalar { get; set; }

    public static Kitap KitapEkle(string baslik, string yazar, string isbn, int kopyaSayisi)
    {
        Kitap yeniKitap = new Kitap
        {
            Baslik = baslik,
            Yazar = yazar,
            ISBN = isbn,
            KopyaSayisi = kopyaSayisi
        };

        return yeniKitap;
    }
}
