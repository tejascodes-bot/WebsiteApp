SELECT
    RoomId,
    RoomNumber,
    RoomTypeId,
    IsClean,
    IsOutOfOrder
FROM dbo.Rooms
ORDER BY RoomNumber;