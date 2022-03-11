Imports DKAT.Data

Public Class Inventory
    ReadOnly Property Id As Long
    Sub New(inventoryId As Long)
        Id = inventoryId
    End Sub
    Sub Add(item As Item)
        InventoryItemData.Write(Id, item.Id)
    End Sub
    ReadOnly Property Items As List(Of Item)
        Get
            Return InventoryItemData.ReadForInventory(Id).Select(Function(itemId)
                                                                     Return New Item(itemId)
                                                                 End Function).ToList
        End Get
    End Property
    ReadOnly Property IsEmpty As Boolean
        Get
            Return Not Items.Any
        End Get
    End Property
    ReadOnly Property StackedItems As Dictionary(Of ItemType, List(Of Item))
        Get
            Dim stacks = Items.GroupBy(Function(item)
                                           Return item.ItemType
                                       End Function)
            Dim result As New Dictionary(Of ItemType, List(Of Item))
            For Each stack In stacks
                result.Add(stack.Key, stack.ToList())
            Next
            Return result
        End Get
    End Property
End Class
