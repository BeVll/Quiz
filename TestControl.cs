using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Quiz2
{
    internal class TestControl
    {
        public string Path { get; set; }
        public List<User> Users = new List<User>();
        public List<Category> categories = new List<Category>();
        public List<Test> tests = new List<Test>();
        public List<Result> results = new List<Result>();
        public User Current_user { get; set; }
        public void SaveAll()
        {
            SaveCategories();
            SaveResults();
            SaveTests();
            SaveUsers();
        }
        public void LoadUsers()
        {
            if (File.Exists(Path + "\\users.json") == false)
                File.Create(Path + "\\users.json");
            else
            {
                string json = File.ReadAllText(Path + "\\users.json");
                if(json.Length > 0)
                    Users = JsonConvert.DeserializeObject<List<User>>(json);
            }
        }
        public void SaveUsers()
        {
            string json = JsonConvert.SerializeObject(Users);
            if (File.Exists(Path + "\\users.json") == false)
                File.CreateText(Path + "\\users.json");

            File.WriteAllText(Path + "\\users.json", json);
        }
        public void LoadTests()
        {
            if (File.Exists(Path + "\\tests.json") == false)
                File.Create(Path + "\\tests.json");
            else
            {

                string json = File.ReadAllText(Path + "\\tests.json");
                if (json.Length > 0)
                    tests = JsonConvert.DeserializeObject<List<Test>>(json);
            }
        }
        public void SaveTests()
        {
            if (File.Exists(Path + "\\tests.json") == false)
                File.CreateText(Path + "\\tests.json");
      
                string json = JsonConvert.SerializeObject(tests);
            File.WriteAllText(Path + "\\tests.json", json);
        }
        public void LoadResults()
        {
            if (File.Exists(Path + "\\results.json") == false)
                File.Create(Path + "\\results.json");
            else
            {
                string json = File.ReadAllText(Path + "\\results.json");
                if (json.Length > 0)
                    results = JsonConvert.DeserializeObject<List<Result>>(json);
            }
        }
        public void SaveResults()
        {
            if (File.Exists(Path + "\\results.json") == false)
                File.CreateText(Path + "\\results.json");
                string json = JsonConvert.SerializeObject(results);
            File.WriteAllText(Path + "\\results.json", json);
        }
        public void LoadCategories()
        {
            if (File.Exists(Path + "\\categories.json") == false)
                File.Create(Path + "\\categories.json");
            else
            {
                string json = File.ReadAllText(Path + "\\categories.json");
                if (json.Length > 0)
                    categories = JsonConvert.DeserializeObject<List<Category>>(json);
            }
        }
        public void SaveCategories()
        {
            if (File.Exists(Path + "\\categories.json") == false)
                File.CreateText(Path + "\\categories.json");
            string json = JsonConvert.SerializeObject(categories);
            File.WriteAllText(Path + "\\categories.json", json);
        }
        public bool CheckLogin(string log)
        {
            if (Users.Count > 0)
            {
                if (Users.Exists(s => s.Login == log) == false)
                    return false;
                else
                {
                    return true;
                    Console.WriteLine("Логин уже занят!");
                }
            }
            else
                return false;
        }
        public bool CheckCategory(string name)
        {
            if (categories.Count > 0)
            {
                if (categories.Exists(s => s.Name == name) == false)
                    return false;
                else
                {
                    return true;
                    Console.WriteLine("Название уже занято!");
                }
            }
            else
                return false;
        }
        public bool CheckTest(string log)
        {
            if (tests.Count > 0)
            {
                if (tests.Exists(s => s.Name == log) == false)
                    return false;
                else
                {
                    return true;
                    Console.WriteLine("Название уже занято!");
                }
            }
            else
                return false;
        }
        public void CreateCategory()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------------Создание категории--------------------------------------------------");
            Category ct = new Category();
            string temp_name = string.Empty;
            do
            {
                Console.WriteLine("Введите название: ");
                temp_name = Console.ReadLine();
            } while (CheckCategory(temp_name) != false);
            ct.Name = temp_name;
            categories.Add(ct);
        }

        public void CreateTest()
        {
            try
            {
                Console.Clear();
                if (categories.Count > 0)
                {
                    Console.WriteLine("--------------------------------------------------Создание теста--------------------------------------------------");
                    Test test = new Test();
                    string temp_name = string.Empty;
                    do
                    {
                        Console.WriteLine("Введите название: ");
                        temp_name = Console.ReadLine();
                    } while (CheckLogin(temp_name) != false);
                    test.Name = temp_name;
                    for (int i = 0; i < categories.Count; i++)
                        ShowCategories(i);
                    int id = -1;
                    do
                    {
                        Console.WriteLine("Введите ид категории: ");
                        id = Convert.ToInt32(Console.ReadLine());
                        if (id > categories.Count - 1 && id < 0)
                            Console.WriteLine("Неправильное ид!");

                    } while (id > tests.Count - 1 && id < 0);
                    test.category = categories[id];
                    string str = string.Empty;
                    Console.WriteLine("Тест должен состоять из 20 вопросов!");
                    do
                    {


                        test.AddQuestion();

                    } while (test.questions.Count < 20);

                    var c = new ValidationContext(test);
                    var res = new List<ValidationResult>();
                    if (!Validator.TryValidateObject(test, c, res, true))
                    {
                        Console.WriteLine("Ошибка при создании теста:");
                        foreach (var er in res)
                            Console.WriteLine(er.ErrorMessage);
                    }
                    else
                    {
                        tests.Add(test);
                    }
                }
                else
                {
                    Console.WriteLine("Категории отсуствуют!");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
                Console.ReadKey();
            }
        }
        public void NewUser()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("-----------------------------------------------------Создание пользователя-----------------------------------------------------");
                User ur = new User();
                string temp_log = string.Empty;
                do
                {
                    Console.WriteLine("Введите логин: ");
                    temp_log = Console.ReadLine();
                } while (CheckLogin(temp_log) != false);
                ur.Login = temp_log;
                Console.WriteLine("Введите пароль: ");
                ur.Password = Console.ReadLine();
                Console.WriteLine("Введите дату рождения: ");
                ur.Birthday_date = Convert.ToDateTime(Console.ReadLine());
                ur.Type = "User";
                var c = new ValidationContext(ur);
                var res = new List<ValidationResult>();
                if (!Validator.TryValidateObject(ur, c, res, true))
                {
                    Console.WriteLine("Ошибка при создании пользователя:");
                    foreach (var er in res)
                        Console.WriteLine(er.ErrorMessage);
                }
                else
                {
                    Users.Add(ur);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: "+ex.Message);
                Console.ReadKey();
            }
        }

        public void DeleteTest()
        {
            Console.Clear();
            if (tests.Count > 0)
            {
                Console.WriteLine("-----------------------------------------------------Удаление теста-----------------------------------------------------");
                for (int i = 0; i < tests.Count; i++)
                    ShowTests(i);
                int id = -1;
                do
                {
                    Console.WriteLine("Введите ид теста: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    if (id > tests.Count - 1 && id < 0)
                        Console.WriteLine("Неправильное ид!");

                } while (id > tests.Count - 1 && id < 0);
                tests.RemoveAt(id);
            }
            else
            {
                Console.WriteLine("Тесты отсуствуют!");
                Console.ReadKey();
            }
        }
        public void DeleteCategory()
        {
            Console.Clear();
            if (categories.Count > 0)
            {
                Console.WriteLine("-----------------------------------------------------Удаление категории-----------------------------------------------------");
                for (int i = 0; i < tests.Count; i++)
                    ShowCategories(i);
                int id = -1;
                do
                {
                    Console.WriteLine("Введите ид категории: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    if (id > categories.Count - 1 && id < 0)
                        Console.WriteLine("Неправильное ид!");

                } while (id > categories.Count - 1 && id < 0);
                categories.RemoveAt(id);
            }
            else
            {
                Console.WriteLine("Категории отсуствуют!");
                Console.ReadKey();
            }
        }
        public void DeleteUser()
        {
            Console.Clear();
            Console.WriteLine("-----------------------------------------------------Удаление пользователя-----------------------------------------------------");
            for (int i = 0; i < Users.Count; i++)
                ShowUsers(i);
            int id = -1;
            do
            {
                Console.WriteLine("Введите ид пользователя: ");
                id = Convert.ToInt32(Console.ReadLine());
                if (id > Users.Count - 1 && id < 0)
                    Console.WriteLine("Неправильное ид!");

            } while (id > Users.Count - 1 && id < 0);
            Users.RemoveAt(id);
        }
        public void ShowTests(int index)
        {
            
            Console.WriteLine($"ID: {index}");
            Console.WriteLine($"Название: {tests[index].Name}");
            Console.WriteLine($"Категория {tests[index].category.Name}");
            Console.WriteLine($"Количество вопросов {tests[index].questions.Count}");    
        }
        public void ShowCategories(int index)
        {
            Console.WriteLine($"{index} - {categories[index].Name}");
        }
        public void ShowUsers(int index)
        {
            Console.WriteLine($"ID: {index}");
            Console.WriteLine($"Логин: {Users[index].Login}");
            Console.WriteLine($"Пароль: {Users[index].Password}");
            Console.WriteLine($"Дата рождения: {Users[index].Birthday_date.ToShortDateString()}");
        }
        public void ShowQuestion(Question qn, List<string> ans)
        {
            Console.WriteLine(qn.Name);

            for (int i = 0; i < ans.Count(); i++)
                Console.WriteLine($"{i + 1} - {ans[i]}");
        }

        public void ShowResult(int c_p, int max_p, int c_q, int c_c)
        {
            Console.WriteLine($"Вы набрали {c_p} из {max_p} балла(ов)!");
            double procent;
            procent = Convert.ToDouble(c_p) / Convert.ToDouble(max_p);
            procent = procent * 100.00;
            Console.WriteLine($"Процент баллов: {procent}%");
            int grade;
            grade = Convert.ToInt32(0.12 * procent);
            Console.WriteLine($"Оценка: {grade}");
            Console.WriteLine($"Правильных ответов: {c_c}");
            Console.WriteLine($"Неправильных ответов: {c_q - c_c}");
        }
        public string ShowResultString(int c_p, int max_p, int c_q, int c_c)
        {
            Console.WriteLine($"Вы набрали {c_p} из {max_p} балла(ов)!");
            double procent;
            procent = Convert.ToDouble(c_p) / Convert.ToDouble(max_p);
            procent = procent * 100.00;
            Console.WriteLine($"Процент баллов: {procent}%");
            int grade;
            grade = Convert.ToInt32(0.12 * procent);
            Console.WriteLine($"Оценка: {grade}");
            Console.WriteLine($"Правильных ответов: {c_c}");
            Console.WriteLine($"Неправильных ответов: {c_q - c_c}");
            string result = $"Вы набрали {c_p} из {max_p} балла(ов)! \nПроцент баллов: {procent}% \nОценка: {grade} \nПравильных ответов: {c_c} \nНеправильных ответов: {c_q - c_c}";
            return result;
        }
        public void UseTest()
        {
            Random rnd = new Random();
            Console.Clear();
            int count_points = 0;
            int count_correct = 0;
            Console.WriteLine("-----------------------------------------------------Прохождение теста-----------------------------------------------------");
            Console.WriteLine("1 - Конкретный тест");
            Console.WriteLine("2 - Смешанный тест");
            Console.WriteLine("Выберите тип теста ->");
            int choose = Convert.ToInt32(Console.ReadLine());
            if (choose == 1)
            {
                for (int i = 0; i < tests.Count; i++)
                    ShowTests(i);
                int id = -1;
                do
                {
                    Console.WriteLine("Введите ид теста: ");
                    id = Convert.ToInt32(Console.ReadLine());
                    if (id > tests.Count - 1 && id < 0)
                        Console.WriteLine("Неправильное ид!");

                } while (id > tests.Count - 1 && id < 0);
                Test ts = tests[id];
                ts.questions = RandomSort(ts.questions);
                List<string> answers = new List<string>();
                int max_points = ts.questions.Sum(s => s.count_points);

                for (int i = 0; i < ts.questions.Count; i++)
                {
                    Console.Clear();
                    answers = ts.questions[i].wrong_answers;
                    answers.AddRange(ts.questions[i].correct_answers);
                    answers = RandomSort(answers);

                    Console.WriteLine($"Вопрос {i + 1}/{ts.questions.Count}");
                    ShowQuestion(ts.questions[i], answers);

                    if (ts.questions[i].correct_answers.Count == 1)
                    {
                        Console.WriteLine("Выберите правильный ответ -> ");
                        int otvet = Convert.ToInt32(Console.ReadLine());
                        if (answers[otvet - 1] == ts.questions[i].correct_answers[0])
                        {
                            count_points += ts.questions[i].count_points;
                            count_correct++;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Выберите правильные ответы(99 - что бы остановить) -> ");
                        int otvet = 0;
                        List<int> otvety = new List<int>();
                        while (otvet != 99)
                        {
                            otvet = Convert.ToInt32(Console.ReadLine());
                            if (otvet != 99)
                                otvety.Add(otvet);
                        }
                        int count_sovp = 0;
                        for (int x = 0; x < otvety.Count; x++)
                        {
                            for (int z = 0; z < ts.questions[i].correct_answers.Count; z++)
                            {
                                if (answers[otvety[x] - 1] == ts.questions[i].correct_answers[z])
                                {
                                    count_sovp++;
                                }
                            }
                        }
                        if (count_sovp == ts.questions[i].correct_answers.Count)
                        {
                            count_points += ts.questions[i].count_points;
                            count_correct++;
                        }
                    }
                    
                }
                ShowResult(count_points, max_points, ts.questions.Count, count_correct);
                Result res = new Result();
                res.test = ts;
                res.user = Current_user;
                res.count_p = count_points;
                res.count_correct_q = count_correct;
                results.Add(res);
                Console.WriteLine($"Позиция в топе теста: {Convert.ToString(GetPosition(res))}");
            }
            else if(choose == 2)
            {

                
                Test ts = MakeComboTest();
                ts.questions = RandomSort(ts.questions);
                List<string> answers = new List<string>();
                int max_points = ts.questions.Sum(s => s.count_points);

                for (int i = 0; i < ts.questions.Count; i++)
                {
                    Console.Clear();
                    answers = ts.questions[i].wrong_answers;
                    answers.AddRange(ts.questions[i].correct_answers);
                    answers = RandomSort(answers);

                    Console.WriteLine($"Вопрос {i + 1}/{ts.questions.Count}");
                    ShowQuestion(ts.questions[i], answers);

                    if (ts.questions[i].correct_answers.Count == 1)
                    {
                        Console.WriteLine("Выберите правильный ответ -> ");
                        int otvet = Convert.ToInt32(Console.ReadLine());
                        if (answers[otvet - 1] == ts.questions[i].correct_answers[0])
                        {
                            count_points += ts.questions[i].count_points;
                            count_correct++;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Выберите правильные ответы(99 - что бы остановить) -> ");
                        int otvet = 0;
                        List<int> otvety = new List<int>();
                        while (otvet != 99)
                        {
                            otvet = Convert.ToInt32(Console.ReadLine());
                            if (otvet != 99)
                                otvety.Add(otvet);
                        }
                        int count_sovp = 0;
                        for (int x = 0; x < otvety.Count; x++)
                        {
                            for (int z = 0; z < ts.questions[i].correct_answers.Count; z++)
                            {
                                if (answers[otvety[x] - 1] == ts.questions[i].correct_answers[z])
                                {
                                    count_sovp++;
                                }
                            }
                        }
                        if (count_sovp == ts.questions[i].correct_answers.Count)
                        {
                            count_points += ts.questions[i].count_points;
                            count_correct++;
                        }
                    }

                }
                ShowResult(count_points, max_points, ts.questions.Count, count_correct);
                Result res = new Result();
                res.test = ts;
                res.user = Current_user;
                res.count_p = count_points;
                res.count_correct_q = count_correct;
                results.Add(res);
                Console.WriteLine($"Позиция в топе теста: {Convert.ToString(GetPosition(res))}");
            }
        }
        public Test MakeComboTest()
        {
            Random rnd = new Random();
            Test ts = new Test();
            ts.Name = "Смешанный";
            Category cat = new Category();
            cat.Name = "Смешанный";
            ts.category = cat;
            Question qs = new Question();
            while (ts.questions.Count != 20)
            {
                int random_test = rnd.Next(0, tests.Count-1);
                int random_qs = rnd.Next(0, tests[random_test].questions.Count-1);
                qs = tests[random_test].questions[random_qs];
                ts.questions.Add(qs);
            }
            return ts;
        }
        public int GetPosition(Result res)
        {
            List<Result> temp_res = new List<Result>();
            temp_res = results.FindAll(s => s.test == res.test);
            temp_res.OrderByDescending(s => s.count_p);
            int pos = temp_res.IndexOf(res);
            return pos + 1;
        }
        static List<Question> RandomSort(List<Question> a)
        {
            Random random = new Random();
            var n = a.Count;
            while (n > 1)
            {
                n--;
                var i = random.Next(n + 1);
                var temp = a[i];
                a[i] = a[n];
                a[n] = temp;
            }

            return a;
        }
        static List<string> RandomSort(List<string> a)
        {
            Random random = new Random();
            var n = a.Count;
            while (n > 1)
            {
                n--;
                var i = random.Next(n + 1);
                var temp = a[i];
                a[i] = a[n];
                a[n] = temp;
            }

            return a;
        }
        public void CheckMyResults()
        {
            List<Result> res = new List<Result>();
            res = results.FindAll(s => s.user.Login == Current_user.Login);
            foreach(Result r in res)
            {
                Console.WriteLine("=======================================================");
                r.ShowResultFull();
            }
            Console.ReadKey();
        }
        public void CheckTop20Results()
        {
            for (int i = 0; i < tests.Count; i++)
                ShowTests(i);

            int id = -1;
            do
            {
                Console.WriteLine("Введите ид теста: ");
                id = Convert.ToInt32(Console.ReadLine());
                if (id < 0 && id > tests.Count - 1)
                    Console.WriteLine("Неверный ид!");
            } while (id < 0 && id > tests.Count - 1);
            List<Result> res = new List<Result>();
            res = results.FindAll(s => s.test.Name == tests[id].Name);
            res.OrderByDescending(s => s.count_p);
            if (res.Count() == 0)
                Console.WriteLine("Пусто!");
            else
            {
                if (res.Count < 20)
                {
                    for (int i = 0; i < res.Count; i++)
                    {
                        Console.WriteLine($"{i + 1} - {res[i].GetResultShort()}");
                    }
                }
                else
                {
                    for (int i = 0; i < 20; i++)
                    {
                        Console.WriteLine($"{i + 1} - {res[i].GetResultShort()}");
                    }
                }
            }
            Console.ReadKey();
        }
        public void UpdateUser(User ur, User new_user)
        {
            foreach(Result res in results)
            {
                if(res.user == ur)
                    res.user = new_user;
            }
        }
        public void UpdateTest(Test ts, Test new_test)
        {
            foreach (Result res in results)
            {
                if (res.test == ts)
                    res.test = new_test;
            }
        }
        public bool Login()
        {
            string log;
            string pass;
            Console.Clear();
            Console.WriteLine("Введите логин -> ");
            log = Console.ReadLine();
            if (Users.Exists(s => s.Login == log))
            {

                User ur = Users.Find(s => s.Login == log);
                Console.WriteLine("Введите пароль -> ");
                pass = Console.ReadLine();
                if (pass != ur.Password)
                {
                    Console.WriteLine("Неправильный пароль!");
                    Console.WriteLine("Попробуйте еще раз через 3 секунды...");
                    Thread.Sleep(3000);
                }
                
                Current_user = ur;
                return true;
            }
            Console.WriteLine("Пользователь не найдей!");
            Console.ReadKey();
            return false;
        }

        public void MenuUser()
        {
            int menu = 0;
            try
            {
                while (menu != 5)
                {
                    SaveAll();
                    Console.Clear();
                    Console.WriteLine("1 - Сдать новый тест;");
                    Console.WriteLine("2 - Посмотреть результаты прошлых викторин;");
                    Console.WriteLine("3 - Посмотреть Топ-20 по конкретной викторине;");
                    Console.WriteLine("4 - Изменить данные;");
                    Console.WriteLine("5 - Выход;");
                    Console.WriteLine("Выберите действие -> ");
                    menu = Convert.ToInt32(Console.ReadLine());
                    if (menu == 1)
                        UseTest();
                    else if (menu == 2)
                        CheckMyResults();
                    else if (menu == 3)
                        CheckTop20Results();
                    else if (menu == 4)
                    {
                        menu = 0;
                        while (menu != 3)
                        {
                            Console.Clear();
                            Console.WriteLine("1 - Изменить пароль;");
                            Console.WriteLine("2 - Изменить дату рождения;");
                            Console.WriteLine("3 - Выход;");
                            menu = Convert.ToInt32(Console.ReadLine());
                            if (menu == 1)
                            {
                                User new_user = Current_user;
                                Console.WriteLine("Введите новый пароль -> ");
                                string pass = Console.ReadLine();
                                new_user.Password = pass;
                                UpdateUser(Current_user, new_user);
                            }
                            else if (menu == 2)
                            {
                                User new_user = Current_user;
                                Console.WriteLine("Введите нову дату рождения -> ");
                                DateTime dt = Convert.ToDateTime(Console.ReadLine());
                                new_user.Birthday_date = dt;
                                UpdateUser(Current_user, new_user);
                            }
                            else if (menu == 3)
                                menu = 3;
                            SaveAll();
                        }
                        menu = 0;
                        SaveAll();
                    }

                    else if (menu == 5)
                        break;
                    SaveAll();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void MenuAdmin()
        {
            int menu = 0;
            try
            {
                menu = 0;
                while (menu != 6)
                {
                    Console.Clear();
                    Console.WriteLine("1 - Управление тестами;");
                    Console.WriteLine("2 - Управление пользователями;");
                    Console.WriteLine("3 - Посмотреть Топ-20 по конкретной викторине;");
                    Console.WriteLine("4 - Изменить данные;");
                    Console.WriteLine("5 - Управление категориями;");
                    Console.WriteLine("6 - Выход;");
                    Console.WriteLine("Выберите действие -> ");
                    menu = Convert.ToInt32(Console.ReadLine());
                    if (menu == 1)
                    {
                        while (menu != 4)
                        {
                            Console.Clear();
                            Console.WriteLine("1 - Добавить тест;");
                            Console.WriteLine("2 - Изменить тест;");
                            Console.WriteLine("3 - Удалить тест;");
                            Console.WriteLine("4 - Выход;");
                            Console.WriteLine("Выберите действие -> ");
                            menu = Convert.ToInt32(Console.ReadLine());
                            if (menu == 1)
                                CreateTest();
                            else if (menu == 2)
                            {
                                Console.Clear();
                                for (int i = 0; i < tests.Count; i++)
                                    ShowTests(i);

                                int id = -1;
                                do
                                {
                                    Console.WriteLine("Введите ид теста: ");
                                    id = Convert.ToInt32(Console.ReadLine());
                                    if (id < 0 && id > tests.Count - 1)
                                        Console.WriteLine("Неверный ид!");
                                } while (id < 0 && id > tests.Count - 1);
                                Test test = tests[id];
                                menu = 0;
                                while (menu != 5)
                                {
                                    Console.Clear();
                                    Console.WriteLine("1 - Изменить название ;");
                                    Console.WriteLine("2 - Добавить вопрос;");
                                    Console.WriteLine("3 - Удалить вопрос;");
                                    Console.WriteLine("4 - Изменить категорию;");
                                    Console.WriteLine("5 - Выход;");
                                    Console.WriteLine("Выберите действие -> ");
                                    menu = Convert.ToInt32(Console.ReadLine());
                                    if (menu == 1)
                                    {
                                        Console.WriteLine("Введите новое название теста -> ");
                                        test.Name = Console.ReadLine();
                                    }
                                    else if (menu == 2)
                                        test.AddQuestion();

                                    else if (menu == 3)
                                        test.DeleteQuestion();
                                    else if (menu == 4)
                                    {
                                        Console.Clear();
                                        for (int i = 0; i < categories.Count; i++)
                                            ShowCategories(i);
                                        int id_cat = -1;
                                        do
                                        {
                                            Console.WriteLine("Введите ид категории: ");
                                            id_cat = Convert.ToInt32(Console.ReadLine());
                                            if (id_cat > categories.Count - 1 && id_cat < 0)
                                                Console.WriteLine("Неправильное ид!");

                                        } while (id_cat > tests.Count - 1 && id_cat < 0);
                                        test.category = categories[id_cat];
                                    }
                                    UpdateTest(tests[id], test);
                                }

                                menu = 0;
                            }

                            else if (menu == 3)
                                CheckTop20Results();
                        }
                        SaveAll();
                    }

                    else if (menu == 2)
                    {
                        menu = 0;
                        while (menu != 3)
                        {
                            Console.Clear();
                            Console.WriteLine("1 - Удалить пользователя;");
                            Console.WriteLine("2 - Изменить пользователя;");
                            Console.WriteLine("3 - Выход;");
                            Console.WriteLine("Выберите действие -> ");
                            menu = Convert.ToInt32(Console.ReadLine());
                            if (menu == 1)
                                DeleteUser();
                            else if (menu == 2)
                            {
                                    
                                Console.Clear();
                                for (int i = 0; i < Users.Count; i++)
                                    ShowUsers(i);

                                int id = -1;
                                do
                                {
                                    Console.WriteLine("Введите ид пользователя: ");
                                    id = Convert.ToInt32(Console.ReadLine());
                                    if (id < 0 && id > Users.Count - 1)
                                        Console.WriteLine("Неверный ид!");
                                } while (id < 0 && id > Users.Count - 1);
                                User ur = Users[id];
                                menu = 0;
                                while (menu != 5)
                                {
                                    Console.Clear();
                                    Console.WriteLine("1 - Изменить логин ;");
                                    Console.WriteLine("2 - Изменить пароль;");
                                    Console.WriteLine("3 - Изменить дату рождения;");
                                    Console.WriteLine("4 - Изменить тип;");
                                    Console.WriteLine("5 - Выход;");
                                    Console.WriteLine("Выберите действие -> ");
                                    menu = Convert.ToInt32(Console.ReadLine());
                                    if (menu == 1)
                                    {
                                        Console.WriteLine("Введите новый логин пользователя -> ");
                                        ur.Login = Console.ReadLine();
                                    }
                                    else if (menu == 2)
                                    {
                                        Console.WriteLine("Введите новый пароль пользователя -> ");
                                        ur.Password = Console.ReadLine();
                                    }
                                    else if (menu == 3)
                                    {
                                        Console.WriteLine("Введите новую дату рождения пользователя -> ");
                                        ur.Birthday_date = Convert.ToDateTime(Console.ReadLine());
                                    }
                                    else if (menu == 4)
                                    {
                                        if (ur.Type == "User")
                                        {
                                            Console.WriteLine("Вы сделали пользователя администратором!");
                                            ur.Type = "Admin";
                                        }
                                        else if (ur.Type == "Admin")
                                        {
                                            Console.WriteLine("Вы сделали пользователя обычным!");
                                            ur.Type = "User";
                                        }
                                        Console.ReadKey();
                                    }
                                    UpdateUser(Users[id], ur);
                                }
                                menu = 0;
                            }
                        }
                        SaveAll();
                    }
                    else if (menu == 3)
                        CheckTop20Results();
                    else if (menu == 4)
                    {
                        menu = 0;
                        Console.Clear();
                        while (menu != 3)
                        {
                            Console.WriteLine("1 - Изменить пароль;");
                            Console.WriteLine("2 - Изменить дату рождения;");
                            Console.WriteLine("3 - Выход;");
                            menu = Convert.ToInt32(Console.ReadLine());
                            if (menu == 1)
                            {
                                User new_user = Current_user;
                                Console.WriteLine("Введите новый пароль -> ");
                                string pass = Console.ReadLine();
                                new_user.Password = pass;
                                UpdateUser(Current_user, new_user);
                            }
                            else if (menu == 2)
                            {
                                User new_user = Current_user;
                                Console.WriteLine("Введите нову дату рождения -> ");
                                DateTime dt = Convert.ToDateTime(Console.ReadLine());
                                new_user.Birthday_date = dt;
                                UpdateUser(Current_user, new_user);
                            }
                            else if (menu == 3)
                                menu = 3;
                        }
                        menu = 0;
                        SaveAll();
                    }

                    else if (menu == 5)
                    {
                        menu = 0;
                        while (menu != 3)
                        {
                            Console.Clear();
                            Console.WriteLine("1 - Добавить категорию;");
                            Console.WriteLine("2 - Удалить категорию;");
                            Console.WriteLine("3 - Выход;");
                            menu = Convert.ToInt32(Console.ReadLine());
                            if (menu == 1)
                                CreateCategory();
                            else if (menu == 2)
                                DeleteCategory();
                        }
                        SaveAll();
                    }
                    SaveAll();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}
