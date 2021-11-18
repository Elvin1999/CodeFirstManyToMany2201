using ManyToMany.DataAccess;
using ManyToMany.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ManyToMany
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (var context=new MyContext())
            {
                context.Database.CreateIfNotExists();

                //context.Students.Add(new Student
                //{
                //    Firstname="John",
                //    Lastname="Johnlu"
                //});

                //context.Students.Add(new Student
                //{
                //    Firstname = "Aysel",
                //    Lastname = "Mammadova"
                //});
                //context.Students.Add(new Student
                //{
                //    Firstname = "Rafiq",
                //    Lastname = "ALILI"
                //});

                //context.Students.Add(new Student
                //{
                //    Firstname = "Mike",
                //    Lastname = "Novruzlu"
                //});


                //context.Courses.Add(new Course { 

                // CourseName="STEP IT Academy",
                //  Address="Koroglu Rehimov"
                //});


                //context.Courses.Add(new Course
                //{
                //    CourseName = "Elvin Academy",
                //    Address = "Demirchi Tower"
                //});


                //var student1 = context.Students.FirstOrDefault(s => s.Id == 1);
                //var student2 = context.Students.FirstOrDefault(s => s.Id == 2);
                //var student3 = context.Students.FirstOrDefault(s => s.Id == 3);
                //var student4 = context.Students.FirstOrDefault(s => s.Id == 4);

                //var course1 = context.Courses.FirstOrDefault(c => c.Id == 1);
                //var course2 = context.Courses.FirstOrDefault(c => c.Id == 2);


                //student1.Courses.Add(course1);

                //student2.Courses.Add(course1);
                //student2.Courses.Add(course2);

                //student3.Courses.Add(course1);
                //student3.Courses.Add(course2);

                //student4.Courses.Add(course2);
                //context.SaveChanges();


                var students = context.Students.ToList();
                var courses = context.Courses.ToList();

                studentDataGrid.ItemsSource = students;

                courseDataGrid.ItemsSource = courses;



            }
        }

        private void courseDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var context=new MyContext())
            {
                try
                {
                    var item = courseDataGrid.SelectedItem as Course;
                    var course = context
                        .Courses
                        .Include("Students")
                        .FirstOrDefault(c => c.Id == item.Id);
                    if (item != null)
                    {
                        var students = course.Students.ToList();
                        studentDataGrid.ItemsSource = students;
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void studentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var context = new MyContext())
            {
                var item = studentDataGrid.SelectedItem as Student;
                var student = context.Students.Include("Courses").FirstOrDefault(s => s.Id == item.Id);
                if (item != null)
                {
                    var courses = student.Courses.ToList();
                    courseDataGrid.ItemsSource = courses;
                }
            }
        }
    }
}
