using KsiazkaKucharska.Migrations;
using KsiazkaKucharska.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace KsiazkaKucharska.context
{
    public class KsiazkakucharskaInitializer: MigrateDatabaseToLatestVersion<KsiazkaContext, Configuration>
    {
        public static void SeedKsiazkaData(KsiazkaContext context)
        {
            var dania = new List<Models.Danieglowne>
            {
                new Models.Danieglowne() {Nazwa = "SAKIEWKI Z KURCZAKA Z KASZĄ JAGLANĄ I POMIDORAMI ", Skladniki = "2 pojedyncze piersi kurczaka, \n 1/2 szklanki ugotowanej kaszy jaglanej, \n 60 g sera pleśniowego (użyłam owczego, może być kozi ewentualnie krowi), \n 1 pomidor, \n 1 gałązka szczypiorku", Wykonanie ="Piersi kurczaka opłukać, osuszyć, oczyścić z błonek i kostek. Zrobić z nich sakiewki: położyć na desce, każdą pierś przekroić pionowo lub na ukos na 2 połówki (otrzymamy 4 porcje kurczaka). W każdej połówce, od góry, zrobić pionowe nacięcie wzdłuż filetu, rozchylić boki kurczaka i poszerzyć nieco powstałą sakiewkę robiąc boczne nacięcia również w środku kieszonki (aby zmieściło się więcej nadzienia). Można też nie kroić filetu na pół tylko zrobić jedną większą sakiewkę z jednego filetu. \n Mięso doprawić z każdej strony solą, pieprzem, kurkumą, ostrą papryką, następnie polać oliwą, skropić niewielką ilością soku z cytryny, wymieszać, odłożyć na ok. 30 minut a najlepiej na godzinę jeśli mamy czas. \n Nadzienie: ugotowaną kaszę jaglaną doprawić solą, pieprzem, skropić oliwą, dodać 2 łyżki posiekanego szczypiorku, 1 / 2 łyżeczki kurkumy, 1 / 3 łyżeczki ostrej papryki oraz ser kozi pokrojony w kosteczkę, wymieszać i nałożyć do kieszeni kurczaka. Na wierzchu położyć plaster pomidora, doprawić go solą, pieprzem, suszonym oregano i skropić odrobiną miodu lub syropu klonowego i oliwą extra.Ułożyć na blaszce do pieczenia, wstawić do piekarnika nagrzanego do 180 stopni C, piec przez 25 minut(małe sakiewki) oraz ok. 30 minut(duże).Podawać np.z sałatką. \n Sałata z awokado i brukselką: sałatę umyć, poszarpać, włożyć do salaterki, wymieszać z poszarpaną bazylią, dodać obrane i pokrojone w kosteczkę awokado, skropione sokiem z cytryny.Dodać obrane listki brukselki doprawione solą i pieprzem, posypać szczypiorkiem i polać winegretem(2 łyżki oliwy +2 łyżki soku z cytryny +2 łyżki miodu lub syropu klonowego + 1 łyżeczka musztardy +sól i pieprz) i posypać makiem.", Iloscporcji = 4, Ktododal = "admin", DanieglowneID=79},
                new Models.Danieglowne() {Nazwa = "DUSZONE ŻEBERKA W SOSIE WŁASNYM Z WARZYWAMI ", Skladniki = "1 kg żeberek, \n sól i pieprz, \n 1 łyżeczka mielone słodkiej papryki, \n 1/3 łyżeczki mielonej ostrej papryki, \n 1/4 szklanki mąki, \n 2 łyżki oleju roślinnego (np. rzepakowego), \n 1 cebula, \n 1 ząbek czosnku, \n 2 marchewki, \n 1 pietruszka, \n 1 mały seler korzeniowy, \n opcjonalnie - 1/2 batata, \n 2 łyżki sosu sojowego, \n 3 - 4 szklanki wody", Wykonanie = "Żeberka, umyć, pokroić na kawałki z jedną kostką. Doprawić solą i pieprzem. W misce wymieszać mąkę z mieloną papryką, wkładać żeberka i dokładnie obtoczyć je w mieszance. \n Szeroki, duży garnek rozgrzać na średnim ogniu, wlać 1 i 1/2 łyżki oleju, rozprowadzić go po całym dnie. Gdy tłuszcz będzie gorący, włożyć żeberka i dokładnie zrumienić raz przewracając (w sumie ok. 8 minut). Obsmażone żeberka wyjąć na talerz. \n Do garnka wlać pozostałe 1/2 łyżki oleju, włożyć pokrojoną w kosteczkę cebulę i czosnek, smażyć mieszając przez ok. 3 minuty. Dodać obrane i pokrojone w grubsze plasterki marchewkę, pietruszkę, selera. Doprawić do smaku solą i pieprzem, wymieszać. \n Do warzyw włożyć żeberka, wlać sos sojowy i gorącą wodę(do zakrycia składników). Przykryć i gotować przez ok. 1 i 1 / 2 godziny do 2 godzin, aż mięso będzie miękkie i odchodzące od kości. Obranego i pokrojonego w większą kostkę batata dodajemy na ok. 30 minut przed końcem gotowania potrawy.Jeśli na koniec sos nie jest jeszcze gęsty, można potrawę jeszcze pogotować bez przykrycia", Iloscporcji = 4, Ktododal = "admin", DanieglowneID =112},
            };

            dania.ForEach(d => context.Dania.AddOrUpdate(d));

            var zupy = new List<Models.Zupa>
            {
                new Models.Zupa() {Nazwa = "ZUPA Z SOCZEWICY Z ZIEMNIAKAMI I KURCZAKIEM ", Skladniki = "2 łyżki oliwy, \n 1 cebula, \n 1 marchewka, \n 2 litry bulionu,\n  1/2 szklanki \n czerwonej soczewicy, \n 3 ziemniaki, \n po 1/2 łyżeczki pieprzu,\n  kurkumy, papryki słodkiej i oregano, \n 1 pojedynczy filet z kurczaka, \n 2/3 szklanki przecieru pomidorowego", Wykonanie ="W garnku na oliwie zeszklić pokrojoną w kosteczkę cebulę. Dodać obraną i startą marchewkę i wymieszać, chwilę razem podsmażać. \n  Wlać bulion i zagotować. Dodać przepłukaną soczewicę oraz obrane i pokrojone w kostkę ziemniaki. Doprawić pieprzem, kurkumą, papryką i oregano oraz solą w razie potrzeby. Gotować przez ok. 3 minuty. \n  W gotującą się zupę włożyć pokrojoną w małą kosteczkę pierś kurczaka. Przykryć garnek i gotować przez ok. 8 minut. Wlać przecier pomidorowy i gotować jeszcze przez 3 - 5 minut. \n  Posypać zieleniną (np. szczypiorek, natka, koperek, rzeżucha) i podawać np. z pieczywem.", Iloscporcji = 4, Ktododal = "admin", ZupaID=77},
                new Models.Zupa() {Nazwa = "ZUPA BROKUŁOWA Z GORGONZOLĄ I CHIPSAMI Z JARMUŻU ", Skladniki = "1 łyżka oliwy extra vergine,\n 1 por, \n 2 ziemniaki, \n 1 łyżka masła, \n 1 litr bulionu, \n 1 brokuł, \n 80 ml śmietanki 30%, \n 100 g sera gorgonzola (lub innego pleśniowego), \n szczypiorek", Wykonanie ="W szerokim garnku na oliwie poddusić pokrojonego pora (biała i jasnozielona część), ok. 4 minuty. \n Gdy por będzie już miękki dodać obrane i pokrojone w kosteczkę ziemniaki oraz masło i smażyć razem przez ok. 2 minuty. Doprawić solą i pieprzem. \n Wlać gorący bulion i zagotować. Przykryć i gotować przez ok. 10 minut. Dodać pokrojonego w kostkę brokuła i gotować pod przykryciem przez ok. 5 minut, do miękkości warzyw. \n Wlać śmietankę, dodać ok. 1/4 ilości gorgonzoli, przelać do kielicha blendera i zmiksować na krem. Podawać z resztą sera pokrojonego w kosteczkę, z chipsami jarmużowymi oraz szczypiorkiem.", Iloscporcji = 4, Ktododal = "admin",ZupaID = 78},
            };

            zupy.ForEach(z => context.Zupy.AddOrUpdate(z));

            var przepisy = new List<Models.Przepis>
            {
                new Models.Przepis { Nazwa = "CIASTO MICHAŁEK ", Skladniki = "6 jajek (ogrzane), \n 3/4 szklanki cukru, \n1 szklanka mąki pszennej, \n 3 łyżki kakao, \n 1 łyżeczka proszku do pieczenia, \n 1 / 2 łyżeczki sody, \n 1 / 2 szklanki oleju, \n do nasączenia: 1 / 2 szklanki zaparzonej kawy +1 / 3 szklanki likieru lub innego alkoholu", Wykonanie ="Ogrzane jajka ubić z cukrem na puszystą i jasną masę (ok. 10 minut). W oddzielnej misce wymieszać mąkę z kakao, proszkiem do pieczenia i sodą. Przesiać przez sitko bezpośrednio do masy. Wlać olej i całość delikatnie zmiksować na jednolitą masę. \n Wyłożyć do formy o wymiarach ok. 22 x 33 cm wyścielonej papierem do pieczenia i wstawić do piekarnika nagrzanego do 180 stopni C.Piec do suchego patyczka przez ok. 20 minut.Wyjąć i ostudzić, następnie przekroić poziomo na 2 blaty.", Kalorie = 400, Ktododal = "admin", PrzepisID = 77},
                new Models.Przepis { Nazwa = "ŚWIĄTECZNE CIASTO MARCHEWKOWE ", Skladniki = "2 jajka,  \n 200 g cukru, \n 150 ml oleju roślinnego, \n 200 g marchewki, \n 50 g orzechów włoskich lub pekan, \n 1 pomarańcza, \n 50 g wiórków kokosowych, \n 200 g mąki, \n 1/2 łyżeczki proszku do pieczenia, \n 1 i 1 / 2 łyżeczki sody oczyszczonej, \n 2 łyżeczki przyprawy piernikowej", Wykonanie ="Jajka ocieplić w temperaturze pokojowej. Ubić je do podwojenia objętości, następnie stopniowo dodawać po łyżce cukier, cały czas ubijając, w sumie przez ok. 10 minut. \n Wciąż ubijając na wysokich obrotach miksera, dolewać ciągłym i cieniutkim strumieniem olej (można go wcześniej delikatnie podgrzać). \n Dodać obraną i drobno startą marchewkę, drobno startą skórkę z pomarańczy, obraną z reszty skórki pomarańczę pokrojoną w małą kosteczkę, posiekane orzechy, wiórki kokosowe i wszystko delikatnie wymieszać. Piekarnik nagrzać do 150 stopni C. \n Do osobnej miski przesiać mąkę, dodać proszek do pieczenia, sodę, przyprawę piernikową i szczyptę soli, dokładnie wymieszać. Przesypać do miski z marchewką i delikatnie wymieszać łyżką wszystkie składniki. \n Ciasto wyłożyć do formy o średnicy ok. 22 - 24 cm wyłożonej papierem do pieczenia.Piec przez 1 godzinę lub do suchego patyczka.", Kalorie = 350, Ktododal = "admin", PrzepisID =78},
            };

            przepisy.ForEach(p => context.Przepisy.AddOrUpdate(p));

            //var komentarze = new List<Models.Comment>
            //{
            //    new Models.Comment { CommentID = 1, DanieglowneID = 79, Komentarz = "Git", Nazwauzyt = "zosix@wp.pl"},
            //    new Models.Comment { CommentID = 1, DanieglowneID = 79, Komentarz = "czadowy przepisik", Nazwauzyt = "zosix@wp.pl"}
            //};

            //komentarze.ForEach(p => context.Komentarze.AddOrUpdate(p));
            context.SaveChanges();
        }
    }
}