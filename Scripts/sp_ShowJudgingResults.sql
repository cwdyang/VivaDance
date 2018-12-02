/*

delete from dance.judging
dbo.sp_ShowCompetitionsParticipants 
exec dbo.sp_ShowJudgingResults  'Amateur Salsa Solo Female','Natalie_Fester_Masha_Johansson','Judge1'
exec dbo.sp_ShowJudgingResults  'Novice Salsa Solo Female','Sofia_Radak_Ryde','Judge1'

update dance.judging set scorepoints = 2 where id ='E268F43C-53BA-4DE4-B9AE-32855AC6B430'
*/

IF EXISTS (SELECT * FROM dbo.sysobjects
WHERE id = object_id(N'[dbo].[sp_ShowJudgingResults]')
and OBJECTPROPERTY(id, N'IsProcedure') = 1)
   drop PROCEDURE [dbo].[sp_ShowJudgingResults] 
GO

CREATE PROCEDURE dbo.sp_ShowJudgingResults 
    @CompetionName nvarchar(255) = null, 
    @CompetitorName nvarchar(255) = null,
	@JudgeName nvarchar(255) = null
AS 

select * 
from
(select j.id as JudgingId,
j.ScorePoints, 
jgp.FirstName + jgp.LastName as Judge,
crp.EntityName as Competitor,
c.Name as CompName,
bo.caption as Criterion,
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
inner join
[Dance].[BaseObject] bo
on bo.Id = ct.Id 
where
(jgp.FirstName + jgp.LastName = @JudgeName or @JudgeName is null) and
(crp.EntityName = @CompetitorName or @CompetitorName is null) and
(c.Name = @CompetionName or @CompetionName is null)
) results
order by
results.StartedOn desc,
results.Competitor,
results.compname,
results.judge

	

	
GO