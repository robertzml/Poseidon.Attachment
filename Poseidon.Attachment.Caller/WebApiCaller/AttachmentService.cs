using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Attachment.Caller.WebApiCaller
{
    using Poseidon.Base.Framework;
    using Poseidon.Attachment.Caller.Facade;
    using Poseidon.Attachment.Core.DL;

    /// <summary>
    /// 附件业务访问服务类
    /// </summary>
    internal class AttachmentService : AbstractApiService<Attachment>, IBaseService<Attachment>
    {
        #region Constructor
        /// <summary>
        /// 附件业务访问服务类
        /// </summary>
        /// <param name="controller">控制器</param>
        public AttachmentService(string controller) : base(controller)
        {
        }
        #endregion //Constructor
    }
}
