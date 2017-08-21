using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace TransportOverview.Extension {
	public static class NameValueCollectionExtension {
		/// <summary>
		/// Determines whether the specified key exists in the current collection.
		/// </summary>
		/// <returns>Returns <c>true</c> if the specified key exists, otherwise <c>false</c></returns>
		public static Boolean HasKey(this NameValueCollection nvc, String key) {
			return nvc.AllKeys.Any(obj => obj == key);
		}
	}
}
