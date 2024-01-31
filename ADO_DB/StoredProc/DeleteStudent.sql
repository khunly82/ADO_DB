CREATE PROCEDURE [dbo].[DeleteStudent]
	@Id int
AS
BEGIN
	DELETE FROM Student WHERE Id = @Id
END