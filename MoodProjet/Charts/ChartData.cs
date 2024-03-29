﻿using System;

namespace MoodProjet.Charts
{
	public class ChartData
	{
		public ChartData(int face1Count, int face2Count, int face3Count, int face4Count, DateTime date)
		{
			Face1Count = face1Count;
			Face2Count = face2Count;
			Face3Count = face3Count;
			Face4Count = face4Count;
			Date = date.ToString("yyyy-MM-dd");
		}

		public int Face1Count { get; set; }

		public int Face2Count { get; set; }

		public int Face3Count { get; set; }

		public int Face4Count { get; set; }

		public string Date { get; set; }
	}
}
