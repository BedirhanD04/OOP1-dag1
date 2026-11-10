using Microsoft.Extensions.DependencyInjection; // ServiceCollection
using Backend; // IWordSearchService ve WordSearchService 
using OOP1;//UserMenu

// 1. Opsætning af Dependency Injection (DI)-containeren
var services = new ServiceCollection(); // DI konteyneri
services.AddSingleton<IWordSearchService, WordSearchService>(); // Giv WordSearchService til enhver, der anmoder om IWordSearchService
services.AddSingleton<SearchHistory>(); // Singleton: program boyunca TEK bir geçmiş nesnesi olsun
services.AddSingleton<UserMenu>(); // Lad også ConsoleUI blive oprettet af containeren

var provider = services.BuildServiceProvider(); // DI konteyneri oluşturuldu // Opret serviceprovideren, som kan oprette objekter baseret på registreringerne

var userMenu = provider.GetRequiredService<UserMenu>(); // DI konteynerinden UserMenu örneğini alıyoruz // Vi henter en instans af UserMenu fra DI-containeren

userMenu.Run();// programın akışı artık usermenu 'de


