using System;

namespace Sikistirma_Algoritmalari_CS
{
    public static partial class SikistirmaAlgoritmalari
    {
        /// <summary>
        /// Aldığı iki adet listenin içerikleri aynıysa true döndürür.
        /// </summary>
        private static bool ListelerEsitMi<T>(List<T> liste1, List<T> liste2) where T : notnull
        {
            if (liste1 == null && liste2 == null) return true;
            if (liste1 == null || liste2 == null) return false;

            if (liste1.Count != liste2.Count) return false;

            for (int i = 0; i <  liste1.Count; i++)
                if (!liste1[i].Equals(liste2[i])) 
                    return false;

            return true;
        }

        /// <summary>
        /// verideki tekil veriler ve bu tekil verilerin veride kaç adet geçtiğini Dictionary formatında döndürür.
        /// </summary>
        public static Dictionary<T, int> FrekansSozluguOlustur<T>(IEnumerable<T> veri) where T : notnull
        {
            // Sözlük Oluştur.
            // Sözlük Key: veri
            // Sözlük Value: verinin frekansı
            Dictionary<T, int> sozluk = new Dictionary<T, int>();

            foreach (T item in veri)
            {
                if (sozluk.ContainsKey(item))
                    sozluk[item]++;
                else
                    sozluk.Add(item, 1);
            }
            return sozluk;
        }

        private static List<VeriFrekans<T>> FrekansAnalizi<T>(IEnumerable<T> veri) where T : notnull
        {

            // Sözlük Oluştur.
            // Sözlük Key: veri
            // Sözlük Value: verinin frekansı
            Dictionary<T, int> sozluk = FrekansSozluguOlustur(veri);

            // Sözlüğe Bakarak Liste Oluştur
            List<VeriFrekans<T>> veriFrekans = new List<VeriFrekans<T>>();

            foreach (var item in sozluk)
            {
                VeriFrekans<T> frekans = new VeriFrekans<T>(item.Key);
                frekans.Frekans = item.Value;
                veriFrekans.Add(frekans);
            }

            return veriFrekans;
        }

        private class VeriFrekans<T> : IComparable<VeriFrekans<T>>
        {
            public T Veri { get; private set; }
            public int Frekans { get; set; }

            public VeriFrekans(T deger)
            {
                Veri = deger;
                Frekans = 1;
            }

            public int CompareTo(VeriFrekans<T>? other)
            {
                if (other == null) return 1;
                return Frekans.CompareTo(other.Frekans);
            }
        }
    }
}
