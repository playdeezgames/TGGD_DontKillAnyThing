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
End Module
