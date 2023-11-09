using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ENDTERMTEST
{


    public class Program
    {
        public static void Main(string[] args)
        {
            Student student = new Student();
            Hashtable studentHashtable = new Hashtable();

            int choice = 0;

            do
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Insert new student...");
                Console.WriteLine("2. Display all student list...");
                Console.WriteLine("3. Calculator average mark...");
                Console.WriteLine("4. Search student by ...?");
                Console.WriteLine("5. Exit.");
                Console.Write("Enter your choice: ");
                
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        student.InsertStudent();
                        Console.Write("Student ID: ");
                        int id = int.Parse(Console.ReadLine());

                        Console.Write("Student Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Gender: ");
                        string gender = Console.ReadLine();

                        Console.Write("Age: ");
                        int age = int.Parse(Console.ReadLine());

                        Console.Write("Class: ");
                        string className = Console.ReadLine();

                        int[] marks = new int[3];
                        Console.WriteLine("Enter 3 marks:");
                        for (int i = 0; i < 3; i++)
                        {
                            Console.Write($"Mark {i + 1}: ");
                            marks[i] = int.Parse(Console.ReadLine());
                        }
                        
                        // Tạo và thêm sinh viên mới vào Hashtable
                        Student newStudent = new Student();
                        newStudent.StudID = id;
                        newStudent.StudName = name;
                        newStudent.StudGender = gender;
                        newStudent.StudAge = age;
                        newStudent.StudClass = className;
                        newStudent.MarkList = marks;
                        newStudent.CalAvg();

                        studentHashtable[id] = newStudent;
                        Console.WriteLine("Student successfully added.");
                        break;
                    case 2:
                        student.Print();
                        Console.WriteLine($"Average Mark: {student.StudAvgMark}");
                        foreach (DictionaryEntry entry in studentHashtable)
                        {
                            IStudent studentInfo = (IStudent)entry.Value;
                            studentInfo.Print();
                        }
                        break;
                    case 3:
                        foreach (DictionaryEntry entry in studentHashtable)
                        {
                            
                            student.CalAvg();
                            student.Print();
                        }
                        Console.WriteLine("ESC.");
                        break;
                    case 4:
                        Console.WriteLine("Choose one of the following options:");
                        Console.WriteLine("1. Search Students By ID");
                        Console.WriteLine("2. Search Students By Name");
                        Console.WriteLine("3. Search Students By Class");
                        Console.Write("Enter search option: ");
                        int option = int.Parse(Console.ReadLine());
                        switch (option)
                        {
                            case 1:
                                Console.Write("What ID?");
                                int searchID = int.Parse(Console.ReadLine());
                                var listStudentsByID = studentHashtable.Values.Cast<Student>().Where(s => s.StudID == searchID).OrderBy(s => s.StudName);
                                foreach (Student students in listStudentsByID)
                                {
                                    student.Print();
                                }
                                break;
                            case 2:
                                Console.Write("Who to search?");
                                string searchName = Console.ReadLine();
                                var listStudentsByName = studentHashtable.Values.Cast<Student>().Where(s => s.StudName.Equals(searchName, StringComparison.OrdinalIgnoreCase)).OrderBy(s => s.StudName);
                                foreach (Student students in listStudentsByName)
                                {
                                    student.Print();
                                }
                                break;
                            case 3:
                                Console.Write("Which class?");
                                string searchClass = Console.ReadLine();
                                var listStudentsByClass = studentHashtable.Values.Cast<Student>().Where(s => s.StudClass.Equals(searchClass, StringComparison.OrdinalIgnoreCase)).OrderBy(s => s.StudName);
                                foreach (Student students in listStudentsByClass)
                                {
                                    student.Print();
                                }
                                break;

                            default:
                                Console.WriteLine("Choose again.");
                                break;
                        }

                        break;





                    case 5:
                        Console.WriteLine("Quitting ...");
                        break;

                    default:
                        Console.WriteLine("Please chooose again");
                        break;
                }
            } while (choice != 5);
        }
    }

    public interface IStudent
    {
        // Properties
        public int StudID { get; set; } 
        public string StudName { get; set; } 
        public string StudGender { get; set; }
        public int StudAge { get; set; }
        public string StudClass { get; set; }
        public float StudAvgMark { get; }

        // Methods
        public void Print(); 
    }

    public class Student : IStudent //implement interface IStudent
    {

        public int studID;
        public int StudID
        {
            get { return studID; }
            set { studID = value; }
        }

        public string studName;
        public string StudName
        {
            get { return studName; }
            set { studName = value; }
        }

        public string studGender;
        public string StudGender
        {
            get { return studGender; }
            set { studGender = value; }
        }

        public int studAge;
        public int StudAge
        {
            get { return studAge; }
            set { studAge = value; }
        }

        public string studClass;
        public string StudClass
        {
            get { return studClass; }
            set { studClass = value; }
        }

        public float studAvgMark;
        public float StudAvgMark
        {
            get { return studAvgMark; }
        }

        public void Print()
        {
            Console.WriteLine($"Student ID: {StudID}");
            Console.WriteLine($"Student Name: {StudName}");
            Console.WriteLine($"Gender: {StudGender}");
            Console.WriteLine($"Age: {StudAge}");
            Console.WriteLine($"Class: {StudClass}");
            Console.WriteLine($"Average Mark: {StudAvgMark}");
        }

        private int[] _markList = new int[3]; 

        public int[] MarkList
        {
            get { return _markList; }
            set { _markList = value; }
        }

        public float StudAvgMark1 { get; private set; } 

        public void CalAvg()
        {
            if (_markList.Length > 0)
            {
                int sum = 0;
                foreach (int mark in _markList)
                {
                    sum += mark;
                }

                StudAvgMark1 = (float)sum / _markList.Length;
            }
        }

        public void InsertStudent()
        {
            // Thêm sinh viên mới
            Console.WriteLine("============Insert new student.================");

            Console.Write("Student ID: ");
            int id = int.Parse(Console.ReadLine());
            StudID = id;

            Console.Write("Student Name: ");
            string name = Console.ReadLine();
            StudName = name;

            Console.Write("Gender: ");
            string gender = Console.ReadLine();
            StudGender = gender;

            Console.Write("Age: ");
            int age = int.Parse(Console.ReadLine());
            StudAge = age;

            Console.Write("Class: ");
            string className = Console.ReadLine();
            StudClass = className;

            Console.WriteLine("Enter 3 marks:");
            int[] marks = new int[3];
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"Mark {i + 1}: ");
                marks[i] = int.Parse(Console.ReadLine());
            }
            MarkList = marks;

            CalAvg(); // Tính toán điểm trung bình
            Console.WriteLine("Student Information Inserted.");
        }






    }


}