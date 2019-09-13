using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.DTO.DB
{
	public class BaseModel
	{
		[Column("auditedactivity")]
		public char AuditedActivity { get; set; }
		[Column("auditeduserid")]
		public int AuditedUserId { get; set; }
		[Column("auditedtime")]
		public DateTime AuditedTime { get; set; }
	}
}
