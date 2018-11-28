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

    /// <summary>
    /// @package manager front, select the datastore project
    /// 
    /// PM> 
    /// Enable-Migrations
    /// Add-Migration Initial
    /// Update-Database
    /// $Env:RUN_SEED = "true"
    /// Get-Migrations
    /// $Env:RUN_SEED = "false"
    /// </summary>
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

        public void ClearJudgings(Viva.CorporateSys.Dance.Datastore.Contexts.OneDanceMainContext context)
        {
            context.DeleteAll<Judging>();
            context.SaveChanges();
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

            context.SaveChanges();

            #region Judging Criteria

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

            #endregion Judging Criteria

            //Update from spreadsheet
            #region DIVISIONS CATEGORIES


            HashSet<Category> categories = null;


            categories = new HashSet<Category>(); var divAdultFree = new Division() { Caption = "Adult Freestyle", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now, Categories = categories };
            categories = new HashSet<Category>(); var divAmatuer = new Division() { Caption = "Amateur", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now, Categories = categories };
            categories = new HashSet<Category>(); var divAmatuerChor = new Division() { Caption = "Amatuer Choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now, Categories = categories };
            categories = new HashSet<Category>(); var divAmatuerImprov = new Division() { Caption = "Amateur Improvisation", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now, Categories = categories };
            categories = new HashSet<Category>(); var divOpen = new Division() { Caption = "Open", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now, Categories = categories };
            categories = new HashSet<Category>(); var divProAm = new Division() { Caption = "Pro_Am", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now, Categories = categories };
            categories = new HashSet<Category>(); var divProf = new Division() { Caption = "Professional", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now, Categories = categories };
            categories = new HashSet<Category>(); var divProfChor = new Division() { Caption = "Professional Choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now, Categories = categories };
            categories = new HashSet<Category>(); var divProfImProv = new Division() { Caption = "Professional Improvisation", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now, Categories = categories };
            categories = new HashSet<Category>(); var divSemiPro = new Division() { Caption = "Semi_Pro", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now, Categories = categories };
            categories = new HashSet<Category>(); var divSemiProChor = new Division() { Caption = "Semi_Pro Choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now, Categories = categories };
            categories = new HashSet<Category>(); var divSemiProImprov = new Division() { Caption = "Semi_Pro Improvisation", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now, Categories = categories };
            categories = new HashSet<Category>(); var divYouth = new Division() { Caption = "Youth", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now, Categories = categories };



            divYouth.Categories.Add(new Category { Caption = "Youth Salsa Solo Mixed", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divYouth.Categories.Add(new Category { Caption = "Youth Latin Solo Mixed", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divYouth.Categories.Add(new Category { Caption = "Youth Salsa Solo Teams", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divYouth.Categories.Add(new Category { Caption = "Youth Latin Solo Teams", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divYouth.Categories.Add(new Category { Caption = "Youth Salsa Duets Mixed", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divYouth.Categories.Add(new Category { Caption = "Youth Latin Duets Mixed", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divAmatuer.Categories.Add(new Category { Caption = "Amateur Salsa Solo Female", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divSemiPro.Categories.Add(new Category { Caption = "Semi_Pro Salsa Solo Female", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divProf.Categories.Add(new Category { Caption = "Professional Salsa Solo Female", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divProf.Categories.Add(new Category { Caption = "Professional Salsa Solo Male", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divOpen.Categories.Add(new Category { Caption = "Open Latin Solo", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divSemiPro.Categories.Add(new Category { Caption = "Semi_Pro Salsa Couples", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divProf.Categories.Add(new Category { Caption = "Professional Salsa Couples", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divOpen.Categories.Add(new Category { Caption = "Open Salsa Couples", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divSemiPro.Categories.Add(new Category { Caption = "Semi_Pro Bachata Couples", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divProf.Categories.Add(new Category { Caption = "Professional Bachata Couples", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divOpen.Categories.Add(new Category { Caption = "Open Latin Couples", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divOpen.Categories.Add(new Category { Caption = "Open Salsa Shines Duets", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divOpen.Categories.Add(new Category { Caption = "Open Latin Shines Duets", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divProAm.Categories.Add(new Category { Caption = "Pro_Am Latin Couples", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divAmatuer.Categories.Add(new Category { Caption = "Amateur Salsa Teams", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divOpen.Categories.Add(new Category { Caption = "Open Salsa Teams", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divOpen.Categories.Add(new Category { Caption = "Open Latin Teams", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divOpen.Categories.Add(new Category { Caption = "Open Salsa Shines Teams", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divOpen.Categories.Add(new Category { Caption = "Open Latin Shines Teams", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });




            context.Divisions.AddOrUpdate(divAdultFree);
            context.Divisions.AddOrUpdate(divAmatuer);
            context.Divisions.AddOrUpdate(divAmatuerChor);
            context.Divisions.AddOrUpdate(divAmatuerImprov);
            context.Divisions.AddOrUpdate(divOpen);
            context.Divisions.AddOrUpdate(divProAm);
            context.Divisions.AddOrUpdate(divProf);
            context.Divisions.AddOrUpdate(divProfChor);
            context.Divisions.AddOrUpdate(divProfImProv);
            context.Divisions.AddOrUpdate(divSemiPro);
            context.Divisions.AddOrUpdate(divSemiProChor);
            context.Divisions.AddOrUpdate(divSemiProImprov);
            context.Divisions.AddOrUpdate(divYouth);



            #endregion DIVISIONS CATEGORIES

            //Static
            #region ORGANISATIONS

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

            #endregion ORGANISATIONS

            #region JUDGES

            /*
             * 
             * 1 = timing, choreo, appear
             * 2 = timing,difficulty,appear
             * 3 = timing,difficult,tech
             * 4 = difficulty,tech,choreo
             * 5 = tech,choreo,appear
             * 
             * head = penalty
             * 
            Timing & Musicality (20%) 1,2,3
            Difficulty (20%) 2,3,4
            Technique (20%) 3,4,5
            Originality of Choreography 1,4,5
            Appearance, Connection & Synchronicity (20%) 1,2,5
           
             */

            var judgeHead = new Judge
            {
                Id = Guid.NewGuid(),
                EntityName = "VivaJudges",
                Email = "judge0",
                FirstName = "Judge",
                LastName = "Head",
                EntityNumber = 1,
                JudgeType = JudgeType.Head,
                Organisation = orgVivaJudges,
                JudgingAssignments = new HashSet<JudgingAssignment>
                {
                    
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = catPenal
                    }
                }
            };
            var judge1 = new Judge
            {
                Id = Guid.NewGuid(),
                EntityName = "VivaJudges",
                Email = "judge1",
                FirstName = "Judge",
                LastName = "1",
                EntityNumber = 2,
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
                        Criterion = catAppear
                    }
                }
            };
            var judge2 = new Judge
            {
                Id = Guid.NewGuid(),
                EntityName = "VivaJudges",
                Email = "judge2",
                FirstName = "Judge",
                LastName = "2",
                EntityNumber = 3,
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
                        Criterion = catChor
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = catAppear
                    }
                }
            };
            var judge3 = new Judge
            {
                Id = Guid.NewGuid(),
                EntityName = "VivaJudges",
                Email = "judge3",
                FirstName = "Judge",
                LastName = "3",
                EntityNumber = 4,
                JudgeType = JudgeType.Normal,
                Organisation = orgVivaJudges,
                JudgingAssignments = new HashSet<JudgingAssignment>
                {
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = catTech
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
                    }
                }
            };
            var judge4 = new Judge
            {
                Id = Guid.NewGuid(),
                EntityName = "VivaJudges",
                Email = "judge4",
                FirstName = "Judge",
                LastName = "4",
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
                        Criterion = catChor 
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = catDiff
                    }
                }
            };
            var judge5 = new Judge
            {
                Id = Guid.NewGuid(),
                EntityName = "VivaJudges",
                Email = "judge5",
                FirstName = "Judge",
                LastName = "5",
                EntityNumber = 6,
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

            new List<Judge>{judgeHead,judge1,judge2,judge3,judge4,judge5}.ForEach(x=>context.Judges.AddOrUpdate(x));

            #endregion JUDGES

            //Update from spreadsheet
            #region COMPETITORS

            var comptrAcereBongo_SalsaConCoco = new Competitor { Id = Guid.NewGuid(), EntityName = "Acere Bongo", EntityNumber = 5, Email = "AcereBongo_SalsaConCoco", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrAdvancedAllianceDanceTeam_CarineMoraisandRafaelBarros_Brazil = new Competitor { Id = Guid.NewGuid(), EntityName = "Advanced Alliance Dance Team _ Carine Morais and Rafael Barros _ Brazil", EntityNumber = 5, Email = "AdvancedAllianceDanceTeam_CarineMoraisandRafaelBarros_Brazil", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrAlanaMace_RainesSuavesitas = new Competitor { Id = Guid.NewGuid(), EntityName = "Alana Mace", EntityNumber = 5, Email = "AlanaMace_RainesSuavesitas", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrAlinaSolomkinaandCorwinRuegg_EDP = new Competitor { Id = Guid.NewGuid(), EntityName = "Alina Solomkina and Corwin Ruegg", EntityNumber = 6, Email = "AlinaSolomkinaandCorwinRuegg_EDP", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrAndradaNeaguandAlbertoJuarez_PassionofExpression = new Competitor { Id = Guid.NewGuid(), EntityName = "Andrada Neagu and Alberto Juarez", EntityNumber = 6, Email = "AndradaNeaguandAlbertoJuarez_PassionofExpression", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrBachasalsa_SalsaConCoco = new Competitor { Id = Guid.NewGuid(), EntityName = "Bachasalsa", EntityNumber = 7, Email = "Bachasalsa_SalsaConCoco", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrCaitlinQuinn_LatinFire = new Competitor { Id = Guid.NewGuid(), EntityName = "Caitlin Quinn", EntityNumber = 8, Email = "CaitlinQuinn_LatinFire", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrCharlotteJaneBuccahanandCharmaineJoyBuccahan_RaineSymons = new Competitor { Id = Guid.NewGuid(), EntityName = "Charlotte Jane Buccahan and Charmaine Joy Buccahan", EntityNumber = 9, Email = "CharlotteJaneBuccahanandCharmaineJoyBuccahan_RaineSymons", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrDeborahChou_SalsaConCoco = new Competitor { Id = Guid.NewGuid(), EntityName = "Deborah Chou", EntityNumber = 10, Email = "DeborahChou_SalsaConCoco", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrDeborahChouandJeorgeSequeiros_SalsaConCoco = new Competitor { Id = Guid.NewGuid(), EntityName = "Deborah Chou and Jeorge Sequeiros", EntityNumber = 11, Email = "DeborahChouandJeorgeSequeiros_SalsaConCoco", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrEDanceProductions_EDP = new Competitor { Id = Guid.NewGuid(), EntityName = "E Dance Productions", EntityNumber = 12, Email = "EDanceProductions_EDP", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrEmilyWoodfield_StellarPerformingArts = new Competitor { Id = Guid.NewGuid(), EntityName = "Emily Woodfield", EntityNumber = 13, Email = "EmilyWoodfield_StellarPerformingArts", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrEstherMeenken_MamboLab = new Competitor { Id = Guid.NewGuid(), EntityName = "Esther Meenken", EntityNumber = 14, Email = "EstherMeenken_MamboLab", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrEstherMeenkenandCorwinRuegg_MamboLab = new Competitor { Id = Guid.NewGuid(), EntityName = "Esther Meenken and Corwin Ruegg", EntityNumber = 15, Email = "EstherMeenkenandCorwinRuegg_MamboLab", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrEstherMeenkenandEllenHume_MamboLab = new Competitor { Id = Guid.NewGuid(), EntityName = "Esther Meenken and Ellen Hume", EntityNumber = 16, Email = "EstherMeenkenandEllenHume_MamboLab", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrGiancarloJohanssonandHeidiCone_Latinissimo = new Competitor { Id = Guid.NewGuid(), EntityName = "Giancarlo Johansson and Heidi Cone", EntityNumber = 17, Email = "GiancarloJohanssonandHeidiCone_Latinissimo", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrGraceBerge_RaineSymons = new Competitor { Id = Guid.NewGuid(), EntityName = "Grace Berge", EntityNumber = 18, Email = "GraceBerge_RaineSymons", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrHollyBayne_LatinFire = new Competitor { Id = Guid.NewGuid(), EntityName = "Holly Bayne", EntityNumber = 19, Email = "HollyBayne_LatinFire", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrHollyBayneandCaitlinQuin_LatinFire = new Competitor { Id = Guid.NewGuid(), EntityName = "Holly Bayne and Caitlin Quin", EntityNumber = 20, Email = "HollyBayneandCaitlinQuin_LatinFire", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrIntermediateAllianceDanceTeam_CarineMoraisandRafaelBarros_Brazil = new Competitor { Id = Guid.NewGuid(), EntityName = "Intermediate Alliance Dance Team _ Carine Morais and Rafael Barros _ Brazil", EntityNumber = 21, Email = "IntermediateAllianceDanceTeam_CarineMoraisandRafaelBarros_Brazil", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrJeorgeSequeiros_SalsaConCoco = new Competitor { Id = Guid.NewGuid(), EntityName = "Jeorge Sequeiros", EntityNumber = 22, Email = "JeorgeSequeiros_SalsaConCoco", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrJeremySim_EncantoEntertainment = new Competitor { Id = Guid.NewGuid(), EntityName = "Jeremy Sim", EntityNumber = 23, Email = "JeremySim_EncantoEntertainment", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrJessicaAbelenandLevGimelfarb_LatinissimoNelson = new Competitor { Id = Guid.NewGuid(), EntityName = "Jessica Abelen and Lev Gimelfarb", EntityNumber = 24, Email = "JessicaAbelenandLevGimelfarb_LatinissimoNelson", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrKarenForcanoLadiesShinesTeam_SaraDjuric = new Competitor { Id = Guid.NewGuid(), EntityName = "Karen Forcano Ladies Shines Team", EntityNumber = 25, Email = "KarenForcanoLadiesShinesTeam_SaraDjuric", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrKatrinPechingerandKahuLeary_Self = new Competitor { Id = Guid.NewGuid(), EntityName = "Katrin Pechinger and Kahu Leary", EntityNumber = 27, Email = "KatrinPechingerandKahuLeary_Self", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrKearaTyler_Self = new Competitor { Id = Guid.NewGuid(), EntityName = "Keara Tyler", EntityNumber = 28, Email = "KearaTyler_Self", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrKristinaKostevc_Self = new Competitor { Id = Guid.NewGuid(), EntityName = "Kristina Kostevc", EntityNumber = 28, Email = "KristinaKostevc_Self", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrLatinFireBachataTeam_LatinFire = new Competitor { Id = Guid.NewGuid(), EntityName = "Latin Fire Bachata Team", EntityNumber = 29, Email = "LatinFireBachataTeam_LatinFire", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrLatinFireYouthTeam = new Competitor { Id = Guid.NewGuid(), EntityName = "Latin Fire Youth Team", EntityNumber = 30, Email = "LatinFireYouthTeam", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrLaylaMoutrib_EDP = new Competitor { Id = Guid.NewGuid(), EntityName = "Layla Moutrib", EntityNumber = 31, Email = "LaylaMoutrib_EDP", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrMandyYap_Self = new Competitor { Id = Guid.NewGuid(), EntityName = "Mandy Yap", EntityNumber = 33, Email = "MandyYap_Self", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrMareeHanford_LatinAddiction = new Competitor { Id = Guid.NewGuid(), EntityName = "Maree Hanford", EntityNumber = 34, Email = "MareeHanford_LatinAddiction", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrMiaYatiswara_SalsaLatina = new Competitor { Id = Guid.NewGuid(), EntityName = "Mia Yatiswara", EntityNumber = 34, Email = "MiaYatiswara_SalsaLatina", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrMiaYatiswaraandJeremySim_SalsaLatinaandEncantoEntertainment = new Competitor { Id = Guid.NewGuid(), EntityName = "Mia Yatiswara and Jeremy Sim", EntityNumber = 35, Email = "MiaYatiswaraandJeremySim_SalsaLatinaandEncantoEntertainment", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrMichaelHobbsandStephanieHampson_Am_LatinFire = new Competitor { Id = Guid.NewGuid(), EntityName = "Michael Hobbs and Stephanie Hampson_AM", EntityNumber = 36, Email = "MichaelHobbsandStephanieHampson_Am_LatinFire", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrNatashaFrost_LatinFire = new Competitor { Id = Guid.NewGuid(), EntityName = "Natasha Frost", EntityNumber = 37, Email = "NatashaFrost_LatinFire", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrNatashaFrostandMaggieKwon_LatinFire = new Competitor { Id = Guid.NewGuid(), EntityName = "Natasha Frost and Maggie Kwon", EntityNumber = 38, Email = "NatashaFrostandMaggieKwon_LatinFire", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrNazeefKhanAMandEmilyGlubbPro_EDP = new Competitor { Id = Guid.NewGuid(), EntityName = "Nazeef Khan AM and Emily Glubb Pro", EntityNumber = 39, Email = "NazeefKhanAMandEmilyGlubbPro_EDP", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrPassionOfExpressionBachataTeam_PassionofExpression = new Competitor { Id = Guid.NewGuid(), EntityName = "Passion Of Expression Bachata Team", EntityNumber = 40, Email = "PassionOfExpressionBachataTeam_PassionofExpression", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrPassionOfExpressionSalsaTeam_PassionOfExpression = new Competitor { Id = Guid.NewGuid(), EntityName = "Passion Of Expression Salsa Team", EntityNumber = 42, Email = "PassionOfExpressionSalsaTeam_PassionOfExpression", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrPhyllisannTyler_Bray_Self = new Competitor { Id = Guid.NewGuid(), EntityName = "Phyllisann Tyler_Bray", EntityNumber = 42, Email = "PhyllisannTyler_Bray_Self", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrRaineSymons_RainesSuavesitas = new Competitor { Id = Guid.NewGuid(), EntityName = "Raine Symons", EntityNumber = 43, Email = "RaineSymons_RainesSuavesitas", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrRainesSuavesitas_RaineSymons = new Competitor { Id = Guid.NewGuid(), EntityName = "Raines Suavesitas", EntityNumber = 45, Email = "RainesSuavesitas_RaineSymons", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrRicardoGreccoandYanZhou_Self = new Competitor { Id = Guid.NewGuid(), EntityName = "Ricardo Grecco and Yan Zhou", EntityNumber = 46, Email = "RicardoGreccoandYanZhou_Self", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrRitmosCandentes_SalsaLatina = new Competitor { Id = Guid.NewGuid(), EntityName = "Ritmos Candentes", EntityNumber = 47, Email = "RitmosCandentes_SalsaLatina", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrRowenaDalumpienesandJeninaMangoma_RainesSuavesitas = new Competitor { Id = Guid.NewGuid(), EntityName = "Rowena Dalumpienes and Jenina Mangoma", EntityNumber = 48, Email = "RowenaDalumpienesandJeninaMangoma_RainesSuavesitas", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrRubyGilligan_LatinFire = new Competitor { Id = Guid.NewGuid(), EntityName = "Ruby Gilligan", EntityNumber = 49, Email = "RubyGilligan_LatinFire", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrSalsaSabrosa_SalsaLatina = new Competitor { Id = Guid.NewGuid(), EntityName = "Salsa Sabrosa", EntityNumber = 50, Email = "SalsaSabrosa_SalsaLatina", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrSaraDjuricandSebastianVera_SaraandSebastian = new Competitor { Id = Guid.NewGuid(), EntityName = "Sara Djuric and Sebastian Vera", EntityNumber = 51, Email = "SaraDjuricandSebastianVera_SaraandSebastian", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrSarahSonalandLeylaMoutrib_EDP = new Competitor { Id = Guid.NewGuid(), EntityName = "Sarah Sonal and Leyla Moutrib", EntityNumber = 52, Email = "SarahSonalandLeylaMoutrib_EDP", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrSebastianVeraandKearaTyler_AM_SaraandSebastian = new Competitor { Id = Guid.NewGuid(), EntityName = "Sebastian Vera and Keara Tyler _AM", EntityNumber = 53, Email = "SebastianVeraandKearaTyler_AM_SaraandSebastian", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrSharonMiddletonandDarcyLange_Self = new Competitor { Id = Guid.NewGuid(), EntityName = "Sharon Middleton and Darcy Lange", EntityNumber = 54, Email = "SharonMiddletonandDarcyLange_Self", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrShawneeDeath_RainesSuavesitas = new Competitor { Id = Guid.NewGuid(), EntityName = "Shawnee Death", EntityNumber = 55, Email = "ShawneeDeath_RainesSuavesitas", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrTaniaGordon_SalsaLatina = new Competitor { Id = Guid.NewGuid(), EntityName = "Tania Gordon", EntityNumber = 56, Email = "TaniaGordon_SalsaLatina", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrYanZhou_EDanceProductions = new Competitor { Id = Guid.NewGuid(), EntityName = "Yan Zhou", EntityNumber = 57, Email = "YanZhou_EDanceProductions", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrYoheiMikawa_TempoDanceCompany = new Competitor { Id = Guid.NewGuid(), EntityName = "Yohei Mikawa", EntityNumber = 58, Email = "YoheiMikawa_TempoDanceCompany", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            new List<Competitor> { comptrYoheiMikawa_TempoDanceCompany, comptrYanZhou_EDanceProductions, comptrTaniaGordon_SalsaLatina, comptrShawneeDeath_RainesSuavesitas, comptrSharonMiddletonandDarcyLange_Self, comptrSebastianVeraandKearaTyler_AM_SaraandSebastian, comptrSarahSonalandLeylaMoutrib_EDP, comptrSaraDjuricandSebastianVera_SaraandSebastian, comptrSalsaSabrosa_SalsaLatina, comptrRubyGilligan_LatinFire, comptrRowenaDalumpienesandJeninaMangoma_RainesSuavesitas, comptrRitmosCandentes_SalsaLatina, comptrRicardoGreccoandYanZhou_Self, comptrRainesSuavesitas_RaineSymons, comptrRaineSymons_RainesSuavesitas, comptrPassionOfExpressionSalsaTeam_PassionOfExpression, comptrPassionOfExpressionBachataTeam_PassionofExpression, comptrNazeefKhanAMandEmilyGlubbPro_EDP, comptrNatashaFrostandMaggieKwon_LatinFire, comptrNatashaFrost_LatinFire, comptrMichaelHobbsandStephanieHampson_Am_LatinFire, comptrMiaYatiswaraandJeremySim_SalsaLatinaandEncantoEntertainment, comptrMiaYatiswara_SalsaLatina, comptrMandyYap_Self, comptrLaylaMoutrib_EDP, comptrLatinFireYouthTeam, comptrLatinFireBachataTeam_LatinFire, comptrKristinaKostevc_Self, comptrKatrinPechingerandKahuLeary_Self, comptrKarenForcanoLadiesShinesTeam_SaraDjuric, comptrJessicaAbelenandLevGimelfarb_LatinissimoNelson, comptrJeremySim_EncantoEntertainment, comptrJeorgeSequeiros_SalsaConCoco, comptrIntermediateAllianceDanceTeam_CarineMoraisandRafaelBarros_Brazil, comptrHollyBayneandCaitlinQuin_LatinFire, comptrHollyBayne_LatinFire, comptrGraceBerge_RaineSymons, comptrGiancarloJohanssonandHeidiCone_Latinissimo, comptrEstherMeenkenandEllenHume_MamboLab, comptrEstherMeenkenandCorwinRuegg_MamboLab, comptrEstherMeenken_MamboLab, comptrEmilyWoodfield_StellarPerformingArts, comptrEDanceProductions_EDP, comptrDeborahChouandJeorgeSequeiros_SalsaConCoco, comptrDeborahChou_SalsaConCoco, comptrCharlotteJaneBuccahanandCharmaineJoyBuccahan_RaineSymons, comptrCaitlinQuinn_LatinFire, comptrBachasalsa_SalsaConCoco, comptrAndradaNeaguandAlbertoJuarez_PassionofExpression, comptrAcereBongo_SalsaConCoco, }.ForEach(x => context.Competitors.Add(x));


            #endregion COMPETITORS




            //Fill these in
            #region ASSIGN_COMPETITOR_COMPETITION_JUDGES

            var judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            var competitorCompetitions = new HashSet<CompetitorCompetition>();
            var location = "10 Newton Rd";

            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrGraceBerge_RaineSymons,
                comptrNatashaFrost_LatinFire,
                comptrHollyBayne_LatinFire,
                comptrRubyGilligan_LatinFire
            });
            var compYouthSalsaSoloMixed = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Youth Salsa Solo Mixed", Category = divYouth.Categories.First(x => x.Caption == "Youth Salsa Solo Mixed"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 16:00"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrGraceBerge_RaineSymons,
                comptrCaitlinQuinn_LatinFire
            });
            var compYouthLatinSoloMixed = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Youth Latin Solo Mixed", Category = divYouth.Categories.First(x => x.Caption == "Youth Latin Solo Mixed"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 16:20"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrLatinFireYouthTeam
            });
            var compYouthSalsaSoloTeams = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Youth Salsa Solo Teams", Category = divYouth.Categories.First(x => x.Caption == "Youth Salsa Solo Teams"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 16:40"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { 
                comptrRainesSuavesitas_RaineSymons,
                comptrLatinFireYouthTeam
            });
            var compYouthLatinSoloTeams = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Youth Latin Solo Teams", Category = divYouth.Categories.First(x => x.Caption == "Youth Latin Solo Teams"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 17:00"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { 
                
            });
            var compYouthSalsaDuetsMixed = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Youth Salsa Duets Mixed", Category = divYouth.Categories.First(x => x.Caption == "Youth Salsa Duets Mixed"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 17:20"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { 
                comptrCharlotteJaneBuccahanandCharmaineJoyBuccahan_RaineSymons,
                comptrNatashaFrostandMaggieKwon_LatinFire,
                comptrHollyBayneandCaitlinQuin_LatinFire
            });
            var compYouthLatinDuetsMixed = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Youth Latin Duets Mixed", Category = divYouth.Categories.First(x => x.Caption == "Youth Latin Duets Mixed"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 17:40"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { 
                comptrKristinaKostevc_Self,
                comptrMandyYap_Self,
                comptrMareeHanford_LatinAddiction,
                comptrKearaTyler_Self
            });
            var compAmateurSalsaSoloFemale = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Amateur Salsa Solo Female", Category = divAmatuer.Categories.First(x => x.Caption == "Amateur Salsa Solo Female"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 18:45"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };



            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrYanZhou_EDanceProductions,
                comptrEmilyWoodfield_StellarPerformingArts,
                comptrLaylaMoutrib_EDP,
                
            });
            var compSemi_ProSalsaSoloFemale = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Semi_Pro Salsa Solo Female", Category = divSemiPro.Categories.First(x => x.Caption == "Semi_Pro Salsa Solo Female"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 19:00"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> { 
                comptrRaineSymons_RainesSuavesitas,
                comptrMiaYatiswara_SalsaLatina,
                comptrDeborahChou_SalsaConCoco
            });
            var compProfessionalSalsaSoloFemale = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Professional Salsa Solo Female", Category = divProf.Categories.First(x => x.Caption == "Professional Salsa Solo Female"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 19:15"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrYoheiMikawa_TempoDanceCompany,
                comptrJeremySim_EncantoEntertainment,
                comptrJeorgeSequeiros_SalsaConCoco
            });
            var compProfessionalSalsaSoloMale = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Professional Salsa Solo Male", Category = divProf.Categories.First(x => x.Caption == "Professional Salsa Solo Male"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 19:30"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrAlanaMace_RainesSuavesitas,
                comptrYanZhou_EDanceProductions,
                comptrRaineSymons_RainesSuavesitas,
                comptrTaniaGordon_SalsaLatina,
                comptrLaylaMoutrib_EDP,
                comptrJeremySim_EncantoEntertainment,
                comptrEstherMeenken_MamboLab,
                comptrDeborahChou_SalsaConCoco
            });
            var compOpenLatinSolo = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Latin Solo", Category = divOpen.Categories.First(x => x.Caption == "Open Latin Solo"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 19:45"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrJessicaAbelenandLevGimelfarb_LatinissimoNelson,
                comptrSharonMiddletonandDarcyLange_Self,
                comptrEstherMeenkenandCorwinRuegg_MamboLab
            });
            var compSemi_ProSalsaCouples = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Semi_Pro Salsa Couples", Category = divSemiPro.Categories.First(x => x.Caption == "Semi_Pro Salsa Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 20:00"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrSaraDjuricandSebastianVera_SaraandSebastian,
                comptrAndradaNeaguandAlbertoJuarez_PassionofExpression
            });
            var compProfessionalSalsaCouples = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Professional Salsa Couples", Category = divProf.Categories.First(x => x.Caption == "Professional Salsa Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 20:15"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrNazeefKhanAMandEmilyGlubbPro_EDP,
                comptrMiaYatiswaraandJeremySim_SalsaLatinaandEncantoEntertainment,
                comptrRicardoGreccoandYanZhou_Self
            });
            var compOpenSalsaCouples = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Salsa Couples", Category = divOpen.Categories.First(x => x.Caption == "Open Salsa Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 20:30"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrKatrinPechingerandKahuLeary_Self,
                comptrAlinaSolomkinaandCorwinRuegg_EDP
            });
            var compSemi_ProBachataCouples = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Semi_Pro Bachata Couples", Category = divSemiPro.Categories.First(x => x.Caption == "Semi_Pro Bachata Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 20:45"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrSaraDjuricandSebastianVera_SaraandSebastian,
                comptrAndradaNeaguandAlbertoJuarez_PassionofExpression
            });
            var compProfessionalBachataCouples = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Professional Bachata Couples", Category = divProf.Categories.First(x => x.Caption == "Professional Bachata Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 21:00"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrGiancarloJohanssonandHeidiCone_Latinissimo//,
                //comptrDeborahChouandJeorgeSequeiros_SalsaConCoco
            });
            var compOpenLatinCouples = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Latin Couples", Category = divOpen.Categories.First(x => x.Caption == "Open Latin Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 21:15"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrEstherMeenkenandEllenHume_MamboLab
            });
            var compOpenSalsaShinesDuets = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Salsa Shines Duets", Category = divOpen.Categories.First(x => x.Caption == "Open Salsa Shines Duets"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 21:30"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrRowenaDalumpienesandJeninaMangoma_RainesSuavesitas,
                comptrSarahSonalandLeylaMoutrib_EDP
            });
            var compOpenLatinShinesDuets = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Latin Shines Duets", Category = divOpen.Categories.First(x => x.Caption == "Open Latin Shines Duets"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 21:45"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrSebastianVeraandKearaTyler_AM_SaraandSebastian,
                comptrMichaelHobbsandStephanieHampson_Am_LatinFire
            });
            var compPro_AmLatinCouples = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Pro_Am Latin Couples", Category = divProAm.Categories.First(x => x.Caption == "Pro_Am Latin Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 22:00"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrPassionOfExpressionSalsaTeam_PassionOfExpression,
                comptrSalsaSabrosa_SalsaLatina
            });
            var compAmateurSalsaTeams = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Amateur Salsa Teams", Category = divAmatuer.Categories.First(x => x.Caption == "Amateur Salsa Teams"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 22:15"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrAdvancedAllianceDanceTeam_CarineMoraisandRafaelBarros_Brazil,
                comptrAcereBongo_SalsaConCoco,
                comptrIntermediateAllianceDanceTeam_CarineMoraisandRafaelBarros_Brazil
            });
            var compOpenSalsaTeams = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Salsa Teams", Category = divOpen.Categories.First(x => x.Caption == "Open Salsa Teams"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 22:30"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrPassionOfExpressionBachataTeam_PassionofExpression,
                comptrBachasalsa_SalsaConCoco
            });
            var compOpenLatinTeams = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Latin Teams", Category = divOpen.Categories.First(x => x.Caption == "Open Latin Teams"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 22:45"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrEDanceProductions_EDP,
                comptrRitmosCandentes_SalsaLatina
            });
            var compOpenSalsaShinesTeams = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Salsa Shines Teams", Category = divOpen.Categories.First(x => x.Caption == "Open Salsa Shines Teams"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 23:00"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrKarenForcanoLadiesShinesTeam_SaraDjuric,
                comptrRainesSuavesitas_RaineSymons,
                comptrLatinFireBachataTeam_LatinFire,
                comptrEDanceProductions_EDP,
                comptrRitmosCandentes_SalsaLatina
            });
            var compOpenLatinShinesTeams = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Latin Shines Teams", Category = divOpen.Categories.First(x => x.Caption == "Open Latin Shines Teams"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2017-11-25 23:15"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };


            new List<Competition> { compOpenLatinShinesTeams, compOpenSalsaShinesTeams, compOpenLatinTeams, compOpenSalsaTeams, compAmateurSalsaTeams, compPro_AmLatinCouples, compOpenLatinShinesDuets, compOpenSalsaShinesDuets, compOpenLatinCouples, compProfessionalBachataCouples, compSemi_ProBachataCouples, compOpenSalsaCouples, compProfessionalSalsaCouples, compSemi_ProSalsaCouples, compOpenLatinSolo, compProfessionalSalsaSoloMale, compProfessionalSalsaSoloFemale, compSemi_ProSalsaSoloFemale, compAmateurSalsaSoloFemale, compYouthLatinDuetsMixed, compYouthSalsaDuetsMixed, compYouthLatinSoloTeams, compYouthSalsaSoloTeams, compYouthLatinSoloMixed, compYouthSalsaSoloMixed, }.ForEach(x => context.Competitions.Add(x));




            #endregion ASSIGN_COMPETITOR_COMPETITION_JUDGES


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
