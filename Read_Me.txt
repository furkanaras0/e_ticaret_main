🚀 E-Commerce Projesi Geliştirme Günlüğü
Bu dosya, projenin adım adım nasıl geliştirildiğini, hangi teknolojilerin kullanıldığını ve öğrenilen temel kavramları takip etmek amacıyla oluşturulmuştur.

🗓️ GÜN 1: Backend (.NET Web API & EF Core Kurulumu)
İlk gün projenin arka planı olan .NET Web API mimarisini ayağa kaldırdım ve SQLite veritabanı bağlantılarını yapılandırdım.

🛠️ Yapılan İşlemler ve Kullanılan Komutlar
1. Proje ve Çözüm (Solution) Oluşturma:

Bash
dotnet new sln 
dotnet new webapi -o API --use-controllers
dotnet sln add API 
2. Gerekli Paketlerin Kurulumu:
Swagger UI ve Entity Framework Core (SQLite) için gerekli kütüphaneleri ekledim:

Bash
dotnet add package Swashbuckle.AspNetCore.SwaggerUI --version 10.1.7 
dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 9.0.0
3. EF Core CLI Araçlarının Kurulumu ve Veritabanı Güncellemesi:

Bash
dotnet tool install --global dotnet-ef  # İlk kez kuruyorsan
dotnet tool update --global dotnet-ef   # Güncellemek için
dotnet ef migrations add InitialCreate  # Migration oluşturma
dotnet ef database update               # Veritabanını oluşturma/güncelleme
💻 Yazılan Kodlar ve Yapılandırmalar
Swagger Entegrasyonu (Program.cs):

C#
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Swagger UI arayüzünün veri okuyacağı kaynak yolu özelleştirildi
app.UseSwaggerUI(Options =>
{
    Options.SwaggerEndpoint("/openapi/v1.json", "Demo API");
});
Veritabanı Bağlantı Cümlesi (appsettings.Development.json):

JSON
"ConnectionStrings": {
  "defaultConnection": "Data Source=ecommerce.db"
}
Veritabanı Context Ayarı (Program.cs):
Dependency Injection (DI) kullanılarak veritabanı bağlamı servis havuzuna eklendi:

C#
builder.Services.AddDbContext<DataContext>(options =>
{
   var config = builder.Configuration;
   var connectionString = config.GetConnectionString("defaultConnection");

   options.UseSqlite(connectionString);
});
Entity ve Context Sınıfları:
Entity klasörü altına Product modeli eklendi ve DataContext oluşturuldu:

C#
public class DataContext : DbContext
{
    // Veritabanındaki 'Products' tablosunu temsil eder.
    public DbSet<Product> Products => Set<Product>();
}
📝 Backend Notlarım
DataContext : DbContext Veritabanı ile C# arasındaki ana köprüdür. Miras alma (kalıtım) sayesinde EF Core yetenekleri kazanılır.

DbSet: Tablonun kod tarafındaki adıdır. CRUD işlemleri için kullanılır.

builder.Services.AddDbContext: DataContext sınıfını projenin servis havuzuna ekler. Çağırıldığı yerde otomatik olarak hazır gelir (Dependency Injection).

🗓️ GÜN 2: Frontend (React & Vite Kurulumu ve Bileşenler)
İkinci gün, kullanıcı arayüzü (UI) için Vite kullanarak modern bir React uygulaması oluşturdum ve temel bileşen (component) mimarisini kurdum.

🛠️ Yapılan İşlemler ve Kullanılan Komutlar
1. Proje Oluşturma ve Paket Yükleme:

Bash
npm create vite@latest
npm install
2. Port Yapılandırması (vite.config.ts):
Geliştirme sunucusunun 3000 portunda çalışması sağlandı:

TypeScript
export default defineConfig({
  server: {
    port: 3000
  }
})
3. Bileşenlerin (Components) Oluşturulması:

Header bileşeni oluşturuldu (<h1>Header</h1> döndürüyor).

Header bileşeni App fonksiyonuna (ana bileşene) dahil edildi.

ProductList bileşeni oluşturuldu ve altına bir HTML elementi eklendi.

📝 Frontend Notlarım
1. Uygulama Başlatma Süreci (Bootstrapping):

Host Environment (index.html): SPA'nın (Single Page App) çalıştığı tek sayfadır. <div id="root"></div> öğesi, React kodlarının DOM'a entegre edileceği "mount point" yani bağlantı noktasıdır.

Entry Point (main.tsx): Derleyici (Vite) için uygulamanın giriş noktasıdır.

createRoot & render: Sanal DOM'da oluşturulan <App /> ağacını, gerçek tarayıcı DOM'una (root id'li div'e) çizer.

2. Component (Bileşen) Mimarisi:
Bileşenler; bağımsız, yalıtılmış ve yeniden kullanılabilir modüllerdir (Özellik tabanlı ayrım).

Kapsülleme (Encapsulation): Her bileşen kendi state'ini yönetir. Birindeki hata diğerini çökertmez.

Yeniden Kullanılabilirlik (Reusability): Jenerik modüller yazılarak DRY (Kendini Tekrar Etme) ilkesi uygulanır.

Sürdürülebilirlik: Monolitik yapı yerine gevşek bağlı (loosely coupled) modüllerle çalışmak takım çalışmasını ve bakımı kolaylaştırır.

3. React State (Durum) Yönetimi:
Bileşenlerin yaşam döngüsü boyunca değişebilen ve arayüzün (UI) otomatik olarak yeniden çizilmesini (re-rendering) tetikleyen yapıdır.

Doğrudan Değiştirilemezlik (Immutability): count = count + 1 yerine her zaman setCount(count + 1) kullanılmalıdır.

Kapsülleme: Bir state sadece tanımlandığı bileşene aittir.

Toplu İşleme (Batched Updates): Performans için peş peşe yapılan state güncellemeleri biriktirilir ve ekran 1 kez render edilir.

🗓️ GÜN 3: Frontend - Backend Entegrasyonu (CORS) ve React Props Mimarisi
Üçüncü gün, React uygulaması ile .NET Web API arasındaki iletişimi sağladım, CORS ayarlarını yapılandırdım ve React tarafında bileşenler (components) arası veri aktarımını (Props) kurguladım. Dosya yapımı daha profesyonel hale getirerek bileşenlerimi bir klasör altında topladım.

🛠️ Yapılan İşlemler ve Yazılan Kodlar
1. Backend (.NET) Tarafında CORS Yapılandırması:
React uygulamam (localhost:3000), farklı bir portta çalışan API'me (localhost:5189) istek attığında tarayıcının güvenlik engeline takılmaması için CORS (Cross-Origin Resource Sharing) politikası eklendi (Program.cs):

C#
builder.Services.AddCors();

app.UseCors(opt =>
{
    opt.AllowAnyHeader()
       .AllowAnyMethod()
       .WithOrigins("http://localhost:3000"); // Sadece React uygulamama izin veriyorum
});
2. Frontend (React) Tarafında Veri Modeli (Interface) Oluşturulması:
API'den gelecek ürünlerin tip güvenliğini (Type Safety) sağlamak için IProduct arayüzü tanımlandı:

TypeScript
// model/IProduct.ts
export interface IProduct {
  id: number;
  name: string;
  price: number;
  isActive: boolean;
  description?: string;
  imgUrl?: string;
  stock?: number;
}
3. Ana Bileşen (App.tsx) - State ve API İsteği:
Uygulamanın ana verisi (state) burada tutuldu ve useEffect kancası ile API'den veri çekildi.

TypeScript
const [products, setProducts] = useState<IProduct[]>([]);

// Component ekrana ilk çizildiğinde API'ye istek atar
useEffect(() => { 
  fetch("http://localhost:5189/api/product")
    .then(response => response.json())
    .then(data => setProducts(data))
}, []);
4. Bileşenler Arası Veri Aktarımı (Props):
Ana bileşendeki products verisi ve addProduct fonksiyonu, alt bileşenlere Props yöntemiyle aktarıldı.

ProductList.tsx: Liste verisini alıp map fonksiyonu ile döngüye soktu ve her bir veri için <Product/> bileşenini çağırdı.

Product.tsx: Tekil ürün verisini alarak ekrana bastı. Ürünün isActive (aktif) durumuna göre Koşullu Render (Conditional Rendering) yapıldı (Ürün aktifse göster, değilse "Tükendi" yaz).

Header.tsx: Toplam ürün sayısını göstermek için veriyi prop olarak aldı.

📝 Öğrenilen Kavramlar ve Notlarım
CORS (Cross-Origin Resource Sharing): Tarayıcıların güvenlik önlemidir. Farklı portlar veya domainler (örn: 3000'den 5189'a) arası istekleri varsayılan olarak engeller. Backend tarafında izin verilerek çözülür.

useEffect Hook: React bileşenlerinin yaşam döngüsüne (lifecycle) müdahale etmemizi sağlar. İkinci parametre olarak boş dizi [] verdiğimizde, içindeki kod (örneğin API isteği) sayfa yüklendiğinde sadece bir kere çalışır.

Props: "Properties" kelimesinin kısaltmasıdır. React'te verilerin üst bileşenden (Parent - örn: App), alt bileşenlere (Child - örn: ProductList) aktarılmasını sağlayan mekanizmadır. Veri akışı tek yönlüdür (yukarıdan aşağıya).

Koşullu Render (Ternary Operator): {isActive ? (Aktifse Bunu Göster) : (Değilse Bunu Göster)} mantığı ile UI'ın duruma göre anlık şekil almasıdır.

Component Ağacı: App > ProductList > Product şeklinde hiyerarşik ve modüler bir yapı kuruldu.