using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CintaUang.Core.ApplicationSession
{
	public static class ApplicationSession
	{
		// Login User Data
		private static string LOGIN_USER_ID = "LOGIN_USER_ID";
		private static string LOGIN_USER_NAME = "LOGIN_USER_NAME";
		
		public static Int32? GetLoginUserId(this ISession session) => 1;
		public static void SetLoginUserId(this ISession session, Int32 LoginUserId) => session.SetInt32(LOGIN_USER_ID, LoginUserId);

		public static string GetLoginUserName(this ISession session) => "Fernando";
		public static void SetLoginUserName(this ISession session, string LoginUserName) => session.SetString(LOGIN_USER_NAME, LoginUserName);
	}
}
