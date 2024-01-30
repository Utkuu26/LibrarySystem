using UnityEngine;
using TMPro;

public class KutuphaneYonetimi : MonoBehaviour
{
    public Kutuphane kutuphane;
    public TMP_InputField aramaInputField;
    public GameObject kitapBilgiPanel;
    public TextMeshProUGUI kitapBilgiText;
    public TextMeshProUGUI kitapBilgiHataText;

    void Start()
    {
        // Kitapları ekle
        Kitap kitap1 = new Kitap { Baslik = "Kitap 1", Yazar = "Yazar 1", ISBN = "111", KopyaSayisi = 5 };
        Kitap kitap2 = new Kitap { Baslik = "Kitap 2", Yazar = "Yazar 2", ISBN = "222", KopyaSayisi = 3 };
        Kitap kitap3 = new Kitap { Baslik = "Kitap 3", Yazar = "Yazar 3", ISBN = "333", KopyaSayisi = 2 };

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

            // Kitap ödünç al
            kutuphane.KitapOduncAl("111");

            // Kitap iade et
            kutuphane.KitapIadeEt("111");

            // Gecikmiş kitapları görüntüle
            kutuphane.GecikmisKitaplar();
        }
        else
        {
            //Debug.LogError("Kutuphane nesnesi atanmamış. Lütfen Unity Editor'da Kutuphane alanını bağladığınızdan emin olun.");
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
}
