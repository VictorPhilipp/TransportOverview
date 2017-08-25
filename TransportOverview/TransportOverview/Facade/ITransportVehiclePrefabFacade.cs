using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransportOverview.Facade {
	public interface ITransportVehiclePrefabFacade {
		/// <summary>
		/// Retrieves a sorted list of possible transport vehicle prefab names for the given transport line item class
		/// </summary>
		/// <param name="itemClass">item class of the transport line</param>
		/// <returns>sorted prefab names</returns>
		IList<string> GetTransportVehiclePrefabs(ItemClass itemClass);

		/// <summary>
		/// Retrieves a sorted list of possible transport vehicle prefab names for the given transport line service, sub service and level
		/// </summary>
		/// <param name="service">service type</param>
		/// <param name="subService">sub service type</param>
		/// <param name="level">level</param>
		/// <returns>sorted prefab names</returns>
		IList<string> GetTransportVehiclePrefabs(ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level);

		/// <summary>
		/// Retrieves a list of indices describing the allowed set of transport vehicle prefabs on the given line.
		/// </summary>
		/// <param name="lineId">transport line</param>
		/// <returns>allowed prefab indices</returns>
		IList<int> GetTransportLineVehiclePrefabIndices(ushort lineId);

		/// <summary>
		/// Adds the vehicle prefab with the given index to the given transport line
		/// </summary>
		/// <param name="lineId">transport line</param>
		/// <param name="prefabIndex">prefab index</param>
		void AddPrefabToTransportLine(ushort lineId, int prefabIndex);

		/// <summary>
		/// Removes the vehicle prefab with the given index from the given transport line
		/// </summary>
		/// <param name="lineId">transport line</param>
		/// <param name="prefabIndex">prefab index</param>
		void RemovePrefabFromTransportLine(ushort lineId, int prefabIndex);
	}
}
