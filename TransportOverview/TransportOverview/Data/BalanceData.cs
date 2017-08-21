namespace TransportOverview.Data {
	public class BalanceData {
		/// <summary>
		/// Incoming (positive) amount
		/// </summary>
		public int incoming = 0;

		/// <summary>
		/// Outgoing (negative) amount
		/// </summary>
		public int outgoing = 0;

		public BalanceData(int incoming, int outgoing) {
			this.incoming = incoming;
			this.outgoing = outgoing;
		}
	}
}