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

namespace Piranha.Areas.Manager.Views.Shared.EditorTemplates
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.5.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Manager/Views/Shared/EditorTemplates/Region.cshtml")]
    public class Region : System.Web.Mvc.WebViewPage<Models.Region>
    {
        public Region()
        {
        }
        public override void Execute()
        {


            
            #line 2 "..\..\Areas\Manager\Views\Shared\EditorTemplates\Region.cshtml"
  
    string folder = Model.Type.LastIndexOf('.') > -1 ? Model.Type.Substring(0, Model.Type.LastIndexOf('.')) : string.Empty;
    folder = folder != string.Empty ? folder + "/" : string.Empty;
    string view = Model.Type.Substring(folder.Length);



            
            #line default
            #line hidden
WriteLiteral("<div id=\"");


            
            #line 8 "..\..\Areas\Manager\Views\Shared\EditorTemplates\Region.cshtml"
    Write(Model.InternalId);

            
            #line default
            #line hidden
WriteLiteral("\" class=\"region-body\" style=\"display: none\">\r\n    ");


            
            #line 9 "..\..\Areas\Manager\Views\Shared\EditorTemplates\Region.cshtml"
Write(Html.HiddenFor(m => m.IsNew));

            
            #line default
            #line hidden
WriteLiteral("\r\n    ");


            
            #line 10 "..\..\Areas\Manager\Views\Shared\EditorTemplates\Region.cshtml"
Write(Html.HiddenFor(m => m.Id));

            
            #line default
            #line hidden
WriteLiteral("\r\n    ");


            
            #line 11 "..\..\Areas\Manager\Views\Shared\EditorTemplates\Region.cshtml"
Write(Html.HiddenFor(m => m.IsDraft));

            
            #line default
            #line hidden
WriteLiteral("\r\n    ");


            
            #line 12 "..\..\Areas\Manager\Views\Shared\EditorTemplates\Region.cshtml"
Write(Html.HiddenFor(m => m.Name));

            
            #line default
            #line hidden
WriteLiteral("\r\n    ");


            
            #line 13 "..\..\Areas\Manager\Views\Shared\EditorTemplates\Region.cshtml"
Write(Html.HiddenFor(m => m.InternalId));

            
            #line default
            #line hidden
WriteLiteral("\r\n    ");


            
            #line 14 "..\..\Areas\Manager\Views\Shared\EditorTemplates\Region.cshtml"
Write(Html.HiddenFor(m => m.Type));

            
            #line default
            #line hidden
WriteLiteral("\r\n    ");


            
            #line 15 "..\..\Areas\Manager\Views\Shared\EditorTemplates\Region.cshtml"
Write(Html.HiddenFor(m => m.PageId));

            
            #line default
            #line hidden
WriteLiteral("\r\n    ");


            
            #line 16 "..\..\Areas\Manager\Views\Shared\EditorTemplates\Region.cshtml"
Write(Html.HiddenFor(m => m.RegiontemplateId));

            
            #line default
            #line hidden
WriteLiteral("\r\n    ");


            
            #line 17 "..\..\Areas\Manager\Views\Shared\EditorTemplates\Region.cshtml"
Write(Html.HiddenFor(m => m.IsPageDraft));

            
            #line default
            #line hidden
WriteLiteral("\r\n    ");


            
            #line 18 "..\..\Areas\Manager\Views\Shared\EditorTemplates\Region.cshtml"
Write(Html.HiddenFor(m => m.Created));

            
            #line default
            #line hidden
WriteLiteral("\r\n    ");


            
            #line 19 "..\..\Areas\Manager\Views\Shared\EditorTemplates\Region.cshtml"
Write(Html.HiddenFor(m => m.Updated));

            
            #line default
            #line hidden
WriteLiteral("\r\n    ");


            
            #line 20 "..\..\Areas\Manager\Views\Shared\EditorTemplates\Region.cshtml"
Write(Html.HiddenFor(m => m.CreatedBy));

            
            #line default
            #line hidden
WriteLiteral("\r\n    ");


            
            #line 21 "..\..\Areas\Manager\Views\Shared\EditorTemplates\Region.cshtml"
Write(Html.HiddenFor(m => m.UpdatedBy));

            
            #line default
            #line hidden
WriteLiteral("\r\n    ");


            
            #line 22 "..\..\Areas\Manager\Views\Shared\EditorTemplates\Region.cshtml"
Write(Html.PartialFor(folder + view, m => m.Body));

            
            #line default
            #line hidden
WriteLiteral("\r\n</div>");


        }
    }
}
#pragma warning restore 1591
