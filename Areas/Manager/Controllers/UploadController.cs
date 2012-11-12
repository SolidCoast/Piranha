// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UploadController.cs" company="">
//   
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Piranha.Areas.Manager.Controllers
{
    using System;
    using System.IO;
    using System.Web.Mvc;

    using Piranha.Models;

    /// <summary>
    /// Controller for handling uploaded application content in the manager area.
    /// </summary>
    public class UploadController : ManagerController
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the file content for the upload.
        /// </summary>
        /// <param name="id">
        /// The upload id
        /// </param>
        /// <returns>
        /// The file content
        /// </returns>
        public FileResult Get(string id)
        {
            Upload ul = Upload.GetSingle(new Guid(id));
            if (ul != null)
            {
                return new FileStreamResult(new FileStream(ul.PhysicalPath, FileMode.Open), ul.Type);
            }

            throw new FileNotFoundException("Could not find the upload with the given id");
        }

        #endregion
    }
}