
--update  competition order
--update  dance.Competition set startedon = ToDateTimeOffset('2015-11-21 13:31:00','+13:00') where ID = '6D21FA59-7345-4A87-B909-73A01AA9ABFA'
--select Id, StartedOn,Name from dance.Competition order by StartedOn asc;

--update competitor order
/*
update 
[Dance].[CompetitorCompetition]
set Sequence = 5
where Id = 'A1ED6648-51E7-4485-B2F0-D354AC4C0249'
update 
[Dance].[CompetitorCompetition]
set Sequence = 1
where Id = '4975923B-5581-406A-9801-3E30570E2938'
*/
select /*p.Id as CompetitorId,*/ cc.Id as CompetitionCompetitorId, p.EntityName,cc.Name,cc.Sequence as CompName from
[Dance].[Competitor] c
inner join
(select cc.Id, cc.CompetitorId,c.Name,cc.Sequence
from
[Dance].[CompetitorCompetition] cc
inner join
dance.Competition c on cc.CompetitionId = c.Id and c.Name = 'Semi-Pro Solos') cc
on cc.CompetitorId = c.Id
inner join
dance.Participant p on p.Id = c.Id
order by cc.Sequence
