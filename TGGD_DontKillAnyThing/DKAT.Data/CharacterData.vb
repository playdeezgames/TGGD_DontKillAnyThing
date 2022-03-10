Public Module CharacterData
    Friend Sub Initialize()
        LocationData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [Characters]
            (
                [CharacterId] INTEGER PRIMARY KEY AUTOINCREMENT,
                [LocationId] INT NOT NULL,
                [CharacterType] INT NOT NULL,
                FOREIGN KEY ([LocationId]) REFERENCES [Locations]([LocationId])
            );")
    End Sub
    Function Create(locationId As Long, characterType As Long) As Long
        Initialize()
        Using command = CreateCommand(
            "INSERT INTO [Characters]([LocationId],[CharacterType]) VALUES(@LocationId,@CharacterType);",
            MakeParameter("@LocationId", locationId),
            MakeParameter("@CharacterType", characterType))
            command.ExecuteNonQuery()
        End Using
        Return LastInsertRowId
    End Function
    Function Clear(characterId As Long)
        Initialize()
        CharacteristicData.ClearForCharacter(characterId)
        CounterData.ClearForCharacter(characterId)
        Using command = CreateCommand(
            "DELETE FROM [Characters] WHERE [CharacterId]=@CharacterId;",
            MakeParameter("@CharacterId", characterId))
            command.ExecuteNonQuery()
        End Using
    End Function
    Function ReadLocation(characterId As Long) As Long?
        Initialize()
        Using command = CreateCommand(
            "SELECT [LocationId] FROM [Characters] WHERE [CharacterId]=@CharacterId;",
            MakeParameter("@CharacterId", characterId))
            Return ExecuteScalar(Of Long)(command)
        End Using
    End Function
    Function ReadCharacterType(characterId As Long) As Long?
        Initialize()
        Using command = CreateCommand(
            "SELECT [CharacterType] FROM [Characters] WHERE [CharacterId]=@CharacterId;",
            MakeParameter("@CharacterId", characterId))
            Return ExecuteScalar(Of Long)(command)
        End Using
    End Function
    Sub WriteLocation(characterId As Long, locationId As Long)
        Initialize()
        Using command = CreateCommand(
            "UPDATE [Characters] SET [LocationId]=@LocationId WHERE [CharacterId]=@CharacterId;",
            MakeParameter("@CharacterId", characterId),
            MakeParameter("@LocationId", locationId))
            command.ExecuteNonQuery()
        End Using
    End Sub
    Function ReadForLocation(locationId As Long) As List(Of Long)
        Initialize()
        Using command = CreateCommand(
            "SELECT [CharacterId] FROM [Characters] WHERE [LocationId]=@LocationId;",
            MakeParameter("@LocationId", locationId))
            Using reader = command.ExecuteReader
                Dim result As New List(Of Long)
                While reader.Read()
                    result.Add(CLng(reader("CharacterId")))
                End While
                Return result
            End Using
        End Using
    End Function
End Module
