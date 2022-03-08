Public Module PlayerData
    Friend Sub Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Players]
            (
                [PlayerId] INT NOT NULL UNIQUE,
                [CharacterId] INT NOT NULL,
                CHECK([PlayerId]=1)
            );")
    End Sub
    Sub SetCharacterId(characterId As Long)
        Initialize()
        Using command = CreateCommand(
            "REPLACE INTO [Players]([PlayerId],[CharacterId]) VALUES(1,@CharacterId);",
            MakeParameter("@CharacterId", characterId))
            command.ExecuteNonQuery()
        End Using
    End Sub
    Function GetCharacterId() As Long?
        Initialize()
        Using command = CreateCommand("SELECT [CharacterId] FROM [Players];")
            Return ExecuteScalar(Of Long)(command)
        End Using
    End Function
End Module
