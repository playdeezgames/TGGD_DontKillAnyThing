Public Module CharacterInventoryData
    Friend Sub Initialize()
        CharacterData.Initialize()
        InventoryData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [CharacterInventories]
            (
                [CharacterId] INT NOT NULL UNIQUE,
                [InventoryId] INT NOT NULL
            );")
    End Sub
    Sub Write(characterId As Long, inventoryId As Long)
        Initialize()
        Using command = CreateCommand(
            "REPLACE INTO [CharacterInventories]([CharacterId],[InventoryId]) VALUES(@CharacterId,@InventoryId);",
            MakeParameter("@CharacterId", characterId),
            MakeParameter("@InventoryId", inventoryId))
            command.ExecuteNonQuery()
        End Using
    End Sub
    Function Read(characterId As Long) As Long?
        Initialize()
        Using command = CreateCommand(
            "SELECT [InventoryId] FROM [CharacterInventories] WHERE [CharacterId]=@CharacterId;",
            MakeParameter("@CharacterId", characterId))
            Return ExecuteScalar(Of Long)(command)
        End Using
    End Function
    Sub Clear(characterId As Long)
        Initialize()
        Using command = CreateCommand(
            "DELETE FROM [CharacterInventories] WHERE [CharacterId]=@CharacterId;",
            MakeParameter("@CharacterId", characterId))
            command.ExecuteNonQuery()
        End Using
    End Sub
End Module
