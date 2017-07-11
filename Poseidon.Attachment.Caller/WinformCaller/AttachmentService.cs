using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Attachment.Caller.WinformCaller
{
    using Poseidon.Base.Framework;
    using Poseidon.Attachment.Caller.Facade;
    using Poseidon.Attachment.Core.BL;
    using Poseidon.Attachment.Core.DL;
    using Poseidon.Attachment.Core.Utility;

    /// <summary>
    /// 附件业务访问服务类
    /// </summary>
    internal class AttachmentService : AbstractLocalService<Attachment>, IAttachmentService
    {
        #region Field
        /// <summary>
        /// 业务类对象
        /// </summary>
        private AttachmentBusiness bl = null;
        #endregion //Field

        #region Constructor
        /// <summary>
        /// 附件业务访问服务类
        /// </summary>
        public AttachmentService() : base(BusinessFactory<AttachmentBusiness>.Instance)
        {
            this.bl = this.baseBL as AttachmentBusiness;
        }
        #endregion //Constructor

        #region Method
        /// <summary>
        /// 异步上传单个附件
        /// </summary>
        /// <param name="data">上传附件信息</param>
        /// <returns></returns>
        public async Task<Attachment> UploadAsync(UploadInfo data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 同步上传单个附件
        /// </summary>
        /// <param name="data">上传附件信息</param>
        /// <returns></returns>
        public Attachment Upload(UploadInfo data)
        {
            throw new NotImplementedException();
        }
        #endregion //Method
    }
}
