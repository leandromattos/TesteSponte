Imports System.Data.DataSet
Imports System.Data.SqlClient
Public Class cnn

    Private mConexao As New SqlConnection

    Private mComando As New SqlCommand

    Private mDataAdap As New SqlDataAdapter

    Private mDataRead As SqlDataReader

    Private usuarioServidor As String

    Private usuarioBancoDeDados As String

    Private usuarioLogin As String

    Private usuarioSenha As String

    Private Property ServidorUs() As String

        Get

            Return usuarioServidor

        End Get

        Set(ByVal value As String)

            usuarioServidor = value

        End Set

    End Property

    Private Property BancoDeDadosUs() As String

        Get

            Return usuarioBancoDeDados

        End Get

        Set(ByVal value As String)

            usuarioBancoDeDados = value

        End Set

    End Property

    Private Property LoginUs() As String

        Get

            Return usuarioLogin

        End Get

        Set(ByVal value As String)

            usuarioLogin = value

        End Set

    End Property

    Private Property SenhaUs() As String

        Get

            Return usuarioSenha

        End Get

        Set(ByVal value As String)

            usuarioSenha = value

        End Set

    End Property

    Private Sub ConectarMySql()
        Dim booabrir As Boolean = False

        If IsNothing(mConexao) Then
            booabrir = True
        ElseIf mConexao.State = ConnectionState.Closed Then
            booabrir = True
        Else
            Exit Sub
        End If

        Dim strConexao As String = ""

        Select Case Ambiente
            Case enumAmbiente.Producao
                strConexao = "Data Source=DESKTOP-UIPL7CQ; Initial Catalog=Sponte; User ID=sa; Password=o4b2g8()sa;"
            Case enumAmbiente.Dev
                strConexao = "Data Source=DESKTOP-UIPL7CQ; Initial Catalog=Sponte; User ID=sa; Password=o4b2g8()sa;"
        End Select

        mConexao = New SqlConnection()

        mConexao.ConnectionString = strConexao

        mConexao.Open()

    End Sub

    Private Sub DesconectarSql()

        If mConexao.State = ConnectionState.Open Then

            mConexao.Close()

            mConexao.Dispose()

            mConexao = Nothing
        End If
    End Sub

    Public Function ExecutaDataTable(ByVal sql As String, Optional ByVal Parametros As SqlParameter() = Nothing, ByRef Optional erro As String = "") As DataTable

        Dim mDataTable As New DataTable

        Try

            erro = ""

            ConectarMySql()

            mComando.CommandType = CommandType.Text

            mComando.CommandText = sql

            mComando.Connection = mConexao

            mComando.Parameters.Clear()

            mDataAdap.SelectCommand = mComando

            If IsNothing(Parametros) = False Then
                If Parametros.Count > 0 Then
                    For i = 0 To Parametros.Count - 1
                        mComando.Parameters.Add(Parametros(i))
                    Next
                End If
            End If

            mDataAdap.Fill(mDataTable)

            mDataAdap.Dispose()

            mComando.Dispose()

            Return mDataTable

        Catch ex As Exception
            If IsNothing(ex.InnerException) = False Then
                erro = ex.InnerException.Message
            Else
                erro = ex.Message
            End If
        Finally
            DesconectarSql()
        End Try

        Return mDataTable

    End Function

    Public Function ExecutaDataRead(ByVal sql As String) As SqlDataReader

        mDataRead = Nothing

        Try

            ConectarMySql()

            mComando.CommandType = CommandType.Text

            mComando.CommandText = sql

            mComando.Connection = mConexao

            mDataRead = mComando.ExecuteReader()

            mComando.Dispose()

            Return mDataRead

        Catch ex As Exception

        Finally
            DesconectarSql()
        End Try

        Return mDataRead

    End Function

    Public Function ExecutaQuery(ByVal sql As String, Optional ByVal Parametros As SqlParameter() = Nothing, Optional ByRef erro As String = "") As SqlCommand

        Try

            erro = ""

            ConectarMySql()

            mComando.CommandType = CommandType.Text

            mComando.CommandText = sql

            mComando.Connection = mConexao

            mComando.Parameters.Clear()

            If IsNothing(Parametros) = False Then
                If Parametros.Count > 0 Then
                    For i = 0 To Parametros.Count - 1
                        mComando.Parameters.Add(Parametros(i))
                    Next
                End If
            End If

            Return mComando.ExecuteScalar

        Catch ex As Exception
            If IsNothing(ex.InnerException) = False Then
                erro = ex.InnerException.Message
            Else
                erro = ex.Message
            End If
        Finally
            DesconectarSql()
        End Try

        Return mComando

    End Function

End Class
