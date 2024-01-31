CREATE PROCEDURE [dbo].[AddStudent]
	
	@FirstName VARCHAR(50),
	@LastName VARCHAR(50),
	@BirthDate DATETIME2(7),
	@YearResult INT,
	@SectionID INT
AS
BEGIN
	INSERT INTO Student (FirstName, LastName, BirthDate, YearResult, SectionID) 
	VALUES (@FirstName, @LastName, @BirthDate, @YearResult, @SectionID)
END
