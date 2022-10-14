using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz2
{
    internal class Result
    {
        public User user { get; set; }
        public Test test { get; set; }
        public int count_p { get; set; }
        public int count_correct_q { get; set; }
        
        public void ShowResultFull()
        {
            Console.WriteLine($"Пользователь: {user.Login}");
            Console.WriteLine($"Тест: {test.Name}");
            Console.WriteLine($"Сумарно {count_p} из {test.questions.Sum(s => s.count_points)} балла(ов)!");
            double procent;
            procent = Convert.ToDouble(count_p) / Convert.ToDouble(test.questions.Sum(s => s.count_points));
            procent = procent * 100.00;
            Console.WriteLine($"Процент баллов: {procent}%");
            int grade;
            grade = Convert.ToInt32(0.12 * procent);
            Console.WriteLine($"Оценка: {grade}");
            Console.WriteLine($"Правильных ответов: {count_correct_q}");
            Console.WriteLine($"Неправильных ответов: {test.questions.Count - count_correct_q}");
        }
        public void ShowResultShort()
        {
            Console.WriteLine($"{user.Login}: {count_p} балла(ов)");
        }
        public string GetResultShort()
        {
            string res = $"{user.Login}: {count_p} балла(ов)";
            return res;
        }
    }
}
