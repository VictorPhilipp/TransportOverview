using ColossalFramework;
using ImprovedPublicTransport2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace TransportOverview.Facade.Impl {
	public class CameraFacade : AbstractFacade, ICameraFacade {
		public static ICameraFacade Instance = new CameraFacade();

		public void GoToBuilding(ushort buildingId, bool openInfoPanel = false) {
			CSUtil.CameraControl.CameraController.Instance.GoToBuilding(buildingId, openInfoPanel);
		}

		public void GoToCitizenInstance(ushort citizenInstanceId, bool openInfoPanel = false) {
			CSUtil.CameraControl.CameraController.Instance.GoToCitizenInstance(citizenInstanceId, openInfoPanel);
		}

		public void GoToNode(ushort nodeId, bool openInfoPanel = false) {
			CSUtil.CameraControl.CameraController.Instance.GoToNode(nodeId, false);
			if (openInfoPanel) {
				Singleton<SimulationManager>.instance.m_ThreadingWrapper.QueueMainThread(() => {

					InstanceID id = default(InstanceID);
					id.NetNode = nodeId;

					PublicTransportStopWorldInfoPanel.instance.Show(Camera.main.transform.position, id);
				});
			}
		}

		public void GoToParkedVehicle(ushort parkedVehicleId, bool openInfoPanel = false) {
			CSUtil.CameraControl.CameraController.Instance.GoToParkedVehicle(parkedVehicleId, openInfoPanel);
		}

		public void GoToPos(Vector3 pos) {
			CSUtil.CameraControl.CameraController.Instance.GoToPos(pos);
		}

		public void GoToSegment(ushort segmentId, bool openInfoPanel = false) {
			CSUtil.CameraControl.CameraController.Instance.GoToSegment(segmentId, openInfoPanel);
		}

		public void GoToVehicle(ushort vehicleId, bool openInfoPanel = false) {
			CSUtil.CameraControl.CameraController.Instance.GoToVehicle(vehicleId, openInfoPanel);
		}
	}
}
