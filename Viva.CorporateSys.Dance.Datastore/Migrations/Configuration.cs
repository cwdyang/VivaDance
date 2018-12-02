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
            categories = new HashSet<Category>(); var divNovice = new Division() { Caption = "Novice", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now, Categories = categories };
            categories = new HashSet<Category>(); var divMasters = new Division() { Caption = "Masters", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now, Categories = categories };
            categories = new HashSet<Category>(); var divYouth = new Division() { Caption = "Youth", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now, Categories = categories };


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
            context.Divisions.AddOrUpdate(divNovice);
            context.Divisions.AddOrUpdate(divMasters);
            context.Divisions.AddOrUpdate(divYouth);

            divNovice.Categories.Add(new Category { Caption = "Novice Salsa Solo Female", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divAmatuer.Categories.Add(new Category { Caption = "Amateur Salsa Solo Male", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divAmatuer.Categories.Add(new Category { Caption = "Amateur Salsa Solo Female", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divProf.Categories.Add(new Category { Caption = "Professional Salsa Solo Female", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divProf.Categories.Add(new Category { Caption = "Professional Salsa Solo Male", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divOpen.Categories.Add(new Category { Caption = "Open Latin Solo Mixed", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divMasters.Categories.Add(new Category { Caption = "Masters Salsa Solo Mixed", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divOpen.Categories.Add(new Category { Caption = "Open Salsa Shines Duets", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divOpen.Categories.Add(new Category { Caption = "Open Latin Shines Duets", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divAmatuer.Categories.Add(new Category { Caption = "Amateur Salsa Couples", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divProf.Categories.Add(new Category { Caption = "Professional Salsa Couples", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divAmatuer.Categories.Add(new Category { Caption = "Amateur Bachata Couples", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divProf.Categories.Add(new Category { Caption = "Professional Bachata Couples", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divProAm.Categories.Add(new Category { Caption = "Pro_Am Salsa Couples Mixed", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divProAm.Categories.Add(new Category { Caption = "Pro_Am Latin Couples Mixed", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divMasters.Categories.Add(new Category { Caption = "Masters Salsa Couples", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divMasters.Categories.Add(new Category { Caption = "Masters Latin Couples", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divAmatuer.Categories.Add(new Category { Caption = "Amateur Salsa Teams", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divOpen.Categories.Add(new Category { Caption = "Open Salsa Teams", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divOpen.Categories.Add(new Category { Caption = "Open Latin Teams", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divOpen.Categories.Add(new Category { Caption = "Open Salsa Shines Teams", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divOpen.Categories.Add(new Category { Caption = "Open Latin Shines Teams", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divOpen.Categories.Add(new Category { Caption = "Open Showcase Teams", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });
            divMasters.Categories.Add(new Category { Caption = "Masters Latin Teams Mixed", Requirements = "100% choreography", Id = Guid.NewGuid(), CreatedOn = DateTimeOffset.Now });

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

            var comptrAlberto_Juarez_Pro_Anoushka_Jain_Am_Passion_Of_Expression = new Competitor { Id = Guid.NewGuid(), EntityName = "Alberto_Juarez_Pro_Anoushka_Jain_Am_Passion_Of_Expression", EntityNumber = 1, Email = "Alberto_Juarez_Pro_Anoushka_Jain_Am_Passion_Of_Expression", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrAracelly_Roxana_Silva_Cortes_and_Rodrigo_Hernan_Plaza_Rosales_Self_Represented = new Competitor { Id = Guid.NewGuid(), EntityName = "Aracelly_Roxana_Silva_Cortes_and_Rodrigo_Hernan_Plaza_Rosales_Self_Represented", EntityNumber = 2, Email = "Aracelly_Roxana_Silva_Cortes_and_Rodrigo_Hernan_Plaza_Rosales_Self_Represented", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrArturo_Flores_and_Kirsten_Scott_Salsa_Cartel_Australia = new Competitor { Id = Guid.NewGuid(), EntityName = "Arturo_Flores_and_Kirsten_Scott_Salsa_Cartel_Australia", EntityNumber = 3, Email = "Arturo_Flores_and_Kirsten_Scott_Salsa_Cartel_Australia", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrArturo_Flores_Salsa_Cartel_Australia = new Competitor { Id = Guid.NewGuid(), EntityName = "Arturo_Flores_Salsa_Cartel_Australia", EntityNumber = 4, Email = "Arturo_Flores_Salsa_Cartel_Australia", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrBen_Hammond_Self_Represented = new Competitor { Id = Guid.NewGuid(), EntityName = "Ben_Hammond_Self_Represented", EntityNumber = 5, Email = "Ben_Hammond_Self_Represented", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrCamila_Sarlo_RYDE = new Competitor { Id = Guid.NewGuid(), EntityName = "Camila_Sarlo_RYDE", EntityNumber = 6, Email = "Camila_Sarlo_RYDE", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrCoco_and_Debbie_Chou_Salsa_Con_Coco_ = new Competitor { Id = Guid.NewGuid(), EntityName = "Coco_and_Debbie_Chou_Salsa_Con_Coco_", EntityNumber = 7, Email = "Coco_and_Debbie_Chou_Salsa_Con_Coco_", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrCoco_Pro_Helga_Moran_Am_Salsa_Con_Coco = new Competitor { Id = Guid.NewGuid(), EntityName = "Coco_Pro_Helga_Moran_Am_Salsa_Con_Coco", EntityNumber = 8, Email = "Coco_Pro_Helga_Moran_Am_Salsa_Con_Coco", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrCoco_Salsa_Con_Coco = new Competitor { Id = Guid.NewGuid(), EntityName = "Coco_Salsa_Con_Coco", EntityNumber = 9, Email = "Coco_Salsa_Con_Coco", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrDebbie_Chou_Salsa_Con_Coco = new Competitor { Id = Guid.NewGuid(), EntityName = "Debbie_Chou_Salsa_Con_Coco", EntityNumber = 10, Email = "Debbie_Chou_Salsa_Con_Coco", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrDiane_Field_and_Jorge_Sequeiros_Salsa_Con_Coco = new Competitor { Id = Guid.NewGuid(), EntityName = "Diane_Field_and_CoCo_Salsa_Con_Coco", EntityNumber = 11, Email = "Diane_Field_and_Jorge_Sequeiros_Salsa_Con_Coco", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrFarya_Zaman_RYDE = new Competitor { Id = Guid.NewGuid(), EntityName = "Farya_Zaman_RYDE", EntityNumber = 12, Email = "Farya_Zaman_RYDE", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrHannah_Gin_and_Michelle_Knowler_Self_represented = new Competitor { Id = Guid.NewGuid(), EntityName = "Hannah_Gin_and_Michelle_Knowler_Self_represented", EntityNumber = 13, Email = "Hannah_Gin_and_Michelle_Knowler_Self_represented", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrKathrin_Robertson_and_Lee_Aiono_Salsa_In_The_Suburbs = new Competitor { Id = Guid.NewGuid(), EntityName = "Kathrin_Robertson_and_Lee_Aiono_Salsa_In_The_Suburbs", EntityNumber = 14, Email = "Kathrin_Robertson_and_Lee_Aiono_Salsa_In_The_Suburbs", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrKirsten_Scott_Salsa_Cartel_Australia = new Competitor { Id = Guid.NewGuid(), EntityName = "Kirsten_Scott_Salsa_Cartel_Australia", EntityNumber = 15, Email = "Kirsten_Scott_Salsa_Cartel_Australia", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrKrissi_Gould_and_Phil_Gentry_Self_Represented = new Competitor { Id = Guid.NewGuid(), EntityName = "Krissi_Gould_and_Phil_Gentry_Self_Represented", EntityNumber = 16, Email = "Krissi_Gould_and_Phil_Gentry_Self_Represented", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrLee_Aiono_Salsa_In_The_Suburbs = new Competitor { Id = Guid.NewGuid(), EntityName = "Lee_Aiono_Salsa_In_The_Suburbs", EntityNumber = 17, Email = "Lee_Aiono_Salsa_In_The_Suburbs", CompetitorType = CompetitorType.Soloist, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrLiz_Parons_Sweet_Azucar = new Competitor { Id = Guid.NewGuid(), EntityName = "Liz_Parons_Sweet_Azucar", EntityNumber = 18, Email = "Liz_Parons_Sweet_Azucar", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrMia_Yatiswara_and_Robyn_Lindsay_Ritmos_Candentes = new Competitor { Id = Guid.NewGuid(), EntityName = "Mia_Yatiswara_and_Robyn_Lindsay_Ritmos_Candentes", EntityNumber = 19, Email = "Mia_Yatiswara_and_Robyn_Lindsay_Ritmos_Candentes", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrMichael_Cui_RYDE_ = new Competitor { Id = Guid.NewGuid(), EntityName = "Michael_Cui_RYDE_", EntityNumber = 20, Email = "Michael_Cui_RYDE_", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrNatalie_Fester_Masha_Johansson = new Competitor { Id = Guid.NewGuid(), EntityName = "Natalie_Fester_Masha_Johansson", EntityNumber = 21, Email = "Natalie_Fester_Masha_Johansson", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrNicola_Taylor_Viva_Dance = new Competitor { Id = Guid.NewGuid(), EntityName = "Nicola_Taylor_Viva_Dance", EntityNumber = 22, Email = "Nicola_Taylor_Viva_Dance", CompetitorType = CompetitorType.CouplesTeam, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrNola_Chong_and_Wayne_Lapwood_Salsa_Con_Coco_ = new Competitor { Id = Guid.NewGuid(), EntityName = "Nola_Chong_and_Wayne_Lapwood_Salsa_Con_Coco_", EntityNumber = 23, Email = "Nola_Chong_and_Wayne_Lapwood_Salsa_Con_Coco_", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrSara_Djuric_Pro_and_Jonny_Watson_Am_Sara_and_Sebasitan_Dance_Co = new Competitor { Id = Guid.NewGuid(), EntityName = "Sara_Djuric_Pro_and_Jonny_Watson_Am_Sara_and_Sebasitan_Dance_Co", EntityNumber = 24, Email = "Sara_Djuric_Pro_and_Jonny_Watson_Am_Sara_and_Sebasitan_Dance_Co", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrSebasitan_Vera_Pro_and_Lucile_Payraudeau_Am_To_Register_Sara_and_Sebastian_Dance_Co = new Competitor { Id = Guid.NewGuid(), EntityName = "Sebasitan_Vera_Pro_and_Lucile_Payraudeau_Am_To_Register_Sara_and_Sebastian_Dance_Co", EntityNumber = 25, Email = "Sebasitan_Vera_Pro_and_Lucile_Payraudeau_Am_To_Register_Sara_and_Sebastian_Dance_Co", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrSebastian_Vera_Pro_and_Nicola_Taylor_Am_Sara_and_Sebastian_Dance_Co = new Competitor { Id = Guid.NewGuid(), EntityName = "Sebastian_Vera_Pro_and_Nicola_Taylor_Am_Sara_and_Sebastian_Dance_Co", EntityNumber = 26, Email = "Sebastian_Vera_Pro_and_Nicola_Taylor_Am_Sara_and_Sebastian_Dance_Co", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrSofia_Radak_Ryde = new Competitor { Id = Guid.NewGuid(), EntityName = "Sofia_Radak_Ryde", EntityNumber = 27, Email = "Sofia_Radak_Ryde", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrYan_Zhou_and_Ricardo_Grecco_RYDE = new Competitor { Id = Guid.NewGuid(), EntityName = "Yan_Zhou_and_Ricardo_Grecco_RYDE", EntityNumber = 28, Email = "Yan_Zhou_and_Ricardo_Grecco_RYDE", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrYan_Zhou_RYDE = new Competitor { Id = Guid.NewGuid(), EntityName = "Yan_Zhou_RYDE", EntityNumber = 29, Email = "Yan_Zhou_RYDE", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrYohei_Mikawa_Pro_and_Farya_Zaman_Am_Self_Represented = new Competitor { Id = Guid.NewGuid(), EntityName = "Yohei_Mikawa_Pro_and_Farya_Zaman_Am_Self_Represented", EntityNumber = 30, Email = "Yohei_Mikawa_Pro_and_Farya_Zaman_Am_Self_Represented", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrYohei_Mikawa_Self_Represented = new Competitor { Id = Guid.NewGuid(), EntityName = "Yohei_Mikawa_Self_Represented", EntityNumber = 31, Email = "Yohei_Mikawa_Self_Represented", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrSalsa_Latina = new Competitor { Id = Guid.NewGuid(), EntityName = "Salsa_Latina", EntityNumber = 32, Email = "Salsa_Latina", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrPassion_Of_Expression_Salsa_Team = new Competitor { Id = Guid.NewGuid(), EntityName = "Passion_Of_Expression_Salsa_Team", EntityNumber = 33, Email = "Passion_Of_Expression_Salsa_Team", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrSalsa_Con_Coco_Pelota = new Competitor { Id = Guid.NewGuid(), EntityName = "Salsa_Con_Coco_Pelota", EntityNumber = 34, Email = "Salsa_Con_Coco_Pelota", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrRYDE_Salsa_Team = new Competitor { Id = Guid.NewGuid(), EntityName = "RYDE_Salsa_Team", EntityNumber = 35, Email = "RYDE_Salsa_Team", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrSalsa_Latina_Mambo_Team = new Competitor { Id = Guid.NewGuid(), EntityName = "Salsa_Latina_Mambo_Team", EntityNumber = 36, Email = "Salsa_Latina_Mambo_Team", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrPassion_Of_Expressions_Bachata_Team = new Competitor { Id = Guid.NewGuid(), EntityName = "Passion_Of_Expressions_Bachata_Team", EntityNumber = 37, Email = "Passion_Of_Expressions_Bachata_Team", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrRYDE_Salsa_Shines_Team = new Competitor { Id = Guid.NewGuid(), EntityName = "RYDE_Salsa_Shines_Team", EntityNumber = 38, Email = "RYDE_Salsa_Shines_Team", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrRitmos_Candentes_Cha = new Competitor { Id = Guid.NewGuid(), EntityName = "Ritmos_Candentes_Cha", EntityNumber = 39, Email = "Ritmos_Candentes_Cha", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrLas_Isadoras_Salsa_Latina = new Competitor { Id = Guid.NewGuid(), EntityName = "Las_Isadoras_Salsa_Latina", EntityNumber = 40, Email = "Las_Isadoras_Salsa_Latina", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrRitmos_Candentes_Bachata = new Competitor { Id = Guid.NewGuid(), EntityName = "Ritmos_Candentes_Bachata", EntityNumber = 41, Email = "Ritmos_Candentes_Bachata", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrSweet_Azucar_Showcase_Team = new Competitor { Id = Guid.NewGuid(), EntityName = "Sweet_Azucar_Showcase_Team", EntityNumber = 42, Email = "Sweet_Azucar_Showcase_Team", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrSalsa_Latina_Bachata_Kizomba_Team = new Competitor { Id = Guid.NewGuid(), EntityName = "Salsa_Latina_Bachata_Kizomba_Team", EntityNumber = 43, Email = "Salsa_Latina_Bachata_Kizomba_Team", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };
            var comptrSalsa_Con_Coco_Mixed_Latin_Team = new Competitor { Id = Guid.NewGuid(), EntityName = "Salsa_Con_Coco_Mixed_Latin_Team", EntityNumber = 44, Email = "Salsa_Con_Coco_Mixed_Latin_Team", CompetitorType = CompetitorType.Couple, Organisation = orgVivaDance, MobileNumber = "021", FirstName = "", LastName = "" };

            new List<Competitor> { comptrAlberto_Juarez_Pro_Anoushka_Jain_Am_Passion_Of_Expression, comptrAracelly_Roxana_Silva_Cortes_and_Rodrigo_Hernan_Plaza_Rosales_Self_Represented, comptrArturo_Flores_and_Kirsten_Scott_Salsa_Cartel_Australia, comptrArturo_Flores_Salsa_Cartel_Australia, comptrBen_Hammond_Self_Represented, comptrCamila_Sarlo_RYDE, comptrCoco_and_Debbie_Chou_Salsa_Con_Coco_, comptrCoco_Pro_Helga_Moran_Am_Salsa_Con_Coco, comptrCoco_Salsa_Con_Coco, comptrDebbie_Chou_Salsa_Con_Coco, comptrDiane_Field_and_Jorge_Sequeiros_Salsa_Con_Coco, comptrFarya_Zaman_RYDE, comptrHannah_Gin_and_Michelle_Knowler_Self_represented, comptrKathrin_Robertson_and_Lee_Aiono_Salsa_In_The_Suburbs, comptrKirsten_Scott_Salsa_Cartel_Australia, comptrKrissi_Gould_and_Phil_Gentry_Self_Represented, comptrLee_Aiono_Salsa_In_The_Suburbs, comptrLiz_Parons_Sweet_Azucar, comptrMia_Yatiswara_and_Robyn_Lindsay_Ritmos_Candentes, comptrMichael_Cui_RYDE_, comptrNatalie_Fester_Masha_Johansson, comptrNicola_Taylor_Viva_Dance, comptrNola_Chong_and_Wayne_Lapwood_Salsa_Con_Coco_, comptrSara_Djuric_Pro_and_Jonny_Watson_Am_Sara_and_Sebasitan_Dance_Co, comptrSebasitan_Vera_Pro_and_Lucile_Payraudeau_Am_To_Register_Sara_and_Sebastian_Dance_Co, comptrSebastian_Vera_Pro_and_Nicola_Taylor_Am_Sara_and_Sebastian_Dance_Co, comptrSofia_Radak_Ryde, comptrYan_Zhou_and_Ricardo_Grecco_RYDE, comptrYan_Zhou_RYDE, comptrYohei_Mikawa_Pro_and_Farya_Zaman_Am_Self_Represented, comptrYohei_Mikawa_Self_Represented, comptrSalsa_Latina, comptrPassion_Of_Expression_Salsa_Team, comptrSalsa_Con_Coco_Pelota, comptrRYDE_Salsa_Team, comptrSalsa_Latina_Mambo_Team, comptrPassion_Of_Expressions_Bachata_Team, comptrRYDE_Salsa_Shines_Team, comptrRitmos_Candentes_Cha, comptrLas_Isadoras_Salsa_Latina, comptrRitmos_Candentes_Bachata, comptrSweet_Azucar_Showcase_Team, comptrSalsa_Latina_Bachata_Kizomba_Team, comptrSalsa_Con_Coco_Mixed_Latin_Team }.ForEach(x => context.Competitors.Add(x));

            #endregion COMPETITORS




            //Fill these in
            #region ASSIGN_COMPETITOR_COMPETITION_JUDGES

            var judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            var competitorCompetitions = new HashSet<CompetitorCompetition>();
            var location = "10 Newton Rd";

            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrSofia_Radak_Ryde
            });
            var compNoviceSalsaSoloFemale = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Novice Salsa Solo Female", Category = divNovice.Categories.First(x => x.Caption == "Novice Salsa Solo Female"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 12:00"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrMichael_Cui_RYDE_
            });
            var compAmateurSalsaSolomale = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Amateur Salsa Solo male", Category = divAmatuer.Categories.First(x=>x.Caption=="Amateur Salsa Solo Male"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 12:15"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor>
            {
                comptrNatalie_Fester_Masha_Johansson,
                comptrNicola_Taylor_Viva_Dance,
                comptrCamila_Sarlo_RYDE,
                comptrFarya_Zaman_RYDE
            });
            var compAmateurSalsaSoloFemale = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Amateur Salsa Solo Female", Category = divAmatuer.Categories.First(x => x.Caption == "Amateur Salsa Solo Female"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 12:30"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };
                
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor>
            {
                comptrYan_Zhou_RYDE
            });
            var compProfessionalSalsaSoloFemale = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Professional Salsa Solo Female", Category = divProf.Categories.First(x => x.Caption == "Professional Salsa Solo Female"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 12:40"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor>
            {
                comptrArturo_Flores_Salsa_Cartel_Australia,
                comptrYohei_Mikawa_Self_Represented,
                comptrBen_Hammond_Self_Represented
            });
            var compProfessionalSalsaSoloMale = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Professional Salsa Solo Male", Category = divProf.Categories.First(x => x.Caption == "Professional Salsa Solo Male"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 12:50"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor>
            {
                comptrDebbie_Chou_Salsa_Con_Coco,
                comptrLiz_Parons_Sweet_Azucar
            });
            var compOpenLatinSoloMixed = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Latin Solo Mixed", Category = divOpen.Categories.First(x => x.Caption == "Open Latin Solo Mixed"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 13:10"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor>
            {
                comptrKirsten_Scott_Salsa_Cartel_Australia,
                comptrCoco_Salsa_Con_Coco,
                comptrLee_Aiono_Salsa_In_The_Suburbs
            });
            var compMastersSalsaSoloMixed = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Masters Salsa Solo Mixed", Category = divMasters.Categories.First(x => x.Caption == "Masters Salsa Solo Mixed"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 13:20"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor>
            {
                comptrMia_Yatiswara_and_Robyn_Lindsay_Ritmos_Candentes
            });
            var compOpenSalsaShinesDuets = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Salsa Shines Duets", Category = divOpen.Categories.First(x => x.Caption == "Open Salsa Shines Duets"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 13:30"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrHannah_Gin_and_Michelle_Knowler_Self_represented,
                comptrArturo_Flores_and_Kirsten_Scott_Salsa_Cartel_Australia
            });
            var compOpenLatinShinesDuets = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Latin Shines Duets", Category = divOpen.Categories.First(x => x.Caption == "Open Latin Shines Duets"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 13:31"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrAracelly_Roxana_Silva_Cortes_and_Rodrigo_Hernan_Plaza_Rosales_Self_Represented
            });
            var compAmateurSalsaCouples = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Amateur Salsa Couples", Category = divAmatuer.Categories.First(x => x.Caption == "Amateur Salsa Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 13:40"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrYan_Zhou_and_Ricardo_Grecco_RYDE
            });
            var compProfessionalSalsaCouples = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Professional Salsa Couples", Category = divProf.Categories.First(x => x.Caption == "Professional Salsa Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 13:50"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrAracelly_Roxana_Silva_Cortes_and_Rodrigo_Hernan_Plaza_Rosales_Self_Represented
            });
            var compAmateurBachataCouples = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Amateur Bachata Couples", Category = divAmatuer.Categories.First(x => x.Caption == "Amateur Bachata Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 14:00"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrCoco_and_Debbie_Chou_Salsa_Con_Coco_
            });
            var compProfessionalBachataCouples = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Professional Bachata Couples", Category = divProf.Categories.First(x => x.Caption == "Professional Bachata Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 14:20"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrSara_Djuric_Pro_and_Jonny_Watson_Am_Sara_and_Sebasitan_Dance_Co,
                comptrSebastian_Vera_Pro_and_Nicola_Taylor_Am_Sara_and_Sebastian_Dance_Co
             });
            var compPro_AmSalsaCouplesMixed = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Pro_Am Salsa Couples Mixed", Category = divProAm.Categories.First(x => x.Caption == "Pro_Am Salsa Couples Mixed"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 14:30"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrCoco_Pro_Helga_Moran_Am_Salsa_Con_Coco,
                comptrAlberto_Juarez_Pro_Anoushka_Jain_Am_Passion_Of_Expression,
                comptrYohei_Mikawa_Pro_and_Farya_Zaman_Am_Self_Represented,
                comptrSebasitan_Vera_Pro_and_Lucile_Payraudeau_Am_To_Register_Sara_and_Sebastian_Dance_Co
            });
            var compPro_AmLatinCouplesMixed = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Pro_Am Latin Couples Mixed", Category = divProAm.Categories.First(x => x.Caption == "Pro_Am Latin Couples Mixed"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 14:50"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            //IRINA! ISSUES
            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrCoco_and_Debbie_Chou_Salsa_Con_Coco_,
                comptrKrissi_Gould_and_Phil_Gentry_Self_Represented,
                comptrKathrin_Robertson_and_Lee_Aiono_Salsa_In_The_Suburbs,
                comptrNola_Chong_and_Wayne_Lapwood_Salsa_Con_Coco_
            });
            var compMastersSalsaCouples = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Masters Salsa Couples", Category = divMasters.Categories.First(x => x.Caption == "Masters Salsa Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 16:00"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrDiane_Field_and_Jorge_Sequeiros_Salsa_Con_Coco
            });
            var compMastersLatinCouples = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Masters Latin Couples", Category = divMasters.Categories.First(x => x.Caption == "Masters Latin Couples"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 16:20"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrSalsa_Latina,
                comptrPassion_Of_Expression_Salsa_Team
            });
            var compAmateurSalsaTeams = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Amateur Salsa Teams", Category = divAmatuer.Categories.First(x => x.Caption == "Amateur Salsa Teams"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 16:30"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrSalsa_Con_Coco_Pelota,
                comptrRYDE_Salsa_Team,
                comptrSalsa_Latina_Mambo_Team
            });
            var compOpenSalsaTeams = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Salsa Teams", Category = divOpen.Categories.First(x => x.Caption == "Open Salsa Teams"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 16:45"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrPassion_Of_Expressions_Bachata_Team
            });
            var compOpenLatinTeams = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Latin Teams", Category = divOpen.Categories.First(x => x.Caption == "Open Latin Teams"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 16:55"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrRYDE_Salsa_Shines_Team
            });
            var compOpenSalsaShinesTeams = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Salsa Shines Teams", Category = divOpen.Categories.First(x => x.Caption == "Open Salsa Shines Teams"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 17:10"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrRitmos_Candentes_Cha,
                comptrLas_Isadoras_Salsa_Latina,
                comptrRitmos_Candentes_Bachata
            });
            var compOpenLatinShinesTeams = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Latin Shines Teams", Category = divOpen.Categories.First(x => x.Caption == "Open Latin Shines Teams"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 17:20"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrSweet_Azucar_Showcase_Team,
                comptrSalsa_Latina_Bachata_Kizomba_Team
            });
            var compOpenShowcaseTeams = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Open Showcase Teams", Category = divOpen.Categories.First(x => x.Caption == "Open Showcase Teams"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 17:22"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            judgeCompetitions = CreateJudgeCompetitions(new List<Judge> { judgeHead, judge1, judge2, judge3, judge4, judge5 });
            competitorCompetitions = CreateCompetitorCompetitions(new List<Competitor> {
                comptrSalsa_Con_Coco_Mixed_Latin_Team
            });
            var compMastersLatinTeamsMixed = new Competition { Id = Guid.NewGuid(), Location = location, Name = "Masters Latin Teams Mixed", Category = divMasters.Categories.First(x => x.Caption == "Masters Latin Teams Mixed"), CompetitionStatus = CompetitionStatus.Created, StartedOn = DateTimeOffset.Parse("2018-12-01 17:30"), CompletedOn = null, CreatedOn = DateTimeOffset.Now, CompetitorCompetitions = competitorCompetitions, JudgeCompetitions = judgeCompetitions };

            new List<Competition> { compNoviceSalsaSoloFemale, compAmateurSalsaSolomale, compAmateurSalsaSoloFemale, compProfessionalSalsaSoloFemale, compProfessionalSalsaSoloMale, compOpenLatinSoloMixed, compMastersSalsaSoloMixed, compOpenSalsaShinesDuets, compOpenLatinShinesDuets, compAmateurSalsaCouples, compProfessionalSalsaCouples, compAmateurBachataCouples, compProfessionalBachataCouples, compPro_AmSalsaCouplesMixed, compPro_AmLatinCouplesMixed, compMastersSalsaCouples, compMastersLatinCouples, compAmateurSalsaTeams, compOpenSalsaTeams, compOpenLatinTeams, compOpenSalsaShinesTeams, compOpenLatinShinesTeams, compOpenShowcaseTeams, compMastersLatinTeamsMixed }.ForEach(x => context.Competitions.Add(x));

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
