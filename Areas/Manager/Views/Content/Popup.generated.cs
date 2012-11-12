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

namespace Piranha.Areas.Manager.Views.Content
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
    
    #line 1 "..\..\Areas\Manager\Views\Content\Popup.cshtml"
    using Piranha.Models;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.5.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Manager/Views/Content/Popup.cshtml")]
    public class Popup : System.Web.Mvc.WebViewPage<Models.Manager.ContentModels.PopupModel>
    {
        public Popup()
        {
        }
        public override void Execute()
        {



            
            #line 3 "..\..\Areas\Manager\Views\Content\Popup.cshtml"
  
    Layout = string.Empty;
    bool first = true;



            
            #line default
            #line hidden
WriteLiteral(@"<div class=""title"">
    <ul class=""box-tabs"">
        <li class=""selected""><a href=""#attach-existing"">Choose existing</a></li>
        <li class=""hidden""><a href=""#attach-new"">Upload new</a></li>
    </ul>
    <a class=""close-btn right"" data-id=""boxContent""></a>
    <div class=""clear""></div>
</div>
<div id=""attach-existing"" class=""inner gallery ");


            
            #line 16 "..\..\Areas\Manager\Views\Content\Popup.cshtml"
                                           Write(Model.Content.Count > 12 ? "gallery-compressed" : string.Empty);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n");


            
            #line 17 "..\..\Areas\Manager\Views\Content\Popup.cshtml"
     foreach (Content content in Model.Content)
    {

            
            #line default
            #line hidden
WriteLiteral("        <div class=\"gallery-item ");


            
            #line 19 "..\..\Areas\Manager\Views\Content\Popup.cshtml"
                             Write(Model.Content.Count > 12 ? "compressed" : string.Empty);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n");


            
            #line 20 "..\..\Areas\Manager\Views\Content\Popup.cshtml"
             if (first && content.ParentId != Guid.Empty)
            {

            
            #line default
            #line hidden
WriteLiteral("                <img class=\"folder\" data-id=\"");


            
            #line 22 "..\..\Areas\Manager\Views\Content\Popup.cshtml"
                                        Write(content.Id);

            
            #line default
            #line hidden
WriteLiteral("\" src=\"");


            
            #line 22 "..\..\Areas\Manager\Views\Content\Popup.cshtml"
                                                          Write(Url.Content("~/areas/manager/content/img/ico-folder-up-96.png"));

            
            #line default
            #line hidden
WriteLiteral("\" />\r\n");


            
            #line 23 "..\..\Areas\Manager\Views\Content\Popup.cshtml"
            }
            else
            {

            
            #line default
            #line hidden
WriteLiteral("                <img");


            
            #line 26 "..\..\Areas\Manager\Views\Content\Popup.cshtml"
                Write(content.IsFolder ? " class=folder" : (!content.IsImage ? " class=document" : string.Empty));

            
            #line default
            #line hidden
WriteLiteral(" data-id=\"");


            
            #line 26 "..\..\Areas\Manager\Views\Content\Popup.cshtml"
                                                                                                                      Write(content.Id);

            
            #line default
            #line hidden
WriteLiteral("\" src=\"");


            
            #line 26 "..\..\Areas\Manager\Views\Content\Popup.cshtml"
                                                                                                                                        Write(Url.Content("~/thumb/" + content.Id + "/96"));

            
            #line default
            #line hidden
WriteLiteral("\" title=\"");


            
            #line 26 "..\..\Areas\Manager\Views\Content\Popup.cshtml"
                                                                                                                                                                                              Write(content.AlternateText);

            
            #line default
            #line hidden
WriteLiteral("\" alt=\"");


            
            #line 26 "..\..\Areas\Manager\Views\Content\Popup.cshtml"
                                                                                                                                                                                                                           Write(content.AlternateText);

            
            #line default
            #line hidden
WriteLiteral("\" />\r\n");


            
            #line 27 "..\..\Areas\Manager\Views\Content\Popup.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("            <p><small>");


            
            #line 28 "..\..\Areas\Manager\Views\Content\Popup.cshtml"
                 Write(content.DisplayName);

            
            #line default
            #line hidden
WriteLiteral("</small></p>\r\n        </div>\r\n");


            
            #line 30 "..\..\Areas\Manager\Views\Content\Popup.cshtml"
        first = false;
    }

            
            #line default
            #line hidden
WriteLiteral("    <div class=\"clear\"></div>\r\n</div>");


        }
    }
}
#pragma warning restore 1591
