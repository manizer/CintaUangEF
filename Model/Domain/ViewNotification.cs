using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Domain
{
    public class ViewNotification
    {
		public static readonly string ERROR = "Error";
		public static readonly string SUCCESS = "Success";

        public string Type { get; set; }
        public string Message { get; set; }
        public string Form { get; set; }

        public ViewNotification()
        {

        }

        public ViewNotification(string type, string message)
        {
            this.Type = type;
            this.Message = message;
        }

        public ViewNotification(string type, string message, string form)
        {
            this.Type = type;
            this.Message = message;
            this.Form = form;
        }

		public static ViewNotification Make(string Message, string Type)
		{
			return new ViewNotification(Type, Message);
		}
    }
}
