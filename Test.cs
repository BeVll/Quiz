using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz2
{
    internal class Test
    {
        public string Name { get; set; }
        public List<Question> questions { get; set; }
        public  Category category { get; set; }
        public Test()
        {
            questions = new List<Question>();
        }
        public void AddQuestion()
        {
            try
            {
                Console.Clear();
                Question question = new Question();
                Console.WriteLine("Введите вопрос: ");
                question.Name = Console.ReadLine();

                string str = string.Empty;
                do
                {
                    Console.WriteLine("Введите неправильные ответы(остановить 99): ");
                    str = Console.ReadLine();
                    if (str != "99")
                        question.wrong_answers.Add(str);
                    else
                        break;
                } while (question.wrong_answers.Count < 6 || str != "9");

                do
                {
                    Console.WriteLine("Введите правильные ответы(остановить 99): ");
                    str = Console.ReadLine();
                    if (str != "99")
                        question.correct_answers.Add(str);
                    else
                        break;
                } while (question.correct_answers.Count < 6 || str != "99");

                Console.WriteLine("Введите количество баллов за вопрос: ");
                question.count_points = Convert.ToInt32(Console.ReadLine());

                questions.Add(question);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
            }
        }

        public void DeleteQuestion()
        {
            Console.Clear();
            for (int i = 0; i < questions.Count; i++)
                ShowQuestion(i);
            int id = -1;
            do
            {
                Console.WriteLine("Введите ид вопроса: ");
                id = Convert.ToInt32(Console.ReadLine());
                if (id > questions.Count - 1 && id < 0)
                    Console.WriteLine("Неправильное ид!");

            } while (id > questions.Count - 1 && id < 0);
            questions.RemoveAt(id);
        }
        public void ShowQuestion(int index)
        {
            Console.WriteLine($"ID: {index}");
            Console.WriteLine($"Вопрос: {questions[index].Name}");
            Console.WriteLine("Неправильные ответы: ");
            foreach (string x in questions[index].wrong_answers)
            {
                Console.WriteLine(x);
            }
            Console.WriteLine("Правильные ответы: ");
            foreach (string x in questions[index].correct_answers)
            {
                Console.WriteLine(x);
            }
            Console.WriteLine($"Баллов: {questions[index].count_points}");
        }
    }
}
