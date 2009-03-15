//------------------------------------------------------------------------------
// <copyright file="MediaHandler.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;

namespace Wis.Toolkit
{
    /// <summary>
    /// MediaHandler 的摘要说明
    /// </summary>
    public class MediaHandler
    {
        // 获取 Media 的信息
        // mediainfo
        // http://mediainfo.sourceforge.net/zh-CN
        /*
        获得多媒体文件的信息
            内容信息：标题，作者，专辑名，音轨号，日期，总时间…… 
            视频：编码器，长宽比，帧频率，比特率…… 
            音频：编码器，采样率，声道数，语言，比特率…… 
            文本：语言和字幕 
            段落：段落数，列表

        MediaInfo支持哪些文件格式?
            视频：MKV, OGM, AVI, DivX, WMV, QuickTime, Real, MPEG-1, MPEG-2, MPEG-4, DVD (VOB)...
            (编码器：DivX, XviD, MSMPEG4, ASP, H.264, AVC...) 
            音频：OGG, MP3, WAV, RA, AC3, DTS, AAC, M4A, AU, AIFF... 
            字幕：SRT, SSA, ASS, SAMI... 
         */

        // Action<> killHanlder;
        // http://www.cnblogs.com/TianFang/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inFile"></param>
        /// <param name="outFile"></param>
        /// <param name="dataReceivedEventHandler"></param>
        public void CreatePreviewFrame(string ffmpegFile, int timeOffset, string inFile, string outFile, int thumbnailWidth, int thumbnailHeight, DataReceivedEventHandler dataReceivedEventHandler)
        {
            if (System.IO.File.Exists(inFile) == false) return;

            // -ss time_off        set the start time offset
            // -ss  参数后跟的时间有两种写法,hh:mm:ss 或 直接写秒数，要在视频的有效时间内
            if (timeOffset == 0) timeOffset = 1;

            Process p = new Process(); // creating process
            p.StartInfo.FileName = ffmpegFile;
            p.StartInfo.Arguments = "-i \"" + inFile + "\" -y -f image2 -ss " + timeOffset.ToString() + " -vframes 1 -s " + thumbnailWidth.ToString() + "x" + thumbnailHeight.ToString() + " -an \"" + outFile + "\"";

            // ffmpeg -i test.asf -y -f  image2  -ss 00:01:00 -vframes 1  test1.jpg
            // ffmpeg -i test.asf -y -f  image2  -ss 60 -vframes 1  test1.jpg

            // -y                   overwrite output files
            // -f fmt               force format, image2强制使用jpg
            // -s size              设置帧大小,格式为WXH,缺省160X128.下面的简写也可以直接使用:Sqcif 128X96 qcif 176X144 cif 252X288 4cif 704X576
            // -vframes             fn指定截取某帧图片,fn=1,2,3,...

            p.StartInfo.UseShellExecute = false; // 不使用操作系统外壳程序启动线程
            p.StartInfo.RedirectStandardError = true; // 把外部程序错误输出写到StandardError流中
            // FFMPEG的所有输出信息，都为错误输出流，mencoder就是用standardOutput
            p.StartInfo.CreateNoWindow = false; // 不创建进程窗口
            p.ErrorDataReceived += dataReceivedEventHandler;// new DataReceivedEventHandler(Process_ErrorDataReceived);
            p.Start(); // 启动线程
            p.BeginErrorReadLine(); // 开始异步读取 StandardError 流
            p.WaitForExit(); // 阻塞等待进程结束
            p.Close(); // 关闭进程
            p.Dispose(); // 释放资源
        }

        /// <summary>
        /// 视频总时长
        /// </summary>
        public double GetTotalSeconds(string ffmpegFile, string inFile)
        {
            sbTotalSeconds = null;
            sbTotalSeconds = new System.Text.StringBuilder();
            using (Process p = new Process()) // creating process
            {
                //Action killHanlder = p.Kill;
                p.StartInfo.FileName = ffmpegFile;
                p.StartInfo.Arguments = "-i \"" + inFile + "\"";

                p.StartInfo.UseShellExecute = false; // 不使用操作系统外壳程序启动线程
                p.StartInfo.RedirectStandardError = true; // 把外部程序错误输出写到StandardError流中
                p.StartInfo.RedirectStandardOutput = true;
                // FFMPEG的所有输出信息，都为错误输出流，mencoder就是用standardOutput
                p.StartInfo.CreateNoWindow = false; // 不创建进程窗口
                p.ErrorDataReceived += new DataReceivedEventHandler(Process_DataReceived);
                p.OutputDataReceived += new DataReceivedEventHandler(Process_DataReceived);

                p.Start(); // 启动线程
                p.BeginOutputReadLine();
                p.BeginErrorReadLine(); // 开始异步读取 StandardError 流
                p.WaitForExit(); // 阻塞等待进程结束
            }

            System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(sbTotalSeconds.ToString(), @"Duration:\s+(\S+?)[,\s]");
            if (m.Success == false) return 0;
            return TimeSpan.Parse(m.Groups[1].Value).TotalSeconds;
        }

        private System.Text.StringBuilder sbTotalSeconds;
        private void Process_DataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Data)) return;
            if (sbTotalSeconds == null) sbTotalSeconds = new System.Text.StringBuilder();
            sbTotalSeconds.AppendLine(e.Data);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="inFile"></param>
        /// <param name="outFile"></param>
        /// <param name="dataReceivedEventHandler">比如 new DataReceivedEventHandler(Process_ErrorDataReceived)</param>
        public void ConvertingVideo(string ffmpegFile, string inFile, string outFile, DataReceivedEventHandler dataReceivedEventHandler)
        {
            // File 'C:\WebSite\WebSite1\thewitcher12.mpg' already exists. Overwrite ? [y/N] Not overwriting - exiting
            //if (System.IO.File.Exists(mpg)) System.IO.File.Delete(mpg);
            // 
            // Converting video
            // Gui参考 http://www.codeproject.com/KB/audio-video/FFMPEG_Interface.aspx
            // ffmpeg能解析的格式：（asx，asf，mpg，wmv，3gp，mp4，mov，avi，flv等）
            // ffmpeg无法解析文件格式 wmv9，rm，rmvb，先使用mencoder转换为avi，然后用ffmpeg转换
            // AVI转FLV ffmpeg -i test.avi -ab 56 -ar 22050 -b 500 -r 15 -s 320×240 test.flv
            // 转较高质量的flv ffmpeg -i test.avi -ab 56 -ar 22050 -qscale 4 -r 15 -s 320×240 test.flv
            // 抓图JPG ffmpeg -i 2.wmv -y -f image2 -ss 8 -t 0.001 -s 350×240 test.jpg

            Process p = new Process(); // creating process
            p.StartInfo.FileName = ffmpegFile;
            // 从 http://ffdshow.faireal.net/mirror/ffmpeg/ 下载

            // high quality flv 为什么有问题？
            // "-i " & newPath & " -acodec mp3 -ab 64k -ac 2 -ar 44100 -f flv -deinterlace -nr 500 -croptop 4 -cropbottom 8 -cropleft 8 -cropright 8 -s 640x480 -aspect 4:3 -r 25 -b 650k -me_range 25 -i_qfactor 0.71 -g 500 " & outputPath & "\" & out & ""
            // p.StartInfo.Arguments = "-i \"" + video + "\" -y -acodec mp3 -ab 64k -ac 2 -ar 44100 -f flv -deinterlace -nr 500 -croptop 4 -cropbottom 8 -cropleft 8 -cropright 8 -s 640x480 -aspect 4:3 -r 25 -b 650k -me_range 25 -i_qfactor 0.71 -g 500 \"" + mpg + "\"";

            p.StartInfo.Arguments = "-i \"" + inFile + "\" -y -ab 56k -ar 22050 -b 300k -r 15 -s 480x360 \"" + outFile + "\"";
            p.StartInfo.UseShellExecute = false; // 不使用操作系统外壳程序启动线程
            p.StartInfo.RedirectStandardError = true; // 把外部程序错误输出写到StandardError流中
            // FFMPEG的所有输出信息，都为错误输出流，mencoder就是用standardOutput
            p.StartInfo.CreateNoWindow = false; // 不创建进程窗口
            p.ErrorDataReceived += dataReceivedEventHandler;
            p.Start(); // 启动线程
            p.BeginErrorReadLine(); // 开始异步读取 StandardError 流
            p.WaitForExit(); // 阻塞等待进程结束
            p.Close(); // 关闭进程
            p.Dispose(); // 释放资源

            /*
            mencoder
            下载：http://mediacoder.sourceforge.net/download_zh.htm，下载最新版本的mediacoder的安装后；找到其中的mencoder.exe；drv43260.dll和pncrt.dll三个文件。
        
            水印字幕
            http://xxbin.com/2008m11/flv-for-mencoder/
            http://www.codeproject.com/KB/aspnet/MediaHandler.aspx
        
            RMVB转AVI
            mencoder 1.rmvb -oac mp3lame -lameopts preset=64 -ovc xvid -xvidencopts bitrate=600 -of avi -o rmvb.avi 

            RM转AVI
            mencoder 1.rm -oac mp3lame -lameopts preset=64 -ovc xvid -xvidencopts bitrate=600 -of avi -o rm.avi

            MPEG转AVI
            mencoder mp4.mpeg -oac mp3lame -lameopts preset=64 -ovc xvid -xvidencopts bitrate=600 -of avi -o mp4.avi

            MOV转AVI
            mencoder qtime.mov -oac mp3lame -lameopts preset=64 -ovc xvid -xvidencopts bitrate=600 -of avi -o qtime.avi

            WMV转AVI 包括WMV7到WMV9，只实验到WMV9
            mencoder m7.wmv -oac mp3lame -lameopts preset=64 -ovc xvid -xvidencopts bitrate=600 -of avi -o m7.avi

            RV转AVI
            mencoder 1.rv -oac mp3lame -lameopts preset=64 -ovc xvid -xvidencopts bitrate=600 -of avi -o rv.avi
         
             * http://www.sifung.com/pages/1074.shtm
             * http://blog.verycd.com/dash/showentry=35982
             * http://wf.xplore.cn/read.php/90.htm
             * http://blog.csdn.net/hrybird/archive/2008/08/31/2854384.aspx 重点
             * http://www.tzwhx.com/newOperate/html/1/11/112/11375.html 参数细节
             * http://www.mplayerhq.hu/DOCS/HTML/zh_CN/ Mencoder中文参考手册地址
             */
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="dataReceivedEventHandler"></param>
        public void InjectMetadata(string flvtool2File, string flvFile, DataReceivedEventHandler dataReceivedEventHandler)
        {
            // 使用 flvtool2-1.0.6.zip
            // http://blog.inlet-media.de/flvtool2/
            // http://rubyforge.org/frs/?group_id=1096&release_id=9694
            // http://rubyforge.org/frs/download.php/17498/flvtool2-1.0.6.zip
            // flvtool2 -U targetfile.flv
            // 命令格式:
            // flvtool2.exe [-ACDPUVaciklnoprstvx]... [-key:value]... in-path|stdin [out-path|stdout]
            // 如果out-path未定义，则将覆盖原文件，如果指定in-path为目录，out-path应为同一目录，否则将被忽略。
            Process p = new Process(); // creating process
            p.StartInfo.FileName = flvtool2File;
            p.StartInfo.Arguments = " -U \"" + flvFile + "\"";
            p.StartInfo.UseShellExecute = false; // 不使用操作系统外壳程序启动线程
            p.StartInfo.RedirectStandardError = true; // 把外部程序错误输出写到StandardError流中
            // FFMPEG的所有输出信息，都为错误输出流，mencoder就是用standardOutput
            p.StartInfo.CreateNoWindow = false; // 不创建进程窗口
            p.ErrorDataReceived += dataReceivedEventHandler;
            p.Start(); // 启动线程
            p.BeginErrorReadLine(); // 开始异步读取 StandardError 流
            p.WaitForExit(); // 阻塞等待进程结束
            p.Close(); // 关闭进程
            p.Dispose(); // 释放资源
            /*
             Commands:
            -A Adds tags from -t tags-file
            -C Cuts file using -i inpoint and -o outpoint
            -D Debugs file (writes a lot to stdout)
            -H Helpscreen will be shown
            -P Prints out meta data to stdout
            -U Updates FLV with an onMetaTag event

            Switches:
            -a Collapse space between cutted regions
            -c Compatibility mode calculates some onMetaTag values different
            -key:value Key-value-pair for onMetaData tag (overwrites generated values)
            -i timestamp Inpoint for cut command in miliseconds
            -k Keyframe mode slides onCuePoint(navigation) tags added by the
            add command to nearest keyframe position
            -l Logs FLV stream reading to stream.log in current directory
            -n Number of tag to debug
            -o timestamp Outpoint for cut command in miliseconds
            -p Preserve mode only updates FLVs that have not been processed
            before
            -r Recursion for directory processing
            -s Simulation mode never writes FLV data to out-path
            -t path Tagfile (MetaTags written in XML)
            -v Verbose mode
            -x XML mode instead of YAML mode
         
             flvtool2.exe" -UPx "C:\in.flv" "C:\out.flv"
             更新（修复）C:\in.flv文件的Meta Data，同时按照XML格式输出Meta Data信息内容到屏幕
             * 
             */

            // 使用 flvmdi
            // http://www.buraks.com/flvmdi/
            /*
             命令提示符下输入>flvmdi inputFile [outputFile][/s] [/x] [/e] [/k] 
             inputFile 和 outputFile可以是单独文件或目录，如果未指定输出文件或路径，则对原始文件覆盖。

             Switch：

             [/s] 将运行结果以阿拉伯数字形式显示，结果显示含义：
                 结果信息   Completed.   结果代号 0  含义：成功
                 结果信息   An error occured. [filename]   结果代号 1  含义：Some error occured while processing, no details available.
                 结果信息   usage: flvmdi inputFilename [outputFilename] [/s] [/x] [/k] [/eExtraData]   结果代号 2  含义：Error with parameters supplied
                 结果信息  directory does not exist : [outputFolder]   结果代号 3  含义：The folder specified cannot be found.

             [/x] 将重新注入后的FLV MetaData 到处为FLV文件同目录同名的XML文件。

             [/s] 给FLV文件写入附加字符串DATA。如果字符串有空格，则应将命令用双引号引用
                  如"/eThis is data with spaces" 

             [/k] 给FLV文件添加keyframes，如文件原有keyframes将被覆盖。
             */
        }
    }
}
