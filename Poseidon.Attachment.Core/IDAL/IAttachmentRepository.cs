using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Attachment.Core.IDAL
{
    using Poseidon.Base.Framework;
    using Poseidon.Attachment.Core.DL;

    /// <summary>
    /// 附件对象访问接口
    /// </summary>
    internal interface IAttachmentRepository : IBaseDAL<Attachment>
    {
    }
}
