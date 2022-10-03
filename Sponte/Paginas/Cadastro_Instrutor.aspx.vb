Imports System.Data.SqlClient
Imports Sponte.Cadastro_Inscritos
Imports Telerik.Web
Imports Telerik.Web.UI

Public Class Cadastro_Instrutor
    Inherits System.Web.UI.Page

    Dim cnn As New cnn
    Dim erro As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack Then
            Exit Sub
        End If

        LimparCampos()
        PreencherGrid(False)
        TabInstrutor.SelectedIndex = 0
        MultiPageInstrutor.SelectedIndex = 0

    End Sub

#Region "Funções"

    Private Sub DesabilitarCampos(boo As Boolean)
        inputNome.Disabled = boo
        inputEmail.Disabled = boo
        inputDataNascimento.Disabled = boo
    End Sub

    Private Sub LimparValidacao()
        inputNome.Attributes("class") = "form-control inputNome"
        inputEmail.Attributes("class") = "form-control inputEmail"
        inputDataNascimento.Attributes("class") = "form-control inputDataNascimento"
    End Sub

    Private Sub LimparCampos()

        'Flag de controle de alteração
        Operacao.Value = "Inclusao"
        ID.Value = ""

        DesabilitarCampos(True)
        LimparValidacao()
        EstadoBotoes(enumEstadoBotoes.Limpar)

        inputNome.Value = ""
        inputEmail.Value = ""
        inputDataNascimento.Value = ""

    End Sub

    Private Sub PreencherGrid(Filtro As Boolean)
        Dim dt As New DataTable
        Dim SQL As String = ""
        Dim SQLFiltro As String = ""
        Dim erro As String = ""
        Dim Parametros As SqlParameter() = Nothing

        SQL = $"SELECT ID, Nome, DtNascimento, Email
                  FROM tbInstrutor "

        If Filtro = True Then
            If inputFiltroNome.Value <> "" Then
                SQLFiltro = "Nome LIKE @Nome "
            End If

            If inputFiltroEmail.Value <> "" Then
                If SQLFiltro = "" Then
                    SQLFiltro = " Email LIKE @Email"
                Else
                    SQLFiltro = SQLFiltro & " AND Email LIKE @Email"
                End If
            End If

            Parametros = New SqlParameter(1) {}
            Parametros(0) = New SqlParameter() With {.ParameterName = "@Nome", .Value = "%" & inputFiltroNome.Value & "%", .SqlDbType = SqlDbType.VarChar, .Size = 50}
            Parametros(1) = New SqlParameter() With {.ParameterName = "@Email", .Value = "%" & inputFiltroEmail.Value & "%", .SqlDbType = SqlDbType.VarChar, .Size = 50}
        End If

        dt = cnn.ExecutaDataTable(SQL & IIf(SQLFiltro <> "", "WHERE " & SQLFiltro, ";"), Parametros, erro)

        If erro <> "" Then
            lblerro.InnerText = "Erro na função PreencherGrid()."
            Exit Sub
        End If

        gridInstrutor.DataSource = dt
        gridInstrutor.DataBind()

    End Sub

    Private Sub EstadoBotoes(Estado As enumEstadoBotoes)

        'Função que controla o estado dos botoes na página
        Select Case Estado
            Case enumEstadoBotoes.Limpar
                btnNovoInstrutor.Enabled = True
                btnNovoInstrutor.Attributes("cssclass") = "btn btn-primary" 'retira css disabled
                btnExcluirModal.Disabled = True
                btnExcluirModal.Attributes("class") = "btn btn-danger disabled" 'seta css disabled
                btnSalvarInstrutor.Enabled = False
                btnSalvarInstrutor.Attributes("cssclass") = "btn btn-success disabled" 'seta css disabled
                btnCancelarInstrutor.Enabled = False
                btnCancelarInstrutor.Attributes("cssclass") = "btn btn-warning disabled" 'seta css disabled
            Case enumEstadoBotoes.Inclusao
                btnNovoInstrutor.Enabled = False
                btnExcluirModal.Disabled = True
                btnSalvarInstrutor.Enabled = True
                btnCancelarInstrutor.Enabled = True
                btnNovoInstrutor.Attributes("cssclass") = "btn btn-primary disabled"
                btnExcluirModal.Attributes("class") = "btn btn-danger disabled"
                btnSalvarInstrutor.Attributes("cssclass") = "btn btn-success"
                btnCancelarInstrutor.Attributes("cssclass") = "btn btn-warning"
            Case enumEstadoBotoes.Alteracao
                btnNovoInstrutor.Enabled = False
                btnExcluirModal.Disabled = False
                btnSalvarInstrutor.Enabled = True
                btnCancelarInstrutor.Enabled = True
                btnNovoInstrutor.Attributes("cssclass") = "btn btn-primary disabled"
                btnExcluirModal.Attributes("class") = "btn btn-danger"
                btnSalvarInstrutor.Attributes("cssclass") = "btn btn-success"
                btnCancelarInstrutor.Attributes("cssclass") = "btn btn-warning"
        End Select

    End Sub

#End Region

#Region "Botões"

    Protected Sub btnNovoInstrutor_Click(sender As Object, e As EventArgs)

        LimparCampos()
        DesabilitarCampos(False)
        EstadoBotoes(enumEstadoBotoes.Inclusao)

        inputNome.Focus()

    End Sub

    Protected Sub btnSalvarInstrutor_Click(sender As Object, e As EventArgs)
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

        If booErro Then
            RadScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "ModalSucesso", jquery, True)
            Exit Sub
        End If

        Dim strSQL As String = ""

        If Operacao.Value = "Inclusao" Then
            strSQL = $"INSERT INTO tbInstrutor 
                            ( Nome, DtNascimento, Email)
                       SELECT @Nome, @DtNascimento, @Email"
        Else
            strSQL =
                $"UPDATE tbInstrutor
                     SET Nome = @Nome
                       , DtNascimento = @DtNascimento
                       , Email = @Email 
                   WHERE ID = @ID;"
        End If

        Dim erro As String = ""

        Dim Parametros As SqlParameter() = New SqlParameter(3) {}

        Parametros(0) = New SqlParameter() With {.ParameterName = "@Nome", .Value = inputNome.Value, .SqlDbType = SqlDbType.VarChar, .Size = 50}
        Parametros(1) = New SqlParameter() With {.ParameterName = "@DtNascimento", .Value = Format(CDate(inputDataNascimento.Value), ("yyyy-MM-dd 00:00:00")), .SqlDbType = SqlDbType.DateTime, .Size = 50}
        Parametros(2) = New SqlParameter() With {.ParameterName = "@Email", .Value = inputEmail.Value, .SqlDbType = SqlDbType.VarChar, .Size = 50}
        Parametros(3) = New SqlParameter() With {.ParameterName = "@ID", .Value = IIf(ID.Value = "", 0, ID.Value), .SqlDbType = SqlDbType.Int}

        cnn.ExecutaQuery(strSQL, Parametros, erro)

        If erro <> "" Then
            lblerro.InnerText = erro
            Exit Sub
        End If

        PreencherGrid(False)
        LimparCampos()

        RadScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "ModalSucesso", "$('.ModalSucesso').modal('show');", True)

    End Sub

    Protected Sub btnCancelarInstrutor_Click(sender As Object, e As EventArgs)
        LimparCampos()
        DesabilitarCampos(True)
    End Sub

    Protected Sub btnExcluirInstrutor_Click(sender As Object, e As EventArgs)
        Dim SQL As String = ""
        Dim erro As String = ""

        If Operacao.Value = "Alteracao" And CInt(ID.Value) <> 0 Then
            SQL = $"DELETE FROM tbInstrutor WHERE ID ={ID.Value};"
            cnn.ExecutaQuery(SQL, , erro)

            LimparCampos()
            DesabilitarCampos(True)
            PreencherGrid(False)
        End If

    End Sub

    Protected Sub btnFiltrarInstrutor_Click(sender As Object, e As EventArgs)
        PreencherGrid(True)
    End Sub

#End Region

    Protected Sub gridInstrutor_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs)

        If e.CommandName = "Select" Then

            LimparCampos()
            DesabilitarCampos(False)
            EstadoBotoes(enumEstadoBotoes.Alteracao)

            Try
                'Recupera o objeto grid
                Dim Grid As RadGrid = sender

                'Pega a linha selecionada da grid
                Dim linhaSelecionada As GridDataItem = TryCast(e.Item, Telerik.Web.UI.GridDataItem)

                Operacao.Value = "Alteracao"

                TabInstrutor.SelectedIndex = 0
                MultiPageInstrutor.SelectedIndex = 0

                'Recupera o valor da coluna de índice 1 que é a coluna ID
                ID.Value = linhaSelecionada(Grid.Columns(1)).Text

                inputNome.Value = linhaSelecionada(Grid.Columns(2)).Text
                inputDataNascimento.Value = CDate(linhaSelecionada(Grid.Columns(3)).Text).ToString("yyyy-MM-dd")
                inputEmail.Value = linhaSelecionada(Grid.Columns(4)).Text

            Catch ex As Exception
                lblerro.InnerText = erro
            End Try

        End If

    End Sub

End Class