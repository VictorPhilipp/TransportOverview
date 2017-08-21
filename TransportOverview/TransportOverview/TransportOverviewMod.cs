using ICities;

namespace TransportOverview
{
    public class TransportOverviewMod : IUserMod {
		public static readonly string Version = "0.1-alpha";

		public string Description {
			get {
				return "Adds public transportation data to the City Web Server mod";
			}
		}

		public string Name {
			get {
				return "Public Transport Overview [" + Version + "]";
			}
		}
	}
}
