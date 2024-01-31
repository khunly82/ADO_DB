CREATE PROCEDURE [dbo].[AddSection]
	@Id INT,
	@SectionName VARCHAR(50)
AS
BEGIN
	INSERT INTO Section VALUES (@Id, @SectionName)
END
