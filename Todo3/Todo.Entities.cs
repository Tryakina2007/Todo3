using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo3
{
    internal class Todo3
    {
    }
    public class UserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public UserModel(string username, string password, string email)
        {
            Username = username;
            Password = password; 
            Email = email;
            RegistrationDate = DateTime.Now;
        }
        public static class UserManager
        {
            private static List<UserModel> _registeredUsers = new List<UserModel>();

            public static bool RegisterUser(string username, string password, string email)
            {
                if (_registeredUsers.Any(u => u.Username == username))
                {
                    Console.WriteLine($"Пользователь с именем '{username}' уже существует.");
                    return false;
                }

                if (_registeredUsers.Any(u => u.Email == email))
                {
                    Console.WriteLine($"Пользователь с email '{email}' уже зарегистрирован.");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(username) || username.Length < 3)
                {
                    Console.WriteLine("Имя пользователя должно содержать минимум 3 символа.");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
                {
                    Console.WriteLine("Пароль должен содержать минимум 6 символов.");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                {
                    Console.WriteLine("Введите корректный email адрес.");
                    return false;
                }

                var newUser = new UserModel(username, password, email);
                _registeredUsers.Add(newUser);

                Console.WriteLine($"Пользователь '{username}' успешно зарегистрирован!");
                return true;
            }

            public static bool LoginUser(string username, string password)
            {
                var user = _registeredUsers.FirstOrDefault(u =>
                    u.Username == username && u.Password == password);

                if (user != null)
                {
                    Console.WriteLine($"Добро пожаловать, {username}!");
                    Console.WriteLine($"Дата регистрации: {user.RegistrationDate:dd.MM.yyyy HH:mm}");
                    return true;
                }
                else
                {
                    Console.WriteLine("Неверное имя пользователя или пароль.");
                    return false;
                }
            }

            public static int GetUserCount()
            {
                return _registeredUsers.Count;
            }

            public static bool UserExists(string username)
            {
                return _registeredUsers.Any(u => u.Username == username);
            }

            public static void DisplayAllUsers()
            {
                Console.WriteLine("\n--- Зарегистрированные пользователи ---");
                foreach (var user in _registeredUsers)
                {
                    Console.WriteLine($"Имя: {user.Username}, Email: {user.Email}, Дата: {user.RegistrationDate:dd.MM.yyyy}");
                }
                Console.WriteLine("--------------------------------------\n");
            }
            class Program
            {
                static void ToDo3(string[] args)
                {
                    UserManager.RegisterUser("ivanov", "password123", "ivanov@mail.com");
                    UserManager.RegisterUser("petrov", "qwerty456", "petrov@mail.com");
                    UserManager.RegisterUser("sidorova", "secret789", "sidorova@mail.com");

                    UserManager.RegisterUser("ivanov", "newpass", "new@mail.com");

                    UserManager.LoginUser("ivanov", "password123"); 
                    UserManager.LoginUser("ivanov", "wrongpass");   
                    UserManager.LoginUser("unknown", "password123"); 

                    Console.WriteLine($"\nВсего зарегистрировано пользователей: {UserManager.GetUserCount()}");
                    UserManager.DisplayAllUsers();
                }
            }
        }
    }
}
