using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static TransportPassengerData;

namespace TransportOverview.Data {
	public class TransportPassengersGroupData {
		/// <summary>
		/// Average number of passengers
		/// </summary>
		public int average;

		/// <summary>
		/// Total number of passengers
		/// </summary>
		public int total;

		public TransportPassengersGroupData(ref GroupData groupData) {
			average = (int)groupData.m_averageCount;
			total = (int)groupData.m_finalCount;
		}
	}
}
