select * 
from
(select j.id as JudgingId,
j.ScorePoints, 
jgp.FirstName + jgp.LastName as Judge,
crp.EntityName as Competitor,
c.Name as CompName,
c.StartedOn

from 
[Dance].[Judging] j
inner join
[Dance].[JudgeCompetition] jc
on j.JudgeCompetitionId = jc.Id
inner join
[Dance].[Judge] jg
on jg.id = jc.JudgeId
inner join
[Dance].[Participant] jgp on 
jgp.id = jg.Id 
inner join
[Dance].[Competition] c
on c.Id = jc.CompetitionId
inner join
[Dance].[CompetitorCompetition] cc
on 
cc.Id = j.CompetitorCompetitionId
inner join 
[Dance].[Competitor] cr
on cr.Id = cc.CompetitorId 
inner join
[Dance].[Participant] crp on 
crp.id = cr.Id 
inner join
[Dance].[Criterion] ct
on ct.Id = j.CriterionId
) results
order by
results.StartedOn desc,
results.Competitor,
results.compname,
results.judge
