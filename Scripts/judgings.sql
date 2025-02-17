--JUDGINGS LIST

select c.Name ,
c.StartedOn,
cab.Caption as Category,
db.Caption as Division,
jp.Email as Judge,
j.JudgeType,
cpp.EntityNumber as CompetitorNo,
cpp.EntityName as Competitor,
cc.Id as competitorcompetitionid,
crp.Caption as Criterion,
jj.ScorePoints as Score,
jj.Id as JudgingId
from Dance.Judge j inner join
Dance.Participant jp on jp.Id = j.id
inner join Dance.JudgeCompetition jc on j.Id=jc.JudgeId
inner join Dance.Competition c on c.Id = jc.CompetitionId
inner join Dance.Category ca on ca.Id = c.CategoryId
inner join Dance.BaseObject cab on cab.id = ca.Id
inner join Dance.Division d on d.Id = ca.DivisionId
inner join Dance.BaseObject db on db.id = d.Id
inner join Dance.Judging jj on jj.JudgeCompetitionId = jc.Id
inner join Dance.CompetitorCompetition cc on jj.CompetitorCompetitionId = cc.Id
inner join Dance.Competitor cp on cp.Id = cc.CompetitorId
inner join Dance.Participant cpp on cpp.Id = cp.Id
inner join Dance.Criterion cr on cr.Id = jj.CriterionId
inner join Dance.BaseObject crp on crp.id = cr.Id
--where cpp.EntityName like '%gino%'

order by c.StartedOn, c.Name,
db.DisplaySequence
, cab.DisplaySequence
, jp.Email
, cpp.EntityNumber
, crp.DisplaySequence 


/*
delete from Dance.Judging where id in 
(
'88D42847-556A-4BB2-B561-E2A3B74611EA',
'F7C8782E-B559-4815-808A-AED6BCD100CD',
'C7D47882-E105-45BA-B5C1-ABBACA5455C4'
)
*/