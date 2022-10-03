Imports System.Data.SqlClient
Imports Telerik.Web.UI

Public Class Cadastro_Lives
    Inherits System.Web.UI.Page

    Dim cnn As New cnn
    Dim erro As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack Then
            Exit Sub
        End If

        LimparCampos()
        PreencherGrid(False)
        TabLives.SelectedIndex = 0
        MultiPageLives.SelectedIndex = 0

    End Sub

#Region "Funções"

    Private Sub DesabilitarCampos(boo As Boolean)
        inputNome.Disabled = boo
        inputDescricao.Disabled = boo
        inputData.Disabled = boo
        inputDuracao.Disabled = boo
        inputValor.Disabled = boo
        comboInstrutor.Enabled = Not boo
    End Sub

    Private Sub LimparValidacao()
        inputNome.Attributes("class") = "form-control inputNome"
        inputDescricao.Attributes("class") = "form-control inputDescricao"
        inputData.Attributes("class") = "form-control inputData"
        inputValor.Attributes("class") = "form-control inputValor"
        inputDuracao.Attributes("class") = "form-control inputDuracao"
        comboInstrutor.Attributes("class") = "form-control btn btn-info dropdown-toggle comboInstrutor"
    End Sub

    Private Sub EstadoBotoes(Estado As enumEstadoBotoes)

        'Função que controla o estado dos botoes na página
        Select Case Estado
            Case enumEstadoBotoes.Limpar
                btnNovoLives.Enabled = True
                btnNovoLives.Attributes("cssclass") = "btn btn-primary" 'retira css disabled
                btnExcluirModal.Disabled = True
                btnExcluirModal.Attributes("class") = "btn btn-danger disabled" 'seta css disabled
                btnSalvarLives.Enabled = False
                btnSalvarLives.Attributes("cssclass") = "btn btn-success disabled" 'seta css disabled
                btnCancelarLives.Enabled = False
                btnCancelarLives.Attributes("cssclass") = "btn btn-warning disabled" 'seta css disabled
            Case enumEstadoBotoes.Inclusao
                btnNovoLives.Enabled = False
                btnExcluirModal.Disabled = True
                btnSalvarLives.Enabled = True
                btnCancelarLives.Enabled = True
                btnNovoLives.Attributes("cssclass") = "btn btn-primary disabled"
                btnExcluirModal.Attributes("class") = "btn btn-danger disabled"
                btnSalvarLives.Attributes("cssclass") = "btn btn-success"
                btnCancelarLives.Attributes("cssclass") = "btn btn-warning"
            Case enumEstadoBotoes.Alteracao
                btnNovoLives.Enabled = False
                btnExcluirModal.Disabled = False
                btnSalvarLives.Enabled = True
                btnCancelarLives.Enabled = True
                btnNovoLives.Attributes("cssclass") = "btn btn-primary disabled"
                btnExcluirModal.Attributes("class") = "btn btn-danger"
                btnSalvarLives.Attributes("cssclass") = "btn btn-success"
                btnCancelarLives.Attributes("cssclass") = "btn btn-warning"
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
        inputDescricao.Value = ""
        inputData.Value = ""
        inputDuracao.Value = "0"
        inputValor.Value = "0,00"
        CarregarCombo()

    End Sub

    Private Sub CarregarCombo()

        Dim SQL As String = ""
        Dim dt As New DataTable
        Dim item As New ListItem

        'Instrutor
        SQL = $"SELECT ID, Nome
                  FROM tbInstrutor
              ORDER BY Nome;"
        dt = cnn.ExecutaDataTable(SQL)

        item = New ListItem
        item.Text = "(Vazio)"
        item.Value = ""
        comboInstrutor.Items.Clear()
        comboInstrutor.Items.Add(item)
        comboFiltroInstrutor.Items.Clear()
        comboFiltroInstrutor.Items.Add(item)

        If dt.Rows.Count > 0 Then
            item = New ListItem

            For i = 0 To dt.Rows.Count - 1
                item = New ListItem
                item.Value = dt(i)("ID")
                item.Text = dt(i)("Nome")
                comboInstrutor.Items.Add(item)
                comboFiltroInstrutor.Items.Add(item)
            Next

            dt.Dispose()
        End If


        'Inscrito
        SQL = $"SELECT ID, Nome
                  FROM tbInscritos
              ORDER BY Nome;"
        dt = cnn.ExecutaDataTable(SQL)

        comboInscritos.DataSource = dt
        comboInscritos.DataBind()

    End Sub

    Private Sub PreencherGrid(Filtro As Boolean)
        Dim dt As New DataTable
        Dim SQL As String = ""
        Dim SQLFiltro As String = ""
        Dim erro As String = ""
        Dim Parametros As SqlParameter() = Nothing

        SQL = $"SELECT tbLive.ID
                     , tbLive.Nome
                     , Descricao
                     , Data
                     , Duracao
                     , Valor
                     , IDInstrutor
                     , tbInstrutor.Nome AS NomeInstrutor
                  FROM tbLive 
            INNER JOIN tbInstrutor 
                    ON tbLive.IDInstrutor = tbInstrutor.ID"

        If Filtro = True Then
            If inputFiltroNome.Value <> "" Then
                SQLFiltro = "tbLive.Nome LIKE @Nome "
            End If

            If comboFiltroInstrutor.SelectedValue <> "" Then
                If SQLFiltro <> "" Then
                    SQLFiltro = SQLFiltro & " AND "
                End If
                SQLFiltro = " IDInstrutor = @IDInstrutor"
            End If

            Dim filtroData As String = ""

            If inputFiltroData.Value = "" Then

            Else
                filtroData = Format(CDate(inputFiltroData.Value), ("yyyy-MM-dd 00:00:00"))

                If SQLFiltro <> "" Then
                    SQLFiltro += " AND "
                End If

                SQLFiltro += "Data = @Data"
            End If

            Parametros = New SqlParameter(2) {}

            Try
                Parametros(0) = New SqlParameter() With {.ParameterName = "@Nome", .Value = "%" & inputFiltroNome.Value & "%", .SqlDbType = SqlDbType.VarChar, .Size = 50}
                Parametros(1) = New SqlParameter() With {.ParameterName = "@IDInstrutor", .Value = IIf(comboFiltroInstrutor.SelectedValue = "", DBNull.Value, comboFiltroInstrutor.SelectedValue), .SqlDbType = SqlDbType.Int}
                Parametros(2) = New SqlParameter() With {.ParameterName = "@Data", .Value = IIf(filtroData = "", DBNull.Value, filtroData), .SqlDbType = SqlDbType.DateTime}

            Catch ex As Exception
                lblerro.InnerText = ex.Message
                Exit Sub
            End Try

        End If

        dt = cnn.ExecutaDataTable(SQL & IIf(SQLFiltro <> "", " WHERE " & SQLFiltro, ""), Parametros, erro)

        If erro <> "" Then
            lblerro.InnerText = "Erro na função PreencherGrid()."
            Exit Sub
        End If

        gridLives.DataSource = dt
        gridLives.DataBind()

    End Sub

    Private Sub PreencherGridInscritos(Filtro As Boolean)
        Dim dt As New DataTable
        Dim SQL As String = ""
        Dim SQLFiltro As String = ""
        Dim erro As String = ""
        Dim Parametros As SqlParameter() = Nothing

        SQL = $"SELECT tbLive.ID
                     , tbLive.Nome
                     , Descricao
                     , Data
                     , Duracao
                     , Valor
                     , IDInstrutor
                     , tbInstrutor.Nome AS NomeInstrutor
                  FROM tbLive 
            INNER JOIN tbInstrutor 
                    ON tbLive.IDInstrutor = tbInstrutor.ID"

        If Filtro = True Then
            If inputFiltroNome.Value <> "" Then
                SQLFiltro = "tbLive.Nome LIKE @Nome "
            End If

            If comboFiltroInstrutor.SelectedValue <> "" Then
                If SQLFiltro <> "" Then
                    SQLFiltro = SQLFiltro & " AND "
                End If
                SQLFiltro = " IDInstrutor = @IDInstrutor"
            End If

            Dim filtroData As String = ""

            If inputFiltroData.Value = "" Then

            Else
                filtroData = Format(CDate(inputFiltroData.Value), ("yyyy-MM-dd 00:00:00"))

                If SQLFiltro <> "" Then
                    SQLFiltro += " AND "
                End If

                SQLFiltro += "Data = @Data"
            End If

            Parametros = New SqlParameter(2) {}

            Try
                Parametros(0) = New SqlParameter() With {.ParameterName = "@Nome", .Value = "%" & inputFiltroNome.Value & "%", .SqlDbType = SqlDbType.VarChar, .Size = 50}
                Parametros(1) = New SqlParameter() With {.ParameterName = "@IDInstrutor", .Value = IIf(comboFiltroInstrutor.SelectedValue = "", DBNull.Value, comboFiltroInstrutor.SelectedValue), .SqlDbType = SqlDbType.Int}
                Parametros(2) = New SqlParameter() With {.ParameterName = "@Data", .Value = IIf(filtroData = "", DBNull.Value, filtroData), .SqlDbType = SqlDbType.DateTime}

            Catch ex As Exception
                lblerro.InnerText = ex.Message
                Exit Sub
            End Try

        End If

        dt = cnn.ExecutaDataTable(SQL & IIf(SQLFiltro <> "", " WHERE " & SQLFiltro, ""), Parametros, erro)

        If erro <> "" Then
            lblerro.InnerText = "Erro na função PreencherGrid()."
            Exit Sub
        End If

        gridLives.DataSource = dt
        gridLives.DataBind()

    End Sub

#End Region

#Region "Botões"

    Protected Sub btnNovoLives_Click(sender As Object, e As EventArgs)

        LimparCampos()
        DesabilitarCampos(False)
        EstadoBotoes(enumEstadoBotoes.Inclusao)

        TabLives.SelectedIndex = 0
        MultiPageLives.SelectedIndex = 0
        inputNome.Focus()

    End Sub

    Protected Sub btnSalvarLives_Click(sender As Object, e As EventArgs)
        'variavel que controla se houve erro de validação
        Dim booErro As Boolean = False
        Dim jquery As String = ""

        LimparValidacao()

        If inputNome.Value = "" Then
            inputNome.Attributes("class") = "form-control inputNome is-invalid"
            booErro = True
        End If

        If inputDescricao.Value = "" Then
            inputDescricao.Attributes("class") = "form-control inputDescricao is-invalid"
            booErro = True
        End If

        If inputData.Value = "" Or IsDate(inputData.Value) = False Then
            inputData.Attributes("class") = "form-control inputData is-invalid"
            booErro = True
        End If

        If inputDuracao.Value = "" Then
            inputDuracao.Attributes("class") = "form-control inputDuracao is-invalid"
            booErro = True
        ElseIf IsNumeric(inputDuracao.Value) = False Then
            inputDuracao.Attributes("class") = "form-control inputDuracao is-invalid"
            booErro = True
        ElseIf CInt(inputDuracao.Value) <= 0 Then
            inputDuracao.Attributes("class") = "form-control inputDuracao is-invalid"
            booErro = True
        End If

        If inputValor.Value = "" Then
            inputValor.Attributes("class") = "form-control inputValor is-invalid"
            booErro = True
        ElseIf IsNumeric(inputValor.Value.Replace(".", "").Replace(",", "")) = False Then
            inputValor.Attributes("class") = "form-control inputValor is-invalid"
            booErro = True
        ElseIf CInt(inputValor.Value.Replace(".", "").Replace(",", "")) <= 0 Then
            inputValor.Attributes("class") = "form-control inputValor is-invalid"
            booErro = True
        End If

        If comboInstrutor.SelectedValue = "" Then
            jquery = "$('.comboInstrutor').addClass('is-invalid');"
            comboInstrutor.Attributes("class") = "form-control btn btn-info dropdown-toggle comboInstrutor is-invalid"
            booErro = True
        End If

        If booErro Then
            RadScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "ModalSucesso", jquery, True)
            Exit Sub
        End If

        Dim strSQL As String = ""

        If Operacao.Value = "Inclusao" Then
            strSQL = $"INSERT INTO tbLive 
                            ( Nome, Descricao, Data, Duracao, Valor, IDInstrutor )
                       SELECT @Nome, @Descricao, @Data, @Duracao, @Valor, @IDInstrutor"
        Else
            strSQL =
                $"UPDATE tbLive
                     SET Nome = @Nome
                       , Descricao = @Descricao
                       , Data = @Data
                       , Duracao = @Duracao
                       , Valor = @Valor
                       , IDInstrutor = @IDInstrutor
                   WHERE ID = @ID;"
        End If

        Dim erro As String = ""

        Dim Parametros As SqlParameter() = New SqlParameter(6) {}

        Try
            Parametros(0) = New SqlParameter() With {.ParameterName = "@ID", .Value = IIf(ID.Value = "", 0, ID.Value), .SqlDbType = SqlDbType.Int}
            Parametros(1) = New SqlParameter() With {.ParameterName = "@Nome", .Value = inputNome.Value, .SqlDbType = SqlDbType.VarChar, .Size = 50}
            Parametros(2) = New SqlParameter() With {.ParameterName = "@Descricao", .Value = inputDescricao.Value, .SqlDbType = SqlDbType.VarChar, .Size = 50}
            Parametros(3) = New SqlParameter() With {.ParameterName = "@Data", .Value = Format(CDate(inputData.Value), ("yyyy-MM-dd 00:00:00")), .SqlDbType = SqlDbType.DateTime, .Size = 50}
            Parametros(4) = New SqlParameter() With {.ParameterName = "@Duracao", .Value = inputDuracao.Value.Replace(" ", ""), .SqlDbType = SqlDbType.Int}
            Parametros(5) = New SqlParameter() With {.ParameterName = "@Valor", .Value = inputValor.Value.Replace("-", ""), .SqlDbType = SqlDbType.Decimal, .Precision = 18, .Scale = 2}
            Parametros(6) = New SqlParameter() With {.ParameterName = "@IDInstrutor", .Value = comboInstrutor.SelectedValue, .SqlDbType = SqlDbType.Int}
        Catch ex As Exception
            lblerro.InnerText = ex.Message
        End Try

        cnn.ExecutaQuery(strSQL, Parametros, erro)

        If erro <> "" Then
            lblerro.InnerText = erro
            Exit Sub
        End If

        PreencherGrid(False)
        LimparCampos()

        RadScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "ModalSucesso", "$('.ModalSucesso').modal('show');", True)

    End Sub

    Protected Sub btnCancelarLives_Click(sender As Object, e As EventArgs)
        LimparCampos()
        DesabilitarCampos(True)
    End Sub

    Protected Sub btnExcluirLives_Click(sender As Object, e As EventArgs)
        Dim SQL As String = ""
        Dim erro As String = ""

        If Operacao.Value = "Alteracao" And CInt(ID.Value) <> 0 Then
            SQL = $"DELETE FROM tbLive WHERE ID ={ID.Value};"
            cnn.ExecutaQuery(SQL, , erro)

            If erro <> "" Then
                lblerro.InnerText = erro
            End If

            LimparCampos()
            PreencherGrid(False)
        End If

    End Sub

    Protected Sub btnFiltrarLives_Click(sender As Object, e As EventArgs)
        PreencherGrid(True)
    End Sub

    Protected Sub btnLimparFiltroLives_Click(sender As Object, e As EventArgs)
        inputFiltroNome.Value = ""
        inputFiltroData.Value = ""
        comboFiltroInstrutor.ClearSelection()
        comboFiltroInstrutor.SelectedIndex = 0
        PreencherGrid(False)
    End Sub

#End Region

    Protected Sub gridLives_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs)

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

                TabLives.SelectedIndex = 0
                MultiPageLives.SelectedIndex = 0

                'Recupera o valor da coluna de índice 1 que é a coluna ID
                ID.Value = linhaSelecionada(Grid.Columns(1)).Text

                inputNome.Value = linhaSelecionada(Grid.Columns(2)).Text
                inputDescricao.Value = linhaSelecionada(Grid.Columns(3)).Text
                inputData.Value = CDate(linhaSelecionada(Grid.Columns(4)).Text).ToString("yyyy-MM-dd")
                inputDuracao.Value = linhaSelecionada(Grid.Columns(5)).Text
                inputValor.Value = linhaSelecionada(Grid.Columns(6)).Text.Replace("R$ ", "")

                comboInstrutor.ClearSelection()
                comboInstrutor.Items.FindByValue(linhaSelecionada(Grid.Columns(7)).Text).Selected = True

            Catch ex As Exception
                lblerro.InnerText = erro
            End Try

        End If

    End Sub

    Protected Sub gridInscritosLive_ItemCommand(sender As Object, e As GridCommandEventArgs)

    End Sub

End Class