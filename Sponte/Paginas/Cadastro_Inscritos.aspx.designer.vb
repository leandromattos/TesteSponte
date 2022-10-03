'------------------------------------------------------------------------------
' <gerado automaticamente>
'     Esse código foi gerado por uma ferramenta.
'
'     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
'     o código for recriado
' </gerado automaticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class Cadastro_Inscritos

    '''<summary>
    '''Controle panelInscritos.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents panelInscritos As Global.Telerik.Web.UI.RadAjaxPanel

    '''<summary>
    '''Controle TabInscritos.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents TabInscritos As Global.Telerik.Web.UI.RadTabStrip

    '''<summary>
    '''Controle MultiPageInscritos.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents MultiPageInscritos As Global.Telerik.Web.UI.RadMultiPage

    '''<summary>
    '''Controle ViewCadastro.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents ViewCadastro As Global.Telerik.Web.UI.RadPageView

    '''<summary>
    '''Controle panelCadastroInscritos.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents panelCadastroInscritos As Global.Telerik.Web.UI.RadAjaxPanel

    '''<summary>
    '''Controle Operacao.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents Operacao As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Controle ID.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents ID As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Controle inputNome.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents inputNome As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Controle inputDataNascimento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents inputDataNascimento As Global.System.Web.UI.HtmlControls.HtmlInputGenericControl

    '''<summary>
    '''Controle msgDataNascimento.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents msgDataNascimento As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Controle inputEmail.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents inputEmail As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Controle inputUserInstagram.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents inputUserInstagram As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Controle btnNovoInscritos.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnNovoInscritos As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle btnSalvarInscritos.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnSalvarInscritos As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle btnCancelarInscritos.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnCancelarInscritos As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle btnExcluirModal.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnExcluirModal As Global.System.Web.UI.HtmlControls.HtmlButton

    '''<summary>
    '''Controle btnExcluirInscritos.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnExcluirInscritos As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle ViewListagem.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents ViewListagem As Global.Telerik.Web.UI.RadPageView

    '''<summary>
    '''Controle inputFiltroNome.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents inputFiltroNome As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Controle inputFiltroEmail.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents inputFiltroEmail As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Controle inputFiltroUsuario.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents inputFiltroUsuario As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Controle btnFiltrarInscritos.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents btnFiltrarInscritos As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Controle panelgridInscritos.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents panelgridInscritos As Global.Telerik.Web.UI.RadAjaxPanel

    '''<summary>
    '''Controle gridInscritos.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents gridInscritos As Global.Telerik.Web.UI.RadGrid

    '''<summary>
    '''Controle lblerro.
    '''</summary>
    '''<remarks>
    '''Campo gerado automaticamente.
    '''Para modificar, mova a declaração de campo do arquivo de designer a um arquivo code-behind.
    '''</remarks>
    Protected WithEvents lblerro As Global.System.Web.UI.HtmlControls.HtmlGenericControl
End Class
