using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Kutuphane : MonoBehaviour
{
    public List<Kitap> kitaplar = new List<Kitap>();
    public TextMeshProUGUI kitapListesiText;
    public GameObject kitapBilgiPanel;
    public TextMeshProUGUI kitapBilgiText;
    public TextMeshProUGUI kitapBilgiHataText;
    public TextMeshProUGUI borrowedBooksText;

    public TMP_InputField kitapIsmiInput;
    public TMP_InputField yazarIsmiInput;
    public TMP_InputField isbnInput;
    public GameObject warningTxt;
    public GameObject warningTxt2;
    public TMP_InputField returnInputField;

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

        kitapListesiText.text = kitapListesi; 
    }

    public bool KitapOduncAlVeGoster(string kitapBaslik)
    {
        var kitap = kitaplar.Find(k => k.Baslik == kitapBaslik);

        if (kitap != null && kitap.KopyaSayisi - kitap.OduncAlinanKopyalar > 0)
        {
            kitap.OduncAlinanKopyalar++;
            Debug.Log($"{kitap.Baslik} kitabı ödünç alındı.");
            UpdateBorrowedBooksText();
            return true;
        }
        else
        {
            Debug.Log($"{kitap.Baslik} kitabının ödünç alınacak yeterli kopyası bulunmamaktadır.");
            return false;
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

        borrowedBooksText.text = borrowedBooks;
    }

    public bool KitapKalmadiMi()
    {
        foreach (var kitap in kitaplar)
        {
            if (kitap.KopyaSayisi > kitap.OduncAlinanKopyalar)
            {
                return false; 
            }
        }
        return true; 
    }

    public void KitapEkleButton()
    {
        string kitapIsmi = kitapIsmiInput.text;
        string yazarIsmi = yazarIsmiInput.text;
        int isbn;

        if (!int.TryParse(isbnInput.text, out isbn))
        {
            Debug.LogError("ISBN değeri geçerli bir tam sayı değil.");
            warningTxt.SetActive(true);
            warningTxt2.SetActive(false);
            return;
        }

        if (string.IsNullOrEmpty(kitapIsmi) || string.IsNullOrEmpty(yazarIsmi))
        {
            Debug.LogError("Lütfen tüm bilgileri doldurun.");
            warningTxt2.SetActive(true);
            warningTxt.SetActive(false);
            return;
        }

        Kitap yeniKitap = new Kitap
        {
            Baslik = kitapIsmi,
            Yazar = yazarIsmi,
            ISBN = isbn.ToString(), 
            KopyaSayisi = 1 
        };

        KitapEkle(yeniKitap);

        kitapIsmiInput.text = "";
        yazarIsmiInput.text = "";
        isbnInput.text = "";
    }

    public void KitapIadeEt(string kitapIsmi)
    {
        var kitap = kitaplar.Find(k => k.Baslik == kitapIsmi);

        if (kitap != null && kitap.OduncAlinanKopyalar > 0)
        {
            kitap.OduncAlinanKopyalar--;
            UpdateBorrowedBooksText();
            TumKitaplariListele();
            Debug.Log($"{kitap.Baslik} kitabı iade edildi.");
        }
        else
        {
            Debug.Log($"Kitap iade edilemedi. '{kitapIsmi}' isminde ödünç alınmış bir kopya bulunamadı.");
        }
    }

    public void ReturnButtonClicked()
    {
        string kitapIsmi = returnInputField.text;
        KitapIadeEt(kitapIsmi);
        kitapBilgiPanel.SetActive(false);
    }

}
