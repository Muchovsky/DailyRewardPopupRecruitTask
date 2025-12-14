# DailyRewardPopupRecruitTask
Stworzenie prostego elementu funkcjonalnego używanego w grach mobilnych – okna Daily Reward z krótką progresją.

# Prewarunki
Windows 11 PL

Unity 6.0 (6000.0.62f1)

Unity Package 2D, Mobile

TextMeshPro

Rozdzielczość docelowa 1920x1080

Platforma docelowa Android

Pliki graficzne UI Pack - Adventure dostępne https://kenney.nl/

# Instukcja

Należy włączyć scene UI. 

Po uruchomieniu gry u góry ekranu widoczne są aktualne wartości poszczególnych walut: złota, kryształów i kluczy.

W centralnej części znajdują się przyciski do pokazywania poszczególnych kalendarzy:

* Weekly Calendar - 7 dniowy odmierzający czas od pierwszego uruchomienia gry.
* Monthly Calendar - 30 dniowy odmierzający czas od pierwszego uruchomienia gry. 
* Advent Calendar - 24 dniowy z datą rozpoczęcia 01-12-2025. Istnieje możliwość odbierania poprzednich dni.
* Advent Calendar Hard - 24 dniowy z datą rozpoczęcia 01-12-2025. Zablokowana jest możliwość odbierania poprzednich dni.

Po uruchomieniu kalendarza podświetlone zostają możliwe do odebrania nagrody.
Nagrody odbierane są po naciśnięciu na konkretny dzień.

W prawym dolnym rogu okna kalendarza umieszczone są dwa przyciski:
* Simulate Next Day - symuluje upływ dnia, dając możliwość odebrania kolejnej nagrody.
* RestoreCalendar - przywraca kalendarz do stanu właściwego 

Przyciski te widoczne są jeśli gra jest uruchomiona w Edytorze lub jeśli został ustawiony Scripting Define Symbol TEST_BUILD

Odebrane nagrody nie mogą, zostać odebrane ponownie i zostają oznaczone grafiką ✓.

Resetowanie danych zapisu jest możliwe poprzez wyczyszczenie PlayerPrefs 
* w Edytorze Unity należy wybrać Edit -> Clear All PlayerPrefs
* na telefonie z androidem poprzez czyszczenie danych aplikacji  Ustawienia -> Aplikacje  -> wybrać apliakcje z listy -> Pamięć i pamięć podręczna -> Wyczyść dane

#
