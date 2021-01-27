CREATE PROCEDURE [dbo].[spCalcSideC]
	@SideA decimal(7,2),
	@SideB decimal(7,2)
AS
	SELECT SQRT(POWER(@SideA, 2) + POWER(@SideB, 2)) As SideC
RETURN 0
