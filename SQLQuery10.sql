SELECT
    RoomNumber,
    COUNT(*) AS Total
FROM dbo.Rooms
GROUP BY RoomNumber
HAVING COUNT(*) > 1;SELECT
    