Imports DKAT.Data
Public Class Location
    ReadOnly Property Id As Long
    Sub New(locationId As Long)
        Id = locationId
        'TODO: populate the location as needed
    End Sub
    ReadOnly Property Doors As List(Of Door)
        Get
            Return DoorData.ReadForFromLocation(Id).Select(Function(doorId)
                                                               Return New Door(doorId)
                                                           End Function).ToList()
        End Get
    End Property
End Class
