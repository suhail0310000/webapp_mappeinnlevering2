using MappeInnlevering_1.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MappeInnlevering_1.Models
{
    public class DBInit
    {
        public static void Initialize(IApplicationBuilder app)
        {   
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DB>();

                // Oppretter og sletter databasen hver gang den skal seedes
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                //var sted = new Sted { StedsNavn= "Suhail " };


                //var datoAvgang1 = "05/11/2021";
                //var datoAvgang2 = "10/12/2021";
                //var dato3 = "01/01/2021";

                //var tid_1 = "10:00";
                //var tid_2 = "13:00";
                //var tid_3 = "21:00";

                //var land1 = "Tyskland";
                //var land2 = "Spania";

                //STEDER
                var Sted1 = new Sted { StedsNavn = "Oslo" };
                var Sted2 = new Sted { StedsNavn = "Kiel" };
                var Sted3 = new Sted { StedsNavn = "København" };

                context.Steder.Add(Sted1);
                context.Steder.Add(Sted2);
                context.Steder.Add(Sted3);


                //DATO
                string dato1 = "07.02.2021";
                string dato2 = "20.05.2021";
                //string dato3 = "31.09.2021";

                //TID
                string tid1 = "08:00";
                string tid2 = "12:00";
                //string tid3 = "18:00";

                //opprett reise
                // TID1 = 08:00 GIR 100 KR FOR BARN, 120 FOR STUDENT OG 200 FOR VOKSNE
                var reise1 = new Reiser { FraSted = Sted1, TilSted = Sted2, Dato = dato1, Tid = tid1, PrisBarn = 100, PrisStudent = 120, PrisVoksen = 200 };

                //EDRET KUN PÅ TIDEN. TID2 = 18:00, GIR 130 FOR BARN, 170 FOR STUDENT OG 300 FOR VOKSNE
                var reise2 = new Reiser { FraSted = Sted1, TilSted = Sted2, Dato = dato1, Tid = tid2, PrisBarn = 130, PrisStudent = 170, PrisVoksen = 300 };

                //ENDRET TIL DATO2, MED TID1
                var reise3 = new Reiser { FraSted = Sted1, TilSted = Sted2, Dato = dato2, Tid = tid1, PrisBarn = 100, PrisStudent = 120, PrisVoksen = 200 };

                //ENDRET TIL DATO2 , MED TID2 (øker priser)
                var reise4 = new Reiser { FraSted = Sted1, TilSted = Sted2, Dato = dato2, Tid = tid2, PrisBarn = 130, PrisStudent = 170, PrisVoksen = 300 };

                //ENDRET TILSTED TIL STED3
                var reise5 = new Reiser { FraSted = Sted1, TilSted = Sted3, Dato = dato1, Tid = tid1, PrisBarn = 100, PrisStudent = 120, PrisVoksen = 300 };

                //ENDRET TIL TID2 PÅ STED 3
                var reise6 = new Reiser { FraSted = Sted1, TilSted = Sted3, Dato = dato1, Tid = tid2, PrisBarn = 130, PrisStudent = 170, PrisVoksen = 300 };

                //ENDRET TIL DATO2 MED STED3
                var reise7 = new Reiser { FraSted = Sted1, TilSted = Sted3, Dato = dato2, Tid = tid1, PrisBarn = 100, PrisStudent = 120, PrisVoksen = 200 };

                //ENDRET TIL TID2 MED DATO2
                var reise8 = new Reiser { FraSted = Sted1, TilSted = Sted3, Dato = dato2, Tid = tid2, PrisBarn = 130, PrisStudent = 170, PrisVoksen = 300 };

                //ENDRET TIL FRASTED = STED2
                var reise9 = new Reiser { FraSted = Sted2, TilSted = Sted1, Dato = dato1, Tid = tid1, PrisBarn = 100, PrisStudent = 120, PrisVoksen = 200 };

                //ENDRET TIL TID2
                var reise10 = new Reiser { FraSted = Sted2, TilSted = Sted1, Dato = dato1, Tid = tid2, PrisBarn = 130, PrisStudent = 170, PrisVoksen = 300 };

                //ENDRET TIL DATO2
                var reise11 = new Reiser { FraSted = Sted2, TilSted = Sted1, Dato = dato2, Tid = tid1, PrisBarn = 100, PrisStudent = 120, PrisVoksen = 200 };

                //ENDRET TIL TID2 MED DATO2
                var reise12 = new Reiser { FraSted = Sted2, TilSted = Sted1, Dato = dato2, Tid = tid2, PrisBarn = 130, PrisStudent = 170, PrisVoksen = 300 };

                //ENDRER TILSTED TIL STED3
                var reise13 = new Reiser { FraSted = Sted2, TilSted = Sted3, Dato = dato1, Tid = tid1, PrisBarn = 100, PrisStudent = 120, PrisVoksen = 200 };
                var reise14 = new Reiser { FraSted = Sted2, TilSted = Sted3, Dato = dato1, Tid = tid2, PrisBarn = 130, PrisStudent = 170, PrisVoksen = 300 };
                var reise15 = new Reiser { FraSted = Sted2, TilSted = Sted3, Dato = dato2, Tid = tid1, PrisBarn = 100, PrisStudent = 120, PrisVoksen = 200 };
                var reise16 = new Reiser { FraSted = Sted2, TilSted = Sted3, Dato = dato2, Tid = tid2, PrisBarn = 130, PrisStudent = 170, PrisVoksen = 300 };

                //ENDRER FRASTED TIL STED 3
                var reise17 = new Reiser { FraSted = Sted3, TilSted = Sted1, Dato = dato1, Tid = tid1, PrisBarn = 100, PrisStudent = 120, PrisVoksen = 200 };
                var reise18 = new Reiser { FraSted = Sted3, TilSted = Sted1, Dato = dato1, Tid = tid2, PrisBarn = 130, PrisStudent = 170, PrisVoksen = 300 };
                var reise19 = new Reiser { FraSted = Sted3, TilSted = Sted1, Dato = dato2, Tid = tid1, PrisBarn = 100, PrisStudent = 120, PrisVoksen = 200 };
                var reise20 = new Reiser { FraSted = Sted3, TilSted = Sted1, Dato = dato2, Tid = tid2, PrisBarn = 130, PrisStudent = 170, PrisVoksen = 300 };

                //ENDRER TILSTED TIL STED2
                var reise21 = new Reiser { FraSted = Sted3, TilSted = Sted2, Dato = dato1, Tid = tid1, PrisBarn = 100, PrisStudent = 120, PrisVoksen = 200 };
                var reise22 = new Reiser { FraSted = Sted3, TilSted = Sted2, Dato = dato1, Tid = tid2, PrisBarn = 130, PrisStudent = 170, PrisVoksen = 300 };
                var reise23 = new Reiser { FraSted = Sted3, TilSted = Sted2, Dato = dato2, Tid = tid1, PrisBarn = 100, PrisStudent = 120, PrisVoksen = 200 };
                var reise24 = new Reiser { FraSted = Sted3, TilSted = Sted2, Dato = dato2, Tid = tid2, PrisBarn = 130, PrisStudent = 170, PrisVoksen = 300 };


                ;




                //var Ordre1 = new Ordre { AntallBarn = 1, AntallStudent= 1, AntallVoksne = 1, TotalPris= 100 };
                //var Ordre2 = new Ordre { AntallBarn = 1, AntallStudent = 1, AntallVoksne = 1, TotalPris = 100 };




                //var kunde1 = new Kunder { Fornavn = "Ole", Etternavn = "Hansen",  Ordre = Ordre1 };
                //var kunde2 = new Kunder { Fornavn = "Line", Etternavn = "Jensen",  Email = "Email2", Poststed = poststed2 };

                context.Reiser.Add(reise1);
                context.Reiser.Add(reise2);
                context.Reiser.Add(reise3);
                context.Reiser.Add(reise4);
                context.Reiser.Add(reise5);
                context.Reiser.Add(reise6);
                context.Reiser.Add(reise7);
                context.Reiser.Add(reise8);
                context.Reiser.Add(reise9);
                context.Reiser.Add(reise10);
                context.Reiser.Add(reise11);
                context.Reiser.Add(reise12);
                context.Reiser.Add(reise13);
                context.Reiser.Add(reise14);
                context.Reiser.Add(reise15);
                context.Reiser.Add(reise16);
                context.Reiser.Add(reise17);
                context.Reiser.Add(reise18);
                context.Reiser.Add(reise19);
                context.Reiser.Add(reise20);
                context.Reiser.Add(reise21);
                context.Reiser.Add(reise22);
                context.Reiser.Add(reise23);
                context.Reiser.Add(reise24);

                // lag en påoggingsbruker
                var bruker = new Brukere();
                bruker.Brukernavn = "Admin";
                string passord = "Test11";
                byte[] salt = KundeOrdreRepository.LagSalt();
                byte[] hash = KundeOrdreRepository.LagHash(passord, salt);
                bruker.Passord = hash;
                bruker.Salt = salt;
                context.Brukere.Add(bruker);


                //context.Kunder.Add(kunde2);

                context.SaveChanges();
            }
        }
    }
}
