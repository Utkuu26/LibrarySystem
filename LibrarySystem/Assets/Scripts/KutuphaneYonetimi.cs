using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KutuphaneYonetimi : MonoBehaviour
{
    public Kutuphane kutuphane;
    public TMP_InputField aramaInputField;
    public GameObject kitapBilgiPanel;
    public TextMeshProUGUI kitapBilgiText;
    public TextMeshProUGUI kitapBilgiHataText;
    public GameObject borrowBtn;
    public GameObject notEnoughText;

    void Start()
    {
        // Kitapları ekle
        Kitap kitap1 = new Kitap { Baslik = "Deniz Kurdu", Yazar = "Jack London", ISBN = "111", KopyaSayisi = 5 };
        Kitap kitap2 = new Kitap { Baslik = "Simsek Hirsizi", Yazar = "Dogan Elmont", ISBN = "222", KopyaSayisi = 3 };
        Kitap kitap3 = new Kitap { Baslik = "Budala", Yazar = "Dostoyevski", ISBN = "333", KopyaSayisi = 2 };

        // Kütüphane'ye kitapları ekle
        if (kutuphane != null)
        {
            kutuphane.KitapEkle(kitap1);
            kutuphane.KitapEkle(kitap2);
            kutuphane.KitapEkle(kitap3);

            // Tüm kitapları listele
            kutuphane.TumKitaplariListele();

            // Kitap arama InputField'ını dinle
            if (aramaInputField != null)
            {
                aramaInputField.onEndEdit.AddListener(AramaYap);
            }
            else
            {
                Debug.LogError("Arama InputField'ı atanmamış. Lütfen Unity Editor'da Arama InputField alanını bağladığınızdan emin olun.");
            }
        }
        else
        {
            Debug.LogError("Kutuphane nesnesi atanmamış. Lütfen Unity Editor'da Kutuphane alanını bağladığınızdan emin olun.");
        }
    }

    // Arama işlemi için çağrılan metod
    void AramaYap(string aramaKelimesi)
    {
        if (kutuphane != null)
        {
            kutuphane.KitapAraVeGoster(aramaKelimesi);
        }
        else
        {
            Debug.LogError("Kutuphane nesnesi atanmamış. Lütfen Unity Editor'da Kutuphane alanını bağladığınızdan emin olun.");
        }
    }

    public void BorrowButtonClick()
    {
        if (kutuphane != null)
        {
            // Arama input field'ındaki değeri al
            string aramaKelimesi = aramaInputField.text;

            // Arama yap ve ilk bulunan kitabı al
            var bulunanKitaplar = kutuphane.KitapAra(aramaKelimesi);

            if (bulunanKitaplar.Count > 0)
            {
                // İlk bulunan kitabı al ve ödünç al
                Kitap ilkBulunanKitap = bulunanKitaplar[0];
                bool oduncAlindiMi = kutuphane.KitapOduncAlVeGoster(ilkBulunanKitap.Baslik);

                if (oduncAlindiMi)
                {
                    // Kitap ödünç alındıysa bilgileri güncelle
                    kutuphane.TumKitaplariListele();
                    Debug.Log($"{ilkBulunanKitap.Baslik} kitabı ödünç alındı.");

                    // Kontrol: Ödünç alınacak kitap kalmadıysa BorrowButton'ı kapat
                    if (kutuphane.KitapKalmadiMi())
                    {
                        // BorrowButton'ı kapat
                        // (Eğer BorrowButton bir GameObject olarak sahnede varsa setActive kullanılabilir)
                        Debug.Log("Ödünç alınacak kitap kalmadı. BorrowButton kapatılıyor.");
                        //borrowBtn.SetActive(false);
                        notEnoughText.SetActive(kutuphane.KitapKalmadiMi());
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
        }
        else
        {
            Debug.LogError("Kutuphane nesnesi atanmamış. Lütfen Unity Editor'da Kutuphane alanını bağladığınızdan emin olun.");
        }
    }
    
}
