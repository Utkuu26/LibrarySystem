using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Kutuphane : MonoBehaviour
{
    private List<Kitap> kitaplar = new List<Kitap>();
    public TextMeshProUGUI kitapListesiText;

     void Start()
    {
        TumKitaplariListele();
    }

    public void KitapEkle(Kitap yeniKitap)
    {
        kitaplar.Add(yeniKitap);
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

    public void KitapAra(string aramaKelimesi)
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
    }

    public void KitapOduncAl(string isbn)
    {
        var kitap = kitaplar.Find(k => k.ISBN == isbn);

        if (kitap != null && kitap.KopyaSayisi > kitap.OduncAlinanKopyalar)
        {
            kitap.OduncAlinanKopyalar++;
            Debug.Log($"{kitap.Baslik} kitabı ödünç alındı.");
        }
        else
        {
            Debug.Log("Kitap ödünç alınamadı. Stokta yeterli kopya yok veya kitap bulunamadı.");
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

    public void GecikmisKitaplar()
    {
        var gecikmisKitaplar = kitaplar.FindAll(k => k.OduncAlinanKopyalar > 0);

        if (gecikmisKitaplar.Count > 0)
        {
            Debug.Log("Gecikmiş Kitaplar:");
            foreach (var kitap in gecikmisKitaplar)
            {
                Debug.Log($"Başlık: {kitap.Baslik}, Yazar: {kitap.Yazar}, ISBN: {kitap.ISBN}, Kopya Sayısı: {kitap.KopyaSayisi}, Ödünç Alınan Kopyalar: {kitap.OduncAlinanKopyalar}");
            }
        }
        else
        {
            Debug.Log("Gecikmiş kitap bulunmamaktadır.");
        }
    }
}
