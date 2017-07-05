using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Poseidon.Attachment.Caller.WebApiCaller
{
    using Poseidon.Attachment.Caller.Facade;
    using Poseidon.Attachment.Core.DL;
    using Poseidon.Attachment.Core.Utility;
    using Poseidon.Base.Framework;
    using Poseidon.Common;

    /// <summary>
    /// 附件业务API访问服务类
    /// </summary>
    internal class AttachmentService : AbstractApiService<Attachment>, IAttachmentService
    {
        #region Constructor
        /// <summary>
        /// 附件业务访问服务类
        /// </summary>
        /// <param name="controller">控制器</param>
        public AttachmentService() : base("attachment")
        {
            this.host = "http://localhost:4341/api/";
        }
        #endregion //Constructor

        #region Function
        /// <summary>
        /// 设置文件相关内容
        /// </summary>
        /// <param name="uploadInfo">上传文件信息</param>
        /// <returns></returns>
        private ByteArrayContent SetFileByteArrayContent(UploadInfo uploadInfo)
        {
            var fileContent = new ByteArrayContent(File.ReadAllBytes(uploadInfo.LocalPath));
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = Path.GetFileName(uploadInfo.LocalPath)
            };

            fileContent.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(Path.GetFileName(uploadInfo.LocalPath)));
            fileContent.Headers.Add("name", uploadInfo.Name);
            fileContent.Headers.Add("remark", uploadInfo.Remark);

            string md5Hash = Hasher.GetFileMD5Hash(uploadInfo.LocalPath);
            fileContent.Headers.Add("md5hash", md5Hash);

            return fileContent;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 上传单个附件
        /// </summary>
        /// <param name="data">上传附件信息</param>
        /// <returns></returns>
        public async Task<List<Attachment>> Upload(UploadInfo data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var content = new MultipartFormDataContent())//表明是通过multipart/form-data的方式上传数据  
                {
                    var fileContent = SetFileByteArrayContent(data);
                    content.Add(fileContent);

                    string url = this.host + "upload/";

                    var response = await client.PostAsync(url, content);

                    response.EnsureSuccessStatusCode();
                    var entity = response.Content.ReadAsAsync<List<Attachment>>();
                                        
                    return await entity;
                }
            }
        }

        /// <summary>
        /// 上传多个附件
        /// </summary>
        /// <param name="data">上传附件信息</param>
        /// <returns></returns>
        public async Task<List<Attachment>> Upload(List<UploadInfo> data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();

                using (var content = new MultipartFormDataContent())//表明是通过multipart/form-data的方式上传数据  
                {
                    foreach (var item in data)
                    {
                        var fileContent = SetFileByteArrayContent(item);
                        content.Add(fileContent);
                    }

                    string url = this.host + "upload/";

                    var response = await client.PostAsync(url, content);

                    response.EnsureSuccessStatusCode();
                    var entity = response.Content.ReadAsAsync<List<Attachment>>();

                    return await entity;
                }
            }
        }
        #endregion //Method
    }
}
