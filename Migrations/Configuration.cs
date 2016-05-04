namespace testfan2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Collections.Generic;
    using System.Linq;
    using testfan2.Models;


    internal sealed class Configuration : DbMigrationsConfiguration<testfan2.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            // ContextKey = "testfan2.DAL.ApplicationDbContext";
        }

        protected override void Seed(testfan2.Models.ApplicationDbContext context)
        {


            var nations = new List<NationTeam>
            {
                new NationTeam {NationCode = "IRE", Nation = Nation.Ireland, TeamGoalWeight = TeamGoalWeight.C, Players = new List<Player> (),
                    Fixtures = new List<Fixture> () },
                       new NationTeam {NationCode = "SWE", Nation = Nation.Sweden, TeamGoalWeight = TeamGoalWeight.C, Players = new List<Player> (),
                    Fixtures = new List<Fixture> () },
            new NationTeam {NationCode = "ITA", Nation = Nation.Italy, TeamGoalWeight = TeamGoalWeight.B, Players = new List<Player> (),
                    Fixtures = new List<Fixture> () },
                       new NationTeam {NationCode = "BEL", Nation = Nation.Belgium, TeamGoalWeight = TeamGoalWeight.A, Players = new List<Player> (),
                    Fixtures = new List<Fixture> ()
                       },
 new NationTeam {NationCode = "ENG", Nation = Nation.England, TeamGoalWeight = TeamGoalWeight.A, Players = new List<Player> (),
                    Fixtures = new List<Fixture> () },
                       new NationTeam {NationCode = "WAL", Nation = Nation.Wales, TeamGoalWeight = TeamGoalWeight.C, Players = new List<Player> (),
                    Fixtures = new List<Fixture> () },
            new NationTeam {NationCode = "SVK", Nation = Nation.Slovakia, TeamGoalWeight = TeamGoalWeight.D, Players = new List<Player> (),
                    Fixtures = new List<Fixture> () },
                       new NationTeam {NationCode = "RUS", Nation = Nation.Russia, TeamGoalWeight = TeamGoalWeight.C, Players = new List<Player> (),
                    Fixtures = new List<Fixture> () }
            };
            nations.ForEach(n => context.NationTeams.AddOrUpdate(n));
            context.SaveChanges();
            var players = new List<Player>
           {
               new Player {PlayerFirstname = "Shay", PlayerSurname = "Given", DateOfBirth = DateTime.Parse("1982-03-10"), PlayerValue = 4.5, Position = Position.GoalKeeper, NationCode = "IRE", PlayerID = 001, GoalWeight = .002},
                new Player {PlayerFirstname = "Seamus", PlayerSurname = "Coleman", DateOfBirth = DateTime.Parse("1984-04-10"), PlayerValue = 4.5, Position = Position.Defender, NationCode = "IRE", PlayerID = 002, GoalWeight= .07},
                 new Player {PlayerFirstname = "John", PlayerSurname = "O'Shea", DateOfBirth = DateTime.Parse("1984-08-10"), PlayerValue = 4.4, Position = Position.Defender, NationCode = "IRE", PlayerID = 003, GoalWeight = .07 },
                  new Player {PlayerFirstname = "Ciaran", PlayerSurname = "Clarke", DateOfBirth = DateTime.Parse("1988-04-10"), PlayerValue = 4.5, Position = Position.Defender, NationCode = "IRE", PlayerID = 004, GoalWeight = .07 },
                   new Player {PlayerFirstname = "James", PlayerSurname = "McClean", DateOfBirth = DateTime.Parse("1987-04-10"), PlayerValue = 4.3, Position = Position.Defender, NationCode = "IRE", PlayerID = 005, GoalWeight = .06 },
                 new Player {PlayerFirstname = "Robbie", PlayerSurname = "Brady", DateOfBirth = DateTime.Parse("1987-03-10"), PlayerValue = 5.5, Position = Position.Midfielder, NationCode = "IRE", PlayerID = 006, GoalWeight = .11},
                new Player {PlayerFirstname = "Shane", PlayerSurname = "Long", DateOfBirth = DateTime.Parse("1986-03-12"), PlayerValue = 5.5, Position = Position.Forward, NationCode = "IRE", PlayerID = 011, GoalWeight = .179 },
                     new Player {PlayerFirstname = "Andreas", PlayerSurname = "Isaksson", DateOfBirth = DateTime.Parse("1987-08-10"), PlayerValue = 5.0, Position = Position.GoalKeeper, NationCode = "SWE", PlayerID = 012, GoalWeight = .002},
                new Player {PlayerFirstname = "Martin", PlayerSurname = "Olsson", DateOfBirth = DateTime.Parse("1984-03-12"), PlayerValue = 4.8, Position = Position.Defender, NationCode = "SWE", PlayerID = 013, GoalWeight = .07},
 new Player {PlayerFirstname = "Glen", PlayerSurname = "Whealen", DateOfBirth = DateTime.Parse("1984-03-10"), PlayerValue = 4.8, Position = Position.Midfielder, NationCode = "IRE", PlayerID = 007, GoalWeight = .1},
                new Player {PlayerFirstname = "Wes", PlayerSurname = "Houlihan", DateOfBirth = DateTime.Parse("1981-04-10"), PlayerValue = 4.9, Position = Position.Midfielder, NationCode = "IRE", PlayerID = 008, GoalWeight = .1 },
                 new Player {PlayerFirstname = "John", PlayerSurname = "Walters", DateOfBirth = DateTime.Parse("1984-09-10"), PlayerValue = 5.1, Position = Position.Forward, NationCode = "IRE", PlayerID = 010, GoalWeight = .179 },
                  new Player {PlayerFirstname = "James", PlayerSurname = "McCarthy", DateOfBirth = DateTime.Parse("1989-04-10"), PlayerValue = 4.5, Position = Position.Midfielder, NationCode = "IRE", PlayerID = 009, GoalWeight = .1 },
                   new Player {PlayerFirstname = "Oscar", PlayerSurname = "Hiljemark", DateOfBirth = DateTime.Parse("1987-04-10"), PlayerValue = 4.3, Position = Position.Midfielder, NationCode = "SWE", PlayerID = 018, GoalWeight = .06 },
                 new Player {PlayerFirstname = "Mikael", PlayerSurname = "Lustig", DateOfBirth = DateTime.Parse("1987-03-10"), PlayerValue = 5.0, Position = Position.Defender, NationCode = "SWE", PlayerID = 014, GoalWeight = .07},
                new Player {PlayerFirstname = "Jonas", PlayerSurname = "Olsson", DateOfBirth = DateTime.Parse("1981-03-12"), PlayerValue = 5.2, Position = Position.Defender, NationCode = "SWE", PlayerID = 015, GoalWeight = .07 },
                     new Player {PlayerFirstname = "Pierre", PlayerSurname = "Bengtsson", DateOfBirth = DateTime.Parse("1989-08-10"), PlayerValue = 5.0, Position = Position.Defender, NationCode = "SWE", PlayerID = 016, GoalWeight = .06},
                new Player {PlayerFirstname = "Sebastian", PlayerSurname = "Larsson", DateOfBirth = DateTime.Parse("1984-03-12"), PlayerValue = 5.6, Position = Position.Midfielder, NationCode = "SWE", PlayerID = 017, GoalWeight = .09},
                new Player {PlayerFirstname = "Albin", PlayerSurname = "Ekdal", DateOfBirth = DateTime.Parse("1991-03-12"), PlayerValue = 5.4, Position = Position.Midfielder, NationCode = "SWE", PlayerID = 019, GoalWeight = .09},
new Player {PlayerFirstname = "Kim", PlayerSurname = "Kallstrom", DateOfBirth = DateTime.Parse("1990-03-12"), PlayerValue = 5.6, Position = Position.Midfielder, NationCode = "SWE", PlayerID = 020, GoalWeight = .09},
new Player {PlayerFirstname = "Ivo", PlayerSurname = "Toivonen", DateOfBirth = DateTime.Parse("1989-03-12"), PlayerValue = 5.9, Position = Position.Forward, NationCode = "SWE", PlayerID = 021, GoalWeight = .1},
new Player {PlayerFirstname = "Zlatan", PlayerSurname = "Ibrahimovic", DateOfBirth = DateTime.Parse("1984-03-12"), PlayerValue = 7.0, Position = Position.Forward, NationCode = "SWE", PlayerID = 022, GoalWeight = .318},

new Player {PlayerFirstname = "Gianluigi", PlayerSurname = "Buffon", DateOfBirth = DateTime.Parse("1981-03-12"), PlayerValue = 5.7, Position = Position.GoalKeeper, NationCode = "ITA", PlayerID = 023, GoalWeight = .001},
new Player {PlayerFirstname = "Matteo", PlayerSurname = "Darmian", DateOfBirth = DateTime.Parse("1990-03-12"), PlayerValue = 6.0, Position = Position.Defender, NationCode = "ITA", PlayerID = 024, GoalWeight = .06},
new Player {PlayerFirstname = "Giogio", PlayerSurname = "Chiellini", DateOfBirth = DateTime.Parse("1991-03-12"), PlayerValue = 5.9, Position = Position.Defender, NationCode = "ITA", PlayerID = 025, GoalWeight = .03 },
new Player {PlayerFirstname = "Andrea", PlayerSurname = "Barzagli", DateOfBirth = DateTime.Parse("1990-03-12"), PlayerValue = 5.6, Position = Position.Defender, NationCode = "ITA", PlayerID = 026, GoalWeight = .03},
new Player {PlayerFirstname = "Mattia", PlayerSurname = "De Sciglio", DateOfBirth = DateTime.Parse("1989-03-12"), PlayerValue = 5.9, Position = Position.Defender, NationCode = "ITA", PlayerID = 027, GoalWeight = .06},
new Player {PlayerFirstname = "Stephan", PlayerSurname = "El Shaarawy", DateOfBirth = DateTime.Parse("1984-03-12"), PlayerValue = 6.5, Position = Position.Midfielder, NationCode = "ITA", PlayerID = 028, GoalWeight = .2},
new Player {PlayerFirstname = "Claudio", PlayerSurname = "Marchisio", DateOfBirth = DateTime.Parse("1991-03-12"), PlayerValue = 5.6, Position = Position.Midfielder, NationCode = "ITA", PlayerID = 029, GoalWeight = .1},
new Player {PlayerFirstname = "Alessandro", PlayerSurname = "Florenzi", DateOfBirth = DateTime.Parse("1990-03-12"), PlayerValue = 5.7, Position = Position.Midfielder, NationCode = "ITA", PlayerID = 030, GoalWeight = .09},
new Player {PlayerFirstname = "Ricardo", PlayerSurname = "Montolivio", DateOfBirth = DateTime.Parse("1989-03-12"), PlayerValue = 5.9, Position = Position.Midfielder, NationCode = "ITA", PlayerID = 031, GoalWeight = .1},
new Player {PlayerFirstname = "Simone", PlayerSurname = "Zaza", DateOfBirth = DateTime.Parse("1984-03-12"), PlayerValue = 6.0, Position = Position.Forward, NationCode = "ITA", PlayerID = 032, GoalWeight = .129},
new Player {PlayerFirstname = "Graziano", PlayerSurname = "Pelle", DateOfBirth = DateTime.Parse("1989-03-12"), PlayerValue = 6.2, Position = Position.Forward, NationCode = "ITA", PlayerID = 033, GoalWeight = .2},

new Player {PlayerFirstname = "Thibaut", PlayerSurname = "Courtois", DateOfBirth = DateTime.Parse("1991-03-12"), PlayerValue = 5.8, Position = Position.GoalKeeper, NationCode = "BEL", PlayerID = 034, GoalWeight = .001},
new Player {PlayerFirstname = "Vincent", PlayerSurname = "Kompany", DateOfBirth = DateTime.Parse("1990-03-12"), PlayerValue = 6.0, Position = Position.Defender, NationCode = "BEL", PlayerID = 035, GoalWeight = .06},
new Player {PlayerFirstname = "Toby", PlayerSurname = "Alderweireld", DateOfBirth = DateTime.Parse("1991-03-12"), PlayerValue = 5.8, Position = Position.Defender, NationCode = "BEL", PlayerID = 036, GoalWeight = .06},
new Player {PlayerFirstname = "Jan", PlayerSurname = "Vertonghen", DateOfBirth = DateTime.Parse("1990-03-12"), PlayerValue = 5.9, Position = Position.Defender, NationCode = "BEL", PlayerID = 037, GoalWeight = .03},
new Player {PlayerFirstname = "Thomas", PlayerSurname = "Vermaelen", DateOfBirth = DateTime.Parse("1989-03-12"), PlayerValue = 5.6, Position = Position.Defender, NationCode = "BEL", PlayerID = 038, GoalWeight = .03},
new Player {PlayerFirstname = "Kevin", PlayerSurname = "De Bruyne", DateOfBirth = DateTime.Parse("1984-03-12"), PlayerValue = 8.0, Position = Position.Midfielder, NationCode = "BEL", PlayerID = 039, GoalWeight = .2},
new Player {PlayerFirstname = "Eden", PlayerSurname = "Hazard", DateOfBirth = DateTime.Parse("1991-03-12"), PlayerValue = 7.4, Position = Position.Midfielder, NationCode = "BEL", PlayerID = 040, GoalWeight = .09},
new Player {PlayerFirstname = "Mousa", PlayerSurname = "Dembele", DateOfBirth = DateTime.Parse("1990-03-12"), PlayerValue = 6.6, Position = Position.Midfielder, NationCode = "BEL", PlayerID = 041, GoalWeight = .1},
new Player {PlayerFirstname = "Kevin", PlayerSurname = "Mirallas", DateOfBirth = DateTime.Parse("1989-03-12"), PlayerValue = 5.9, Position = Position.Midfielder, NationCode = "BEL", PlayerID = 042, GoalWeight = .1},
new Player {PlayerFirstname = "Romelu", PlayerSurname = "Lukaku", DateOfBirth = DateTime.Parse("1984-03-12"), PlayerValue = 7.0, Position = Position.Forward, NationCode = "BEL", PlayerID = 043, GoalWeight = .129},
new Player {PlayerFirstname = "Christian", PlayerSurname = "Benteke", DateOfBirth = DateTime.Parse("1989-03-12"), PlayerValue = 6.9, Position = Position.Forward, NationCode = "BEL", PlayerID = 044, GoalWeight = .2},

new Player {PlayerFirstname = "Joe", PlayerSurname = "Hart", DateOfBirth = DateTime.Parse("1991-03-12"), PlayerValue = 5.9, Position = Position.GoalKeeper, NationCode = "ENG", PlayerID = 045, GoalWeight = .001},
new Player {PlayerFirstname = "Danny", PlayerSurname = "Rose", DateOfBirth = DateTime.Parse("1990-03-12"), PlayerValue = 5.9, Position = Position.Defender, NationCode = "ENG", PlayerID = 046, GoalWeight = .06},
new Player {PlayerFirstname = "Chris", PlayerSurname = "Smalling", DateOfBirth = DateTime.Parse("1991-03-12"), PlayerValue = 5.6, Position = Position.Defender, NationCode = "ENG", PlayerID = 047, GoalWeight = .03},
new Player {PlayerFirstname = "John", PlayerSurname = "Stones", DateOfBirth = DateTime.Parse("1990-03-12"), PlayerValue = 5.9, Position = Position.Defender, NationCode = "ENG", PlayerID = 048, GoalWeight = .06},
new Player {PlayerFirstname = "Kyle", PlayerSurname = "Walker", DateOfBirth = DateTime.Parse("1989-03-12"), PlayerValue = 5.6, Position = Position.Defender, NationCode = "ENG", PlayerID = 049, GoalWeight = .03},
new Player {PlayerFirstname = "Delli", PlayerSurname = "Alli", DateOfBirth = DateTime.Parse("1984-03-12"), PlayerValue = 7.0, Position = Position.Midfielder, NationCode = "ENG", PlayerID = 050, GoalWeight = .1},
new Player {PlayerFirstname = "Ross", PlayerSurname = "Barkley", DateOfBirth = DateTime.Parse("1991-03-12"), PlayerValue = 7.4, Position = Position.Midfielder, NationCode = "ENG", PlayerID = 051, GoalWeight = .1},
new Player {PlayerFirstname = "Raheem", PlayerSurname = "Sterling", DateOfBirth = DateTime.Parse("1990-03-12"), PlayerValue = 7.6, Position = Position.Midfielder, NationCode = "ENG", PlayerID = 052, GoalWeight = .1},
new Player {PlayerFirstname = "Wayne", PlayerSurname = "Rooney", DateOfBirth = DateTime.Parse("1989-03-12"), PlayerValue = 6.9, Position = Position.Midfielder, NationCode = "ENG", PlayerID = 053, GoalWeight =.1},
new Player {PlayerFirstname = "Harry", PlayerSurname = "Kane", DateOfBirth = DateTime.Parse("1984-03-12"), PlayerValue = 7.7, Position = Position.Forward, NationCode = "ENG", PlayerID = 054, GoalWeight = .21},
new Player {PlayerFirstname = "Jamie", PlayerSurname = "Vardy", DateOfBirth = DateTime.Parse("1989-03-12"), PlayerValue = 6.9, Position = Position.Forward, NationCode = "ENG", PlayerID = 055, GoalWeight = .209},

new Player {PlayerFirstname = "Igor", PlayerSurname = "Akinfeev", DateOfBirth = DateTime.Parse("1991-03-12"), PlayerValue = 5.0, Position = Position.GoalKeeper, NationCode = "RUS", PlayerID = 056, GoalWeight = .001},
new Player {PlayerFirstname = "Yuri", PlayerSurname = "Zhirkov", DateOfBirth = DateTime.Parse("1990-03-12"), PlayerValue = 6.0, Position = Position.Defender, NationCode = "RUS", PlayerID = 057, GoalWeight = .06},
new Player {PlayerFirstname = "Sergei", PlayerSurname = "Ignashevich", DateOfBirth = DateTime.Parse("1991-03-12"), PlayerValue = 5.1, Position = Position.Defender, NationCode = "RUS", PlayerID = 058, GoalWeight = .06},
new Player {PlayerFirstname = "Dmitri", PlayerSurname = "Kombarov", DateOfBirth = DateTime.Parse("1990-03-12"), PlayerValue = 5.2, Position = Position.Defender, NationCode = "RUS", PlayerID = 059, GoalWeight = .03},
new Player {PlayerFirstname = "Georgi", PlayerSurname = "Shchennikov", DateOfBirth = DateTime.Parse("1989-03-12"), PlayerValue = 4.8, Position = Position.Defender, NationCode = "RUS", PlayerID = 060, GoalWeight = .03},
new Player {PlayerFirstname = "Roman", PlayerSurname = "Shirokov", DateOfBirth = DateTime.Parse("1984-03-12"), PlayerValue = 7.1, Position = Position.Midfielder, NationCode = "RUS", PlayerID = 061, GoalWeight = .1},
new Player {PlayerFirstname = "Igor", PlayerSurname = "Denisov", DateOfBirth = DateTime.Parse("1991-03-12"), PlayerValue = 6.4, Position = Position.Midfielder, NationCode = "RUS", PlayerID = 062, GoalWeight = .1},
new Player {PlayerFirstname = "Alan", PlayerSurname = "Dzagoev", DateOfBirth = DateTime.Parse("1990-03-12"), PlayerValue = 6.1, Position = Position.Midfielder, NationCode = "RUS", PlayerID = 063, GoalWeight = .1},
new Player {PlayerFirstname = "Denis", PlayerSurname = "Glushakov", DateOfBirth = DateTime.Parse("1989-03-12"), PlayerValue = 5.9, Position = Position.Midfielder, NationCode = "RUS", PlayerID = 064, GoalWeight = .1},
new Player {PlayerFirstname = "Artyom", PlayerSurname = "Dzyuba", DateOfBirth = DateTime.Parse("1984-03-12"), PlayerValue = 6.2, Position = Position.Forward, NationCode = "RUS", PlayerID = 065, GoalWeight =.21},
new Player {PlayerFirstname = "Aleksandr", PlayerSurname = "Kokorin", DateOfBirth = DateTime.Parse("1989-03-12"), PlayerValue = 5.9, Position = Position.Forward, NationCode = "RUS", PlayerID = 066, GoalWeight = .209},

new Player {PlayerFirstname = "Wayne", PlayerSurname = "Hennessey", DateOfBirth = DateTime.Parse("1991-03-12"), PlayerValue = 5.0, Position = Position.GoalKeeper, NationCode = "WAL", PlayerID = 067},
new Player {PlayerFirstname = "Ashley", PlayerSurname = "Williams", DateOfBirth = DateTime.Parse("1990-03-12"), PlayerValue = 5.2, Position = Position.Defender, NationCode = "WAL", PlayerID = 068},
new Player {PlayerFirstname = "Chris", PlayerSurname = "Gunter", DateOfBirth = DateTime.Parse("1991-03-12"), PlayerValue = 4.6, Position = Position.Defender, NationCode = "WAL", PlayerID = 069},
new Player {PlayerFirstname = "James", PlayerSurname = "Collins", DateOfBirth = DateTime.Parse("1990-03-12"), PlayerValue = 4.9, Position = Position.Defender, NationCode = "WAL", PlayerID = 070},
new Player {PlayerFirstname = "Ben", PlayerSurname = "Davies", DateOfBirth = DateTime.Parse("1989-03-12"), PlayerValue = 5.2, Position = Position.Defender, NationCode = "WAL", PlayerID = 071},
new Player {PlayerFirstname = "Joe", PlayerSurname = "Allen", DateOfBirth = DateTime.Parse("1984-03-12"), PlayerValue = 5.3, Position = Position.Midfielder, NationCode = "WAL", PlayerID = 072},
new Player {PlayerFirstname = "Aaron", PlayerSurname = "Ramsey", DateOfBirth = DateTime.Parse("1991-03-12"), PlayerValue = 7.4, Position = Position.Midfielder, NationCode = "WAL", PlayerID = 073},
new Player {PlayerFirstname = "Joe", PlayerSurname = "Ledley", DateOfBirth = DateTime.Parse("1990-03-12"), PlayerValue = 5.6, Position = Position.Midfielder, NationCode = "WAL", PlayerID = 074},
new Player {PlayerFirstname = "Andy", PlayerSurname = "King", DateOfBirth = DateTime.Parse("1989-03-12"), PlayerValue = 5.9, Position = Position.Midfielder, NationCode = "WAL", PlayerID = 075},
new Player {PlayerFirstname = "Gareth", PlayerSurname = "Bale", DateOfBirth = DateTime.Parse("1984-03-12"), PlayerValue = 8.7, Position = Position.Forward, NationCode = "WAL", PlayerID = 076},
new Player {PlayerFirstname = "Hal", PlayerSurname = "Robson-Kanu", DateOfBirth = DateTime.Parse("1989-03-12"), PlayerValue = 4.9, Position = Position.Forward, NationCode = "WAL", PlayerID = 077},

new Player {PlayerFirstname = "Jan", PlayerSurname = "Mucha", DateOfBirth = DateTime.Parse("1991-03-12"), PlayerValue = 4.8, Position = Position.GoalKeeper, NationCode = "SVK", PlayerID = 078},
new Player {PlayerFirstname = "Jan", PlayerSurname = "Durica", DateOfBirth = DateTime.Parse("1990-03-12"), PlayerValue = 4.5, Position = Position.Defender, NationCode = "SVK", PlayerID = 079},
new Player {PlayerFirstname = "Martin", PlayerSurname = "Skrtel", DateOfBirth = DateTime.Parse("1991-03-12"), PlayerValue = 5.6, Position = Position.Defender, NationCode = "SVK", PlayerID = 080},
new Player {PlayerFirstname = "Thomas", PlayerSurname = "Hubocan", DateOfBirth = DateTime.Parse("1990-03-12"), PlayerValue = 5.2, Position = Position.Defender, NationCode = "SVK", PlayerID = 081},
new Player {PlayerFirstname = "Norbert", PlayerSurname = "Gyomber", DateOfBirth = DateTime.Parse("1989-03-12"), PlayerValue = 4.5, Position = Position.Defender, NationCode = "SVK", PlayerID = 082},
new Player {PlayerFirstname = "Marek", PlayerSurname = "Hamsik", DateOfBirth = DateTime.Parse("1984-03-12"), PlayerValue = 7.0, Position = Position.Midfielder, NationCode = "SVK", PlayerID = 083},
new Player {PlayerFirstname = "Stanislav", PlayerSurname = "Sestak", DateOfBirth = DateTime.Parse("1991-03-12"), PlayerValue = 5.4, Position = Position.Midfielder, NationCode = "SVK", PlayerID = 084},
new Player {PlayerFirstname = "Juraj", PlayerSurname = "Kucka", DateOfBirth = DateTime.Parse("1990-03-12"), PlayerValue = 4.7, Position = Position.Midfielder, NationCode = "SVK", PlayerID = 085},
new Player {PlayerFirstname = "Miroslav", PlayerSurname = "Stoch", DateOfBirth = DateTime.Parse("1989-03-12"), PlayerValue = 4.9, Position = Position.Midfielder, NationCode = "SVK", PlayerID = 086},
new Player {PlayerFirstname = "Robert", PlayerSurname = "Vittek", DateOfBirth = DateTime.Parse("1984-03-12"), PlayerValue = 5.2, Position = Position.Forward, NationCode = "SVK", PlayerID = 087},
new Player {PlayerFirstname = "Michal", PlayerSurname = "Duris", DateOfBirth = DateTime.Parse("1989-03-12"), PlayerValue = 4.9, Position = Position.Forward, NationCode = "SVK", PlayerID = 088}
           };
            players.ForEach(p => context.Players.AddOrUpdate(p));
            context.SaveChanges();


            var fixtures = new List<Fixture>
            {
                new Fixture {FixtureId = 001, Venue = Venue.Bordeaux, RoundStage = RoundStage.FirstRound, HomeTeamNationCode = "WAL", AwayTeamNationCode = "SVK", HomeTeamScore = 0, AwayTeamScore = 0, AwayTeamScorer = new List<Player>(), HomeTeamScorer = new List<Player>(), RedCards = new List<Player>(), YellowCards = new List<Player>(), gamePlayed = false },
                new Fixture {FixtureId = 002, Venue = Venue.Marseille, RoundStage = RoundStage.FirstRound, HomeTeamNationCode = "ENG", AwayTeamNationCode = "RUS",  HomeTeamScore = 0, AwayTeamScore = 0,  AwayTeamScorer = new List<Player>(), HomeTeamScorer = new List<Player>(), RedCards = new List<Player>(), YellowCards = new List<Player>(), gamePlayed = false},
                new Fixture {FixtureId = 004, Venue = Venue.Lyon, RoundStage = RoundStage.FirstRound,HomeTeamNationCode = "BEL", AwayTeamNationCode = "ITA",   HomeTeamScore = 0, AwayTeamScore = 0,  AwayTeamScorer = new List<Player>(), HomeTeamScorer = new List<Player>(), RedCards = new List<Player>(), YellowCards = new List<Player>(), gamePlayed = false },
                new Fixture {FixtureId = 003, Venue = Venue.SaintDenis, RoundStage = RoundStage.FirstRound, HomeTeamNationCode = "IRE", AwayTeamNationCode = "SWE",  HomeTeamScore = 0, AwayTeamScore = 0, AwayTeamScorer = new List<Player>(), HomeTeamScorer = new List<Player>(), RedCards = new List<Player>(), YellowCards = new List<Player>(), gamePlayed = false}

            };
            fixtures.ForEach(f => context.Fixtures.AddOrUpdate(f));
            context.SaveChanges();


            var fantasyLeagues = new List<FantasyLeague>
            {
                new FantasyLeague {FantasyLeagueId = 001, FantasyLeagueName = "Fans Of Ireland", FantasyTeams = new List<FantasyTeam>() },
                new FantasyLeague {FantasyLeagueId = 002, FantasyLeagueName = "Fans Of England", FantasyTeams = new List<FantasyTeam>() },
                new FantasyLeague {FantasyLeagueId = 003, FantasyLeagueName = "Fans Of Wales", FantasyTeams = new List<FantasyTeam>() },
                new FantasyLeague {FantasyLeagueId = 004, FantasyLeagueName = "Fans Of Russia", FantasyTeams = new List<FantasyTeam>() },
                new FantasyLeague {FantasyLeagueId = 005, FantasyLeagueName = "Fans Of Sweden", FantasyTeams = new List<FantasyTeam>() },
                 new FantasyLeague {FantasyLeagueId = 006, FantasyLeagueName = "Fans Of Italy", FantasyTeams = new List<FantasyTeam>() },
                new FantasyLeague {FantasyLeagueId = 007, FantasyLeagueName = "Fans Of Belgium", FantasyTeams = new List<FantasyTeam>() },
                new FantasyLeague {FantasyLeagueId = 008, FantasyLeagueName = "Fans Of Slovakia", FantasyTeams = new List<FantasyTeam>() }
            };
            fantasyLeagues.ForEach(l => context.FantasyLeagues.AddOrUpdate(l));
            context.SaveChanges();

            var playerRoundStats = new List<PlayerRoundStat>
            {
                new PlayerRoundStat {PlayerID = 001, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 002, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 003, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 004, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 005, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 006, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 007, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 008, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 009, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 010, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 011, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 012, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 013, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 014, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 015, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 016, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 017, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 018, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 019, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 020, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 021, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 022, FixtureId = 003, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 023, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 024, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 025, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 026, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 027, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 028, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 029, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 030, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 031, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 032, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 033, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 034, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 035, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 036, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 037, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 038, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 039, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 040, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 041, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 042, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 043, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 044, FixtureId = 004, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 045, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 046, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 047, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 048, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 049, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 050, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 051, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 052, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 053, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 054, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 055, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 056, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                 new PlayerRoundStat {PlayerID = 057, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 058, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 059, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 060, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 061, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 062, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 063, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 064, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                new PlayerRoundStat {PlayerID = 065, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                 new PlayerRoundStat {PlayerID = 066, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false }
                //new PlayerRoundStat {PlayerID = 049, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                //new PlayerRoundStat {PlayerID = 050, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                //new PlayerRoundStat {PlayerID = 051, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                //new PlayerRoundStat {PlayerID = 052, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                //new PlayerRoundStat {PlayerID = 053, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                //new PlayerRoundStat {PlayerID = 054, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                //new PlayerRoundStat {PlayerID = 055, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false },
                //new PlayerRoundStat {PlayerID = 056, FixtureId = 002, goalScored = 0, RedCarded = false, YellowCarded = false, GoalsConceded = 0, CleanSheet = 0, IsWin = false, MinutesPlayed = 90,ManOfTheMatch = false }
            };
            //playerRoundStats.ForEach(l => context.PlayerRoundStats.AddOrUpdate(l));
            //context.SaveChanges();
            foreach (PlayerRoundStat e in playerRoundStats)
            {
                var statsInDataBase = context.PlayerRoundStats.Where(
                    s =>
                         s.Player.PlayerID == e.PlayerID &&
                         s.Fixture.FixtureId == e.FixtureId).SingleOrDefault();
                if (statsInDataBase == null)
                {
                    context.PlayerRoundStats.Add(e);
                }
            }
            context.SaveChanges();
        }
    }
}