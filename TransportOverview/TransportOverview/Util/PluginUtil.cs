using ColossalFramework;
using ColossalFramework.Plugins;
using ICities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ColossalFramework.Plugins.PluginManager;

namespace TransportOverview.Util {
	public static class PluginUtil {
		public static PluginInfo FindModPluginInfo(Type pluginType) {
			foreach (PluginInfo pluginInfo in Singleton<PluginManager>.instance.GetPluginsInfo()) {
				try {
					IUserMod[] instances = pluginInfo.GetInstances<IUserMod>();

					if (instances == null || instances.Length <= 0) {
						continue;
					}

					if (!pluginType.Equals(instances.FirstOrDefault().GetType())) {
						continue;
					}

					DebugOutputPanel.AddMessage(MessageType.Message, $"PluginUtil.FindModPlugin({pluginType}): Mod pluginInfo found: {pluginInfo}");
					return pluginInfo;
				} catch (Exception e) {
					DebugOutputPanel.AddMessage(MessageType.Error, $"PluginUtil.FindModPlugin({pluginType}): An error occurred: {e}");
				}
			}

			DebugOutputPanel.AddMessage(MessageType.Warning, $"PluginUtil.FindModPlugin({pluginType}): No matching plugin found!");
			return null;
		}
	}
}
