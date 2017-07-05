using System;
using System.Collections.Generic;
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
        /// 上传单个附件
        /// </summary>
        /// <param name="data">上传附件信息</param>
        /// <returns></returns>
        Task<List<Attachment>> Upload(UploadInfo data);

        /// <summary>
        /// 上传多个附件
        /// </summary>
        /// <param name="data">上传附件信息</param>
        /// <returns></returns>
        Task<List<Attachment>> Upload(List<UploadInfo> data);
    }
}
