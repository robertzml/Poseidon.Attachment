﻿using System;
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
    using Poseidon.Base.System;
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
        }
        #endregion //Constructor

        #region Function
        /// <summary>
        /// 设置文件相关内容
        /// </summary>
        /// <param name="uploadInfo">上传文件信息</param>
        /// <returns></returns>
        private ByteArrayContent SetFileContent(UploadInfo uploadInfo)
        {
            var fileContent = new ByteArrayContent(File.ReadAllBytes(uploadInfo.LocalPath));
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = Path.GetFileNameWithoutExtension(uploadInfo.LocalPath) + Path.GetExtension(uploadInfo.LocalPath).ToLower()
            };

            fileContent.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping(Path.GetFileName(uploadInfo.LocalPath)));
            fileContent.Headers.ContentType.CharSet = "utf-8";

            string md5Hash = Hasher.GetFileMD5Hash(uploadInfo.LocalPath);
            fileContent.Headers.Add("md5hash", md5Hash);

            return fileContent;
        }

        /// <summary>
        /// 设置文件附加信息
        /// </summary>
        /// <param name="uploadInfo">上传文件信息</param>
        /// <returns></returns>
        private List<ByteArrayContent> SetFormContent(UploadInfo uploadInfo)
        {
            List<ByteArrayContent> list = new List<ByteArrayContent>();

            var nameContent = new ByteArrayContent(Encoding.UTF8.GetBytes(uploadInfo.Name));
            nameContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                Name = "name"
            };
            list.Add(nameContent);

            var remarkContent = new ByteArrayContent(Encoding.UTF8.GetBytes(uploadInfo.Remark));
            remarkContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                Name = "remark"
            };
            list.Add(remarkContent);

            return list;
        }
        #endregion //Function

        #region Method
        /// <summary>
        /// 异步上传单个附件
        /// </summary>
        /// <param name="data">上传附件信息</param>
        /// <returns></returns>
        public async Task<Attachment> UploadAsync(UploadInfo data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var content = new MultipartFormDataContent())//表明是通过multipart/form-data的方式上传数据  
                {
                    content.Headers.ContentType.CharSet = "utf-8";
                    var fileContent = SetFileContent(data);
                    content.Add(fileContent);

                    var formContent = SetFormContent(data);
                    foreach (var byteContent in formContent)
                    {
                        content.Add(byteContent);
                    }

                    string url = this.host + "upload/";

                    var response = await client.PostAsync(url, content);

                    response.EnsureSuccessStatusCode();
                    var entity = response.Content.ReadAsAsync<Attachment>();

                    return await entity;
                }
            }
        }

        /// <summary>
        /// 同步上传单个附件
        /// </summary>
        /// <param name="data">上传附件信息</param>
        /// <returns></returns>
        public Attachment Upload(UploadInfo data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var content = new MultipartFormDataContent())//表明是通过multipart/form-data的方式上传数据  
                {
                    content.Headers.ContentType.CharSet = "utf-8";
                    var fileContent = SetFileContent(data);
                    content.Add(fileContent);

                    var formContent = SetFormContent(data);
                    foreach (var byteContent in formContent)
                    {
                        content.Add(byteContent);
                    }

                    string url = this.host + "upload/";

                    var response = client.PostAsync(url, content).Result;

                    response.EnsureSuccessStatusCode();
                    var entity = response.Content.ReadAsAsync<Attachment>().Result;

                    return entity;
                }
            }
        }

        /// <summary>
        /// 同步下载附件
        /// </summary>
        /// <param name="id"></param>
        public Stream Download(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();

                string url = this.host + "upload/" + id;

                var response = client.GetAsync(url).Result;

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var stream = response.Content.ReadAsStreamAsync().Result;
                    return stream;
                }
                else
                {
                    throw new PoseidonException(ErrorCode.HTTPError, response.StatusCode);
                }
            }
        }

        /// <summary>
        /// 按文件夹获取附件
        /// </summary>
        /// <param name="folder">文件夹</param>
        /// <returns></returns>
        public IEnumerable<Attachment> FindByFolder(string folder)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取文件夹列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetFolders()
        {
            throw new NotImplementedException();
        }
        #endregion //Method
    }
}
