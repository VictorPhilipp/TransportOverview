using ColossalFramework;
using ImprovedPublicTransport2.Detour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportOverview.Data;
using UnityEngine;

namespace TransportOverview.Facade.Impl {
	public class TransportStopFacade : AbstractFacade, ITransportStopFacade {
		public static ITransportStopFacade Instance = new TransportStopFacade();

		public IList<TransportStopData> GetTransportLineStops(ushort lineId) {
			TransportManager transportMan = Singleton<TransportManager>.instance;
			NetManager netMan = Singleton<NetManager>.instance;
			InstanceManager instanceMan = Singleton<InstanceManager>.instance;
			DistrictManager districtMan = Singleton<DistrictManager>.instance;

			IList<TransportStopData> stops = new List<TransportStopData>();

			if (!TransportOverviewLoadingExtension.GameLoaded) {
				return stops;
			}

			if ((transportMan.m_lines.m_buffer[lineId].m_flags & (TransportLine.Flags.Created | TransportLine.Flags.Temporary | TransportLine.Flags.Hidden)) != TransportLine.Flags.Created
					|| !transportMan.m_lines.m_buffer[lineId].Complete
			) {
				throw new ArgumentException("Line is invalid / not complete");
			}

			uint problems = (uint)Notification.Problem.None;
			float[] stopDistances = new float[transportMan.m_lines.m_buffer[lineId].CountStops((ushort)lineId)];
			float totalDistance = 0f;
			ushort firstStopId = transportMan.m_lines.m_buffer[lineId].m_stops;
			ushort curStopId = firstStopId;
			int iter = 0;
			while (curStopId != 0) {
				TransportStopData stop = new TransportStopData();

				stop.id = (int)curStopId;

				InstanceID nodeInstance = InstanceID.Empty;
				nodeInstance.NetNode = curStopId;
				stop.name = instanceMan.GetName(nodeInstance);

				stop.position = new Vector3Data(ref netMan.m_nodes.m_buffer[curStopId].m_position);

				byte districtId = districtMan.GetDistrict(netMan.m_nodes.m_buffer[curStopId].m_position);
				stop.districtName = districtId == 0 ? null : districtMan.GetDistrictName(districtId);

				stop.numWaitingPassengers = transportMan.m_lines.m_buffer[lineId].CalculatePassengerCount(curStopId);

				if (NetManagerMod.m_cachedNodeData != null) {
					stop.lastWeekServedPassengers = new BalanceData(NetManagerMod.m_cachedNodeData[curStopId].LastWeekPassengersIn, NetManagerMod.m_cachedNodeData[curStopId].LastWeekPassengersOut);
				}

				stop.problems = (uint)netMan.m_nodes.m_buffer[curStopId].m_problems;

				problems |= stop.problems;

				stops.Add(stop);

				ushort nextStopId = 0;
				for (int i = 0; i < 8; i++) {
					ushort curSegmentId = netMan.m_nodes.m_buffer[curStopId].GetSegment(i);
					if (curSegmentId != 0 && netMan.m_segments.m_buffer[curSegmentId].m_startNode == curStopId) {
						nextStopId = netMan.m_segments.m_buffer[curSegmentId].m_endNode;
						stopDistances[iter] = Mathf.Max(100f, netMan.m_segments.m_buffer[curSegmentId].m_averageLength);
						break;
					}
				}

				totalDistance += stopDistances[iter];

				curStopId = nextStopId;
				if (curStopId == firstStopId) {
					break;
				}
				if (++iter >= NetManager.MAX_NODE_COUNT) {
					CODebugBase<LogChannel>.Error(LogChannel.Core, "Invalid node list detected!\n" + Environment.StackTrace);
					break;
				}
			}

			if (totalDistance > 0f) {
				float accDist = 0;
				for (int i = 1; i < stopDistances.Length; ++i) {
					stops[i].relLinePos = Mathf.Min(1f, (accDist + stopDistances[i - 1]) / totalDistance);
					accDist += stopDistances[i - 1];
				}
			}

			return stops;
		}
	}
}
