using CityWebServer.Extensibility;
using System;
using System.Collections.Generic;
using System.Net;
using ColossalFramework;
using ImprovedPublicTransport2.Detour;
using TransportOverview.Data;
using UnityEngine;
using System.IO;
using ColossalFramework.Plugins;
using static ColossalFramework.Plugins.PluginManager;
using ICities;
using TransportOverview.Util;

namespace TransportOverview.RequestHandler {
	public class TransportLinesUIRequestHandler : RequestHandlerBase, IExtRequestHandler {
		public static readonly string HANDLER_PATH = "/PTO/UI";

		public static TransportLinesUIRequestHandler Instance { get; private set; }

		public TransportLinesUIRequestHandler(IWebServer server)
			: base(server, Guid.NewGuid(), "Transport Lines UI", "Victor-Philipp Negoescu (@LinuxFan)", 100, HANDLER_PATH) {
			Instance = this;
		}

		public override IResponseFormatter Handle(HttpListenerRequest request) {
			string absFilePath = null;
			bool handle = false;
			try {
				handle = GetAbsoluteFilePath(request.Url.AbsolutePath, out absFilePath);
			} catch (Exception e) {
				OnLogMessage($"TransportLinesUIRequestHandler.Handle({request.Url.AbsolutePath}): An error occurred: {e}");
			}
			OnLogMessage($"TransportLinesUIRequestHandler.Handle({request.Url.AbsolutePath}): handle={handle}, absFilePath={absFilePath}");
			
			if (! handle) {
				return PlainTextResponse($"404/File Not Found", HttpStatusCode.NotFound);
			}

			return new FileResponseFormatter(this, absFilePath);
		}

		public override bool ShouldHandle(HttpListenerRequest request) {
			string absFilePath;
			try {
				bool ret = GetAbsoluteFilePath(request.Url.AbsolutePath, out absFilePath);
				OnLogMessage($"TransportLinesUIRequestHandler.ShouldHandle({request.Url.AbsolutePath}): ret={ret}, absFilePath={absFilePath}");
				return ret;
			} catch (Exception e) {
				OnLogMessage($"TransportLinesUIRequestHandler.ShouldHandle({request.Url.AbsolutePath}): An error occurred: {e}");
				return false;
			}
		}

		/// <summary>
		/// Determines the absolute file path to retrieve from the given absolute request path
		/// </summary>
		/// <param name="requestUrl">Request path</param>
		/// <param name="absFilePath">Output absolute file path</param>
		/// <returns>true if the handler should handle the request, false otherwise</returns>
		protected bool GetAbsoluteFilePath(string requestUrl, out string absFilePath) {
			absFilePath = null;
			if (!requestUrl.StartsWith(HANDLER_PATH)) {
				OnLogMessage($"TransportLinesUIRequestHandler.GetAbsoluteFilePath({requestUrl}): Request path does not start with handler path! -> false");
				return false;
			}

			string filePath = GetRequestedFilePath(requestUrl);
			absFilePath = GetAbsFilePath(filePath);
			bool ret = File.Exists(absFilePath);
			OnLogMessage($"TransportLinesUIRequestHandler.GetAbsoluteFilePath({requestUrl}): ret={ret} absFilePath={absFilePath} -> {ret}");
			return ret;
		}

		/// <summary>
		/// Builds a relative file path from the given request path
		/// </summary>
		/// <param name="requestUrl">Request path</param>
		/// <returns>Relative file path to retrieve</returns>
		protected string GetRequestedFilePath(string requestUrl) {
			var tmpUrl = requestUrl.Replace(HANDLER_PATH, "").Replace('/', Path.DirectorySeparatorChar);
			string ret = tmpUrl.Length > 0 && tmpUrl[0].Equals(Path.DirectorySeparatorChar) ? tmpUrl.Remove(0, 1) : tmpUrl;
			OnLogMessage($"TransportLinesUIRequestHandler.GetRequestedFilePath({requestUrl}): ret after pre-processing: {ret}");
			if (ret.IsNullOrWhiteSpace()) {
				ret = "index.html";
			}
			ret = Path.Combine("www", ret);
			OnLogMessage($"TransportLinesUIRequestHandler.GetRequestedFilePath({requestUrl}): ret={ret}");
			return ret;
		}

		/// <summary>
		/// Builds an absolute file path from the given request path
		/// </summary>
		/// <param name="relFilePath">Relative file path</param>
		/// <returns>Absolute file path</returns>
		protected string GetAbsFilePath(string relFilePath) {
			string ret = GetRootDirectory() + relFilePath;
			OnLogMessage($"TransportLinesUIRequestHandler.GetAbsFilePath({relFilePath}): ret={ret}");
			return ret;
		}

		/// <summary>
		/// Retrieves the mod's root directory
		/// </summary>
		/// <returns></returns>
		protected string GetRootDirectory() {
			string ret = TransportOverviewLoadingExtension.ModPluginInfo.modPath + Path.DirectorySeparatorChar;
			OnLogMessage($"TransportLinesUIRequestHandler.GetRootDirectory(): ret={ret}");
			return ret;
		}

		public void Log(string message) {
			OnLogMessage(message);
		}
		
		public class FileResponseFormatter : IResponseFormatter {
			private string filePath;
			private IExtRequestHandler reqHandler;

			public FileResponseFormatter(IExtRequestHandler reqHandler, string fileAbsolutePath) {
				filePath = fileAbsolutePath;
				this.reqHandler = reqHandler;
			}

			public override void WriteContent(HttpListenerResponse response) {
				try {
					var extension = Path.GetExtension(this.filePath);
					response.ContentType = MimeUtil.GetMimeType(extension.Replace(".", ""));
					response.StatusCode = (int)HttpStatusCode.OK;

					reqHandler.Log($"FileResponseFormatter.WriteContent: filePath={filePath} extension={extension} response.ContentType={response.ContentType}");

					// Open file, read bytes into buffer and write them to the output stream.
					using (FileStream fileReader = File.OpenRead(this.filePath)) {
						byte[] buffer = new byte[4096];
						int read;
						while ((read = fileReader.Read(buffer, 0, buffer.Length)) > 0) {
							response.OutputStream.Write(buffer, 0, read);
						}
					}
				} catch (Exception e) {
					response.ContentType = "text/plain";
					response.StatusCode = (int)HttpStatusCode.InternalServerError;
					using (StreamWriter sw = new StreamWriter(response.OutputStream)) {
						sw.Write($"An error occurred: {e}");
					}
				}
			}
		}
	}
}
