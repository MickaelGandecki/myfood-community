using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Models.Helpers
{
	public class AddEvent
	{
		public int ProductionUnitId { get; set; }
		public int EventTypeId { get; set; }
		public int EventTypeItemId { get; set; }
		public string Note { get; set; }
		public DateTime CurrentDate { get; set; }
		public string Details { get; set; }
	}
}