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
	public class TransportLineFacade : AbstractFacade, ITransportLineFacade {
		public static ITransportLineFacade Instance = new TransportLineFacade();

		public IList<TransportLineData> GetTransportLines() {
			List<TransportLineData> lines = new List<TransportLineData>();

			if (!TransportOverviewLoadingExtension.GameLoaded) {
				return lines;
			}

			TransportManager transportMan = Singleton<TransportManager>.instance;

			for (uint lineId = 0; lineId < transportMan.m_lines.m_buffer.Length; ++lineId) {
				if ((transportMan.m_lines.m_buffer[lineId].m_flags & (TransportLine.Flags.Created | TransportLine.Flags.Temporary | TransportLine.Flags.Hidden)) != TransportLine.Flags.Created
					|| !transportMan.m_lines.m_buffer[lineId].Complete
				) {
					continue;
				}

				lines.Add(GetTransportLine((ushort)lineId));
			}

			return lines;
		}

		public TransportLineData GetTransportLine(ushort lineId) {
			TransportManager transportMan = Singleton<TransportManager>.instance;

			if (!TransportOverviewLoadingExtension.GameLoaded) {
				return null;
			}

			if ((transportMan.m_lines.m_buffer[lineId].m_flags & (TransportLine.Flags.Created | TransportLine.Flags.Temporary | TransportLine.Flags.Hidden)) != TransportLine.Flags.Created
					|| !transportMan.m_lines.m_buffer[lineId].Complete
			) {
				throw new ArgumentException("Line is invalid / not complete");
			}

			TransportLineData line = new TransportLineData();
			TransportInfo lineInfo = transportMan.m_lines.m_buffer[lineId].Info;
			if (lineInfo != null) {
				line.service = lineInfo.GetService();
				line.subService = lineInfo.GetSubService();
				line.level = lineInfo.GetClassLevel();

				line.allowedVehiclePrefabIndices = Facades.TransportVehiclePrefabFacade.GetTransportLineVehiclePrefabIndices(lineId).ToArray();
			}
			line.id = (int)lineId;
			
			line.enqueuedVehiclePrefabs = TransportLineMod.GetEnqueuedVehicles(lineId);

			if ((transportMan.m_lines.m_buffer[lineId].m_flags & TransportLine.Flags.CustomColor) != TransportLine.Flags.None) {
				line.color = new ColorData(ref transportMan.m_lines.m_buffer[lineId].m_color);
			} else if (lineInfo != null) {
				line.color = new ColorData(ref transportMan.m_properties.m_transportColors[(int)lineInfo.m_transportType]);
			}

			line.name = transportMan.GetLineName((ushort)lineId);
			line.flags = (uint)transportMan.m_lines.m_buffer[lineId].m_flags;
			line.budgetInPercent = transportMan.m_lines.m_buffer[lineId].m_budget;
			line.problems = (uint)Notification.Problem.None;
			line.passengerStats = new TransportPassengersData(ref transportMan.m_lines.m_buffer[lineId].m_passengers);
			line.targetNumVehicles = transportMan.m_lines.m_buffer[lineId].CalculateTargetVehicleCount();

			// fill vehicle DTOs
			IList<TransportVehicleData> vehicles = Facades.TransportVehicleFacade.GetTransportLineVehicles((ushort)lineId);
			line.vehicles = vehicles.ToArray();

			// fill stop DTOs
			IList<TransportStopData> stops = Facades.TransportStopFacade.GetTransportLineStops((ushort)lineId);

			foreach (TransportStopData stop in stops) {
				line.problems |= stop.problems;
			}

			line.stops = stops.ToArray();

			return line;
		}
	}
}
