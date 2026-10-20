<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Personas.aspx.vb" Inherits="CasoPersona.Personas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-3">Gestión de Personas</h2>
        <div class="card mb-4">
            <div class="card-header bg-primary text-white">
                <asp:Label ID="lblTituloForm" runat="server" Text="Nueva Persona" />
            </div>
            <div class="card-body row g-3">
                <div class="col-md-4">
                    <label>Identificación</label>
                    <asp:TextBox ID="txtIdentificacion" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                    <label>Nombre</label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-4">
                    <label>Apellidos</label>
                    <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" />
                </div>
                <div class="col-md-6">
                    <label>Correo</label>
                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email" />
                </div>
                <div class="col-md-3">
                    <label>Edad</label>
                    <asp:TextBox ID="txtEdad" runat="server" CssClass="form-control" TextMode="Number" />
                </div>
                <div class="col-md-3 d-flex align-items-end gap-2">
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success w-100" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnLimpiar" runat="server" Text="Cancelar" CssClass="btn btn-secondary w-100" OnClick="btnLimpiar_Click" />
                </div>
            </div>
          
            <div class="card-body row g-3">
                <asp:GridView ID="gvPersonas" runat="server"
                    CssClass="table"
                    AllowPaging="True" AutoGenerateColumns="False"
                    DataKeyNames="Identificacion" 
                    
                    OnRowDeleting="gvPersonas_RowDeleting"
                    OnRowCommand="gvPersonas_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="IdPersona" HeaderText="ID" />
                        <asp:BoundField DataField="Identificacion" HeaderText="Identificación" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" />
                        <asp:BoundField DataField="Correo" HeaderText="Correo" />
                        <asp:BoundField DataField="Edad" HeaderText="Edad" />
                        <asp:TemplateField HeaderText="Acciones">
                            <ItemTemplate>
                                <asp:Button ID="btnSeleccionar" runat="server" Text="Editar" CommandName="Seleccionar" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-sm btn-warning me-1" />
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('¿Eliminar este registro?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>
     </div>
</asp:Content>
