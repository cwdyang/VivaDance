/*
Semi-Pro Solos
Phyllisann Tyler_Independent 
Scott Suen_Salsa Latina

select Name,startedon from dance.competition order by startedon
exec dbo.sp_ShowParticipantsByNames  'Semi-Pro Solos'
*/

IF EXISTS (SELECT * FROM dbo.sysobjects
WHERE id = object_id(N'[dbo].[sp_ShowParticipantsByNames]')
and OBJECTPROPERTY(id, N'IsProcedure') = 1)
   drop PROCEDURE [dbo].[sp_ShowParticipantsByNames] 
GO

CREATE PROCEDURE dbo.sp_ShowParticipantsByNames 
    @CompetionName nvarchar(255)
AS 

    select /*p.Id as CompetitorId,*/ cc.Id as CompetitionCompetitorId, p.EntityName,cc.Name,cc.Sequence as CompName from
[Dance].[Competitor] c
inner join
(select cc.Id, cc.CompetitorId,c.Name,cc.Sequence
from
[Dance].[CompetitorCompetition] cc
inner join
dance.Competition c on cc.CompetitionId = c.Id and c.Name = @CompetionName) cc
on cc.CompetitorId = c.Id
inner join
dance.Participant p on p.Id = c.Id
order by cc.Sequence
	
GO