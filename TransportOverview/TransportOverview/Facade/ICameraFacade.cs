using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TransportOverview.Facade {
	public interface ICameraFacade {
		/// <summary>
		/// Moves the camera to the given position
		/// </summary>
		/// <param name="pos">position</param>
		void GoToPos(Vector3 pos);

		/// <summary>
		/// Moves the camera to the given building
		/// </summary>
		/// <param name="buildingId">building</param>
		/// <param name="openInfoPanel">if true, opens the world info panel of the given building</param>
		void GoToBuilding(ushort buildingId, bool openInfoPanel = false);

		/// <summary>
		/// Moves the camera to the given vehicle
		/// </summary>
		/// <param name="vehicleId">vehicle</param>
		/// <param name="openInfoPanel">if true, opens the world info panel of the given vehicle</param>
		void GoToVehicle(ushort vehicleId, bool openInfoPanel = false);

		/// <summary>
		/// Moves the camera to the given parked vehicle
		/// </summary>
		/// <param name="parkedVehicleId">parked vehicle</param>
		/// <param name="openInfoPanel">if true, opens the world info panel of the given parked vehicle</param>
		void GoToParkedVehicle(ushort parkedVehicleId, bool openInfoPanel = false);

		/// <summary>
		/// Moves the camera to the given segment
		/// </summary>
		/// <param name="segmentId">segment</param>
		/// <param name="openInfoPanel">if true, opens the world info panel of the given segment</param>
		void GoToSegment(ushort segmentId, bool openInfoPanel = false);

		/// <summary>
		/// Moves the camera to the given node
		/// </summary>
		/// <param name="nodeId">node</param>
		/// <param name="openInfoPanel">if true, opens the world info panel of the given node</param>
		void GoToNode(ushort nodeId, bool openInfoPanel = false);

		/// <summary>
		/// Moves the camera to the given citizen instance
		/// </summary>
		/// <param name="citizenInstanceId">citizen instance</param>
		/// <param name="openInfoPanel">if true, opens the world info panel of the given citizen instance</param>
		void GoToCitizenInstance(ushort citizenInstanceId, bool openInfoPanel = false);
	}
}
