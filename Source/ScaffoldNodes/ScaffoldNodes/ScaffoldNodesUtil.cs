//	ScaffoldNodesUtil.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	ScaffoldNodesUtil																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Utilities and functionality for the scaffold application.
	/// </summary>
	public class ScaffoldNodesUtil
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//[DllImport("shlwapi.dll")]
		//private static extern int ColorHLSToRGB(int H, int L, int S);

		private static string[] mBoolChoices =
			new string[] { "true", "false", "yes", "no", "1", "0" };

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//* AttachResource																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Attach the specified existing resource to the node.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to which a resource will be attached.
		/// </param>
		/// <param name="resourceTicket">
		/// Globally unique identification of the resource to attach.
		/// </param>
		public static void AttachResource(NodeItem node,
			string resourceTicket)
		{
			PropertyItem property = null;
			ResourceItem resource = null;

			if(node != null && mNodeFileObject != null &&
				mNodeFileObject.Resources?.Count > 0 && resourceTicket?.Length > 0)
			{
				resource = mNodeFileObject.Resources.FirstOrDefault(x =>
					x.Ticket.ToLower() == resourceTicket.ToLower());
				if(resource != null)
				{
					property = node.Properties.FirstOrDefault(x =>
						x.Name == resource.ResourceType);
					if(property == null)
					{
						property = node.Properties.Add(resource.ResourceType, null);
					}
					property.Value = resource.Ticket;
					switch(resource.ResourceType)
					{
						case "MediaAudio":
							property = node.Properties.FirstOrDefault(x =>
								x.Name == "IconAudio");
							if(property != null)
							{
								node.Properties.Remove(property);
							}
							CreateAudioIcon(node);
							break;
						case "MediaImage":
							property = node.Properties.FirstOrDefault(x =>
								x.Name == "ThumbImage");
							if(property != null)
							{
								node.Properties.Remove(property);
							}
							CreateImageThumbnail(node, resource);
							break;
						case "MediaLink":
							property = node.Properties.FirstOrDefault(x =>
								x.Name == "IconLink");
							if(property != null)
							{
								node.Properties.Remove(property);
							}
							CreateLinkIcon(node);
							break;
						case "MediaVideo":
							property = node.Properties.FirstOrDefault(x =>
								x.Name == "ThumbVideo");
							if(property != null)
							{
								node.Properties.Remove(property);
							}
							CreateVideoThumbnail(node, resource);
							break;
					}
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Attach the specified existing resource to the properties collection.
		/// </summary>
		/// <param name="properties">
		/// Reference to the properties collection to which a resource will be
		/// attached.
		/// </param>
		/// <param name="resourceTicket">
		/// Globally unique identification of the resource to attach.
		/// </param>
		/// <param name="createIcons">
		/// Value indicating whether to create icons and thumbnails.
		/// </param>
		public static void AttachResource(PropertyCollection properties,
			string resourceTicket, bool createIcons = false)
		{
			PropertyItem property = null;
			ResourceItem resource = null;

			if(properties?.Count > 0 && mNodeFileObject != null &&
				mNodeFileObject.Resources?.Count > 0 &&
				resourceTicket?.Length > 0)
			{
				resource = mNodeFileObject.Resources.FirstOrDefault(x =>
					x.Ticket.ToLower() == resourceTicket.ToLower());
				if(resource != null)
				{
					property = properties.FirstOrDefault(x =>
						x.Name == resource.ResourceType);
					if(property == null)
					{
						property = properties.Add(resource.ResourceType, null);
					}
					property.Value = resource.Ticket;
					switch(resource.ResourceType)
					{
						case "MediaAudio":
							property = properties.FirstOrDefault(x =>
								x.Name == "IconAudio");
							if(property != null)
							{
								properties.Remove(property);
							}
							if(createIcons)
							{
								CreateAudioIcon(properties);
							}
							break;
						case "MediaImage":
							property = properties.FirstOrDefault(x =>
								x.Name == "ThumbImage");
							if(property != null)
							{
								properties.Remove(property);
							}
							if(createIcons)
							{
								CreateImageThumbnail(properties, resource);
							}
							break;
						case "MediaLink":
							property = properties.FirstOrDefault(x =>
								x.Name == "IconLink");
							if(property != null)
							{
								properties.Remove(property);
							}
							if(createIcons)
							{
								CreateLinkIcon(properties);
							}
							break;
						case "MediaVideo":
							property = properties.FirstOrDefault(x =>
								x.Name == "ThumbVideo");
							if(property != null)
							{
								properties.Remove(property);
							}
							if(createIcons)
							{
								CreateVideoThumbnail(properties, resource);
							}
							break;
					}
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Attach the specified existing resource to the node.
		/// </summary>
		/// <param name="socket">
		/// Reference to the socket to which a resource will be attached.
		/// </param>
		/// <param name="resourceTicket">
		/// Globally unique identification of the resource to attach.
		/// </param>
		public static void AttachResource(SocketItem socket,
			string resourceTicket)
		{
			string prefResource = "";
			PropertyItem property = null;
			ResourceItem resource = null;

			if(socket != null && mNodeFileObject != null &&
				mNodeFileObject.Resources?.Count > 0 && resourceTicket?.Length > 0)
			{
				resource = mNodeFileObject.Resources.FirstOrDefault(x =>
					x.Ticket.ToLower() == resourceTicket.ToLower());
				if(resource != null)
				{
					property = socket.Properties.FirstOrDefault(x =>
						x.Name == resource.ResourceType);
					if(property == null)
					{
						property = socket.Properties.Add(resource.ResourceType, null);
					}
					property.Value = resource.Ticket;
					if(MediaExists(socket, "MediaImage"))
					{
						prefResource = "MediaImage";
					}
					else if(MediaExists(socket, "MediaVideo"))
					{
						prefResource = "MediaVideo";
					}
					else if(MediaExists(socket, "MediaAudio"))
					{
						prefResource = "MediaAudio";
					}
					else if(MediaExists(socket, "MediaLink"))
					{
						prefResource = "MediaLink";
					}
					if(prefResource == resource.ResourceType)
					{
						property = socket.Properties.FirstOrDefault(x =>
							x.Name == "Icon");
						if(property != null)
						{
							socket.Properties.Remove(property);
						}
						switch(resource.ResourceType)
						{
							case "MediaAudio":
								CreateAudioIcon(socket);
								break;
							case "MediaImage":
								CreateImageThumbnail(socket);
								break;
							case "MediaLink":
								CreateLinkIcon(socket);
								break;
							case "MediaVideo":
								CreateVideoThumbnail(socket);
								break;
						}
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* CommonMediaTypes																											*
		//*-----------------------------------------------------------------------*
		private static List<string> mCommonMediaTypes = new List<string>()
		{
			"MediaImage",
			"MediaLink",
			"MediaVideo",
			"MediaAudio"
		};
		/// <summary>
		/// Get an array of common media type names.
		/// </summary>
		public static List<string> CommonMediaTypes
		{
			get { return mCommonMediaTypes; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ConnectionExists																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether any of the sockets in the list are
		/// connected to one or more sockets in the specified node.
		/// </summary>
		/// <param name="sockets">
		/// Collection of sockets to inspect for connections to the specified
		/// node.
		/// </param>
		/// <param name="node">
		/// Reference to the node to be tested for incoming connection from
		/// one of the provided sockets.
		/// </param>
		/// <returns>
		/// True if any of the provided sockets have an outgoing connection to
		/// any of the input sockets on the specified node. Otherwise, false.
		/// </returns>
		private static bool ConnectionExists(List<SocketItem> sockets,
			NodeItem node)
		{
			bool result = false;
			foreach(SocketItem socket in sockets)
			{
				foreach(SocketItem connection in socket.Connections)
				{
					if(node.Sockets.Exists(x => x == connection))
					{
						result = true;
						break;
					}
				}
				if(result)
				{
					break;
				}
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a value indicating whether two nodes share a connection.
		/// </summary>
		/// <param name="node1">
		/// Reference to the node for which output nodes will be tested.
		/// </param>
		/// <param name="node2">
		/// Reference to the node for which input nodes will be tested.
		/// </param>
		/// <returns>
		/// True if a connection is found between any of the output sockets
		/// of node 1 and the input sockets of node 2.
		/// </returns>
		public static bool ConnectionExists(NodeItem node1, NodeItem node2)
		{
			bool result = false;
			List<SocketItem> sockets = null;

			if(node1 != null && node2 != null)
			{
				sockets =
					node1.Sockets.FindAll(x => x.SocketMode == SocketModeEnum.Output);
				result = ConnectionExists(sockets, node2);
				if(!result)
				{
					sockets =
						node2.Sockets.FindAll(x => x.SocketMode == SocketModeEnum.Output);
					result = ConnectionExists(sockets, node1);
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ConvertVideoToThumbnail																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert the specified source video to a single thumbnail image.
		/// </summary>
		/// <param name="sourcePath">
		/// The video source providing the image.
		/// </param>
		/// <param name="targetPath">
		/// The image target to be created.
		/// </param>
		/// <param name="time">
		/// Time at which to capture a frame from the image.
		/// </param>
		/// <returns></returns>
		public static async Task<bool> ConvertVideoToThumbnail(string sourcePath,
			string targetPath, TimeSpan time)
		{
			FileInfo appFile = null;
			string appPath = "";
			FileInfo fileSource = null;
			FileInfo fileTarget = null;
			Process process = null;
			ProcessStartInfo processInfo = null;
			bool result = false;
			StreamReader reader = null;
			int timeOut = 10000;

			if(sourcePath?.Length > 0 && targetPath?.Length > 0)
			{
				appPath =
					Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				appFile = new FileInfo(Path.Combine(appPath, "ffmpeg.exe"));
				if(appFile.Exists)
				{
					//	FFMPEG.exe was found.
					fileSource = new FileInfo(sourcePath);
					if(fileSource.Exists)
					{
						//	Source file is already expected to exist.
						//	Target file is expected not to exist.
						fileTarget = new FileInfo(targetPath);
						if(fileTarget.Exists)
						{
							try
							{
								fileTarget.Delete();
							}
							catch { }
						}
						if(fileTarget.Exists)
						{
							Debug.WriteLine($"Error deleting target file: {targetPath}.");
						}
						else
						{
							//	Go ahead with the conversion.
							try
							{
								processInfo = new ProcessStartInfo();
								processInfo.CreateNoWindow = true;
								processInfo.UseShellExecute = false;
								processInfo.RedirectStandardError = true;
								processInfo.RedirectStandardOutput = true;
								processInfo.FileName = $"\"{appFile.FullName}\"";
								processInfo.WindowStyle = ProcessWindowStyle.Hidden;
								processInfo.Arguments =
									$"-i \"{fileSource.FullName}\" " +
									$"-ss {time:hh:mm:ss.fff} -vframes 1 " +
									$"\"{fileTarget.FullName}\"";
								process = Process.Start(processInfo);
								////	Wait for the window to finish loading.
								//process.WaitForInputIdle();
								//	Wait for the process to end or time-out.
								await Task.Run(() => process.WaitForExit(timeOut));
								if(!process.HasExited)
								{
									//	The process is still running.
									if(process.Responding)
									{
										//	The process is still responding, so cancel and close.
										process.CloseMainWindow();
										Debug.WriteLine("Error: FFMPEG did not finish in time...");
									}
									else
									{
										//	The process is hung up. Force close.
										process.Kill();
										Debug.WriteLine(
											"Error: FFMPEG hanged the host application...");
									}
								}
								else
								{
									Debug.WriteLine("FFMPEG process has exited...");
								}
								reader = process.StandardOutput;
								Debug.WriteLine(reader.ReadToEnd());
								reader = process.StandardError;
								Debug.WriteLine(reader.ReadToEnd());
								//	Refresh the file.
								fileTarget = new FileInfo(fileTarget.FullName);
								if(fileTarget.Exists)
								{
									result = true;
									Debug.WriteLine("FFMPEG target file was created...");
								}
								else
								{
									Debug.WriteLine("FFMPEG target file was not created " +
										$"{fileTarget.FullName}");
								}
							}
							catch(Exception ex)
							{
								Debug.WriteLine(
									$"Error running FFMPEG: {ex.Message} at {ex.StackTrace}");
							}
						}
					}
				}
				else
				{
					Debug.WriteLine($"Error: FFMPEG.exe was not found in {appPath}.");
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* CreateAudioIcon																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a local instance of the audio icon for the provided node.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to be updated..
		/// </param>
		public static void CreateAudioIcon(NodeItem node)
		{
			PropertyItem property = null;
			string typeName = "";

			if(node != null)
			{
				property = node.Properties.FirstOrDefault(x => x.Name == "IconAudio");
				if(property == null)
				{
					property = new PropertyItem()
					{
						Name = "IconAudio",
						Static = false
					};
					node.Properties.Add(property);
				}
				//property.Value = ResourceLib.Audio32;
				typeName = ResourceLib.Audio32.GetType().Name.ToLower();
				switch(typeName)
				{
					case "bitmap":
						property.Value = ResourceLib.Audio32;
						break;
					case "byte[]":
						using(MemoryStream memoryStream =
							new MemoryStream(ResourceLib.Audio32))
						{
							property.Value = new Bitmap(memoryStream);
						}
						break;
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a local instance of the audio icon for the provided node.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to be updated..
		/// </param>
		public static void CreateAudioIcon(PropertyCollection properties)
		{
			PropertyItem property = null;
			string typeName = "";

			if(properties?.Count > 0)
			{
				property = properties.FirstOrDefault(x => x.Name == "IconAudio");
				if(property == null)
				{
					property = new PropertyItem()
					{
						Name = "IconAudio",
						Static = false
					};
					properties.Add(property);
				}
				//property.Value = ResourceLib.Audio32;
				typeName = ResourceLib.Audio32.GetType().Name.ToLower();
				switch(typeName)
				{
					case "bitmap":
						property.Value = ResourceLib.Audio32;
						break;
					case "byte[]":
						using(MemoryStream memoryStream =
							new MemoryStream(ResourceLib.Audio32))
						{
							property.Value = new Bitmap(memoryStream);
						}
						break;
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a local instance of the audio icon for the provided node.
		/// </summary>
		/// <param name="socket">
		/// Reference to the socket to be updated..
		/// </param>
		public static void CreateAudioIcon(SocketItem socket)
		{
			Bitmap bitmap = null;
			float iconWidth = ToFloat(ResourceLib.SocketMediaIconSize);
			PropertyItem property = null;
			string typeName = "";

			if(socket != null)
			{
				property = socket.Properties.FirstOrDefault(x => x.Name == "Icon");
				if(property == null)
				{
					property = new PropertyItem()
					{
						Name = "Icon",
						Static = false
					};
					socket.Properties.Add(property);
				}
				//property.Value = ResourceLib.Audio32;
				typeName = ResourceLib.Audio32.GetType().Name.ToLower();
				switch(typeName)
				{
					case "bitmap":
						property.Value = ResourceLib.Audio32;
						RefreshThumbnail(socket,
							"Icon", (Bitmap)property.Value, iconWidth);
						break;
					case "byte[]":
						using(MemoryStream memoryStream =
							new MemoryStream(ResourceLib.Audio32))
						{
							bitmap = new Bitmap(memoryStream);
							RefreshThumbnail(socket, "Icon", bitmap, iconWidth);
						}
						break;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* CreateAudioResource																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create or update an audio resource from the specified audio file.
		/// </summary>
		/// <param name="file">
		/// Reference to the audio file to be loaded.
		/// </param>
		/// <param name="relativeFilename">
		/// Relative filename of the media file to load.
		/// </param>
		/// <param name="embed">
		/// Value indicating whether the data of the file will be embedded directly
		/// into the resource record.
		/// </param>
		/// <returns>
		/// Globally unique identification of the resource created or updated.
		/// </returns>
		public static string CreateAudioResource(FileInfo file,
			string relativeFilename, bool embed)
		{
			ResourceItem resource = null;
			string result = "";

			if(file != null && mNodeFileObject != null &&
				mNodeFileObject.Resources != null)
			{
				resource = mNodeFileObject.Resources.FirstOrDefault(x =>
					x.RelativeFilename == relativeFilename);
				//	If the file was already loaded, update its content.
				//	Otherwise, add a new resource.
				if(resource == null)
				{
					resource = new ResourceItem();
					resource.RelativeFilename = relativeFilename;
					mNodeFileObject.Resources.Add(resource);
				}
				resource.AbsoluteFilename = file.FullName.Replace('\\', '/');
				resource.ResourceType = "MediaAudio";
				if(embed)
				{
					//	Embed file.
					resource.Uri = GetDataUri(file);
					resource.DataUriHeaderLength = GetDataUriHeaderLength(file);
				}
				else
				{
					//	Link to file.
					resource.Uri = relativeFilename;
				}
				result = resource.Ticket;
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create or update an audio resource from the specified audio file.
		/// </summary>
		/// <param name="node">
		/// Reference to the node from which the audio file will be referenced.
		/// </param>
		/// <param name="file">
		/// Reference to the audio file to be loaded.
		/// </param>
		/// <param name="relativeFilename">
		/// Relative filename of the media file to load.
		/// </param>
		/// <param name="embed">
		/// Value indicating whether the data of the file will be embedded directly
		/// into the resource record.
		/// </param>
		/// <returns>
		/// Globally unique identification of the resource created or updated.
		/// </returns>
		public static string CreateAudioResource(NodeItem node,
			FileInfo file, string relativeFilename, bool embed)
		{
			ResourceItem resource = null;
			string result = "";

			if(node != null && file != null && mNodeFileObject != null &&
				mNodeFileObject.Resources != null)
			{
				resource = mNodeFileObject.Resources.FirstOrDefault(x =>
					x.RelativeFilename == relativeFilename);
				//	If the file was already loaded, update its content.
				//	Otherwise, add a new resource.
				if(resource == null)
				{
					resource = new ResourceItem();
					resource.RelativeFilename = relativeFilename;
					mNodeFileObject.Resources.Add(resource);
				}
				resource.AbsoluteFilename = file.FullName.Replace('\\', '/');
				resource.ResourceType = "MediaAudio";
				if(embed)
				{
					//	Embed file.
					resource.Uri = GetDataUri(file);
					resource.DataUriHeaderLength = GetDataUriHeaderLength(file);
				}
				else
				{
					//	Link to file.
					resource.Uri = relativeFilename;
				}
				result = resource.Ticket;
				if(result?.Length > 0)
				{
					//	Assign the resource ticket to the node.
					node.Properties["MediaAudio"].Value = result;
				}
				CreateAudioIcon(node);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* CreateImageResource																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create or update an image resource from the specified image file.
		/// </summary>
		/// <param name="file">
		/// Reference to the image file to be loaded.
		/// </param>
		/// <param name="relativeFilename">
		/// Relative filename of the media file to load.
		/// </param>
		/// <param name="embed">
		/// Value indicating whether the data of the file will be embedded directly
		/// into the resource record.
		/// </param>
		/// <returns>
		/// Globally unique identification of the resource that was created or
		/// updated.
		/// </returns>
		public static string CreateImageResource(FileInfo file,
			string relativeFilename, bool embed)
		{
			ResourceItem resource = null;
			string result = "";

			if(file != null && mNodeFileObject != null &&
				mNodeFileObject.Resources != null)
			{
				resource = mNodeFileObject.Resources.FirstOrDefault(x =>
					x.RelativeFilename == relativeFilename);
				//	If the file was already loaded, update its content.
				//	Otherwise, add a new resource.
				if(resource == null)
				{
					resource = new ResourceItem();
					resource.RelativeFilename = relativeFilename;
					mNodeFileObject.Resources.Add(resource);
				}
				resource.AbsoluteFilename = file.FullName.Replace('\\', '/');
				resource.ResourceType = "MediaImage";
				if(embed)
				{
					//	Embed file.
					resource.Uri = GetDataUri(file);
					resource.DataUriHeaderLength = GetDataUriHeaderLength(file);
				}
				else
				{
					//	Link to file.
					resource.Uri = relativeFilename;
				}
				result = resource.Ticket;
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create or update an image resource from the specified image file.
		/// </summary>
		/// <param name="node">
		/// Reference to the node from which the image file will be referenced.
		/// </param>
		/// <param name="file">
		/// Reference to the image file to be loaded.
		/// </param>
		/// <param name="relativeFilename">
		/// Relative filename of the media file to load.
		/// </param>
		/// <param name="embed">
		/// Value indicating whether the data of the file will be embedded directly
		/// into the resource record.
		/// </param>
		/// <returns>
		/// Globally unique identification of the resource that was created or
		/// updated.
		/// </returns>
		public static string CreateImageResource(NodeItem node, FileInfo file,
			string relativeFilename, bool embed)
		{
			ResourceItem resource = null;
			string result = "";

			if(node != null && file != null && mNodeFileObject != null &&
				mNodeFileObject.Resources != null)
			{
				resource = mNodeFileObject.Resources.FirstOrDefault(x =>
					x.RelativeFilename == relativeFilename);
				//	If the file was already loaded, update its content.
				//	Otherwise, add a new resource.
				if(resource == null)
				{
					resource = new ResourceItem();
					resource.RelativeFilename = relativeFilename;
					mNodeFileObject.Resources.Add(resource);
				}
				resource.AbsoluteFilename = file.FullName.Replace('\\', '/');
				resource.ResourceType = "MediaImage";
				if(embed)
				{
					//	Embed file.
					resource.Uri = GetDataUri(file);
					resource.DataUriHeaderLength = GetDataUriHeaderLength(file);
				}
				else
				{
					//	Link to file.
					resource.Uri = relativeFilename;
				}
				result = resource.Ticket;
				if(result?.Length > 0)
				{
					//	Assign the resource ticket to the node.
					node.Properties["MediaImage"].Value = result;
				}
				CreateImageThumbnail(node);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* CreateImageThumbnail																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create an image thumbnail from the specified image data.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to be updated.
		/// </param>
		/// <param name="data">
		/// Bitmap data representing a prepared thumbnail.
		/// </param>
		public static void CreateImageThumbnail(NodeItem node, Bitmap data)
		{
			PropertyItem property = null;

			if(node != null && data != null)
			{
				property = node.Properties.FirstOrDefault(p => p.Name == "ThumbImage");
				if(property == null)
				{
					property = new PropertyItem()
					{
						Name = "ThumbImage",
						Static = false
					};
					node.Properties.Add(property);
				}
				property.Value = data;
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create an image thumbnail from the associated image resource.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to be updated.
		/// </param>
		public static void CreateImageThumbnail(NodeItem node)
		{
			Bitmap bitmap = null;
			ResourceItem resource = null;
			ResourceLiveItem resourceLive = null;
			float thumbWidth = 200f;

			if(node != null)
			{
				if(MediaExists(node, "MediaImage"))
				{
					//	Image is attached.
					resource = GetResource(node, "MediaImage");
					if(resource != null)
					{
						resourceLive = ResourceLiveItem.FromResourceItem(resource);
						if(resourceLive.Data is byte[] rData && rData.Length > 0)
						{
							try
							{
								using(MemoryStream stream = new MemoryStream(rData))
								{
									bitmap = new Bitmap(stream);
								}
							}
							catch { }
						}
						RefreshThumbnail(node, "ThumbImage", bitmap, thumbWidth);
					}
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create an image thumbnail from the associated image resource.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to be updated.
		/// </param>
		/// <param name="resource">
		/// Resource to represent.
		/// </param>
		public static void CreateImageThumbnail(NodeItem node,
			ResourceItem resource)
		{
			Bitmap bitmap = null;
			ResourceLiveItem resourceLive = null;
			float thumbWidth = 200f;

			if(node != null && resource != null)
			{
				resourceLive = ResourceLiveItem.FromResourceItem(resource);
				if(resourceLive.Data is byte[] rData && rData.Length > 0)
				{
					try
					{
						using(MemoryStream stream = new MemoryStream(rData))
						{
							bitmap = new Bitmap(stream);
						}
					}
					catch { }
				}
				RefreshThumbnail(node, "ThumbImage", bitmap, thumbWidth);
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create an image thumbnail from the associated image resource.
		/// </summary>
		/// <param name="properties">
		/// Reference to the property collection to be updated.
		/// </param>
		/// <param name="resource">
		/// Resource to represent.
		/// </param>
		public static void CreateImageThumbnail(PropertyCollection properties,
			ResourceItem resource)
		{
			Bitmap bitmap = null;
			ResourceLiveItem resourceLive = null;
			float thumbWidth = 200f;

			if(properties?.Count > 0 && resource != null)
			{
				resourceLive = ResourceLiveItem.FromResourceItem(resource);
				if(resourceLive.Data is byte[] rData && rData.Length > 0)
				{
					try
					{
						using(MemoryStream stream = new MemoryStream(rData))
						{
							bitmap = new Bitmap(stream);
						}
					}
					catch { }
				}
				RefreshThumbnail(properties, "ThumbImage", bitmap, thumbWidth);
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create an image thumbnail from the associated image resource.
		/// </summary>
		/// <param name="resource">
		/// Collection of resources to search.
		/// </param>
		/// <param name="width">
		/// Width of the thumbnail.
		/// </param>
		/// <param name="height">
		/// Height of the thumbnail.
		/// </param>
		/// <returns>
		/// Reference to the thumbnail bitmap created.
		/// </returns>
		public static Bitmap CreateImageThumbnail(ResourceItem resource,
			float width, float height)
		{
			Bitmap bitmap = null;
			RectangleF rectSource = RectangleF.Empty;
			RectangleF rectTarget = RectangleF.Empty;
			ResourceLiveItem resourceLive = null;
			float scale = 1f;
			Bitmap thumbnail = new Bitmap((int)width, (int)height);

			if(resource != null)
			{
				resourceLive = ResourceLiveItem.FromResourceItem(resource);
				if(resourceLive.Data is byte[] rData && rData.Length > 0)
				{
					try
					{
						using(MemoryStream stream = new MemoryStream(rData))
						{
							bitmap = new Bitmap(stream);
						}
					}
					catch { }
				}
				if(bitmap != null)
				{
					//	The original bitmap was reconstructed.
					rectSource = new RectangleF(0f, 0f,
						(float)bitmap.Width, (float)bitmap.Height);
					//	Draw on the thumbnail.
					using(Graphics g = Graphics.FromImage(thumbnail))
					{
						if(rectSource.Width >= rectSource.Height &&
							width >= height)
						{
							//	Landscape.
							scale = rectSource.Width / width;
						}
						else
						{
							//	Portrait.
							scale = rectSource.Height / height;
						}
						rectTarget = new RectangleF(0f, 0f,
							rectSource.Width / scale, rectSource.Height / scale);
						if(rectTarget.Width < width)
						{
							rectTarget.X = (width / 2f) - (rectTarget.Width / 2f);
						}
						else if(rectTarget.Height < height)
						{
							rectTarget.Y = (height / 2f) - (rectTarget.Height / 2f);
						}
						g.CompositingQuality =
							System.Drawing.Drawing2D.
							CompositingQuality.HighQuality;
						g.InterpolationMode =
							System.Drawing.Drawing2D.InterpolationMode.High;
						g.SmoothingMode =
							System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
						g.DrawImage(bitmap, rectTarget, rectSource, GraphicsUnit.Pixel);
					}
				}
			}
			return thumbnail;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create an image thumbnail from the associated image resource.
		/// </summary>
		/// <param name="socket">
		/// Reference to the socket to be updated.
		/// </param>
		public static void CreateImageThumbnail(SocketItem socket)
		{
			Bitmap bitmap = null;
			float iconWidth = ToFloat(ResourceLib.SocketMediaIconSize);
			ResourceItem resource = null;
			ResourceLiveItem resourceLive = null;

			if(socket != null)
			{
				if(MediaExists(socket, "MediaImage"))
				{
					//	Image is attached.
					resource = GetResource(socket, "MediaImage");
					if(resource != null)
					{
						resourceLive = ResourceLiveItem.FromResourceItem(resource);
						if(resourceLive.Data is byte[] rData && rData.Length > 0)
						{
							try
							{
								using(MemoryStream stream = new MemoryStream(rData))
								{
									bitmap = new Bitmap(stream);
								}
							}
							catch { }
						}
						RefreshThumbnail(socket, "Icon", bitmap, iconWidth);
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* CreateLinkIcon																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create or update the link icon on the specified node.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to which a link icon will be applied.
		/// </param>
		public static void CreateLinkIcon(NodeItem node)
		{
			PropertyItem property = null;
			string typeName = "";

			if(node != null)
			{
				property = node.Properties.FirstOrDefault(x => x.Name == "IconLink");
				if(property == null)
				{
					property = new PropertyItem()
					{
						Name = "IconLink",
						Static = false
					};
					node.Properties.Add(property);
				}
				typeName = ResourceLib.Link32.GetType().Name.ToLower();
				switch(typeName)
				{
					case "bitmap":
						property.Value = ResourceLib.Link32;
						break;
					case "byte[]":
						using(MemoryStream memoryStream =
							new MemoryStream(ResourceLib.Link32))
						{
							property.Value = new Bitmap(memoryStream);
						}
						break;
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create or update the link icon in the specified property collection.
		/// </summary>
		/// <param name="properties">
		/// Reference to the property collection in which a link icon will be
		/// placed.
		/// </param>
		public static void CreateLinkIcon(PropertyCollection properties)
		{
			PropertyItem property = null;
			string typeName = "";

			if(properties?.Count > 0)
			{
				property = properties.FirstOrDefault(x => x.Name == "IconLink");
				if(property == null)
				{
					property = new PropertyItem()
					{
						Name = "IconLink",
						Static = false
					};
					properties.Add(property);
				}
				typeName = ResourceLib.Link32.GetType().Name.ToLower();
				switch(typeName)
				{
					case "bitmap":
						property.Value = ResourceLib.Link32;
						break;
					case "byte[]":
						using(MemoryStream memoryStream =
							new MemoryStream(ResourceLib.Link32))
						{
							property.Value = new Bitmap(memoryStream);
						}
						break;
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create or update the link icon on the specified socket.
		/// </summary>
		/// <param name="socket">
		/// Reference to the socket to which a link icon will be applied.
		/// </param>
		public static void CreateLinkIcon(SocketItem socket)
		{
			Bitmap bitmap = null;
			float iconWidth = ToFloat(ResourceLib.SocketMediaIconSize);
			PropertyItem property = null;
			string typeName = "";

			if(socket != null)
			{
				property = socket.Properties.FirstOrDefault(x => x.Name == "Icon");
				if(property == null)
				{
					property = new PropertyItem()
					{
						Name = "Icon",
						Static = false
					};
					socket.Properties.Add(property);
				}
				typeName = ResourceLib.Link32.GetType().Name.ToLower();
				switch(typeName)
				{
					case "bitmap":
						property.Value = ResourceLib.Link32;
						RefreshThumbnail(socket,
							"Icon", (Bitmap)property.Value, iconWidth);
						break;
					case "byte[]":
						using(MemoryStream memoryStream =
							new MemoryStream(ResourceLib.Link32))
						{
							bitmap = new Bitmap(memoryStream);
							RefreshThumbnail(socket, "Icon", bitmap, iconWidth);
						}
						break;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* CreateLinkResource																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create or update a link resource.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to be updated.
		/// </param>
		/// <param name="uri">
		/// URI content to be stored.
		/// </param>
		/// <returns>
		/// Globally unique identifier of the updated or created resource.
		/// </returns>
		public static string CreateLinkResource(NodeItem node, string uri)
		{
			ResourceItem resource = null;
			string result = "";

			if(node != null && mNodeFileObject != null &&
				mNodeFileObject.Resources != null)
			{
				resource = mNodeFileObject.Resources.FirstOrDefault(x => x.Uri == uri);
				//	If the link was already loaded, update its content.
				//	Otherwise, add a new resource.
				if(resource == null)
				{
					resource = new ResourceItem();
					resource.Uri = uri;
					mNodeFileObject.Resources.Add(resource);
				}
				resource.ResourceType = "MediaLink";
				result = resource.Ticket;
				if(result?.Length > 0)
				{
					//	Assign the resource ticket to the node.
					node.Properties["MediaLink"].Value = result;
				}
				CreateLinkIcon(node);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* CreateVideoResource																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create or update a video resource.
		/// </summary>
		/// <param name="file">
		/// Reference to the file containing the raw video data.
		/// </param>
		/// <param name="relativeFilename">
		/// Relative filename of the media file to load.
		/// </param>
		/// <param name="embed">
		/// Value indicating whether the data of the file will be embedded directly
		/// into the resource record.
		/// </param>
		/// <returns>
		/// The globally unique identifier of the new or retrieved resource.
		/// </returns>
		public static string CreateVideoResource(FileInfo file,
			string relativeFilename, bool embed)
		{
			ResourceItem resource = null;
			string result = "";

			if(file != null && mNodeFileObject != null &&
				mNodeFileObject.Resources != null)
			{
				resource = mNodeFileObject.Resources.FirstOrDefault(x =>
					x.RelativeFilename == relativeFilename);
				//	If the file was already loaded, update its content.
				//	Otherwise, add a new resource.
				if(resource == null)
				{
					resource = new ResourceItem();
					resource.RelativeFilename = relativeFilename;
					mNodeFileObject.Resources.Add(resource);
				}
				resource.AbsoluteFilename = file.FullName.Replace('\\', '/');
				resource.ResourceType = "MediaVideo";
				if(embed)
				{
					//	Embed file.
					resource.Uri = GetDataUri(file);
					resource.DataUriHeaderLength = GetDataUriHeaderLength(file);
				}
				else
				{
					//	Link to file.
					resource.Uri = relativeFilename;
				}
				result = resource.Ticket;
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create or update a video resource.
		/// </summary>
		/// <param name="node">
		/// Reference to the node that will refer to the resource.
		/// </param>
		/// <param name="file">
		/// Reference to the file containing the raw video data.
		/// </param>
		/// <param name="relativeFilename">
		/// Relative filename of the media file to load.
		/// </param>
		/// <param name="embed">
		/// Value indicating whether the data of the file will be embedded directly
		/// into the resource record.
		/// </param>
		/// <returns>
		/// The globally unique identifier of the new or retrieved resource.
		/// </returns>
		public static string CreateVideoResource(NodeItem node, FileInfo file,
			string relativeFilename, bool embed)
		{
			ResourceItem resource = null;
			string result = "";

			if(node != null && file != null && mNodeFileObject != null &&
				mNodeFileObject.Resources != null)
			{
				resource = mNodeFileObject.Resources.FirstOrDefault(x =>
					x.RelativeFilename == relativeFilename);
				//	If the file was already loaded, update its content.
				//	Otherwise, add a new resource.
				if(resource == null)
				{
					resource = new ResourceItem();
					resource.RelativeFilename = relativeFilename;
					mNodeFileObject.Resources.Add(resource);
				}
				resource.AbsoluteFilename = file.FullName.Replace('\\', '/');
				resource.ResourceType = "MediaVideo";
				if(embed)
				{
					//	Embed file.
					resource.Uri = GetDataUri(file);
					resource.DataUriHeaderLength = GetDataUriHeaderLength(file);
				}
				else
				{
					//	Link to file.
					resource.Uri = relativeFilename;
				}
				result = resource.Ticket;
				if(result?.Length > 0)
				{
					//	Assign the resource ticket to the node.
					node.Properties["MediaVideo"].Value = result;
				}
				CreateVideoThumbnail(node);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* CreateVideoThumbnail																									*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a video thumbnail from the specified image data.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to be updated.
		/// </param>
		/// <param name="data">
		/// Bitmap data representing a prepared thumbnail.
		/// </param>
		public static void CreateVideoThumbnail(NodeItem node, Bitmap data)
		{
			PropertyItem property = null;

			if(node != null && data != null)
			{
				property = node.Properties.FirstOrDefault(p => p.Name == "ThumbVideo");
				if(property == null)
				{
					property = new PropertyItem()
					{
						Name = "ThumbVideo",
						Static = false
					};
					node.Properties.Add(property);
				}
				property.Value = data;
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a runtime video thumbnail from an attached video resource.
		/// </summary>
		/// <param name="node">
		/// Reference to the node item associated to a video resource.
		/// </param>
		public async static void CreateVideoThumbnail(NodeItem node)
		{
			Bitmap bitmap = null;
			byte[] data = null;
			string dataPath = "";
			string extension = "";
			ResourceItem resource = null;
			ResourceLiveItem resourceLive = null;
			//FileInfo thumbFile = null;
			string thumbPath = "";
			float thumbWidth = 200f;

			if(node != null)
			{
				if(MediaExists(node, "MediaVideo"))
				{
					//	Image is attached.
					resource = GetResource(node, "MediaVideo");
					if(resource != null)
					{
						resourceLive = ResourceLiveItem.FromResourceItem(resource);
						if(resourceLive.Data is byte[] rData && rData.Length > 0)
						{
							try
							{
								extension = MimeTypeExtension(resourceLive.MimeType);
								dataPath = Path.Combine(Path.GetTempPath(),
									Guid.NewGuid().ToString("D") + "." + extension);
								File.WriteAllBytes(dataPath, rData);
								thumbPath = Path.Combine(Path.GetTempPath(),
									Guid.NewGuid().ToString("D") + ".png");
								if(await ConvertVideoToThumbnail(dataPath, thumbPath,
									TimeSpan.FromSeconds(0.1)))
								{
									//	Thumbnail file was created.
									data = File.ReadAllBytes(thumbPath);
									using(MemoryStream stream = new MemoryStream(data))
									{
										bitmap = (Bitmap)Bitmap.FromStream(stream);
									}
									RefreshThumbnail(node, "ThumbVideo", bitmap, thumbWidth);
									File.Delete(thumbPath);
								}
								File.Delete(dataPath);
							}
							catch(Exception ex)
							{
								Debug.WriteLine(
									$"Error in CreateVideoThumbnail: {ex.Message}.");
							}
						}
					}
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a runtime video thumbnail from an attached video resource.
		/// </summary>
		/// <param name="node">
		/// Reference to the node item associated to a video resource.
		/// </param>
		/// <param name="resource">
		/// Reference to the resource to represent.
		/// </param>
		public async static void CreateVideoThumbnail(NodeItem node,
			ResourceItem resource)
		{
			Bitmap bitmap = null;
			byte[] data = null;
			string dataPath = "";
			string extension = "";
			ResourceLiveItem resourceLive = null;
			string thumbPath = "";
			float thumbWidth = 200f;

			if(node != null && resource != null)
			{
				resourceLive = ResourceLiveItem.FromResourceItem(resource);
				if(resourceLive.Data is byte[] rData && rData.Length > 0)
				{
					try
					{
						extension = MimeTypeExtension(resourceLive.MimeType);
						dataPath = Path.Combine(Path.GetTempPath(),
							Guid.NewGuid().ToString("D") + "." + extension);
						File.WriteAllBytes(dataPath, rData);
						thumbPath = Path.Combine(Path.GetTempPath(),
							Guid.NewGuid().ToString("D") + ".png");
						if(await ConvertVideoToThumbnail(dataPath, thumbPath,
							TimeSpan.FromSeconds(0.1)))
						{
							//	Thumbnail file was created.
							data = File.ReadAllBytes(thumbPath);
							using(MemoryStream stream = new MemoryStream(data))
							{
								bitmap = (Bitmap)Bitmap.FromStream(stream);
							}
							RefreshThumbnail(node, "ThumbVideo", bitmap, thumbWidth);
							File.Delete(thumbPath);
						}
						File.Delete(dataPath);
					}
					catch(Exception ex)
					{
						Debug.WriteLine(
							$"Error in CreateVideoThumbnail: {ex.Message}.");
					}
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a runtime video thumbnail from an attached video resource.
		/// </summary>
		/// <param name="properties">
		/// Reference to the property collection associated to a video resource.
		/// </param>
		/// <param name="resource">
		/// Reference to the resource to represent.
		/// </param>
		public async static void CreateVideoThumbnail(
			PropertyCollection properties, ResourceItem resource)
		{
			Bitmap bitmap = null;
			byte[] data = null;
			string dataPath = "";
			string extension = "";
			ResourceLiveItem resourceLive = null;
			string thumbPath = "";
			float thumbWidth = 200f;

			if(properties?.Count > 0 && resource != null)
			{
				resourceLive = ResourceLiveItem.FromResourceItem(resource);
				if(resourceLive.Data is byte[] rData && rData.Length > 0)
				{
					try
					{
						extension = MimeTypeExtension(resourceLive.MimeType);
						dataPath = Path.Combine(Path.GetTempPath(),
							Guid.NewGuid().ToString("D") + "." + extension);
						File.WriteAllBytes(dataPath, rData);
						thumbPath = Path.Combine(Path.GetTempPath(),
							Guid.NewGuid().ToString("D") + ".png");
						if(await ConvertVideoToThumbnail(dataPath, thumbPath,
							TimeSpan.FromSeconds(0.1)))
						{
							//	Thumbnail file was created.
							data = File.ReadAllBytes(thumbPath);
							using(MemoryStream stream = new MemoryStream(data))
							{
								bitmap = (Bitmap)Bitmap.FromStream(stream);
							}
							RefreshThumbnail(properties, "ThumbVideo", bitmap, thumbWidth);
							File.Delete(thumbPath);
						}
						File.Delete(dataPath);
					}
					catch(Exception ex)
					{
						Debug.WriteLine(
							$"Error in CreateVideoThumbnail: {ex.Message}.");
					}
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a video thumbnail from the associated image resource.
		/// </summary>
		/// <param name="resource">
		/// Collection of resources to search.
		/// </param>
		/// <param name="width">
		/// Width of the thumbnail.
		/// </param>
		/// <param name="height">
		/// Height of the thumbnail.
		/// </param>
		/// <returns>
		/// Reference to the thumbnail bitmap created.
		/// </returns>
		public async static Task<Bitmap> CreateVideoThumbnail(
			ResourceItem resource, float width, float height)
		{
			Bitmap bitmap = null;
			byte[] data = null;
			string dataPath = "";
			string extension = "";
			RectangleF rectSource = RectangleF.Empty;
			RectangleF rectTarget = RectangleF.Empty;
			ResourceLiveItem resourceLive = null;
			float scale = 1f;
			Bitmap thumbnail = new Bitmap((int)width, (int)height);
			string thumbPath = "";

			if(resource != null)
			{
				resourceLive = ResourceLiveItem.FromResourceItem(resource);
				if(resourceLive.Data is byte[] rData && rData.Length > 0)
				{
					try
					{
						extension = MimeTypeExtension(resourceLive.MimeType);
						dataPath = Path.Combine(Path.GetTempPath(),
							Guid.NewGuid().ToString("D") + "." + extension);
						File.WriteAllBytes(dataPath, rData);
						thumbPath = Path.Combine(Path.GetTempPath(),
							Guid.NewGuid().ToString("D") + ".png");
						if(await ConvertVideoToThumbnail(dataPath, thumbPath,
							TimeSpan.FromSeconds(0.1)))
						{
							//	Thumbnail file was created.
							data = File.ReadAllBytes(thumbPath);
							using(MemoryStream stream = new MemoryStream(data))
							{
								bitmap = (Bitmap)Bitmap.FromStream(stream);
							}
							File.Delete(thumbPath);
						}
						File.Delete(dataPath);
					}
					catch(Exception ex)
					{
						Debug.WriteLine(
							$"Error in CreateVideoThumbnail: {ex.Message}.");
					}
				}
				if(bitmap != null)
				{
					//	The bitmap was reconstructed from a frame.
					rectSource = new RectangleF(0f, 0f,
						(float)bitmap.Width, (float)bitmap.Height);
					//	Draw on the thumbnail.
					using(Graphics g = Graphics.FromImage(thumbnail))
					{
						if(rectSource.Width >= rectSource.Height &&
							width >= height)
						{
							//	Landscape.
							scale = rectSource.Width / width;
						}
						else
						{
							//	Portrait.
							scale = rectSource.Height / height;
						}
						rectTarget = new RectangleF(0f, 0f,
							rectSource.Width / scale, rectSource.Height / scale);
						if(rectTarget.Width < width)
						{
							rectTarget.X = (width / 2f) - (rectTarget.Width / 2f);
						}
						else if(rectTarget.Height < height)
						{
							rectTarget.Y = (height / 2f) - (rectTarget.Height / 2f);
						}
						g.CompositingQuality =
							System.Drawing.Drawing2D.
							CompositingQuality.HighQuality;
						g.InterpolationMode =
							System.Drawing.Drawing2D.InterpolationMode.High;
						g.SmoothingMode =
							System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
						g.DrawImage(bitmap, rectTarget, rectSource, GraphicsUnit.Pixel);
					}
				}
			}
			return thumbnail;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a runtime video thumbnail from an attached video resource.
		/// </summary>
		/// <param name="socket">
		/// Reference to the socket item associated to a video resource.
		/// </param>
		public async static void CreateVideoThumbnail(SocketItem socket)
		{
			Bitmap bitmap = null;
			byte[] data = null;
			string dataPath = "";
			string extension = "";
			ResourceItem resource = null;
			ResourceLiveItem resourceLive = null;
			//FileInfo thumbFile = null;
			string thumbPath = "";
			float iconWidth = ToFloat(ResourceLib.SocketMediaIconSize);

			if(socket != null)
			{
				if(MediaExists(socket, "MediaVideo"))
				{
					//	Image is attached.
					resource = GetResource(socket, "MediaVideo");
					if(resource != null)
					{
						resourceLive = ResourceLiveItem.FromResourceItem(resource);
						if(resourceLive.Data is byte[] rData && rData.Length > 0)
						{
							try
							{
								extension = MimeTypeExtension(resourceLive.MimeType);
								dataPath = Path.Combine(Path.GetTempPath(),
									Guid.NewGuid().ToString("D") + "." + extension);
								File.WriteAllBytes(dataPath, rData);
								thumbPath = Path.Combine(Path.GetTempPath(),
									Guid.NewGuid().ToString("D") + ".png");
								if(await ConvertVideoToThumbnail(dataPath, thumbPath,
									TimeSpan.FromSeconds(0.1)))
								{
									//	Thumbnail file was created.
									data = File.ReadAllBytes(thumbPath);
									using(MemoryStream stream = new MemoryStream(data))
									{
										bitmap = (Bitmap)Bitmap.FromStream(stream);
									}
									RefreshThumbnail(socket, "Icon", bitmap, iconWidth);
									File.Delete(thumbPath);
								}
								File.Delete(dataPath);
							}
							catch(Exception ex)
							{
								Debug.WriteLine(
									$"Error in CreateVideoThumbnail: {ex.Message}.");
							}
						}
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* DetachResourceByProperty																							*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Detach the resource referred to by the property name from the node.
		/// </summary>
		/// <param name="node">
		/// Reference to the node containing the resource to remove.
		/// </param>
		/// <param name="propertyName">
		/// Name of the property to remove.
		/// </param>
		public static void DetachResourceByProperty(NodeItem node,
			string propertyName)
		{
			PropertyItem property = null;
			string propertyType = "";

			if(node != null && propertyName?.Length > 0)
			{
				property = node.Properties.FirstOrDefault(x =>
					x.Name == propertyName);
				if(property != null)
				{
					propertyType = property.Name;
					node.Properties.Remove(property);
					property = null;
					switch(propertyType)
					{
						case "MediaAudio":
							property =
								node.Properties.FirstOrDefault(x => x.Name == "IconAudio");
							break;
						case "MediaImage":
							property =
								node.Properties.FirstOrDefault(x => x.Name == "ThumbImage");
							break;
						case "MediaLink":
							property =
								node.Properties.FirstOrDefault(x => x.Name == "IconLink");
							break;
						case "MediaVideo":
							property =
								node.Properties.FirstOrDefault(x => x.Name == "ThumbVideo");
							break;
					}
					if(property != null)
					{
						node.Properties.Remove(property);
					}
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Detach the resource referred to by the property name from the property
		/// collection.
		/// </summary>
		/// <param name="properties">
		/// Reference to the property collection containing the resource to remove.
		/// </param>
		/// <param name="propertyName">
		/// Name of the property to remove.
		/// </param>
		public static void DetachResourceByProperty(PropertyCollection properties,
			string propertyName)
		{
			PropertyItem property = null;
			string propertyType = "";

			if(properties?.Count > 0 && propertyName?.Length > 0)
			{
				property = properties.FirstOrDefault(x =>
					x.Name == propertyName);
				if(property != null)
				{
					propertyType = property.Name;
					properties.Remove(property);
					property = null;
					switch(propertyType)
					{
						case "MediaAudio":
							property =
								properties.FirstOrDefault(x => x.Name == "IconAudio");
							break;
						case "MediaImage":
							property =
								properties.FirstOrDefault(x => x.Name == "ThumbImage");
							break;
						case "MediaLink":
							property =
								properties.FirstOrDefault(x => x.Name == "IconLink");
							break;
						case "MediaVideo":
							property =
								properties.FirstOrDefault(x => x.Name == "ThumbVideo");
							break;
					}
					if(property != null)
					{
						properties.Remove(property);
					}
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Detach the resource referred to by the property name from the socket.
		/// </summary>
		/// <param name="socket">
		/// Reference to the socket containing the resource to remove.
		/// </param>
		/// <param name="propertyName">
		/// Name of the property to remove.
		/// </param>
		public static void DetachResourceByProperty(SocketItem socket,
			string propertyName)
		{
			PropertyItem property = null;
			string propertyType = "";

			if(socket != null && propertyName?.Length > 0)
			{
				property = socket.Properties.FirstOrDefault(x =>
					x.Name == propertyName);
				if(property != null)
				{
					propertyType = property.Name;
					socket.Properties.Remove(property);
					property = null;
					switch(propertyType)
					{
						case "MediaAudio":
							property =
								socket.Properties.FirstOrDefault(x => x.Name == "IconAudio");
							break;
						case "MediaImage":
							property =
								socket.Properties.FirstOrDefault(x => x.Name == "ThumbImage");
							break;
						case "MediaLink":
							property =
								socket.Properties.FirstOrDefault(x => x.Name == "IconLink");
							break;
						case "MediaVideo":
							property =
								socket.Properties.FirstOrDefault(x => x.Name == "ThumbVideo");
							break;
					}
					if(property != null)
					{
						socket.Properties.Remove(property);
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* DetachResourceByTicket																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Detach the resource referred to by the unique identifier from the node.
		/// </summary>
		/// <param name="node">
		/// Reference to the node containing the resource to remove.
		/// </param>
		/// <param name="ticket">
		/// Reference to the globally unique identifier of the resource to
		/// remove.
		/// </param>
		public static void DetachResourceByTicket(NodeItem node, string ticket)
		{
			PropertyItem property = null;
			string propertyType = "";

			if(node != null && ticket?.Length > 0)
			{
				property = node.Properties.FirstOrDefault(x =>
					x.StringValue().ToLower() == ticket.ToLower());
				if(property != null)
				{
					propertyType = property.Name;
					node.Properties.Remove(property);
					property = null;
					switch(propertyType)
					{
						case "MediaAudio":
							property =
								node.Properties.FirstOrDefault(x => x.Name == "IconAudio");
							break;
						case "MediaImage":
							property =
								node.Properties.FirstOrDefault(x => x.Name == "ThumbImage");
							break;
						case "MediaLink":
							property =
								node.Properties.FirstOrDefault(x => x.Name == "IconLink");
							break;
						case "MediaVideo":
							property =
								node.Properties.FirstOrDefault(x => x.Name == "ThumbVideo");
							break;
					}
					if(property != null)
					{
						node.Properties.Remove(property);
					}
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Detach the resource referred to by the unique identifier from the
		/// property collection.
		/// </summary>
		/// <param name="properties">
		/// Reference to the property collection containing the resource to remove.
		/// </param>
		/// <param name="ticket">
		/// Reference to the globally unique identifier of the resource to
		/// remove.
		/// </param>
		public static void DetachResourceByTicket(PropertyCollection properties,
			string ticket)
		{
			PropertyItem property = null;
			string propertyType = "";

			if(properties?.Count > 0 && ticket?.Length > 0)
			{
				property = properties.FirstOrDefault(x =>
					x.StringValue().ToLower() == ticket.ToLower());
				if(property != null)
				{
					propertyType = property.Name;
					properties.Remove(property);
					property = null;
					switch(propertyType)
					{
						case "MediaAudio":
							property =
								properties.FirstOrDefault(x => x.Name == "IconAudio");
							break;
						case "MediaImage":
							property =
								properties.FirstOrDefault(x => x.Name == "ThumbImage");
							break;
						case "MediaLink":
							property =
								properties.FirstOrDefault(x => x.Name == "IconLink");
							break;
						case "MediaVideo":
							property =
								properties.FirstOrDefault(x => x.Name == "ThumbVideo");
							break;
					}
					if(property != null)
					{
						properties.Remove(property);
					}
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Detach the resource referred to by the unique identifier from the
		/// socket.
		/// </summary>
		/// <param name="socket">
		/// Reference to the socket containing the resource to remove.
		/// </param>
		/// <param name="ticket">
		/// Reference to the globally unique identifier of the resource to
		/// remove.
		/// </param>
		public static void DetachResourceByTicket(SocketItem socket, string ticket)
		{
			PropertyItem property = null;
			string propertyType = "";

			if(socket != null && ticket?.Length > 0)
			{
				property = socket.Properties.FirstOrDefault(x =>
					x.StringValue().ToLower() == ticket.ToLower());
				if(property != null)
				{
					propertyType = property.Name;
					socket.Properties.Remove(property);
					property = null;
					switch(propertyType)
					{
						case "MediaAudio":
							property =
								socket.Properties.FirstOrDefault(x => x.Name == "IconAudio");
							break;
						case "MediaImage":
							property =
								socket.Properties.FirstOrDefault(x => x.Name == "ThumbImage");
							break;
						case "MediaLink":
							property =
								socket.Properties.FirstOrDefault(x => x.Name == "IconLink");
							break;
						case "MediaVideo":
							property =
								socket.Properties.FirstOrDefault(x => x.Name == "ThumbVideo");
							break;
					}
					if(property != null)
					{
						socket.Properties.Remove(property);
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	FromHex																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a system drawing color from the caller's HTML hex code.
		/// </summary>
		/// <param name="hexColor">
		/// Hex color string to parse.
		/// </param>
		/// <returns>
		/// System drawing color equal to the specified string.
		/// </returns>
		public static Color FromHex(string hexColor)
		{
			int alpha = 255;
			int blue = 0;
			int green = 0;
			int red = 0;
			Color result = Color.Empty;

			if(hexColor?.Length >= 7 && hexColor.StartsWith("#"))
			{
				red = Convert.ToInt32(hexColor.Substring(1, 2), 16);
				green = Convert.ToInt32(hexColor.Substring(3, 2), 16);
				blue = Convert.ToInt32(hexColor.Substring(5, 2), 16);
				if(hexColor.Length >= 9)
				{
					alpha = Convert.ToInt32(hexColor.Substring(7, 2), 16);
				}
				result = Color.FromArgb(alpha, red, green, blue);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetDataUri																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a reference to a Data URI created from the specified binary
		/// file.
		/// </summary>
		/// <param name="file">
		/// Reference to a file to load.
		/// </param>
		/// <returns>
		/// Fully prepared data URI.
		/// </returns>
		public static string GetDataUri(FileInfo file)
		{
			byte[] buffer = null;
			StringBuilder builder = new StringBuilder();

			if(file?.Exists == true)
			{
				buffer = File.ReadAllBytes(file.FullName);
				builder.Append("data:");
				builder.Append(MimeType(file.Extension));
				builder.Append(";base64,");
				builder.Append(Convert.ToBase64String(buffer));
			}
			return builder.ToString();
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a reference to a Data URI created from the provided binary data
		/// loaded in association with a named file.
		/// </summary>
		/// <param name="extension">
		/// File extension used to establish the MIME type.
		/// </param>
		/// <param name="data">
		/// Binary data to convert.
		/// </param>
		/// <returns>
		/// </returns>
		public static string GetDataUri(string extension, byte[] data)
		{
			StringBuilder builder = new StringBuilder();

			if(data?.Length > 0)
			{
				builder.Append("data:");
				builder.Append(MimeType(extension));
				builder.Append(";base64,");
				builder.Append(Convert.ToBase64String(data));
			}
			return builder.ToString();
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetDataUriHeaderLength																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the length of the data uri header.
		/// </summary>
		/// <param name="file">
		/// Reference to information of the file to inspect.
		/// </param>
		/// <returns>
		/// Count of characters in length occupied by the Data URI header.
		/// </returns>
		public static int GetDataUriHeaderLength(FileInfo file)
		{
			int result = 13;

			if(file != null)
			{
				result += MimeType(file.Extension).Length;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetEffectivePath																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the effective path of the Uri, given its pattern and specified
		/// active base path.
		/// </summary>
		/// <param name="uri">
		/// URI to inspect.
		/// </param>
		/// <returns>
		/// Full effective path to the referenced resource.
		/// </returns>
		public static string GetEffectivePath(string uri)
		{
			string result = "";

			if(uri?.Length > 0)
			{
				if(new string[] { "\\\\", "//", ":" }.ToList().
					Exists(x => uri.IndexOf(x) > -1))
				{
					//	The URI is a full path.
					result = uri;
				}
				else if(ResourceCollection.BasePath?.Length > 0)
				{
					//	Create a full path.
					result = Path.Combine(ResourceCollection.BasePath, uri);
				}
				else
				{
					//	No base path was provided.
					result = uri;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetResource																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a reference to the resource found by reference in the specified
		/// property.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to which the property is attached.
		/// </param>
		/// <param name="mediaType">
		/// Name of the property to inspect.
		/// </param>
		/// <returns>
		/// Reference to the associated single instance resource, if found.
		/// Otherwise, null.
		/// </returns>
		public static ResourceItem GetResource(NodeItem node,
			MediaTypeEnum mediaType)
		{
			Match match = null;
			string propertyName = $"Media{mediaType}";
			ResourceItem result = null;
			string ticket = "";

			if(node != null && mNodeFileObject != null &&
				mNodeFileObject.Resources.Count > 0)
			{
				if(PropertyExists(node, propertyName))
				{
					ticket = node[propertyName].StringValue().ToLower();
					match = Regex.Match(ticket, ResourceLib.rxIsGUID);
					if(match.Success)
					{
						result =
							mNodeFileObject.Resources.FirstOrDefault(x =>
								x.Ticket.ToLower() == ticket);
					}
				}
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a reference to the resource found by reference in the specified
		/// property.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to which the property is attached.
		/// </param>
		/// <param name="propertyName">
		/// Name of the property to inspect.
		/// </param>
		/// <returns>
		/// Reference to the associated single instance resource, if found.
		/// Otherwise, null.
		/// </returns>
		public static ResourceItem GetResource(NodeItem node, string propertyName)
		{
			Match match = null;
			ResourceItem result = null;
			string ticket = "";

			if(node != null && mNodeFileObject != null &&
				mNodeFileObject.Resources.Count > 0 && propertyName?.Length > 0)
			{
				if(PropertyExists(node, propertyName))
				{
					ticket = node[propertyName].StringValue().ToLower();
					match = Regex.Match(ticket, ResourceLib.rxIsGUID);
					if(match.Success)
					{
						result =
							mNodeFileObject.Resources.FirstOrDefault(x =>
								x.Ticket.ToLower() == ticket);
					}
				}
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a reference to the resource found by reference in the specified
		/// property.
		/// </summary>
		/// <param name="properties">
		/// Reference to the properties to which the property is attached.
		/// </param>
		/// <param name="propertyName">
		/// Name of the property to inspect.
		/// </param>
		/// <returns>
		/// Reference to the associated single instance resource, if found.
		/// Otherwise, null.
		/// </returns>
		public static ResourceItem GetResource(PropertyCollection properties,
			string propertyName)
		{
			Match match = null;
			ResourceItem result = null;
			string ticket = "";

			if(properties != null && mNodeFileObject != null &&
				mNodeFileObject.Resources.Count > 0 && propertyName?.Length > 0)
			{
				if(PropertyExists(properties, propertyName))
				{
					ticket = properties[propertyName].StringValue().ToLower();
					match = Regex.Match(ticket, ResourceLib.rxIsGUID);
					if(match.Success)
					{
						result =
							mNodeFileObject.Resources.FirstOrDefault(x =>
								x.Ticket.ToLower() == ticket);
					}
				}
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a reference to the resource found by reference in the specified
		/// property.
		/// </summary>
		/// <param name="socket">
		/// Reference to the socket to which the property is attached.
		/// </param>
		/// <param name="mediaType">
		/// Name of the property to inspect.
		/// </param>
		/// <returns>
		/// Reference to the associated single instance resource, if found.
		/// Otherwise, null.
		/// </returns>
		public static ResourceItem GetResource(SocketItem socket,
			MediaTypeEnum mediaType)
		{
			Match match = null;
			string propertyName = $"Media{mediaType}";
			ResourceItem result = null;
			string ticket = "";

			if(socket != null && mNodeFileObject != null &&
				mNodeFileObject.Resources.Count > 0)
			{
				if(PropertyExists(socket, propertyName))
				{
					ticket = socket[propertyName].StringValue().ToLower();
					match = Regex.Match(ticket, ResourceLib.rxIsGUID);
					if(match.Success)
					{
						result =
							mNodeFileObject.Resources.FirstOrDefault(x =>
								x.Ticket.ToLower() == ticket);
					}
				}
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a reference to the resource found by reference in the specified
		/// property.
		/// </summary>
		/// <param name="socket">
		/// Reference to the socket to which the property is attached.
		/// </param>
		/// <param name="propertyName">
		/// Name of the property to inspect.
		/// </param>
		/// <returns>
		/// Reference to the associated single instance resource, if found.
		/// Otherwise, null.
		/// </returns>
		public static ResourceItem GetResource(SocketItem socket,
			string propertyName)
		{
			Match match = null;
			ResourceItem result = null;
			string ticket = "";

			if(socket != null && mNodeFileObject != null &&
				mNodeFileObject.Resources.Count > 0 && propertyName?.Length > 0)
			{
				if(PropertyExists(socket, propertyName))
				{
					ticket = socket[propertyName].StringValue().ToLower();
					match = Regex.Match(ticket, ResourceLib.rxIsGUID);
					if(match.Success)
					{
						result =
							mNodeFileObject.Resources.FirstOrDefault(x =>
								x.Ticket.ToLower() == ticket);
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetValue																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the value of the specified group member in the provided match.
		/// </summary>
		/// <param name="match">
		/// Reference to the match to be inspected.
		/// </param>
		/// <param name="groupName">
		/// Name of the group for which the value will be found.
		/// </param>
		/// <returns>
		/// The value found in the specified group, if found. Otherwise, empty
		/// string.
		/// </returns>
		public static string GetValue(Match match, string groupName)
		{
			string result = "";

			if(match != null && match.Groups[groupName] != null &&
				match.Groups[groupName].Value != null)
			{
				result = match.Groups[groupName].Value;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsBoolean																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the supplied value is boolean.
		/// </summary>
		/// <param name="value">
		/// The value to inspect.
		/// </param>
		/// <returns>
		/// Value indicating whether the supplied value was boolean.
		/// </returns>
		public static bool IsBoolean(object value)
		{
			bool result = false;

			if(value != null)
			{
				result = (mBoolChoices.FirstOrDefault(x =>
					x == value.ToString().ToLower()) != null);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsNumeric																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the entire value fits a recognizable
		/// numeric pattern.
		/// </summary>
		/// <param name="value">
		/// The value to inspect.
		/// </param>
		/// <returns>
		/// True if the value can be directly converted to a numeric format.
		/// Otherwise, false.
		/// </returns>
		public static bool IsNumeric(object value)
		{
			string comparison = "";
			Match match = null;
			bool result = false;

			if(value != null)
			{
				comparison = value.ToString();
				match = Regex.Match(comparison, ResourceLib.rxNumeric);
				if(match.Success &&
					GetValue(match, "pattern").Length == comparison.Length)
				{
					//	The entire string matches the pattern.
					result = true;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsNumericFloatingPoint																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the entire value fits a recognizable
		/// numeric floating point pattern.
		/// </summary>
		/// <param name="value">
		/// The value to inspect.
		/// </param>
		/// <returns>
		/// True if the value can be directly converted to a numeric format, and
		/// that format contains a decimal point. Otherwise, false. Note that
		/// an integer value will return false on this method.
		/// </returns>
		public static bool IsNumericFloatingPoint(object value)
		{
			string comparison = "";
			Match match = null;
			bool result = false;

			if(value != null)
			{
				comparison = value.ToString();
				match = Regex.Match(comparison, ResourceLib.rxNumeric);
				if(match.Success &&
					GetValue(match, "pattern").Length == comparison.Length &&
					match.Value.IndexOf(".") > -1)
				{
					//	The entire string matches the floating point pattern.
					result = true;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsNumericScientific																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the entire value fits a recognizable
		/// numeric scientific notation pattern.
		/// </summary>
		/// <param name="value">
		/// The value to inspect.
		/// </param>
		/// <returns>
		/// True if the value can be directly converted to a numeric format, and
		/// that format contains scientific notation. Otherwise, false. Note that
		/// a simple integer or floating point value will return false on this
		/// method.
		/// </returns>
		public static bool IsNumericScientific(object value)
		{
			string comparison = "";
			Match match = null;
			bool result = false;

			if(value != null)
			{
				comparison = value.ToString();
				match = Regex.Match(comparison, ResourceLib.rxNumeric);
				if(match.Success &&
					GetValue(match, "pattern").Length == comparison.Length &&
					match.Value.ToLower().IndexOf("e") > -1)
				{
					//	The entire string matches the scientific notation pattern.
					result = true;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* MediaExists																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicaing whether the provided node has the specified
		/// resource.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to inspect.
		/// </param>
		/// <param name="mediaType">
		/// Type of media to test for.
		/// </param>
		/// <returns>
		/// True if the specified media exists on the provided node. Otherwise,
		/// false.
		/// </returns>
		public static bool MediaExists(NodeItem node, MediaTypeEnum mediaType)
		{
			ResourceItem resource = null;
			bool result = false;

			if(node != null)
			{
				resource =
					GetResource(node, $"Media{mediaType}");
				result = (resource != null);
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a value indicating whether the specified media exists on the
		/// provided node.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to test.
		/// </param>
		/// <param name="propertyName">
		/// Name of the property to check. If name is null or blank, any fully
		/// qualified media is counted as a match.
		/// </param>
		/// <returns>
		/// True if media type is found on the node. Otherwise, false.
		/// </returns>
		public static bool MediaExists(NodeItem node, string propertyName = null)
		{
			bool result = false;

			if(node != null && mNodeFileObject != null)
			{
				if(propertyName?.Length > 0)
				{
					//	Property was specified.
					if(PropertyExists(node, propertyName) &&
						mNodeFileObject.Resources.Exists(x => x.Ticket.ToLower() ==
						node[propertyName].StringValue().ToLower()))
					{
						//	Specific media found.
						result = true;
					}
				}
				else
				{
					//	Any media.
					foreach(string mediaType in CommonMediaTypes)
					{
						if(PropertyExists(node, mediaType) &&
							mNodeFileObject.Resources.Exists(x => x.Ticket.ToLower() ==
							node[mediaType].StringValue().ToLower()))
						{
							//	Specific media found.
							result = true;
							break;
						}
					}
				}
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a value indicating whether the specified media exists on the
		/// provided properties collection.
		/// </summary>
		/// <param name="properties">
		/// Reference to the property collection to inspect.
		/// </param>
		/// <param name="propertyName">
		/// Name of the property to check. If name is null or blank, any fully
		/// qualified media is counted as a match.
		/// </param>
		/// <returns>
		/// True if media type is found on the node. Otherwise, false.
		/// </returns>
		public static bool MediaExists(PropertyCollection properties,
			string propertyName = null)
		{
			bool result = false;

			if(properties?.Count > 0 && mNodeFileObject != null)
			{
				if(propertyName?.Length > 0)
				{
					//	Property was specified.
					if(PropertyExists(properties, propertyName) &&
						mNodeFileObject.Resources.Exists(x => x.Ticket.ToLower() ==
						properties[propertyName].StringValue().ToLower()))
					{
						//	Specific media found.
						result = true;
					}
				}
				else
				{
					//	Any media.
					foreach(string mediaType in CommonMediaTypes)
					{
						if(PropertyExists(properties, mediaType) &&
							mNodeFileObject.Resources.Exists(x => x.Ticket.ToLower() ==
							properties[mediaType].StringValue().ToLower()))
						{
							//	Specific media found.
							result = true;
							break;
						}
					}
				}
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a value indicaing whether the provided socket has the specified
		/// resource.
		/// </summary>
		/// <param name="socket">
		/// Reference to the socket to inspect.
		/// </param>
		/// <param name="mediaType">
		/// Type of media to test for.
		/// </param>
		/// <returns>
		/// True if the specified media exists on the provided socket. Otherwise,
		/// false.
		/// </returns>
		public static bool MediaExists(SocketItem socket, MediaTypeEnum mediaType)
		{
			ResourceItem resource = null;
			bool result = false;

			if(socket != null)
			{
				resource =
					GetResource(socket, $"Media{mediaType}");
				result = (resource != null);
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a value indicating whether the specified media exists on the
		/// provided node.
		/// </summary>
		/// <param name="socket">
		/// Reference to the socket to test.
		/// </param>
		/// <param name="propertyName">
		/// Name of the property to check. If name is null or blank, any fully
		/// qualified media is counted as a match.
		/// </param>
		/// <returns>
		/// True if media type is found on the node. Otherwise, false.
		/// </returns>
		public static bool MediaExists(SocketItem socket,
			string propertyName = null)
		{
			bool result = false;

			if(socket != null && mNodeFileObject != null)
			{
				if(propertyName?.Length > 0)
				{
					//	Property was specified.
					if(PropertyExists(socket, propertyName) &&
						mNodeFileObject.Resources.Exists(x => x.Ticket.ToLower() ==
						socket[propertyName].StringValue().ToLower()))
					{
						//	Specific media found.
						result = true;
					}
				}
				else
				{
					//	Any media.
					foreach(string mediaType in CommonMediaTypes)
					{
						if(PropertyExists(socket, mediaType) &&
							mNodeFileObject.Resources.Exists(x => x.Ticket.ToLower() ==
							socket[mediaType].StringValue().ToLower()))
						{
							//	Specific media found.
							result = true;
							break;
						}
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MimeType																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the MIME-type associated with the specified extension.
		/// </summary>
		/// <param name="extension">
		/// File extension to associate.
		/// </param>
		/// <returns>
		/// MIME-type associated with the specified file extension.
		/// </returns>
		public static string MimeType(string extension)
		{
			string result = "";

			if(extension?.Length > 0)
			{
				if(extension.StartsWith("."))
				{
					extension = extension.Substring(1);
				}
				extension = extension.ToLower();
				switch(extension)
				{
					case "3g2": //	3GPP2 audio/video container.
						result = "video/3gpp2";
						//	also, audio/3gpp2 for audio only.
						break;
					case "3gp": //	3GPP audio/video container.
						result = "video/3gpp";
						//	also, audio/3gpp for audio only.
						break;
					case "7z":  //	7-zip archive.
						result = "application/x-7z-compressed";
						break;
					case "aac": //	AAC audio.
						result = "audio/aac";
						break;
					case "abw": //	AbiWord document.
						result = "application/x-abiword";
						break;
					case "arc": //	Archive document (multiple files embedded).
						result = "application/x-freearc";
						break;
					case "avi": //	AVI: Audio Video Interleave.
						result = "video/x-msvideo";
						break;
					case "azw": //	Amazon Kindle eBook format.
						result = "application/vnd.amazon.ebook";
						break;
					case "bin": //	Any kind of binary data.
						result = "application/octet-stream";
						break;
					case "bmp": //	Windows OS/2 Bitmap Graphics.
						result = "image/bmp";
						break;
					case "bz":  //	BZip archive.
						result = "application/x-bzip";
						break;
					case "bz2": //	BZip2 archive.
						result = "application/x-bzip2";
						break;
					case "csh": //	C-Shell script.
						result = "application/x-csh";
						break;
					case "css": //	Cascading Style Sheets (CSS).
						result = "text/css";
						break;
					case "csv": //	Comma-separated values (CSV).
						result = "text/csv";
						break;
					case "doc": //	Microsoft Word (Legacy).
						result = "application/msword";
						break;
					case "docx":  //	Microsoft Word (OpenXML).
						result = "application/vnd.openxmlformats-officedocument." +
							"wordprocessingml.document";
						break;
					case "eot": //	MS Embedded OpenType fonts.
						result = "application/vnd.ms-fontobject";
						break;
					case "epub":  //	Electronic publication (EPUB).
						result = "application/epub+zip";
						break;
					case "gz":  //	GZip Compressed Archive.
						result = "application/gzip";
						break;
					case "gif": //	Graphics Interchange Format (GIF).
						result = "image/gif";
						break;
					case "htm": //	HyperText Markup Language (HTML).
					case "html":
						result = "text/html";
						break;
					case "ico": //	Icon format.
						result = "image/vnd.microsoft.icon";
						break;
					case "ics": //	iCalendar format.
						result = "text/calendar";
						break;
					case "jar": //	Java Archive (JAR).
						result = "application/java-archive";
						break;
					case "jpeg":  //	JPEG image.
					case "jpg":
						result = "image/jpeg";
						break;
					case "js":  //	JavaScript.
						result = "text/javascript";
						break;
					case "json":  //	JSON format.
						result = "application/json";
						break;
					case "jsonld":  //	JSON-LD format.
						result = "application/ld+json";
						break;
					case "mid": //	Musical Instrument Digital Interface (MIDI).
					case "midi":
						result = "audio/midi";
						//	also, audio/x-midi
						break;
					case "mjs": //	JavaScript module.
						result = "text/javascript";
						break;
					case "mp3": //	MP3 audio.
						result = "audio/mpeg";
						break;
					case "mp4": //	MP4 video.
						result = "video/mp4";
						break;
					case "mpeg":  //	MPEG Video.
						result = "video/mpeg";
						break;
					case "mpkg":  //	Apple Installer Package.
						result = "application/vnd.apple.installer+xml";
						break;
					case "odp": //	OpenDocument presentation document.
						result = "application/vnd.oasis.opendocument.presentation";
						break;
					case "ods": //	OpenDocument spreadsheet document.
						result = "application/vnd.oasis.opendocument.spreadsheet";
						break;
					case "odt": //	OpenDocument text document.
						result = "application/vnd.oasis.opendocument.text";
						break;
					case "oga": //	OGG audio.
						result = "audio/ogg";
						break;
					case "ogv": //	OGG video.
						result = "video/ogg";
						break;
					case "ogx": //	OGG.
						result = "application/ogg";
						break;
					case "opus":  //	Opus audio.
						result = "audio/opus";
						break;
					case "otf": //	OpenType font.
						result = "font/otf";
						break;
					case "png": //	Portable Network Graphics.
						result = "image/png";
						break;
					case "pdf": //	Adobe Portable Document Format (PDF).
						result = "application/pdf";
						break;
					case "php": //	Hypertext Preprocessor (Personal Home Page).
						result = "application/x-httpd-php";
						break;
					case "ppt": //	Microsoft PowerPoint (Legacy).
						result = "application/vnd.ms-powerpoint";
						break;
					case "pptx":  //	Microsoft PowerPoint (OpenXML).
						result = "application/vnd.openxmlformats-officedocument." +
							"presentationml.presentation";
						break;
					case "rar": //	RAR archive.
						result = "application/vnd.rar";
						break;
					case "rtf": //	Rich Text Format (RTF).
						result = "application/rtf";
						break;
					case "sh":  //	Bourne shell script.
						result = "application/x-sh";
						break;
					case "svg": //	Scalable Vector Graphics (SVG).
						result = "image/svg+xml";
						break;
					case "swf": //	Small web format (SWF) or Adobe Flash document.
						result = "application/x-shockwave-flash";
						break;
					case "tar": //	Tape Archive (TAR).
						result = "application/x-tar";
						break;
					case "tif": //	Tagged Image File Format (TIFF).
					case "tiff":
						result = "image/tiff";
						break;
					//case "ts":  //	MPEG transport stream.
					//	result = "video/mp2t";
					//	break;
					case "ts":  //	Typescript.
						result = "application/x-typescript";
						break;
					case "ttf": //	TrueType Font.
						result = "font/ttf";
						break;
					case "txt": //	Text, (generally ASCII or ISO 8859-n).
						result = "text/plain";
						break;
					case "vsd": //	Microsoft Visio.
						result = "application/vnd.visio";
						break;
					case "wav": //	Waveform Audio Format.
						result = "audio/wav";
						break;
					case "weba":  //	WEBM audio.
						result = "audio/webm";
						break;
					case "webm":  //	WEBM video.
						result = "video/webm";
						break;
					case "webp":  //	WEBP image.
						result = "image/webp";
						break;
					case "woff":  //	Web Open Font Format (WOFF).
						result = "font/woff";
						break;
					case "woff2": //	Web Open Font Format (WOFF).
						result = "font/woff2";
						break;
					case "xhtml": //	XHTML.
						result = "application/xhtml+xml";
						break;
					case "xls": //	Microsoft Excel (Legacy).
						result = "application/vnd.ms-excel";
						break;
					case "xlsx":  //	Microsoft Excel (OpenXML).
						result = "application/vnd.openxmlformats-officedocument." +
							"spreadsheetml.sheet";
						break;
					case "xml": //	XML.
						result = "application/xml";
						//	also, text/xml if readable by casual users
						break;
					case "xul": //	XUL.
						result = "application/vnd.mozilla.xul+xml";
						break;
					case "zip": //	ZIP archive.
						result = "application/zip";
						break;
					default:  //	Any unidentified format is binary data.
						result = "application/octet-stream";
						break;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* MimeTypeExtension																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a file extension corresponding to the specified MIME-type.
		/// </summary>
		/// <param name="mimeType">
		/// MIME-type to represent.
		/// </param>
		/// <returns>
		/// Closest recognized file extension for the specified MIME-type.
		/// </returns>
		public static string MimeTypeExtension(string mimeType)
		{
			string result = "";

			switch(mimeType)
			{
				case "video/3gpp2":
					//	3GPP2 audio/video container.
					result = "3g2";
					//	also, audio/3gpp2 for audio only.
					break;
				case "video/3gpp":
					//	3GPP audio/video container.
					result = "3gp";
					//	also, audio/3gpp for audio only.
					break;
				case "application/x-7z-compressed":
					//	7-zip archive.
					result = "7z";
					break;
				case "audio/aac":
					//	AAC audio.
					result = "aac";
					break;
				case "application/x-abiword":
					//	AbiWord document.
					result = "abw";
					break;
				case "application/x-freearc":
					//	Archive document (multiple files embedded).
					result = "arc";
					break;
				case "video/x-msvideo":
					//	AVI: Audio Video Interleave.
					result = "avi";
					break;
				case "application/vnd.amazon.ebook":
					//	Amazon Kindle eBook format.
					result = "azw";
					break;
				case "application/octet-stream":
					//	Any kind of binary data.
					result = "bin";
					break;
				case "image/bmp":
					//	Windows OS/2 Bitmap Graphics.
					result = "bmp";
					break;
				case "application/x-bzip":
					//	BZip archive.
					result = "bz";
					break;
				case "application/x-bzip2":
					//	BZip2 archive.
					result = "bz2";
					break;
				case "application/x-csh":
					//	C-Shell script.
					result = "csh";
					break;
				case "text/css":
					//	Cascading Style Sheets (CSS).
					result = "css";
					break;
				case "text/csv":
					//	Comma-separated values (CSV).
					result = "csv";
					break;
				case "application/msword":
					//	Microsoft Word (Legacy).
					result = "doc";
					break;
				case "application/vnd.openxmlformats-officedocument." +
						"wordprocessingml.document":
					//	Microsoft Word (OpenXML).
					result = "docx";
					break;
				case "application/vnd.ms-fontobject":
					//	MS Embedded OpenType fonts.
					result = "eot";
					break;
				case "application/epub+zip":
					//	Electronic publication (EPUB).
					result = "epub";
					break;
				case "application/gzip":
					//	GZip Compressed Archive.
					result = "gz";
					break;
				case "image/gif":
					//	Graphics Interchange Format (GIF).
					result = "gif";
					break;
				case "text/html":
					//	HyperText Markup Language (HTML).
					result = "html";
					break;
				case "image/vnd.microsoft.icon":
					//	Icon format.
					result = "ico";
					break;
				case "text/calendar":
					//	iCalendar format.
					result = "ics";
					break;
				case "application/java-archive":
					//	Java Archive (JAR).
					result = "jar";
					break;
				case "image/jpeg":
					//	JPEG image.
					result = "jpeg";
					break;
				case "text/javascript":
					//	JavaScript.
					result = "js";
					break;
				case "application/json":
					//	JSON format.
					result = "json";
					break;
				case "application/ld+json":
					//	JSON-LD format.
					result = "jsonld";
					break;
				case "audio/midi":
					//	Musical Instrument Digital Interface (MIDI).
					result = "mid";
					//	also, audio/x-midi
					break;
				//case "text/javascript":
				//	//	JavaScript module.
				//	result = "mjs";
				//break;
				case "audio/mpeg":
					//	MP3 audio.
					result = "mp3";
					break;
				case "video/mp4":
					//	MP4 video.
					result = "mp4";
					break;
				case "video/mpeg":  //	MPEG Video.
					result = "mpeg";
					break;
				case "application/vnd.apple.installer+xml":
					//	Apple Installer Package.
					result = "mpkg";
					break;
				case "application/vnd.oasis.opendocument.presentation":
					//	OpenDocument presentation document.
					result = "odp";
					break;
				case "application/vnd.oasis.opendocument.spreadsheet":
					//	OpenDocument spreadsheet document.
					result = "ods";
					break;
				case "application/vnd.oasis.opendocument.text":
					//	OpenDocument text document.
					result = "odt";
					break;
				case "audio/ogg":
					//	OGG audio.
					result = "oga";
					break;
				case "video/ogg":
					//	OGG video.
					result = "ogv";
					break;
				case "application/ogg":
					//	OGG.
					result = "ogx";
					break;
				case "audio/opus":
					//	Opus audio.
					result = "opus";
					break;
				case "font/otf":
					//	OpenType font.
					result = "otf";
					break;
				case "image/png":
					//	Portable Network Graphics.
					result = "png";
					break;
				case "application/pdf":
					//	Adobe Portable Document Format (PDF).
					result = "pdf";
					break;
				case "application/x-httpd-php":
					//	Hypertext Preprocessor (Personal Home Page).
					result = "php";
					break;
				case "application/vnd.ms-powerpoint":
					//	Microsoft PowerPoint (Legacy).
					result = "ppt";
					break;
				case "application/vnd.openxmlformats-officedocument." +
						"presentationml.presentation":
					//	Microsoft PowerPoint (OpenXML).
					result = "pptx";
					break;
				case "application/vnd.rar":
					//	RAR archive.
					result = "rar";
					break;
				case "application/rtf":
					//	Rich Text Format (RTF).
					result = "rtf";
					break;
				case "application/x-sh":
					//	Bourne shell script.
					result = "sh";
					break;
				case "image/svg+xml":
					//	Scalable Vector Graphics (SVG).
					result = "svg";
					break;
				case "application/x-shockwave-flash":
					//	Small web format (SWF) or Adobe Flash document.
					result = "swf";
					break;
				case "application/x-tar":
					//	Tape Archive (TAR).
					result = "tar";
					break;
				case "image/tiff":
					//	Tagged Image File Format (TIFF).
					result = "tiff";
					break;
				//case "ts":  //	MPEG transport stream.
				//	result = "video/mp2t";
				//	break;
				case "application/x-typescript":
					//	Typescript.
					result = "ts";
					break;
				case "font/ttf":
					//	TrueType Font.
					result = "ttf";
					break;
				case "text/plain":
					//	Text, (generally ASCII or ISO 8859-n).
					result = "txt";
					break;
				case "application/vnd.visio":
					//	Microsoft Visio.
					result = "vsd";
					break;
				case "audio/wav":
					//	Waveform Audio Format.
					result = "wav";
					break;
				case "audio/webm":
					//	WEBM audio.
					result = "weba";
					break;
				case "video/webm":
					//	WEBM video.
					result = "webm";
					break;
				case "image/webp":
					//	WEBP image.
					result = "webp";
					break;
				case "font/woff":
					//	Web Open Font Format (WOFF).
					result = "woff";
					break;
				case "font/woff2":
					//	Web Open Font Format (WOFF).
					result = "woff2";
					break;
				case "application/xhtml+xml":
					//	XHTML.
					result = "xhtml";
					break;
				case "application/vnd.ms-excel":
					//	Microsoft Excel (Legacy).
					result = "xls";
					break;
				case "application/vnd.openxmlformats-officedocument." +
						"spreadsheetml.sheet":
					//	Microsoft Excel (OpenXML).
					result = "xlsx";
					break;
				case "application/xml":
					//	XML.
					result = "xml";
					//	also, text/xml if readable by casual users
					break;
				case "application/vnd.mozilla.xul+xml":
					//	XUL.
					result = "xul";
					break;
				case "application/zip":
					//	ZIP archive.
					result = "zip";
					break;
				default:
					//	Any unidentified format is binary data.
					result = "bin";
					break;
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeFileInfo																													*
		//*-----------------------------------------------------------------------*
		private static FileInfo mNodeFileInfo = null;
		/// <summary>
		/// Get/Set a reference to the physical loaded node file info.
		/// </summary>
		public static FileInfo NodeFileInfo
		{
			get { return mNodeFileInfo; }
			set { mNodeFileInfo = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	NodeFileObject																												*
		//*-----------------------------------------------------------------------*
		private static NodeFileItem mNodeFileObject = new NodeFileItem();
		/// <summary>
		/// Get/Set a reference to the currently loaded node file object.
		/// </summary>
		public static NodeFileItem NodeFileObject
		{
			get { return mNodeFileObject; }
			set { mNodeFileObject = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* PropertyExists																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified property exists on
		/// a node.
		/// </summary>
		/// <param name="propertyName">
		/// Name of the property to search for.
		/// </param>
		/// <returns>
		/// True if the specified property exists. Otherwise, false.
		/// </returns>
		public static bool PropertyExists(NodeItem node, string propertyName)
		{
			PropertyItem item = null;
			List<PropertyInfo> objProperties = null;
			PropertyInfo property = null;
			bool result = false;

			if(node != null && propertyName?.Length > 0)
			{
				item = node.Properties.FirstOrDefault(x =>
					x.Name.ToLower() == propertyName.ToLower());
				if(item == null)
				{
					//	Not found in the properties collection. Check locally.
					objProperties = node.GetType().GetProperties().ToList();
					property = objProperties.FirstOrDefault(x => x.Name == propertyName);
					if(property != null)
					{
						//	Property found on node.
						result = true;
					}
				}
				else
				{
					//	Property found in collection.
					result = true;
				}
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a value indicating whether the specified property exists in the
		/// collection.
		/// </summary>
		/// <param name="propertyName">
		/// Name of the property to search for.
		/// </param>
		/// <returns>
		/// True if the specified property exists. Otherwise, false.
		/// </returns>
		public static bool PropertyExists(PropertyCollection properties,
			string propertyName)
		{
			PropertyItem item = null;
			bool result = false;

			if(properties != null && propertyName?.Length > 0)
			{
				item = properties.FirstOrDefault(x =>
					x.Name.ToLower() == propertyName.ToLower());
				result = (item != null);
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return a value indicating whether the specified property exists on
		/// a socket.
		/// </summary>
		/// <param name="propertyName">
		/// Name of the property to search for.
		/// </param>
		/// <returns>
		/// True if the specified property exists. Otherwise, false.
		/// </returns>
		public static bool PropertyExists(SocketItem socket, string propertyName)
		{
			PropertyItem item = null;
			List<PropertyInfo> objProperties = null;
			PropertyInfo property = null;
			bool result = false;

			if(socket != null && propertyName?.Length > 0)
			{
				item = socket.Properties.FirstOrDefault(x =>
					x.Name.ToLower() == propertyName.ToLower());
				if(item == null)
				{
					//	Not found in the properties collection. Check locally.
					objProperties = socket.GetType().GetProperties().ToList();
					property = objProperties.FirstOrDefault(x => x.Name == propertyName);
					if(property != null)
					{
						//	Property found on node.
						result = true;
					}
				}
				else
				{
					//	Property found in collection.
					result = true;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RefreshThumbnail																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Post or update a runtime thumbnail of the provided image on the
		/// specified node property.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to update.
		/// </param>
		/// <param name="propertyName">
		/// Name of the node property to update.
		/// </param>
		/// <param name="bitmap">
		/// Reference to the image to receive thumbnail.
		/// </param>
		/// <param name="thumbWidth">
		/// Maximum width of the thumbnail.
		/// </param>
		/// <remarks>
		/// The property created here is non-permanent (Static = false), and will
		/// not be saved with other node properties.
		/// </remarks>
		public static void RefreshThumbnail(NodeItem node, string propertyName,
			Bitmap bitmap, float thumbWidth)
		{
			float height = 0f;
			PropertyItem property = null;
			float scale = 0f;
			Bitmap thumbnail = null;
			float width = 0f;

			if(bitmap != null)
			{
				width = (float)bitmap.Width;
				height = (float)bitmap.Height;
				if(width != 0f && height != 0f)
				{
					//	A thumbnail will be generated.
					if(width > thumbWidth)
					{
						scale = width / thumbWidth;
					}
					else
					{
						scale = 1f;
					}
					thumbnail = new Bitmap(
						(int)(width / scale), (int)(height / scale));
					using(Graphics g = Graphics.FromImage(thumbnail))
					{
						g.CompositingQuality =
							System.Drawing.Drawing2D.
							CompositingQuality.HighQuality;
						g.InterpolationMode =
							System.Drawing.Drawing2D.InterpolationMode.High;
						g.SmoothingMode =
							System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
						g.DrawImage(bitmap,
							new RectangleF(0f, 0f, width / scale, height / scale),
							new RectangleF(0f, 0f, width, height),
							GraphicsUnit.Pixel);
					}
					property = node.Properties.FirstOrDefault(x =>
						x.Name == propertyName);
					if(property == null)
					{
						property = new PropertyItem()
						{
							Name = propertyName,
							Static = false
						};
						node.Properties.Add(property);
					}
					property.Value = thumbnail;
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Post or update a runtime thumbnail of the provided image on the
		/// specified property collection.
		/// </summary>
		/// <param name="properties">
		/// Reference to the property collection to update.
		/// </param>
		/// <param name="propertyName">
		/// Name of the node property to update.
		/// </param>
		/// <param name="bitmap">
		/// Reference to the image to receive thumbnail.
		/// </param>
		/// <param name="thumbWidth">
		/// Maximum width of the thumbnail.
		/// </param>
		/// <remarks>
		/// The property created here is non-permanent (Static = false), and will
		/// not be saved with other node properties.
		/// </remarks>
		public static void RefreshThumbnail(PropertyCollection properties,
			string propertyName, Bitmap bitmap, float thumbWidth)
		{
			float height = 0f;
			PropertyItem property = null;
			float scale = 0f;
			Bitmap thumbnail = null;
			float width = 0f;

			if(bitmap != null)
			{
				width = (float)bitmap.Width;
				height = (float)bitmap.Height;
				if(width != 0f && height != 0f)
				{
					//	A thumbnail will be generated.
					if(width > thumbWidth)
					{
						scale = width / thumbWidth;
					}
					else
					{
						scale = 1f;
					}
					thumbnail = new Bitmap(
						(int)(width / scale), (int)(height / scale));
					using(Graphics g = Graphics.FromImage(thumbnail))
					{
						g.CompositingQuality =
							System.Drawing.Drawing2D.
							CompositingQuality.HighQuality;
						g.InterpolationMode =
							System.Drawing.Drawing2D.InterpolationMode.High;
						g.SmoothingMode =
							System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
						g.DrawImage(bitmap,
							new RectangleF(0f, 0f, width / scale, height / scale),
							new RectangleF(0f, 0f, width, height),
							GraphicsUnit.Pixel);
					}
					property = properties.FirstOrDefault(x =>
						x.Name == propertyName);
					if(property == null)
					{
						property = new PropertyItem()
						{
							Name = propertyName,
							Static = false
						};
						properties.Add(property);
					}
					property.Value = thumbnail;
				}
			}
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Post or update a runtime thumbnail of the provided image on the
		/// specified socket property.
		/// </summary>
		/// <param name="socket">
		/// Reference to the socket to update.
		/// </param>
		/// <param name="propertyName">
		/// Name of the node property to update.
		/// </param>
		/// <param name="bitmap">
		/// Reference to the image to receive thumbnail.
		/// </param>
		/// <param name="thumbWidth">
		/// Maximum width of the thumbnail.
		/// </param>
		/// <remarks>
		/// The property created here is non-permanent (Static = false), and will
		/// not be saved with other socket properties.
		/// </remarks>
		public static void RefreshThumbnail(SocketItem socket, string propertyName,
			Bitmap bitmap, float thumbWidth)
		{
			float height = 0f;
			PropertyItem property = null;
			float scale = 0f;
			Bitmap thumbnail = null;
			float width = 0f;

			if(bitmap != null)
			{
				width = (float)bitmap.Width;
				height = (float)bitmap.Height;
				if(width != 0f && height != 0f)
				{
					//	A thumbnail will be generated.
					if(width > thumbWidth)
					{
						scale = width / thumbWidth;
					}
					else
					{
						scale = 1f;
					}
					thumbnail = new Bitmap(
						(int)(width / scale), (int)(height / scale));
					using(Graphics g = Graphics.FromImage(thumbnail))
					{
						g.CompositingQuality =
							System.Drawing.Drawing2D.
							CompositingQuality.HighQuality;
						g.InterpolationMode =
							System.Drawing.Drawing2D.InterpolationMode.High;
						g.SmoothingMode =
							System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
						g.DrawImage(bitmap,
							new RectangleF(0f, 0f, width / scale, height / scale),
							new RectangleF(0f, 0f, width, height),
							GraphicsUnit.Pixel);
					}
					property = socket.Properties.FirstOrDefault(x =>
						x.Name == propertyName);
					if(property == null)
					{
						property = new PropertyItem()
						{
							Name = propertyName,
							Static = false
						};
						socket.Properties.Add(property);
					}
					property.Value = thumbnail;
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ToBoolean																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Provide fail-safe conversion of string to boolean value.
		/// </summary>
		/// <param name="value">
		/// Value to convert.
		/// </param>
		/// <returns>
		/// Boolean value. False if not convertible.
		/// </returns>
		public static bool ToBoolean(object value)
		{
			bool result = false;
			if(value != null)
			{
				result = ToBoolean(value.ToString());
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Provide fail-safe conversion of string to boolean value.
		/// </summary>
		/// <param name="value">
		/// Value to convert.
		/// </param>
		/// <returns>
		/// Boolean value. False if not convertible.
		/// </returns>
		public static bool ToBoolean(string value)
		{
			int count = 0;
			int index = 0;
			string lower = "";
			bool result = false;

			if(IsBoolean(value))
			{
				lower = value.ToLower();
				count = mBoolChoices.Length;
				for(index = 0; index < count; index++)
				{
					if(mBoolChoices[index] == lower)
					{
						//	Match found.
						if(index % 2 == 0)
						{
							result = true;
						}
						break;
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ToFloat																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Provide fail-safe conversion of string to numeric value.
		/// </summary>
		/// <param name="value">
		/// Value to convert.
		/// </param>
		/// <returns>
		/// Floating point value. 0 if not convertible.
		/// </returns>
		public static float ToFloat(object value)
		{
			float result = 0f;
			if(value != null)
			{
				result = ToFloat(value.ToString());
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Provide fail-safe conversion of string to numeric value.
		/// </summary>
		/// <param name="value">
		/// Value to convert.
		/// </param>
		/// <returns>
		/// Floating point value. 0 if not convertible.
		/// </returns>
		public static float ToFloat(string value)
		{
			float result = 0f;
			try
			{
				result = float.Parse(value);
			}
			catch { }
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ToHex																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Convert a color value to HTML hex.
		/// </summary>
		/// <param name="color">
		/// The System.Drawing.Color to inspect.
		/// </param>
		/// <param name="alpha">
		/// Value indicating whether to include alpha information (#RGBA).
		/// </param>
		/// <returns>
		/// HTML hex color value, if color was non-empty. Otherwise, empty string.
		/// </returns>
		public static string ToHex(Color color, bool alpha = false)
		{
			string result = "";

			if(!color.IsEmpty)
			{
				if(alpha)
				{
					result = $"#{color.R:X2}{color.G:X2}{color.B:X2}{color.A:X2}";
				}
				else
				{
					result = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ToInt																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Provide fail-safe conversion of string to numeric value.
		/// </summary>
		/// <param name="value">
		/// Value to convert.
		/// </param>
		/// <returns>
		/// Integer value. 0 if not convertible.
		/// </returns>
		public static int ToInt(object value)
		{
			int result = 0;
			if(value != null)
			{
				result = ToInt(value.ToString());
			}
			return result;
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Provide fail-safe conversion of string to numeric value.
		/// </summary>
		/// <param name="value">
		/// Value to convert.
		/// </param>
		/// <returns>
		/// Integer value. 0 if not convertible.
		/// </returns>
		public static int ToInt(string value)
		{
			//	A try .. catch block was originally implemented here, but the
			//	following text was being sent to output on each unsuccessful
			//	match.
			//	Exception thrown: 'System.FormatException' in mscorlib.dll
			int result = 0;
			int.TryParse(value, out result);
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ToString																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a non-null string representation of the specified object.
		/// </summary>
		/// <param name="value">
		/// Value to interpret.
		/// </param>
		/// <returns>
		/// String representation of the specified value, if converted
		/// successfully. Otherwise, an empty string.
		/// </returns>
		public static string ToString(object value)
		{
			string result = "";

			if(value != null)
			{
				result = value.ToString();
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* UpdateThumbnails																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Update all thumbnails on a node and its sockets.
		/// </summary>
		/// <param name="node">
		/// Reference to the node for which the thumbnails will be updated.
		/// </param>
		public static void UpdateThumbnails(NodeItem node)
		{
			List<string> iconNames = new List<string>()
			{
				"IconAudio",
				"ThumbImage",
				"IconLink",
				"ThumbVideo"
			};
			List<string> mediaNames = new List<string>()
			{
				"MediaAudio",
				"MediaImage",
				"MediaLink",
				"MediaVideo"
			};
			PropertyItem property = null;
			ResourceItem resource = null;


			if(node != null)
			{
				foreach(string iconName in iconNames)
				{
					property = node.Properties.FirstOrDefault(x => x.Name == iconName);
					if(property != null)
					{
						node.Properties.Remove(property);
					}
				}

				foreach(string mediaName in mediaNames)
				{
					property = node.Properties.FirstOrDefault(x => x.Name == mediaName);
					if(property != null)
					{
						//	Property is present.
						switch(mediaName)
						{
							case "MediaAudio":
								CreateAudioIcon(node);
								break;
							case "MediaImage":
								resource = mNodeFileObject.Resources.FirstOrDefault(x =>
									x.Ticket == property.StringValue());
								if(resource != null)
								{
									CreateImageThumbnail(node, resource);
								}
								break;
							case "MediaLink":
								CreateLinkIcon(node);
								break;
							case "MediaVideo":
								resource = mNodeFileObject.Resources.FirstOrDefault(x =>
									x.Ticket == property.StringValue());
								if(resource != null)
								{
									CreateVideoThumbnail(node, resource);
								}
								break;
						}
					}
				}
			}
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*
}
