Public Module CounterData
    Friend Sub Initialize()
        CharacterData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Counters]
            (
                [CharacterId] INT NOT NULL,
                [CounterType] INT NOT NULL,
                [Value] INT NOT NULL,
                UNIQUE([CharacterId],[CounterType]),
                FOREIGN KEY([CharacterId]) REFERENCES [Characters]([CharacterId])
            );")
    End Sub
    Sub Write(characterId As Long, counterType As Long, value As Long)
        Initialize()
        Using command = CreateCommand(
            "REPLACE INTO [Counters]([CharacterId],[CounterType],[Value]) VALUES(@CharacterId,@CounterType,@Value);",
            MakeParameter("@CharacterId", characterId),
            MakeParameter("@CounterType", counterType),
            MakeParameter("@Value", value))
            command.ExecuteNonQuery()
        End Using
    End Sub
    Function Read(characterId As Long, counterType As Long) As Long?
        Initialize()
        Using command = CreateCommand(
            "SELECT [Value] FROM [Counters] WHERE [CharacterId]=@CharacterId AND [CounterType]=@CounterType;",
            MakeParameter("@CharacterId", characterId),
            MakeParameter("@CounterType", counterType))
            Return ExecuteScalar(Of Long)(command)
        End Using
    End Function
    Sub ClearForCharacter(characterId As Long)
        Initialize()
        Using command = CreateCommand(
            "DELETE FROM [Counters] WHERE [CharacterId]=@CharacterId;",
            MakeParameter("@CharacterId", characterId))
            command.ExecuteNonQuery()
        End Using
    End Sub
End Module
