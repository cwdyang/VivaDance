--JUDGE LIST

select c.Name ,
c.StartedOn,
cab.Caption as Category,
db.Caption as Division,
jp.Email as Judge,
j.JudgeType
from Dance.Judge j inner join
Dance.Participant jp on jp.Id = j.id
inner join Dance.JudgeCompetition jc on j.Id=jc.JudgeId
inner join Dance.Competition c on c.Id = jc.CompetitionId
inner join Dance.Category ca on ca.Id = c.CategoryId
inner join Dance.BaseObject cab on cab.id = ca.Id
inner join Dance.Division d on d.Id = ca.DivisionId
inner join Dance.BaseObject db on db.id = d.Id

order by c.StartedOn, c.Name,
db.DisplaySequence
, cab.DisplaySequence
, jp.Email