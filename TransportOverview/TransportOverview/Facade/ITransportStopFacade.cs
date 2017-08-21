using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportOverview.Data;

namespace TransportOverview.Facade {
	public interface ITransportStopFacade {
		/// <summary>
		/// Retrieves all stops on a given transport line
		/// </summary>
		/// <param name="lineId">Line id</param>
		/// <returns></returns>
		IList<TransportStopData> GetTransportLineStops(ushort lineId);
	}
}
