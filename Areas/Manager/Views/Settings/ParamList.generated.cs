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
    
    #line 1 "..\..\Areas\Manager\Views\Settings\ParamList.cshtml"
    using Piranha.Models;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Areas\Manager\Views\Settings\ParamList.cshtml"
    using Piranha.Resources;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.5.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Manager/Views/Settings/ParamList.cshtml")]
    public class ParamList : System.Web.Mvc.WebViewPage<Models.Manager.SettingModels.ParamListModel>
    {
        public ParamList()
        {
        }
        public override void Execute()
        {




            
            #line 4 "..\..\Areas\Manager\Views\Settings\ParamList.cshtml"
  
    ViewBag.Title = Settings.ListTitleParams;



            
            #line default
            #line hidden

DefineSection("Head", () => {

WriteLiteral(@"
    <script type=""text/javascript"">
        $(document).ready(function() {
            $(document).ready(function() {
                var options = {
                    listClass: 'list-js',
                    searchId: 'search',
                    valueNames: ['name', 'updated', 'created']
                };
                var list = new List('list', options);
            });
        });
    </script>
");


});

WriteLiteral("\r\n");


DefineSection("Toolbar", () => {

WriteLiteral("\r\n    ");


            
            #line 23 "..\..\Areas\Manager\Views\Settings\ParamList.cshtml"
Write(Html.Partial("Partial/Tabs"));

            
            #line default
            #line hidden
WriteLiteral("\r\n    <div class=\"toolbar\">\r\n        <div class=\"inner\">\r\n            <ul>\r\n     " +
"           <li id=\"add\"><a href=\"");


            
            #line 27 "..\..\Areas\Manager\Views\Settings\ParamList.cshtml"
                                 Write(Url.Action("param"));

            
            #line default
            #line hidden
WriteLiteral("\" class=\"add\">");


            
            #line 27 "..\..\Areas\Manager\Views\Settings\ParamList.cshtml"
                                                                   Write(Global.ToolbarAdd);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n                <li><a href=\"");


            
            #line 28 "..\..\Areas\Manager\Views\Settings\ParamList.cshtml"
                        Write(Url.Action("paramlist"));

            
            #line default
            #line hidden
WriteLiteral("\" class=\"refresh\">");


            
            #line 28 "..\..\Areas\Manager\Views\Settings\ParamList.cshtml"
                                                                  Write(Global.ToolbarReload);

            
            #line default
            #line hidden
WriteLiteral("</a></li>\r\n            </ul>\r\n            <button class=\"search\"></button>");


            
            #line 30 "..\..\Areas\Manager\Views\Settings\ParamList.cshtml"
                                       Write(Html.TextBox("search"));

            
            #line default
            #line hidden
WriteLiteral("\r\n            <div class=\"clear\"></div>\r\n        </div>\r\n    </div>\r\n");


});

WriteLiteral("\r\n<div class=\"grid_12\">\r\n    <table id=\"list\" class=\"list\">\r\n        <thead>\r\n   " +
"         <tr>\r\n                <th><span class=\"sort asc\" data-sort=\"name\">");


            
            #line 39 "..\..\Areas\Manager\Views\Settings\ParamList.cshtml"
                                                       Write(Global.Name);

            
            #line default
            #line hidden
WriteLiteral("</span></th>\r\n                <th class=\"date\"><span class=\"sort\" data-sort=\"upda" +
"ted\">");


            
            #line 40 "..\..\Areas\Manager\Views\Settings\ParamList.cshtml"
                                                                   Write(Global.Updated);

            
            #line default
            #line hidden
WriteLiteral("</span></th>\r\n                <th class=\"date\"><span class=\"sort\" data-sort=\"crea" +
"ted\">");


            
            #line 41 "..\..\Areas\Manager\Views\Settings\ParamList.cshtml"
                                                                   Write(Global.Created);

            
            #line default
            #line hidden
WriteLiteral("</span></th>\r\n                <th class=\"one\"></th>\r\n            </tr>\r\n        <" +
"/thead>\r\n        <tbody class=\"list-js\">\r\n");


            
            #line 46 "..\..\Areas\Manager\Views\Settings\ParamList.cshtml"
             foreach (SysParam param in Model.Params)
            {

            
            #line default
            #line hidden
WriteLiteral("                <tr>\r\n                    <td class=\"name\"><a href=\"");


            
            #line 49 "..\..\Areas\Manager\Views\Settings\ParamList.cshtml"
                                         Write(Url.Action("param", new { id = param.Id }));

            
            #line default
            #line hidden
WriteLiteral("\">");


            
            #line 49 "..\..\Areas\Manager\Views\Settings\ParamList.cshtml"
                                                                                      Write(param.Name);

            
            #line default
            #line hidden
WriteLiteral("</a></td>\r\n                    <td class=\"updated\">");


            
            #line 50 "..\..\Areas\Manager\Views\Settings\ParamList.cshtml"
                                   Write(param.Updated.ToString("yyyy-MM-dd"));

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                    <td class=\"created\">");


            
            #line 51 "..\..\Areas\Manager\Views\Settings\ParamList.cshtml"
                                   Write(param.Created.ToString("yyyy-MM-dd"));

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                    <td class=\"buttons\">\r\n");


            
            #line 53 "..\..\Areas\Manager\Views\Settings\ParamList.cshtml"
                         if (!param.IsLocked)
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <a class=\"icon delete\" href=\"");


            
            #line 55 "..\..\Areas\Manager\Views\Settings\ParamList.cshtml"
                                                    Write(Url.Action("deleteparam", new { id = param.Id }));

            
            #line default
            #line hidden
WriteLiteral("\"></a>\r\n");


            
            #line 56 "..\..\Areas\Manager\Views\Settings\ParamList.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("                    </td>\r\n                </tr>\r\n");


            
            #line 59 "..\..\Areas\Manager\Views\Settings\ParamList.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </tbody>\r\n        <tfoot>\r\n            <tr>\r\n                <td colspan=" +
"\"6\"></td>\r\n            </tr>\r\n        </tfoot>\r\n    </table>\r\n</div>");


        }
    }
}
#pragma warning restore 1591
