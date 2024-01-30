using UnityEngine;

public class KutuphaneYonetimi : MonoBehaviour
{
    public Kutuphane kutuphane; 

    void Start()
    {
        // Kitapları ekle
        Kitap kitap1 = new Kitap { Baslik = "Vahsetin Cagrisi", Yazar = "Jack London", ISBN = "111", KopyaSayisi = 5 };
        Kitap kitap2 = new Kitap { Baslik = "Deniz Kurdu", Yazar = "Jack London", ISBN = "222", KopyaSayisi = 3 };
        Kitap kitap3 = new Kitap { Baslik = "Olaganustu Bir Gece", Yazar = "Stefan Zweig", ISBN = "333", KopyaSayisi = 2 };

        // Kütüphane'ye kitapları ekle
        if (kutuphane != null)
        {
            kutuphane.KitapEkle(kitap1);
            kutuphane.KitapEkle(kitap2);
            kutuphane.KitapEkle(kitap3);

            // Tüm kitapları listele
            kutuphane.TumKitaplariListele();

            // Kitap ara
            kutuphane.KitapAra("Kitap");

            // Kitap ödünç al
            kutuphane.KitapOduncAl("111");

            // Kitap iade et
            kutuphane.KitapIadeEt("111");

            // Gecikmiş kitapları görüntüle
            kutuphane.GecikmisKitaplar();
        }
        else
        {
            Debug.LogError("Kutuphane nesnesi atanmamış. Lütfen Unity Editor'da Kutuphane alanını bağladığınızdan emin olun.");
        }
    }
}
