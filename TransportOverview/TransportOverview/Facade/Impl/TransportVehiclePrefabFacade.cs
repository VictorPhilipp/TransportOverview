using ColossalFramework;
using ImprovedPublicTransport2;
using ImprovedPublicTransport2.Detour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TransportOverview.Data;
using UnityEngine;

namespace TransportOverview.Facade.Impl {
	public class TransportVehiclePrefabFacade : AbstractFacade, ITransportVehiclePrefabFacade {
		public static ITransportVehiclePrefabFacade Instance = new TransportVehiclePrefabFacade();

		public IList<string> GetTransportVehiclePrefabs(ItemClass itemClass) {
			return GetTransportVehiclePrefabs(itemClass.m_service, itemClass.m_subService, itemClass.m_level);
		}

		public IList<string> GetTransportVehiclePrefabs(ItemClass.Service service, ItemClass.SubService subService, ItemClass.Level level) {			
			if (!TransportOverviewLoadingExtension.GameLoaded) {
				return new List<string>();
			}

			string[] prefabNames = VehiclePrefabs.instance.GetPrefabs(service, subService, level)
				.Select(pf => pf.Info == null ? "<unnamed>" : pf.Info.name)
				.ToArray();
			Array.Sort(prefabNames);

			return new List<string>(prefabNames);
		}

		public IList<int> GetTransportLineVehiclePrefabIndices(ushort lineId) {
			TransportManager transportMan = Singleton<TransportManager>.instance;

			if (!TransportOverviewLoadingExtension.GameLoaded) {
				return new List<int>();
			}

			if ((transportMan.m_lines.m_buffer[lineId].m_flags & (TransportLine.Flags.Created | TransportLine.Flags.Temporary | TransportLine.Flags.Hidden)) != TransportLine.Flags.Created
					|| !transportMan.m_lines.m_buffer[lineId].Complete
			) {
				throw new ArgumentException("Line is invalid / not complete");
			}

			ICollection<string> allowedPrefabNames = TransportLineMod.GetPrefabs(lineId);
			TransportInfo lineInfo = transportMan.m_lines.m_buffer[lineId].Info;
			if (lineInfo == null) {
				return new List<int>();
			}
			IList<string> allPrefabNames = GetTransportVehiclePrefabs(lineInfo.m_class);

			if (allowedPrefabNames == null || allowedPrefabNames.Count == 0) {
				allowedPrefabNames = allPrefabNames;
			}

			return allowedPrefabNames.Select(n => allPrefabNames.IndexOf(n)).ToList();
		}

		public void AddPrefabToTransportLine(ushort lineId, int prefabIndex) {
			TransportManager transportMan = Singleton<TransportManager>.instance;
			SimulationManager simMan = Singleton<SimulationManager>.instance;

			if (!TransportOverviewLoadingExtension.GameLoaded) {
				return;
			}

			if ((transportMan.m_lines.m_buffer[lineId].m_flags & (TransportLine.Flags.Created | TransportLine.Flags.Temporary | TransportLine.Flags.Hidden)) != TransportLine.Flags.Created
					|| !transportMan.m_lines.m_buffer[lineId].Complete
			) {
				throw new ArgumentException("Line is invalid / not complete");
			}

			TransportInfo lineInfo = transportMan.m_lines.m_buffer[lineId].Info;
			if (lineInfo == null) {
				return;
			}

			IList<string> allPrefabNames = GetTransportVehiclePrefabs(lineInfo.m_class);
			if (prefabIndex < 0 || prefabIndex >= allPrefabNames.Count) {
				throw new ArgumentException("Invalid prefab index");
			}

			simMan.AddAction(() => {
				TransportLineMod.AddPrefab(lineId, allPrefabNames[prefabIndex]);
				UpdateIPTSelectedPrefabs();
			});
		}

		public void RemovePrefabFromTransportLine(ushort lineId, int prefabIndex) {
			TransportManager transportMan = Singleton<TransportManager>.instance;
			SimulationManager simMan = Singleton<SimulationManager>.instance;

			if (!TransportOverviewLoadingExtension.GameLoaded) {
				return;
			}

			if ((transportMan.m_lines.m_buffer[lineId].m_flags & (TransportLine.Flags.Created | TransportLine.Flags.Temporary | TransportLine.Flags.Hidden)) != TransportLine.Flags.Created
					|| !transportMan.m_lines.m_buffer[lineId].Complete
			) {
				throw new ArgumentException("Line is invalid / not complete");
			}

			TransportInfo lineInfo = transportMan.m_lines.m_buffer[lineId].Info;
			if (lineInfo == null) {
				return;
			}

			IList<string> allPrefabNames = GetTransportVehiclePrefabs(lineInfo.m_class);
			if (prefabIndex < 0 || prefabIndex >= allPrefabNames.Count) {
				throw new ArgumentException("Invalid prefab index");
			}

			simMan.AddAction(() => {
				TransportLineMod.RemovePrefab(lineId, allPrefabNames[prefabIndex]);
				UpdateIPTSelectedPrefabs();
			});
		}

		protected void UpdateIPTSelectedPrefabs() {
			// TODO ugly hack to force IPT2 to update the selected vehicle types
			Singleton<SimulationManager>.instance.m_ThreadingWrapper.QueueMainThread(() => {
				PanelExtenderLine panelExtenderLine = GameObject.FindObjectOfType<PanelExtenderLine>();
				if (panelExtenderLine == null) {
					throw new Exception("Could not find PanelExtenderLine");
				}

				ushort lineId = panelExtenderLine.GetLineID();

				if (lineId == 0) {
					return;
				}

				FieldInfo prefabListBoxField = typeof(PanelExtenderLine).GetField("_prefabListBox", BindingFlags.NonPublic | BindingFlags.Instance);
				if (prefabListBoxField == null) {
					throw new Exception("Could not find prefabListBox field");
				}

				VehicleListBox prefabListBox = prefabListBoxField.GetValue(panelExtenderLine) as VehicleListBox;
				if (prefabListBox == null) {
					throw new Exception("Could not find prefabListBox");
				}

				prefabListBox.SetSelectionStateToAll(false, false);
				prefabListBox.SelectedItems = TransportLineMod.GetPrefabs(lineId);
			});
		}
	}
}
