<%@ Page Title="" Language="C#" MasterPageFile="~/RecipeCalculator.Master" AutoEventWireup="true" CodeBehind="RecipeCalculator.aspx.cs" Inherits="RecipeCalculator.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <nav class="navbar">
        <div class="form-inline my-2 my-lg-0">
            <asp:DropDownList ID="DropDownListCountry" required="true" CssClass="form-control border-0 shadow form-control-lg text-base" AutoPostBack = "true" placeholder="Priority" runat="server" OnSelectedIndexChanged="DropDownListCountry_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
    </nav>
    <div class="col-lg-8 pb-5">
        <div class="col-lg-8 px-lg-4">
            <h1 class="text-base text-primary text-uppercase mb-4">Add Ingredient Here</h1>
            <div class="form-group mb-4">
                <asp:DropDownList ID="DropDownListIngredient" required="true" CssClass="form-control border-0 shadow form-control-lg text-base" placeholder="Priority" runat="server">
                </asp:DropDownList>
            </div>
            <div class="form-group mb-4">
                <asp:TextBox ID="editBoxQuantity" required="true" type="number" step="0.01" CssClass="form-control border-0 shadow form-control-lg text-base" placeholder="Quantity" runat="server" />
            </div>
            <asp:Button ID="btnAddIngredient" Text="Add Ingredient" CssClass="btn btn-primary" Height="40px" Width="250px" runat="server" OnClick="btnAddIngredient_Click" />
        </div>
    </div>
    <div class="col-lg-8 pb-5">
        <asp:ListView ID="IngredientListView" runat="server">
            <LayoutTemplate>
                <table class="table table-hover mb-0" id="itemPlaceholderContainer">
                    <thead>
                        <tr>
                            <th>Ingredient</th>
                            <th>Quantity</th>
                            <th>Rate</th>
                            <th>Gross Amount</th>
                            <th>Discount</th>
                            <th>Tax</th>
                            <th>Total</th>
                        </tr>
                        <tr runat="server" id="itemPlaceholder">
                        </tr>
                    </thead>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <asp:Label ID="lblingredient" runat="server" Text='<%# Eval("ingredient") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lblquantity" runat="server" Text='<%# Eval("quantity") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lblrate" runat="server" Text='<%# Eval("rate") %>' />
                    </td>
                    <td>
                        <asp:Label ID="LabelgrossAmount" runat="server" Text='<%# Eval("grossAmount") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lbldiscount" runat="server" Text='<%# Eval("discount") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lbltax" runat="server" Text='<%# Eval("tax") %>' />
                    </td>
                    <td>
                        <asp:Label ID="lbltotal" runat="server" Text='<%# Eval("total") %>' />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
        <div class="alert alert-success" runat="server" id="totalTaxDiv">
        </div>
        <div class="alert alert-danger" runat="server" id="totalDiscountDiv">
        </div>
        <div class="alert alert-info" runat="server" id="totalDiv">
        </div>
    </div>
</asp:Content>
