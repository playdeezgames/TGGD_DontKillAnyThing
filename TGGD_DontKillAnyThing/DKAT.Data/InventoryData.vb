Public Module InventoryData
    Friend Sub Initialize()
        ExecuteNonQuery("CREATE TABLE IF NOT EXISTS [Inventories]([InventoryId] INTEGER PRIMARY KEY AUTOINCREMENT);")
    End Sub
    Function Create() As Long
        Initialize()
        Using command = CreateCommand("INSERT INTO [Inventories] DEFAULT VALUES;")
            command.ExecuteNonQuery()
        End Using
        Return LastInsertRowId
    End Function
End Module
