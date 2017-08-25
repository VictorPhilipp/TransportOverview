namespace TransportOverview.Data {
	public class TransportLineData {
		/// <summary>
		/// Line id
		/// </summary>
		public int id = 0;

		/// <summary>
		/// Line service
		/// </summary>
		public ItemClass.Service service = ItemClass.Service.None;

		/// <summary>
		/// Line sub service (Bus, Train, Metro, etc.)
		/// </summary>
		public ItemClass.SubService subService = ItemClass.SubService.None;

		/// <summary>
		/// Line level
		/// </summary>
		public ItemClass.Level level = ItemClass.Level.None;

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
		/// Active vehicles
		/// </summary>
		public TransportVehicleData[] vehicles = null;

		/// <summary>
		/// Queued vehicle prefab names
		/// </summary>
		public string[] enqueuedVehiclePrefabs = null;

		/// <summary>
		/// Allowed vehicle prefab names
		/// </summary>
		public int[] allowedVehiclePrefabIndices = null;

		/// <summary>
		/// Transport stops
		/// </summary>
		public TransportStopData[] stops = null;
	}
}
