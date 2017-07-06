using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Poseidon.Attachment.UnitTest
{
    using Poseidon.Base.Framework;
    using Poseidon.Common;
    using Poseidon.Attachment.Caller.Facade;
    using Poseidon.Attachment.Core.DL;
    using Poseidon.Attachment.Core.Utility;

    /// <summary>
    /// 附件服务测试
    /// </summary>
    [TestClass]
    public class AttachmentServerTest
    {
        #region Constructor
        public AttachmentServerTest()
        {
            // 设置连接字符串
            Cache.Instance.Add("ConnectionString", "mongodb://localhost:27017");

            // 设置数据库类型
            Cache.Instance.Add("DALPrefix", "Mongo");

            Cache.Instance.Add("CallerType", "webapi");
        }
        #endregion //Constructor

        #region Test

        [TestMethod]
        public void TestFind()
        {
            var data = CallerFactory<IAttachmentService>.Instance.FindAll();

            Assert.IsTrue(data.Count() > 0);
        }

        [TestMethod]
        public void TestFindBydId()
        {
            var data = CallerFactory<IAttachmentService>.Instance.FindById("594cd5a2672e210cbc5eb874");

            Assert.IsNotNull(data);
            Console.WriteLine(data.Name);
        }

        [TestMethod]
        public void TestFindAsync()
        {
            var data = CallerFactory<IAttachmentService>.Instance.FindAllAsync();

            var attachment = data.Result;

            Assert.IsTrue(attachment.Count() > 0);

            foreach(var item in attachment)
            {
                Console.WriteLine(item.FileName);
            }
        }

        /// <summary>
        /// 测试上传
        /// </summary>
        [TestMethod]
        public void TestUpload1()
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\文档.doc";

            UploadInfo info = new UploadInfo();
            info.Name = "兔斯基";
            info.Remark = "8dfs";
            info.LocalPath = filePath;

            var result = CallerFactory<IAttachmentService>.Instance.Upload(info);

            var attachment = result.Result;

            Assert.AreEqual(info.Name, attachment.Name);
        }
        #endregion //Test
    }
}
