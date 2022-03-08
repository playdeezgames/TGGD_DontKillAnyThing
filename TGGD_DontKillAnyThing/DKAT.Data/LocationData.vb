Public Module LocationData
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
End Module
