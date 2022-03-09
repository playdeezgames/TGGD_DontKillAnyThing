Public Module CharacteristicData
    Friend Sub Initialize()
        CharacterData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Characteristics]
            (
                [CharacterId] INT NOT NULL,
                [CharacteristicType] INT NOT NULL,
                [Base] INT NOT NULL,
                [Delta] INT NOT NULL,
                UNIQUE([CharacterId],[CharacteristicType]),
                FOREIGN KEY ([CharacterId]) REFERENCES [Characters]([CharacterId])
            );")
    End Sub
    Function ReadBase(characterId As Long, characteristicType As Long) As Long?
        Initialize()
        Using command = CreateCommand(
            "SELECT [Base] FROM [Characteristics] WHERE [CharacterId]=@CharacterId AND [CharacteristicType]=@CharacteristicType;",
            MakeParameter("@CharacterId", characterId),
            MakeParameter("@CharacteristicType", characteristicType))
            Return ExecuteScalar(Of Long)(command)
        End Using
    End Function
    Function ReadDelta(characterId As Long, characteristicType As Long) As Long?
        Initialize()
        Using command = CreateCommand(
            "SELECT [Delta] FROM [Characteristics] WHERE [CharacterId]=@CharacterId AND [CharacteristicType]=@CharacteristicType;",
            MakeParameter("@CharacterId", characterId),
            MakeParameter("@CharacteristicType", characteristicType))
            Return ExecuteScalar(Of Long)(command)
        End Using
    End Function
    Sub Write(characterId As Long, characteristicType As Long, base As Long, delta As Long)
        Initialize()
        Using command = CreateCommand(
            "REPLACE INTO [Characteristics]
            (
                [CharacterId],
                [CharacteristicType],
                [Base],
                [Delta]
            ) 
            VALUES
            (
                @CharacterId,
                @CharacteristicType,
                @Base,
                @Delta
            );",
            MakeParameter("@CharacterId", characterId),
            MakeParameter("@CharacteristicType", characteristicType),
            MakeParameter("@Base", base),
            MakeParameter("@Delta", delta))
            command.ExecuteNonQuery()
        End Using
    End Sub
End Module
