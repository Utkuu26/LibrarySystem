using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Kutuphane : MonoBehaviour
{
    private List<Kitap> kitaplar = new List<Kitap>();
    public TextMeshProUGUI kitapListesiText;
    public GameObject kitapBilgiPanel;
    public TextMeshProUGUI kitapBilgiText;
    public TextMeshProUGUI kitapBilgiHataText;
    public TextMeshProUGUI borrowedBooksText;

    void Start()
    {
        kitapBilgiPanel.SetActive(false);
        TumKitaplariListele();
    }

    public List<Kitap> KitapAra(string aramaKelimesi)
    {
        var bulunanKitaplar = kitaplar.FindAll(k => k.Baslik.Contains(aramaKelimesi) || k.Yazar.Contains(aramaKelimesi));

        if (bulunanKitaplar.Count > 0)
        {
            Debug.Log("Arama Sonuçları:");
            foreach (var kitap in bulunanKitaplar)
            {
                Debug.Log($"Başlık: {kitap.Baslik}, Yazar: {kitap.Yazar}, ISBN: {kitap.ISBN}, Kopya Sayısı: {kitap.KopyaSayisi}, Ödünç Alınan Kopyalar: {kitap.OduncAlinanKopyalar}");
            }
        }
        else
        {
            Debug.Log("Aranan kriterlere uygun kitap bulunamadı.");
        }

        return bulunanKitaplar;
    }

    public void KitapAraVeGoster(string aramaKelimesi)
    {
        var bulunanKitaplar = KitapAra(aramaKelimesi);

        if (kitapBilgiPanel != null && kitapBilgiText != null && kitapBilgiHataText != null)
        {
            if (bulunanKitaplar.Count > 0)
            {
                kitapBilgiPanel.SetActive(true);
                kitapBilgiHataText.text = "";
                Kitap ilkBulunanKitap = bulunanKitaplar[0];
                kitapBilgiText.text = $"Başlık: {ilkBulunanKitap.Baslik}\nYazar: {ilkBulunanKitap.Yazar}\nISBN: {ilkBulunanKitap.ISBN}\n";
            }
            else
            {
                kitapBilgiPanel.SetActive(false);
                kitapBilgiHataText.text = "Aranan kriterlere uygun kitap bulunamadı.";
            }
        }
        else
        {
            Debug.LogError("Kitap Bilgi Paneli atanmamış");
        }
    }

    public void KitapEkle(Kitap yeniKitap)
    {
        kitaplar.Add(yeniKitap);
        TumKitaplariListele();
    }

    public void TumKitaplariListele()
    {
        string kitapListesi = "";

        foreach (var kitap in kitaplar)
        {
            kitapListesi += $"Başlık: {kitap.Baslik}, Yazar: {kitap.Yazar}, ISBN: {kitap.ISBN}, Kopya Sayısı: {kitap.KopyaSayisi}, Ödünç Alınan Kopyalar: {kitap.OduncAlinanKopyalar}\n\n";
        }

        if (kitapListesiText != null)
        {
            kitapListesiText.text = kitapListesi; // TextMeshProUGUI elemanının metin içeriğini güncelle
        }
        else
        {
            Debug.LogError("TextMeshProUGUI elemanı atanmamış.");
        }
    }

    public bool KitapOduncAlVeGoster(string kitapBaslik)
    {
        var kitap = kitaplar.Find(k => k.Baslik == kitapBaslik);

        if (kitap != null && kitap.KopyaSayisi - kitap.OduncAlinanKopyalar > 0)
        {
            kitap.OduncAlinanKopyalar++;
            Debug.Log($"{kitap.Baslik} kitabı ödünç alındı.");

            // Ödünç alındığında borrowedBooksText'i güncelle
            UpdateBorrowedBooksText();
            return true;
        }
        else
        {
            Debug.Log($"{kitap.Baslik} kitabının ödünç alınacak yeterli kopyası bulunmamaktadır.");
            return false;
        }
    }

    public void KitapIadeEt(string isbn)
    {
        var kitap = kitaplar.Find(k => k.ISBN == isbn);

        if (kitap != null && kitap.OduncAlinanKopyalar > 0)
        {
            kitap.OduncAlinanKopyalar--;
            Debug.Log($"{kitap.Baslik} kitabı iade edildi.");
        }
        else
        {
            Debug.Log("Kitap iade edilemedi. Ödünç alınan bir kopya bulunamadı.");
        }
    }

    public void UpdateBorrowedBooksText()
    {
        string borrowedBooks = "";

        foreach (var kitap in kitaplar)
        {
            if (kitap.OduncAlinanKopyalar > 0)
            {
                borrowedBooks += $"{kitap.Baslik} - {kitap.OduncAlinanKopyalar} adet\n";
            }
        }

        if (borrowedBooksText != null)
        {
            borrowedBooksText.text = borrowedBooks;
        }
        else
        {
            Debug.LogError("BorrowedBooksText atanmamış.");
        }
    }

    public bool KitapKalmadiMi()
{
    foreach (var kitap in kitaplar)
    {
        if (kitap.KopyaSayisi > kitap.OduncAlinanKopyalar)
        {
            return false; // Hala ödünç alınacak kitap var
        }
    }
    return true; // Ödünç alınacak kitap kalmadı
}
}
