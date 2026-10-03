public class Playfair {
    private string banro = "";
    private string khoa = "";
    private char[] bangchucai = {
        'A', 'B', 'C', 'D', 'E', 'F', 'G',
        'H', 'I', 'K', 'L', 'M', 'N', 'O', 
        'P', 'Q', 'R', 'S', 'T', 'U', 'V', 
        'W', 'X', 'Y', 'Z'
    };
    // dãy khóa đã chọn lọc từ trùng nhưng là 1 dãy nằm ngang 
    // tiện cho việc sắp xếp và chọn lọc
    private List<char> daykhoa = new List<char>();
    // sau đó bỏ dãy vào ma trận 5x5 
    private List<List<char>> matrankhoa = new List<List<char>>();

    public Playfair(){
        this.BanRo = "";
        this.Khoa = "";
        this.KhoiTaoMaTranKhoa();
    }

    public Playfair(string banro, string khoa){
        this.BanRo = banro;
        this.Khoa = khoa;
        this.KhoiTaoMaTranKhoa();
        this.BoKhoaVaoMaTran();
    }

    public string BanRo {
        get { return this.banro; }
        set {
            if(!string.IsNullOrEmpty(value)){
                this.banro = value.ToUpper().Replace(" ", "");
            }
        }
    }

    public string Khoa {
        get { return this.khoa; }
        set {
            if(!string.IsNullOrEmpty(value)){
                this.khoa = value.ToUpper().Replace(" ", "");
            }
        }
    }

    private void KhoiTaoMaTranKhoa(){
        for(int i = 0; i < 5; i++){
            this.matrankhoa.Add(new List<char>());
            for(int j = 0; j < 5; j++){
                this.matrankhoa[i].Add(' ');
            }
        }
    }

    public void NhapThongTin(){
        try {
            string banro_tam, khoa_tam;
            while(true){
                Console.Write("Nhập bản rõ: ");
                banro_tam = Console.ReadLine()!;
                if(string.IsNullOrEmpty(banro_tam)){
                    Console.WriteLine("Không để trống thông tin !");
                    continue;
                }

                Console.Write("Nhập khóa: ");
                khoa_tam = Console.ReadLine()!;
                if(string.IsNullOrEmpty(khoa_tam)){
                    Console.WriteLine("Không để trống thông tin !");
                    continue;
                }
                this.BanRo = banro_tam;
                this.Khoa = khoa_tam;
                this.BoKhoaVaoMaTran();
                break;
            }
        } catch(Exception ex){
            Console.WriteLine($"Lỗi: {ex.Message}");
        }
    }

    private void BoKhoaVaoMaTran(){
        // bỏ khóa vào dãy khóa trước
        for(int i = 0; i < this.khoa.Length; i++){
            // nếu là chữ cái đầu thì bỏ vào luôn 
            if(i == 0){
                this.daykhoa.Add(this.khoa[i]);
                continue;                
            } 
            // từ chữ cái thứ 2 trở đi phải kiểm tra có trùng ko 
            // nếu không trùng mới add vào dãy khóa
            if(!this.daykhoa.Contains(this.khoa[i])) {
                this.daykhoa.Add(this.khoa[i]);
            } 
        }

        // bỏ các từ còn lại trong bảng chữ cái vào dãy khóa 
        for(int i = 0; i < this.bangchucai.Length; i++){
            // nếu chữ cái nào không trùng với những từ đã có trong dãy khóa
            // thì thêm vào cho đủ 
            if(!this.daykhoa.Contains(this.bangchucai[i])){
                this.daykhoa.Add(this.bangchucai[i]);
            }
        }

        // chuyển dãy khóa (mảng 1 chiều) thành bảng khóa (ma trận)
        for(int i = 0; i < 5; i++){
            for(int j = 0; j < 5; j++){
                this.matrankhoa[i][j] = this.daykhoa[i * 5 + j];
            }
        }
    }

    public string MaHoa(){
        string banma = "";
        string banro_tam = this.banro.ToUpper();
        // thay những chữ J thành I và I/J chung 1 ô nên quy về I 
        banro_tam = banro_tam.Replace('J', 'I');
        for(int i = 0; i < banro_tam.Length; i+=2){
            char a = banro_tam[i];
            char b;
            // nếu từ kế bên không có thì cho a + với chữ X
            // sao cho mỗi cặp đều đủ 2 chữ cái
            // VD: SAYHAI -> SA YH I -> SA YH IX
            if(i + 1 >= banro_tam.Length){
                b = 'X';
            }
            // nếu 1 cặp có 2 từ giống nhau thì thay X vào giữa
            else if(a == banro_tam[i + 1]){
                b = 'X';
                i--;
            }
            else {
                b = banro_tam[i + 1];
            }

            int dong1 = 0, cot1 = 0;
            int dong2 = 0, cot2 = 0;

            for(int j = 0; j < 5; j++){
                for(int k = 0; k < 5; k++){
                    // gán dòng và cột cho từng chữ cái trong 1 cặp
                    if(this.matrankhoa[j][k] == a){
                        dong1 = j; cot1 = k;
                    }
                    if(this.matrankhoa[j][k] == b){
                        dong2 = j; cot2 = k;
                    }
                }
            }

            // nếu cùng dòng thì xích sang phải 1 ký tự 
            // dòng giữ nguyên, cột xích 1  
            // % 5 là để quay lại đầu dòng đó  
            if(dong1 == dong2){
                banma += this.matrankhoa[dong1][(cot1 + 1) % 5];
                banma += this.matrankhoa[dong2][(cot2 + 1) % 5];
            }
            // nếu cùng cột thì xích xuống 1 hàng 
            // dòng xích 1, cột giữ nguyên 
            else if(cot1 == cot2){
                banma += this.matrankhoa[(dong1 + 1) % 5][cot1];
                banma += this.matrankhoa[(dong2 + 1) % 5][cot2];
            }
            // nếu khác dòng khác cột 
            // thì giữ nguyên dòng đổi cột
            else {
                banma += this.matrankhoa[dong1][cot2];
                banma += this.matrankhoa[dong2][cot1];
            }
        }
        return banma;
    }

    public string GiaiMa(string banma){
        string banro = "";
        string banma_tam = banma;
        // nếu số ký tự lẽ (có 1 cặp lẻ)
        if(banma_tam.Length % 2 != 0){
            return "Mã hóa không đủ ký tự trong cặp để giải mã !";
        }
        for(int i = 0; i < banma_tam.Length; i+=2){
            char a = banma_tam[i];
            char b = banma_tam[i + 1];
            int dong1 = 0, cot1 = 0;
            int dong2 = 0, cot2 = 0;

            for(int j = 0; j < 5; j++){
                for(int k = 0; k < 5; k++){
                    // gán dòng và cột cho từng chữ cái trong 1 cặp
                    if(this.matrankhoa[j][k] == a){
                        dong1 = j; cot1 = k;
                    }
                    if(this.matrankhoa[j][k] == b){
                        dong2 = j; cot2 = k;
                    }
                }
            }

            // nếu cùng dòng thì tăng thêm 4 chữ để quay lại chữ ban đầu
            // VD: A  B  C  D  E  đang ở E muốn quay về D thì (Vị trí hiện tại + 4) % 5 
            // --> 1  2  3  4  0  lúc này nó đã quay về D
            if(dong1 == dong2){
                banro += matrankhoa[dong1][(cot1 + 4) % 5];
                banro += matrankhoa[dong2][(cot2 + 4) % 5];
            }
            // cùng cột làm tương tự 
            else if(cot1 == cot2){
                banro += matrankhoa[(dong1 + 4) % 5][cot1];
                banro += matrankhoa[(dong2 + 4) % 5][cot2];
            }
            // bản mã giữ dòng đổi cột
            // bản rõ cũng làm tương tự để giữ nguyên vị trí ban đầu 
            else{
                banro += matrankhoa[dong1][cot2];
                banro += matrankhoa[dong2][cot1];
            }

        }
        this.XoaKyTuThua(ref banro);
        return banro;
    }

    // xóa đi ký tự 'X' lúc đầu (nếu có thêm vào)
    private void XoaKyTuThua(ref string banro){
        for(int i = 1; i < banro.Length - 1; i++){
            // nếu 'X' tại vị trí i và chữ cái trước và sau X giống nhau
            // thì 'X' đó được đệm thêm 
            // bỏ 'X' và giảm i để tiếp tục kiểm tra 
            if(banro[i] == 'X' && banro[i - 1] == banro[i + 1]){
                banro = banro.Remove(i, 1);
                i--;
            }
        }
        // nếu 'X' nằm cuối chuỗi thì xóa 
        // vì đó là 'X' đệm cho đủ cặp
        if(banro.EndsWith("X")){
            banro = banro.Remove(banro.Length - 1);
        }
    }

    // hàm tách cặp chữ để in ra 
    private string TachCap(string noidung){
        string ketquatachcap = "";
        for(int i = 0; i < noidung.Length; i += 2){
            ketquatachcap += noidung[i];
            if(i + 1 < noidung.Length){
                ketquatachcap += noidung[i + 1];
            }
            ketquatachcap += " ";
        }
        return ketquatachcap;
    }

    public void InMaTranKhoa(){
        Console.WriteLine("Ma trận khóa: ");
        for(int i = 0; i < 5; i++){
            for(int j = 0; j < 5; j++){
                Console.Write(this.matrankhoa[i][j] + " ");
            }
            Console.WriteLine();
        }
    }

    public void InThongTin(string banma, string banro){
        Console.WriteLine($"Bản rõ (ban đầu): {this.banro}");
        Console.WriteLine($"Nội dung khóa: {this.khoa}");
        Console.WriteLine($"Bản mã: {this.TachCap(banma)} ({banma})");
        Console.WriteLine($"Bản rõ (giải mã): {this.TachCap(banro)} ({banro})");
    }
}