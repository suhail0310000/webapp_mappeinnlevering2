using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MappeInnlevering_1.Models;
using Microsoft.EntityFrameworkCore;
using MappeInnlevering_1.DAL;
using Microsoft.Extensions.Logging;
using webapp2.Models;
using Microsoft.AspNetCore.Http;

namespace MappeInnlevering_1.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class KundeController : ControllerBase
    {
        private readonly IKundeOrdreRepository _DB;
        private readonly ILogger<KundeController> _log;
        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";

        public KundeController(IKundeOrdreRepository Db, ILogger<KundeController> log)
        {
            _DB = Db;
            _log = log;

        }

        public async Task<ActionResult> GetAlleReiser()
        {
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            //{
            //    return Unauthorized("Beklager du er ikke logget inn!");
            //}
            List<Reiser> alleReiser = await _DB.GetAlleReiser();
            _log.LogInformation("Hallo loggen");
            return Ok(alleReiser);
        }

        public async Task<ActionResult> GetAllSteder()
        {
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            //{
            //    return Unauthorized("Beklager du er ikke logget inn!");
            //}
            List<Sted> alleSteder = await _DB.GetAllSteder();
            return Ok(alleSteder);
        }

        public async Task<ActionResult> GetAllDestinasjoner(string avgang)
        {
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            //{
            //    return Unauthorized("Beklager du er ikke logget inn!");
            //}
            List<Sted> destinasjon = await _DB.GetAllDestinasjoner(avgang);
            return Ok(destinasjon);
        }

        [HttpPut]
        public async Task<ActionResult> OppdatereReise(Reiser reise)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Beklager du er ikke logget inn!");
            }
            if (ModelState.IsValid)
            {
                bool test = await _DB.OppdatereReise(reise);
                if (!test)
                {
                    _log.LogInformation("Beklager reisen ble ikke oppdatert");
                    return BadRequest("Beklager reisen ble ikke oppdatert");
                }
                return Ok("Reisen er nå oppdatert!");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        [HttpPost]
        public async Task<ActionResult> OpprettReise(Reiser reise)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Beklager du er ikke logget inn!");
            }
            if (ModelState.IsValid)
            {
                bool test = await _DB.OpprettReise(reise);
                if (!test)
                //return Ok(test);

                {
                    _log.LogInformation("Reisen ble ikke registrert");
                    return BadRequest("Reisen ble ikke registrert");
                }
                //return Ok("Reisen er nå registrert");
            }
            return Ok("Reisen er nå registrert");
            //_log.LogInformation("Feil i inputvalidering");
            //return BadRequest("Feil i inputvalidering på server");
        }

        [HttpDelete("{rId}")]
        public async Task<ActionResult> SlettReise(int RId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Beklager du er ikke logget inn!");
            }
            bool test = await _DB.SlettReise(RId);
            if (!test)
            {
                _log.LogInformation("Beklager reisen ble ikke slettet");
                return BadRequest("Beklager reisen ble ikke slettet");
            }
            return Ok("Reisen er nå slettet");
        }

        public async Task<ActionResult> Lagre(KundeOrdre innOrdre)
        {
            if (ModelState.IsValid)
            {

                bool returOk = await _DB.Lagre(innOrdre);
                if (!returOk)
                {
                    _log.LogInformation("Beklager bestilling ble ikke lagret");
                    return BadRequest("Beklager bestillingen ble ikke lagret");
                }
                return Ok("Bestillingen er Lagret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        /*public async Task<ActionResult> LoggInn(Bruker bruker)
        {
            if (ModelState.IsValid)
            {
                bool returnOK = await _DB.LoggInn(bruker);
                if (!returnOK)
                {
                    _log.LogInformation("Innloggingen feilet for bruker" + bruker.Brukernavn);
                    return Ok(false);
                }
                return Ok(true);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }*/
        public async Task<ActionResult> LoggInn(Bruker bruker)
        {

            if (ModelState.IsValid)
            {
                bool returnOK = await _DB.LoggInn(bruker);
                if (!returnOK)
                {
                    _log.LogInformation("Innloggingen feilet for bruker");
                    HttpContext.Session.SetString(_loggetInn, _ikkeLoggetInn);
                    return Ok(false);
                }
                HttpContext.Session.SetString(_loggetInn, _loggetInn);
                return Ok(true);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public ActionResult ValidSession()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Ok(false);
            }
            return Ok(true);

        }
        public void LoggUt()
        {
            HttpContext.Session.SetString(_loggetInn, "");
        }

        //POSTMAN
        public async Task<ActionResult> GetAlleKunder()
        {

            List<Kunder> alleKunder = await _DB.GetAlleKunder();
            return Ok(alleKunder);
        }

        public async Task<ActionResult> GetAlleOrdre()
        {
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            //{
            //    return Unauthorized("Beklager du er ikke logget inn!");
            //}
            List<Ordre> alleOrdre = await _DB.GetAlleOrdre();
            return Ok(alleOrdre);
        }


        public async Task<ActionResult> GetAlleBrukere()
        {
            //if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            //{
            //    return Unauthorized("Beklager du er ikke logget inn!");
            //}
            List<Brukere> alleBrukere = await _DB.GetAlleBrukere();
            return Ok(alleBrukere);
        }

        [HttpPost]
        public async Task<ActionResult> OpprettKunde(Kunder innKunde)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Beklager du er ikke logget inn!");
            }

            if (ModelState.IsValid)
            {
                bool test = await _DB.OpprettKunde(innKunde);
                if (!test)
                {
                    _log.LogInformation("Beklager kunden ble ikke opprettet");
                    return BadRequest("Beklager kunden ble ikke opprettet");
                }
                return Ok("Kunden er nå opprettet");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        [HttpPost]
        public async Task<ActionResult> OpprettSted(Sted innSted)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Beklager du er ikke logget inn!");
            }
            if (ModelState.IsValid)
            {
                bool test = await _DB.OpprettSted(innSted);
                if (!test)
                {
                    _log.LogInformation("Beklager stedet ble ikke opprettet");
                    return BadRequest("Beklager stedet ble ikke opprettet");
                }
                //return Ok("Sted er nå opprettet");
            }
            return Ok("Sted er nå opprettet");
            //_log.LogInformation("Feil i inputvalidering");
            //return BadRequest("Feil i inputvalidering på server");

        }

        [HttpDelete("{oId}")]
        public async Task<ActionResult> SlettOrdre(int oId)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Beklager du er ikke logget inn!");
            }
            bool test = await _DB.SlettOrdre(oId);
            if (!test)
            {
                _log.LogInformation("Beklager ordre ble ikke slettet");
                return BadRequest("Beklager ordre ble ikke slettet");
            }
            return Ok("Ordre er nå slettet");
        }


        [HttpPut]
        public async Task<ActionResult> EndreKunde(Kunder endreKunde)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Beklager du er ikke logget inn!");
            }
            if (ModelState.IsValid)
            {
                bool test = await _DB.EndreKunde(endreKunde);
                if (!test)
                {
                    _log.LogInformation("Beklager kunden ble ikke oppdatert");
                    return BadRequest("Beklager kunden ble ikke oppdatert");
                }
                return Ok("Kunden er nå oppdatert!");
            }

            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        [HttpPut]
        public async Task<ActionResult> EndreOrdre(Ordre endreOrdre)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Beklager du er ikke logget inn!");
            }
            if (ModelState.IsValid)
            {
                bool test = await _DB.EndreOrdre(endreOrdre);
                if (!test)
                {
                    _log.LogInformation("Beklager ordre ble ikke oppdatert");
                    return BadRequest("Beklager ordre ble ikke oppdatert");
                }

            }
            return Ok("Ordre er nå oppdatert!");
        }
    }


    /* public async Task<ActionResult> EndreSted(Sted endreSted)
     {
         if (ModelState.IsValid)
         {
             bool test = await _DB.EndreSted(endreSted);
             if (!test)
             {
                 _log.LogInformation("Sted er oppdatert");
                 return BadRequest("Sted er ikke oppdatert");
             }

         }
         return Ok("Sted er nå oppdatert!");
     }*/



    /*[HttpDelete("{kId}")]
    public async Task<ActionResult> SlettKunde(int kId)
    {

        bool test = await _DB.SlettReise(kId);
        if (!test)
        {
            _log.LogInformation("Kunden er ikke slettet");
            return BadRequest("Kunden er ikke slettet");
        }
        return Ok("Kunden er nå slettet");
    }*/




    /*[HttpDelete("{SId}")]
    public async Task<ActionResult> SlettSted(int SId)
    {
        bool test = await _DB.SlettSted(SId);
        if (!test)
        {
            _log.LogInformation("Stedet ble ikke slettet");
            return BadRequest("Stedet ble ikke slettet");
        }
        return Ok("Stedet er nå slettet");
    }*/

}
