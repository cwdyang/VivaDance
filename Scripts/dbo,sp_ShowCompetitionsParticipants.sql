--JUDGINGS LIST

IF EXISTS (SELECT * FROM dbo.sysobjects
WHERE id = object_id(N'[dbo].[sp_ShowCompetitionsParticipants]')
and OBJECTPROPERTY(id, N'IsProcedure') = 1)
   drop PROCEDURE [dbo].[sp_ShowCompetitionsParticipants] 
GO

CREATE PROCEDURE dbo.sp_ShowCompetitionsParticipants 
    @CompetionName nvarchar(255) = null
AS 


select c.Name as Competition ,
cpp.EntityName as Competitor,
c.GroupComp,
c.StartedOn,
cab.Caption as Category,
db.Caption as Division,

cc.sequence as CompOrder
from 
 Dance.Competition c
inner join Dance.Category ca on ca.Id = c.CategoryId
inner join Dance.BaseObject cab on cab.id = ca.Id
inner join Dance.Division d on d.Id = ca.DivisionId
inner join Dance.BaseObject db on db.id = d.Id
inner join Dance.CompetitorCompetition cc on c.Id = cc.CompetitionId
inner join Dance.Competitor cp on cp.Id = cc.CompetitorId
inner join Dance.Participant cpp on cpp.Id = cp.Id
--where cpp.EntityName like '%gino%'
where c.name = @CompetionName or @CompetionName is null

order by c.StartedOn, c.Name,
cc.Sequence
, cab.DisplaySequence
, cpp.EntityNumber

GO

/*
delete from Dance.Judging where id in 
(
'88D42847-556A-4BB2-B561-E2A3B74611EA',
'F7C8782E-B559-4815-808A-AED6BCD100CD',
'C7D47882-E105-45BA-B5C1-ABBACA5455C4'
)
*/