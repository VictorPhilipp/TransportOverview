namespace TransportOverview.Data {
	public class TransportStopData {
		/// <summary>
		/// Stop id
		/// </summary>
		public int id = 0;

		/// <summary>
		/// Stop name
		/// </summary>
		public string name = null;

		/// <summary>
		/// Stop position
		/// </summary>
		public Vector3Data position = null;

		/// <summary>
		/// District name
		/// </summary>
		public string districtName = null;

		/// <summary>
		/// Stop position, relative to line (0..1)
		/// </summary>
		public float relLinePos = 0f;

		/// <summary>
		/// Problems
		/// </summary>
		public uint problems = 0;

		/// <summary>
		/// Current number of waiting passengers
		/// </summary>
		public int numWaitingPassengers = 0;

		/// <summary>
		/// Previous week incoming/outgoing passengers
		/// </summary>
		public BalanceData lastWeekServedPassengers = null;
	}
}