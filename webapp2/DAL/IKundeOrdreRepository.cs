using MappeInnlevering_1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapp2.Models;

namespace MappeInnlevering_1.DAL
{
    public interface IKundeOrdreRepository
    {
        //Task<List<Reise>> HentAlle();

        Task<List<Reiser>> GetAlleReiser();
        Task<List<Sted>> GetAllSteder();

        Task<bool> OppdatereReise(Reiser reise);

        Task<List<Sted>> GetAllDestinasjoner(string avgang);

        Task<bool> Lagre(KundeOrdre innOrdre);

        Task<bool> SlettReise(int RId);

        Task<bool> OpprettReise(Reiser reise);

        Task<bool> LoggInn(Bruker bruker);

        //POSTMAN

        //Get requests
        Task<List<Kunder>> GetAlleKunder();

        Task<List<Ordre>> GetAlleOrdre();

        Task<List<Sted>> GetAlleSteder();

        Task<List<Brukere>> GetAlleBrukere();

        //Post Requests
        Task<bool> OpprettKunde(Kunder innKunde);

        //Not needed for ordre, created on client side
        Task<bool> OpprettSted(Sted innSted);

        //Delete

        Task<bool> SlettOrdre(int oId);
        //Task<bool> SlettKunde(int kId);

        //Task<bool> SlettSted(int SId);

        //Endre
        Task<bool> EndreKunde(Kunder endreKunde);

        Task<bool> EndreOrdre(Ordre endreOrdre);

        //Task<bool> EndreSted(Sted endreSted);
    }
}