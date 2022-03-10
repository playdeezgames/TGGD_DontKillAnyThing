Public Class Inventory
    ReadOnly Property Id As Long
    Sub New(inventoryId As Long)
        Id = inventoryId
    End Sub
    Sub Add(item As Item)
        Throw New NotImplementedException
    End Sub
End Class
