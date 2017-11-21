spIsJudgingCompleteForJudge.sql

USE [OneDance]
GO

/****** Object:  StoredProcedure [dbo].[DeleteCCQAnswer]    Script Date: 11/18/2017 9:10:16 AM ******/

DROP PROCEDURE [dbo].[DeleteCCQAnswer]
GO

/****** Object:  StoredProcedure [dbo].[DeleteCCQAnswer]    Script Date: 11/18/2017 9:10:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO



CREATE PROCEDURE [dbo].[DeleteCCQAnswer]
	-- Add the parameters for the stored procedure here
	@id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE  [dbo].[CCQAnswers]
	SET [IsDeleted] = 1 
	WHERE ID = @id
 
END

GO



