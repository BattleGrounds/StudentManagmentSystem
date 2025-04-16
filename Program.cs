using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagmentSystem
{
	internal class Program
	{
		/// <summary>
		/// Максимальное количество студентов
		/// </summary>
		const int MAX_STUDENTS = 100;
		/// <summary>
		/// Минимальное количество атрибутов у студента
		/// </summary>
		const int MIN_STUDENT_DATA_SIZE = 4;
		/// <summary>
		/// Текущая позиция курсора (следующий ID после последнего добавленного студента)
		/// </summary>
		static int cursor = 0;
		static bool AddStudent(string[,] students, string[] studentData)
		{
			if (students == null || studentData == null) return false;
			if (cursor >= MAX_STUDENTS) return false;

			for (int i = 0; i < MIN_STUDENT_DATA_SIZE; i++)
			{
				students[cursor, i] = studentData[i];
			}

			cursor++;
			return true;
		}
		static bool DeleteStudent(string[,] students, int id)
		{
			if (id < 1 || id > cursor) return false; // Проверка на корректный ID
			int index = id - 1; // Приведение к индексации массива

			for (int i = index + 1; i < cursor; i++)
			{
				for (int j = 0; j < MIN_STUDENT_DATA_SIZE; j++)
				{
					students[i - 1, j] = students[i, j];
				}
			}

			cursor--;
			return true;
		}
		static void PrintStudents(string[,] students)
		{
			Console.WriteLine("ID\tИмя\tВозраст\tОценка\tСпециальность");
			for(int i = 0;i < cursor; i++)
			{
				Console.Write($"{i + 1}. ");
				for(int j = 0; j < MIN_STUDENT_DATA_SIZE; j++)
				{
					Console.Write($"{students[i, j]}");
					if (j < MIN_STUDENT_DATA_SIZE - 1)
						Console.Write("\t");
				}
				Console.WriteLine();
			}
		}
		static string[,] SortStudentsByName(string[,] students, bool asc = true)
		{
			// Допишите реализацию сортировки по имени
			return students;
		}
		static string[,] SortStudentsByAge(string[,] students, bool asc = true)
		{
			// Допишите реализацию сортировки по возрасту
			return students;
		}
		static string[,] SortStudentsByMarks(string[,] students, bool asc = true)
		{
			// Допишите реализацию сортировки по оценкам
			return students;
		}
		/// <summary>
		/// Функция сортировки студентов по ключу
		/// </summary>
		/// <param name="students">Массив студентов и их данных</param>
		/// <param name="key">Ключ по которому сортируем студентов (например: по оценкам)</param>
		/// <param name="asc">По возрастанию или по убыванию (по умолчанию: по возрастанию)</param>
		static string[,] SortStudents(string[,] students, string key, bool asc = true)
		{
			switch(key)
			{
				case "по оценкам":
					students = SortStudentsByMarks(students, asc);
					break;				
				case "по возрасту":
					students = SortStudentsByAge(students, asc);
					break;				
				case "по имени":
					students = SortStudentsByName(students, asc);
					break;
			}
			return students;
		}
		static void PrintMenu()
		{
			Console.WriteLine("╔══════════════════════════════════════╗");
			Console.WriteLine("║          Меню управления             ║");
			Console.WriteLine("╠══════════════════════════════════════╣");
			Console.WriteLine("║ 1. ➕ Добавление студента            ║");
			Console.WriteLine("║ 2. ➖ Удаление студента              ║");
			Console.WriteLine("║ 3. 🔄 Сортировка студентов           ║");
			Console.WriteLine("║ 4. 📋 Вывести список студентов       ║");
			Console.WriteLine("║ 0. ❌ Выход                          ║");
			Console.WriteLine("╚══════════════════════════════════════╝");
			Console.Write("Выберите действие: ");
		}
		static void Main(string[] args)
		{
			Console.OutputEncoding = System.Text.Encoding.UTF8;
			string[,] students = new string[MAX_STUDENTS, MIN_STUDENT_DATA_SIZE];
			bool quit = false;
			while(!quit)
			{
				PrintMenu();
				if (int.TryParse(Console.ReadLine(), out int choice))
				{
					switch (choice)
					{
						case 0: quit = true; break;
						case 1:
							{
								string[] temp = new string[MIN_STUDENT_DATA_SIZE];
								Console.Write("Введите имя студента: ");
								temp[0] = Console.ReadLine();
								Console.Write("Введите возраст студента: ");
								temp[1] = Console.ReadLine();
								Console.Write("Введите оценку студента: ");
								temp[2] = Console.ReadLine();
								Console.Write("Введите специальность студента: ");
								temp[3] = Console.ReadLine();
								if (!AddStudent(students, temp))
								{
									Console.WriteLine("Возникла ошибка при добавлении студента!");
								}
								else
								{
									Console.WriteLine($"Студент '{temp[0]}' успешно добавлен!");
								}
							}
							break;
						case 2:
							{
								Console.Write("Введите номер студента для удаления: ");
								if (int.TryParse(Console.ReadLine(), out int id) && DeleteStudent(students, id))
								{
									Console.WriteLine($"Студент с ID '{id}' успешно удалён!");
								}
								else
								{
									Console.WriteLine($"Ошибка при удалении студента с ID '{id}'");
								}
							}
							break;
						case 3:
							{
								Console.Write("Введите ключ для сортировки (по оценкам/по возрасту/по имени): ");
								string key = Console.ReadLine();
								string[,] sorted = SortStudents(students, key);
								if (sorted != null)
								{
									Console.WriteLine($"Студенты успешно отсортированы {key}!");
								}
								else
								{
									Console.WriteLine($"Ошибка при сортировке с ключом '{key}'");
								}
							}
							break;
						case 4:
							PrintStudents(students);
							break;
						default:
							Console.WriteLine("Вы неверно выбрали пункт!");
							break;
					}
				}
				else
				{
					Console.WriteLine("Некорректный ввод. Пожалуйста, введите число.");
				}
			}
		}
	}
}
