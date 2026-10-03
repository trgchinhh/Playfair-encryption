public class Program {
    public static void Inbanner(){
        string banner = @"┌──────────────────────────────┐
│       Mã hóa ứng dụng        │
│ Mô phỏng mã hóa Playfair     │
│ Tác giả: Trường Chinh        │
│ Github: Github.com/trgchinhh │
└──────────────────────────────┘
";      
        Console.Clear();
        Console.WriteLine(banner);
    }

    public static void Main(){
        Inbanner();

        Playfair pf = new Playfair();
        pf.NhapThongTin();

        pf.InMaTranKhoa();
        string banma = pf.MaHoa();
        string banro = pf.GiaiMa(banma);
        pf.InThongTin(banma, banro);
    }
}