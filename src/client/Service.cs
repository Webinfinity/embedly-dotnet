﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using Common.Logging;

namespace Embedly
{
	/// <summary>
	/// Retrieves the list of supported services and provides a way to check if a URL is supported
	/// </summary>
	/// <remarks>
	/// This uses a string splitting and matching algorithm to check for supported URLs which is
	/// significantly faster than using regular expressions
	/// </remarks>
	internal sealed class Service
	{
		private const string ServiceApi = "http://api.embed.ly/1/services";
		private static readonly ILog Log = LogManager.GetCurrentClassLogger();

		// singleton pattern from: http://csharpindepth.com/Articles/General/Singleton.aspx
		private static readonly Lazy<Service> Lazy = new Lazy<Service>(() => new Service());

		private Service()
		{
			DownloadServices();
		}

		internal static Service Instance
		{
			get { return Lazy.Value; }
		}

		internal IEnumerable<Provider> Providers { get; private set; }

		private void DownloadServices()
		{
			Log.InfoFormat("Loading embed.ly service list");

			try
			{
				using (var client = new EnhancedWebClient())
				{
					client.Encoding = Encoding.UTF8;
					byte[] data = client.DownloadData(ServiceApi);

					DeserializeServices(data);
				}
			}
			catch (Exception ex)
			{
				Log.ErrorFormat("Error downloading embed.ly service list", ex);
			}
		}

		private void DeserializeServices(byte[] data)
		{
			try
			{
				using (var stream = new MemoryStream(data))
				{
					var serializer = new DataContractJsonSerializer(typeof(IEnumerable<Provider>));
					Providers = (IEnumerable<Provider>)serializer.ReadObject(stream);
				}
			}
			catch (Exception ex)
			{
				Log.ErrorFormat("Error deserializing embed.ly service list", ex);
			}
		}

		private class EnhancedWebClient : WebClient
		{
			protected override WebRequest GetWebRequest(Uri address)
			{
				var request = base.GetWebRequest(address) as HttpWebRequest;
				if (request == null) return null;

				request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
				return request;
			}
		}
	}
}