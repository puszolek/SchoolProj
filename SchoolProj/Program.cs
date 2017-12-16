using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SchoolProj
{
    class Program
    {
        static void Main(string[] args)
        {
            School mySchool = new School();

            //****** Task 0 *******
            int[] array1D = new int[100];
            Random rnd = new Random();

            for(int i = 0; i < array1D.Length; i++)
            {
                array1D[i] = rnd.Next(0, 99);
            }

            foreach (int element in array1D)
            {
                System.Console.WriteLine(element);
            }

            var sizeArrayQuery = (from a in array1D select a).Count();            System.Console.WriteLine("Size of the array: " + sizeArrayQuery);

            var evenArrayQuery = (from a in array1D where a % 2 == 0 select a).Count();            System.Console.WriteLine("Even numbers: " + evenArrayQuery);

            var oddArrayQuery = (from a in array1D where a % 2 != 0 select a).Count();            System.Console.WriteLine("Odd numbers: " + oddArrayQuery);

            int[,] array2D = new int[10,5];
            for (int i = 0; i < array2D.GetLength(0); i++)
            {
                for (int j = 0; j < array2D.GetLength(1); j++)
                {
                    array2D[i,j] = rnd.Next(0, 99);
                }
            }

            int n = 0;
            foreach (int element in array2D)
            {
                System.Console.Write(element + " ");
                n++;
                if (n % array2D.GetLength(0) == 0)
                {
                    n = 0;
                    System.Console.WriteLine();
                }
            }
            System.Console.WriteLine();

            //****** Task 1 *******
            System.Console.WriteLine("Seattle or Warsaw students: ");
            var studentsSeattleOrWarsawQuery = (from student in mySchool.students
                                                where (student.City == "Seattle" || student.City == "Warsaw")
                                                select student);
            foreach(Student s in studentsSeattleOrWarsawQuery)
            {
                System.Console.WriteLine(s.First + " " + s.Last);
            }

            var studentsSeattleOrWarsawCountQuery = (from student in mySchool.students
                                                where (student.City == "Seattle" || student.City == "Warsaw")
                                                select student).Count();
            System.Console.WriteLine();
            System.Console.WriteLine("Seattle or Warsaw students count: " + studentsSeattleOrWarsawCountQuery);
            System.Console.WriteLine();

            //****** Task 2 *******
           var studentsSeattleOrWarsawSortedQuery = (from student in mySchool.students
                                                where (student.City == "Seattle" || student.City == "Warsaw")
                                                orderby student.Last
                                                select student);
            System.Console.WriteLine("Seattle or Warsaw students in order:");
            foreach (Student s in studentsSeattleOrWarsawSortedQuery)
            {
                System.Console.WriteLine(s.First + " " + s.Last);
            }
            System.Console.WriteLine();

            //****** Task 3 *******
            var studentsSeattleOrWarsawGroupedQuery = (from student in mySchool.students
                                                      group student by student.City);

            System.Console.WriteLine("Students grouped by city:");
            foreach (var sg in studentsSeattleOrWarsawGroupedQuery)
            {
                Console.WriteLine(sg.Key);
                foreach (Student s in sg)
                {
                    System.Console.WriteLine(s.First + " " + s.Last);
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine();

            //****** Task 4 *******
            IEnumerable<string> studentsTeachersQuery = (from student in mySchool.students select student.Last)
                                        .Concat(from teacher in mySchool.teachers select teacher.Last);
            System.Console.WriteLine("Last names:");
            foreach (string name in studentsTeachersQuery)
            {
                System.Console.WriteLine(name);
            }
            System.Console.WriteLine();

            //****** Task 5 *******
            var studentsTeachers2Query = (from student in mySchool.students select new { First=student.First, Last=student.Last })
                                        .Concat(from teacher in mySchool.teachers select new { First=teacher.First, Last=teacher.Last });

            //****** Task 6 *******
            System.Console.WriteLine("Seattle or Warsaw students 2nd query: ");
            var studentsSeattleOrWarsawQuery2 = mySchool.students.Where(p=>p.City == "Seattle" || p.City == "Warsaw").Select(p => p);
            foreach (Student s in studentsSeattleOrWarsawQuery2)
            {
                System.Console.WriteLine(s.First + " " + s.Last);
            }

            var studentsSeattleOrWarsawCountQuery2 = mySchool.students.Where(p => p.City == "Seattle" || p.City == "Warsaw")
                                                     .Select(p => p).Count();
            System.Console.WriteLine();
            System.Console.WriteLine("Seattle or Warsaw students 2nd query count: " + studentsSeattleOrWarsawCountQuery2);
            System.Console.WriteLine();

            IEnumerable<string> studentsTeachersQuery2 = mySchool.students.Select(p => p.Last)
                                                        .Concat(mySchool.teachers.Select(p => p.Last));
            System.Console.WriteLine("Last names 2nd query:");
            foreach (string name in studentsTeachersQuery2)
            {
                System.Console.WriteLine(name);
            }
            System.Console.WriteLine();


            //****** Task 7 ******
            var studentsToXML = new XElement("Root",
                                from student in mySchool.students
                                let x = String.Format("{0},{1},{2},{3}", student.Scores[0],
                                student.Scores[1], student.Scores[2], student.Scores[3])
                                select new XElement("student",
                                new XElement("First", student.First),
                                new XElement("Last", student.Last),
                                new XElement("Scores", x)
                                ) // end "student"
                                ); // end "Root"
            // Execute the query.
            Console.WriteLine("XML: ");
            Console.WriteLine(studentsToXML);

            System.Console.ReadKey();
        }
    }
}
