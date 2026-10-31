USE WebsiteAppdb;

SELECT
    RoomNumber,
    COUNT(*) AS DuplicateCount
FROM dbo.Rooms
GROUP BY RoomNumber
HAVING COUNT(*) > 1;