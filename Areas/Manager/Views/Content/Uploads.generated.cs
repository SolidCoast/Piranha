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
    
    #line 1 "..\..\Areas\Manager\Views\Content\Uploads.cshtml"
    using Piranha.Models.Manager.ContentModels;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.5.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Areas/Manager/Views/Content/Uploads.cshtml")]
    public class Uploads : System.Web.Mvc.WebViewPage<UploadModel>
    {
        public Uploads()
        {
        }
        public override void Execute()
        {



            
            #line 3 "..\..\Areas\Manager\Views\Content\Uploads.cshtml"
  
    Layout = string.Empty;



            
            #line default
            #line hidden
WriteLiteral("<table class=\"list files\">\r\n    <thead>\r\n        <tr>\r\n            <th>Filename</" +
"th>\r\n            <th>Size</th>\r\n            <th>Uploaded</th>\r\n        </tr>\r\n  " +
"  </thead>\r\n    <tbody>\r\n");


            
            #line 16 "..\..\Areas\Manager\Views\Content\Uploads.cshtml"
         foreach (UploadModel.UploadedFile f in Model.Files)
        {

            
            #line default
            #line hidden
WriteLiteral("            <tr data-path=\"");


            
            #line 18 "..\..\Areas\Manager\Views\Content\Uploads.cshtml"
                      Write(f.Path);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n                <td>");


            
            #line 19 "..\..\Areas\Manager\Views\Content\Uploads.cshtml"
               Write(f.Name);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                <td>");


            
            #line 20 "..\..\Areas\Manager\Views\Content\Uploads.cshtml"
                Write(Math.Max((long)Math.Round((double)f.Size / 1024), 1));

            
            #line default
            #line hidden
WriteLiteral(" kb</td>\r\n                <td>");


            
            #line 21 "..\..\Areas\Manager\Views\Content\Uploads.cshtml"
               Write(f.Date.ToShortDateString());

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                <td class=\"buttons one\">\r\n                    <a class=\"ic" +
"on add\"></a></td>\r\n            </tr>\r\n");


            
            #line 25 "..\..\Areas\Manager\Views\Content\Uploads.cshtml"
        }

            
            #line default
            #line hidden
WriteLiteral("    </tbody>\r\n</table>");


        }
    }
}
#pragma warning restore 1591
