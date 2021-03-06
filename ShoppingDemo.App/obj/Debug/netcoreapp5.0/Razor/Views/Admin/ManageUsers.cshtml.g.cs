#pragma checksum "/Users/jasoncampah/Desktop/Dev/ShoppingAppDemo/ShoppingDemo.App/Views/Admin/ManageUsers.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6dc3dbf8153a5b2ab606d1d360d0f7ea16a1d3ec"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_ManageUsers), @"mvc.1.0.view", @"/Views/Admin/ManageUsers.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "/Users/jasoncampah/Desktop/Dev/ShoppingAppDemo/ShoppingDemo.App/Views/Admin/ManageUsers.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/jasoncampah/Desktop/Dev/ShoppingAppDemo/ShoppingDemo.App/Views/Admin/ManageUsers.cshtml"
using ShoppingDemo.App.Data.Entites;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6dc3dbf8153a5b2ab606d1d360d0f7ea16a1d3ec", @"/Views/Admin/ManageUsers.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c1b17b240a4f76c9c570e741333d28315bea92ab", @"/_ViewImports.cshtml")]
    public class Views_Admin_ManageUsers : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<IdentityRole>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary btn-sm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Admin", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "AddToRole", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-danger btn-sm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "RemoveFromRole", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
            WriteLiteral("\n");
            WriteLiteral("\n");
#nullable restore
#line 8 "/Users/jasoncampah/Desktop/Dev/ShoppingAppDemo/ShoppingDemo.App/Views/Admin/ManageUsers.cshtml"
  
    ViewData["Title"] = "Manage Roles";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<h1>Manage Roles</h1>\n\n");
#nullable restore
#line 14 "/Users/jasoncampah/Desktop/Dev/ShoppingAppDemo/ShoppingDemo.App/Views/Admin/ManageUsers.cshtml"
 if(Model.ToList().Count > 0)
{
    foreach (var role in Model)
    {
      if(role.Name.Equals("Admin"))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("<br></br>\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6dc3dbf8153a5b2ab606d1d360d0f7ea16a1d3ec5420", async() => {
                WriteLiteral(" Add User ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-role", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 23 "/Users/jasoncampah/Desktop/Dev/ShoppingAppDemo/ShoppingDemo.App/Views/Admin/ManageUsers.cshtml"
                                                           WriteLiteral(role.Name);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["role"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-role", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["role"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("<br></br>");
#nullable restore
#line 23 "/Users/jasoncampah/Desktop/Dev/ShoppingAppDemo/ShoppingDemo.App/Views/Admin/ManageUsers.cshtml"
                                                                                                         }

#line default
#line hidden
#nullable disable
            WriteLiteral(" <table class=\"table\">\n  <thead class=\"thead-dark\">\n    <tr>\n      <th scope=\"col\">");
#nullable restore
#line 27 "/Users/jasoncampah/Desktop/Dev/ShoppingAppDemo/ShoppingDemo.App/Views/Admin/ManageUsers.cshtml"
                 Write(role.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Role</th>\n    </tr>\n  </thead>\n  <tbody>\n\n");
#nullable restore
#line 32 "/Users/jasoncampah/Desktop/Dev/ShoppingAppDemo/ShoppingDemo.App/Views/Admin/ManageUsers.cshtml"
       foreach (var user in _userManager.GetUsersInRoleAsync(role.Name).Result)
      {

#line default
#line hidden
#nullable disable
            WriteLiteral("    <tr>\n\n          <td>\n              ");
#nullable restore
#line 36 "/Users/jasoncampah/Desktop/Dev/ShoppingAppDemo/ShoppingDemo.App/Views/Admin/ManageUsers.cshtml"
         Write(user.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral("      ");
#nullable restore
#line 36 "/Users/jasoncampah/Desktop/Dev/ShoppingAppDemo/ShoppingDemo.App/Views/Admin/ManageUsers.cshtml"
                              Write(user.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("       ");
#nullable restore
#line 36 "/Users/jasoncampah/Desktop/Dev/ShoppingAppDemo/ShoppingDemo.App/Views/Admin/ManageUsers.cshtml"
                                                         if((user.FirstName != "Sa" && role.Name.Equals("Admin"))){

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6dc3dbf8153a5b2ab606d1d360d0f7ea16a1d3ec9759", async() => {
                WriteLiteral(" Remove ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-role", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 40 "/Users/jasoncampah/Desktop/Dev/ShoppingAppDemo/ShoppingDemo.App/Views/Admin/ManageUsers.cshtml"
                                                           WriteLiteral(role.Name);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["role"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-role", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["role"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#nullable restore
#line 41 "/Users/jasoncampah/Desktop/Dev/ShoppingAppDemo/ShoppingDemo.App/Views/Admin/ManageUsers.cshtml"
                                                         WriteLiteral(user.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
#nullable restore
#line 41 "/Users/jasoncampah/Desktop/Dev/ShoppingAppDemo/ShoppingDemo.App/Views/Admin/ManageUsers.cshtml"
                                                                                          }

#line default
#line hidden
#nullable disable
            WriteLiteral("          </td>    </tr>     \n");
#nullable restore
#line 43 "/Users/jasoncampah/Desktop/Dev/ShoppingAppDemo/ShoppingDemo.App/Views/Admin/ManageUsers.cshtml"

      }

#line default
#line hidden
#nullable disable
            WriteLiteral("  </tbody>\n</table>\n");
#nullable restore
#line 47 "/Users/jasoncampah/Desktop/Dev/ShoppingAppDemo/ShoppingDemo.App/Views/Admin/ManageUsers.cshtml"
    }
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("  <p>No Roles Found.</p>\n");
#nullable restore
#line 52 "/Users/jasoncampah/Desktop/Dev/ShoppingAppDemo/ShoppingDemo.App/Views/Admin/ManageUsers.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<ApplicationUser> _userManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<IdentityRole>> Html { get; private set; }
    }
}
#pragma warning restore 1591
