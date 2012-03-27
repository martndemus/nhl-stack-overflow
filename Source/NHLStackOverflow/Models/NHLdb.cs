using System.Data.Entity;
using System.Collections.Generic;
using NHLStackOverflow.Classes;
using System;

namespace NHLStackOverflow.Models
{
    public class NHLdb : DbContext
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionTag> QuestionTags { get; set; }
        public DbSet<Read> Read { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UserMeta> UserMeta { get; set; }
        public DbSet<User> Users { get; set; }
    }

    public class NHLdbInitializer : DropCreateDatabaseAlways<NHLdb>
    {
        protected override void Seed(NHLdb context)
        {
            var users = new List<User>
            {
                new User { UserName = "Piet25", Password = Cryptography.PasswordHash("test"), Email = "test1@testmail.com", Rank = 3, Activated = 1 }, // admin allowed to view all!
                new User { UserName = "Klaas538", Password = Cryptography.PasswordHash("admin"), Email = "test2@testmail.com", Rank = 0, Activated = 1 },
                new User { UserName = "Kees1979", Password = Cryptography.PasswordHash("abc"), Email = "test3@testmail.com", Rank = 0, Activated = 1, PassLost = "BCLob11SgwNtcanBG3ZmayFehZfpBz67rIrNuPNO8xTHU4JJHZD1EKpSfXDnouNJeyK572UGaBpaSau+xjRfSw" }, // only person 
                new User { UserName = "HiteshPatel", Password = Cryptography.PasswordHash("Patel"), Email = "Patel@Hitesh.com", Rank = 1, Activated = 1 },  
                new User { UserName = "RajPAtel", Password = Cryptography.PasswordHash("PAtel"), Email = "info@Raj.nl", Rank = 2, Activated = 1 },  
                //new User { UserName = "user1217685", Password = PasswordHasher.Hash("user"), Email = "info@user.com", Rank = 0, Activated = 1 },  
                //new User { UserName = "Legycsapo", Password = PasswordHasher.Hash("Legy"), Email = "Legycsapo@gmail.com", Rank = 0, Activated = 1 },  
                //new User { UserName = "swiecki", Password = PasswordHasher.Hash("swiecki"), Email = "swiecki@hotmail.com", Rank = 3, Activated = 1 },  
                //new User { UserName = "MattBeckman", Password = PasswordHasher.Hash("Matt"), Email = "Matt@Beckman.com", Rank = 0, Activated = 1 },  
                //new User { UserName = "Benn", Password = PasswordHasher.Hash("Benn"), Email = "Benn@hi.com", Rank = 0, Activated = 1 },  
                //new User { UserName = "Kumar", Password = PasswordHasher.Hash("Ku"), Email = "Kumar@gmail.com", Rank = 2, Activated = 1 },  
                //new User { UserName = "ntziolis", Password = PasswordHasher.Hash("ntz"), Email = "ntziolis@hotmail.com", Rank = 0, Activated = 1 },  
                //new User { UserName = "ottel142", Password = PasswordHasher.Hash("142"), Email = "ottel142@gmail.com", Rank = 1, Activated = 1 },  
                //new User { UserName = "Mick", Password = PasswordHasher.Hash("Litjens"), Email = "Mick@gmail.com", Rank = 3, Activated = 1 },  
                //new User { UserName = "Benn", Password = PasswordHasher.Hash("Benn"), Email = "Benn@hi.com", Rank = 2, Activated = 1 } 
};

            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

            var usermeta = new List<UserMeta>
            {
                new UserMeta { UserId = 1 },
                new UserMeta { UserId = 2 },
                new UserMeta { UserId = 3 },
                new UserMeta { UserId = 4 },
                new UserMeta { UserId = 5 },
                //new UserMeta { UserId = 6 },
                //new UserMeta { UserId = 7 },
                //new UserMeta { UserId = 8 },
                //new UserMeta { UserId = 9 },
                //new UserMeta { UserId = 10 },
                //new UserMeta { UserId = 11 },
                //new UserMeta { UserId = 12 },
                //new UserMeta { UserId = 13 },
                //new UserMeta { UserId = 14 }
            };

            usermeta.ForEach(s => context.UserMeta.Add(s));
            context.SaveChanges();

            var tags = new List<Tag>
            {
                new Tag { Name = "C#", Description = "C# (Engels uitgesproken als \"C sharp\") is een objectgeoriënteerde programmeertaal ontwikkeld door Microsoft als deel van het .NET-initiatief, en later geaccepteerd als standaard door ECMA (ECMA-334) en ISO (ISO/IEC 23270). C# is objectgeoriënteerd en lijkt qua syntaxis en semantiek sterk op Java, terwijl vooral in de bibliotheken en programmeeromgeving een sterke invloed van Object Pascal en Delphi te zien is.", Count = 1 },
                //new Tag { Name = "JavaScript", Description = "JavaScript is een scripttaal die veel gebruikt wordt om webpagina's interactief te maken en webapplicaties te ontwikkelen. De syntaxis van JavaScript vertoont overeenkomsten met de programmeertaal Java. Omdat beide talen het meest zichtbaar zijn op en rond de browser, maar vooral door de naamgeving, worden ze vaak met elkaar verward. ", Count = 0 },
                //new Tag { Name = "C++", Description = "C++ (uitgesproken als C plus plus) is een programmeertaal gebaseerd op C. In tegenstelling tot C is C++ een multi-paradigmataal, wat inhoudt dat er verschillende programmeerparadigma's gebruikt kunnen worden. De taal is ontworpen door Bjarne Stroustrup voor AT&T Labs, als verbetering van C. De naam is afkomstig van de programma-opdracht \"C++\", wat betekent: verhoog de waarde van de variabele C met 1.", Count = 0},
                //new Tag { Name = "Python", Description = "Python gebruikt als een van de weinige talen de mate van 'inspringing' van de regel, ook wel: indentatie genoemd, als indicatie van gelaagdheid van de verschillende onderdelen van het programma. Dit is iets wat Jaap van Ganswijk, de ontwerper van JPL en UHL al sinds het begin van de jaren tachtig voorstaat, maar dat ook anderen wel geopperd hebben.", Count = 0},
                //new Tag { Name = "Ruby", Description = "Ruby is een programmeertaal, die doorgaans wordt geïnterpreteerd. De taal is ontworpen om snel en makkelijk objectgeoriënteerd te programmeren. Het heeft verschillende mogelijkheden om tekstbestanden te verwerken en kan ook systeemtaken aan.", Count = 0},
                //new Tag { Name = "Smalltalk", Description = "Smalltalk is een objectgeoriënteerde programmeertaal met dynamische typen, die ontwikkeld werd bij Xerox PARC door Alan Kay, Dan Ingalls, Ted Kaehler, Adele Goldberg en anderen in de jaren zeventig. De taal werd oorspronkelijk uitgebracht als Smalltalk-80 en wordt sindsdien in brede kring gebruikt.", Count = 0},
                new Tag { Name = "ASP.NET", Description = "ASP.NET is een manier om op een webserver webpagina's aan te maken met behulp van programmacode. Hiermee kunnen vaste HTML-codes gecombineerd worden met variabele inhoud die door een programma wordt geproduceerd. Hierdoor kunnen websites met een dynamisch karakter worden gemaakt. Hiermee worden geen (interactieve) animaties bedoeld, maar websites die aan de hand van gebruikeracties verschillende gegevens weergeven.", Count = 4},
                new Tag { Name = "MVC", Description = "Model-view-controller (of MVC) is een ontwerppatroon (\"design pattern\") dat het ontwerp van complexe toepassingen opdeelt in drie eenheden met verschillende verantwoordelijkheden: datamodel (model), datapresentatie (view) en applicatielogica (controller). Het scheiden van deze verantwoordelijkheden bevordert de leesbaarheid en herbruikbaarheid van code.", Count = 2},
                new Tag { Name = "UnitTest", Description = "Unittesten is een methode om softwaremodulen of stukjes broncode (units) afzonderlijk te testen. Bij unittesten zal voor iedere unit een of meerdere tests ontwikkeld worden. Hierbij worden dan verschillende testcases doorlopen. In het ideale geval zijn alle testcases onafhankelijk van andere tests. Eventueel worden hiertoe zogenaamde mockobjecten gemaakt om de unittests gescheiden uit te kunnen voeren.", Count = 1},
                //new Tag { Name = "PHP", Description = "PHP (PHP: Hypertext Preprocessor) is een scripttaal, die bedoeld is om op webservers dynamische webpagina's te creëren. PHP is in 1994 ontworpen door Rasmus Lerdorf, een senior software engineer bij IBM. Lerdorf gebruikte Perl als inspiratie. Aanvankelijk stonden de letters PHP voor Personal Home Page (de volledige naam was Personal Home Page/Forms Interpreter, PHP/FI). Sinds PHP 3.0 is de betekenis een recursief acroniem geworden: PHP: Hypertext Preprocessor. ", Count = 0},
                new Tag { Name = "HTML", Description = "HyperText Markup Language (afgekort HTML) is een opmaaktaal voor de specificatie van documenten, voornamelijk bedoeld voor het World Wide Web.", Count = 1},
                new Tag { Name = "Objective-C", Description = "Objective-C is, in tegenstelling tot C++, een superset van C, wat inhoudt dat alles wat in C voorkomt ook geldig is in Objective-C. Het voegt echter (net als C++) de mogelijkheid toe tot object-georiënteerd programmeren, door de toevoeging van klassen. Een (instantie van een) klasse kan een zogeheten message gestuurd worden, die een methode (een stuk code) aanroept", Count = 1},
                new Tag { Name = "iOS", Description = "Het mobiele besturingssysteem iOS (voor juni 2010 iPhone OS) is het besturingssysteem van de iPhone, iPad, iPod touch en Apple TV. Het wordt ontwikkeld door Apple Inc.. Het besturingssysteem stond op 26 % van alle verkochte smartphones in het laatste kwartaal van 2010.[2] Het was hiermee het op twee na meest verkochte smartphone-besturingssysteem, na Google Android en Symbian van Nokia.", Count = 1},
                //new Tag { Name = "CSS", Description = "Cascading Style Sheets (afgekort tot CSS), stijlbladen, zijn een mogelijkheid om de vormgeving van webpagina's los te koppelen van hun feitelijke inhoud en centraal vast te leggen. Het Engelse \"style\" heeft de betekenis van \"opmaak\", niet van schrijfstijl. Het begrip \"cascading\" (als een waterval) verwijst naar de mogelijkheid van het vererven van opmaak-eigenschappen", Count = 0},
                //new Tag { Name = "F#", Description = "Net als bij de andere .NET-talen (zoals C#, VB.NET en J#) zijn de .NET-bibliotheken gewoon beschikbaar. Een CTP voor Visual Studio zorgt ervoor dat je F#-applicaties kunt schrijven in een voor veel programmeurs bekende interface, en dat de code die je schrijft direct gecontroleerd wordt op fouten. F# is ontworpen door Don Syme in de labs van Microsoft. De kern van de taal is ongeveer gelijk aan die van de Caml-programmeertaal, maar F# heeft zelf ook een standaard library, die ontworpen is om compatibel te zijn met de OCaml-library.", Count = 0},
                //new Tag { Name = "Visual Basic", Description = "Visual Basic (VB) is de naam van een reeks programmeeromgevingen, later programmeertalen, uitgebracht door Microsoft. Het doel van Visual Basic is de ondersteuning van het bouwen van grafische applicaties op een visuele manier, dat wil zeggen, zo veel mogelijk via directe grafische manipulatie van elementen in plaats van het expliciet invoeren van programmacode.", Count = 0},
                //new Tag { Name = "Delphi", Description = "Delphi is de ontwikkelomgeving voor de objectgeoriënteerde programmeertaal Pascal. Borland breidde na versie 5.5 van Turbo Pascal de taal uit met objecten (Turbo Pascal 6) en maakte een versie voor Windows, die echter niet aan de eigen verwachtingen voldeed. Het kocht Object Pascal van Apple, dat op een andere taal overstapte, en ontwikkelde dat verder voor het Windows-platform. ", Count = 0},
                //new Tag { Name = "ActionScript", Description = "Actionscript is de scripttaal of programmeertaal van Adobe Flash en Adobe Flex. Deze taal wordt gebruikt om animaties of filmpjes interactief te maken. Er kunnen ook volledige spelletjes of geavanceerde applicaties mee worden gemaakt, waarbij er bijvoorbeeld interactie optreedt met de muis en het toetsenbord. Actionscript is een event-based taal, wat wil zeggen dat alle acties getriggerd worden door events (gebeurtenissen).", Count = 0},
                //new Tag { Name = "Java", Description = "Java is een objectgeoriënteerde programmeertaal. Historisch gezien is Java een platformonafhankelijke taal die qua syntaxis grotendeels gebaseerd is op de (eveneens objectgeoriënteerde) programmeertaal C++. Java beschikt echter over een uitgebreidere klassenbibliotheek dan C++", Count = 0}
            };

            tags.ForEach(s => context.Tags.Add(s) );
            context.SaveChanges();

            var read = new List<Read>
            {
                new Read { UserId = 1, QuestionId = 1},
                new Read { UserId = 2, QuestionId = 2},
                new Read { UserId = 3, QuestionId = 3}
            };

            read.ForEach(s => context.Read.Add(s));
            context.SaveChanges();

            var question = new List<Question>
            {
                new Question { UserId = 2, Title = "Hoe sluit ik een form met C#?", Content = "Hoi, \n\n Ik heb een windows form applicatie gemaakt. Alleen is het mij niet duidelijk hoe ik een form sluit door middel van code. \n\nZou iemand dit aan mij kunnen vertellen? Danku!", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 2, 13, 8, 5, 10)), Answers = 1, Views = 14 },
                new Question { UserId = 3, Title = "Hoe maak ik een form met ASP.NET in combinatie met MVC?", Content = "Ik maak gebruik van de razor syntax. Ik krijg alleen niet voor elkaar dat hij de validatie errors terug geeft. Ik van alles geprobeerd :-S.\n\nKan iemand mij hier mee helpen? Het zou wel erg fijn zijn.", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 3, 19, 8, 5, 10)), Answers = 1, Views = 21 },
                new Question { UserId = 3, Title = "Mijn ASP.NET blijft steeds crashen met de UnitTests", Content = "Op het moment dat ik de null tests draai (die kijken of het object goed geinistaliseerd word (dus eigenlijk naar null)). Failen ze allemaal. Het word namelijk gelijk al op 0 gezet als standaard terwijl dit niet de bedoeling was. \n\nHoe kan ik dit verhelpen?", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 1, 10, 8, 5, 10)), Flag = 1,Views = 31, Answers = 2 , Votes = 23},
                new Question { UserId = 4, Title = "Hoe voer ik goede HTML sanitize uit met ASP.NET", Content = "Ik maak gebruik van de razor syntax. Het is de bedoeling om alle HTML tags te escappen of iets anders zodat er niets mee gedaan kan worden door kwaadwillende.\n\nOp het moment worden er alleen de standaard dingen uit gehaald. Ik wil het alleen helemaal schoon hebben. \n\nHelp!", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 3, 11, 8, 5, 10)), Views = 31, Votes = 3, Answers = 1 },
                new Question { UserId = 1, Title = "Waarom maakt ASP.NET mijn code block verkeerd?", Content = "Ik ben begonnen met een leeg project en heb nu al bijna StackOverFlow na gemaakt voor het project voor school. Het is alleen zo dat de code element onder elkaar kruipen. \n\nWeet iemand hoe dit komt?", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 3, 8, 8, 5, 10)), Views = 26 },
                new Question { UserId = 1, Title = "Advies over horizontaal scrollen met iOS", Content = "Ik heb een applicatie gemaakt voor iOS. Alleen ik wou graag wat advies over het horizontale scrollen. Ik heb geen idee hoe ik dit op een nette manier kan implementeren. Hoe zouden jullie zoiets doen?", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 1, 8, 8, 5, 10)), Views = 7 },
           };

            question.ForEach(s => context.Questions.Add(s));
            context.SaveChanges();

            var questiontag = new List<QuestionTag>
            {
                new QuestionTag { QuestionId = 1, TagId = 1 },
                new QuestionTag { QuestionId = 2, TagId = 2 },
                new QuestionTag { QuestionId = 2, TagId = 3 },
                new QuestionTag { QuestionId = 3, TagId = 2 },
                new QuestionTag { QuestionId = 3, TagId = 4 },
                new QuestionTag { QuestionId = 3, TagId = 3 },
                new QuestionTag { QuestionId = 4, TagId = 2 },
                new QuestionTag { QuestionId = 4, TagId = 5 },
                new QuestionTag { QuestionId = 5, TagId = 2 },
                new QuestionTag { QuestionId = 6, TagId = 6 },
                new QuestionTag { QuestionId = 6, TagId = 7 },
            };

            questiontag.ForEach(s => context.QuestionTags.Add(s));
            context.SaveChanges();

            var option = new List<Option>
            {
                new Option { Name = "pietpaal", Value = "een"},
                new Option { Name = "henksen", Value = "twee"},
                new Option { Name = "jantjes", Value = "drie"}
            };

            option.ForEach(s => context.Options.Add(s));
            context.SaveChanges();

            var message = new List<Message>
            {
                new Message { SenderId = 1, ReceiverId = 1, Title = "1Hello world! Test title.", Content = "1Hello, Could you awnser this for me. Why do you keep generating in bugs? It would be a lot easier if you didn't :<. Bai."},
                new Message { SenderId = 2, ReceiverId = 2, QuestionId = 2, Title = "2Hello world! Test title.", Content = "2Hello, Could you awnser this for me. Why do you keep generating in bugs? It would be a lot easier if you didn't :<. Bai."},
                new Message { SenderId = 3, ReceiverId = 3, QuestionId = 3, Title = "3Hello world! Test title.", Content = "3Hello, Could you awnser this for me. Why do you keep generating in bugs? It would be a lot easier if you didn't :<. Bai."}
            };

            message.ForEach(s => context.Messages.Add(s));
            context.SaveChanges();

            var favorite = new List<Favorite>
            {
                new Favorite { UserId = 1, QuestionId = 1},
                new Favorite { UserId = 2, QuestionId = 2},
                new Favorite { UserId = 3, QuestionId = 3}
            };

            favorite.ForEach(s => context.Favorites.Add(s));
            context.SaveChanges();

            var comment = new List<Comment>
            {
                new Comment { UserId = 5, QuestionId = 1, Content = "Is dat niet gewoon simpel weg this.Close();?", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 3, 19, 18, 5, 10)), Flag = 1},
                new Comment { UserId = 2, QuestionId = 1, Content = "Owja dat zou best wel eens kunnen ja. Zal het even proberen danku!", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 3, 19, 20, 5, 10))},
                new Comment { UserId = 2, AnswerId = 1, Content = "Ja dat werkte :D, danku! Nu kan ik weer verder met mijn lingo opdracht.", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 3, 19, 21, 35, 10))},
                new Comment { UserId = 3, AnswerId = 2, Content = "Ah volgens mij snap ik het. Zal het even proberen.", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 3, 19, 21, 35, 10))},
                new Comment { UserId = 3, AnswerId = 2, Content = "Het werkte. Danku!", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 3, 19, 21, 40, 10))},
                new Comment { UserId = 3, AnswerId = 3, Content = "Ach ja. Dat is ook zo. Stom van me. In iedergeval bedankt. Je hebt ons StackOverFlow project gered.", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 1, 10, 21, 40, 10))},
                new Comment { UserId = 2, QuestionId = 3, Content = "Ja of door de tests ed. te herschrijven dat ze naar die null testen.", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 1, 10, 21, 40, 10))},
                new Comment { UserId = 1, AnswerId = 4, Content = "Dat zou inderdaad nog netter zijn.", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 1, 10, 22, 40, 10))},
                new Comment { UserId = 5, QuestionId = 4, Content = "Welke dingen heb je er nu dan al uit gehaald? En hoe?", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 3, 11, 22, 40, 10))},
                new Comment { UserId = 4, AnswerId = 5, Content = "Maar je hebt ook nog bepaalde properties die je mee kunt geven aan tags. Hoe test ik daar dan op? En hoe blijven niet slechte dingen tussen <> tags beschermt?", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 3, 11, 14, 40, 10))},
// Add more comments :<
            };

            comment.ForEach(s => context.Comments.Add(s));
            context.SaveChanges();

            var badge = new List<Badge>
            {
                new Badge { UserId = 1, Name = "goud" },
                new Badge { UserId = 2, Name = "zilver" },
                new Badge { UserId = 3, Name = "brons" }
            };

            badge.ForEach(s => context.Badges.Add(s));
            context.SaveChanges();

            var answer = new List<Answer>
            {
                new Answer { UserId = 3, QuestionId = 1, Content = "Hoi,\n\nDe makkelijkste manier om zoiets te doen is simpelweg de huidige form te sluiten. Meestal als je in je form werkt (bijvoorbeeld onClick) en je doet dan this.close(); Dan zou het huidige open form moeten sluiten.\n\nLukt dat niet dan hoor ik het graag.",Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 3, 19, 21, 5, 10))},
                new Answer { UserId = 4, QuestionId = 2, Content = "Hmm\n\nMet MVC zal je eerst je form moeten starten. Dit doe je door middel van: @using (Html.BeginForm()){\n\n}. Daartussen zet je vervolgens @Html.ValidationSummary(true). Vervolgens kan je bijvoorbeeld een textbox maken door middel van: @Html.TextBox( <proppertyName> , <beginValue> }). \n\nHopelijk heb ik het zo duidelijk genoeg uitgelegd. Ik hoor het anders wel.",Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 3, 19, 21, 5, 10)), Votes = 10},
                new Answer { UserId = 1, QuestionId = 3, Content = "Hoi,  \n\nDit komt doordat int's altijd worden geinitialiseerd naar 0 in plaats van naar null. Een heel erg simpele oplossing hiervoor is om een ? bij de int te zetten dus: int? <property>. Hierdoor word de int geinitaliseerd naar null ;-).", Flag = 1, Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 1, 10, 20, 5, 10))},
                new Answer { UserId = 4, QuestionId = 3, Content = "Wat ook een mogelijke optie is is het veranderen van de tests. Het is namelijk zo dat een database niet zero indexed is. Dus daarom zal er ook een getal hoger dan 0 aanwwezig moeten zijn. En daar zou je dan vervolgens ook op kunnen testen.", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 1, 10, 22, 5, 10))},
                new Answer { UserId = 2, QuestionId = 4, Content = "Wat ik gedaan heb is kijken naar de sanitize die google gebruikt en aan de hand daarvan mijn eigen geschreven. De simpelste manier is door middel van een regular expression te zoeken naar een keyword tussen de <> tags en die er vervolgens uit te slopen", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 3, 11, 14, 5, 10))},
                //new Answer { UserId = 1, QuestionId = 3, Content = "Hello, Here is my fabilous awnser to your somewhat silly question. I've never had problems with this kind of things. I always do the thingy first and afterwards I do the other thingy. Result: Profit. Kthxbai.", Created_At = StringToDateTime.ToUnixTimeStamp(new DateTime(2012, 3, 15, 8, 5, 10))}
            };

            answer.ForEach(s => context.Answers.Add(s));
            context.SaveChanges();
        }
    }
}