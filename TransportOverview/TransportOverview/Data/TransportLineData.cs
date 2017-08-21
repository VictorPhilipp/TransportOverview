namespace TransportOverview.Data {
	public class TransportLineData {
		/// <summary>
		/// Line id
		/// </summary>
		public int id = 0;

		/// <summary>
		/// Line type (Bus, Train, Metro, etc.)
		/// </summary>
		public ItemClass.SubService type = ItemClass.SubService.None;

		/// <summary>
		/// Line color
		/// </summary>
		public ColorData color = null;

		/// <summary>
		/// Line name
		/// </summary>
		public string name = null;

		/// <summary>
		/// Line flags
		/// </summary>
		public uint flags = 0;

		/// <summary>
		/// Budget (in %)
		/// </summary>
		public ushort budgetInPercent = 0;

		/// <summary>
		/// Problems (accumulated from stops)
		/// </summary>
		public uint problems = 0;

		/// <summary>
		/// Grouped passenger counts
		/// </summary>
		public TransportPassengersData passengerStats = null;

		/// <summary>
		/// Target number of vehicles on this line
		/// </summary>
		public int targetNumVehicles = 0;

		/// <summary>
		/// Vehicle data
		/// </summary>
		public TransportVehicleData[] vehicles = null;

		/// <summary>
		/// Stop data
		/// </summary>
		public TransportStopData[] stops = null;
	}
}
