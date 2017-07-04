﻿using System;
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
    }
}