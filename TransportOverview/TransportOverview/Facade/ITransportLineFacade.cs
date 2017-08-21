using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportOverview.Data;

namespace TransportOverview.Facade {
	public interface ITransportLineFacade {
		/// <summary>
		/// Retrieves all valid transport lines
		/// </summary>
		/// <returns></returns>
		IList<TransportLineData> GetTransportLines();

		/// <summary>
		/// Retrieves the valid transport line with the given id
		/// </summary>
		/// <returns></returns>
		TransportLineData GetTransportLine(ushort lineId);
	}
}
