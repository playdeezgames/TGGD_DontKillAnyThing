Imports DKAT.Data

Public Class PlayerCharacter
    Inherits Character
    Sub New()
        MyBase.New(PlayerData.GetCharacterId().Value)
    End Sub
End Class
