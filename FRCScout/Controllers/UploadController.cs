using FRCScout.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace FRCScout.Controllers
{
    public class UploadController : ApiController
    {
        private ScoutContext db = new ScoutContext();

        // POST: api/Upload
        public async Task<IHttpActionResult> PostUpload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Request!");
                throw new HttpResponseException(response);
            }

            string uploadPath = HttpContext.Current.Server.MapPath("~/Uploads");
            MultipartFormDataStreamProvider streamProvider = new CustomMultipartFormDataStreamProvider(uploadPath);

            await Request.Content.ReadAsMultipartAsync(streamProvider);

            List<string> files = new List<string>();
            foreach (MultipartFileData file in streamProvider.FileData)
            {
                files.Add("Uploads/" + file.LocalFileName.Split('\\').Last());
            }

            return this.Created<List<string>>("api/Upload", files);
        }

        // DELETE: api/Upload
        [Route("api/Upload/{pictureId}")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeletePicture(int pictureId)
        {
            Picture picture = db.Pictures.Find(pictureId);
            string path = Path.Combine(HttpContext.Current.Server.MapPath("~/"), picture.FileName);
            if (File.Exists(path))
            {
                await Task.Run(() => File.Delete(path));
            }

            db.Pictures.Remove(picture);
            await db.SaveChangesAsync();

            return this.Ok();
        }
    }
}
