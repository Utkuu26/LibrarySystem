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
                kitapBilgiText.text = $"Başlık: {ilkBulunanKitap.Baslik}\nYazar: {ilkBulunanKitap.Yazar}\nISBN: {ilkBulunanKitap.ISBN}\nKopya Sayısı: {ilkBulunanKitap.KopyaSayisi}\nÖdünç Alınan Kopyalar: {ilkBulunanKitap.OduncAlinanKopyalar}";
            }
            else
            {
                kitapBilgiPanel.SetActive(false);
                kitapBilgiHataText.text = "Aranan kriterlere uygun kitap bulunamadı.";
            }
        }
        else
        {
            Debug.LogError("Kitap Bilgi Paneli atanmamis");
        }
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
