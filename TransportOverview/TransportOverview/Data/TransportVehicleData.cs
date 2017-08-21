namespace TransportOverview.Data {
	public class TransportVehicleData {
		/// <summary>
		/// Vehicle id
		/// </summary>
		public int id = 0;

		/// <summary>
		/// Vehicle name
		/// </summary>
		public string name = null;

		/// <summary>
		/// Vehicle position, relative to line (0..1)
		/// </summary>
		public float relLinePos = 0;

		/// <summary>
		/// Vehicle position
		/// </summary>
		public Vector3Data position = null;

		/// <summary>
		/// Number of on-board passengers
		/// </summary>
		public int numPassengers = 0;

		/// <summary>
		/// Max. number of passengers
		/// </summary>
		public int maxNumPassengers = 0;

		/// <summary>
		/// Number of transported passengers in previous week
		/// </summary>
		public int lastWeekNumPassengers = 0;

		/// <summary>
		/// Average number of transported passengers
		/// </summary>
		public int averageNumPassengers = 0;

		/// <summary>
		/// Previous week income
		/// </summary>
		public int lastWeekIncome = 0;

		/// <summary>
		/// Average income
		/// </summary>
		public int averageIncome = 0;
	}
}