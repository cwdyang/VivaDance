/*
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Viva.CorporateSys.Dance.Datastore.Contexts;
using Viva.CorporateSys.Dance.Domain.Models;

namespace Viva.CorporateSys.Dance.Datastore.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Viva.CorporateSys.Dance.Datastore.Contexts.OneDanceMainContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        public void SeedDb(OneDanceMainContext context)
        {
            Seed(context);
        }



        protected override void Seed(Viva.CorporateSys.Dance.Datastore.Contexts.OneDanceMainContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.DeleteAll<Judging>();
            context.DeleteAll<JudgeCompetition>();
            context.DeleteAll<CompetitorCompetition>();
            
            context.DeleteAll<JudgingAssignment>();
            context.DeleteAll<Competition>();
            context.DeleteAll<Category>();
            context.DeleteAll<Division>();
            context.DeleteAll<Criterion>();
            context.DeleteAll<Competitor>();
            context.DeleteAll<Judge>();
            context.DeleteAll<Participant>();
            context.DeleteAll<Organisation>();


            context.DeleteAll<BaseObject>();

            var catTiming = new Criterion()
            {
                Caption = "Timing & Musicality",
                Id = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.Now,
                DisplaySequence = 5,
                ScorePoints = 20
            };

            var catTech = new Criterion()
            {
                Caption = "Technique",
                Id = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.Now,
                DisplaySequence = 4,
                ScorePoints = 20
            };

            var catDiff = new Criterion()
            {
                Caption = "Difficulty",
                Id = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.Now,
                DisplaySequence = 3,
                ScorePoints = 20
            };

            var catAppear = new Criterion()
            {
                Caption = "Appearance, Connection & Synchronicity",
                Id = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.Now,
                DisplaySequence = 2,
                ScorePoints = 20
            };

            var catChor = new Criterion()
            {
                Caption = "Originality of Choreography / OR Freestyle Vocabulary",
                Id = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.Now,
                DisplaySequence = 1,
                ScorePoints = 20
            };

            var catPenal = new Criterion()
            {
                Caption = "Penalty",
                Id = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.Now,
                DisplaySequence = 6,
                ScorePoints = 0
            };

            new List<Criterion>{catTiming,catTech,catDiff,catAppear,catChor,catPenal}.ForEach(x=>context.Criteria.Add(x));

            var divYouth = new Division()
            {
                Caption = "Youth",
                Id = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.Now,
                Categories = new HashSet<Category>
                {
                    new Category
                    {
                        Caption = "Under 18 Amateur Couples",
                        Requirements = "100% freestyle",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Under 18 Semi-Pro Couples",
                        Requirements = "50% choreography, 50% freestyle",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Under 18 Salsa Teams",
                        Requirements = "100% choreography, 90% choreography must be recognizable Salsa, music 100% Salsa",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Under 18 Latin Teams",
                        Requirements = "50% of routine must be a recognizable Latin style",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Under 18 Mixed Salsa Solo",
                        Requirements = "100% choreography",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Under 18 Mixed Latin Solo",
                        Requirements = "100% choreography",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Under 18 Salsa Solo Team",
                        Requirements = "100% choreography",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Under 18 Latin Solo Team",
                        Requirements = "100% choreography",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    }
}
            };

            var divAdultFree = new Division()
            {
                Caption = "Adult Freestyle",
                Id = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.Now,
                Categories = new HashSet<Category>
                {
                    new Category
                    {
                        Caption = "Beginner",
                        Requirements = "100% freestyle",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Intermediate",
                        Requirements = "100% freestyle",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Advanced",
                        Requirements = "100% freestyle",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    }
                }

            };

            var divAmatuer = new Division()
            {
                Caption = "Amateur",
                Id = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.Now,
                Categories = new HashSet<Category>
                {
                    new Category
                    {
                        Caption = "Salsa Solo Female",
                        Requirements = "100% choreography",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Salsa Solo Male",
                        Requirements = "100% choreography",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Salsa Couples",
                        Requirements = "50% choreography (qualifiers) , 50% improvisation (finals)",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Bachata Couples",
                        Requirements = "50% choreography (qualifiers) , 50% improvisation (finals)",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Zouk Couples",
                        Requirements = "50% choreography (qualifiers) , 50% improvisation (finals)",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Salsa Teams",
                        Requirements = "100% choreography",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    }
                }
            };

            var divSemiPro = new Division()
            {
                Caption = "Semi-Pro",
                Id = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.Now,
                Categories = new HashSet<Category>
                {
                    new Category
                    {
                        Caption = "Salsa Solo Female",
                        Requirements = "100% choreography",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Salsa Solo Male",
                        Requirements = "100% choreography",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Salsa Couples",
                        Requirements = "50% choreography (qualifiers) , 50% improvisation (finals)",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Bachata Couples",
                        Requirements = "50% choreography (qualifiers) , 50% improvisation (finals)",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Zouk Couples",
                        Requirements = "50% choreography (qualifiers) , 50% improvisation (finals)",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                }
            };

            var divProf = new Division()
            {
                Caption = "Professional",
                Id = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.Now,
                Categories = new HashSet<Category>
                {
                    new Category
                    {
                        Caption = "Salsa Solo Female",
                        Requirements = "100% choreography",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Salsa Solo Male",
                        Requirements = "100% choreography",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Salsa Couples",
                        Requirements = "50% choreography (qualifiers) , 50% improvisation (finals)",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Bachata Couples",
                        Requirements = "50% choreography (qualifiers) , 50% improvisation (finals)",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Zouk Couples",
                        Requirements = "50% choreography (qualifiers) , 50% improvisation (finals)",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                }
            };

            var divOpen = new Division()
            {
                Caption = "Open",
                Id = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.Now,
                Categories = new HashSet<Category>
                {
                    new Category
                    {
                        Caption = "Salsa Teams",
                        Requirements = "100% choreography",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Latin Teams",
                        Requirements = "100% choreography",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Salsa Shines Teams",
                        Requirements = "100% choreography",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Latin Shines Teams",
                        Requirements = "100% choreography",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Salsa Shines Duets",
                        Requirements = "100% choreography",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    }
                }
            };

            var divProAm = new Division()
            {
                Caption = "Pro/Am",
                Id = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.Now,
                Categories = new HashSet<Category>
                {
                    new Category
                    {
                        Caption = "Salsa Couples Lead Pro",
                        Requirements = "50% choreography (qualifiers) , 50% improvisation (finals)",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    },
                    new Category
                    {
                        Caption = "Salsa Couples Follower Pro",
                        Requirements = "50% choreography (qualifiers) , 50% improvisation (finals)",
                        Id = Guid.NewGuid(),
                        CreatedOn = DateTimeOffset.Now
                    }
                }
            };

            new List<Division>{divYouth,divAdultFree,divAmatuer,divSemiPro,divProf,divOpen,divProAm}.ForEach(x=>context.Divisions.AddOrUpdate(x));

            var orgVivaDance = new Organisation()
            {
                Id = Guid.NewGuid(),
                Caption = "VivaDanceSchool"
            };

            var orgVivaJudges = new Organisation()
            {
                Id = Guid.NewGuid(),
                Caption = "VivaJudges"
            };

            new List<Organisation> { orgVivaDance }.ForEach(x => context.Organisations.Add(x));



            var judgeGML = new Judge
            {
                Id = Guid.NewGuid(),
                EntityName = "VivaJudges",
                Email = "Greydis",
                FirstName = "Greydis Montero",
                LastName = "Liranza",
                EntityNumber = 1,
                JudgeType = JudgeType.Head,
                Organisation = orgVivaJudges,
                JudgingAssignments = new HashSet<JudgingAssignment>
                {
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = catTiming
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = catChor
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = catAppear
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = catPenal
                    }
                }
            };
            var judgeJCOS = new Judge
            {
                Id = Guid.NewGuid(),
                EntityName = "VivaJudges",
                Email = "Juan",
                FirstName = "Juan Carlos Ospina",
                LastName = "Sanchez",
                EntityNumber = 2,
                JudgeType = JudgeType.Normal,
                Organisation = orgVivaJudges,
                JudgingAssignments = new HashSet<JudgingAssignment>
                {
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = catDiff
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = catTech
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = catChor
                    }
                }
            };
            var judgeSPK = new Judge
            {
                Id = Guid.NewGuid(),
                EntityName = "VivaJudges",
                Email = "Sharon",
                FirstName = "Sharon Pakir",
                LastName = "Krygger",
                EntityNumber = 3,
                JudgeType = JudgeType.Normal,
                Organisation = orgVivaJudges,
                JudgingAssignments = new HashSet<JudgingAssignment>
                {
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = catTiming
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = catTech
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = catChor
                    }
                }
            };
            var judgeAB = new Judge
            {
                Id = Guid.NewGuid(),
                EntityName = "VivaJudges",
                Email = "Alex",
                FirstName = "Alex",
                LastName = "Bryan",
                EntityNumber = 4,
                JudgeType = JudgeType.Normal,
                Organisation = orgVivaJudges,
                JudgingAssignments = new HashSet<JudgingAssignment>
                {
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = catDiff
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = catTech
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = catAppear
                    }
                }
            };
            var judgeVR = new Judge
            {
                Id = Guid.NewGuid(),
                EntityName = "VivaJudges",
                Email = "J5",
                FirstName = "Vivio",
                LastName = "Ramos",
                EntityNumber = 5,
                JudgeType = JudgeType.Normal,
                Organisation = orgVivaJudges,
                JudgingAssignments = new HashSet<JudgingAssignment>
                {
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = catTiming
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = catDiff
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = catTech
                    }
                }
            };

            new List<Judge>{judgeGML,judgeJCOS,judgeSPK,judgeAB,judgeVR}.ForEach(x=>context.Judges.AddOrUpdate(x));

            var comptrAveoLatinLadiesTeam = new Competitor{Id = Guid.NewGuid(),EntityName = "Aveo Latin Ladies Team ",EntityNumber = 2,Email ="AveoLatinLadiesTeam",CompetitorType = CompetitorType.CouplesTeam,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrAveoLatinTeam = new Competitor{Id = Guid.NewGuid(),EntityName = "Aveo Latin Team ",EntityNumber = 3,Email ="AveoLatinTeam",CompetitorType = CompetitorType.CouplesTeam,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrAveoMensShinesTeam = new Competitor{Id = Guid.NewGuid(),EntityName = "Aveo Men�s Shines Team ",EntityNumber = 4,Email ="AveoMen�sShinesTeam",CompetitorType = CompetitorType.CouplesTeam,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrAveoOpenSalsaTeam = new Competitor{Id = Guid.NewGuid(),EntityName = "Aveo Open Salsa Team ",EntityNumber = 5,Email ="AveoOpenSalsaTeam",CompetitorType = CompetitorType.CouplesTeam,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrAveoTeam = new Competitor{Id = Guid.NewGuid(),EntityName = "Aveo Team ",EntityNumber = 6,Email ="AveoTeam",CompetitorType = CompetitorType.CouplesTeam,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrCorneliaMuandMitchellKwan = new Competitor{Id = Guid.NewGuid(),EntityName = "Cornelia Mu and Mitchell Kwan ",EntityNumber = 7,Email ="CorneliaMuandMitchellKwan",CompetitorType = CompetitorType.Couple,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrDharshanaRatnayakeandDeepikaGoundar = new Competitor{Id = Guid.NewGuid(),EntityName = "Dharshana Ratnayake and Deepika Goundar ",EntityNumber = 8,Email ="DharshanaRatnayakeandDeepikaGoundar",CompetitorType = CompetitorType.Couple,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrEDanceProductionsChaMania = new Competitor{Id = Guid.NewGuid(),EntityName = "E Dance Productions Cha Mania ",EntityNumber = 9,Email ="EDanceProductionsChaMania",CompetitorType = CompetitorType.CouplesTeam,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrEloiseGantuangcoandVictorZapata = new Competitor{Id = Guid.NewGuid(),EntityName = "Eloise Gantuangco and Victor Zapata ",EntityNumber = 10,Email ="EloiseGantuangcoandVictorZapata",CompetitorType = CompetitorType.Couple,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrEmilyWoodfieldandSoniaHems = new Competitor{Id = Guid.NewGuid(),EntityName = "Emily Woodfield and Sonia Hems",EntityNumber = 11,Email ="EmilyWoodfieldandSoniaHems",CompetitorType = CompetitorType.Couple,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrHeidiConeandDanielMunro = new Competitor{Id = Guid.NewGuid(),EntityName = "Heidi Cone and Daniel Munro ",EntityNumber = 12,Email ="HeidiConeandDanielMunro",CompetitorType = CompetitorType.Couple,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrIrinaKapeli = new Competitor{Id = Guid.NewGuid(),EntityName = "Irina Kapeli ",EntityNumber = 13,Email ="IrinaKapeli",CompetitorType = CompetitorType.Soloist,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrIzadoraCampos = new Competitor{Id = Guid.NewGuid(),EntityName = "Izadora Campos ",EntityNumber = 14,Email ="IzadoraCampos",CompetitorType = CompetitorType.Soloist,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrJeremySim = new Competitor{Id = Guid.NewGuid(),EntityName = "Jeremy Sim ",EntityNumber = 15,Email ="JeremySim",CompetitorType = CompetitorType.Soloist,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrJeremySimandMiaYatiswara = new Competitor{Id = Guid.NewGuid(),EntityName = "Jeremy Sim and Mia Yatiswara ",EntityNumber = 16,Email ="JeremySimandMiaYatiswara",CompetitorType = CompetitorType.Couple,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrJoanneMijaresSupelana = new Competitor{Id = Guid.NewGuid(),EntityName = "Joanne Mijares-Supelana ",EntityNumber = 17,Email ="JoanneMijares-Supelana",CompetitorType = CompetitorType.Soloist,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrJordanParanihiandKenHudsonStardoslocos = new Competitor{Id = Guid.NewGuid(),EntityName = "Jordan Paranihi and Ken Hudson (Star dos locos)",EntityNumber = 18,Email ="JordanParanihiandKenHudson(Stardoslocos)",CompetitorType = CompetitorType.Couple,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrJulianZhu = new Competitor{Id = Guid.NewGuid(),EntityName = "Julian Zhu ",EntityNumber = 19,Email ="JulianZhu",CompetitorType = CompetitorType.Soloist,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrLarissaRaukawaSoniaHems = new Competitor{Id = Guid.NewGuid(),EntityName = "Larissa RaukawaSonia Hems ",EntityNumber = 20,Email ="LarissaRaukawaSoniaHems",CompetitorType = CompetitorType.CouplesTeam,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrLatinAddictionBachatangoTeam = new Competitor{Id = Guid.NewGuid(),EntityName = "Latin Addiction Bachatango Team ",EntityNumber = 21,Email ="LatinAddictionBachatangoTeam",CompetitorType = CompetitorType.CouplesTeam,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrLatinAddictionChaChaLadies = new Competitor{Id = Guid.NewGuid(),EntityName = "Latin Addiction Cha Cha Ladies",EntityNumber = 22,Email ="LatinAddictionChaChaLadies",CompetitorType = CompetitorType.CouplesTeam,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrLatinAddictoinSalsaLadies = new Competitor{Id = Guid.NewGuid(),EntityName = "Latin Addictoin Salsa Ladies ",EntityNumber = 23,Email ="LatinAddictoinSalsaLadies",CompetitorType = CompetitorType.CouplesTeam,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrLatinissimoMensShines = new Competitor{Id = Guid.NewGuid(),EntityName = "Latinissimo Mens Shines ",EntityNumber = 24,Email ="LatinissimoMensShines",CompetitorType = CompetitorType.CouplesTeam,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrLatinissimoZoukTeam = new Competitor{Id = Guid.NewGuid(),EntityName = "Latinissimo Zouk Team",EntityNumber = 25,Email ="LatinissimoZoukTeam",CompetitorType = CompetitorType.CouplesTeam,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrMaiDahlberg = new Competitor{Id = Guid.NewGuid(),EntityName = "Mai Dahlberg ",EntityNumber = 26,Email ="MaiDahlberg",CompetitorType = CompetitorType.Soloist,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrMaiDahlbergAMandGinoGiancarloMayaute = new Competitor{Id = Guid.NewGuid(),EntityName = "Mai Dahlberg(AM) and Gino Giancarlo Mayaute ",EntityNumber = 27,Email ="MaiDahlberg(AM)andGinoGiancarloMayaute",CompetitorType = CompetitorType.Couple,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrMiaYatiswaraEmilyWoodfield = new Competitor{Id = Guid.NewGuid(),EntityName = "Mia YatiswaraEmily Woodfield ",EntityNumber = 28,Email ="MiaYatiswaraEmilyWoodfield",CompetitorType = CompetitorType.Soloist,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrNicolaTaylorandJuanSandoval = new Competitor{Id = Guid.NewGuid(),EntityName = "Nicola Taylor and Juan Sandoval ",EntityNumber = 29,Email ="NicolaTaylorandJuanSandoval",CompetitorType = CompetitorType.Couple,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrRichieNanalesAmandEloiseGantuangco = new Competitor{Id = Guid.NewGuid(),EntityName = "Richie Nanales( Am ) and Eloise Gantuangco ",EntityNumber = 30,Email ="RichieNanales(Am)andEloiseGantuangco",CompetitorType = CompetitorType.Couple,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrRitmosCandentesfromSalsaLatina = new Competitor{Id = Guid.NewGuid(),EntityName = "Ritmos Candentes from Salsa Latina ",EntityNumber = 31,Email ="RitmosCandentesfromSalsaLatina",CompetitorType = CompetitorType.Soloist,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrSalsaRevolutionfromSalsaLatina = new Competitor{Id = Guid.NewGuid(),EntityName = "Salsa Revolution from Salsa Latina ",EntityNumber = 32,Email ="SalsaRevolutionfromSalsaLatina",CompetitorType = CompetitorType.Soloist,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrSaraDjuricandSebastianVera = new Competitor{Id = Guid.NewGuid(),EntityName = "Sara Djuric and Sebastian Vera ",EntityNumber = 33,Email ="SaraDjuricandSebastianVera",CompetitorType = CompetitorType.Couple,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrScottSuenandMiaYatiswara = new Competitor{Id = Guid.NewGuid(),EntityName = "Scott Suen and Mia Yatiswara ",EntityNumber = 34,Email ="ScottSuenandMiaYatiswara",CompetitorType = CompetitorType.Couple,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrSweetAzucarBachaTango = new Competitor{Id = Guid.NewGuid(),EntityName = "Sweet Azucar! BachaTango ",EntityNumber = 35,Email ="SweetAzucar!BachaTango",CompetitorType = CompetitorType.CouplesTeam,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrVivaBachataTeam = new Competitor{Id = Guid.NewGuid(),EntityName = "Viva Bachata Team",EntityNumber = 36,Email ="VivaBachataTeam",CompetitorType = CompetitorType.CouplesTeam,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrVIvaBlack = new Competitor{Id = Guid.NewGuid(),EntityName = "VIva Black ",EntityNumber = 37,Email ="VIvaBlack",CompetitorType = CompetitorType.CouplesTeam,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrVivaChocolat = new Competitor{Id = Guid.NewGuid(),EntityName = "Viva Chocolat ",EntityNumber = 38,Email ="VivaChocolat",CompetitorType = CompetitorType.CouplesTeam,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrVyaraBridgemanandJorgeSequeiros = new Competitor{Id = Guid.NewGuid(),EntityName = "Vyara Bridgeman and Jorge Sequeiros  ",EntityNumber = 39,Email ="VyaraBridgemanandJorgeSequeiros",CompetitorType = CompetitorType.Couple,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};
            var comptrYanitaMcLeayandJulianZhu = new Competitor{Id = Guid.NewGuid(),EntityName = "Yanita McLeay and Julian Zhu ",EntityNumber = 40,Email ="YanitaMcLeayandJulianZhu",CompetitorType = CompetitorType.Couple,Organisation = orgVivaDance,MobileNumber = "021",FirstName = "",LastName = ""};

            new List<Competitor>{comptrYanitaMcLeayandJulianZhu,comptrVyaraBridgemanandJorgeSequeiros,comptrVivaChocolat,comptrVIvaBlack,comptrVivaBachataTeam,comptrSweetAzucarBachaTango,comptrScottSuenandMiaYatiswara,comptrSaraDjuricandSebastianVera,comptrSalsaRevolutionfromSalsaLatina,comptrRitmosCandentesfromSalsaLatina,comptrRichieNanalesAmandEloiseGantuangco,comptrNicolaTaylorandJuanSandoval,comptrMiaYatiswaraEmilyWoodfield,comptrMaiDahlbergAMandGinoGiancarloMayaute,comptrMaiDahlberg,comptrLatinissimoZoukTeam,comptrLatinissimoMensShines,comptrLatinAddictoinSalsaLadies,comptrLatinAddictionChaChaLadies,comptrLatinAddictionBachatangoTeam,comptrLarissaRaukawaSoniaHems,comptrJulianZhu,comptrJordanParanihiandKenHudsonStardoslocos,comptrJoanneMijaresSupelana,comptrJeremySimandMiaYatiswara,comptrJeremySim,comptrIzadoraCampos,comptrIrinaKapeli,comptrHeidiConeandDanielMunro,comptrEmilyWoodfieldandSoniaHems,comptrEloiseGantuangcoandVictorZapata,comptrEDanceProductionsChaMania,comptrDharshanaRatnayakeandDeepikaGoundar,comptrCorneliaMuandMitchellKwan,comptrAveoTeam,comptrAveoOpenSalsaTeam,comptrAveoMensShinesTeam,comptrAveoLatinTeam,comptrAveoLatinLadiesTeam,}.ForEach(x=>context.Competitors.Add(x));


            
            var judgeCompetitions = CreateJudgeCompetitions(new List<Judge>{ judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR});
            var competitorCompetitions = new HashSet<CompetitorCompetition>();
            var location = "10 Newton Rd";


            judgeCompetitions = CreateJudgeCompetitions(new List<Judge>{ judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR});
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrSaraDjuricandSebastianVera });
            var compSalsaCouplesAmateurSemiFinalChoreography = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Salsa Couples Amateur Semi Final Choreography", Category = divAmatuer.Categories.First(x=>x.Caption=="Salsa Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 4:30pm"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrDharshanaRatnayakeandDeepikaGoundar, comptrScottSuenandMiaYatiswara });
            var compSalsaCouplesSemiProSemiFinalChoreography = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Salsa Couples Semi Pro Semi Final Choreography ", Category = divSemiPro.Categories.First(x=>x.Caption=="Salsa Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 4:33pm"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrVyaraBridgemanandJorgeSequeiros, comptrEloiseGantuangcoandVictorZapata });
            var compSalsaCouplesProfessionalSemiFinalChoreography = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Salsa Couples Professional Semi Final Choreography ", Category = divProf.Categories.First(x=>x.Caption=="Salsa Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 4:40pm"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrSaraDjuricandSebastianVera }); 
            var compSalsaCouplesAmateurFinalImprovisation = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Salsa Couples Amateur Final Improvisation", Category = divAmatuer.Categories.First(x => x.Caption == "Salsa Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 4:47pm "), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrDharshanaRatnayakeandDeepikaGoundar, comptrScottSuenandMiaYatiswara });
            var compSalsaCouplesSemiProFinalImprovisation = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Salsa Couples Semi-Pro Final Improvisation ", Category = divSemiPro.Categories.First(x => x.Caption == "Salsa Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 4:50pm"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrVyaraBridgemanandJorgeSequeiros, comptrEloiseGantuangcoandVictorZapata });
            var compSalsaCouplesProfessionalFinalImprovisation = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Salsa Couples Professional Final Improvisation ", Category = divProf.Categories.First(x => x.Caption == "Salsa Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 4:56pm "), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrSaraDjuricandSebastianVera, comptrYanitaMcLeayandJulianZhu, comptrNicolaTaylorandJuanSandoval });
            var compBachataCouplesAmateurSemiFinalChoreography = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Bachata Couples Amateur Semi Final Choreography", Category = divAmatuer.Categories.First(x => x.Caption == "Bachata Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 5:02pm"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrVyaraBridgemanandJorgeSequeiros });
            var compBachataProfessionalCouplesSemiFinalChoreography = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Bachata Professional Couples Semi Final Choreography ", Category = divProf.Categories.First(x => x.Caption == "Bachata Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 5:14pm"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrSaraDjuricandSebastianVera, comptrYanitaMcLeayandJulianZhu, comptrNicolaTaylorandJuanSandoval });
            var compBachataCouplesAmateurFinalImprovisation = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Bachata Couples Amateur Final- Improvisation ", Category = divAmatuer.Categories.First(x => x.Caption == "Bachata Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 5:18pm"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrVyaraBridgemanandJorgeSequeiros });
            var compBachataProfessionalCouplesImprovisation = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Bachata Professional Couples Improvisation", Category = divProf.Categories.First(x => x.Caption == "Bachata Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 5:27pm"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrCorneliaMuandMitchellKwan, comptrHeidiConeandDanielMunro });
            var compZoukCouplesAmateurSemiFinalChoreography = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Zouk Couples Amateur Semi Final Choreography ", Category = divAmatuer.Categories.First(x => x.Caption == "Zouk Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 5:30pm"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrCorneliaMuandMitchellKwan, comptrHeidiConeandDanielMunro });
            var compZoukCouplesAmateurAmateurFinalImprovisation = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Zouk Couples Amateur Amateur Final Improvisation ", Category = divAmatuer.Categories.First(x => x.Caption == "Zouk Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 5:40pm"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrRichieNanalesAmandEloiseGantuangco, comptrMaiDahlbergAMandGinoGiancarloMayaute });
            var compProAmSemiFinalsChoreography = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Pro Am Semi Finals Choreography ", Category = divProAm.Categories.First(x => x.Caption == "Salsa Couples Lead Pro"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 5:46pm "), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrRichieNanalesAmandEloiseGantuangco, comptrMaiDahlbergAMandGinoGiancarloMayaute });
            var compProAmFinalsImprovisation = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Pro Am Finals Improvisation ", Category = divProAm.Categories.First(x => x.Caption == "Salsa Couples Lead Pro"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 5:54pm "), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrJoanneMijaresSupelana, comptrMaiDahlberg, comptrIzadoraCampos });
            var compAmateurSoloFemale = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Amateur Solo Female ", Category = divAmatuer.Categories.First(x => x.Caption == "Salsa Solo Female"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 6:20pm "), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrJulianZhu });
            var compAmateurSoloMale = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Amateur Solo Male ", Category = divAmatuer.Categories.First(x => x.Caption == "Salsa Solo Male"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 6:30pm "), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrMiaYatiswaraEmilyWoodfield });
            var compSemiProSoloFemale = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Semi Pro Solo Female ", Category = divSemiPro.Categories.First(x => x.Caption == "Salsa Solo Female"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 6:34pm"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrJeremySim });
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            var compSemiProSoloMale = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Semi Pro Solo Male ", Category = divSemiPro.Categories.First(x => x.Caption == "Salsa Solo Male"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 6:38pm "), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrIrinaKapeli, comptrLarissaRaukawaSoniaHems });
            var compProfessionalSoloFemale = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Professional Solo Female ", Category = divProf.Categories.First(x => x.Caption == "Salsa Solo Female"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 6:42pm "), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrJeremySimandMiaYatiswara, comptrJordanParanihiandKenHudsonStardoslocos, comptrEmilyWoodfieldandSoniaHems });
            var compOpenDuets = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Duets ", Category = divOpen.Categories.First(x => x.Caption == "Salsa Shines Duets"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 6:52pm "), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrAveoTeam });
            var compAmateurSalsaTeam = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Amateur Salsa Team ", Category = divAmatuer.Categories.First(x => x.Caption == "Salsa Teams"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 7:10pm "), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrVIvaBlack, comptrAveoOpenSalsaTeam });
            var compOpenSalsaTeams = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Salsa Teams ", Category = divOpen.Categories.First(x => x.Caption == "Salsa Teams"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 7:14pm "), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrLatinAddictionBachatangoTeam, comptrSweetAzucarBachaTango, comptrAveoLatinTeam, comptrLatinissimoZoukTeam, comptrVivaBachataTeam });
            var compOpenLatinTeam = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Latin Team ", Category = divOpen.Categories.First(x => x.Caption == "Latin Teams"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 7:22pm "), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrVivaChocolat, comptrLatinAddictoinSalsaLadies, comptrAveoMensShinesTeam, comptrLatinissimoMensShines, comptrRitmosCandentesfromSalsaLatina });
            var compOpenSalsaShinesTeams = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Salsa Shines Teams ", Category = divOpen.Categories.First(x => x.Caption == "Salsa Shines Teams"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 7:38pm "), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeGML, judgeJCOS, judgeSPK, judgeAB, judgeVR });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { comptrSalsaRevolutionfromSalsaLatina, comptrEDanceProductionsChaMania, comptrAveoLatinLadiesTeam, comptrRitmosCandentesfromSalsaLatina, comptrLatinAddictionChaChaLadies });
            var compOpenLatinShinesTeams = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Latin Shines Teams ", Category = divOpen.Categories.First(x => x.Caption == "Latin Shines Teams"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2014-11-15 7:58pm "), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            new List<Competition> { compOpenLatinShinesTeams, compOpenSalsaShinesTeams, compOpenLatinTeam, compOpenSalsaTeams, compAmateurSalsaTeam, compOpenDuets, compProfessionalSoloFemale, compSemiProSoloMale, compSemiProSoloFemale, compAmateurSoloMale, compAmateurSoloFemale, compProAmFinalsImprovisation, compProAmSemiFinalsChoreography, compZoukCouplesAmateurAmateurFinalImprovisation, compZoukCouplesAmateurSemiFinalChoreography, compBachataProfessionalCouplesImprovisation, compBachataCouplesAmateurFinalImprovisation, compBachataProfessionalCouplesSemiFinalChoreography, compBachataCouplesAmateurSemiFinalChoreography, compSalsaCouplesProfessionalFinalImprovisation, compSalsaCouplesSemiProFinalImprovisation, compSalsaCouplesAmateurFinalImprovisation, compSalsaCouplesProfessionalSemiFinalChoreography, compSalsaCouplesSemiProSemiFinalChoreography, compSalsaCouplesAmateurSemiFinalChoreography, }.ForEach(x => context.Competitions.Add(x));


            context.SaveChanges();
        }

        private static HashSet<CompetitorCompetition> CreateCompetitorCompetitions(List<Competitor> competitors)
        {
            var competitorCompetitions = new HashSet<CompetitorCompetition>();

            var sequence = 1;

            competitors.ForEach(x => competitorCompetitions.Add(new CompetitorCompetition
            {
                Id = Guid.NewGuid(),
                Competitor = x,
                Sequence = sequence++,
                CompetitorType = x.CompetitorType
            }));


            return competitorCompetitions;
        }

        private static HashSet<JudgeCompetition> CreateJudgeCompetitions(List<Judge> judges)
        {
            var judgeCompetitions = new HashSet<JudgeCompetition>();

            judges.ForEach(x => judgeCompetitions.Add(new JudgeCompetition
            {
                Id = Guid.NewGuid(),
                Judge = x,
                JudgeType = x.JudgeType
            }));

                
            return judgeCompetitions;
        }
    }
}
*/