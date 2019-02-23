using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TelegrammAppMvcDotNetCore___Buisness_Logic.Models;

namespace TelegrammAppMvcDotNetCore___Buisness_Logic
{
	public static class Schedule
	{

		//static DbContextOptionsBuilder<MyContext> optionsBuilder = new DbContextOptionsBuilder<MyContext>().UseSqlite("Server=(localdb)\\mssqllocaldb;Database=mobilesdb;Trusted_Connection=True;");

		//public MyContext db = HttpContext.RequestServices.GetService<MyContext>();

		static MyContext _db;
		//private static MyContext db = new MyContext(optionsBuilder.Options);
		
		public static void Unit()
		{
			var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
			optionsBuilder.UseSqlServer("Server=localhost;Database=u0473827_bot;User Id=u0473827_bot;Password=a12345!;");
			_db = new MyContext(optionsBuilder.Options);
		}

		public static void AddUniversity(string name)
		{

			//MyContext dbContext = new MyContext(optionsBuilder.Options);
			//var controller = new ScheduleController(dbContext);

			if (!IsUniversityExist(name))
			{
				University un = new University();
				un.Name = name;

				_db.Universities.Add(un);
				_db.SaveChanges();
			}
		}

		public static void AddFaculty(string university, string name)
		{
			if (!IsFacultyExist(university, name))
			{
				Faculty fac = new Faculty();
				fac.Name = name;
				fac.University = _db.Universities.Where(n => n.Name == university).FirstOrDefault();

				_db.Faculties.Add(fac);
				_db.SaveChanges();
			}
		}

		public static void AddCourse(string university, string faculty, string name)
		{
			if (!IsCourseExist(university, faculty, name))
			{
				Course co = new Course();
				co.Name = name;
				co.Facultie = _db.Faculties.Where(n => n.University == _db.Universities.Where(m => m.Name == university).FirstOrDefault()).Where(x => x.Name == faculty).FirstOrDefault();

				_db.Courses.Add(co);
				_db.SaveChanges();
			}
		}

		public static void AddGroup(string university, string faculty, string course, string name)
		{
			if (!IsGroupExist(university, faculty, course, name))
			{
				Group gr = new Group();
				gr.Name = name;
				gr.Course = _db.Courses.Where(l => l.Facultie == _db.Faculties.Where(n => n.University == _db.Universities.Where(m => m.Name == university).FirstOrDefault()).Where(x => x.Name == faculty).FirstOrDefault()).Where(x => x.Name == course).FirstOrDefault();

				_db.Groups.Add(gr);
				_db.SaveChanges();
			}
		}



		public static void AddScheduleWeek(string university, string faculty, string course, string group, ScheduleWeek week)
		{
			week.Group = _db.Groups.Where(c => c.Course == _db.Courses.Where(ll => ll.Facultie == _db.Faculties.Where(n => n.University == _db.Universities.Where(m => m.Name == university).FirstOrDefault()).Where(x => x.Name == faculty).FirstOrDefault()).Where(x => x.Name == course).FirstOrDefault()).Where(v => v.Name == group).FirstOrDefault();
			_db.ScheduleWeeks.Add(week);
			_db.SaveChanges();
		}
		


		public static bool IsUniversityExist(string university)
		{
			University universitym = _db.Universities.Where(m => m.Name == university).FirstOrDefault();

			bool result = false;

			if (universitym != null)
				result = true;

			return result;
		}

		public static bool IsFacultyExist(string university, string faculty)
		{
			University universitym = _db.Universities.Where(m => m.Name == university).FirstOrDefault();

			Faculty facultym = _db.Faculties.Where(l => l.University == universitym).Where(t => t.Name == faculty).FirstOrDefault();

			bool result = false;

			if (facultym != null)
				result = true;

			return result;
		}

		public static bool IsCourseExist(string university, string faculty, string course)
		{
			University universitym = _db.Universities.Where(m => m.Name == university).FirstOrDefault();

			Faculty facultym = _db.Faculties.Where(l => l.University == universitym).Where(t => t.Name == faculty).FirstOrDefault();

			Course coursem = _db.Courses.Where(o => o.Facultie == facultym).Where(t => t.Name == course).FirstOrDefault();

			bool result = false;

			if (coursem != null)
				result = true;

			return result;
		}

		public static bool IsGroupExist(string university, string faculty, string course, string group)
		{
			University universitym = _db.Universities.Where(m => m.Name == university).FirstOrDefault();

			Faculty facultym = _db.Faculties.Where(l => l.University == universitym).Where(t => t.Name == faculty).FirstOrDefault();

			Course coursem = _db.Courses.Where(o => o.Facultie == facultym).Where(t => t.Name == course).FirstOrDefault();

			Group groupm = _db.Groups.Where(n => n.Course == coursem).Where(t => t.Name == group).FirstOrDefault();

			bool result = false;

			if (groupm != null)
				result = true;
			
			return result;
		}



		public static List<string> GetUniversities()
		{
			List<string> result = new List<string>();
			List<University> source = _db.Universities.ToList();

			foreach (University item in source)
			{
				result.Add(item.Name);
			}

			return result;
		}

		public static List<string> GetFaculties(string university)
		{
			List<string> result = new List<string>();

			University universitym = _db.Universities.Where(m => m.Name == university).FirstOrDefault();
			
			List<Faculty> source = _db.Faculties.Where(n => n.University == universitym).ToList();

			foreach (Faculty item in source)
			{
				result.Add(item.Name);
			}

			return result;
		}

		public static List<string> GetCourses(string university, string faculty)
		{
			University universitym = _db.Universities.Where(m => m.Name == university).FirstOrDefault();
			Faculty facultym = _db.Faculties.Where(l => l.University == universitym).Where(t => t.Name == faculty).FirstOrDefault();
			
			List<string> result = new List<string>();
			List<Course> source = _db.Courses.Where(n => n.Facultie == facultym).ToList();

			foreach (Course item in source)
			{
				result.Add(item.Name);
			}

			return result;
		}

		public static List<string> GetGroups(string university, string faculty, string course)
		{
			University universitym = _db.Universities.Where(m => m.Name == university).FirstOrDefault();
			Faculty facultym = _db.Faculties.Where(l => l.University == universitym).Where(t => t.Name == faculty).FirstOrDefault();
			Course coursem = _db.Courses.Where(o => o.Facultie == facultym).Where(t => t.Name == course).FirstOrDefault();

			List<string> result = new List<string>();
			List<Group> source = _db.Groups.Where(n => n.Course == coursem).ToList();

			foreach (Group item in source)
			{
				result.Add(item.Name);
			}

			return result;
		}

		public static ScheduleDay GetSchedule(string university, string faculty, string course, string group, int week, int day)
		{
			University universitym = _db.Universities.Where(m => m.Name == university).FirstOrDefault();

			Faculty facultym = _db.Faculties.Where(l => l.University == universitym).Where(t => t.Name == faculty).FirstOrDefault();

			Course coursem = _db.Courses.Where(o => o.Facultie == facultym).Where(t => t.Name == course).FirstOrDefault();

			Group groupm = _db.Groups.Where(n => n.Course == coursem).Where(t => t.Name == group).FirstOrDefault();

			List<ScheduleDay> li = _db.ScheduleWeeks.Include(c => c.Day).Where(n => n.Group == groupm).FirstOrDefault(m => m.Week == week)?.Day.ToList();
			
			return _db.ScheduleDays.Include(r => r.Lesson).FirstOrDefault(f => f.Id == li.Where(n => n.Day == day).FirstOrDefault().Id);
		}
	}
}