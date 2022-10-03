<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Paginas/Site1.Master" CodeBehind="Controle_Live.aspx.vb" Inherits="Sponte.Controle_Live" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function formataCampos() {
            $(".inputValor").mask('#.##0,00', { reverse: true });
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-row" style="margin:auto;">
        <div class="form-group col-md-12 mb-3" style="margin-top: 20px;">
            <div class="col" style="text-align: center">
                <h1>Cadastro de ControleControleLives</h1>
            </div>
        </div>
        <div class="form-group col-md-6 mb-3" style="margin:auto;">
            <telerik:RadAjaxPanel ID="panelControleLives" runat="server">
                <telerik:RadTabStrip ID="TabControleLives" runat="server" MultiPageID="MultiPageControleLives" Skin="WebBlue">
                    <Tabs>
                        <telerik:RadTab Text="Lives" runat="server">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Inscritos" runat="server">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
            </telerik:RadAjaxPanel>
            <telerik:RadMultiPage runat="server" ID="MultiPageControleLives">
                <telerik:RadPageView runat="server" ID="ViewListagem" Style="background-color: #d8d8d8; padding: 20px; border: 1px solid #dee2e6;">
                    <div class="form-row mb-4" style="padding-bottom: 10px; border-bottom: 1px solid #e9ecef;">
                        <div class="col-md-4">
                            <label for="inputFiltroNome">Nome</label>
                            <input type="text" class="form-control inputFiltroNome" id="inputFiltroNome" placeholder="Procurar por nome..." runat="server">
                        </div>
                        <div class="col-md-4">
                            <label for="comboFiltroInstrutor">Instrutor</label>
                            <asp:DropDownList ID="comboFiltroInstrutor" runat="server" CssClass="form-control btn btn-info dropdown-toggle comboFiltroInstrutor">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <label for="inputFiltroData">Data da Live</label>
                            <input type="date" runat="server" id="inputFiltroData" class="form-control inputData" />
                        </div>
                    </div>
                    <div class="form-row mb-4" style="margin-top: 10px;">
                        <div class="col-md-3">
                            <asp:Button runat="server" ID="btnLimparControleLives" CssClass="btn btn-warning" Text="LimparFiltro" OnClick="btnLimparControleLives_Click" />
                            <asp:Button runat="server" ID="btnFiltrarControleLives" CssClass="btn btn-primary" Text="Filtrar" OnClick="btnFiltrarControleLives_Click" />
                        </div>
                    </div>
                    <div class="form-row mb-3">
                        <div class="col-md-12">
                            <telerik:RadAjaxPanel ID="panelgridControleLives" runat="server">
                                <telerik:RadGrid ID="gridControleLives" runat="server" AllowPaging="True" OnItemCommand="gridControleLives_ItemCommand" Skin="WebBlue">
                                    <ClientSettings>
                                        <Selecting AllowRowSelect="True"></Selecting>
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                                    </ClientSettings>
                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="ID">
                                        <Columns>
                                            <%--coluna0--%>
                                            <telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="column" HeaderText=""
                                                ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" CommandName="Select" ImageUrl="../Imagens/edit.png" HeaderStyle-Width="30px" ItemStyle-Width="30px">
                                            </telerik:GridButtonColumn>
                                            <%--coluna1--%>
                                            <telerik:GridBoundColumn DataField="ID" DataType="System.Int32" HeaderText="ID" UniqueName="ID" HeaderStyle-Width="50px" Display="false">
                                            </telerik:GridBoundColumn>
                                            <%--coluna2--%>
                                            <telerik:GridBoundColumn DataField="Nome" HeaderText="Nome" FilterControlAltText="Filter Nome column" SortExpression="Nome" UniqueName="Nome">
                                            </telerik:GridBoundColumn>
                                            <%--coluna3--%>
                                            <telerik:GridBoundColumn DataField="Descricao" HeaderText="Descrição" FilterControlAltText="Filter Descricao column" SortExpression="Descricao" UniqueName="Descricao">
                                            </telerik:GridBoundColumn>
                                            <%--coluna4--%>
                                            <telerik:GridDateTimeColumn DataField="Data" FilterControlAltText="Filter Data column" SortExpression="Data" UniqueName="Data" HeaderText="Data" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="70px">
                                            </telerik:GridDateTimeColumn>
                                            <%--coluna5--%>
                                            <telerik:GridBoundColumn DataField="Duracao" HeaderText="Duração" FilterControlAltText="Filter Duracao column" SortExpression="Duracao" UniqueName="Duracao">
                                            </telerik:GridBoundColumn>
                                            <%--coluna6--%>
                                            <telerik:GridNumericColumn DataField="Valor" FilterControlAltText="Filter Valor column" SortExpression="Valor" UniqueName="Valor" HeaderText="Valor" HeaderStyle-Width="80px" DataType="System.Decimal" NumericType="Currency">
                                                <ItemStyle ForeColor="Blue" />
                                            </telerik:GridNumericColumn>
                                            <%--coluna7--%>
                                            <telerik:GridBoundColumn DataField="IDInstrutor" DataType="System.Int32" HeaderText="IDInstrutor" UniqueName="IDInstrutor" HeaderStyle-Width="50px" Display="false">
                                            </telerik:GridBoundColumn>
                                            <%--coluna8--%>
                                            <telerik:GridBoundColumn DataField="NomeInstrutor" HeaderText="NomeInstrutor" FilterControlAltText="Filter NomeInstrutor column" SortExpression="NomeInstrutor" UniqueName="NomeInstrutor">
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </telerik:RadAjaxPanel>
                        </div>
                    </div>
                </telerik:RadPageView>
                <telerik:RadPageView runat="server" ID="ViewInscritos" Style="background-color: #d8d8d8; padding: 20px; border: 1px solid #dee2e6;">
                    <telerik:RadAjaxPanel ID="panelInscritos" runat="server">
                        <asp:HiddenField runat="server" ID="Operacao" />
                        <asp:HiddenField runat="server" ID="IDLive" />
                        <asp:HiddenField runat="server" ID="IDInscrito" />

                        <div class="col" style="text-align: center; color:rebeccapurple">
                            <h3>Dados da Live</h3>
                        </div>
                        <div class="form-row">
                            <%--Nome Live--%>
                            <div class="form-group col-md-6">
                                <label for="inputNomeLive">Live</label>
                                <input type="text" class="form-control inputNomeLive disabled" id="inputNomeLive" runat="server" disabled="disabled">
                            </div>
                            <%--Nome Instrutor--%>
                            <div class="form-group col-md-6">
                                <label for="inputNomeInstrutor">Instrutor</label>
                                <input type="text" class="form-control inputNomeInstrutor disabled" id="inputNomeInstrutor" runat="server" disabled="disabled">
                            </div>
                        </div>
                        <div class="form-row" style="border-bottom:1px solid grey">
                            <%--Valor--%>
                            <div class="form-group col-md-3">
                                <label for="inputValorLive">Valor Inscrição</label>
                                <input type="text" class="form-control inputValorLive disabled" id="inputValorLive" runat="server" disabled="disabled">
                            </div>
                            <%--Data--%>
                            <div class="form-group col-md-3">
                                <label for="inputDataLive">Data da Live</label>
                                <input type="text" class="form-control inputDataLive disabled" id="inputDataLive" runat="server" disabled="disabled">
                            </div>
                        </div>
                        <%--Inscritos--%>
                        <div class="form-row" style="margin-top:10px;">
                            <div class="form-group col-md-12">
                                <div class="form-row">
                                    <label for="comboInscritos">Inscritos</label>                                    
                                </div>
                                <div class="form-row">
                                    <telerik:RadComboBox RenderMode="Lightweight" ID="comboInscritos" AllowCustomText="true" runat="server" Width="100%"
                                        DataTextField="Nome" DataValueField="ID" EmptyMessage="Procure pelo nome..." Skin="WebBlue" Filter="Contains" ValidateRequestMode="Enabled" ValidationGroup="divmsg">
                                    </telerik:RadComboBox>
                                </div>                                                                                                
                                <div class="form-row">
                                    <asp:Label runat="server" ID="lblerrorinscrito" ForeColor="Red" Font-Size="Medium"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="form-row">
                            <%--Data Vencimento--%>
                            <div class="form-group col-md-4">
                                <label for="inputDataVencimento">Data de Vencimento</label>
                                <input type="date" runat="server" id="inputDataVencimento" class="form-control inputDataVencimento" />
                                <div class="invalid-feedback" runat="server" id="Div1">
                                    Valor informado inválido!
                                </div>
                            </div>
                            <%--Valor--%>
                            <div class="form-group col-md-4">
                                <label for="inputValor">Valor Inscrição</label>
                                <input type="text" class="form-control inputValor" id="inputValor" placeholder="0,00" onkeypress="$(this).mask('#.##0,00',{reverse: true})" runat="server">
                                <div class="invalid-feedback">
                                    O valor não pode ser vazio ou 0,00.
                                </div>
                            </div>
                            <%--Situação Pagamento--%>
                            <div class="form-group col-md-4">
                                <label for="comboPago">Inscrição Paga</label>
                                <asp:DropDownList ID="comboPago" runat="server" CssClass="form-control btn btn-info dropdown-toggle comboPago">
                                    <asp:ListItem Text="Não" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="Sim" Value="1"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-row">
                            <div class="form-group col-md-12">
                                <label for="linhapagamento">Linha Digitável:</label>
                                <asp:Label runat="server" ID="linhapagamento" Text="111111111111111111111111111111111111111111111111" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                        <%--Botoes--%>
                        <div class="modal-body">
                            <asp:Button runat="server" ID="btnNovoControleLives" CssClass="btn btn-primary" OnClick="btnNovoControleLives_Click" Text="Novo" />
                            <asp:Button runat="server" ID="btnSalvarControleLives" CssClass="btn btn-success" OnClick="btnSalvarControleLives_Click" Text="Salvar" />
                            <asp:Button runat="server" ID="btnCancelarControleLives" CssClass="btn btn-warning" OnClick="btnCancelarControleLives_Click" Text="Cancelar" Style="color: white;" />
                            <!-- Button trigger modal -->
                            <button id="btnExcluirModal" type="button" class="btn btn-danger" data-toggle="modal" data-target="#Modal" runat="server">
                                Excluir
                            </button>
                            <!-- Modal exclusão -->
                            <div class="modal fade" id="Modal" tabindex="=1" role="dialog" data-backdrop="static" data-keyboard="false" aria-labelledby="exampleModalLabel" aria-hidden="true" style="z-index: 7001">
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="exampleModalLabel" style="color: red;">Confirmação de exclusão</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            Deseja realmente excluir o registro selecionado ?!?!
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-warning" data-dismiss="modal" style="color: white;">Cancelar</button>
                                            <asp:Button runat="server" ID="btnExcluirControleLives" CssClass="btn btn-danger" OnClick="btnExcluirControleLives_Click" Text="Excluir" data-dismiss="modal" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- Modal msg Sucesso -->
                            <div class="modal fade ModalSucesso" id="ModalSucesso" data-backdrop="static" data-keyboard="false" aria-labelledby="staticBackdropLabel" aria-hidden="true" tabindex="=1" role="dialog" style="z-index: 7001">
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="staticBackdropLabel">Mensagem</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <label style="color: green">Operação realizada com sucesso!!!</label>
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-row mb-3">
                            <div class="col-md-12">
                                <telerik:RadAjaxPanel ID="panelgridInscritosLive" runat="server">
                                    <telerik:RadGrid ID="gridInscritosLive" runat="server" AllowPaging="True" OnItemCommand="gridInscritosLive_ItemCommand" Skin="WebBlue">
                                        <ClientSettings>
                                            <Selecting AllowRowSelect="True"></Selecting>
                                            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
                                        </ClientSettings>
                                        <MasterTableView AutoGenerateColumns="False" DataKeyNames="IDInscrito">
                                            <Columns>
                                                <%--coluna0--%>
                                                <telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="column" HeaderText=""
                                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" CommandName="Select" ImageUrl="../Imagens/edit.png" HeaderStyle-Width="30px" ItemStyle-Width="30px">
                                                </telerik:GridButtonColumn>
                                                <%--coluna1--%>
                                                <telerik:GridBoundColumn DataField="IDInscrito" DataType="System.Int32" HeaderText="IDInscrito" UniqueName="IDInscrito" HeaderStyle-Width="50px" Display="false">
                                                </telerik:GridBoundColumn>
                                                <%--coluna2--%>
                                                <telerik:GridBoundColumn DataField="Nome" HeaderText="Nome" FilterControlAltText="Filter Nome column" SortExpression="Nome" UniqueName="Nome">
                                                </telerik:GridBoundColumn>
                                                <%--coluna3--%>
                                                <telerik:GridDateTimeColumn DataField="DtVencimento" FilterControlAltText="Filter DtVencimento column" SortExpression="DtVencimento" UniqueName="DtVencimento" HeaderText="Vencimento" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="100px">
                                                </telerik:GridDateTimeColumn>
                                                <%--coluna4--%>
                                                <telerik:GridNumericColumn DataField="Valor_inscricao" FilterControlAltText="Filter Valor_inscricao column" SortExpression="Valor_inscricao" UniqueName="Valor_Inscricao" HeaderText="Vlr. Inscrição" HeaderStyle-Width="120px" DataType="System.Decimal" NumericType="Currency">
                                                    <ItemStyle ForeColor="Blue" />
                                                </telerik:GridNumericColumn>
                                                <%--coluna5--%>
                                                <telerik:GridBoundColumn DataField="Pago" HeaderText="Pago" FilterControlAltText="Filter Pago column" SortExpression="Pago" UniqueName="Pago">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadAjaxPanel>
                            </div>
                        </div>
                    </telerik:RadAjaxPanel>
                </telerik:RadPageView>
            </telerik:RadMultiPage>          
            <div class="form-row">
                <label runat="server" id="lblerro" style="color: red; font-weight: bold;"></label>
            </div>
        </div>
    </div>
        
</asp:Content>
