using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportOverview.Data;

namespace TransportOverview.Facade {
	public interface ITransportVehicleFacade {
		/// <summary>
		/// Retrieves all vehicles on a given transport line
		/// </summary>
		/// <param name="lineId">Line id</param>
		/// <returns></returns>
		IList<TransportVehicleData> GetTransportLineVehicles(ushort lineId);

		/// <summary>
		/// Adds a (random) vehicle to the given transport line
		/// </summary>
		/// <param name="lineId">line to modify</param>
		/// <param name="prefabIndex">(optional) index of the prefab to use. If none is given a random valid prefab is added</param>
		bool AddTransportVehicle(ushort lineId, int? prefabIndex = null);

		/// <summary>
		/// Removes a (random) vehicle from the given transport line. If the line has queued vehicles, removes the
		/// <code>vehicleIndex</code><sup>th</sup> vehicle from the queue, otherwise removes the <code>vehicleIndex</code><sup>th</sup>
		/// vehicle from the active line.
		/// </summary>
		/// <param name="lineId">line to modify</param>
		/// <param name="vehicleIndex">(optional) vehicle to remove. If none is given either the most previous vehicle on the queue or the first vehicle of the active line is removed</param>
		bool RemoveTransportVehicle(ushort lineId, int? vehicleIndex = null);
	}
}
