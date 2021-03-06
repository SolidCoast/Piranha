﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Piranha.Areas.Manager.Views.Settings
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 1 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
    using Piranha.Models;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
    using Piranha.Resources;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.5.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Manager/Views/Settings/Group.cshtml")]
    public class Group : System.Web.Mvc.WebViewPage<Models.Manager.SettingModels.GroupEditModel>
    {
        public Group()
        {
        }
        public override void Execute()
        {




DefineSection("Toolbar", () => {

WriteLiteral("\r\n    ");


            
            #line 5 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
Write(Html.Partial("Partial/Tabs"));

            
            #line default
            #line hidden
WriteLiteral("\r\n    <div class=\"toolbar\">\r\n        <div class=\"inner\">\r\n            <ul>\r\n     " +
"           <li><a class=\"save submit\">");


            
            #line 9 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
                                      Write(Global.ToolbarSave);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n                <li><a href=\"");


            
            #line 10 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
                        Write(Url.Action("deletegroup", new { id = Model.Group.Id }));

            
            #line default
            #line hidden
WriteLiteral("\" class=\"delete\">");


            
            #line 10 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
                                                                                                Write(Global.ToolbarDelete);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n                <li><a href=\"");


            
            #line 11 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
                        Write(Url.Action("grouplist"));

            
            #line default
            #line hidden
WriteLiteral("\" class=\"back\">");


            
            #line 11 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
                                                               Write(Global.ToolbarBack);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n                <li><a href=\"");


            
            #line 12 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
                        Write(Url.Action("group", new { id = Model.Group.Id }));

            
            #line default
            #line hidden
WriteLiteral("\" class=\"refresh\">");


            
            #line 12 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
                                                                                           Write(Global.ToolbarReload);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n            </ul>\r\n            <div class=\"clear\"></div>\r\n        </di" +
"v>\r\n    </div>\r\n");


});

WriteLiteral("\r\n");


            
            #line 18 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
   Html.BeginForm(); 

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n");


            
            #line 21 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
Write(Html.HiddenFor(m => m.Group.Id));

            
            #line default
            #line hidden
WriteLiteral("\r\n");


            
            #line 22 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
Write(Html.HiddenFor(m => m.Group.IsNew));

            
            #line default
            #line hidden
WriteLiteral("\r\n");


            
            #line 23 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
Write(Html.HiddenFor(m => m.Group.Created));

            
            #line default
            #line hidden
WriteLiteral("\r\n<div class=\"grid_9\">\r\n    <div class=\"box\">\r\n        <div class=\"title\"><h2>");


            
            #line 26 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
                          Write(Global.Information);

            
            #line default
            #line hidden
WriteLiteral("</h2></div>\r\n        <div class=\"inner\">\r\n            <ul class=\"form\">\r\n        " +
"        <li>\r\n                    ");


            
            #line 30 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
               Write(Html.LabelFor(m => m.Group.Name));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    <div class=\"input\">\r\n                        ");


            
            #line 32 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
                   Write(Html.TextBoxFor(m => m.Group.Name));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                    ");


            
            #line 33 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
               Write(Html.ValidationMessageFor(m => m.Group.Name));

            
            #line default
            #line hidden
WriteLiteral("\r\n                </li>\r\n                <li>\r\n                    ");


            
            #line 36 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
               Write(Html.LabelFor(m => m.Group.Description));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    <div class=\"input\">\r\n                        ");


            
            #line 38 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
                   Write(Html.TextAreaFor(m => m.Group.Description, new { @rows = 3, @placeholder = Global.Optional }));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                </li>\r\n                <li>\r\n                    ");


            
            #line 41 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
               Write(Html.LabelFor(m => m.Group.ParentId));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    <div class=\"input\">\r\n                        ");


            
            #line 43 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
                   Write(Html.DropDownListFor(m => m.Group.ParentId, Model.Groups));

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n                </li>\r\n            </ul>\r\n        </div>\r\n    </div>\r\n</d" +
"iv>\r\n<div class=\"grid_3\">\r\n    <div class=\"box\">\r\n        <div class=\"title\"><h2" +
">");


            
            #line 51 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
                          Write(Settings.GroupMembers);

            
            #line default
            #line hidden
WriteLiteral("</h2></div>\r\n        <div class=\"inner\">\r\n");


            
            #line 53 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
             if (!Model.Group.IsNew)
            {

            
            #line default
            #line hidden
WriteLiteral("                <ul class=\"list\">\r\n");


            
            #line 56 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
                     foreach (SysUser user in Model.Members)
                    {

            
            #line default
            #line hidden
WriteLiteral("                        <li><a href=\"");


            
            #line 58 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
                                Write(Url.Action("user", new { id = user.Id }));

            
            #line default
            #line hidden
WriteLiteral("\">");


            
            #line 58 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
                                                                            Write(!string.IsNullOrEmpty(user.Name) ? user.Name : user.Login);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n");


            
            #line 59 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
                    }

            
            #line default
            #line hidden
WriteLiteral("                </ul>\r\n");


            
            #line 61 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
            }
            else
            {

            
            #line default
            #line hidden
WriteLiteral("                <p><em>");


            
            #line 64 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
                  Write(Settings.GroupMembersNew);

            
            #line default
            #line hidden
WriteLiteral("</em></p>\r\n");


            
            #line 65 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </div>\r\n    </div>\r\n</div>\r\n");


            
            #line 69 "..\..\Areas\Manager\Views\Settings\Group.cshtml"
   Html.EndForm(); 
            
            #line default
            #line hidden

        }
    }
}
#pragma warning restore 1591
