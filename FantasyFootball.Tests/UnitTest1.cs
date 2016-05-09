using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using testfan2.Models;
using System.Collections.Generic;
using testfan2.Controllers;
using System.Web.Mvc;
using System.Net;


namespace FantasyFootball.Tests
{ 

    [TestClass]
    public class UnitTest1
    {
        NationTeam a;
        NationTeam b;
        [TestInitialize]
        public void SetUp()
        {
            a = new NationTeam { NationCode = "IRE", Nation = Nation.Ireland, TeamGoalWeight = TeamGoalWeight.C, Players = new List<Player>(), Fixtures = new List<Fixture>() };
            b = new NationTeam { NationCode = "SWE", Nation = Nation.Sweden, TeamGoalWeight = TeamGoalWeight.C, Players = new List<Player>(), Fixtures = new List<Fixture>() };
            NationTeam c = new NationTeam { NationCode = "ITA", Nation = Nation.Italy, TeamGoalWeight = TeamGoalWeight.B, Players = new List<Player>(), Fixtures = new List<Fixture>() };
            NationTeam d = new NationTeam { NationCode = "BEL", Nation = Nation.Belgium, TeamGoalWeight = TeamGoalWeight.A, Players = new List<Player>(), Fixtures = new List<Fixture>() };

            Player p7 = new Player { PlayerFirstname = "Pierre", PlayerSurname = "Bengtsson", DateOfBirth = DateTime.Parse("1989-08-10"), PlayerValue = 5.0, Position = Position.Defender, NationCode = "SWE", PlayerID = 016, GoalWeight = .06 };
            Player p8 = new Player { PlayerFirstname = "Robbie", PlayerSurname = "Brady", DateOfBirth = DateTime.Parse("1987-03-10"), PlayerValue = 5.5, Position = Position.Midfielder, NationCode = "IRE", PlayerID = 006, GoalWeight = .11 };
            Player p9 = new Player { PlayerFirstname = "Shane", PlayerSurname = "Long", DateOfBirth = DateTime.Parse("1986-03-12"), PlayerValue = 5.5, Position = Position.Forward, NationCode = "IRE", PlayerID = 011, GoalWeight = .179 };
            Player p10 = new Player { PlayerFirstname = "Glen", PlayerSurname = "Whealen", DateOfBirth = DateTime.Parse("1984-03-10"), PlayerValue = 4.8, Position = Position.Midfielder, NationCode = "IRE", PlayerID = 007, GoalWeight = .1 };
            Player p11 = new Player { PlayerFirstname = "Wes", PlayerSurname = "Houlihan", DateOfBirth = DateTime.Parse("1981-04-10"), PlayerValue = 4.9, Position = Position.Midfielder, NationCode = "IRE", PlayerID = 008, GoalWeight = .1 };
            Player p12 = new Player { PlayerFirstname = "John", PlayerSurname = "Walters", DateOfBirth = DateTime.Parse("1984-09-10"), PlayerValue = 5.1, Position = Position.Forward, NationCode = "IRE", PlayerID = 010, GoalWeight = .179 };
            Player p13 = new Player { PlayerFirstname = "James", PlayerSurname = "McCarthy", DateOfBirth = DateTime.Parse("1989-04-10"), PlayerValue = 4.5, Position = Position.Midfielder, NationCode = "IRE", PlayerID = 009, GoalWeight = .1 };
            Player p14 = new Player { PlayerFirstname = "Oscar", PlayerSurname = "Hiljemark", DateOfBirth = DateTime.Parse("1987-04-10"), PlayerValue = 4.3, Position = Position.Midfielder, NationCode = "SWE", PlayerID = 018, GoalWeight = .06 };
            Player p15 = new Player { PlayerFirstname = "Mikael", PlayerSurname = "Lustig", DateOfBirth = DateTime.Parse("1987-03-10"), PlayerValue = 5.0, Position = Position.Defender, NationCode = "SWE", PlayerID = 014, GoalWeight = .07 };
            Player p16 = new Player { PlayerFirstname = "Jonas", PlayerSurname = "Olsson", DateOfBirth = DateTime.Parse("1981-03-12"), PlayerValue = 5.2, Position = Position.Defender, NationCode = "SWE", PlayerID = 015, GoalWeight = .07 };
            Player p17 = new Player { PlayerFirstname = "Pierre", PlayerSurname = "Bengtsson", DateOfBirth = DateTime.Parse("1989-08-10"), PlayerValue = 5.0, Position = Position.Defender, NationCode = "SWE", PlayerID = 016, GoalWeight = .06 };
            Player p18 = new Player { PlayerFirstname = "Sebastian", PlayerSurname = "Larsson", DateOfBirth = DateTime.Parse("1984-03-12"), PlayerValue = 5.6, Position = Position.Midfielder, NationCode = "SWE", PlayerID = 017, GoalWeight = .09 };
            Player p19 = new Player { PlayerFirstname = "Albin", PlayerSurname = "Ekdal", DateOfBirth = DateTime.Parse("1991-03-12"), PlayerValue = 5.4, Position = Position.Midfielder, NationCode = "SWE", PlayerID = 019, GoalWeight = .09 };
            Player p20 = new Player { PlayerFirstname = "Kim", PlayerSurname = "Kallstrom", DateOfBirth = DateTime.Parse("1990-03-12"), PlayerValue = 5.6, Position = Position.Midfielder, NationCode = "SWE", PlayerID = 020, GoalWeight = .09 };
            Player p21 = new Player { PlayerFirstname = "Ivo", PlayerSurname = "Toivonen", DateOfBirth = DateTime.Parse("1989-03-12"), PlayerValue = 5.9, Position = Position.Forward, NationCode = "SWE", PlayerID = 021, GoalWeight = .1 };
            Player p22 = new Player { PlayerFirstname = "Zlatan", PlayerSurname = "Ibrahimovic", DateOfBirth = DateTime.Parse("1984-03-12"), PlayerValue = 7.0, Position = Position.Forward, NationCode = "SWE", PlayerID = 022, GoalWeight = .318 };

           // Fixture f1 = new Fixture { FixtureId = 003, Venue = Venue.SaintDenis, RoundStage = RoundStage.FirstRound, HomeTeamNationCode = "IRE", AwayTeamNationCode = "SWE", HomeTeamScore = 0, AwayTeamScore = 0, AwayTeamScorer = new List<Player>(), HomeTeamScorer = new List<Player>(), RedCards = new List<Player>(), YellowCards = new List<Player>(), gamePlayed = false };

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateTeamWithTooManyDefenders()
        {
            Player p1 = new Player() { PlayerFirstname = "Shay", PlayerSurname = "Given", DateOfBirth = DateTime.Parse("1982-03-10"), PlayerValue = 4.5, Position = Position.GoalKeeper, NationCode = "IRE", PlayerID = 001, GoalWeight = .002 };
            Player p2 = new Player { PlayerFirstname = "Seamus", PlayerSurname = "Coleman", DateOfBirth = DateTime.Parse("1984-04-10"), PlayerValue = 4.5, Position = Position.Defender, NationCode = "IRE", PlayerID = 002, GoalWeight = .07 };
            Player p3 = new Player { PlayerFirstname = "John", PlayerSurname = "O'Shea", DateOfBirth = DateTime.Parse("1984-08-10"), PlayerValue = 4.4, Position = Position.Defender, NationCode = "IRE", PlayerID = 003, GoalWeight = .07 };
            Player p4 = new Player { PlayerFirstname = "Ciaran", PlayerSurname = "Clarke", DateOfBirth = DateTime.Parse("1988-04-10"), PlayerValue = 4.5, Position = Position.Defender, NationCode = "IRE", PlayerID = 004, GoalWeight = .07 };
            Player p5 = new Player { PlayerFirstname = "James", PlayerSurname = "McClean", DateOfBirth = DateTime.Parse("1987-04-10"), PlayerValue = 4.3, Position = Position.Defender, NationCode = "IRE", PlayerID = 005, GoalWeight = .06 };
            Player p6 = new Player { PlayerFirstname = "Jonas", PlayerSurname = "Olsson", DateOfBirth = DateTime.Parse("1981-03-12"), PlayerValue = 5.2, Position = Position.Defender, NationCode = "SWE", PlayerID = 015, GoalWeight = .07 };

            FantasyTeam myTeam = new FantasyTeam() { TeamName = "manCity" } ;
            myTeam.Players = new List<Player>();
            myTeam.AddPlayer(p2);
            myTeam.AddPlayer(p3);
            myTeam.AddPlayer(p4); 
            myTeam.AddPlayer(p5);
            myTeam.AddPlayer(p6);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateTeamWithTooManyMidfielders()
        {
            Player p10 = new Player { PlayerFirstname = "Glen", PlayerSurname = "Whealen", DateOfBirth = DateTime.Parse("1984-03-10"), PlayerValue = 4.8, Position = Position.Midfielder, NationCode = "IRE", PlayerID = 007, GoalWeight = .1 };
            Player p11 = new Player { PlayerFirstname = "Wes", PlayerSurname = "Houlihan", DateOfBirth = DateTime.Parse("1981-04-10"), PlayerValue = 4.9, Position = Position.Midfielder, NationCode = "IRE", PlayerID = 008, GoalWeight = .1 };
            Player p8 = new Player { PlayerFirstname = "Robbie", PlayerSurname = "Brady", DateOfBirth = DateTime.Parse("1987-03-10"), PlayerValue = 5.5, Position = Position.Midfielder, NationCode = "IRE", PlayerID = 006, GoalWeight = .11 };
            Player p19 = new Player { PlayerFirstname = "Albin", PlayerSurname = "Ekdal", DateOfBirth = DateTime.Parse("1991-03-12"), PlayerValue = 5.4, Position = Position.Midfielder, NationCode = "SWE", PlayerID = 019, GoalWeight = .09 };
            Player p20 = new Player { PlayerFirstname = "Kim", PlayerSurname = "Kallstrom", DateOfBirth = DateTime.Parse("1990-03-12"), PlayerValue = 5.6, Position = Position.Midfielder, NationCode = "SWE", PlayerID = 020, GoalWeight = .09 };
            FantasyTeam myTeam = new FantasyTeam() { TeamName = "manCity" };
            myTeam.Players = new List<Player>();
            myTeam.AddPlayer(p10);
            myTeam.AddPlayer(p11);
            myTeam.AddPlayer(p8);
            myTeam.AddPlayer(p19);
            myTeam.AddPlayer(p20);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateTeamWithTooManyForwards()
        {
            Player p21 = new Player { PlayerFirstname = "Ivo", PlayerSurname = "Toivonen", DateOfBirth = DateTime.Parse("1989-03-12"), PlayerValue = 5.9, Position = Position.Forward, NationCode = "SWE", PlayerID = 021, GoalWeight = .1 };
            Player p22 = new Player { PlayerFirstname = "Zlatan", PlayerSurname = "Ibrahimovic", DateOfBirth = DateTime.Parse("1984-03-12"), PlayerValue = 7.0, Position = Position.Forward, NationCode = "SWE", PlayerID = 022, GoalWeight = .318 };
            Player p12 = new Player { PlayerFirstname = "John", PlayerSurname = "Walters", DateOfBirth = DateTime.Parse("1984-09-10"), PlayerValue = 5.1, Position = Position.Forward, NationCode = "IRE", PlayerID = 010, GoalWeight = .179 };

            FantasyTeam myTeam = new FantasyTeam() { TeamName = "manCity" };
            myTeam.Players = new List<Player>();
            myTeam.AddPlayer(p21);
            myTeam.AddPlayer(p22);
            myTeam.AddPlayer(p12);
           
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateTeamWithTooManyGoalkeepers()
        {
           Player p23 = new Player() { PlayerFirstname = "Andreas", PlayerSurname = "Isaksson", DateOfBirth = DateTime.Parse("1987-08-10"), PlayerValue = 5.0, Position = Position.GoalKeeper, NationCode = "SWE", PlayerID = 012, GoalWeight = .002 };
            Player p1 = new Player() { PlayerFirstname = "Shay", PlayerSurname = "Given", DateOfBirth = DateTime.Parse("1982-03-10"), PlayerValue = 4.5, Position = Position.GoalKeeper, NationCode = "IRE", PlayerID = 001, GoalWeight = .002 };
            FantasyTeam myTeam = new FantasyTeam() { TeamName = "manCity" };
            myTeam.Players = new List<Player>();
            myTeam.AddPlayer(p23);
            myTeam.AddPlayer(p1);
            

        }
        [TestMethod]
        public void testPlayerRoundPointsAddition()
        {
            Player p22 = new Player { PlayerFirstname = "Zlatan", PlayerSurname = "Ibrahimovic", DateOfBirth = DateTime.Parse("1984-03-12"), PlayerValue = 7.0, Position = Position.Forward, NationCode = "SWE", PlayerID = 022, GoalWeight = .318 };
            Fixture f1 = new Fixture { FixtureId = 003, Venue = Venue.SaintDenis, RoundStage = RoundStage.FirstRound, HomeTeamNationCode = "IRE", AwayTeamNationCode = "SWE", HomeTeamScore = 0, AwayTeamScore = 0, AwayTeamScorer = new List<Player>(), HomeTeamScorer = new List<Player>(), RedCards = new List<Player>(), YellowCards = new List<Player>(), gamePlayed = false };
            PlayerRoundStat prs1 = new PlayerRoundStat() { FixtureId = f1.FixtureId, PlayerID = p22.PlayerID, YellowCarded = true, RedCarded = false, IsWin = true, goalScored = 1, MinutesPlayed = 80 };
            Assert.AreEqual(7, prs1.TotalPoints);
        }

        [TestMethod]
        public void testPlayerRedCardAndMOTM()
        {
            Player p22 = new Player { PlayerFirstname = "Zlatan", PlayerSurname = "Ibrahimovic", DateOfBirth = DateTime.Parse("1984-03-12"), PlayerValue = 7.0, Position = Position.Forward, NationCode = "SWE", PlayerID = 022, GoalWeight = .318 };
            Fixture f1 = new Fixture { FixtureId = 003, Venue = Venue.SaintDenis, RoundStage = RoundStage.FirstRound, HomeTeamNationCode = "IRE", AwayTeamNationCode = "SWE", HomeTeamScore = 0, AwayTeamScore = 0, AwayTeamScorer = new List<Player>(), HomeTeamScorer = new List<Player>(), RedCards = new List<Player>(), YellowCards = new List<Player>(), gamePlayed = false };
            PlayerRoundStat prs1 = new PlayerRoundStat() { FixtureId = f1.FixtureId, PlayerID = p22.PlayerID, YellowCarded = false, RedCarded = true, IsWin = true, goalScored = 2, MinutesPlayed = 59, ManOfTheMatch = true };
            Assert.AreEqual(10, prs1.TotalPoints);
        }
        [TestMethod]
        public void testPlayerDoesNotPlay()
        {
            Player p22 = new Player { PlayerFirstname = "Zlatan", PlayerSurname = "Ibrahimovic", DateOfBirth = DateTime.Parse("1984-03-12"), PlayerValue = 7.0, Position = Position.Forward, NationCode = "SWE", PlayerID = 022, GoalWeight = .318 };
            Fixture f1 = new Fixture { FixtureId = 003, Venue = Venue.SaintDenis, RoundStage = RoundStage.FirstRound, HomeTeamNationCode = "IRE", AwayTeamNationCode = "SWE", HomeTeamScore = 0, AwayTeamScore = 0, AwayTeamScorer = new List<Player>(), HomeTeamScorer = new List<Player>(), RedCards = new List<Player>(), YellowCards = new List<Player>(), gamePlayed = false };
            PlayerRoundStat prs1 = new PlayerRoundStat() { FixtureId = f1.FixtureId, PlayerID = p22.PlayerID, YellowCarded = true, RedCarded = false, IsWin = true, goalScored = 1, MinutesPlayed = 0 };
            Assert.AreEqual(0, prs1.TotalPoints);
        }

        [TestMethod]
        public void testPlayerCleansheetGoalConceded()
        {
            Player p23 = new Player() { PlayerFirstname = "Andreas", PlayerSurname = "Isaksson", DateOfBirth = DateTime.Parse("1987-08-10"), PlayerValue = 5.0, Position = Position.GoalKeeper, NationCode = "SWE", PlayerID = 012, GoalWeight = .002 };
            Player p1 = new Player() { PlayerFirstname = "Shay", PlayerSurname = "Given", DateOfBirth = DateTime.Parse("1982-03-10"), PlayerValue = 4.5, Position = Position.GoalKeeper, NationCode = "IRE", PlayerID = 001, GoalWeight = .002 };
            Fixture f1 = new Fixture { FixtureId = 003, Venue = Venue.SaintDenis, RoundStage = RoundStage.FirstRound, HomeTeamNationCode = "IRE", AwayTeamNationCode = "SWE", HomeTeamScore = 2, AwayTeamScore = 0, AwayTeamScorer = new List<Player>(), HomeTeamScorer = new List<Player>(), RedCards = new List<Player>(), YellowCards = new List<Player>(), gamePlayed = false };
            PlayerRoundStat prs1 = new PlayerRoundStat() { FixtureId = f1.FixtureId, PlayerID = p23.PlayerID, YellowCarded = false, RedCarded = false, IsWin = false, goalScored = 0, MinutesPlayed = 90, GoalsConceded = 2, CleanSheet = false };
            PlayerRoundStat prs2 = new PlayerRoundStat() { FixtureId = f1.FixtureId, PlayerID = p23.PlayerID, YellowCarded = false, RedCarded = false, IsWin = false, goalScored = 0, MinutesPlayed = 90, GoalsConceded = 0, CleanSheet = true };
            Assert.AreEqual(2, prs1.TotalPoints);
            Assert.AreEqual(9, prs2.TotalPoints);
        }
      

        [TestMethod]
        public void testPlayGame()
        {
           
            var controller = new FixtureController();
            var _target = controller.PlayGame(null);

           // Assert.AreEqual("fixture", _target.ViewName);
            _target.Equals(new HttpStatusCodeResult(HttpStatusCode.OK));
        }


      
    }
}
