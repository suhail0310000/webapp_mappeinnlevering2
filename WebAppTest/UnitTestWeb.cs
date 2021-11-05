using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MappeInnlevering_1.Controllers;
using MappeInnlevering_1.DAL;
using MappeInnlevering_1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using webapp2.Models;
using Xunit;

namespace WebAppTest
{
    public class UnitTestWeb
    {
        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";

        private readonly Mock<IKundeOrdreRepository> mockRep = new Mock<IKundeOrdreRepository>();
        private readonly Mock<ILogger<KundeController>> mockLog = new Mock<ILogger<KundeController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        [Fact]
        public async Task OpprettReiseLoggedinOK()
        {
            // Arrange
            mockRep.Setup(k => k.OpprettReise(It.IsAny<Reiser>())).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.OpprettReise(It.IsAny<Reiser>()) as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Reisen er nå registrert", resultat.Value);
        }

        [Fact]
        public async Task OpprettReiseLoggedinNotOK()
        {
            // Arrange
            mockRep.Setup(k => k.OpprettReise(It.IsAny<Reiser>())).ReturnsAsync(false);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.OpprettReise(It.IsAny<Reiser>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Reisen ble ikke registrert", resultat.Value);
        }


        [Fact]
        public async Task OpprettReiseNotLoggedin()
        {
            // Arrange
            mockRep.Setup(k => k.OpprettReise(It.IsAny<Reiser>())).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.OpprettReise(It.IsAny<Reiser>()) as UnauthorizedObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Beklager du er ikke logget inn!", resultat.Value);
        }

        [Fact]
        public async Task SlettReiseLoggedinnOK()
        {
            // Arrange
            mockRep.Setup(k => k.SlettReise(It.IsAny<int>())).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.SlettReise(It.IsAny<int>()) as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Reisen er nå slettet", resultat.Value);
        }

        [Fact]
        public async Task SlettReiseLoggedinNotOK()
        {
            // Arrange
            mockRep.Setup(k => k.SlettReise(It.IsAny<int>())).ReturnsAsync(false);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.SlettReise(It.IsAny<int>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Beklager reisen ble ikke slettet", resultat.Value);
        }
        [Fact]
        public async Task SlettReiseNotLoggedin()
        {
            // Arrange
            mockRep.Setup(k => k.SlettReise(It.IsAny<int>())).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.SlettReise(It.IsAny<int>()) as UnauthorizedObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Beklager du er ikke logget inn!", resultat.Value);
        }



        [Fact]
        public async Task SlettOrdreLoggedinnOK()
        {
            // Arrange
            mockRep.Setup(k => k.SlettOrdre(It.IsAny<int>())).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.SlettOrdre(It.IsAny<int>()) as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Ordre er nå slettet", resultat.Value);
        }
        [Fact]
        public async Task SlettOrdreLoggedinNotOK()
        {
            // Arrange
            mockRep.Setup(k => k.SlettOrdre(It.IsAny<int>())).ReturnsAsync(false);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.SlettOrdre(It.IsAny<int>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Beklager ordre ble ikke slettet", resultat.Value);
        }

        [Fact]
        public async Task SlettOrdreNotLoggedin()
        {
            // Arrange
            mockRep.Setup(k => k.SlettOrdre(It.IsAny<int>())).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.SlettOrdre(It.IsAny<int>()) as UnauthorizedObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Beklager du er ikke logget inn!", resultat.Value);
        }

        [Fact]
        public async Task EndreReiseLoggedinnOK()
        {
            // Arrange
            mockRep.Setup(k => k.OppdatereReise(It.IsAny<Reiser>())).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.OppdatereReise(It.IsAny<Reiser>()) as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Reisen er nå oppdatert!", resultat.Value);
        }
        [Fact]
        public async Task EndreReiseLoggedinNotOK()
        {
            // Arrange
            mockRep.Setup(k => k.OppdatereReise(It.IsAny<Reiser>())).ReturnsAsync(false);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.OppdatereReise(It.IsAny<Reiser>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Beklager reisen ble ikke oppdatert", resultat.Value);
        }

        [Fact]
        public async Task EndreReiseLoggedInFeilModel()
        {
            var sted1 = new Sted
            {
                SId = 1,
                StedsNavn = "Oslo"
            };

            var sted2 = new Sted
            {
                SId = 2,
                StedsNavn = "Kiel"
            };
            var reise1 = new Reiser
            {
                RId = 1,
                FraSted = sted2,
                TilSted = sted1,
                Dato = "",
                Tid = "08:00",
                PrisBarn = 100,
                PrisStudent = 130,
                PrisVoksen = 200
            };

            mockRep.Setup(k => k.OppdatereReise(reise1)).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            kundeController.ModelState.AddModelError("Dato", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await kundeController.OppdatereReise(reise1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task EndreReiseNotLoggedin()
        {
            // Arrange
            mockRep.Setup(k => k.OppdatereReise(It.IsAny<Reiser>())).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.OppdatereReise(It.IsAny<Reiser>()) as UnauthorizedObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Beklager du er ikke logget inn!", resultat.Value);
        }

        [Fact]
        public async Task LoggedinnOK()
        {
            // Arrange
            mockRep.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.LoggInn(It.IsAny<Bruker>()) as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.True((bool)resultat.Value);
        }

        [Fact]
        public async Task LoggedinnFeilNavnogPass()
        {
            mockRep.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(false);

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await kundeController.LoggInn(It.IsAny<Bruker>()) as OkObjectResult;
            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.False((bool)resultat.Value);
        }

        [Fact]
        public async Task LoggInnInputFeil()
        {
            mockRep.Setup(k => k.LoggInn(It.IsAny<Bruker>())).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            kundeController.ModelState.AddModelError("Brukernavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await kundeController.LoggInn(It.IsAny<Bruker>()) as BadRequestObjectResult;
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);

        }

        [Fact]
        public async Task LagreOK()
        {

            mockRep.Setup(k => k.Lagre(It.IsAny<KundeOrdre>())).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);


            //Act
            var resultat = await kundeController.Lagre(It.IsAny<KundeOrdre>()) as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Bestillingen er Lagret", resultat.Value);
        }

        [Fact]
        public async Task LagreNotOK()
        {

            mockRep.Setup(k => k.Lagre(It.IsAny<KundeOrdre>())).ReturnsAsync(false);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            //Act
            var resultat = await kundeController.Lagre(It.IsAny<KundeOrdre>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Beklager bestillingen ble ikke lagret", resultat.Value);
        }

        [Fact]
        public async Task LagreLoggedInFeilModel()
        {
            var kunde1 = new KundeOrdre
            {
                Fornavn = "",
                Etternavn = "Pettersen",
                FraSted = "Oslo",
                TilSted = "Kiel",
                Dato = "10.10.2021",
                Tid = "10:00",
                AntallBarn = 1,
                AntallStudent = 0,
                AntallVoksne = 2
            };

            mockRep.Setup(k => k.Lagre(kunde1)).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            kundeController.ModelState.AddModelError("Fornavn", "Feil i inputvalidering på server");



            var resultat = await kundeController.Lagre(kunde1) as BadRequestObjectResult;

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }


        [Fact]
        public async Task GetAllStederOK()
        {
            var sted1 = new Sted
            {
                SId = 1,
                StedsNavn = "Oslo",
            };

            var sted2 = new Sted
            {
                SId = 2,
                StedsNavn = "København",
            };

            var sted3 = new Sted
            {
                SId = 3,
                StedsNavn = "Kiel",
            };

            var stedsListe = new List<Sted>();
            stedsListe.Add(sted1);
            stedsListe.Add(sted2);
            stedsListe.Add(sted3);
            // Arrange
            mockRep.Setup(k => k.GetAllSteder()).ReturnsAsync(stedsListe);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);


            //Act
            var resultat = await kundeController.GetAllSteder() as OkObjectResult;
            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal(stedsListe, resultat.Value);
        }

        [Fact]
        public async Task GetAllStederOKFeilDB()
        {
            //var stedListe = new List<Sted>();
            mockRep.Setup(k => k.GetAllSteder()).ReturnsAsync(() => null);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            //Act
            var resultat = await kundeController.GetAllSteder() as OkObjectResult;
            //Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Null(resultat.Value);
        }



        [Fact]
        public async Task GetAllDestinasjonerOK()
        {


            var destinasjon1 = new Sted
            {
                StedsNavn = "Oslo"
            };

            var destinasjon2 = new Sted
            {
                StedsNavn = "Kiel"
            };

            var destinasjon3 = new Sted
            {
                StedsNavn = "København"
            };

            var reise1 = new Reiser
            {
                RId = 1,
                FraSted = destinasjon1,
                TilSted = destinasjon2,
                Dato = "10/10/2021",
                Tid = "18:00",
                PrisBarn = 130,
                PrisVoksen = 300,
                PrisStudent = 170
            };


            var destinasjonsList = new List<Sted>();
            destinasjonsList.Add(destinasjon1);
            destinasjonsList.Add(destinasjon2);
            destinasjonsList.Add(destinasjon3);

            // Arrange
            mockRep.Setup(k => k.GetAllDestinasjoner(destinasjon1.StedsNavn)).ReturnsAsync(destinasjonsList);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            //Act
            var resultat = await kundeController.GetAllDestinasjoner(destinasjon1.StedsNavn) as OkObjectResult;
            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal(destinasjonsList, resultat.Value);
        }
        [Fact]
        public async Task GetAllDestinasjonerNull()
        {
            var destinasjon1 = new Sted();
            var destinasjonListe = new List<Sted>();
            destinasjonListe.Add(destinasjon1);
            mockRep.Setup(k => k.GetAllDestinasjoner(destinasjon1.StedsNavn)).ReturnsAsync(() => null);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            //Act
            var resultat = await kundeController.GetAllDestinasjoner(destinasjon1.StedsNavn) as OkObjectResult;
            //Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Null(resultat.Value);
        }


        [Fact]
        public async Task GetAlleReiserOK()
        {
            var sted1 = new Sted
            {
                SId = 1,
                StedsNavn = "Oslo",
            };

            var sted2 = new Sted
            {
                SId = 2,
                StedsNavn = "København",
            };

            var sted3 = new Sted
            {
                SId = 3,
                StedsNavn = "Kiel",
            };
            var reise1 = new Reiser
            {
                RId = 1,
                FraSted = sted2,
                TilSted = sted3,
                Dato = "06/11/2021",
                Tid = "09:00",
                PrisBarn = 100,
                PrisVoksen = 200,
                PrisStudent = 120

            };
            var reise2 = new Reiser
            {
                RId = 2,
                FraSted = sted3,
                TilSted = sted2,
                Dato = "07/11/2021",
                Tid = "15:00",
                PrisBarn = 100,
                PrisVoksen = 200,
                PrisStudent = 120
            };

            var reise3 = new Reiser
            {
                RId = 3,
                FraSted = sted1,
                TilSted = sted3,
                Dato = "08/11/2021",
                Tid = "18:00",
                PrisBarn = 130,
                PrisVoksen = 300,
                PrisStudent = 170
            };

            var reiseList = new List<Reiser>();
            reiseList.Add(reise1);
            reiseList.Add(reise2);
            reiseList.Add(reise3);

            // Arrange
            mockRep.Setup(k => k.GetAlleReiser()).ReturnsAsync(reiseList);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            //Act
            var resultat = await kundeController.GetAlleReiser() as OkObjectResult;
            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal(reiseList, resultat.Value);
        }

        [Fact]
        public async Task GetAllReiserNull()
        {
            mockRep.Setup(k => k.GetAlleReiser()).ReturnsAsync(() => null);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            //Act
            var resultat = await kundeController.GetAlleReiser() as OkObjectResult;
            //Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Null(resultat.Value);
        }


        [Fact]
        public async Task GetAlleKunderOK()
        {
            var kunde1 = new Kunder
            {
                KId = 1,
                Fornavn = "Suhail",
                Etternavn = "Rauf"

            };
            var kunde2 = new Kunder
            {
                KId = 2,
                Fornavn = "Soulaimane",
                Etternavn = "Benmessaoud"

            };

            var kunde3 = new Kunder
            {
                KId = 3,
                Fornavn = "sevde",
                Etternavn = "Oguz"
            };

            var kundeList = new List<Kunder>();
            kundeList.Add(kunde1);
            kundeList.Add(kunde2);
            kundeList.Add(kunde3);
            // Arrange
            mockRep.Setup(k => k.GetAlleKunder()).ReturnsAsync(kundeList);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            //Act
            var resultat = await kundeController.GetAlleKunder() as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal(kundeList, resultat.Value);
        }

        [Fact]
        public async Task GetAlleKunderNull()
        {
            mockRep.Setup(k => k.GetAlleKunder()).ReturnsAsync(() => null);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            //Act
            var resultat = await kundeController.GetAlleKunder() as OkObjectResult;
            //Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Null(resultat.Value);
        }

        [Fact]
        public async Task GetAlleOrdreOK()
        {

            var kunde1 = new Kunder
            {
                KId = 1,
                Fornavn = "Sevde",
                Etternavn = "Oguz"
            };

            var sted1 = new Sted
            {
                SId = 1,
                StedsNavn = "Oslo"
            };

            var sted2 = new Sted
            {
                SId = 1,
                StedsNavn = "Kiel"
            };

            var reise1 = new Reiser
            {
                RId = 1,
                FraSted = sted1,
                TilSted = sted2,
                Dato = "25/02/2021",
                Tid = "09:00",
                PrisVoksen = 200,
                PrisBarn = 100,
                PrisStudent = 130
            };


            var ordre1 = new Ordre
            {
                OId = 1,
                AntallBarn = 1,
                AntallStudent = 0,
                AntallVoksne = 2,
                Kunder = kunde1,
                Reiser = reise1,
                TotalPris = 300

            };


            var ordreList = new List<Ordre>();
            ordreList.Add(ordre1);


            // Arrange
            mockRep.Setup(k => k.GetAlleOrdre()).ReturnsAsync(ordreList);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            //Act
            var resultat = await kundeController.GetAlleOrdre() as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal(ordreList, resultat.Value);
        }

        [Fact]
        public async Task GetAlleOrdreNull()
        {
            mockRep.Setup(k => k.GetAlleOrdre()).ReturnsAsync(() => null);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            //Act
            var resultat = await kundeController.GetAlleOrdre() as OkObjectResult;
            //Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Null(resultat.Value);
        }

        [Fact]
        public async Task GetAlleBrukereOK()
        {
            var passord = Encoding.Unicode.GetBytes("passord123");
            var salt = Encoding.Unicode.GetBytes("salt");
            var bruker1 = new Brukere
            {
                Id = 1,
                Brukernavn = "Superbruker",
                Passord = passord,
                Salt = salt

            };
            var brukerList = new List<Brukere>();
            brukerList.Add(bruker1);

            // Arrange
            mockRep.Setup(k => k.GetAlleBrukere()).ReturnsAsync(brukerList);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            //Act
            var resultat = await kundeController.GetAlleBrukere() as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal(brukerList, resultat.Value);
        }
        [Fact]
        public async Task GetAlleBrukereNull()
        {
            mockRep.Setup(k => k.GetAlleBrukere()).ReturnsAsync(() => null);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);

            //Act
            var resultat = await kundeController.GetAlleOrdre() as OkObjectResult;
            //Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Null(resultat.Value);
        }

        [Fact]
        public async Task OpprettKundeLoggedinOK()
        {
            // Arrange
            mockRep.Setup(k => k.OpprettKunde(It.IsAny<Kunder>())).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.OpprettKunde(It.IsAny<Kunder>()) as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Kunden er nå opprettet", resultat.Value);
        }

        [Fact]
        public async Task OpprettKundeLoggedinNotOK()
        {
            // Arrange
            mockRep.Setup(k => k.OpprettKunde(It.IsAny<Kunder>())).ReturnsAsync(false);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.OpprettKunde(It.IsAny<Kunder>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Beklager kunden ble ikke opprettet", resultat.Value);
        }
        [Fact]
        public async Task OpprettKundeLoggedInnFeilModel()
        {

            var kunde1 = new Kunder
            {
                KId = 1,
                Fornavn = "",
                Etternavn = "Hansen"
            };
            mockRep.Setup(k => k.OpprettKunde(It.IsAny<Kunder>())).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            kundeController.ModelState.AddModelError("Fornavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await kundeController.OpprettKunde(kunde1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);

        }


        [Fact]
        public async Task OpprettKundeNotLoggedin()
        {
            // Arrange
            mockRep.Setup(k => k.OpprettKunde(It.IsAny<Kunder>())).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.OpprettKunde(It.IsAny<Kunder>()) as UnauthorizedObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Beklager du er ikke logget inn!", resultat.Value);
        }

        [Fact]
        public async Task OpprettStedLoggedinOK()
        {
            // Arrange
            mockRep.Setup(k => k.OpprettSted(It.IsAny<Sted>())).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.OpprettSted(It.IsAny<Sted>()) as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Sted er nå opprettet", resultat.Value);
        }


        [Fact]
        public async Task OpprettStedLoggedinNotOK()
        {
            // Arrange
            mockRep.Setup(k => k.OpprettSted(It.IsAny<Sted>())).ReturnsAsync(false);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.OpprettSted(It.IsAny<Sted>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Beklager stedet ble ikke opprettet", resultat.Value);
        }


        [Fact]
        public async Task OpprettStedNotLoggedin()
        {
            // Arrange
            mockRep.Setup(k => k.OpprettSted(It.IsAny<Sted>())).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.OpprettSted(It.IsAny<Sted>()) as UnauthorizedObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Beklager du er ikke logget inn!", resultat.Value);
        }

        [Fact]
        public async Task EndreKundeLoggedinnOK()
        {
            // Arrange
            mockRep.Setup(k => k.EndreKunde(It.IsAny<Kunder>())).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.EndreKunde(It.IsAny<Kunder>()) as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Kunden er nå oppdatert!", resultat.Value);
        }

        [Fact]
        public async Task EndreKundeLoggedinNotOK()
        {
            // Arrange
            mockRep.Setup(k => k.EndreKunde(It.IsAny<Kunder>())).ReturnsAsync(false);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.EndreKunde(It.IsAny<Kunder>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Beklager kunden ble ikke oppdatert", resultat.Value);
        }

        [Fact]
        public async Task EndreKundeLoggedInFeilModel()
        {

            var kunde1 = new Kunder
            {
                KId = 1,
                Fornavn = "",
                Etternavn = "Oguz"

            };

            mockRep.Setup(k => k.EndreKunde(kunde1)).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            kundeController.ModelState.AddModelError("Fornavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await kundeController.EndreKunde(kunde1) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task EndreKundeNotLoggedin()
        {
            // Arrange
            mockRep.Setup(k => k.EndreKunde(It.IsAny<Kunder>())).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.EndreKunde(It.IsAny<Kunder>()) as UnauthorizedObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Beklager du er ikke logget inn!", resultat.Value);
        }

        [Fact]
        public async Task EndreOrdreLoggedinnOK()
        {
            // Arrange
            mockRep.Setup(k => k.EndreOrdre(It.IsAny<Ordre>())).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.EndreOrdre(It.IsAny<Ordre>()) as OkObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Ordre er nå oppdatert!", resultat.Value);
        }


        [Fact]
        public async Task EndreOrdreLoggedinNotOK()
        {
            // Arrange
            mockRep.Setup(k => k.EndreOrdre(It.IsAny<Ordre>())).ReturnsAsync(false);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.EndreOrdre(It.IsAny<Ordre>()) as BadRequestObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Beklager ordre ble ikke oppdatert", resultat.Value);
        }
        [Fact]
        public async Task EndreOrdreNotLoggedin()
        {
            // Arrange
            mockRep.Setup(k => k.EndreOrdre(It.IsAny<Ordre>())).ReturnsAsync(true);
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await kundeController.EndreOrdre(It.IsAny<Ordre>()) as UnauthorizedObjectResult;

            //Assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Beklager du er ikke logget inn!", resultat.Value);
        }



        [Fact]
        public void validSession()
        {

            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            mockSession[_loggetInn] = _loggetInn;
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            kundeController.ValidSession();

            Assert.Equal(_loggetInn, mockSession[_loggetInn]);

        }

        [Fact]
        public void LoggUt()
        {
            var kundeController = new KundeController(mockRep.Object, mockLog.Object);
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            mockSession[_loggetInn] = _loggetInn;
            kundeController.ControllerContext.HttpContext = mockHttpContext.Object;

            kundeController.LoggUt();

            Assert.Equal(_ikkeLoggetInn, mockSession[_loggetInn]);
        }
    }
}
