Imports System.Data.SqlClient
Imports Telerik.Web.UI

Public Class Cadastro_Inscritos
    Inherits System.Web.UI.Page

    Dim cnn As New cnn
    Dim erro As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack Then
            Exit Sub
        End If

        LimparCampos()
        PreencherGrid(False)
        TabInscritos.SelectedIndex = 0
        MultiPageInscritos.SelectedIndex = 0

    End Sub

#Region "Funções"

    Private Sub DesabilitarCampos(boo As Boolean)
        inputNome.Disabled = boo
        inputEmail.Disabled = boo
        inputDataNascimento.Disabled = boo
        inputUserInstagram.Disabled = boo
    End Sub

    Private Sub LimparValidacao()
        inputNome.Attributes("class") = "form-control inputNome"
        inputEmail.Attributes("class") = "form-control inputEmail"
        inputDataNascimento.Attributes("class") = "form-control inputDataNascimento"
        inputUserInstagram.Attributes("class") = "form-control inputUserInstagram"
    End Sub

    Private Sub EstadoBotoes(Estado As enumEstadoBotoes)

        'Função que controla o estado dos botoes na página
        Select Case Estado
            Case enumEstadoBotoes.Limpar
                btnNovoInscritos.Enabled = True
                btnNovoInscritos.Attributes("cssclass") = "btn btn-primary" 'retira css disabled
                btnExcluirModal.Disabled = True
                btnExcluirModal.Attributes("class") = "btn btn-danger disabled" 'seta css disabled
                btnSalvarInscritos.Enabled = False
                btnSalvarInscritos.Attributes("cssclass") = "btn btn-success disabled" 'seta css disabled
                btnCancelarInscritos.Enabled = False
                btnCancelarInscritos.Attributes("cssclass") = "btn btn-warning disabled" 'seta css disabled
            Case enumEstadoBotoes.Inclusao
                btnNovoInscritos.Enabled = False
                btnExcluirModal.Disabled = True
                btnSalvarInscritos.Enabled = True
                btnCancelarInscritos.Enabled = True
                btnNovoInscritos.Attributes("cssclass") = "btn btn-primary disabled"
                btnExcluirModal.Attributes("class") = "btn btn-danger disabled"
                btnSalvarInscritos.Attributes("cssclass") = "btn btn-success"
                btnCancelarInscritos.Attributes("cssclass") = "btn btn-warning"
            Case enumEstadoBotoes.Alteracao
                btnNovoInscritos.Enabled = False
                btnExcluirModal.Disabled = False
                btnSalvarInscritos.Enabled = True
                btnCancelarInscritos.Enabled = True
                btnNovoInscritos.Attributes("cssclass") = "btn btn-primary disabled"
                btnExcluirModal.Attributes("class") = "btn btn-danger"
                btnSalvarInscritos.Attributes("cssclass") = "btn btn-success"
                btnCancelarInscritos.Attributes("cssclass") = "btn btn-warning"
        End Select

    End Sub

    Private Sub LimparCampos()

        'Flag de controle de alteração
        Operacao.Value = "Inclusao"
        ID.Value = ""
        lblerro.InnerText = ""

        DesabilitarCampos(True)
        LimparValidacao()
        EstadoBotoes(enumEstadoBotoes.Limpar)

        inputNome.Value = ""
        inputEmail.Value = ""
        inputDataNascimento.Value = ""
        inputUserInstagram.Value = ""

    End Sub

    Private Sub PreencherGrid(Filtro As Boolean)
        Dim dt As New DataTable
        Dim SQL As String = ""
        Dim SQLFiltro As String = ""
        Dim erro As String = ""
        Dim Parametros As SqlParameter() = Nothing

        SQL = $"SELECT ID, Nome, DtNascimento, Email, UserInstagram
                  FROM tbInscritos "

        If Filtro = True Then
            If inputFiltroNome.Value <> "" Then
                SQLFiltro = "Nome LIKE @Nome "
            End If

            If inputFiltroEmail.Value <> "" Then
                If SQLFiltro <> "" Then
                    SQLFiltro = SQLFiltro & " AND Email LIKE @Email"
                Else
                    SQLFiltro = " Email LIKE @Email"
                End If
            End If

            If inputFiltroUsuario.Value <> "" Then
                If SQLFiltro <> "" Then
                    SQLFiltro = SQLFiltro & " AND UserInstagram LIKE @UserInstagram"
                Else
                    SQLFiltro = " UserInstagram LIKE @UserInstagram"
                End If
            End If

            Parametros = New SqlParameter(2) {}
            Parametros(0) = New SqlParameter() With {.ParameterName = "@Nome", .Value = "%" & inputFiltroNome.Value & "%", .SqlDbType = SqlDbType.VarChar, .Size = 50}
            Parametros(1) = New SqlParameter() With {.ParameterName = "@Email", .Value = "%" & inputFiltroEmail.Value & "%", .SqlDbType = SqlDbType.VarChar, .Size = 50}
            Parametros(2) = New SqlParameter() With {.ParameterName = "@UserInstagram", .Value = "%" & inputFiltroUsuario.Value & "%", .SqlDbType = SqlDbType.VarChar, .Size = 50}
        End If

        dt = cnn.ExecutaDataTable(SQL & IIf(SQLFiltro <> "", "WHERE " & SQLFiltro, ""), Parametros, erro)

        If erro <> "" Then
            lblerro.InnerText = "Erro na função PreencherGrid()."
            Exit Sub
        End If

        gridInscritos.DataSource = dt
        gridInscritos.DataBind()

    End Sub

#End Region

#Region "Botões"

    Protected Sub btnNovoInscritos_Click(sender As Object, e As EventArgs)

        LimparCampos()
        DesabilitarCampos(False)
        EstadoBotoes(enumEstadoBotoes.Inclusao)

        inputNome.Focus()

    End Sub

    Protected Sub btnSalvarInscritos_Click(sender As Object, e As EventArgs)
        'variavel que controla se houve erro de validação
        Dim booErro As Boolean = False
        Dim jquery As String = ""

        LimparValidacao()

        If inputNome.Value = "" Then
            inputNome.Attributes("class") = "form-control inputNome is-invalid"
            booErro = True
        End If

        If inputDataNascimento.Value = "" Or IsDate(inputDataNascimento.Value) = False Then
            inputDataNascimento.Attributes("class") = "form-control inputDataNascimento is-invalid"
            booErro = True
        End If

        If inputEmail.Value = "" Or ValidarEmail(inputEmail.Value) = False Then
            inputEmail.Attributes("class") = "form-control inputEmail is-invalid"
            booErro = True
        End If

        If inputUserInstagram.Value = "" Then
            inputEmail.Attributes("class") = "form-control inputUserInstagram is-invalid"
            booErro = True
        End If

        If booErro Then
            RadScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "ModalSucesso", jquery, True)
            Exit Sub
        End If

        Dim strSQL As String = ""

        If Operacao.Value = "Inclusao" Then
            strSQL = $"INSERT INTO tbInscritos 
                            ( Nome, DtNascimento, Email, UserInstagram)
                       SELECT @Nome, @DtNascimento, @Email, @UserInstagram"
        Else
            strSQL =
                $"UPDATE tbInscritos
                     SET Nome = @Nome
                       , DtNascimento = @DtNascimento
                       , Email = @Email
                       , UserInstagram = @UserInstagram
                   WHERE ID = @ID;"
        End If

        Dim erro As String = ""

        Dim Parametros As SqlParameter() = New SqlParameter(4) {}

        Parametros(0) = New SqlParameter() With {.ParameterName = "@ID", .Value = IIf(ID.Value = "", 0, ID.Value), .SqlDbType = SqlDbType.Int}
        Parametros(1) = New SqlParameter() With {.ParameterName = "@Nome", .Value = inputNome.Value, .SqlDbType = SqlDbType.VarChar, .Size = 50}
        Parametros(2) = New SqlParameter() With {.ParameterName = "@DtNascimento", .Value = Format(CDate(inputDataNascimento.Value), ("yyyy-MM-dd 00:00:00")), .SqlDbType = SqlDbType.DateTime, .Size = 50}
        Parametros(3) = New SqlParameter() With {.ParameterName = "@Email", .Value = inputEmail.Value, .SqlDbType = SqlDbType.VarChar, .Size = 50}
        Parametros(4) = New SqlParameter() With {.ParameterName = "@UserInstagram", .Value = inputUserInstagram.Value, .SqlDbType = SqlDbType.VarChar, .Size = 50}

        cnn.ExecutaQuery(strSQL, Parametros, erro)

        If erro <> "" Then
            lblerro.InnerText = erro
            Exit Sub
        End If

        PreencherGrid(False)
        LimparCampos()

        RadScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "ModalSucesso", "$('.ModalSucesso').modal('show');", True)

    End Sub

    Protected Sub btnCancelarInscritos_Click(sender As Object, e As EventArgs)
        LimparCampos()
        DesabilitarCampos(True)
    End Sub

    Protected Sub btnExcluirInscritos_Click(sender As Object, e As EventArgs)
        Dim SQL As String = ""
        Dim erro As String = ""

        If Operacao.Value = "Alteracao" And CInt(ID.Value) <> 0 Then
            SQL = $"DELETE FROM tbInscritos WHERE ID ={ID.Value};"
            cnn.ExecutaQuery(SQL, , erro)

            LimparCampos()
            PreencherGrid(False)
        End If

    End Sub

    Protected Sub btnFiltrarInscritos_Click(sender As Object, e As EventArgs)
        PreencherGrid(True)
    End Sub

#End Region

    Protected Sub gridInscritos_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs)

        If e.CommandName = "Select" Then

            LimparCampos()
            DesabilitarCampos(False)

            Try
                'Recupera o objeto grid
                Dim Grid As RadGrid = sender

                'Pega a linha selecionada da grid
                Dim linhaSelecionada As GridDataItem = TryCast(e.Item, Telerik.Web.UI.GridDataItem)

                Operacao.Value = "Alteracao"

                TabInscritos.SelectedIndex = 0
                MultiPageInscritos.SelectedIndex = 0

                'Recupera o valor da coluna de índice 1 que é a coluna ID
                ID.Value = linhaSelecionada(Grid.Columns(1)).Text

                inputNome.Value = linhaSelecionada(Grid.Columns(2)).Text
                inputDataNascimento.Value = CDate(linhaSelecionada(Grid.Columns(3)).Text).ToString("yyyy-MM-dd")
                inputEmail.Value = linhaSelecionada(Grid.Columns(4)).Text
                inputUserInstagram.Value = linhaSelecionada(Grid.Columns(5)).Text

                EstadoBotoes(enumEstadoBotoes.Alteracao)

            Catch ex As Exception
                lblerro.InnerText = erro
            End Try

        End If

    End Sub

End Class