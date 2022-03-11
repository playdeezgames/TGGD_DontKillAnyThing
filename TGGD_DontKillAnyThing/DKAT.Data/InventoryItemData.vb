Public Module InventoryItemData
    Friend Sub Initialize()
        ItemData.Initialize()
        InventoryData.Initialize()
        ExecuteNonQuery(
            "CREATE TABLE IF NOT EXISTS [InventoryItems]
            (
                [InventoryId] INT NOT NULL,
                [ItemId] INT NOT NULL UNIQUE,
                FOREIGN KEY ([InventoryId]) REFERENCES [Inventories]([InventoryId]),
                FOREIGN KEY ([ItemId]) REFERENCES [Items]([ItemId])
            );")
    End Sub
    Sub Write(inventoryId As Long, itemId As Long)
        Initialize()
        Using command = CreateCommand(
            "REPLACE INTO [InventoryItems]([InventoryId],[ItemId]) VALUES(@InventoryId,@ItemId);",
            MakeParameter("@InventoryId", inventoryId),
            MakeParameter("@ItemId", itemId))
            command.ExecuteNonQuery()
        End Using
    End Sub
    Function ReadForInventory(inventoryId As Long) As List(Of Long)
        Initialize()
        Using command = CreateCommand(
            "SELECT [ItemId] FROM [InventoryItems] WHERE [InventoryId]=@InventoryId;",
            MakeParameter("@InventoryId", inventoryId))
            Using reader = command.ExecuteReader
                Dim result As New List(Of Long)
                While reader.Read
                    result.Add(CLng(reader("ItemId")))
                End While
                Return result
            End Using
        End Using
    End Function
End Module
