using System;
using System.Collections.Generic;
using System.Text;

namespace Z_P_B_1.Models.HardData
{
    public class FirstAidData
    {
        public static List<FirstAidDto> DataList = new List<FirstAidDto> {
            new FirstAidDto {
                Id=1,
                IsOther = false,
                Title="W przypadku omdlenia",
                Steps = " 1.Postaraj się złagodzić upadek \n 2.Oceń sytuacje i bezpieczeństwo swoje i poszkodowanego \n 3.Ułóż poszkodowanego na plecach " +
                "\n 4.Udrożnij drogi oddechowe i sprawdź czy poszkodowany oddycha" +
                "\n 5.Jeśli poszkodowany nie oddycha rozpocznij resuscytacje \n 5.Gdy oddycha zapewnij dostęp do świeżego powietrza i rozluźnij ubranie uciskające szyję" +
                "\n 6.Ułóź poszkodowanego w pozycji bezpiecznej \n 7.Kontroluj oddech poszkodowanego, oraz zadbaj by nie doszło do wychłodzenia/przegrzania \n 8.Gdy poszkodowany nie odzyskał przytomności po dłuższym czasie wezwij pogotowie " +
                "\n 9. Po odzyskaniu przytomności nie podawaj niczego do picia i jedzenia \n 10.Przeprowadź wywiad SAMPLE i kontroluj poszkodowanego jeszcze przez jakiś czas",
                TitlePhotoName="Omdlenie2.jpg",
                OtherInforamtion="Sprawdzenie czy poszkodowany oddycha: Wykorzystaj 3 zmysły, Skieruj wzrok na klatkę piersiową, ucho przyłóż do ust. Obserwuj przez 10 sekund powinny wystąpić 2 oddechy \n \n" +
                "Resuscytacja dorosłych i starszych dzieci: Uklęknij przy poszkodowanym. Połóż dłoń w dolnej połowie mostka, nadgarstek drugiej dłoni ułóż na grzbiecie położonej dłoni i spleć palce. Utrzymuj ramiona wyprostowane i wykonuj rytmicznie uciśnięcia." +
                " Uciśnięcia powinny mieć głębokość ok 5-6cm, częstotliwość 100-120 na minutę. Należy wykonać 30 uciśnięć i kolejno 2 wdechy. Powtarzać do przyjazdu służb lub odzyskania oddechu. \n" +
                "Resuscytacja u dzieci: Wykonuje się najpierw 5 wdechów gdy to nie pomoże wykonuje się, 15 uciśnięć kolejno 2 wdechy do odzyskania oddechu lub przyjadu slużb, u niemowląt wykonuje się resuscytacje 2 palcami, a u małych dzieci jedną ręką \n \n" +
                "Pozycja bezpieczna: Zdejmij okulary u poszkodowanego,udrożnij drogi oddechowe delikatnie prostując kark, wyprostuj mu nogi, kończynę górną po stronie której jesteś zegnij o 90 stopni w stawie barkowym i łokciowym dłoń ułóż ku górze. Dalsze ramie przełóż przez klatkę piersiową a grzbiet podłóż pod policzek." +
                "Kończynę dolną chwyć trochę powyżej kolana i pociągnij ku górze bez oderwania stopy od ziemi. Następnie pociągnij za nogę tak by poszkodowany obrócił się na bok przy którym jesteś. Kolejno kończynę dolną będącą u góry zegnij w stawie biodrowym i kolanowym pod kątem 90 stopni. Ponownie udrożnij drogi oddechowe odchylając głowę poszkodowanego ku tyłowi" +
                "Gdy pozycja będzie utrzymywana przez 30 minut, odwróć poszkodowanego na drugi bok"
            },
            new FirstAidDto {Id=2, IsOther = false,Title="Ciało obece w ranie",
            Steps="",
            TitlePhotoName="Przebicie.jpg"},
            new FirstAidDto {Id=3,IsOther = false, Title="Brak funkcji życiowych",
            Steps="",
            TitlePhotoName="ECD.jpg"},
            new FirstAidDto {Id=4,IsOther = false, Title="Zawał",
            Steps="",
            TitlePhotoName="Zawal.jpg"},
            new FirstAidDto {Id=5,IsOther = false, Title="Hipotermia",
            Steps="",
            TitlePhotoName="Hipotermia.jpg"},
            new FirstAidDto {Id=6,IsOther = false, Title="Zawartość apteczki",
            Steps="",
            TitlePhotoName="Apteczka.jpg"},
            new FirstAidDto {Id=7,IsOther = false, Title="Zakrztuszenia",
            Steps="",
            TitlePhotoName="Zakrztuszenie.jpg"},
            new FirstAidDto {Id=8,IsOther = true, Title="Pożar",
            Steps="1. W przypadku powstania pożaru wszyscy zobowiązani są podjąć działania w celu jego likwidacji:\n" +
                "zaalarmować niezwłocznie, przy użyciu wszystkich dostępnych środków osoby będące w strefie zagrożenia\n" +
                "wezwać straż pożarną.\n" +
                "2. Telefoniczne alarmowanie należy wykonać w następujący sposób: Po wybraniu numeru alarmowego straży pożarnej 998 i zgłoszeniu się dyżurnego spokojnie i wyraznie podaje się:\n" +
                "swoje imię i nazwisko, numer telefonu, z którego nadawana jest informacja o zdarzeniu\n" +
                "adres i nazwę obiektu,\n" +
                "co się pali, na którym piętrze,\n" +
                "czy jest zagrożenie dla życia i zdrowia ludzkiego.\n" +
                "po podaniu informacji nie odkładać słuchawki do chwili potwierdzenia przyjęcia zgłoszenia.\n" +
                "3. Przystąpić niezwłocznie, przy użyciu miejscowych środków gaśniczych do gaszenia pożaru i nieść pomoc osobom zagrożonym w przypadku koniecznym przystąpić do ewakuacji ludzi i mienia. Należy czynności te wykonać w taki sposób aby nie doszło do powstania paniki jaka może ogarnąć ludzi będących w zagrożeniu, które wywołuje u ludzi ogień i dym.\n" +
                "Do czasu przybycia straży pożarnej kierowanie akcją obejmuje kierownik zakładu pracy /właściciel obiektu/ lub osoba najbardziej energiczna i opanowana.",
            OtherInforamtion="Jak ewakuować ludzi i mienie\n" +
                "Do celów ewakuacji ludzi służą korytarze - poziome drogi ewakuacji i klatki schodowe - pionowe drogi ewakuacyjne z których istnieje możliwość bezpośredniego wyjścia na zewnątrz. Drogi i wyjścia ewakuacyjne oznakowane muszą być pożarniczymi tablicami informacyjnymi." +
                "Osoby opuszczające strefę zagrożenia kierują się do najbliższego wyjścia służącego celom ewakuacji zgodnie z oznakowaniem. Osoby ewakuowane muszą podporządkować się poleceniom ratowników to jest osobom prowadzącym ewakuację: strażacy, pracownikom służby zabezpieczenia obiektu.",
            TitlePhotoName="Pozar.jpg"},
            new FirstAidDto {Id=9,IsOther = true, Title="Powódź",
            Steps="",
            TitlePhotoName="Powodz.jpg"},
            new FirstAidDto {Id=10,IsOther = true, Title="Burza",
            Steps="",
            TitlePhotoName="Burza.jpg"},
            new FirstAidDto {Id=11,IsOther = true, Title="Huragan",
            Steps="",
            TitlePhotoName="Tornado.jpg"},
            new FirstAidDto {Id=12,IsOther = true, Title="Wypadek drogowy",
            Steps="",
            TitlePhotoName="WypadekDrog.jpg"},
            new FirstAidDto {Id=13,IsOther = true, Title="Zgubienie się",
            Steps="",
            TitlePhotoName="Las.jpg"},
        };
    }
}
