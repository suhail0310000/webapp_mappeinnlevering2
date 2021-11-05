using MappeInnlevering_1.Controllers;
using MappeInnlevering_1.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using webapp2.Models;

namespace MappeInnlevering_1.DAL
{
    public class KundeOrdreRepository : IKundeOrdreRepository
    {
        private readonly DB _DB;
        private readonly ILogger<KundeOrdreRepository> _log;

        public KundeOrdreRepository(DB Db, ILogger<KundeOrdreRepository> log)
        {
            _DB = Db;
            _log = log;
        }



        //public async Task<List<Reiser>> HentAlle()
        //{
        //    List<Reiser> alleTurer = await _DB.Reiser.ToListAsync();

        //    //No needed just for debugging
        //    foreach (var i in alleTurer)
        //    {
        //        System.Diagnostics.Debug.WriteLine(i.PrisBarn);
        //    }
        //    return alleTurer;

        //}

        public async Task<List<Sted>> GetAllSteder()
        {
            List<Sted> alleStasjoner = await _DB.Steder.ToListAsync();
            foreach (var i in alleStasjoner)
            {
                System.Diagnostics.Debug.WriteLine(i.StedsNavn);
            }
            return alleStasjoner;
        }



        public async Task<List<Reiser>> GetAlleReiser()
        {
            List<Reiser> alleTurer = await _DB.Reiser.ToListAsync();

            //No needed just for debugging
            foreach (var i in alleTurer)
            {
                System.Diagnostics.Debug.WriteLine(i.PrisBarn);
            }
            return alleTurer;

        }
        public async Task<bool> SjekkReise(Reiser reise)
        {
            List<Reiser> alleTurer = await _DB.Reiser.ToListAsync();
            foreach (var turen in alleTurer)
            {
                if (reise.FraSted.Equals(turen.FraSted.StedsNavn) &&
                    reise.TilSted.Equals(turen.TilSted.StedsNavn) &&
                    reise.Tid.Equals(turen.Tid) && reise.Dato.Equals(turen.Dato))
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> OppdatereReise(Reiser reise)
        {
            try
            {
                var finnesReise = SjekkReise(reise);
                finnesReise.Wait();
                bool isValid = finnesReise.Result;
                Console.WriteLine(isValid);
                // Det allerede finnes en reise
                if (isValid) {
                    return false;
                }

                int turIDFra = 0;
                int turIDTil = 0;
                List<Sted> alleSteder = await _DB.Steder.ToListAsync();
                foreach (var sted in alleSteder)
                {
                    if (reise.FraSted.StedsNavn.Equals(sted.StedsNavn))
                    {
                        Console.WriteLine("inne i første if");
                        turIDFra = sted.SId;
                    }

                    if (reise.TilSted.StedsNavn.Equals(sted.StedsNavn))
                    {
                        Console.WriteLine("inne i andre if");
                        turIDTil = sted.SId;
                    }
                }
                Sted funnetTurFra = _DB.Steder.Find(turIDFra);
                Sted funnetTurTil = _DB.Steder.Find(turIDTil);

                var endreObjekt = await _DB.Reiser.FindAsync(reise.RId);
                endreObjekt.FraSted = funnetTurFra;
                endreObjekt.TilSted = funnetTurTil;
                endreObjekt.Dato = reise.Dato;
                endreObjekt.Tid = reise.Tid;
                endreObjekt.PrisBarn = reise.PrisBarn;
                endreObjekt.PrisStudent = reise.PrisStudent;
                endreObjekt.PrisVoksen = reise.PrisVoksen;
                await _DB.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;

            }

            return true;
        }

        public async Task<bool> SlettReise(int rId)
        {
            try
            {
                Reiser enDBKunde = await _DB.Reiser.FindAsync(rId);
                _DB.Reiser.Remove(enDBKunde);
                await _DB.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> OpprettReise(Reiser reise)
        {
            try
            {
                int turIDFra = 0;
                int turIDTil = 0;
                List<Sted> alleSteder = await _DB.Steder.ToListAsync();
                foreach (var sted in alleSteder)
                {
                    Console.WriteLine("alle steder");
                    Console.WriteLine(sted.StedsNavn);
                    Console.WriteLine("Frasted");
                    Console.WriteLine(reise.FraSted.StedsNavn);
                    Console.WriteLine("Tilsted");
                    Console.WriteLine(reise.TilSted.StedsNavn);
                    if (reise.FraSted.StedsNavn.Equals(sted.StedsNavn))
                    {
                        Console.WriteLine("inne i første if");
                        turIDFra = sted.SId;
                    }

                    if (reise.TilSted.StedsNavn.Equals(sted.StedsNavn))
                    {
                        Console.WriteLine("inne i andre if");
                        turIDTil = sted.SId;
                    }
                }
                Sted funnetTurFra = _DB.Steder.Find(turIDFra);
                Sted funnetTurTil = _DB.Steder.Find(turIDTil);





                Console.WriteLine("INPUT");
                Console.WriteLine(reise.FraSted.StedsNavn);
                Console.WriteLine(reise.TilSted.StedsNavn);
                Console.WriteLine(reise.Dato);
                Console.WriteLine(reise.Tid);
                Console.WriteLine(reise.PrisBarn);
                Console.WriteLine(reise.PrisStudent);
                Console.WriteLine(reise.PrisVoksen);

                var fraSted = new Sted { StedsNavn = reise.FraSted.StedsNavn };
                var tilSted = new Sted { StedsNavn = reise.TilSted.StedsNavn };

                /*Console.WriteLine("PRINTER UT");
                Console.WriteLine(fraSted.StedsNavn);
                Console.WriteLine(tilSted.StedsNavn);*/
                Sted copyStedFra = reise.FraSted;
                Sted copyStedTil = reise.TilSted;


                Reiser nyReise = new Reiser();
                /* nyReise.FraSted = reise.FraSted;
                 nyReise.TilSted = reise.TilSted;*/
                /*nyReise.FraSted = reise.FraSted.StedsNavn;
                nyReise.TilSted.StedsNavn = reise.TilSted.StedsNavn;*/
                nyReise.FraSted = funnetTurFra;
                nyReise.TilSted = funnetTurTil;
                nyReise.Dato = reise.Dato;
                nyReise.Tid = reise.Tid;
                nyReise.PrisBarn = reise.PrisBarn;
                nyReise.PrisStudent = reise.PrisStudent;
                nyReise.PrisVoksen = reise.PrisVoksen;
                


                //var reise1 = new Reiser { FraSted = fraSted, TilSted = tilSted, Dato = reise.Dato, Tid = reise.Tid, PrisBarn = reise.PrisBarn, PrisStudent = reise.PrisStudent, PrisVoksen = reise.PrisVoksen };
                /*Console.WriteLine("Reiser:");
                Console.WriteLine(reise1.FraSted);
                Console.WriteLine(reise1.TilSted);*/
                _DB.Reiser.Add(nyReise);
                await _DB.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public async Task<List<Sted>> GetAllDestinasjoner(string avgang)
        {
            List<Reiser> alleReiser = await _DB.Reiser.ToListAsync();
            var endeStasjon = new List<Sted>();

            foreach (var reise in alleReiser)
            {
                if (avgang.Equals(reise.FraSted.StedsNavn))
                {
                    endeStasjon.Add(reise.TilSted);
                }
            }
            return endeStasjon;
        }


        public async Task<bool> Lagre(KundeOrdre innOrdre)
        {
            int turID = 0;
            List<Reiser> alleTurer = await _DB.Reiser.ToListAsync();
            foreach (var turen in alleTurer)
            {
                if (innOrdre.FraSted.Equals(turen.FraSted.StedsNavn) &&
                    innOrdre.TilSted.Equals(turen.TilSted.StedsNavn) &&
                    innOrdre.Tid.Equals(turen.Tid) && innOrdre.Dato.Equals(turen.Dato))
                {
                    turID = turen.RId;
                }
            }
            Reiser funnetTur = _DB.Reiser.Find(turID);

            double totalpris = (innOrdre.AntallBarn * funnetTur.PrisBarn) + (innOrdre.AntallVoksne * funnetTur.PrisVoksen);


            int kundeID = 0;
            List<Kunder> alleKunder = await _DB.Kunder.ToListAsync();

            foreach (var kunde in alleKunder)
            {
                if (innOrdre.Fornavn.Equals(kunde.Fornavn) &&
                    innOrdre.Etternavn.Equals(kunde.Etternavn))
                {
                    kundeID = kunde.KId;
                }
            }
            try
            {
                var nyBestillingRad = new Ordre();
                nyBestillingRad.AntallBarn = innOrdre.AntallBarn;
                nyBestillingRad.AntallVoksne = innOrdre.AntallVoksne;
                nyBestillingRad.TotalPris = totalpris;
                nyBestillingRad.Reiser = funnetTur;


                Kunder funnetKunde = await _DB.Kunder.FindAsync(kundeID);

                if (funnetKunde == null)
                {
                    var kundeRad = new Kunder();
                    kundeRad.Fornavn = innOrdre.Fornavn;
                    kundeRad.Etternavn = innOrdre.Etternavn;
                    _DB.Kunder.Add(kundeRad);
                    await _DB.SaveChangesAsync();
                    nyBestillingRad.Kunder = kundeRad;

                }
                else
                {
                    nyBestillingRad.Kunder = funnetKunde;
                }
                _DB.Ordre.Add(nyBestillingRad);
                await _DB.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        public async Task<List<Reiser>> HentAlle()
        {
            List<Reiser> alleTurer = await _DB.Reiser.ToListAsync();


            return alleTurer;
        }

        public static byte[] LagHash(string passord, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                                password: passord,
                                salt: salt,
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 1000,
                                numBytesRequested: 32);
        }

        public static byte[] LagSalt()
        {
            var csp = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csp.GetBytes(salt);
            return salt;
        }

        public async Task<bool> LoggInn(Bruker bruker)
        {
            try
            {
                Console.WriteLine(bruker.Brukernavn);
                Console.WriteLine(bruker.Passord);
                Brukere funnetBruker = await _DB.Brukere.FirstOrDefaultAsync(b => b.Brukernavn == bruker.Brukernavn);
                // sjekk passordet
                byte[] hash = LagHash(bruker.Passord, funnetBruker.Salt);
                bool ok = hash.SequenceEqual(funnetBruker.Passord);
                if (ok)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }

        //POSTMAN
        public async Task<List<Kunder>> GetAlleKunder()
        {
            List<Kunder> alleKunder = await _DB.Kunder.ToListAsync();
            foreach (var i in alleKunder)
            {
                System.Diagnostics.Debug.WriteLine(i.Fornavn);
            }
            return alleKunder;
        }

        public async Task<List<Ordre>> GetAlleOrdre()
        {
            List<Ordre> alleOrdre = await _DB.Ordre.ToListAsync();
            foreach (var i in alleOrdre)
            {
                System.Diagnostics.Debug.WriteLine(i.Kunder.Fornavn);
            }
            return alleOrdre;
        }

        public async Task<List<Sted>> GetAlleSteder()
        {
            List<Sted> alleSteder = await _DB.Steder.ToListAsync();
            foreach (var i in alleSteder)
            {
                System.Diagnostics.Debug.WriteLine(i.StedsNavn);
            }
            return alleSteder;
        }

        public async Task<List<Brukere>> GetAlleBrukere()
        {
            List<Brukere> alleBrukere = await _DB.Brukere.ToListAsync();
            foreach (var i in alleBrukere)
            {
                System.Diagnostics.Debug.WriteLine(i.Brukernavn);
            }
            return alleBrukere;
        }

        //POST requests
        public async Task<bool> OpprettKunde(Kunder innKunde)
        {
            Kunder nyKunde = new Kunder();
            nyKunde.Fornavn = innKunde.Fornavn;
            nyKunde.Etternavn = innKunde.Etternavn;
            _DB.Kunder.Add(nyKunde);
            await _DB.SaveChangesAsync();
            return true;

        }

        public async Task<bool> OpprettSted(Sted innSted)
        {
            Sted nySted = new Sted();
            nySted.StedsNavn = innSted.StedsNavn;
            _DB.Steder.Add(nySted);
            await _DB.SaveChangesAsync();
            return true;
        }

        //DELETE Ordre

        public async Task<bool> SlettOrdre(int oId)
        {
            try
            {
                Ordre enOrdre = await _DB.Ordre.FindAsync(oId);
                _DB.Ordre.Remove(enOrdre);
                await _DB.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Update ordre, kunde, sted

        public async Task<bool> EndreKunde(Kunder endreKunde)
        {
            try
            {
                var kunder = await _DB.Kunder.FindAsync(endreKunde.KId);
                kunder.Fornavn = endreKunde.Fornavn;
                kunder.Etternavn = endreKunde.Etternavn;
                await _DB.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;

            }

            return true;
        }

        public async Task<bool> EndreOrdre(Ordre endreOrdre)
        {
            try
            {
                var ordre = await _DB.Ordre.FindAsync(endreOrdre.OId);
                ordre.AntallBarn = endreOrdre.AntallBarn;
                ordre.AntallStudent = endreOrdre.AntallStudent;
                ordre.AntallVoksne = endreOrdre.AntallVoksne;
                ordre.TotalPris = endreOrdre.TotalPris;
                ordre.Kunder.Fornavn = endreOrdre.Kunder.Fornavn;
                ordre.Kunder.Etternavn = endreOrdre.Kunder.Etternavn;
                ordre.Reiser.FraSted = endreOrdre.Reiser.FraSted;
                ordre.Reiser.TilSted = endreOrdre.Reiser.TilSted;
                ordre.Reiser.Dato = endreOrdre.Reiser.Dato;
                ordre.Reiser.Tid = endreOrdre.Reiser.Tid;
                ordre.Reiser.PrisBarn = endreOrdre.Reiser.PrisBarn;
                ordre.Reiser.PrisStudent = endreOrdre.Reiser.PrisStudent;
                ordre.Reiser.PrisVoksen = endreOrdre.Reiser.PrisVoksen;
                await _DB.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }








        /*public async Task<bool> EndreSted(Sted endreSted)
        {
            try
            {
                var steder = await _DB.Kunder.FindAsync(endreSted.SId);
                steder.Fornavn = endreSted.StedsNavn;
                await _DB.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;

            }

            return true;
        }*/







        /* Slett kunde -> fungerer delvis
         * public async Task<bool> SlettKunde(int kId)
        {
            try
            {   
                Reiser enKunde = await _DB.Reiser.FindAsync(kId);
                _DB.Reiser.Remove(enKunde);
                await _DB.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }*/




        /*public async Task<bool> SlettSted(int SId)
        {
            try
            {
                Sted enSted = await _DB.Steder.FindAsync(SId);
                _DB.Steder.Remove(enSted);
                await _DB.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }*/
    }
}
