## Teknik Gereksinimler
- **.NET 7.0 ve üzeri** ile çalışması.
- **Model – Context – API – Web** katmanlarının ayrı olması.
- **MSSQL** veritabanı kullanımı.

## İş Gereksinimleri
1. **Kayıt Olma Sayfası:**
   - Ad, soyad, e-mail, telefon, şifre.
   - Kayıt sonrası **hoş geldiniz e-postası** gönderme.
  
2. **Giriş Yapma Sayfası:**
   - E-mail ve şifre ile giriş yapılması.

3. **Toplantı CRUD İşlemleri:**
   - Toplantı adı, başlangıç ve bitiş tarihi, açıklama.
  
4. **Toplantı İptali ve Silinmesi:**
   - Toplantı iptal işlemi ve **sistemden silinmesi**. 
   - Quartz kullanarak belirli periyotlarla silme işlemi.
   - Silinen toplantılar, **MSSQL Trigger** kullanılarak bir log tablosuna kaydedilecektir.

5. **Toplantı Bilgilendirme Maili:**
   - Toplantı oluşturuldukça ilgili kişilere bilgilendirme maili gönderilecektir.

6. **Parola Şifreleme:**
   - Veritabanında **şifrelerin şifrelenerek** tutulması sağlanacaktır.

7. **API Katmanı Özellikleri:**
   - **JWT** ile kimlik doğrulama (Authentication).
   - **Swagger** desteği ile API dokümantasyonu sağlanacaktır.


## Kurulum

### Prerequisites
- [.NET 7.0 SDK](https://dotnet.microsoft.com/download) veya üzeri kurulu olmalıdır.
- **MSSQL** veritabanı kurulumuna ihtiyaç vardır.
- Bir SMTP sunucusu kullanarak e-posta gönderimi yapılandırılmalıdır.

### Adımlar
1. **Projeyi Klonla:**
   ```bash
   git clone https://github.com/MeralTd/OctapullProject.git
   cd OctapullProject
