using System;

namespace TodoAndris.Models
{
	public class Item
	{
		public string Id { get; set; }
		public string Text { get; set; }
		public string Description { get; set; }
        public int Days { get; set; }
        public int Progress { get; set; }
        public DateTime LastCompleted { get; set; }
    }
}
