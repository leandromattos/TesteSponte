Module fcnGeral
    Enum enumAmbiente
        Producao
        Dev
    End Enum

    Enum enumEstadoBotoes
        Limpar
        Inclusao
        Alteracao
    End Enum

    Private _Ambiente As enumAmbiente
    Public Property Ambiente() As enumAmbiente
        Get
            Return _Ambiente
        End Get
        Set(ByVal value As enumAmbiente)
            _Ambiente = value
        End Set
    End Property

    Function ValidarEmail(ByVal email As String) As Boolean
        Dim regExPattern As String = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$"
        Dim emailAddressMatch As Match = Regex.Match(email, regExPattern)
        If emailAddressMatch.Success Then
            Return True
        Else
            Return False
        End If
    End Function

End Module
