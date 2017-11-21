/*
select Id, StartedOn,Name from dance.Competition order by StartedOn asc

exec dbo.sp_SwapCompetitionsByNames  'Semi-Pro Solos','Amateur Solos'
*/

IF EXISTS (SELECT * FROM dbo.sysobjects
WHERE id = object_id(N'[dbo].[sp_SwapCompetitionsByNames]')
and OBJECTPROPERTY(id, N'IsProcedure') = 1)
   drop PROCEDURE [dbo].[sp_SwapCompetitionsByNames] 
GO

CREATE PROCEDURE dbo.sp_SwapCompetitionsByNames 
    @CompetionSwapper nvarchar(255),
	@CompetionSwappee nvarchar(255)
AS 

    select 	 
	c.Id as CompetitionId ,c.Name as CompName ,c.StartedOn
	
	INTO 
	#tmpC
	from
	[Dance].Competition c
	where c.Name in (@CompetionSwapper,@CompetionSwappee)
	order by c.StartedOn asc

	

	update c
	set c.StartedOn = c2.StartedOn
	--select * 
	from
	[Dance].[Competition] c
	inner join
	#tmpC c1 on c1.CompetitionId = c.id 
	left join 
	#tmpC c2 on c2.CompetitionId <> c.id 

	select * from #tmpC
	
GO