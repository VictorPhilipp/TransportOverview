using ColossalFramework;
using ImprovedPublicTransport2.Detour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportOverview.Data;
using UnityEngine;

namespace TransportOverview.Facade.Impl {
	public class TransportVehicleFacade : AbstractFacade, ITransportVehicleFacade {
		public static ITransportVehicleFacade Instance = new TransportVehicleFacade();

		public IList<TransportVehicleData> GetTransportLineVehicles(ushort lineId) {
			IList<TransportVehicleData> vehicles = new List<TransportVehicleData>();

			TransportManager transportMan = Singleton<TransportManager>.instance;
			VehicleManager vehicleMan = Singleton<VehicleManager>.instance;

			ushort curVehicleId = transportMan.m_lines.m_buffer[lineId].m_vehicles;
			int iter = 0;
			while (curVehicleId != 0) {
				TransportVehicleData vehicle = new TransportVehicleData();

				ushort firstVehicleId = vehicleMan.m_vehicles.m_buffer[curVehicleId].GetFirstVehicle(curVehicleId);

				vehicle.id = (int)firstVehicleId;
				vehicle.name = vehicleMan.GetVehicleName(firstVehicleId);
				Vector3 pos = vehicleMan.m_vehicles.m_buffer[firstVehicleId].GetLastFramePosition();
				vehicle.position = new Vector3Data(ref pos);

				VehicleInfo vehicleInfo = vehicleMan.m_vehicles.m_buffer[firstVehicleId].Info;
				if (vehicleInfo != null) {
					float curProgress;
					float maxProgress;
					if (vehicleInfo.m_vehicleAI.GetProgressStatus(firstVehicleId, ref vehicleMan.m_vehicles.m_buffer[firstVehicleId], out curProgress, out maxProgress)) {
						if (maxProgress > 0) {
							vehicle.relLinePos = curProgress / maxProgress;
						}
					}

					string localeKey;
					int curPassengers;
					int maxPassengers;
					vehicleInfo.m_vehicleAI.GetBufferStatus(firstVehicleId, ref vehicleMan.m_vehicles.m_buffer[firstVehicleId], out localeKey, out curPassengers, out maxPassengers);
					vehicle.numPassengers = curPassengers;
					vehicle.maxNumPassengers = maxPassengers;
				}

				if (VehicleManagerMod.m_cachedVehicleData != null) {
					vehicle.lastWeekNumPassengers = VehicleManagerMod.m_cachedVehicleData[firstVehicleId].PassengersLastWeek;
					vehicle.averageNumPassengers = VehicleManagerMod.m_cachedVehicleData[firstVehicleId].PassengersAverage;
					vehicle.lastWeekIncome = VehicleManagerMod.m_cachedVehicleData[firstVehicleId].IncomeLastWeek;
					vehicle.averageIncome = VehicleManagerMod.m_cachedVehicleData[firstVehicleId].IncomeAverage;
				}

				vehicles.Add(vehicle);

				curVehicleId = vehicleMan.m_vehicles.m_buffer[curVehicleId].m_nextLineVehicle;
				if (++iter > VehicleManager.MAX_VEHICLE_COUNT) {
					CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid vehicle list detected!\n" + Environment.StackTrace);
					break;
				}
			}

			return vehicles;
		}
	}
}
