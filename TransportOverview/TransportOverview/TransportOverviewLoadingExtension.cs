using ColossalFramework;
using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransportOverview.Util;
using static ColossalFramework.Plugins.PluginManager;

namespace TransportOverview {
	public class TransportOverviewLoadingExtension : LoadingExtensionBase {
		public static bool GameLoaded { get; private set; }
		public static PluginInfo ModPluginInfo { get; private set; }

		public override void OnLevelLoaded(LoadMode mode) {
			SimulationManager.UpdateMode updateMode = Singleton<SimulationManager>.instance.m_metaData.m_updateMode;

			GameLoaded = false;
			switch (updateMode) {
				case SimulationManager.UpdateMode.NewGameFromMap:
				case SimulationManager.UpdateMode.NewGameFromScenario:
				case SimulationManager.UpdateMode.LoadGame:
					ModPluginInfo = PluginUtil.FindModPluginInfo(typeof(TransportOverviewMod));
					GameLoaded = true;
					break;
			}
		}

		public override void OnLevelUnloading() {
			GameLoaded = false;
		}
	}
}
