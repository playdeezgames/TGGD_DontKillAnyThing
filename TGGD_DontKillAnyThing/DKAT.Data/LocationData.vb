﻿Public Module LocationData
    Friend Sub Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Locations]
            (
                [LocationId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [Populated] INT NOT NULL
            );")
    End Sub
    Function Create() As Long
        Initialize()
        ExecuteNonQuery("INSERT INTO [Locations]([Populated]) VALUES(0);")
        Return LastInsertRowId
    End Function
    Sub SetPopulated(locationId As Long, populated As Boolean)
        Initialize()
        Using command = CreateCommand(
            "UPDATE [Locations] SET [Populated]=@Populated WHERE [LocationId]=@LocationId;",
            MakeParameter("@LocationId", locationId),
            MakeParameter("@Populated", populated))
            command.ExecuteNonQuery()
        End Using
    End Sub
End Module