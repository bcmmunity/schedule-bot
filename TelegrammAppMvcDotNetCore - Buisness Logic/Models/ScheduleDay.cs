﻿using System.Collections.Generic;

namespace TelegrammAppMvcDotNetCore___Buisness_Logic.Models
{
	public class ScheduleDay
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Day { get; set; }
		public ICollection<Lesson> Lesson { get; set; }

		public ScheduleDay()
		{
			Lesson = new List<Lesson>();
		}
	}
}
