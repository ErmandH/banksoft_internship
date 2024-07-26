using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScrumProcessTask.Utilites
{
	public class ScrumOperations
	{
		public static string GetTeamCode(string filename)
		{
			string[] teamCodeArr = filename.Split('_');
			return teamCodeArr[0];
		}

		public static DateTime? GetExcelDate(dynamic col)
		{
			DateTime? date = null;
			try
			{
				date = (DateTime)col;
			}
			catch (Exception ex)
			{
				try
				{
					string dateStr = Regex.Replace((string)col, @"\s+", "");
					date = Convert.ToDateTime(dateStr);
				}
				catch (Exception ex2)
				{
					date = null;
				}

			}
			return date;
		}

		public static double? HandleDoubleColumn(dynamic col) 
		{
			try
			{
				double decimalVal = col;
				return decimalVal;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}
