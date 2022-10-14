namespace Quiz2
{
    class Program
    {
        
        
       
        static public void Main(String[] args)
        {
            TestControl tc = new TestControl();
            tc.Path = @"D:\ITSTEP\C#\Exam\Test";
            tc.LoadCategories();
            tc.LoadResults();
            tc.LoadTests();
            tc.LoadUsers();
            int menu = 0;
            while (menu != 3)
            {
                Console.Clear();
                Console.WriteLine("1 - Регистрация;");
                Console.WriteLine("2 - Вход;");
                Console.WriteLine("3 - Выход;");
                Console.WriteLine("Выберите действие -> ");
                menu = Convert.ToInt32(Console.ReadLine());
                if (menu == 1)
                    tc.NewUser();
                else if (menu == 2)
                {
                    if (tc.Login() == true)
                        menu = 3;
                }
            }
            if (tc.Current_user.Type == "Admin")
                tc.MenuAdmin();
            else
                tc.MenuUser();

            tc.SaveAll();
        }
    }
}
