using bookreview.Models;
using bookreview.Models.BaseModels;
using System;
using System.Linq;

namespace bookreview.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.Initialize(false);

            // Look for any students.
            if (context.Authors.Any())
            {
                return;   // DB has been seeded
            }

            var authors = new Author[]
            {
            new Author("Jane", "Austen", new DateTime(1775, 12, 16), "Angielska pisarka. Autorka powieści opisujących życie angielskiej klasy wyższej z początku XIX wieku."),
            new Author("Stanisław", "Lem", new DateTime(1921, 09, 12), "Polski pisarz science fiction, filozof i futurolog oraz krytyk. Jego twórczość porusza tematy takie jak: rozwój nauki i techniki, natura ludzka, możliwość porozumienia się istot inteligentnych czy miejsce człowieka we Wszechświecie. Dzieła Lema zawierają odniesienia do stanu współczesnego społeczeństwa, refleksje naukowo-filozoficzne na jego temat, a także krytykę ustroju socjalistycznego."),
            new Author("Andrzej", "Sapkowski", new DateTime(1948, 06, 21), "Polski pisarz fantasy, z wykształcenia ekonomista. Twórca postaci wiedźmina. Jest najczęściej po Lemie tłumaczonym polskim autorem fantastyki.")
            };
            foreach (Author a in authors)
            {
                context.Authors.Add(a);
            }
            context.SaveChanges();

            var categories = new Category[]
            {
                new Category("Literatura piękna"),
                new Category("Fantastyczno-naukowa"),
                new Category("Fantastyka")
            };
            foreach (Category c in categories)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();
            var books = new Book[]
            {
                new Book("Emma", authors[0], new DateTime(1915, 01, 01), "Jane Austen opisuje rozterki dystyngowanej kobiety żyjącej w Anglii okresu regencji."),
                new Book("Duma i uprzedzenie", authors[0], new DateTime(1813, 01, 01), "Autorka ukazuje życie angielskich wyższych sfer na przełomie XVIII i XIX wieku. Trafne obserwacje psychologiczne, humor i wątki romansowe sprawiły, że książka cieszy się wielką popularnością także współcześnie – w plebiscycie BBC z 2003 roku na ulubioną książkę brytyjskiego czytelnika (UK's Best-Loved Book) Duma i uprzedzenie zajęła drugie miejsce, zaraz po Władcy Pierścieni."),
                new Book("Rozważna i romantyczna", authors[0], new DateTime(1811, 01, 01), "Pierwsza opublikowana powieść angielskiej pisarki Jane Austen, wydana w 1811 roku. Rozważna i romatyczna jest najbardziej zbliżoną do romansu powieścią Austen, choć charakterystyczne dla tej pisarki trafne obserwacje ówczesnej obyczajowości, humor i interesujące portrety psychologiczne bohaterek sprawiły, że także ta książka cieszy się niesłabnącą popularnością również obecnie."),
                new Book("Eden", authors[1], new DateTime(1959, 01, 01), "W wyniku błędów w obliczeniach rakieta z grupą kosmonautów przymusowo ląduje na planecie Eden. Ludzie rozpoczynają naprawę wbitego w grunt statku, a także badanie planety, która okazuje się zamieszkana przez istoty rozumne."),
                new Book("Fiasko", authors[1], new DateTime(1986, 01, 01), "Początkowo akcja toczy się na Tytanie, księżycu Saturna, gdzie bohater Angus Parvis poszukując zaginionych budowniczych kopalni oraz komandora Pirxa, który ruszył im na ratunek, sam popełnia błąd w obcym człowiekowi, mieszkańcowi Ziemi, terenie i staje w obliczu nieuchronnej śmierci. W nadziei wskrzeszenia przy pomocy przyszłych zdobyczy nauki, dokonuje aktu szczególnego samobójstwa przez poddanie się hibernacji."),
                new Book("Solaris", authors[1], new DateTime(1961, 01, 01), "Główny bohater – psycholog Kris Kelvin – przybywa z Ziemi na stację badawczą unoszącą się nad cytoplazmatycznym oceanem pokrywającym obcą planetę Solaris. Ocean ten wydaje się być pewną formą inteligencji, o zdumiewających możliwościach interwencji w chaotyczny ruch orbitalny planety wewnątrz układu podwójnej gwiazdy. Ludzie od wielu lat nie potrafią zrozumieć tajemniczej natury oceanu; wszelkie próby porozumienia zawodzą."),
                new Book("Czas pogardy", authors[2], new DateTime(1995, 01, 01), ""),
                new Book("Wieża Jaskółki", authors[2], new DateTime(1997, 01, 01), ""),
                new Book("Pani Jeziora", authors[2], new DateTime(1999, 01, 01), ""),
            };
            books[0].AddCategory(categories[0]);
            books[1].AddCategory(categories[0]);
            books[2].AddCategory(categories[0]);
            books[3].AddCategory(categories[1]);
            books[4].AddCategory(categories[1]);
            books[5].AddCategory(categories[1]);
            books[6].AddCategory(categories[2]);
            books[7].AddCategory(categories[2]);
            books[8].AddCategory(categories[2]);
            foreach (Book b in books)
            {
                context.Books.Add(b);
            }
            context.SaveChanges();

        }
    }
}