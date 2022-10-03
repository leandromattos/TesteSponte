<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Paginas/Site1.Master" CodeBehind="Cadastro_Inscritos.aspx.vb" Inherits="Sponte.Cadastro_Inscritos" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="form-row" style="margin:auto;">
        <div class="form-group col-md-12 mb-3" style="margin-top: 20px;">
            <div class="col" style="text-align: center">
                <h1>Cadastro de Inscritos</h1>
            </div>
        </div>
        <div class="form-group col-md-6 mb-3" style="margin:auto;">
            <telerik:RadAjaxPanel ID="panelInscritos" runat="server">
                <telerik:RadTabStrip ID="TabInscritos" runat="server" MultiPageID="MultiPageInscritos" Skin="WebBlue">
                    <Tabs>
                        <telerik:RadTab Text="Cadastro" runat="server">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Listagem" runat="server">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage runat="server" ID="MultiPageInscritos">
                    <telerik:RadPageView runat="server" ID="ViewCadastro" Style="background-color: #d8d8d8; padding: 20px; border: 1px solid #dee2e6;">
                        <telerik:RadAjaxPanel ID="panelCadastroInscritos" runat="server">
                            <asp:HiddenField runat="server" ID="Operacao" />
                            <asp:HiddenField runat="server" ID="ID" />
                            <%--Nome--%>
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label for="inputNome">Nome</label>
                                    <input type="text" class="form-control inputNome" id="inputNome" runat="server">
                                    <div class="invalid-feedback">
                                        Valor informado inválido!
                                    </div>
                                </div>
                            </div>
                            <%--Data--%>
                            <div class="form-row">
                                <div class="form-group col-md-3">
                                    <label for="inputDataNascimento">Data Inicial</label>
                                    <input type="date" runat="server" id="inputDataNascimento" class="form-control inputDataNascimento" />
                                    <div class="invalid-feedback" runat="server" id="msgDataNascimento">
                                        Valor informado inválido!
                                    </div>
                                </div>
                            </div>
                            <%--Email--%>
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label for="inputEmail">Email</label>
                                    <input type="text" class="form-control inputEmail" id="inputEmail" placeholder="fulano@gmail.com" runat="server">
                                    <div class="invalid-feedback">
                                        Valor informado inválido!
                                    </div>
                                </div>
                            </div>
                            <%--UsuárioInstagram--%>
                            <div class="form-row">
                                <div class="form-group col-md-6">
                                    <label for="inputUserInstagram">Usuário Instagram</label>
                                    <input type="text" class="form-control inputUserInstagram" id="inputUserInstagram" placeholder="@nome_usuario" runat="server">
                                    <div class="invalid-feedback">
                                        Valor informado inválido!
                                    </div>
                                </div>
                            </div>
                            <%--Botoes--%>
                            <div class="modal-footer">
                                <asp:Button runat="server" ID="btnNovoInscritos" CssClass="btn btn-primary" OnClick="btnNovoInscritos_Click" Text="Novo" />
                                <asp:Button runat="server" ID="btnSalvarInscritos" CssClass="btn btn-success" OnClick="btnSalvarInscritos_Click" Text="Salvar" />
                                <asp:Button runat="server" ID="btnCancelarInscritos" CssClass="btn btn-warning" OnClick="btnCancelarInscritos_Click" Text="Cancelar" Style="color: white;" />
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
                                                <asp:Button runat="server" ID="btnExcluirInscritos" CssClass="btn btn-danger" OnClick="btnExcluirInscritos_Click" Text="Excluir" data-dismiss="modal" />
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
                        </telerik:RadAjaxPanel>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="ViewListagem" Style="background-color: #d8d8d8; padding: 20px; border: 1px solid #dee2e6;">
                        <div class="form-row mb-4" style="padding-bottom:10px;border-bottom: 1px solid #e9ecef;">
                            <div class="col-md-4">
                                <label for="inputFiltroNome">Nome</label>
                                <input type="text" class="form-control inputFiltroNome" id="inputFiltroNome" placeholder="Procurar por nome..." runat="server">
                            </div>
                            <div class="col-md-4">
                                <label for="inputFiltroEmail">Email</label>
                                <input type="text" class="form-control inputFiltroEmail" id="inputFiltroEmail" placeholder="Procurar por email..." runat="server">
                            </div>
                            <div class="col-md-2">
                                <label for="inputFiltroUsuario">Instagram</label>
                                <input type="text" class="form-control inputFiltroUsuario" id="inputFiltroUsuario" placeholder="@instagram" runat="server">
                            </div>
                            <div class="col-md-2" style="margin-top:30px;"">
                                <asp:Button runat="server" ID="btnFiltrarInscritos" CssClass="btn btn-primary" Text="Filtrar" OnClick="btnFiltrarInscritos_Click" />
                            </div>
                        </div>

                        <div class="form-row mb-3">
                            <div class="col-md-12">
                                <telerik:RadAjaxPanel ID="panelgridInscritos" runat="server">
                                    <telerik:RadGrid ID="gridInscritos" runat="server" AllowPaging="True" OnItemCommand="gridInscritos_ItemCommand" Skin="WebBlue">
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
                                                <telerik:GridDateTimeColumn DataField="DtNascimento" FilterControlAltText="Filter DtNascimento column" SortExpression="DtNascimento" UniqueName="DtNascimento" HeaderText="Data" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="70px">
                                                </telerik:GridDateTimeColumn>
                                                <%--coluna4--%>
                                                <telerik:GridBoundColumn DataField="Email" HeaderText="Email" FilterControlAltText="Filter Email column" SortExpression="Email" UniqueName="Email">
                                                </telerik:GridBoundColumn>
                                                <%--coluna4--%>
                                                <telerik:GridBoundColumn DataField="UserInstagram" HeaderText="Instagram" FilterControlAltText="Filter UserInstagram column" SortExpression="UserInstagram" UniqueName="UserInstagram">
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </telerik:RadAjaxPanel>
                            </div>
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
                <div class="form-row">
                    <label runat="server" id="lblerro" style="color: red; font-weight: bold;"></label>
                </div>
            </telerik:RadAjaxPanel>
        </div>
    </div>

</asp:Content>
