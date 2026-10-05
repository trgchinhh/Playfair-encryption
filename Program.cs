#region chú thích tài liệu 
/* 
──────────────────────────────────────────── 
     TRIỂN KHAI MÃ HÓA PLAYFAIR BẰNG C#   
           TÁC GIẢ: TRƯỜNG CHINH 
──────────────────────────────────────────── 
 
 
Chương trình nhỏ mô phỏng thuật toán mã hóa Playfair (mã hóa theo cặp ký tự) tự build 
 
 
- Giới thiệu: 
  + Mã hóa Playfair: là thuật toán mã hóa cổ điển dùng bảng chữ 5x5
    - Nguyên tắc: bản rõ được chia thành từng cặp ký tự, mỗi cặp được thay bằng 1 cặp khác 
      dựa trên vị trí của 2 ký tự trong bảng 5x5 sinh từ từ khóa 
    - Mã hóa thì dịch tiến (phải/xuống), giải mã thì dịch lùi (trái/lên) 
    - Chỉ cần 1 từ khóa nhưng dùng được cho cả 2 chiều mã hóa và giải mã 
  => Mã hóa theo cặp nên che được tần suất xuất hiện của từng chữ cái đơn lẻ, khó phá hơn Ceasar  
  -> Vì dùng 1 từ khóa cho 2 chiều mã hóa và giải mã nên đây cũng là 1 loại mã hóa đối xứng (dùng 1 khóa)  
 
 
- Thành phần: 
  + P: nội dung (bản rõ) 
  + C: nội dung sau mã hóa (bản mã) 
  + K: từ khóa (keyword) dùng để sinh bảng 5x5 
  + M: ma trận 5x5 chứa 25 chữ cái (vì I và J gộp chung làm 1) 
  + (r, c): tọa độ hàng, cột của ký tự trong ma trận M 
 
 
- Cách tạo ma trận 5x5: 
  + Viết từ khóa K vào ma trận từng ký tự theo hàng, bỏ ký tự trùng lặp 
  + Điền tiếp các chữ cái còn lại trong bảng chữ cái (A -> Z) vào các ô trống 
  + Gộp J thành I (chỉ còn 25 chữ cái) 
 
 
- Chuẩn hóa bản rõ: 
  + Chuyển thành chữ hoa, bỏ ký tự không phải chữ cái, thay J bằng I 
  + Chia bản rõ thành từng cặp 2 ký tự 
  + Nếu 2 ký tự trong cặp giống nhau -> chèn thêm 'X' vào giữa (VD: LL -> LX L...) 
  + Nếu số ký tự lẻ -> thêm 'X' vào cuối để đủ cặp 
 
 
- Các quy tắc biến đổi (cho mỗi cặp ký tự): 
  + Cùng hàng:     mỗi ký tự lấy ký tự bên phải nó (giải mã: bên trái), hết hàng thì quay về đầu hàng 
  + Cùng cột:      mỗi ký tự lấy ký tự bên dưới nó (giải mã: bên trên), hết cột thì quay về đầu cột 
  + Khác hàng khác cột (hình chữ nhật): mỗi ký tự lấy ký tự cùng hàng của mình 
    nhưng nằm ở cột của ký tự còn lại (mã hóa và giải mã giống nhau) 
 
 
- Công thức mã hóa (cặp P1 P2 -> C1 C2): 
  + Cùng hàng (r1 = r2):  C1 = M[r1][(c1 + 1) mod 5],  C2 = M[r2][(c2 + 1) mod 5] 
  + Cùng cột  (c1 = c2):  C1 = M[(r1 + 1) mod 5][c1],  C2 = M[(r2 + 1) mod 5][c2] 
  + Hình chữ nhật:        C1 = M[r1][c2],              C2 = M[r2][c1] 
 
 
- Công thức giải mã (cặp C1 C2 -> P1 P2): 
  + Cùng hàng (r1 = r2):  P1 = M[r1][(c1 - 1 + 5) mod 5],  P2 = M[r2][(c2 - 1 + 5) mod 5] 
  + Cùng cột  (c1 = c2):  P1 = M[(r1 - 1 + 5) mod 5][c1],  P2 = M[(r2 - 1 + 5) mod 5][c2] 
  + Hình chữ nhật:        P1 = M[r1][c2],                  P2 = M[r2][c1] 
 
 
- Ví dụ: 
  + Bản rõ P = "HELLO" 
  + Từ khóa: K = "MONARCHY" 
  + Ma trận 5x5: 
        M  O  N  A  R 
        C  H  Y  B  D 
        E  F  G  I  K       (I/J gộp chung) 
        L  P  Q  S  T 
        U  V  W  X  Z 
  + Chuẩn hóa: HELLO -> HE LX LO   (chèn X giữa cặp LL giống nhau) 
  + Mã hóa: 
    HE: H(1,1), E(2,0) -> hình chữ nhật -> C(1,0) F(2,1)  => CF 
    LX: L(3,0), X(4,3) -> hình chữ nhật -> S(3,3) U(4,0)  => SU 
    LO: L(3,0), O(0,1) -> hình chữ nhật -> P(3,1) M(0,0)  => PM 
    C = "CFSUPM" 
  + Giải mã: 
    CF: C(1,0), F(2,1) -> hình chữ nhật -> H E  => HE 
    SU: S(3,3), U(4,0) -> hình chữ nhật -> L X  => LX 
    PM: P(3,1), M(0,0) -> hình chữ nhật -> L O  => LO 
    -> HELXLO -> bỏ X chèn thêm -> P = "HELLO" 
    -> Nội dung sau khi giải mã quay trở lại đúng bản rõ ban đầu 
 
 
- Đút kết: 
  + Playfair chỉ dùng 1 từ khóa duy nhất nên được gọi là mã hóa đối xứng 
  + Từ khóa K (tức ma trận 5x5) vừa dùng để mã hóa vừa dùng để giải mã 
  + Mã hóa theo cặp nên an toàn hơn Ceasar, không bị lộ tần suất chữ cái đơn lẻ 
  + Nhưng vẫn kém an toàn vì: 
    - Chỉ có 25x25 = 625 cặp ký tự khả dĩ nên vẫn có thể phân tích tần suất theo cặp 
    - Bản mã không bao giờ có cặp 2 ký tự giống nhau (vd: "AA") -> để lộ quy luật 
 
        MÃ HÓA         │       GIẢI MÃ 
     ──────────────────┼────────────────── 
      - từ khóa K      │    - từ khóa K 
      - bản rõ P       │    - bản mã C 
      - cặp ký tự      │    - cặp ký tự 
      - dịch phải/xuống│    - dịch trái/lên 
      - ra C           │    - ra P 
 
*/ 
#endregion

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

/*
từ: truongchinh 
bản mã ban đầu: 
-> us mG hA Ừt Aj xÀ
sau khi đối xứng cặp 
-> xÀ Aj Ừt hA mG us
sau khi xoay 2 lần 
-> us xÀ Aj Ừt hA mG 

giải mã thì phải làm ngược lại từ bước cuối đến bước đầu 
*/ 