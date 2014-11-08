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

            context.DeleteAll<JudgeCompetition>();
            context.DeleteAll<CompetitorCompetition>();
            context.DeleteAll<Judging>();
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

            var cat1 = new Criterion()
            {
                Caption = "Timing & Musicality",
                Id = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.Now,
                DisplaySequence = 5,
                ScorePoints = 20
            };

            var cat2 = new Criterion()
            {
                Caption = "Technique",
                Id = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.Now,
                DisplaySequence = 4,
                ScorePoints = 20
            };

            var cat3 = new Criterion()
            {
                Caption = "Difficulty",
                Id = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.Now,
                DisplaySequence = 3,
                ScorePoints = 20
            };

            var cat4 = new Criterion()
            {
                Caption = "Appearance, Connection & Synchronicity",
                Id = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.Now,
                DisplaySequence = 2,
                ScorePoints = 20
            };

            var cat5 = new Criterion()
            {
                Caption = "Choreography originality / Freestyle vocabulary",
                Id = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.Now,
                DisplaySequence = 1,
                ScorePoints = 20
            };

            var cat6 = new Criterion()
            {
                Caption = "Penalty",
                Id = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.Now,
                DisplaySequence = 6,
                ScorePoints = 0
            };

            new List<Criterion>{cat1,cat2,cat3,cat4,cat5,cat6}.ForEach(x=>context.Criteria.Add(x));

            var div1 = new Division()
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

            var div2 = new Division()
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

            var div3 = new Division()
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

            var div4 = new Division()
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

            var div5 = new Division()
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

            var div6 = new Division()
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

            var div7 = new Division()
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

            new List<Division>{div1,div2,div3,div4,div5,div6,div7}.ForEach(x=>context.Divisions.AddOrUpdate(x));

            var org1 = new Organisation()
            {
                Id = Guid.NewGuid(),
                Caption = "VivaDanceSchool"
            };

            var org2 = new Organisation()
            {
                Id = Guid.NewGuid(),
                Caption = "VivaJudges"
            };

            new List<Organisation> { org1 }.ForEach(x => context.Organisations.Add(x));



            var judge1 = new Judge
            {
                Id = Guid.NewGuid(),
                EntityName = "JudgePanel",
                Email = "J1",
                FirstName = "J1",
                LastName = "J1",
                EntityNumber = 1,
                JudgeType = JudgeType.Head,
                Organisation = org2,
                JudgingAssignments = new HashSet<JudgingAssignment>
                {
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = cat2
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = cat4
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = cat5
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = cat6
                    }
                }
            };
            var judge2 = new Judge
            {
                Id = Guid.NewGuid(),
                EntityName = "JudgePanel",
                Email = "J2",
                FirstName = "J2",
                LastName = "J2",
                EntityNumber = 2,
                JudgeType = JudgeType.Normal,
                Organisation = org2,
                JudgingAssignments = new HashSet<JudgingAssignment>
                {
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = cat1
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = cat2
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = cat3
                    }
                }
            };
            var judge3 = new Judge
            {
                Id = Guid.NewGuid(),
                EntityName = "JudgePanel",
                Email = "J3",
                FirstName = "J3",
                LastName = "J3",
                EntityNumber = 3,
                JudgeType = JudgeType.Normal,
                Organisation = org2,
                JudgingAssignments = new HashSet<JudgingAssignment>
                {
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = cat3
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = cat4
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = cat5
                    }
                }
            };
            var judge4 = new Judge
            {
                Id = Guid.NewGuid(),
                EntityName = "JudgePanel",
                Email = "J4",
                FirstName = "J4",
                LastName = "J4",
                EntityNumber = 4,
                JudgeType = JudgeType.Normal,
                Organisation = org2,
                JudgingAssignments = new HashSet<JudgingAssignment>
                {
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = cat1
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = cat2
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = cat4
                    }
                }
            };
            var judge5 = new Judge
            {
                Id = Guid.NewGuid(),
                EntityName = "JudgePanel",
                Email = "J5",
                FirstName = "J5",
                LastName = "J5",
                EntityNumber = 5,
                JudgeType = JudgeType.Normal,
                Organisation = org2,
                JudgingAssignments = new HashSet<JudgingAssignment>
                {
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = cat1
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = cat3
                    },
                    new JudgingAssignment
                    {
                        Id = Guid.NewGuid(),
                        Criterion = cat5
                    }
                }
            };

            new List<Judge>{judge1,judge2,judge3,judge4,judge5}.ForEach(x=>context.Judges.AddOrUpdate(x));

            var compr1 = new Competitor
            {
                Id = Guid.NewGuid(),
                EntityName = "Irina Leo",
                EntityNumber = 1,
                Email = "i@i.co.nz",
                CompetitorType = CompetitorType.Couple,
                Organisation = org1,
                MobileNumber = "021",
                FirstName = "Irina",
                LastName = "Leo"
            };

            var compr2 = new Competitor
            {
                Id = Guid.NewGuid(),
                EntityName = "Elena Casper",
                EntityNumber = 2,
                Email = "i@i.co.nz",
                CompetitorType = CompetitorType.Couple,
                Organisation = org1,
                MobileNumber = "021",
                FirstName = "Elena",
                LastName = "Casper"
            };

            var compr3 = new Competitor
            {
                Id = Guid.NewGuid(),
                EntityName = "Davidy",
                EntityNumber = 3,
                Email = "i@i.co.nz",
                CompetitorType = CompetitorType.Soloist,
                Organisation = org1,
                MobileNumber = "021",
                FirstName = "David",
                LastName = "Yang"
            };

            var compr4 = new Competitor
            {
                Id = Guid.NewGuid(),
                EntityName = "Sina",
                EntityNumber = 4,
                Email = "i@i.co.nz",
                CompetitorType = CompetitorType.Soloist,
                Organisation = org1,
                MobileNumber = "021",
                FirstName = "Sina",
                LastName = "Mule"
            };

            new List<Competitor>{compr1,compr2,compr3,compr4}.ForEach(x=>context.Competitors.Add(x));


            var comp1 = new Competition
            {
                Id = Guid.NewGuid(),
                Location = "Viva Studio",
                Name = "Test Comp 1",
                Category = div1.Categories.First(),
                CompetitionStatus = CompetitionStatus.Created,
                StartedOn = DateTimeOffset.Parse("2014-11-02 13:20"),
                CompletedOn = DateTimeOffset.Parse("2014-11-02 14:20"),
                CreatedOn = DateTimeOffset.Now,
                CompetitorCompetitions = new HashSet<CompetitorCompetition>
                {
                    new CompetitorCompetition
                    {
                        Id =  Guid.NewGuid(),
                        Competitor = compr1,
                        Sequence = 1,
                        CompetitorType = compr1.CompetitorType
                    },
                    new CompetitorCompetition
                    {
                        Id =  Guid.NewGuid(),
                        Competitor = compr2,
                        Sequence = 2,
                        CompetitorType = compr2.CompetitorType
                    },
                    new CompetitorCompetition
                    {
                        Id =  Guid.NewGuid(),
                        Competitor = compr3,
                        Sequence = 3,
                        CompetitorType = compr2.CompetitorType
                    }
                },
                JudgeCompetitions = new HashSet<JudgeCompetition>
                {

                    new JudgeCompetition
                    {
                        Id =  Guid.NewGuid(),
                        Judge = judge1,
                        JudgeType = judge1.JudgeType
                    },
                    new JudgeCompetition
                    {
                        Id =  Guid.NewGuid(),
                        Judge = judge2,
                        JudgeType = judge2.JudgeType
                    },
                    new JudgeCompetition
                    {
                        Id =  Guid.NewGuid(),
                        Judge = judge3,
                        JudgeType = judge3.JudgeType
                    },
                    new JudgeCompetition
                    {
                        Id =  Guid.NewGuid(),
                        Judge = judge4,
                        JudgeType = judge4.JudgeType
                    },
                    new JudgeCompetition
                    {
                        Id =  Guid.NewGuid(),
                        Judge = judge5,
                        JudgeType = judge5.JudgeType
                    }
                }
            };

            var comp2 = new Competition
            {
                Id = Guid.NewGuid(),
                Location = "Viva Studio",
                Name = "Test Comp 2",
                Category = div1.Categories.First(),
                CompetitionStatus = CompetitionStatus.Created,
                StartedOn = DateTimeOffset.Parse("2014-11-02 14:20"),
                CompletedOn = DateTimeOffset.Parse("2014-11-02 15:20"),
                CreatedOn = DateTimeOffset.Now,
                CompetitorCompetitions = new HashSet<CompetitorCompetition>
                {
                    new CompetitorCompetition
                    {
                        Id =  Guid.NewGuid(),
                        Competitor = compr4,
                        Sequence = 1,
                        CompetitorType = compr1.CompetitorType
                    },
                    new CompetitorCompetition
                    {
                        Id =  Guid.NewGuid(),
                        Competitor = compr2,
                        Sequence = 2,
                        CompetitorType = compr2.CompetitorType
                    },
                    new CompetitorCompetition
                    {
                        Id =  Guid.NewGuid(),
                        Competitor = compr1,
                        Sequence = 3,
                        CompetitorType = compr2.CompetitorType
                    }
                },
                JudgeCompetitions = new HashSet<JudgeCompetition>
                {

                    new JudgeCompetition
                    {
                        Id =  Guid.NewGuid(),
                        Judge = judge1,
                        JudgeType = judge1.JudgeType
                    },
                    new JudgeCompetition
                    {
                        Id =  Guid.NewGuid(),
                        Judge = judge2,
                        JudgeType = judge2.JudgeType
                    },
                    new JudgeCompetition
                    {
                        Id =  Guid.NewGuid(),
                        Judge = judge3,
                        JudgeType = judge3.JudgeType
                    },
                    new JudgeCompetition
                    {
                        Id =  Guid.NewGuid(),
                        Judge = judge4,
                        JudgeType = judge4.JudgeType
                    },
                    new JudgeCompetition
                    {
                        Id =  Guid.NewGuid(),
                        Judge = judge5,
                        JudgeType = judge5.JudgeType
                    }
                }
            };


            new List<Competition>{comp1,comp2}.ForEach(x=>context.Competitions.Add(x));

            context.SaveChanges();
        }
    }
}
