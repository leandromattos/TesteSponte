Imports System.Data.SqlClient
Imports System.Deployment
Imports System.Drawing
Imports Telerik.Web
Imports Telerik.Web.UI

Public Class Controle_Live
    Inherits System.Web.UI.Page

    Dim cnn As New cnn
    Dim erro As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack Then
            Exit Sub
        End If

        CarregarCombo()
        LimparCampos()
        PreencherGrid(False)
        TabControleLives.SelectedIndex = 0
        MultiPageControleLives.SelectedIndex = 0

    End Sub

#Region "Funções"

    Private Sub DesabilitarCampos(boo As Boolean)
        comboInscritos.Enabled = Not boo
        inputDataVencimento.Disabled = boo
        comboPago.Enabled = Not boo
        inputValor.Disabled = boo
    End Sub

    Private Sub LimparValidacao()
        inputDataVencimento.Attributes("class") = "form-control inputDataVencimento"
        inputValor.Attributes("class") = "form-control inputValor"
    End Sub

    Private Sub EstadoBotoes(Estado As enumEstadoBotoes)

        'Função que controla o estado dos botoes na página
        Select Case Estado
            Case enumEstadoBotoes.Limpar
                btnNovoControleLives.Enabled = True
                btnNovoControleLives.Attributes("cssclass") = "btn btn-primary" 'retira css disabled
                btnExcluirModal.Disabled = True
                btnExcluirModal.Attributes("class") = "btn btn-danger disabled" 'seta css disabled
                btnSalvarControleLives.Enabled = False
                btnSalvarControleLives.Attributes("cssclass") = "btn btn-success disabled" 'seta css disabled
                btnCancelarControleLives.Enabled = False
                btnCancelarControleLives.Attributes("cssclass") = "btn btn-warning disabled" 'seta css disabled
            Case enumEstadoBotoes.Inclusao
                btnNovoControleLives.Enabled = False
                btnExcluirModal.Disabled = True
                btnSalvarControleLives.Enabled = True
                btnCancelarControleLives.Enabled = True
                btnNovoControleLives.Attributes("cssclass") = "btn btn-primary disabled"
                btnExcluirModal.Attributes("class") = "btn btn-danger disabled"
                btnSalvarControleLives.Attributes("cssclass") = "btn btn-success"
                btnCancelarControleLives.Attributes("cssclass") = "btn btn-warning"
            Case enumEstadoBotoes.Alteracao
                btnNovoControleLives.Enabled = False
                btnExcluirModal.Disabled = False
                btnSalvarControleLives.Enabled = True
                btnCancelarControleLives.Enabled = True
                btnNovoControleLives.Attributes("cssclass") = "btn btn-primary disabled"
                btnExcluirModal.Attributes("class") = "btn btn-danger"
                btnSalvarControleLives.Attributes("cssclass") = "btn btn-success"
                btnCancelarControleLives.Attributes("cssclass") = "btn btn-warning"
        End Select

    End Sub

    Private Sub LimparCampos()

        'Flag de controle de alteração
        Operacao.Value = "Inclusao"
        IDInscrito.Value = ""
        lblerro.InnerText = ""

        DesabilitarCampos(True)
        LimparValidacao()
        EstadoBotoes(enumEstadoBotoes.Limpar)

        comboInscritos.ClearSelection()
        comboInscritos.Text = ""
        inputDataVencimento.Value = ""
        inputValor.Value = "0,00"
        comboPago.SelectedIndex = 0
        linhapagamento.Text = ""
        lblerrorinscrito.Text = ""
    End Sub

    Private Sub CarregarCombo()

        Dim SQL As String = ""
        Dim dt As New DataTable
        Dim item As New ListItem

        'Inscrito
        SQL = $"SELECT ID, Nome
                  FROM tbInscritos
                 WHERE ID NOT IN ( SELECT IDInscrito FROM tbLives_Inscritos WHERE IDLive = {IDLive.Value}"

        If isnumeric(IDInscrito.Value) Then
            If CInt(IDInscrito.Value) > 0 Then
                SQL += " AND IDInscrito <> " & IDInscrito.Value & " "
            End If
        End If

        SQL += " ) ORDER BY Nome;"

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

        gridControleLives.DataSource = dt
        gridControleLives.DataBind()

    End Sub

    Private Sub PreencherGridInscritos(Filtro As Boolean)
        Dim dt As New DataTable
        Dim SQL As String = ""
        Dim SQLFiltro As String = ""
        Dim erro As String = ""
        Dim Parametros As SqlParameter() = Nothing

        SQL = $"SELECT tbInscritos.ID as IDInscrito
                     , tbInscritos.Nome
                     , DtVencimento
                     , Valor_inscricao
                     , Pago
                  FROM tbLives_Inscritos
  			INNER JOIN tbLive
					ON tbLives_Inscritos.IDLive = tbLive.ID
			inner join tbInscritos
					ON tbLives_Inscritos.IDInscrito = tbInscritos.ID
                 WHERE tbLives_Inscritos.IDLive = @IDLive "

        Parametros = New SqlParameter(0) {}

        Try
            Parametros(0) = New SqlParameter() With {.ParameterName = "@IDLive", .Value = IIf(IsNumeric(IDLive.Value), IDLive.Value, DBNull.Value), .SqlDbType = SqlDbType.Int}
        Catch ex As Exception
            lblerro.InnerText = ex.Message
            Exit Sub
        End Try

        dt = cnn.ExecutaDataTable(SQL & IIf(SQLFiltro <> "", " WHERE " & SQLFiltro, ""), Parametros, erro)

        If erro <> "" Then
            lblerro.InnerText = "Erro na função PreencherGrid()."
            Exit Sub
        End If

        gridInscritosLive.DataSource = dt
        gridInscritosLive.DataBind()

    End Sub

#End Region

#Region "Botões"

    Protected Sub btnNovoControleLives_Click(sender As Object, e As EventArgs)

        LimparCampos()
        DesabilitarCampos(False)
        EstadoBotoes(enumEstadoBotoes.Inclusao)
        CarregarCombo()
        Operacao.Value = "Inclusao"
        comboInscritos.Focus()

    End Sub

    Protected Sub btnSalvarControleLives_Click(sender As Object, e As EventArgs)
        'variavel que controla se houve erro de validação
        Dim booErro As Boolean = False
        Dim jquery As String = ""

        LimparValidacao()

        If IDLive.Value = "" Or IsNumeric(IDLive.Value) = False Then
            lblerro.InnerText = "Selecione uma live para prosseguir."
            Exit Sub
        End If

        If comboInscritos.SelectedValue = "" Then
            lblerrorinscrito.Text = "Selecione um inscrito"
            booErro = True
        End If

        If inputDataVencimento.Value = "" Or IsDate(inputDataVencimento.Value) = False Then
            inputDataVencimento.Attributes("class") = "form-control inputDataVencimento is-invalid"
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

        If booErro Then
            RadScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "ModalSucesso", jquery, True)
            Exit Sub
        End If

        Dim strSQL As String = ""

        If Operacao.Value = "Inclusao" Then
            strSQL = $"INSERT INTO tbLives_Inscritos 
                            ( IDLive, IDInscrito, Valor_inscricao, DtVencimento, Pago )
                       SELECT @IDLive, @IDInscrito, @Valor_inscricao, @DtVencimento, @Pago "
        Else
            strSQL =
                $"UPDATE tbLives_Inscritos
                     SET IDInscrito = @IDInscrito
                       , Valor_inscricao = @Valor_inscricao
                       , DtVencimento = @DtVencimento
                       , Pago = @Pago
                   WHERE IDLive = @IDLive 
                     AND IDInscrito = @IDInscritoOriginal "
        End If

        Dim erro As String = ""

        Dim Parametros As SqlParameter() = New SqlParameter(5) {}

        Try
            Parametros(0) = New SqlParameter() With {.ParameterName = "@IDLive", .Value = IDLive.Value, .SqlDbType = SqlDbType.Int}
            Parametros(1) = New SqlParameter() With {.ParameterName = "@IDInscritoOriginal", .Value = IIf(IDInscrito.Value = "", DBNull.Value, IDInscrito.Value), .SqlDbType = SqlDbType.Int}
            Parametros(2) = New SqlParameter() With {.ParameterName = "@IDInscrito", .Value = comboInscritos.SelectedValue, .SqlDbType = SqlDbType.Int}
            Parametros(3) = New SqlParameter() With {.ParameterName = "@Valor_inscricao", .Value = inputValor.Value.Replace("-", ""), .SqlDbType = SqlDbType.Decimal, .Precision = 18, .Scale = 2}
            Parametros(4) = New SqlParameter() With {.ParameterName = "@DtVencimento", .Value = Format(CDate(inputDataVencimento.Value), ("yyyy-MM-dd 00:00:00")), .SqlDbType = SqlDbType.DateTime, .Size = 50}
            Parametros(5) = New SqlParameter() With {.ParameterName = "@Pago", .Value = comboPago.SelectedValue, .SqlDbType = SqlDbType.Int}
        Catch ex As Exception
            lblerro.InnerText = ex.Message
        End Try

        cnn.ExecutaQuery(strSQL, Parametros, erro)

        If erro <> "" Then
            lblerro.InnerText = erro
            Exit Sub
        End If

        PreencherGridInscritos(False)
        LimparCampos()
        CarregarCombo()

        RadScriptManager.RegisterStartupScript(Me.Page, Me.Page.GetType(), "ModalSucesso", "$('.ModalSucesso').modal('show');", True)

    End Sub

    Protected Sub btnCancelarControleLives_Click(sender As Object, e As EventArgs)
        LimparCampos()
        DesabilitarCampos(True)
    End Sub

    Protected Sub btnExcluirControleLives_Click(sender As Object, e As EventArgs)
        Dim SQL As String = ""
        Dim erro As String = ""

        If Operacao.Value = "Alteracao" And IsNumeric(IDInscrito.Value) And IsNumeric(IDInscrito.Value) Then
            SQL = $"DELETE FROM tbLives_Inscritos 
                          WHERE IDLive = @IDLive 
                            AND IDInscrito = @IDInscrito;"

            Dim Parametros As SqlParameter() = New SqlParameter(1) {}
            Parametros(0) = New SqlParameter() With {.ParameterName = "@IDLive", .Value = IDLive.Value, .SqlDbType = SqlDbType.Int}
            Parametros(1) = New SqlParameter() With {.ParameterName = "@IDInscrito", .Value = IDInscrito.Value, .SqlDbType = SqlDbType.Int}

            cnn.ExecutaQuery(SQL, Parametros, erro)

            If erro <> "" Then
                lblerro.InnerText = erro
            End If

            LimparCampos()
            PreencherGridInscritos(False)
            CarregarCombo()
        End If
    End Sub

    Protected Sub btnFiltrarControleLives_Click(sender As Object, e As EventArgs)
        PreencherGrid(True)
    End Sub

    Protected Sub btnLimparControleLives_Click(sender As Object, e As EventArgs)
        inputFiltroNome.Value = ""
        inputFiltroData.Value = ""
        comboFiltroInstrutor.ClearSelection()
        comboFiltroInstrutor.SelectedIndex = 0
        PreencherGrid(False)
    End Sub

#End Region

    Protected Sub gridControleLives_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs)

        If e.CommandName = "Select" Then

            LimparCampos()
            DesabilitarCampos(True)
            EstadoBotoes(enumEstadoBotoes.Limpar)

            Try
                'Recupera o objeto grid
                Dim Grid As RadGrid = sender

                'Pega a linha selecionada da grid
                Dim linhaSelecionada As GridDataItem = TryCast(e.Item, Telerik.Web.UI.GridDataItem)

                MultiPageControleLives.SelectedIndex = 1
                TabControleLives.Enabled = True
                TabControleLives.SelectedIndex = 1

                'Recupera o valor da coluna de índice 1 que é a coluna ID
                IDLive.Value = linhaSelecionada(Grid.Columns(1)).Text

                inputNomeLive.Value = ""
                inputNomeInstrutor.Value = ""
                inputDataLive.Value = ""
                inputValorLive.Value = ""

                inputNomeLive.Value = linhaSelecionada(Grid.Columns(2)).Text
                inputNomeInstrutor.Value = linhaSelecionada(Grid.Columns(8)).Text
                inputDataLive.Value = linhaSelecionada(Grid.Columns(4)).Text
                inputValorLive.Value = linhaSelecionada(Grid.Columns(6)).Text

                PreencherGridInscritos(False)
                CarregarCombo()

            Catch ex As Exception
                lblerro.InnerText = erro
            End Try

        End If

    End Sub

    Protected Sub gridInscritosLive_ItemCommand(sender As Object, e As GridCommandEventArgs)

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

                'Recupera o valor da coluna de índice 1 que é a coluna ID
                IDInscrito.Value = linhaSelecionada(Grid.Columns(1)).Text
                CarregarCombo()

                comboInscritos.ClearSelection()
                comboInscritos.SelectedValue = linhaSelecionada(Grid.Columns(1)).Text
                comboInscritos.Text = linhaSelecionada(Grid.Columns(2)).Text

                inputDataVencimento.Value = CDate(linhaSelecionada(Grid.Columns(3)).Text).ToString("yyyy-MM-dd")
                inputValor.Value = linhaSelecionada(Grid.Columns(4)).Text.Replace("R$ ", "")

                comboPago.SelectedIndex = CInt(linhaSelecionada(Grid.Columns(5)).Text)
                linhapagamento.Text = "00000.00000 00000.000000 00000.000000 0 "
                linhapagamento.Text = linhapagamento.Text & DateDiff(DateInterval.Day, CDate("07/10/1997"), CDate(linhaSelecionada(Grid.Columns(3)).Text))
                linhapagamento.Text = linhapagamento.Text & CInt(inputValor.Value.Replace(",", "").Replace(".", "")).ToString("D10")

                Select Case comboPago.SelectedValue
                    Case 1
                        linhapagamento.ForeColor = Color.Blue
                        linhapagamento.Font.Strikeout = True
                    Case 0
                        linhapagamento.ForeColor = Color.Red
                        linhapagamento.Font.Strikeout = False
                End Select

            Catch ex As Exception
                lblerro.InnerText = erro
            End Try

        End If

    End Sub

End Class