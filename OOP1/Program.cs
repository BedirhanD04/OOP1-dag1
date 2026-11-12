using Microsoft.Extensions.DependencyInjection; // ServiceCollection için
using Backend;                                  // SearchHistory için
using OOP1;                                     // UserMenu için

var services = new ServiceCollection();         // DI konteyneri

// IWordSearchService kaydı SİLİNDİ, çünkü arama artık WordSearcher sınıflarında
services.AddSingleton<SearchHistory>();         // program boyunca tek bir geçmiş nesnesi
services.AddSingleton<UserMenu>();              // UserMenu, constructor'ında SearchHistory ister, DI verir

var provider = services.BuildServiceProvider(); // konteyneri kur (kayıtlar bundan ÖNCE olmalı)

var userMenu = provider.GetRequiredService<UserMenu>(); // UserMenu'yu iste
userMenu.Run();                                         // programı başlat


