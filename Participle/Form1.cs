using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using JiebaNet.Segmenter;

namespace Participle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 结巴分词部分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var original = txt_Original.Text;
            var result = "";


            var segment = new JiebaSegmenter();


            //全模式
            if (radioButton1.Checked == true)
            {
                var segments = segment.Cut(original, cutAll: true);
                result = "【全模式】：" + string.Join("/", segments);

                //e.g.我来到北京清华大学
                Console.WriteLine("【全模式】：{0}", string.Join("/ ", segments));
            }

            //精确模式
            // 默认为精确模式
            if (radioButton2.Checked == true)
            {
                var segments = segment.Cut(original);
                result = "【精确模式】：" + string.Join("/", segments);

                //e.g.我来到北京清华大学
                Console.WriteLine("【精确模式】：{0}", string.Join("/ ", segments));
            }

            //新词识别
            // 默认为精确模式，同时也使用HMM模型
            if (radioButton3.Checked == true)
            {
                var segments = segment.Cut(original);
                result = "【新词识别】：" + string.Join("/", segments);

                //e.g.他来到了网易杭研大厦  
                Console.WriteLine("【新词识别】：{0}", string.Join("/ ", segments));
            }

            //搜索引擎模式
            if (radioButton4.Checked == true)
            {
                //匹配这些中文标点符号 。 ？ ！ ， 、 ； ： “ ” ‘ ' （ ） 《 》 〈 〉 【 】 『 』 「 」 ﹃ ﹄ 〔 〕 … — ～ ﹏ ￥
                var reg = "/[\u3002|\uff1f|\uff01|\uff0c|\u3001|\uff1b|\uff1a|\u201c|\u201d|\u2018|\u2019|\uff08|\uff09|\u300a|\u300b|\u3008|\u3009|\u3010|\u3011|\u300e|\u300f|\u300c|\u300d|\ufe43|\ufe44|\u3014|\u3015|\u2026|\u2014|\uff5e|\ufe4f|\uffe5] /";



                var segments = segment.CutForSearch(original);
                result = "【搜索引擎模式】：" + string.Join("/", segments);



                //e.g.小明硕士毕业于中国科学院计算所，后在日本京都大学深造
                Console.WriteLine("【搜索引擎模式】:{0}", string.Join("/", segments));


            }

            //歧义消除
            if (radioButton5.Checked == true)
            {
                var segments = segment.Cut(original);
                result = "【歧义消除】：" + string.Join("/", segments);


                //结过婚的和尚未结过婚的
                Console.WriteLine("【歧义消除】：{0}", string.Join("/ ", segments));
            }




            txt_Result.Text = result;

        }
    }
}
