using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using Wis.Website.DataManager;

namespace Wis.Website.Web
{
    public partial class HtmlEditorTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Wis.Toolkit.WebControls.DropdownMenus.DropdownMenuItem item;
            
            //item = new Wis.Toolkit.WebControls.DropdownMenus.DropdownMenuItem();
            //item.Text = "1";

            //DropdownMenu1.MenuItems.Add(item);

            //Wis.Toolkit.ClientScript.MessageBox mb = new Wis.Toolkit.ClientScript.MessageBox(this.Page, "呵呵", "出现错误");
            //mb.Call();
            //((Wis.Toolkit.SiteMapDataProvider)SiteMap.Provider).Stack(category.CategoryName, string.Format("ArticleList.aspx?CategoryGuid={0}", category.CategoryGuid));
            List<KeyValuePair<string, Uri>> nodes = new List<KeyValuePair<string, Uri>>();
            nodes.Add(new KeyValuePair<string, Uri>("Dynamic Content", new Uri(Request.Url, "Default.aspx?id=")));
            nodes.Add(new KeyValuePair<string, Uri>(Request["id"], Request.Url));
            ((Wis.Toolkit.SiteMapDataProvider)SiteMap.Provider).Stack(nodes);

            ReleaseManager releaseManager = new ReleaseManager();
            Response.Write(releaseManager.ReleasePager(3, 100, 19));
        }

        // 从视频中抓图
        //public void CatchImg(string vFileName)
        //{
        //    string str = base.Server.MapPath(base.Request.ApplicationPath) + @"\Files\" + vFileName.Replace("/", @"\") + ".flv";
        //    string str2 = base.Server.MapPath(base.Request.ApplicationPath) + @"\Files\" + vFileName.Replace("/", @"\") + ".jpg";
        //    string str3 = "240x180";
        //    ProcessStartInfo startInfo = new ProcessStartInfo();
        //    startInfo.FileName = base.Server.MapPath(base.Request.ApplicationPath) + @"\ffmpeg.exe";
        //    startInfo.Arguments = " -i " + str + " -t 0.001 -s " + str3 + " -ss 15 -y -f image2 " + str2;
        //    startInfo.UseShellExecute = true;
        //    startInfo.CreateNoWindow = false;
        //    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    Process.Start(startInfo);
        //}
        /*
thumbnail vedio

C:\TDDOWNLOAD>ffmpeg -i C:\TDDOWNLOAD\thewitcher12.wmv -t 0.02 -s 480x270 -ss 15
 -y -f image2 C:\TDDOWNLOAD\thewitcher12.jpg

转换文件
C:\TDDOWNLOAD>ffmpeg  -i C:\TDDOWNLOAD\thewitcher12.wmv -ab 56 -ar 22050 -b 500
-r 20 -s 480x270 C:\TDDOWNLOAD\thewitcher12.flv
        支持国内常见的几十种视频格式，不论是用DV，手机拍摄的，还是从网上下载的电影片段，MTV，都可以将视频媒体文件高效转换为flv格式，高速转换，显示清晰、流畅，用户可以快速有效的对视频文件进行发布和分享。
         * 视频专辑、个人视频空间集中展示、快速评论、收藏、订阅等功能
         * 参考：http://www.aobosoft.com/Item/63.aspx
         * 
         * 视频新闻：
         * 1、网页文件允许上传视频文件
         * 2、后台调用ffmpeg对上传的视频进行压缩，输出flv格式的文件
         * 3、使用flvtools处理flv文件，标记上时长、帧速、关键帧等元数据，这样的flash文件才可以拖放。
         * 4、使用 ffmpeg 产生flv文件的缩略，和大图像文件的缩略图是一个道理。
         * 5、使用适当的flv播放器在网页中播放服务器端生成的flv文件。
         * http://www.gotonx.com/bbs/simple/index.php?t6322.html
         * 
         * ffmpeg无法解析的文件格式(wmv9，rm，rmvb等)
         * 从http://mediacoder.sourceforge.net/download_zh.htm下载最新版本的mediacoder，使用mencoder.exe；drv43260.dll和pncrt.dll三个文件
         * 
         * 使用ffmpeg转换视频为flv文件：
         * ffmpeg -i "/opt/input/1.mpg" -y -ab 32 -ar 22050 -b 800000 -s 640*480 /opt/output/1.flv"
         * ffmpeg能解析的格式：（asx，asf，mpg，wmv，3gp，mp4，mov，avi，flv等）
         * 对ffmpeg无法解析的文件格式(wmv9，rm，rmvb等)，可以先用别的工具（mencoder）转换为avi(ffmpeg能解析的)格式
         * mencoder /input/a.rmvb -oac lavc -lavcopts acodec=mp3:abitrate=64 -ovc xvid -xvidencopts bitrate=600 -of avi -o /output/a.avi
         * ffmpeg -i "/opt/input/a.avi" -y -ab 32 -ar 22050 -b 800000 -s 640*480 /opt/output/a.flv"
         * 
         * 视频抓图:
         * ffmpeg -i "/opt/input/a.flv" -y -f image2 -t 1 -s 300*200 "/opt/output/1.jpg" //获取静态图
         * ffmpeg -i "test.avi" -y -f image2 -ss 8 -t 0.001 -s 350x240 'test.jpg' 
         * ffmpeg -i "/opt/input/a.mpg" -vframes 30 -y -f gif "/output/1.gif" //获取动态图;
         * 不提倡抓gif文件；因为抓出的gif文件大而播放不流畅。
         * 
         * 用mencoder在线转换视频格式并控制视频品质
         * http://blog.sina.com.cn/u/490343a7010006z6
         * 
下载地址：http://ffdshow.faireal.net/mirror/ffmpeg/ 环境winxp-sp2下，下载最新版本的 FFMpeg.exe直接用就行（须rar解压）。
          http://sourceforge.net/projects/ffmpeg
          教程一 http://soenkerohde.com/tutorials/ffmpeg
          教程二 http://klaus.geekserver.net/flash/streaming.html
播放器：FlashGuru FLV Player (http://www.flashguru.co.uk/free-tool-flash-video-player

         * 异步编程的方式获取Process.StandardOutput和Process.StandardError的值
         * ms-help://MS.MSDNQTR.2003FEB.2052/cpref/html/frlrfSystemDiagnosticsProcessClassStandardOutputTopic.htm
         * http://www.ffmpeg.com.cn/index.php/.NET_2.0%28C#.29.E4.B8.8B.E8.B0.83.E7.94.A8FFMPEG.E7.9A.84.E6.96.B9.E6.B3.95
         * 
         * 利用ffmpeg+mencoder视频转换的总结 http://www.yitian130.com/article.asp?id=69
         * flv视频转换和flash播放的解决方案笔记 http://blog.verycd.com/dash/showentry=35982
ffmpeg常见的命令:
-fromats 显示可用的格式
-f fmt 强迫采用格式fmt
-I filename 输入文件
-y 覆盖输出文件
-t duration 设置纪录时间 hh:mm:ss[.xxx]格式的记录时间也支持(截图需要)
-ss position 搜索到指定的时间 [-]hh:mm:ss[.xxx]的格式也支持
-title string 设置标题
-author string 设置作者
-copyright string 设置版权
-comment string 设置评论
-target type 设置目标文件类型(vcd,svcd,dvd),所有的格式选项(比特率,编解码以及缓冲区大小)自动设置,只需要输入如下的就可以了:ffmpeg -i myfile.avi -target vcd /tmp/vcd.mpg
-hq 激活高质量设置

-b bitrate 设置比特率,缺省200kb/s
-r fps 设置帧频,缺省25
-s size 设置帧大小,格式为WXH,缺省160X128.下面的简写也可以直接使用:Sqcif 128X96 qcif 176X144 cif 252X288 4cif 704X576
-aspect aspect 设置横纵比 4:3 16:9 或 1.3333 1.7777
-croptop/botton/left/right size 设置顶部切除带大小,像素单位
-padtop/botton/left/right size 设置顶部补齐的大小,像素单位
-padcolor color 设置补齐条颜色(hex,6个16进制的数,红:绿:蓝排列,比如 000000代表黑色)
-vn 不做视频记录
-bt tolerance 设置视频码率容忍度kbit/s
-maxrate bitrate设置最大视频码率容忍度
-minrate bitreate 设置最小视频码率容忍度
-bufsize size 设置码率控制缓冲区大小
-vcodec codec 强制使用codec编解码方式. 如果用copy表示原始编解码数据必须被拷贝.(很重要)

-ab bitrate 设置音频码率
-ar freq 设置音频采样率
-ac channels 设置通道,缺省为1
-an 不使能音频纪录
-acodec codec 使用codec编解码

-vd device 设置视频捕获设备,比如/dev/video0
-vc channel 设置视频捕获通道 DV1394专用
-tvstd standard 设置电视标准 NTSC PAL(SECAM)
-dv1394 设置DV1394捕获
-av device 设置音频设备 比如/dev/dsp

-map file:stream 设置输入流映射
-debug 打印特定调试信息
-benchmark 为基准测试加入时间
-hex 倾倒每一个输入包
-bitexact 仅使用位精确算法 用于编解码测试
-ps size 设置包大小，以bits为单位
-re 以本地帧频读数据，主要用于模拟捕获设备
-loop 循环输入流。只工作于图像流，用于ffserver测试

     ffmpeg进行操作的常用方法:

   1.转换成flv文件:ffmpeg -i infile.* -y (-ss second_offset -ar ar -ab ab -r vr -b vb -s vsize) outfile.flv
              其中second_offset是从开始的多好秒钟.可以支持**:**:**格式,至于ar,ab是音频的参数,可以指定ar=22050,24000,44100(PAL制式),48000(NTSC制式),后两种常见,ab=56(视音频协议的codec而定,如果要听高品质,则80以上).vr,vb,vsize是视频参数,可以指定vr=15,25(PAL),29(NTSC),vb=200,500,800,1500(视视频协议的codec而定,可以通过查看专业的codec说明文档获取,如果你手头有一份详细的各种codec的文档,请提供一份给我,不胜感激.)
              还有一些参数-acodec ac -vcodec vc(ac指定音频codec,ar和ab可以省去,vc指定视频codec,vr和vb可以省去,自动采用相应的codec参数)
              还有很多高级参数,如-qmin,-qcale等,请查看详细文档.
              还有-an和-vn参数,分别从多媒体文件中提取出纯粹视频和音频.
              另,如果你是用shell批量处理,请使用-y参数覆盖生成flv.

   2.截取图片:ffmpeg -i infile.* -y (-ss second_offset) -t 0.001 -s msize (-f image_fmt) outfile.jpg
            其中second_offset同上,msize同vsize,图片大小.image_fmt=image2强制使用jpg,image_fmt=gif,强制使用gif格式.
            还可以用-vframes fn指定截取某帧图片,fn=1,2,3,...         

ffmpeg屏幕录像
ffmpeg -vcodec mpeg4 -b 1000 -r 10 -g 300 -vd x11:0,0 -s 1024×768 ~/test.avi
其中，-vd x11:0,0 指录制所使用的偏移为 x=0 和 y=0，-s 1024×768 指录制视频的大小为 1024×768。录制的视频文件为 test.avi，将保存到用户主目录中。其他选项可查阅其说明文档。
如果你只想录制一个应用程序窗口或者桌面上的一个固定区域，那么可以指定偏移位置和区域大小。使用xwininfo -frame命令可以完成查找上述参数。
你也可以重新调整视频尺寸大小，如：./ffmpeg -vcodec mpeg4 -b 1000 -r 10 -g 300 -i ~/test.avi -s 800×600 ~/test-800-600.avi。
参考5(http://linuxtoy.org/archives/ffmpeg.html)

使用ffmpeg抓图
ffmpeg -i test2.asf -y -f image2 -ss 08.010 -t 0.001 -s 352×240 b.jpg
jpg: ffmpeg -i test.asf -y -f image2 -t 0.001 -s 352×240 -ss a.jpg //注意-ss就是要提取视频文件中指定时间的图像
jpg: ffmpeg -i asf.flv -y -f image2 -t 1 asf.jpg
gif: ffmpeg -i test.asf -vframes 30 -y -f gif a.gif
参考3 参考2(http://www.killflash.net/blog/article.asp?id=77)


         * 获取视频信息
         * http://www.codeproject.com/KB/aspnet/ffmpeg_csharp.aspx
         * 
         * *
         */

    }
}
