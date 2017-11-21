/*
Semi-Pro Solos
Phyllisann Tyler_Independent 
Scott Suen_Salsa Latina

exec dbo.sp_SwapParticipantsByNames  'Semi-Pro Solos','Phyllisann Tyler_Independent','Scott Suen_Salsa Latina'
*/

IF EXISTS (SELECT * FROM dbo.sysobjects
WHERE id = object_id(N'[dbo].[sp_SwapParticipantsByNames]')
and OBJECTPROPERTY(id, N'IsProcedure') = 1)
   drop PROCEDURE [dbo].[sp_SwapParticipantsByNames] 
GO

CREATE PROCEDURE dbo.sp_SwapParticipantsByNames 
    @CompetionName nvarchar(255), 
    @CompetitorSwapper nvarchar(255),
	@CompetitorSwappee nvarchar(255)
AS 

    select 	 
	cc.Id as CompetitionCompetitorId, p.EntityName,cc.Name as CompName ,cc.Sequence
	INTO 
	#tmpCC
	from
	[Dance].[Competitor] c
	inner join
	(select cc.Id, cc.CompetitorId,c.Name,cc.Sequence
	from
	[Dance].[CompetitorCompetition] cc
	inner join
	dance.Competition c on cc.CompetitionId = c.Id and c.Name = @CompetionName) cc
	on cc.CompetitorId = c.Id
	inner join
	dance.Participant p on p.Id = c.Id and
	(p.EntityName = @CompetitorSwapper or p.EntityName = @CompetitorSwappee)
	order by cc.Sequence

	

	update cc
	set cc.Sequence = cc2.Sequence
	--select * 
	from
	[Dance].[CompetitorCompetition] cc
	inner join
	#tmpCC cc1 on cc1.CompetitionCompetitorId = cc.id 
	left join 
	#tmpCC cc2 on cc2.CompetitionCompetitorId <> cc.id 

	select * from #tmpCC
	
GO