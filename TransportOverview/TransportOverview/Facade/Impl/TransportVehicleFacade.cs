using ColossalFramework;
using ImprovedPublicTransport2;
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
			TransportManager transportMan = Singleton<TransportManager>.instance;
			VehicleManager vehicleMan = Singleton<VehicleManager>.instance;

			IList<TransportVehicleData> vehicles = new List<TransportVehicleData>();

			if (!TransportOverviewLoadingExtension.GameLoaded) {
				return vehicles;
			}

			if ((transportMan.m_lines.m_buffer[lineId].m_flags & (TransportLine.Flags.Created | TransportLine.Flags.Temporary | TransportLine.Flags.Hidden)) != TransportLine.Flags.Created
					|| !transportMan.m_lines.m_buffer[lineId].Complete
			) {
				throw new ArgumentException("Line is invalid / not complete");
			}

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

		public bool AddTransportVehicle(ushort lineId, int? prefabIndex = null) {
			TransportManager transportMan = Singleton<TransportManager>.instance;
			SimulationManager simMan = Singleton<SimulationManager>.instance;

			if (!TransportOverviewLoadingExtension.GameLoaded) {
				return false;
			}

			if ((transportMan.m_lines.m_buffer[lineId].m_flags & (TransportLine.Flags.Created | TransportLine.Flags.Temporary | TransportLine.Flags.Hidden)) != TransportLine.Flags.Created
					|| !transportMan.m_lines.m_buffer[lineId].Complete
			) {
				throw new ArgumentException("Line is invalid / not complete");
			}

			TransportInfo lineInfo = transportMan.m_lines.m_buffer[lineId].Info;
			if (lineInfo == null) {
				return false;
			}
			IList<string> prefabNames = Facades.TransportVehiclePrefabFacade.GetTransportVehiclePrefabs(lineInfo.m_class);
			IList<int> allowedPrefabIndices = Facades.TransportVehiclePrefabFacade.GetTransportLineVehiclePrefabIndices(lineId);

			if (prefabIndex != null) {
				if (prefabIndex < 0 || prefabIndex >= allowedPrefabIndices.Count) {
					// invalid prefab index
					prefabIndex = null;
				}
			}

			string prefabName = null;
			if (prefabIndex == null) {
				prefabName = TransportLineMod.GetRandomPrefab(lineId);
			} else {
				prefabName = prefabNames[allowedPrefabIndices[(int)prefabIndex]];
			}

			if (prefabName == null) {
				return false;
			}

			ushort depotId = TransportLineMod.GetDepot(lineId);

			if (! TransportLineMod.CanAddVehicle(depotId, ref Singleton<BuildingManager>.instance.m_buildings.m_buffer[depotId], lineInfo)) {
				return false;
			}

			simMan.AddAction(() => {
				TransportLineMod.SetBudgetControlState(lineId, false);

				if (depotId == 0) {
					TransportLineMod.IncreaseTargetVehicleCount(lineId);
				} else {
					TransportLineMod.EnqueueVehicle(lineId, prefabName, true);
				}
			});
			return true;
		}

		public bool RemoveTransportVehicle(ushort lineId, int? vehicleIndex = null) {
			TransportManager transportMan = Singleton<TransportManager>.instance;
			SimulationManager simMan = Singleton<SimulationManager>.instance;

			if (!TransportOverviewLoadingExtension.GameLoaded) {
				return false;
			}

			if ((transportMan.m_lines.m_buffer[lineId].m_flags & (TransportLine.Flags.Created | TransportLine.Flags.Temporary | TransportLine.Flags.Hidden)) != TransportLine.Flags.Created) {
				// invalid line
				return false;
			}

			string[] enqueuedVehiclePrefabs = TransportLineMod.GetEnqueuedVehicles(lineId);
			if (enqueuedVehiclePrefabs != null && enqueuedVehiclePrefabs.Length > 0) {
				// remove a vehicle from the waiting queue
				if (vehicleIndex == null) {
					// remove vehicle that was queued most recently
					vehicleIndex = enqueuedVehiclePrefabs.Length - 1;
				} else if (vehicleIndex < 0) {
					vehicleIndex = 0;
				} else if (vehicleIndex >= enqueuedVehiclePrefabs.Length) {
					vehicleIndex = enqueuedVehiclePrefabs.Length;
				}

				simMan.AddAction(() => {
					TransportLineMod.SetBudgetControlState(lineId, false);
					TransportLineMod.DequeueVehicles(lineId, new int[] { (int)vehicleIndex }, true);
				});
			} else {
				// remove an active vehicle
				if (vehicleIndex == null) {
					vehicleIndex = 0;
				}

				VehicleManager vehicleMan = Singleton<VehicleManager>.instance;

				simMan.AddAction(() => {
					ushort vehicleId = 0;
					ushort curVehicleId = transportMan.m_lines.m_buffer[lineId].m_vehicles;
					int iter = 0;
					while (curVehicleId != 0) {
						ushort firstVehicleId = vehicleMan.m_vehicles.m_buffer[curVehicleId].GetFirstVehicle(curVehicleId);

						if (iter == 0) {
							// remember the first vehicle on line in case vehicleIndex is out of bounds
							vehicleId = curVehicleId;
						}

						if (iter == vehicleIndex) {
							// found!
							vehicleId = curVehicleId;
							break;
						}

						curVehicleId = vehicleMan.m_vehicles.m_buffer[curVehicleId].m_nextLineVehicle;
						if (++iter > VehicleManager.MAX_VEHICLE_COUNT) {
							CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid vehicle list detected!\n" + Environment.StackTrace);
							break;
						}
					}

					// remove the vehicle
					if (vehicleId == 0) {
						return;
					}

					TransportLineMod.SetBudgetControlState(lineId, false);
					TransportLineMod.RemoveVehicle(lineId, vehicleId, true);
				});
			}

			return true;
		}
	}
}
