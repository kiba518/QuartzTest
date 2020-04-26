using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jops 
{
    public class CronString
    {
        //每天1点运行
        public static string Start1 { get; } = "0 0 1 * * ?";
        //每天2点运行
        public static string Start2 { get; } = "0 0 2 * * ?";
        //每天3点运行
        public static string Start3 { get; } = "0 0 3 * * ?";
        //每天4点运行
        public static string Start4 { get; } = "0 0 4 * * ?";
        //每天5点运行
        public static string Start5 { get; } = "0 0 5 * * ?";
        //每天6点运行
        public static string Start6 { get; } = "0 0 6 * * ?";
        //每天7点运行
        public static string Start7 { get; } = "0 0 7 * * ?";
        //每天8点运行
        public static string Start8 { get; } = "0 0 8 * * ?";
        //每天9点运行
        public static string Start9 { get; } = "0 0 9 * * ?";
        //每天10点运行
        public static string Start10 { get; } = "0 0 10 * * ?";
        //每天11点运行
        public static string Start11 { get; } = "0 0 11 * * ?";
        //每天12点运行
        public static string Start12 { get; } = "0 0 12 * * ?"; 
        //每天13点运行
        public static string Start13 { get; } = "0 0 13 * * ?";
        //每天14点运行
        public static string Start14 { get; } = "0 0 14 * * ?";

        //每天上午8点到17点之间每隔2分钟触发一次
        public static string StartCustom1 { get; } = "0 0/2 8-17 * * ?";
        //每天上午8点到17点之间每隔2分钟触发一次
        public static string StartCustom2 { get; } = "0 0/1 8-19 * * ?";
    }
}
/* 
 序号	说明	 是否必填	 允许填写的值	允许的通配符
 1	 秒	 是	 0-59 	  , - * /
 2	 分	 是	 0-59	  , - * /
 3	小时	 是	 0-23	  , - * /
 4	 日	 是	 1-31	  , - * ? / L W
 5	 月	 是	 1-12 or JAN-DEC	  , - * /
 6	 周	 是	 1-7 or SUN-SAT	  , - * ? / L #
 7	 年	 否	 empty 或 1970-2099	 , - * /
 
0 0 12 * * ?	每天12点触发
0 15 10 ? * *	每天10点15分触发
0 15 10 * * ?	每天10点15分触发
0 15 10 * * ? *	每天10点15分触发
0 15 10 * * ? 2005	2005年每天10点15分触发
0 * 14 * * ?	每天下午的 2点到2点59分每分触发
0 0/5 14 * * ?	每天下午的 2点到2点59分(整点开始，每隔5分触发)
0 0/5 14,18 * * ?	每天下午的 2点到2点59分(整点开始，每隔5分触发)
每天下午的 18点到18点59分(整点开始，每隔5分触发)
0 0-5 14 * * ?	每天下午的 2点到2点05分每分触发
0 10,44 14 ? 3 WED	3月分每周三下午的 2点10分和2点44分触发
0 15 10 ? * MON-FRI	从周一到周五每天上午的10点15分触发
0 15 10 15 * ?	每月15号上午10点15分触发
0 15 10 L * ?	每月最后一天的10点15分触发
0 15 10 ? * 6L	每月最后一周的星期五的10点15分触发
0 15 10 ? * 6L 2002-2005	从2002年到2005年每月最后一周的星期五的10点15分触发
0 15 10 ? * 6#3	每月的第三周的星期五开始触发
0 0 12 1/5 * ?	每月的第一个中午开始每隔5天触发一次
0 11 11 11 11 ?	每年的11月11号 11点11分触发(光棍节)
     
     
     */
