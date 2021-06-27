using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Models.Helpers
{
	public class UpdateEvent
	{
		public int Id { get; set; }
		public string Note { get; set; }
		public DateTime CurrentDate { get; set; }
		public string Details { get; set; }
		public bool IsOpen { get; set; }
		public string CreatedBy { get; set; }
	}
}