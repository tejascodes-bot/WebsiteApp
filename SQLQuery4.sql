USE WebsiteAppdb;

SELECT
    RoomId,
    RoomNumber,
    RoomTypeId
FROM dbo.Rooms
ORDER BY RoomNumber;