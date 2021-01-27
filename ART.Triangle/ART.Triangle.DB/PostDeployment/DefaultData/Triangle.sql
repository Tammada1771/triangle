BEGIN
	INSERT INTO dbo.tblTriangle (Id, SideA, SideB, SideC, UserId, ChangeDate)
	VALUES
	(NEWID(), 3, 4, 5, NEWID(), GETDATE())
END