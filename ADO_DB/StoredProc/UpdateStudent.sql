CREATE PROCEDURE [dbo].[UpdateStudent]
	@SectionID INT,
	@YearResult INT,
	@Id INT
AS
BEGIN
	UPDATE Student SET SectionID = @SectionID, YearResult = @YearResult
	WHERE Id = @Id
END