using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Attachment.Caller.Facade
{
    using Poseidon.Base.Framework;
    using Poseidon.Attachment.Core.DL;
    using Poseidon.Attachment.Core.Utility;

    /// <summary>
    /// 附件业务访问服务接口
    /// </summary>
    public interface IAttachmentService : IBaseService<Attachment>
    {
        /// <summary>
        /// 异步上传单个附件
        /// </summary>
        /// <param name="data">上传附件信息</param>
        /// <returns></returns>
        Task<Attachment> UploadAsync(UploadInfo data);

        /// <summary>
        /// 同步上传单个附件
        /// </summary>
        /// <param name="data">上传附件信息</param>
        /// <returns></returns>
        Attachment Upload(UploadInfo data);

        /// <summary>
        /// 同步下载附件
        /// </summary>
        /// <param name="id"></param>
        Stream Download(string id);
    }
}
