# SampleDotNet
.NET forum web app
# TODO
- Sub Communities: Użytkownicy mogą tworzyć lub dołączać do subcommunities, które mogą być powiązane z wieloma kategoriami (relacja ManyToMany). Każde sub community mogłoby mieć własne zasady i moderację.
- Posty i komentarze: Użytkownicy mogą publikować treści oraz komentować w ramach subcommunities. Posty mogą być przypisane do wielu kategorii (relacja ManyToMany) i posiadają relację OneToMany z komentarzami.
- System autoryzacji i autentykacji: Zabezpieczenie dostępu do funkcji administracyjnych, moderacji treści i personalizacji za pomocą systemu ClaimsIdentity, z różnymi poziomami uprawnień (użytkownik, moderator, administrator).
- Zarządzanie plikami: Użytkownicy mogą przesyłać pliki multimedialne (obrazy, filmy) jako część postów lub komentarzy, z wykorzystaniem formularza HTML file i przechowywania na serwerze lokalnym lub w chmurze.
- OAuth2 dla autoryzacji zewnętrznej: Logowanie poprzez popularne serwisy społecznościowe i platformy, ułatwiające nowym użytkownikom dołączenie do społeczności.
- Wystawienie API: Dostęp do funkcjonalności platformy przez REST API, umożliwiający integrację z zewnętrznymi aplikacjami i automatyzację niektórych działań.
