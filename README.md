# 🚀 Clean Architecture Setup 2025 - Öğrenme Projesi

Bu proje, modern yazılım geliştirme prensiplerini ve .NET ekosistemindeki güncel yaklaşımları öğrenmek amacıyla geliştirilmiş bir **Clean Architecture** (Temiz Mimari) uygulamasıdır. Proje, Taner Saydam'ın "Sıfırdan Clean Architecture Setup 2025" eğitim serisi referans alınarak adım adım inşa edilmiştir.

## 🏗 Mimarî Yapı

Aşağıdaki diyagram, projenin temel mimari yapısını ve bağımlılıkların içe doğru (Core/Domain katmanına) olan akışını görselleştirmektedir. Kurumsal seviyedeki projeler için bu yapı, kodun bakımını, test edilebilirliğini ve sürdürülebilirliğini kolaylaştırmaktadır.

## 🛠 Kullanılan Teknolojiler ve Kütüphaneler

Projeyi geliştirirken **.NET 10** ve sektör standartlarına uygun güncel kütüphaneler tercih edilmiştir:

* **.NET 10** (ASP.NET Core Web API)
* **.NET Aspire** (Uygulama orkestrasyonu, OpenTelemetry destekli observability ve dağıtık konfigürasyon yönetimi için)
* **Entity Framework Core** (ORM Aracı)
* **MediatR** (CQRS pattern uygulaması için)
* **FluentValidation** (Veri doğrulama / Validation işlemleri için)
* **Mapster** (Nesneler arası veri transferi / Object Mapping için)
* **Scrutor** (Dependency Injection kayıt işlemlerini otomatize etmek için)
* **OData** (Dinamik ve esnek veri listeleme/sorgulama işlemleri için)
* **Keycloak** (Merkezi kimlik doğrulama, yetkilendirme ve IAM - Identity and Access Management için)
* **MS SQL Server** (Veritabanı olarak)
* **Scalar / OpenAPI** (Modern API dokümantasyonu ve testi için)

## 🏗 Katman Detayları

Proje 4 temel katmandan oluşmaktadır:

1. **Domain (Çekirdek Katman):**
   * Veritabanı tablolarına karşılık gelen Entity sınıfları, Enum'lar ve Value Object'ler (Örn: Adres bilgileri) bulunur.
   * `Auditable Entity` yapısı kullanılarak Oluşturulma (CreateDate), Güncellenme (UpdateDate) ve Silinme (DeleteDate) kayıtları merkezileştirilmiştir.
   * **Domain Events** (Alan olayları) bu katmanda tetiklenmek üzere tanımlanır.
   * Hiçbir dış kütüphaneye bağımlılığı yoktur.

2. **Application (Uygulama Katmanı):**
   * İş kurallarının (Business Rules) ve Use-Case'lerin bulunduğu katmandır.
   * **CQRS** mantığı ile `Commands` (Ekle, Güncelle, Sil) ve `Queries` (Listele, Getir) olarak ayrılmıştır.
   * Validasyon kuralları (`FluentValidation` ile) ve arayüzler (Interfaces) burada tanımlanır. Yalnızca Domain katmanına bağımlıdır.

3. **Infrastructure (Altyapı Katmanı):**
   * Veritabanı bağlantısı (`DbContext`), Entity konfigürasyonları (Fluent API) ve dış servis entegrasyonları burada yer alır.
   * Generic Repository Pattern uygulamaları ve Entity Framework Core işlemleri bu katmanda gerçekleştirilir.
   * **Keycloak** üzerinden sağlanan Authentication (Kimlik Doğrulama) konfigürasyonları ve token doğrulama işlemleri burada izole edilmiştir.

4. **Web API (Sunum Katmanı):**
   * İstemcilerin (Frontend, Mobil vb.) istek attığı katmandır.
   * Performans odaklı olması için yazma (Command) işlemlerinde **Minimal API**, okuma (Query) işlemlerinde ise **OData Controller** yapıları bir arada kullanılmıştır.
   * Global Exception Handling (Merkezi Hata Yönetimi) mekanizması kurularak hatalar standardize edilmiştir (`Result Pattern` ile dönülmüştür).

## ⚙️ Tasarım Desenleri ve Yaklaşımlar

* **CQRS (Command Query Responsibility Segregation):** Okuma ve yazma işlemlerinin birbirinden izole edilmesi.
* **Repository & Unit of Work Pattern:** Veritabanı işlemlerinin soyutlanarak tek bir merkezden yönetilmesi ve transaction işlemlerinin güvene alınması.
* **Domain Events:** Sistem içindeki durum değişikliklerini asenkron bir şekilde diğer parçalara haber vererek iş süreçlerinin tutarlılığını sağlamak.
* **Outbox Pattern:** Veritabanı transaction'ları ile dış sistemlere iletilecek işlemlerin tutarlılığını garanti altına alan yapı.
* **Result Pattern:** API cevaplarının standart bir yapı (Başarılı/Başarısız, Mesaj, Veri Listesi) üzerinden dönülmesi.
* **Soft Delete:** Verilerin fiziksel olarak veritabanından uçurulması yerine silinme tarihinin işaretlenerek saklanması mantığı.
