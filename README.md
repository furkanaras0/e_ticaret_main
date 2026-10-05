# 🛒 FullStack E-Ticaret Projesi (.NET 9 Web API & React)

Bu proje, **Sadık Turan** tarafından hazırlanan **Udemy** üzerindeki **".NET & React ile FullStack E-Ticaret Projesi Geliştirme 2026"** kursu takip edilerek, adım adım modern mimari prensipleri ve en iyi yazılım pratikleri doğrultusunda geliştirilmiştir.

---

## 👨‍🏫 Kurs ve Eğitmen Bilgileri
* **Kurs Adı:** .NET & React ile FullStack E-Ticaret Projesi Geliştirme 2026
* **Platform:** Udemy
* **İçerik Üreticisi / Eğitmen:** Sadık Turan
* **Proje Tipi:** Full-Stack Web Uygulaması (E-Commerce Platform)

---

## 🚀 Teknolojik Altyapı (Tech Stack)

### 🔹 Backend (.NET Web API)
* **Framework:** .NET 9.0 Web API (C#)
* **ORM & Veritabanı:** Entity Framework Core 9.0, SQLite
* **Kimlik Doğrulama & Yetkilendirme:** ASP.NET Core Identity, JWT (JSON Web Token - `System.IdentityModel.Tokens.Jwt`)
* **Ödeme Entegrasyonu:** Iyzipay (.NET SDK - Sanal POS Entegrasyonu)
* **API Dokümantasyonu:** Swagger UI & Scalar OpenAPI
* **Mimari Yaklaşım:** Controller-Service Pattern, DTO (Data Transfer Object) Mimarisi, Global Exception Handling Middleware, Cookie-based Anonymous Session

### 🔹 Frontend (React Client)
* **Kütüphane & Araçlar:** React 19, TypeScript, Vite
* **Arayüz (UI) Kütüphanesi:** Material UI (MUI v6/v7), MUI Icons, MUI Lab
* **State Yönetimi:** Redux Toolkit (`@reduxjs/toolkit`, `react-redux`)
* **Yönlendirme:** React Router (`react-router`) & AuthGuard (Route Protection)
* **Form & Doğrulama:** React Hook Form (`react-hook-form`)
* **HTTP İstekleri:** Axios (İstek/Yanıt Interceptor'ları ile otomatik JWT Bearer Token ekleme)
* **Bildirimler:** React Toastify (`react-toastify`)

---

## 🌟 Temel Özellikler

1. **Ürün Kataloğu & Detay Yönetimi:**
   * Ürünlerin dinamik listelenmesi, filtreleme ve detay görüntüleme.
   * Stok kontrolü ve ürün detay sayfasından sepete dinamik ekleme.

2. **Gelişmiş Sepet (Cart) Mimarisi:**
   * **Anonim Sepet:** Kullanıcı giriş yapmamışken Cookie (`customerId`) üzerinden sepet tutma.
   * **Sepet Birleştirme (Cart Transfer):** Kullanıcı oturum açtığında anonim sepetteki ürünlerin kullanıcı hesabına otomatik aktarılması ve birleştirilmesi.
   * Redux Toolkit ile anlık UI güncellemeleri ve Badge sayacı.

3. **Kimlik Doğrulama & Kullanıcı Yönetimi (Identity & JWT):**
   * Kullanıcı kayıt (Register) ve giriş (Login) akışları.
   * JWT Bearer Token üretimi, LocalStorage saklama ve sayfa yenilendiğinde otomatik oturum kurtarma (`getuser`).
   * Güvenli çıkış (Logout) ve kullanıcı profil menüsü.

4. **Çok Adımlı Ödeme ve Sipariş Akışı (Multi-Step Checkout):**
   * **Adım 1 - Teslimat Bilgileri:** Ad, soyad, telefon, adres satırı ve şehir bilgileri.
   * **Adım 2 - Ödeme Formu:** Kart sahibi adı, kart numarası, son kullanma tarihi ve CVC.
   * **Adım 3 - İnceleme & Özet:** Sipariş kalemleri, kargo, ara toplam ve genel toplam özeti.

5. **Iyzipay Sanal POS Entegrasyonu:**
   * Backend tarafında Iyzipay Sandbox API ile güvenli kartlı ödeme işlemi.
   * Ödeme başarılı olduğunda sipariş kaydı oluşturma, stok adetlerini düşürme ve sepeti temizleme.

6. **Sipariş Geçmişi & Rota Güvenliği (AuthGuard):**
   * Sadece giriş yapmış kullanıcıların erişebildiği korumalı rotalar (`/checkout`, `/orders`).
   * Geçmiş siparişlerin detaylı listelenmesi (`OrderList`).

7. **Merkezi Hata Yönetimi:**
   * Backend: Özel `ExceptionHandling` middleware ile RFC 7807 standartlarında `ProblemDetails` hata yanıtları.
   * Frontend: Axios interceptor ile 400, 401, 404, 500 hatalarının yakalanması, Toastify bildirimleri ve `ServerError` / `NotFound` yönlendirmeleri.

---

## 🗓️ Geliştirme Süreci & Modül Günlüğü

### 📌 GÜN 1: Backend (.NET Web API & EF Core Kurulumu)
* `dotnet new sln` ve `dotnet new webapi -o API --use-controllers` ile temel mimari kuruldu.
* SQLite bağlantısı ve Entity Framework Core (`Microsoft.EntityFrameworkCore.Sqlite`, `Design`) eklendi.
* `DataContext` sınıfı ve `Product` entity modeli oluşturuldu.
* Initial migration çalıştırılarak SQLite veritabanı oluşturuldu.
* Swagger UI ve OpenAPI konfigürasyonu yapıldı.

### 📌 GÜN 2: Frontend (React & Vite Kurulumu ve Bileşenler)
* Vite ve TypeScript ile React projesi (`Client`) oluşturuldu.
* Port ayarları (localhost:3000) yapılandırıldı.
* İlk bileşen (Component) hiyerarşisi oluşturuldu: `Header`, `ProductList`, `Product`.
* React state ve tek yönlü veri akışı temelleri uygulandı.

### 📌 GÜN 3: API Entegrasyonu, CORS ve Props Mimarisi
* Backend'de React origin'ine (`http://localhost:3000`) izin veren CORS politikası tanımlandı.
* Frontend tarafında `IProduct` TypeScript arayüzü tanımlandı.
* `useEffect` ile backend `api/products` endpoint'inden veri çekildi ve props ile bileşenlere dağıtıldı.

### 📌 GÜN 4: React Router & Sayfa Yönlendirme
* `react-router` entegre edildi ve merkezi `Routes.tsx` oluşturuldu.
* `HomePage`, `AboutPage`, `ContactPage`, `CatalogPage`, `ProductDetails` sayfaları eklendi.
* Master layout (`App.tsx`) ve `<Outlet />` yapısına geçildi.

### 📌 GÜN 5: Merkezi Hata Yönetimi (Middleware & Axios Interceptor)
* Backend tarafına global `ExceptionHandling` middleware eklendi.
* Frontend'de merkezi `requests.ts` modülü yazılarak Axios response interceptor yapılandırıldı.
* `react-toastify` ile hata bildirimleri bağlandı; `NotFound` ve `ServerError` sayfaları eklendi.

### 📌 GÜN 6: Sepet (Cart) Mimarisi & Cookie Oturumu
* Backend'de `Cart` ve `CartItem` modelleri ve `AddCartTables` migration'ı oluşturuldu.
* `CartController` ile sepet oluşturma, ürün ekleme ve silme endpoint'leri yazıldı.
* Cookie üzerinden anonim sepet kimliği (`customerId`) yönetimi sağlandı.
* Frontend'de `ShoppingCartPage` ve `CartSummary` bileşenleri oluşturuldu.

### 📌 GÜN 7: Global State Yönetimi (Redux Toolkit)
* Context API yerine Redux Toolkit (`@reduxjs/toolkit`) mimarisine geçildi.
* `cartSlice.ts` oluşturularak `addItemToCart`, `deleteItemFromCart`, `getCart` async thunk'ları yazıldı.
* `Header` üzerindeki sepet sayacı ve sepet sayfaları Redux store'a bağlandı.

### 📌 GÜN 8: Identity & JWT Tabanlı Kimlik Doğrulama
* Backend'de `Microsoft.AspNetCore.Identity` (`AppUser`, `AppRole`) entegrasyonu yapıldı.
* `TokenService` ile JWT token üretimi ve `[Authorize]` korumalı endpoint'ler yapılandırıldı.
* `AccountController` (`login`, `register`, `getuser`) geliştirildi.
* Swagger'a JWT Bearer Token ekleme desteği tanımlandı.

### 📌 GÜN 9: Redux Account Slice & Sepet Birleştirme
* Frontend'de `accountSlice.ts` oluşturuldu; Login ve Register formları (`LoginPage`, `RegisterPage`) yazıldı.
* JWT token LocalStorage'a kaydedilerek Axios istek başlıklarına (`Authorization: Bearer <token>`) otomatik eklendi.
* Giriş yapıldığında anonim cookie sepeti ile kullanıcı veritabanı sepeti birleştirildi (Cart Transfer).

### 📌 GÜN 10: Çok Adımlı Checkout (MUI Stepper & React Hook Form)
* Material UI Stepper ile 3 adımlı Checkout akışı kuruldu:
  1. `AddressForm` (Teslimat Adresi)
  2. `PaymentForm` (Kredi Kartı Bilgileri)
  3. `Review` & `Info` (Sipariş Özeti)
* `react-hook-form` ve `FormProvider` ile tüm adımlardan form verileri toplandı.

### 📌 GÜN 11: Iyzipay Sanal POS Entegrasyonu & Sipariş Yönetimi
* Backend'de `Iyzipay` NuGet paketi entegre edildi.
* `OrdersController` oluşturuldu; `CreateOrder` işleminde Iyzipay Sandbox API üzerinden kart ödemesi yapıldı.
* Ödeme onaylandığında sipariş tablosuna (`Orders`, `OrderItems`) kaydedildi, ürün stokları güncellendi ve sepet sıfırlandı.

### 📌 GÜN 12: Rota Koruma (AuthGuard) & Sipariş Geçmişi
* `AuthGuard` bileşeni ile yetkisiz kullanıcıların `/checkout` ve `/orders` sayfalarına erişimi engellendi.
* Kullanıcının geçmiş siparişlerini listeleyen `OrderList` sayfası eklendi.
* Kullanıcı profil menüsüne "Siparişlerim" linki ve "Çıkış Yap" butonu entegre edildi.

---

## ⚙️ Kurulum ve Çalıştırma

### 1. Gereksinimler
* [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
* [Node.js](https://nodejs.org/) (v18 veya üzeri) & npm
* SQLite (EF Core CLI araçları: `dotnet tool install --global dotnet-ef`)

### 2. Backend'i Başlatma (API)
```bash
cd API
dotnet restore
dotnet ef database update
dotnet run
```
> API varsayılan olarak `http://localhost:5189` adresinde çalışacaktır.
> Swagger Dokümantasyonu: `http://localhost:5189/swagger`

### 3. Frontend'i Başlatma (Client)
```bash
cd Client
npm install
npm run dev
```
> React uygulaması `http://localhost:3000` adresinde açılacaktır.

---

## 📜 Lisans & Teşekkür
Bu proje eğitim amaçlı olarak hazırlanmıştır. Projenin müfredat ve içerik tasarımı **Sadık Turan**'a aittir. Eğitime katkılarından dolayı kendisine teşekkür ederim.
