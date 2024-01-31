using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KutuphaneYonetimi : MonoBehaviour
{
    public Kutuphane kutuphane;
    public TMP_InputField aramaInputField;
    public TMP_InputField returnInputField;
    public GameObject kitapBilgiPanel;
    public TextMeshProUGUI kitapBilgiText;
    public TextMeshProUGUI kitapBilgiHataText;
    public GameObject borrowBtn;
    public GameObject notEnoughText;

    void Start()
    {
        Kitap kitap1 = new Kitap { Baslik = "Deniz Kurdu", Yazar = "Jack London", ISBN = "111", KopyaSayisi = 5 };
        Kitap kitap2 = new Kitap { Baslik = "Simsek Hirsizi", Yazar = "Dogan Elmont", ISBN = "222", KopyaSayisi = 3 };
        Kitap kitap3 = new Kitap { Baslik = "Budala", Yazar = "Dostoyevski", ISBN = "333", KopyaSayisi = 2 };

        kutuphane.KitapEkle(kitap1);
        kutuphane.KitapEkle(kitap2);
        kutuphane.KitapEkle(kitap3);

        kutuphane.TumKitaplariListele();   
        aramaInputField.onEndEdit.AddListener(AramaYap);
    }

    void AramaYap(string aramaKelimesi)
    {
        kutuphane.KitapAraVeGoster(aramaKelimesi);
    }

    public void BorrowButtonClick()
    {
        string aramaKelimesi = aramaInputField.text;
        var bulunanKitaplar = kutuphane.KitapAra(aramaKelimesi);

        if (bulunanKitaplar.Count > 0)
        {
            Kitap ilkBulunanKitap = bulunanKitaplar[0];
            bool oduncAlindiMi = kutuphane.KitapOduncAlVeGoster(ilkBulunanKitap.Baslik);

            if (oduncAlindiMi)
            {
                kutuphane.TumKitaplariListele();
                Debug.Log($"{ilkBulunanKitap.Baslik} kitabı ödünç alındı.");

                if (kutuphane.KitapKalmadiMi())
                {
                    notEnoughText.SetActive(true);
                }
            }
            else
            {
                Debug.Log($"{ilkBulunanKitap.Baslik} kitabının ödünç alınacak yeterli kopyası bulunmamaktadır.");
            }
        }
        else
        {
            Debug.Log("Aranan kriterlere uygun kitap bulunamadı.");
        }

        kutuphane.UpdateBorrowedBooksText();
    }

    public void ReturnButtonClick()
    {
        string kitapIsmi = returnInputField.text;
        var kitap = kutuphane.kitaplar.Find(k => k.Baslik == kitapIsmi);

        if (kitap != null && kitap.OduncAlinanKopyalar > 0)
        {
            kitap.OduncAlinanKopyalar--;
            Debug.Log($"{kitap.Baslik} kitabı iade edildi.");
            kutuphane.UpdateBorrowedBooksText();
            kutuphane.TumKitaplariListele();
            returnInputField.text = "";
            kitapBilgiPanel.SetActive(false);
            Debug.Log($"{kitapIsmi} kitabı iade edildi.");
        }
        else
        {
            Debug.Log($"{kitapIsmi} kitabı iade edilemedi. Ödünç alınan bir kopya bulunamadı.");
        }
    }
    
}
