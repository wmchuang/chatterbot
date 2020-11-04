using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HandleChatterbotDate
{
    class Program
    {
        static void Main(string[] args)
        {
            //var str = "每日一练👇 👇  许多婴幼儿的看护人认为“小孩腹泻是小毛病，没什么事儿”该想法若用健康信念模式来解释是";
            //var st = Regex.Replace(str, @"\p{Cs}", "");
            //Console.WriteLine(st);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            MySqlConnection con = new MySqlConnection("server=127.0.0.1;database=test;uid=root;pwd=123456;charset='utf8mb4';SslMode=None");
            //查询数据
            var list = con.Query<TrainData>("SELECT Content,AnswerContent from traindata where AnswerContent is not null and QuestionId is not null");

            var filePath = AppDomain.CurrentDomain.BaseDirectory + "answer.yml";

            Console.WriteLine($"共读取到文本{list.Count()}条");
            Console.WriteLine("正在写入文件中,请耐心等待。。。");
            var count = 0;
            using (var stream = new StreamWriter(filePath, true))
            {
                var content = string.Empty;
                foreach (var item in list)
                {
                    var question = Re(item.Content);
                    var answer = Re(item.AnswerContent);
                    try
                    {
                        int a = Convert.ToInt32(question);
                        continue;
                    }
                    catch
                    { }

                    if (string.IsNullOrWhiteSpace(question) || string.IsNullOrWhiteSpace(answer)) continue;
                    content += $"\r\n    - - {question}\r\n      - {answer}";
                    count++;
                }

                stream.WriteLine(content);
            }
            Console.WriteLine($"写入成功{count}条！！！O(∩_∩)O");
            Console.ReadKey();
        }

        private static string Re(string str)
        {
            var q = Regex.Replace(str, @"\p{Cs}", "").Replace("\n", "").Replace("@", "").Replace("\t", "").TrimStart(' ').TrimEnd(' ');
            if (q == "1" || string.IsNullOrWhiteSpace(q) || q.Contains(":") || q == "~") return string.Empty;
            return q;
        }
    }
}
