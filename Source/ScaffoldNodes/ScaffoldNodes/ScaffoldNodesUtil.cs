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
		[DllImport("shlwapi.dll")]
		private static extern int ColorHLSToRGB(int H, int L, int S);

		private static string[] mBoolChoices =
			new string[] { "true", "false", "yes", "no", "1", "0" };

		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		////*-----------------------------------------------------------------------*
		////* AddMediaListItems																											*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Populate the specified media list with representations of the media
		///// properties in the collection.
		///// </summary>
		///// <param name="listControl">
		///// Reference to the target list view control.
		///// </param>
		///// <param name="imageControl">
		///// Reference to the associated icons and thumbnails image list.
		///// </param>
		///// <param name="resources">
		///// Collection of resources providing the media.
		///// </param>
		///// <param name="properties">
		///// Collection of properties to inspect for media entries.
		///// </param>
		//public static async void AddMediaListItems(ListView listControl,
		//	ImageList imageControl, ResourceCollection resources,
		//	PropertyCollection properties)
		//{
		//	int index = 0;
		//	ListViewItem item = null;
		//	ResourceItem resource = null;
		//	Bitmap thumbnail = null;

		//	if(MediaExists(properties, resources))
		//	{
		//		resource = GetResource(properties, resources, "MediaAudio");
		//		if(resource != null)
		//		{
		//			item = new ListViewItem(ResourceItem.Filename(resource), 0);
		//			item.Tag = resource.Ticket;
		//			item.Group = listControl.Groups["Audio"];
		//			listControl.Items.Add(item);
		//		}
		//		resource = GetResource(properties, resources, "MediaImage");
		//		if(resource != null)
		//		{
		//			thumbnail = CreateImageThumbnail(resource, 128, 128);
		//			index = imageControl.Images.Count;
		//			imageControl.Images.Add(thumbnail);
		//			item = new ListViewItem(ResourceItem.Filename(resource),
		//				index);
		//			item.Tag = resource.Ticket;
		//			item.Group = listControl.Groups["Image"];
		//			listControl.Items.Add(item);
		//		}
		//		resource = GetResource(properties, resources, "MediaLink");
		//		if(resource != null)
		//		{
		//			item = new ListViewItem(ResourceItem.Filename(resource), 1);
		//			item.Tag = resource.Ticket;
		//			item.Group = listControl.Groups["Link"];
		//			listControl.Items.Add(item);
		//		}
		//		resource = GetResource(properties, resources, "MediaVideo");
		//		if(resource != null)
		//		{
		//			thumbnail = await CreateVideoThumbnail(resource, 128, 128);
		//			index = imageControl.Images.Count;
		//			imageControl.Images.Add(thumbnail);
		//			item = new ListViewItem(ResourceItem.Filename(resource),
		//				index);
		//			item.Tag = resource.Ticket;
		//			item.Group = listControl.Groups["Video"];
		//			listControl.Items.Add(item);
		//		}
		//	}
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* AttachResource																												*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Attach the specified existing resource to the node.
		///// </summary>
		///// <param name="node">
		///// Reference to the node to which a resource will be attached.
		///// </param>
		///// <param name="resources">
		///// Collection of resources containing the asset to be attached.
		///// </param>
		///// <param name="resourceTicket">
		///// Globally unique identification of the resource to attach.
		///// </param>
		//public static void AttachResource(NodeItem node,
		//	ResourceCollection resources, string resourceTicket)
		//{
		//	PropertyItem property = null;
		//	ResourceItem resource = null;

		//	if(node != null && resources?.Count > 0 && resourceTicket?.Length > 0)
		//	{
		//		resource = resources.FirstOrDefault(x =>
		//			x.Ticket.ToLower() == resourceTicket.ToLower());
		//		if(resource != null)
		//		{
		//			property = node.Properties.FirstOrDefault(x =>
		//				x.Name == resource.ResourceType);
		//			if(property == null)
		//			{
		//				property = node.Properties.Add(resource.ResourceType, null);
		//			}
		//			property.Value = resource.Ticket;
		//			switch(resource.ResourceType)
		//			{
		//				case "MediaAudio":
		//					property = node.Properties.FirstOrDefault(x =>
		//						x.Name == "IconAudio");
		//					if(property != null)
		//					{
		//						node.Properties.Remove(property);
		//					}
		//					CreateAudioIcon(node);
		//					break;
		//				case "MediaImage":
		//					property = node.Properties.FirstOrDefault(x =>
		//						x.Name == "ThumbImage");
		//					if(property != null)
		//					{
		//						node.Properties.Remove(property);
		//					}
		//					CreateImageThumbnail(node, resource);
		//					break;
		//				case "MediaLink":
		//					property = node.Properties.FirstOrDefault(x =>
		//						x.Name == "IconLink");
		//					if(property != null)
		//					{
		//						node.Properties.Remove(property);
		//					}
		//					CreateLinkIcon(node);
		//					break;
		//				case "MediaVideo":
		//					property = node.Properties.FirstOrDefault(x =>
		//						x.Name == "ThumbVideo");
		//					if(property != null)
		//					{
		//						node.Properties.Remove(property);
		//					}
		//					CreateVideoThumbnail(node, resource);
		//					break;
		//			}
		//		}
		//	}
		//}
		////*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		///// <summary>
		///// Attach the specified existing resource to the properties collection.
		///// </summary>
		///// <param name="properties">
		///// Reference to the properties collection to which a resource will be
		///// attached.
		///// </param>
		///// <param name="resources">
		///// Collection of resources containing the asset to be attached.
		///// </param>
		///// <param name="resourceTicket">
		///// Globally unique identification of the resource to attach.
		///// </param>
		///// <param name="createIcons">
		///// Value indicating whether to create icons and thumbnails.
		///// </param>
		//public static void AttachResource(PropertyCollection properties,
		//	ResourceCollection resources, string resourceTicket,
		//	bool createIcons = false)
		//{
		//	PropertyItem property = null;
		//	ResourceItem resource = null;

		//	if(properties?.Count > 0 && resources?.Count > 0 &&
		//		resourceTicket?.Length > 0)
		//	{
		//		resource = resources.FirstOrDefault(x =>
		//			x.Ticket.ToLower() == resourceTicket.ToLower());
		//		if(resource != null)
		//		{
		//			property = properties.FirstOrDefault(x =>
		//				x.Name == resource.ResourceType);
		//			if(property == null)
		//			{
		//				property = properties.Add(resource.ResourceType, null);
		//			}
		//			property.Value = resource.Ticket;
		//			switch(resource.ResourceType)
		//			{
		//				case "MediaAudio":
		//					property = properties.FirstOrDefault(x =>
		//						x.Name == "IconAudio");
		//					if(property != null)
		//					{
		//						properties.Remove(property);
		//					}
		//					if(createIcons)
		//					{
		//						CreateAudioIcon(properties);
		//					}
		//					break;
		//				case "MediaImage":
		//					property = properties.FirstOrDefault(x =>
		//						x.Name == "ThumbImage");
		//					if(property != null)
		//					{
		//						properties.Remove(property);
		//					}
		//					if(createIcons)
		//					{
		//						CreateImageThumbnail(properties, resource);
		//					}
		//					break;
		//				case "MediaLink":
		//					property = properties.FirstOrDefault(x =>
		//						x.Name == "IconLink");
		//					if(property != null)
		//					{
		//						properties.Remove(property);
		//					}
		//					if(createIcons)
		//					{
		//						CreateLinkIcon(properties);
		//					}
		//					break;
		//				case "MediaVideo":
		//					property = properties.FirstOrDefault(x =>
		//						x.Name == "ThumbVideo");
		//					if(property != null)
		//					{
		//						properties.Remove(property);
		//					}
		//					if(createIcons)
		//					{
		//						CreateVideoThumbnail(properties, resource);
		//					}
		//					break;
		//			}
		//		}
		//	}
		//}
		////*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		///// <summary>
		///// Attach the specified existing resource to the node.
		///// </summary>
		///// <param name="socket">
		///// Reference to the socket to which a resource will be attached.
		///// </param>
		///// <param name="resources">
		///// Collection of resources containing the asset to be attached.
		///// </param>
		///// <param name="resourceTicket">
		///// Globally unique identification of the resource to attach.
		///// </param>
		//public static void AttachResource(SocketItem socket,
		//	ResourceCollection resources, string resourceTicket)
		//{
		//	PropertyItem property = null;
		//	ResourceItem resource = null;

		//	if(socket != null && resources?.Count > 0 && resourceTicket?.Length > 0)
		//	{
		//		resource = resources.FirstOrDefault(x =>
		//			x.Ticket.ToLower() == resourceTicket.ToLower());
		//		if(resource != null)
		//		{
		//			property = socket.Properties.FirstOrDefault(x =>
		//				x.Name == resource.ResourceType);
		//			if(property == null)
		//			{
		//				property = socket.Properties.Add(resource.ResourceType, null);
		//			}
		//			property.Value = resource.Ticket;
		//		}
		//	}
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	CenterOver																														*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Center the top form over the base form.
		///// </summary>
		///// <param name="baseForm">
		///// </param>
		///// <param name="topForm">
		///// </param>
		///// <returns>
		///// </returns>
		//public static Point CenterOver(Form baseForm, Form topForm)
		//{
		//	Point center = Point.Empty;
		//	Point result = Point.Empty;

		//	if(baseForm != null && topForm != null)
		//	{
		//		//	Both forms have dimensions.
		//		result = new Point(
		//			(baseForm.Width / 2),
		//			(baseForm.Height / 2));
		//		result = new Point(
		//			result.X - (topForm.Width / 2),
		//			result.Y - (topForm.Height / 2));
		//		result = baseForm.PointToScreen(result);
		//		topForm.StartPosition = FormStartPosition.Manual;
		//		if(result.X < 0)
		//		{
		//			result.X = 0;
		//		}
		//		if(result.Y < 0)
		//		{
		//			result.Y = 0;
		//		}
		//		if(result.X + topForm.Width > Screen.PrimaryScreen.Bounds.Width)
		//		{
		//			result.X = Screen.PrimaryScreen.Bounds.Width - topForm.Width;
		//		}
		//		if(result.Y + topForm.Height > Screen.PrimaryScreen.Bounds.Height)
		//		{
		//			result.Y = Screen.PrimaryScreen.Bounds.Height - topForm.Height;
		//		}
		//		topForm.Location = result;
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	ClipboardLoadFromResource																							*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Load the clipboard from an onboard binary resource.
		///// </summary>
		///// <param name="resourceName">
		///// Name of the resource to load.
		///// </param>
		///// <returns>
		///// True if the clipboard was loaded. Otherwise, false.
		///// </returns>
		//public static bool ClipboardLoadFromResource(string resourceName)
		//{
		//	byte[] buffer = new byte[0];
		//	DataObject dataobject = null;
		//	BinaryFormatter formatter = null;
		//	MemoryStream memory = null;
		//	NamedObjectCollection pages = null;
		//	PropertyInfo property = null;
		//	bool result = false;
		//	Type type = typeof(ResourceMain);

		//	property = type.GetProperty(resourceName,
		//		BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
		//	try
		//	{
		//		buffer = (byte[])property.GetValue(null, null);
		//		if(buffer.Length > 0)
		//		{
		//			try
		//			{
		//				memory = new MemoryStream(buffer);
		//				formatter = new BinaryFormatter();
		//				pages = (NamedObjectCollection)formatter.Deserialize(memory);
		//			}
		//			catch(Exception ex)
		//			{
		//				Console.WriteLine($"Could not deserialize clipboard: {ex.Message}",
		//					"Load Clipboard File");
		//			}
		//			finally
		//			{
		//				memory.Close();
		//				memory.Dispose();
		//			}
		//		}
		//		//	Prepare the clipboard.
		//		if(pages.Count > 0)
		//		{
		//			Clipboard.Clear();
		//			dataobject = new DataObject();
		//			foreach(NamedObjectItem page in pages)
		//			{
		//				dataobject.SetData(page.Name, false, page.Value);
		//			}
		//			Clipboard.SetDataObject(dataobject, true);
		//		}
		//	}
		//	catch { }
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

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
				property.Value = ResourceLib.Audio32;
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
				property.Value = ResourceLib.Audio32;
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
		/// <param name="resources">
		/// Collection of resources to which the audio will be sent.
		/// </param>
		/// <returns>
		/// Globally unique identification of the resource created or updated.
		/// </returns>
		public static string CreateAudioResource(FileInfo file,
			string relativeFilename, bool embed, ResourceCollection resources)
		{
			ResourceItem resource = null;
			string result = "";

			if(file != null && resources != null)
			{
				resource = resources.FirstOrDefault(x =>
					x.RelativeFilename == relativeFilename);
				//	If the file was already loaded, update its content.
				//	Otherwise, add a new resource.
				if(resource == null)
				{
					resource = new ResourceItem();
					resource.RelativeFilename = relativeFilename;
					resources.Add(resource);
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
		/// <param name="resources">
		/// Collection of resources to which the audio will be sent.
		/// </param>
		/// <returns>
		/// Globally unique identification of the resource created or updated.
		/// </returns>
		public static string CreateAudioResource(NodeItem node,
			FileInfo file, string relativeFilename, bool embed,
			ResourceCollection resources)
		{
			ResourceItem resource = null;
			string result = "";

			if(node != null && file != null && resources != null)
			{
				resource = resources.FirstOrDefault(x =>
					x.RelativeFilename == relativeFilename);
				//	If the file was already loaded, update its content.
				//	Otherwise, add a new resource.
				if(resource == null)
				{
					resource = new ResourceItem();
					resource.RelativeFilename = relativeFilename;
					resources.Add(resource);
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
		/// <param name="resources">
		/// Collection of resources to which the image will be sent.
		/// </param>
		/// <returns>
		/// Globally unique identification of the resource that was created or
		/// updated.
		/// </returns>
		public static string CreateImageResource(FileInfo file,
			string relativeFilename, bool embed, ResourceCollection resources)
		{
			ResourceItem resource = null;
			string result = "";

			if(file != null && resources != null)
			{
				resource = resources.FirstOrDefault(x =>
					x.RelativeFilename == relativeFilename);
				//	If the file was already loaded, update its content.
				//	Otherwise, add a new resource.
				if(resource == null)
				{
					resource = new ResourceItem();
					resource.RelativeFilename = relativeFilename;
					resources.Add(resource);
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
		/// <param name="resources">
		/// Collection of resources to which the image will be sent.
		/// </param>
		/// <returns>
		/// Globally unique identification of the resource that was created or
		/// updated.
		/// </returns>
		public static string CreateImageResource(NodeItem node, FileInfo file,
			string relativeFilename, bool embed, ResourceCollection resources)
		{
			ResourceItem resource = null;
			string result = "";

			if(node != null && file != null && resources != null)
			{
				resource = resources.FirstOrDefault(x =>
					x.RelativeFilename == relativeFilename);
				//	If the file was already loaded, update its content.
				//	Otherwise, add a new resource.
				if(resource == null)
				{
					resource = new ResourceItem();
					resource.RelativeFilename = relativeFilename;
					resources.Add(resource);
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
				CreateImageThumbnail(node, resources);
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
		/// <param name="resources">
		/// Collection of resources to search.
		/// </param>
		public static void CreateImageThumbnail(NodeItem node,
			ResourceCollection resources)
		{
			Bitmap bitmap = null;
			ResourceItem resource = null;
			ResourceLiveItem resourceLive = null;
			float thumbWidth = 200f;

			if(node != null)
			{
				if(MediaExists(node, resources, "MediaImage"))
				{
					//	Image is attached.
					resource = GetResource(node, resources, "MediaImage");
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
				property.Value = ResourceLib.Link32;
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
				property.Value = ResourceLib.Link32;
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
		/// <param name="resources">
		/// Collection of resources to which the link will be sent.
		/// </param>
		/// <returns>
		/// Globally unique identifier of the updated or created resource.
		/// </returns>
		public static string CreateLinkResource(NodeItem node, string uri,
			ResourceCollection resources)
		{
			ResourceItem resource = null;
			string result = "";

			if(node != null && resources != null)
			{
				resource = resources.FirstOrDefault(x => x.Uri == uri);
				//	If the link was already loaded, update its content.
				//	Otherwise, add a new resource.
				if(resource == null)
				{
					resource = new ResourceItem();
					resource.Uri = uri;
					resources.Add(resource);
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
		/// <param name="resources">
		/// Collection of resources to which the video will be sent.
		/// </param>
		/// <returns>
		/// The globally unique identifier of the new or retrieved resource.
		/// </returns>
		public static string CreateVideoResource(FileInfo file,
			string relativeFilename, bool embed, ResourceCollection resources)
		{
			ResourceItem resource = null;
			string result = "";

			if(file != null && resources != null)
			{
				resource = resources.FirstOrDefault(x =>
					x.RelativeFilename == relativeFilename);
				//	If the file was already loaded, update its content.
				//	Otherwise, add a new resource.
				if(resource == null)
				{
					resource = new ResourceItem();
					resource.RelativeFilename = relativeFilename;
					resources.Add(resource);
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
		/// <param name="resources">
		/// Collection of resources to which the video will be sent.
		/// </param>
		/// <returns>
		/// The globally unique identifier of the new or retrieved resource.
		/// </returns>
		public static string CreateVideoResource(NodeItem node, FileInfo file,
			string relativeFilename, bool embed, ResourceCollection resources)
		{
			ResourceItem resource = null;
			string result = "";

			if(node != null && file != null && resources != null)
			{
				resource = resources.FirstOrDefault(x =>
					x.RelativeFilename == relativeFilename);
				//	If the file was already loaded, update its content.
				//	Otherwise, add a new resource.
				if(resource == null)
				{
					resource = new ResourceItem();
					resource.RelativeFilename = relativeFilename;
					resources.Add(resource);
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
				CreateVideoThumbnail(node, resources);
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
		/// <param name="resources">
		/// Reference to a resource collection.
		/// </param>
		public async static void CreateVideoThumbnail(NodeItem node,
			ResourceCollection resources)
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
				if(MediaExists(node, resources, "MediaVideo"))
				{
					//	Image is attached.
					resource = GetResource(node, resources, "MediaVideo");
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
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* DeScaleDrawing																												*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Scale the coordinate to the specified factor and return the new
		///// coordinate to the caller.
		///// </summary>
		///// <param name="origin">
		///// Original point to scale.
		///// </param>
		///// <param name="scale">
		///// Scale factor.
		///// </param>
		///// <returns>
		///// Scaled coordinate.
		///// </returns>
		//public static PointF DeScaleDrawing(Point origin, SizeF scale,
		//	int scrollHorizontal = 0, int scrollVertical = 0)
		//{
		//	PointF orig = new PointF((float)origin.X, (float)origin.Y);
		//	PointF result = new PointF(0f, 0f);

		//	if(scale.Width != 0f)
		//	{
		//		result.X =
		//			((orig.X * scale.Width) + (float)scrollHorizontal);
		//	}
		//	if(scale.Height != 0f)
		//	{
		//		result.Y =
		//			((orig.Y * scale.Height) + (float)scrollVertical);
		//	}
		//	return result;
		//}
		////*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		///// <summary>
		///// Scale the rectangle to the specified factor and return the new area to
		///// the caller.
		///// </summary>
		///// <param name="rectangle">
		///// Rectangle to inspect.
		///// </param>
		///// <param name="scale">
		///// X and Y scale factors.
		///// </param>
		///// <returns>
		///// New rectangle as a result of the original rectangle scaled by the
		///// specified factors.
		///// </returns>
		//public static RectangleF DeScaleDrawing(RectangleF rectangle, SizeF scale)
		//{
		//	RectangleF result = new RectangleF(
		//		rectangle.X / scale.Width,
		//		rectangle.Y / scale.Height,
		//		rectangle.Width / scale.Width,
		//		rectangle.Height / scale.Height);
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

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

		////*-----------------------------------------------------------------------*
		////*	DrawRoundedRectangle																									*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Draw the outline of a rounded rectangle.
		///// </summary>
		///// <param name="graphics">
		///// </param>
		///// <param name="pen">
		///// </param>
		///// <param name="bounds">
		///// </param>
		///// <param name="cornerRadius">
		///// </param>
		//public static void DrawRoundedRectangle(Graphics graphics, Pen pen,
		//	Rectangle bounds, int cornerRadius)
		//{
		//	if(graphics != null && pen != null)
		//	{
		//		using(GraphicsPath path = RoundedRectangle(bounds, cornerRadius))
		//		{
		//			graphics.DrawPath(pen, path);
		//		}
		//	}
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	FillRoundedRectangle																									*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Fill the shape of a rounded rectangle.
		///// </summary>
		///// <param name="graphics">
		///// </param>
		///// <param name="brush">
		///// </param>
		///// <param name="bounds">
		///// </param>
		///// <param name="cornerRadius">
		///// </param>
		//public static void FillRoundedRectangle(Graphics graphics,
		//	Brush brush, Rectangle bounds, int cornerRadius)
		//{
		//	if(graphics != null && brush != null)
		//	{
		//		using(GraphicsPath path = RoundedRectangle(bounds, cornerRadius))
		//		{
		//			graphics.FillPath(brush, path);
		//		}
		//	}
		//}
		////*-----------------------------------------------------------------------*

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
		//* FromHSL																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the system color from the caller's HSL value.
		/// </summary>
		/// <param name="hue">
		/// The Hue level to convert, from 0 to 360.
		/// </param>
		/// <param name="saturation">
		/// The Saturation to convert, from 0 to 1.
		/// </param>
		/// <param name="luminance">
		/// The Luminance to convert, from 0 to 1. 0 is black and 1 is white.
		/// </param>
		/// <param name="alpha">
		/// The alpha level to apply.
		/// </param>
		/// <returns>
		/// System.Drawing.Color representing the RGB value of the caller's color.
		/// </returns>
		public static Color FromHSL(float hue, float saturation, float luminance,
			int alpha = 255)
		{
			int vHue = (int)((hue / 360f) * 240f);
			int vLum = (int)(saturation * 240f);
			int vSat = (int)(luminance * 240f);
			return FromHSL(vHue, vLum, vSat, alpha);
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Return the system color from the caller's HSL value.
		/// </summary>
		/// <param name="hue">
		/// The Hue level to convert, from 0 to 240.
		/// </param>
		/// <param name="saturation">
		/// The Saturation to convert, from 0 to 240.
		/// </param>
		/// <param name="luminance">
		/// The Luminance to convert, from 0 to 240. 0 is black and 240 is white.
		/// </param>
		/// <param name="alpha">
		/// The Alpha level to apply.
		/// </param>
		/// <returns>
		/// System.Drawing.Color representing the RGB value of the caller's color.
		/// </returns>
		public static Color FromHSL(int hue, int saturation, int luminance,
			int alpha = 255)
		{
			Color result = ColorTranslator.FromWin32(
				ColorHLSToRGB(hue, luminance, saturation));
			if(alpha < 255)
			{
				result = Color.FromArgb(alpha, result);
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetCharacterMatchCount																								*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the number of characters matching in the two strings from
		/// index 0 onward.
		/// </summary>
		/// <param name="value1">
		/// Left string to compare.
		/// </param>
		/// <param name="value2">
		/// Right string to compare.
		/// </param>
		/// <returns>
		/// Count of characters matching in the two strings from the beginning
		/// of the string. Otherwise, 0.
		/// </returns>
		public static int GetCharacterMatchCount(string value1, string value2)
		{
			char[] chars1 = null;
			char[] chars2 = null;
			int count = 0;
			int index = 0;
			int result = 0;

			if(value1?.Length > 0 && value2?.Length > 0)
			{
				chars1 = value1.ToCharArray();
				chars2 = value2.ToCharArray();
				count = Math.Min(chars1.Length, chars2.Length);
				for(index = 0; index < count; index ++)
				{
					if(chars1[index] == chars2[index])
					{
						result++;
					}
					else
					{
						break;
					}
				}
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
		//* GetMediaTypeName																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the media type name for the supplied file extension.
		/// </summary>
		/// <param name="file">
		/// Reference to information about a file.
		/// </param>
		/// <returns>
		/// Media type name for the provided file.
		/// </returns>
		public static string GetMediaTypeName(FileInfo file)
		{
			string extension = "";
			string result = "";

			if(file != null)
			{
				extension = file.Extension.ToLower();
				if(extension.StartsWith("."))
				{
					extension = extension.Substring(1);
				}
				switch(extension)
				{
					case "aac":
					case "flac":
					case "m4a":
					case "mp3":
					case "wav":
					case "wma":
						result = "MediaAudio";
						break;
					case "bmp":
					case "jpeg":
					case "jpg":
					case "png":
					case "tif":
					case "tiff":
					case "webp":
						result = "MediaImage";
						break;
					case "avi":
					case "mov":
					case "mp4":
					case "webm":
					case "wmv":
						result = "MediaVideo";
						break;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* GetRelaxedType																												*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the relaxed type name from the specified object type.
		/// </summary>
		/// <param name="value">
		/// Reference to the object for which a relaxed type will be found.
		/// </param>
		/// <returns>
		/// The name of the relaxed type to be used for this value.
		/// </returns>
		public static string GetRelaxedType(object value)
		{
			string result = "string";

			if(value != null)
			{
				if(value is bool)
				{
					result = "bool";
				}
				else if(value is Color)
				{
					result = "color";
				}
				else if(value is DateTime)
				{
					result = "datetime";
				}
				else if(value is Guid)
				{
					result = "guid";
				}
				else if(value is float)
				{
					result = "single";
				}
				else if(value is int ||
					(IsNumeric(value) && !IsNumericFloatingPoint(value)))
				{
					result = "int";
				}
				else if(value is double || IsNumericFloatingPoint(value))
				{
					result = "double";
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
		/// <param name="resources">
		/// Collection of resources to search.
		/// </param>
		/// <param name="propertyName">
		/// Name of the property to inspect.
		/// </param>
		/// <returns>
		/// Reference to the associated single instance resource, if found.
		/// Otherwise, null.
		/// </returns>
		public static ResourceItem GetResource(NodeItem node,
			ResourceCollection resources, string propertyName)
		{
			Match match = null;
			ResourceItem result = null;
			string ticket = "";

			if(node != null && resources?.Count > 0 && propertyName?.Length > 0)
			{
				if(PropertyExists(node, propertyName))
				{
					ticket = node[propertyName].StringValue().ToLower();
					match = Regex.Match(ticket, ResourceLib.rxIsGUID);
					if(match.Success)
					{
						result =
							resources.FirstOrDefault(x => x.Ticket.ToLower() == ticket);
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
		/// <param name="resources">
		/// Collection of resources to search.
		/// </param>
		/// <param name="propertyName">
		/// Name of the property to inspect.
		/// </param>
		/// <returns>
		/// Reference to the associated single instance resource, if found.
		/// Otherwise, null.
		/// </returns>
		public static ResourceItem GetResource(PropertyCollection properties,
			ResourceCollection resources, string propertyName)
		{
			Match match = null;
			ResourceItem result = null;
			string ticket = "";

			if(properties != null && resources?.Count > 0 &&
				propertyName?.Length > 0)
			{
				if(PropertyExists(properties, propertyName))
				{
					ticket = properties[propertyName].StringValue().ToLower();
					match = Regex.Match(ticket, ResourceLib.rxIsGUID);
					if(match.Success)
					{
						result =
							resources.FirstOrDefault(x => x.Ticket.ToLower() == ticket);
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
		/// <param name="resources">
		/// Collection of resources to search.
		/// </param>
		/// <param name="propertyName">
		/// Name of the property to inspect.
		/// </param>
		/// <returns>
		/// Reference to the associated single instance resource, if found.
		/// Otherwise, null.
		/// </returns>
		public static ResourceItem GetResource(SocketItem socket,
			ResourceCollection resources, string propertyName)
		{
			Match match = null;
			ResourceItem result = null;
			string ticket = "";

			if(socket != null && resources?.Count > 0 && propertyName?.Length > 0)
			{
				if(PropertyExists(socket, propertyName))
				{
					ticket = socket[propertyName].StringValue().ToLower();
					match = Regex.Match(ticket, ResourceLib.rxIsGUID);
					if(match.Success)
					{
						result =
							resources.FirstOrDefault(x => x.Ticket.ToLower() == ticket);
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
		//* InsideOf																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the text found between the left and right pattern edges.
		/// </summary>
		/// <param name="value">
		/// The value to inspect.
		/// </param>
		/// <param name="leftPattern">
		/// The pattern establishing the left boundary of the content.
		/// </param>
		/// <param name="rightPattern">
		/// The pattern establishing the right boundary of the content.
		/// </param>
		/// <returns>
		/// If the left and right patterns were both found, then the content
		/// between those patterns. If only the left pattern was found and the
		/// right pattern was not found to the right of it, then the content to
		/// the right of the left pattern. If the left pattern was not found and
		/// the right pattern was found, then the content to the left of the
		/// right pattern. Otherwise, an empty string.
		/// </returns>
		public static string InsideOf(string value,
			string leftPattern, string rightPattern)
		{
			int leftIndex = 0;
			string result = "";
			int rightIndex = 0;
			
			if(value?.Length > 0)
			{
				if(leftPattern?.Length > 0 && value.IndexOf(leftPattern) > -1)
				{
					//	Left pattern found.
					leftIndex = value.IndexOf(leftPattern);
					if(rightPattern?.Length > 0 &&
						value.IndexOf(rightPattern, leftIndex + 1) > -1)
					{
						//	Right pattern found.
						rightIndex =
							value.IndexOf(rightPattern, leftIndex + leftPattern.Length);
						if(rightIndex > leftIndex + leftPattern.Length)
						{
							result = value.Substring(leftIndex + leftPattern.Length,
								rightIndex - (leftIndex + leftPattern.Length));
						}
					}
					else
					{
						//	Right pattern not found.
						result = value.Substring(leftIndex + leftPattern.Length);
					}
				}
				else if(rightPattern?.Length > 0 && value.IndexOf(rightPattern) > -1)
				{
					//	Left pattern not found. Right pattern found.
					rightIndex = value.IndexOf(rightPattern);
					result = value.Substring(0, rightIndex);
				}
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

		////*-----------------------------------------------------------------------*
		////* LinearInterpolate																											*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return the linear interpolation of the specified amount between values
		///// a and b.
		///// </summary>
		///// <param name="a">
		///// First value.
		///// </param>
		///// <param name="b">
		///// Second value.
		///// </param>
		///// <param name="amount">
		///// The amount to interpolate.
		///// </param>
		///// <returns>
		///// The linearly interpolated difference between a and b.
		///// </returns>
		//public static float LinearInterpolate(float a, float b, float amount)
		//{
		//	return a * (1f - amount) + b * amount;
		//}
		////*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		///// <summary>
		///// Return the linear interpolation between points a and b.
		///// </summary>
		///// <param name="a">
		///// Point a.
		///// </param>
		///// <param name="b">
		///// Point b.
		///// </param>
		///// <param name="amount">
		///// The amount to interpolate.
		///// </param>
		///// <returns>
		///// The linearly interpolated difference between points a and b.
		///// </returns>
		//public static PointF LinearInterpolate(PointF a, PointF b, float amount)
		//{
		//	PointF result = new PointF();

		//	result.X = a.X * (1f - amount) + b.X * amount;
		//	result.Y = a.Y * (1f - amount) + b.Y * amount;

		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	MeasureString																													*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return the size of the caller's string.
		///// </summary>
		///// <param name="text">
		///// Text to measure.
		///// </param>
		///// <param name="fontName">
		///// Name of the font.
		///// </param>
		///// <param name="fontSize">
		///// Size of the font.
		///// </param>
		///// <param name="maxWidth">
		///// Optional maximum width of the string.
		///// </param>
		///// <returns>
		///// </returns>
		//public static Size MeasureString(string text,
		//	string fontName, float fontSize, int maxWidth = 0)
		//{
		//	Graphics g = Graphics.FromHwnd(IntPtr.Zero);
		//	Size result = Size.Empty;

		//	if(text?.Length > 0 && fontName?.Length > 0 && fontSize > 0f)
		//	{
		//		g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
		//		if(maxWidth > 0f)
		//		{
		//			//	Word wrap.
		//			result =
		//				Size.Round(
		//					g.MeasureString(text, new Font(fontName, fontSize), maxWidth,
		//					StringFormat.GenericTypographic));
		//		}
		//		else
		//		{
		//			//	Single line.
		//			result =
		//				Size.Round(
		//					g.MeasureString(text, new Font(fontName, fontSize)));
		//		}
		//	}
		//	g.Dispose();
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* MediaExists																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified media exists on the
		/// provided node.
		/// </summary>
		/// <param name="node">
		/// Reference to the node to test.
		/// </param>
		/// <param name="resources">
		/// Reference to a single instance resource collection.
		/// </param>
		/// <param name="propertyName">
		/// Name of the property to check. If name is null or blank, any fully
		/// qualified media is counted as a match.
		/// </param>
		/// <returns>
		/// True if media type is found on the node. Otherwise, false.
		/// </returns>
		public static bool MediaExists(NodeItem node, ResourceCollection resources,
			string propertyName = null)
		{
			bool result = false;

			if(node != null)
			{
				if(propertyName?.Length > 0)
				{
					//	Property was specified.
					if(PropertyExists(node, propertyName) &&
						resources.Exists(x => x.Ticket.ToLower() ==
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
							resources.Exists(x => x.Ticket.ToLower() ==
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
		/// <param name="resources">
		/// Reference to a single instance resource collection.
		/// </param>
		/// <param name="propertyName">
		/// Name of the property to check. If name is null or blank, any fully
		/// qualified media is counted as a match.
		/// </param>
		/// <returns>
		/// True if media type is found on the node. Otherwise, false.
		/// </returns>
		public static bool MediaExists(PropertyCollection properties,
			ResourceCollection resources, string propertyName = null)
		{
			bool result = false;

			if(properties?.Count > 0)
			{
				if(propertyName?.Length > 0)
				{
					//	Property was specified.
					if(PropertyExists(properties, propertyName) &&
						resources.Exists(x => x.Ticket.ToLower() ==
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
							resources.Exists(x => x.Ticket.ToLower() ==
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
		/// Return a value indicating whether the specified media exists on the
		/// provided node.
		/// </summary>
		/// <param name="socket">
		/// Reference to the socket to test.
		/// </param>
		/// <param name="resources">
		/// Reference to a single instance resource collection.
		/// </param>
		/// <param name="propertyName">
		/// Name of the property to check. If name is null or blank, any fully
		/// qualified media is counted as a match.
		/// </param>
		/// <returns>
		/// True if media type is found on the node. Otherwise, false.
		/// </returns>
		public static bool MediaExists(SocketItem socket,
			ResourceCollection resources, string propertyName = null)
		{
			bool result = false;

			if(socket != null)
			{
				if(propertyName?.Length > 0)
				{
					//	Property was specified.
					if(PropertyExists(socket, propertyName) &&
						resources.Exists(x => x.Ticket.ToLower() ==
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
							resources.Exists(x => x.Ticket.ToLower() ==
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
					default:	//	Any unidentified format is binary data.
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

		////*-----------------------------------------------------------------------*
		////* ProgressUpdate																												*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Update progress bar value through asynchronous invoke.
		///// </summary>
		///// <param name="progress">
		///// Reference to the progress bar to update.
		///// </param>
		///// <param name="value">
		///// Value to place on the progress bar.
		///// </param>
		//public static void ProgressUpdate(ToolStripProgressBar progress, int value)
		//{
		//	if(progress != null && progress.GetCurrentParent().Parent.InvokeRequired)
		//	{
		//		progress.GetCurrentParent().Parent.BeginInvoke(
		//			new MethodInvoker(delegate { progress.Value = value; }));
		//	}
		//}
		////*-----------------------------------------------------------------------*

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

		////*-----------------------------------------------------------------------*
		////*	RectangleFromPoints																										*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Create a rectangle from two points.
		///// </summary>
		///// <param name="a">
		///// </param>
		///// <param name="b">
		///// </param>
		///// <returns>
		///// </returns>
		//public static RectangleF RectangleFromPoints(PointF a, PointF b)
		//{
		//	RectangleF result = RectangleF.Empty;
		//	float x1 = a.X;
		//	float x2 = b.X;
		//	float y1 = a.Y;
		//	float y2 = b.Y;

		//	result = new RectangleF(
		//		Math.Min(x1, x2), Math.Min(y1, y2),
		//		Math.Abs(x2 - x1), Math.Abs(y2 - y1));
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

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
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* RelativeFilename																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return the relative filename from the logical difference between
		/// two paths.
		/// </summary>
		/// <param name="baseFile">
		/// The base file or domain name from which a relative path can be built.
		/// </param>
		/// <param name="relativeFile">
		/// An external file to be referenced with a relative path.
		/// </param>
		/// <returns>
		/// Relative path and filename of the specified relative file, if
		/// feasible. Otherwise, the full path of the relative file.
		/// </returns>
		public static string RelativeFilename(FileInfo baseFile,
			FileInfo relativeFile)
		{
			StringBuilder builder = null;
			int count = 0;
			int index = 0;
			string[] levels = null;
			string pathBase = "";
			string pathOffset = "";
			string pathRel = "";
			int prefixLength = 0;
			string result = "";
			char[] slash = new char[] { '/' };

			if(relativeFile != null)
			{
				if(baseFile != null)
				{
					pathBase = baseFile.Directory.FullName.ToLower().Replace('\\', '/');
					pathRel =
						relativeFile.Directory.FullName.ToLower().Replace('\\', '/');
					if(pathBase == pathRel)
					{
						//	Same folder. The filename is all that is needed.
						result = relativeFile.Name;
					}
					else
					{
						prefixLength = GetCharacterMatchCount(pathBase, pathRel);
						if(prefixLength == 0)
						{
							//	No path in common.
							result = relativeFile.FullName.Replace('\\', '/');
						}
						else if(prefixLength == pathBase.Length)
						{
							//	The relative file is inward from the base.
							result = Path.Combine(
								relativeFile.Directory.FullName.
								Substring(prefixLength),
								relativeFile.Name).Replace('\\', '/');
							if(result.StartsWith("/"))
							{
								result = result.Substring(1);
							}
						}
						else if(prefixLength == pathRel.Length)
						{
							//	The relative file is back from the base.
							pathOffset = baseFile.Directory.FullName.
								Substring(prefixLength).Replace('\\', '/');
							if(pathOffset.StartsWith("/"))
							{
								pathOffset = pathOffset.Substring(1);
							}
							if(pathOffset.EndsWith("/"))
							{
								pathOffset = pathOffset.Substring(0, pathOffset.Length - 1);
							}
							builder = new StringBuilder();
							levels = pathOffset.Split(slash);
							count = levels.Length;
							for(index = 0; index < count; index ++)
							{
								builder.Append("../");
							}
							builder.Append(relativeFile.Name);
							result = builder.ToString();
						}
						else
						{
							//	The relative file is diagonal to the base.
							//	Start by getting the number of directories back.
							pathOffset = baseFile.Directory.FullName.
								Substring(prefixLength).Replace('\\', '/');
							if(pathOffset.StartsWith("/"))
							{
								pathOffset = pathOffset.Substring(1);
							}
							if(pathOffset.EndsWith("/"))
							{
								pathOffset = pathOffset.Substring(0, pathOffset.Length - 1);
							}
							builder = new StringBuilder();
							levels = pathOffset.Split(slash);
							count = levels.Length;
							for(index = 0; index < count; index++)
							{
								builder.Append("../");
							}
							//	Continue by adding in the number of directories forward.
							pathOffset = relativeFile.Directory.FullName.
								Substring(prefixLength).Replace('\\', '/');
							if(pathOffset.StartsWith("/"))
							{
								pathOffset = pathOffset.Substring(1);
							}
							if(pathOffset.EndsWith("/"))
							{
								pathOffset = pathOffset.Substring(0, pathOffset.Length - 1);
							}
							if(builder[builder.Length - 1] != '/')
							{
								builder.Append("/");
							}
							builder.Append(pathOffset);
							builder.Append("/");
							builder.Append(relativeFile.Name);
							result = builder.ToString();
						}
					}
				}
				else
				{
					result = relativeFile.FullName;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ResolveEnvironment																										*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a string value where all environment variable references have
		/// been resolved.
		/// </summary>
		/// <param name="value">
		/// String value potentially containing environment variables wrapped in
		/// percent signs.
		/// </param>
		/// <returns>
		/// String value where all environment variables have been replaced with
		/// their local values.
		/// </returns>
		public static string ResolveEnvironment(string value)
		{
			string env = "";
			MatchCollection matches = null;
			string setting = "";
			string result = value;

			matches = Regex.Matches(value, "%(?<f>[^%]+?)%");
			foreach(Match match in matches)
			{
				setting = GetValue(match, "f");
				if(setting.Length > 0)
				{
					env = Environment.GetEnvironmentVariable(setting);
					if(env?.Length > 0)
					{
						result = result.Replace($"%{setting}%", env);
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* RoundedRectangle																											*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return a graphics path representing a rounded rectangle.
		///// </summary>
		///// <param name="bounds">
		///// </param>
		///// <param name="radius">
		///// </param>
		///// <returns>
		///// </returns>
		//public static GraphicsPath RoundedRectangle(Rectangle bounds, int radius)
		//{
		//	int diameter = radius * 2;
		//	Size size = new Size(diameter, diameter);
		//	Rectangle arc = new Rectangle(bounds.Location, size);
		//	GraphicsPath path = new GraphicsPath();

		//	if(radius == 0)
		//	{
		//		path.AddRectangle(bounds);
		//		return path;
		//	}

		//	// Top left arc  
		//	path.AddArc(arc, 180, 90);

		//	// Top right arc  
		//	arc.X = bounds.Right - diameter;
		//	path.AddArc(arc, 270, 90);

		//	// Bottom right arc  
		//	arc.Y = bounds.Bottom - diameter;
		//	path.AddArc(arc, 0, 90);

		//	// Bottom left arc 
		//	arc.X = bounds.Left;
		//	path.AddArc(arc, 90, 90);

		//	path.CloseFigure();
		//	return path;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* Saturate																															*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Saturate any value outside of a positive decimal fraction.
		///// </summary>
		///// <param name="value">
		///// Decimal fraction to inspect.
		///// </param>
		///// <returns>
		///// Caller's value, saturated to 0 if the original value was less than 0
		///// and saturated to 1 if the original value was more than 1.
		///// </returns>
		//public static double Saturate(double value)
		//{
		//	double result = value;

		//	if(value < 0d)
		//	{
		//		result = 0d;
		//	}
		//	else if(value > 1d)
		//	{
		//		result = 1d;
		//	}
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////* ScaleValues																														*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Return the scale of the caller's values.
		///// </summary>
		///// <param name="x"></param>
		///// <param name="a"></param>
		///// <param name="b"></param>
		///// <param name="c"></param>
		///// <param name="d"></param>
		///// <returns></returns>
		//public static double ScaleValues(double x, double a, double b,
		//	double c, double d)
		//{
		//	double s = (x - a) / (b - a);
		//	return s * (d - c) + c;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	ScaleDrawing																													*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Translate the caller's physical coordinate to the scaled view.
		///// </summary>
		///// <param name="origin">
		///// The physical coordinate being translated.
		///// </param>
		///// <param name="scale">
		///// The current scaling factor.
		///// </param>
		///// <param name="scroll">
		///// The current scroll value in the specified axis.
		///// </param>
		///// <returns>
		///// </returns>
		///// <remarks>
		///// Scrollbar values are assumed to exist in the scaled domain, since
		///// they serve to move the scaled view on the physical viewport.
		///// </remarks>
		//public static float ScaleDrawing(int origin, float scale, int scroll = 0)
		//{
		//	float result = 0f;

		//	if(scale != 0f)
		//	{
		//		result = ((float)origin * (1f / scale)) + (float)scroll;
		//	}
		//	return result;
		//}
		////*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		///// <summary>
		///// Translate the caller's physical point to the scaled view.
		///// </summary>
		///// <param name="origin">
		///// The physical point being translated.
		///// </param>
		///// <param name="scale">
		///// The current scaling factor.
		///// </param>
		///// <param name="scrollHorizontal">
		///// The current horizontal scroll value.
		///// </param>
		///// <param name="scrollVertical">
		///// The current vertical scroll value.
		///// </param>
		///// <returns>
		///// A scaled version of the caller's point.
		///// </returns>
		///// <remarks>
		///// Scrollbar values are assumed to exist in the scaled domain, since
		///// they serve to move the scaled view on the physical viewport.
		///// </remarks>
		//public static PointF ScaleDrawing(Point origin, SizeF scale,
		//	int scrollHorizontal = 0, int scrollVertical = 0)
		//{
		//	PointF orig = new PointF((float)origin.X, (float)origin.Y);
		//	PointF result = new PointF(0f, 0f);

		//	if(scale.Width != 0f)
		//	{
		//		result.X =
		//			(orig.X + (float)scrollHorizontal) * (1f / scale.Width);
		//		//Debug.WriteLine(
		//		//	$"Scale H with {scrollHorizontal} From {orig.X} to {result.X}");
		//	}
		//	if(scale.Height != 0f)
		//	{
		//		result.Y =
		//			(orig.Y + (float)scrollVertical) * (1f / scale.Height);
		//	}
		//	return result;
		//}
		////*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		///// <summary>
		///// Scale the rectangle to the specified factor and return the new area to
		///// the caller.
		///// </summary>
		///// <param name="rectangle">
		///// Rectangle to inspect.
		///// </param>
		///// <param name="scale">
		///// X and Y scale factors.
		///// </param>
		///// <returns>
		///// New rectangle as a result of the original rectangle scaled by the
		///// specified factors.
		///// </returns>`
		//public static RectangleF ScaleDrawing(RectangleF rectangle, SizeF scale)
		//{
		//	RectangleF result = new RectangleF(
		//		rectangle.X * scale.Width,
		//		rectangle.Y * scale.Height,
		//		rectangle.Width * scale.Width,
		//		rectangle.Height * scale.Height);
		//	return result;
		//}
		////*-----------------------------------------------------------------------*

		////*-----------------------------------------------------------------------*
		////*	SvgReplaceFileRefWithB64																							*
		////*-----------------------------------------------------------------------*
		///// <summary>
		///// Replace all file references in the SVG content with base64 embedded
		///// values.
		///// </summary>
		///// <param name="directory">
		///// Working directory serving as the base of the current content.
		///// </param>
		///// <param name="rawContent">
		///// SVG content.
		///// </param>
		///// <param name="progress">
		///// Reference to an optional progress bar to display the current progress.
		///// </param>
		///// <returns>
		///// SVG string value where all external references have been fully
		///// embedded.
		///// </returns>
		//public static async Task SvgReplaceFileRefWithB64(DirectoryInfo directory,
		//	string rawContent, NameValueItem namedResult,
		//	ToolStripProgressBar progress = null)
		//{
		//	Action replaceInstances = () =>
		//	{
		//		byte[] buffer = new byte[0];  //	File content.
		//		StringBuilder builder = new StringBuilder();  //	base64 content.
		//		double count = 0;
		//		FileInfo file = null;
		//		double index = 0;
		//		string inner = "";
		//		MatchCollection matches = null;
		//		List<string> processedLinks = new List<string>();
		//		string result = rawContent;

		//		if(directory != null && rawContent?.Length > 0)
		//		{
		//			matches = Regex.Matches(rawContent, ResourceMain.rxSVGHREFFind);
		//			count = (double)matches.Count;
		//			index = 0.0;
		//			foreach(Match match in matches)
		//			{
		//				//	External reference found.
		//				if(builder.Length > 0)
		//				{
		//					builder.Remove(0, builder.Length);
		//				}
		//				inner = GetValue(match, "inner");
		//				if(inner.Length > 0 && !processedLinks.Exists(x => x == inner))
		//				{
		//					file = new FileInfo(Path.Combine(directory.FullName, inner));
		//					if(file.Exists)
		//					{
		//						//	The file is available.
		//						buffer = File.ReadAllBytes(file.FullName);
		//						builder.Append("data:");
		//						builder.Append(MimeType(file.Extension));
		//						builder.Append(";base64,");
		//						builder.Append(Convert.ToBase64String(buffer));
		//					}
		//					result = result.Replace(GetValue(match, "outer"),
		//						$"xlink:href=\"{builder}\"");
		//					processedLinks.Add(inner);
		//					GC.Collect();
		//				}
		//				if(progress != null && count != 0.0)
		//				{
		//					ProgressUpdate(progress, (int)(index / count));
		//				}
		//				index++;
		//			}
		//		}
		//		namedResult.Name = "OK";
		//		namedResult.Value = result;
		//	};
		//	await Task.Run(replaceInstances);
		//}
		////*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* ThreadSleep																														*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Continue to process the UI while waiting on the calling thread for
		/// the specified number of milliseconds.
		/// </summary>
		/// <param name="milliseconds">
		/// Number of milliseconds to wait.
		/// </param>
		/// <returns>
		/// Reference to an active task.
		/// </returns>
		public static async Task ThreadSleep(int milliseconds)
		{
			await Task.Delay(milliseconds);
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
				for(index = 0; index < count; index ++)
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
		//* ToImpliedType																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value that is either string, numeric, or boolean.
		/// </summary>
		/// <param name="value">
		/// Value to inspect.
		/// </param>
		/// <returns>
		/// Value in a native type corresponding to string, numeric, or boolean.
		/// </returns>
		/// <remarks>
		/// This method is generally intended for use with JSON output.
		/// </remarks>
		public static object ToImpliedType(string value)
		{
			object result = null;

			if(value != null)
			{
				if(IsNumeric(value))
				{
					if(IsNumericFloatingPoint(value))
					{
						result = ToFloat(value);
					}
					else
					{
						result = ToInt(value);
					}
				}
				else if(IsBoolean(value))
				{
					result = ToBoolean(value);
				}
				else
				{
					result = value;
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

		////*-----------------------------------------------------------------------*
		////*	TypeConverter																													*
		////*-----------------------------------------------------------------------*
		//private static RelaxedTypeConverterCollection mTypeConverter =
		//	new RelaxedTypeConverterCollection();
		///// <summary>
		///// Get a reference to the universal relaxed type converter.
		///// </summary>
		//public static RelaxedTypeConverterCollection TypeConverter
		//{
		//	get { return mTypeConverter; }
		//}
		////*-----------------------------------------------------------------------*

#if DEBUG && VERBOSE
		//*-----------------------------------------------------------------------*
		//* Verbose																																*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Send a message to the verbose text output.
		/// </summary>
		/// <param name="value">
		/// Message to display.
		/// </param>
		/// <param name="level">
		/// Verbose level to display.
		/// </param>
		public static void Verbose(string value, int level = 1)
		{
#if V3
			if(level <= 3)
			{
				Debug.WriteLine(value);
			}
#elif V2
			if(level <= 2)
			{
				Debug.WriteLine(value);
			}
#else
			if(level == 1)
			{
				Debug.WriteLine(value);
			}
#endif
		}
		//*-----------------------------------------------------------------------*
#else
			//*-----------------------------------------------------------------------*
			//* Verbose																																*
			//*-----------------------------------------------------------------------*
			/// <summary>
			/// Send a message to the verbose text output.
			/// </summary>
			/// <param name="value">
			/// Message to display.
			/// </param>
			/// <param name="level">
			/// Verbose level to display.
			/// </param>
			public static void Verbose(string value, int level = 1) { }
		//*-----------------------------------------------------------------------*
#endif

	}
	//*-------------------------------------------------------------------------*
}
