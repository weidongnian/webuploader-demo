using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 分部页传参
    /// </summary>
    public class PartialModel
    {
        /// <summary>
        /// 传入id，标识元素和文件，防止重复
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 上传控件按钮的id,不传默认生成
        /// </summary>
        public string BtnId { get; set; }

        /// <summary>
        /// 是否加载js
        /// </summary>
        public bool IsLoadJs { get; set; }

        /// <summary>
        /// 多张图片上传
        /// </summary>
        public bool Mutiple { get; set; }

        /// <summary>
        /// js回调，第一个参数id,第二个参数为图片路径
        /// </summary>
        public string JsCallBack { get; set; }

        /// <summary>
        /// 上传成功是否支持马预览
        /// </summary>
        public bool IsPrveview { get; set; }

        /// <summary>
        ///是否只上传图片
        /// </summary>
        public bool? isImg { get; set; }
    }
}
