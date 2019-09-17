using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.DTO.DB
{
	public class BaseModel
	{
		[Column("auditedactivity")]
		public char AuditedActivity { get; private set; }
		[Column("auditeduserid")]
		public int AuditedUserId { get; private set; }
		[Column("auditedtime")]
		public DateTime AuditedTime { get; private set; }

		public void SetAuditedActivity(char AuditedActivity)
		{
			this.AuditedActivity = AuditedActivity;
		}

		public void SetAuditedUserId(int AuditedUserId)
		{
			this.AuditedUserId = AuditedUserId;
		}

		public void SetAuditedTime(DateTime AuditedTime)
		{
			this.AuditedTime = AuditedTime;
		}
	}
}
