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
    
    #line 1 "..\..\Areas\Manager\Views\Settings\ExternalList.cshtml"
    using Piranha.Resources;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.5.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Manager/Views/Settings/ExternalList.cshtml")]
    public class ExternalList : System.Web.Mvc.WebViewPage<dynamic>
    {
        public ExternalList()
        {
        }
        public override void Execute()
        {


            
            #line 2 "..\..\Areas\Manager\Views\Settings\ExternalList.cshtml"
  
    ViewBag.Title = Settings.ListTitleExternal;



            
            #line default
            #line hidden

DefineSection("Toolbar", () => {

WriteLiteral("\r\n    ");


            
            #line 7 "..\..\Areas\Manager\Views\Settings\ExternalList.cshtml"
Write(Html.Partial("Partial/Tabs"));

            
            #line default
            #line hidden
WriteLiteral("\r\n    <div class=\"toolbar\">\r\n        <div class=\"inner\">\r\n            <ul>\r\n     " +
"           <li id=\"add\"><a href=\"");


            
            #line 11 "..\..\Areas\Manager\Views\Settings\ExternalList.cshtml"
                                 Write(Url.Action("user"));

            
            #line default
            #line hidden
WriteLiteral("\" class=\"add\">");


            
            #line 11 "..\..\Areas\Manager\Views\Settings\ExternalList.cshtml"
                                                                  Write(Global.ToolbarAdd);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n                <li><a href=\"");


            
            #line 12 "..\..\Areas\Manager\Views\Settings\ExternalList.cshtml"
                        Write(Url.Action("paramlist"));

            
            #line default
            #line hidden
WriteLiteral("\" class=\"refresh\">");


            
            #line 12 "..\..\Areas\Manager\Views\Settings\ExternalList.cshtml"
                                                                  Write(Global.ToolbarReload);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n            </ul>\r\n            <button class=\"search\"></button>");


            
            #line 14 "..\..\Areas\Manager\Views\Settings\ExternalList.cshtml"
                                       Write(Html.TextBox("search"));

            
            #line default
            #line hidden
WriteLiteral("\r\n            <div class=\"clear\"></div>\r\n        </div>\r\n    </div>\r\n");


});

WriteLiteral("\r\n<div class=\"grid_12\">\r\n    <table id=\"list\" class=\"list\">\r\n        <thead>\r\n   " +
"         <tr>\r\n                <th><span class=\"sort asc\" data-sort=\"name\">");


            
            #line 23 "..\..\Areas\Manager\Views\Settings\ExternalList.cshtml"
                                                       Write(Global.Name);

            
            #line default
            #line hidden
WriteLiteral("</span></th>\r\n                <th><span class=\"sort\" data-sort=\"account\">");


            
            #line 24 "..\..\Areas\Manager\Views\Settings\ExternalList.cshtml"
                                                      Write(Global.Account);

            
            #line default
            #line hidden
WriteLiteral("</span></th>\r\n                <th><span>Status</span></th>\r\n                <th c" +
"lass=\"date\"><span class=\"sort\" data-sort=\"updated\">");


            
            #line 26 "..\..\Areas\Manager\Views\Settings\ExternalList.cshtml"
                                                                   Write(Global.Updated);

            
            #line default
            #line hidden
WriteLiteral("</span></th>\r\n                <th class=\"date\"><span class=\"sort\" data-sort=\"crea" +
"ted\">");


            
            #line 27 "..\..\Areas\Manager\Views\Settings\ExternalList.cshtml"
                                                                   Write(Global.Created);

            
            #line default
            #line hidden
WriteLiteral(@"</span></th>
                <th class=""one""></th>
            </tr>
        </thead>
        <tbody class=""list-js"">
        </tbody>
        <tfoot>
            <tr>
                <td colspan=""6""></td>
            </tr>
        </tfoot>
    </table>
</div>");


        }
    }
}
#pragma warning restore 1591
