Public Module ItemData
    Friend Sub Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Items]
            (
                [ItemId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [ItemType] INT NOT NULL
            );")
    End Sub
    Function Create(itemType As Long) As Long
        Initialize()
        Using command = CreateCommand(
            "INSERT INTO [Items]([ItemType]) VALUES(@ItemType);",
            MakeParameter("@ItemType", itemType))
            command.ExecuteNonQuery()
        End Using
        Return LastInsertRowId
    End Function
End Module
