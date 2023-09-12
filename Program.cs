namespace Sikistirma_Algoritmalari_CS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            string veri = "Recep Seymen Konuk";

            string sikistirilmisVeri = SikistirmaAlgoritmalari.ShannonFanoEncoding(veri);
            Console.WriteLine(sikistirilmisVeri);

            var sozluk = SikistirmaAlgoritmalari.FrekansSozluguOlustur(veri);
            List<char> list = SikistirmaAlgoritmalari.ShannonFanoDecoding(sozluk, sikistirilmisVeri);

            list.ForEach(x => Console.Write(x+" "));
            
            Console.ReadLine();
        }
    }
}