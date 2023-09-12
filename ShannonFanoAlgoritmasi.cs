using System;

namespace Sikistirma_Algoritmalari_CS
{
    public static partial class SikistirmaAlgoritmalari
    {
        /// <summary>
        /// veriyi yalnızca 0 ve 1 içeren metin formatında sıkıştırır ve geriye döndürür.
        /// </summary>
        public static string ShannonFanoEncoding<T>(IEnumerable<T> veri) where T : notnull
        {
            // Frekansları Bul ve Büyükten Küçüğe Sırala
            List<VeriFrekans<T>> veriFrekanslari = FrekansAnalizi(veri);
            veriFrekanslari.Sort();
            veriFrekanslari.Reverse();

            // Verilerin Yeni Karşılıklarını Bul
            // Sözlük Key: veri
            // Sözlük Value: verinin yeni karşılığı

            // Tüm Verileri Listeye Boş Olarak Ekle
            Dictionary<T, List<byte>> sozluk = new Dictionary<T, List<byte>>();
            foreach (var item in veriFrekanslari)
                sozluk.Add(item.Veri, new List<byte>());

            // Verilerin Karşılıklarını Hesapla
            ShannonFanoSozluk(sozluk, veriFrekanslari, 0, veriFrekanslari.Count-1);

            // Veriyi Yeni Karşılıklarıyla Metne Dönüştür
            string sikistilirmisVeri = "";
            foreach (var item in veri)
                foreach (var deger in sozluk[item])
                    sikistilirmisVeri += (char) (deger + '0');
            
            return sikistilirmisVeri;
        }

        private static void ShannonFanoSozluk<T>(Dictionary<T, List<byte>> sozluk, List<VeriFrekans<T>> veriFrekanslari, int baslangic, int bitis) where T : notnull
        {
            if (sozluk.Count == 1) sozluk[veriFrekanslari[0].Veri].Add(0);

            if (baslangic >= bitis) return;

            // Toplam Frekansı Hesapla
            int toplamFrekans = 0;
            for (int i = baslangic; i <= bitis; i++)
                toplamFrekans += veriFrekanslari[i].Frekans;

            // Orta Noktayı Bul
            int orta = baslangic;
            int yarimFrekans = 0;
            int kalanFrekans = toplamFrekans;
            for (int i = baslangic; yarimFrekans < toplamFrekans / 2; i++)
            {
                yarimFrekans += veriFrekanslari[i].Frekans;
                orta = i;
            }

            // Orta Noktadan Öncekilere 0 Ekle
            for (int i = baslangic; i <= orta; i++)
                sozluk[veriFrekanslari[i].Veri].Add(0);

            // Orta Noktadan Sonrakilere 1 Ekle
            for (int i = orta+1; i <= bitis; i++)
                sozluk[veriFrekanslari[i].Veri].Add(1);

            ShannonFanoSozluk(sozluk, veriFrekanslari, baslangic, orta);
            ShannonFanoSozluk(sozluk, veriFrekanslari, orta+1, bitis);
            
        }

        /// <summary>
        /// Shannon Fano algoritması ile sıkıştırılmış veri ve sıkıştırılmamış verinin frekans sözlüğünü kullanarak orijinal veriyi elde eder.
        /// </summary>
        public static List<T> ShannonFanoDecoding<T>(Dictionary<T, int> frekanslar, string sikistirilmisVeri) where T : notnull
        {
            // Veri Frekanslarını Doğru Formata Dönüştür
            List<VeriFrekans<T>> veriFrekanslari = new List<VeriFrekans<T>>();
            foreach (var item in frekanslar)
            {
                VeriFrekans<T> frekans = new VeriFrekans<T>(item.Key);
                frekans.Frekans = item.Value;
                veriFrekanslari.Add(frekans);
            }
            veriFrekanslari.Sort();
            veriFrekanslari.Reverse();

            // Veri Frekanslarına Bakarak Sözlük Oluştur
            Dictionary<T, List<byte>> sozluk = new Dictionary<T, List<byte>>();
            foreach (var item in veriFrekanslari)
                sozluk.Add(item.Veri, new List<byte>());
            // Verilerin Karşılıklarını Hesapla
            ShannonFanoSozluk(sozluk, veriFrekanslari, 0, veriFrekanslari.Count - 1);

            // Sözlüğe Bakarak Orijinal Veriyi Hesapla
            List<T> sonuc = new List<T>();
            List<byte> veri = new List<byte>();
            foreach (var item in sikistirilmisVeri)
            {
                veri.Add((byte)(item - '0'));

                // Sözlükte Böyle Bir Değer Var Mı Diye Kontrol Et
                // Yoksa Devam Et
                foreach (var sozlukDeger in sozluk)
                    if (ListelerEsitMi(sozlukDeger.Value, veri))
                    {
                        sonuc.Add(sozlukDeger.Key);
                        veri.Clear();
                        break;
                    }
            }

            return sonuc;
        }

    }
}
