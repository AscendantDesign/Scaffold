//	Resource.cs
//	Copyright(c) 2020. Ascendant Design and Training, LLC
//	This file is licensed under the MIT License.
//	Please see the LICENSE file in this project.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using static Scaffold.ScaffoldNodesUtil;

namespace Scaffold
{
	//*-------------------------------------------------------------------------*
	//*	ResourceCollection																											*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of ResourceItem Items.
	/// </summary>
	public class ResourceCollection : List<ResourceItem>
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************

		//*-----------------------------------------------------------------------*
		//*	BasePath																															*
		//*-----------------------------------------------------------------------*
		private static string mBasePath = "";
		/// <summary>
		/// Get/Set the base path of the currently loaded resource collection.
		/// </summary>
		public static string BasePath
		{
			get { return mBasePath; }
			set { mBasePath = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	ResourceItem																														*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Information about a universal single instance resource.
	/// </summary>
	public class ResourceItem
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************
		//*-----------------------------------------------------------------------*
		//*	_Constructor																													*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Create a new instance of the ResourceItem Item.
		/// </summary>
		public ResourceItem()
		{
		}
		//*- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -*
		/// <summary>
		/// Create a new instance of the ResourceItem Item.
		/// </summary>
		/// <param name="original">
		/// Reference to an original resource item to copy.
		/// </param>
		public ResourceItem(ResourceItem original)
		{
			if(original != null)
			{
				this.mAbsoluteFilename = original.mAbsoluteFilename;
				this.mDataUriHeaderLength = original.mDataUriHeaderLength;
				this.mRelativeFilename = original.mRelativeFilename;
				this.mResourceType = original.mResourceType;
				this.mTicket = original.mTicket;
				this.mUri = original.mUri;
				foreach(PropertyItem property in original.mProperties)
				{
					this.mProperties.Add(new PropertyItem(property));
				}
			}
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	AbsoluteFilename																											*
		//*-----------------------------------------------------------------------*
		private string mAbsoluteFilename = "";
		/// <summary>
		/// Get/Set the full path and filename as it was originally loaded into
		/// this file.
		/// </summary>
		public string AbsoluteFilename
		{
			get { return mAbsoluteFilename; }
			set { mAbsoluteFilename = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	DataUriHeaderLength																										*
		//*-----------------------------------------------------------------------*
		private int mDataUriHeaderLength = 0;
		/// <summary>
		/// Get/Set the Data URI header length.
		/// </summary>
		/// <remarks>
		/// This property provides a shortcut for dissecting the Data URI string.
		/// </remarks>
		public int DataUriHeaderLength
		{
			get { return mDataUriHeaderLength; }
			set { mDataUriHeaderLength = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Embed																																	*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return an embedded resource version of the provided resource.
		/// </summary>
		/// <param name="resource">
		/// Reference to the resource to be processed for embedding.
		/// </param>
		/// <returns>
		/// Reference to the newly embedded resource, if the original was a link.
		/// Otherwise, a reference to the original item if that original already
		/// contained an embedded Data URI.
		/// </returns>
		public static ResourceItem Embed(ResourceItem resource)
		{
			FileInfo file = null;
			ResourceItem result = null;

			if(resource != null)
			{
				if(IsDataUri(resource))
				{
					//	The item is already embedded data.
					result = resource;
				}
				else if(!resource.mUri.StartsWith("http") &&
					!resource.mUri.StartsWith("ftp"))
				{
					//	The item needs to be converted to data.
					file = new FileInfo(GetEffectivePath(resource.mUri));
					if(file.Exists)
					{
						//	The referenced file exists.
						result = new ResourceItem(resource);
						result.mDataUriHeaderLength = GetDataUriHeaderLength(file);
						result.mUri = GetDataUri(file);
					}
					else
					{
						//	The specified file could not be found.
						Debug.WriteLine("Error in ResourceItem.Embed: " +
							$"File not found: {file.FullName}");
					}
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* Filename																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return only the filename and extension of the base file for the
		/// referenced resource.
		/// </summary>
		/// <param name="resource">
		/// Reference to the resource to inspect.
		/// </param>
		/// <returns>
		/// Filename and extension of the file without path.
		/// </returns>
		public static string Filename(ResourceItem resource)
		{
			Match match = null;
			string result = "";

			if(resource != null)
			{
				if(resource.mAbsoluteFilename.Contains("."))
				{
					match = Regex.Match(resource.mAbsoluteFilename,
						ResourceLib.rxFilenameInPath);
				}
				else if(resource.mUri.Contains("."))
				{
					match = Regex.Match(resource.mUri,
						ResourceLib.rxFilenameInPath);
				}
				if(match.Success)
				{
					result = GetValue(match, "filenameextension");
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* IsDataUri																															*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a value indicating whether the specified resource has an
		/// embedded Data Uri.
		/// </summary>
		/// <param name="resource">
		/// Resource to inspect.
		/// </param>
		/// <returns>
		/// True if the provided resource has embedded data. Otherwise, false.
		/// </returns>
		public static bool IsDataUri(ResourceItem resource)
		{
			bool result = false;

			if(resource != null)
			{
				if(resource.mDataUriHeaderLength > 0)
				{
					result = true;
				}
				else if(resource.mUri.Length > 0 && resource.mUri.StartsWith("data:"))
				{
					result = true;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Properties																														*
		//*-----------------------------------------------------------------------*
		private PropertyCollection mProperties = new PropertyCollection();
		/// <summary>
		/// Get a reference to the collection of abstract properties for this
		/// resource.
		/// </summary>
		public PropertyCollection Properties
		{
			get { return mProperties; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	RelativeFilename																											*
		//*-----------------------------------------------------------------------*
		private string mRelativeFilename = "";
		/// <summary>
		/// Get/Set the relative filename to the loaded data, if feasible.
		/// Otherwise, blank.
		/// </summary>
		public string RelativeFilename
		{
			get { return mRelativeFilename; }
			set { mRelativeFilename = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	ResourceType																													*
		//*-----------------------------------------------------------------------*
		private string mResourceType = "String";
		/// <summary>
		/// Get/Set the type name of this resource.
		/// </summary>
		public string ResourceType
		{
			get { return mResourceType; }
			set { mResourceType = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Ticket																																*
		//*-----------------------------------------------------------------------*
		private string mTicket = Guid.NewGuid().ToString("D");
		/// <summary>
		/// Get/Set the globally unique identification of this resource.
		/// </summary>
		public string Ticket
		{
			get { return mTicket; }
			set { mTicket = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Uri																																		*
		//*-----------------------------------------------------------------------*
		private string mUri = "";
		/// <summary>
		/// Get/Set the active URI.
		/// </summary>
		/// <remarks>
		/// If Data URI, the digital content of the data will be embedded in this
		/// field. Otherwise, if this resource is accessed by a link, the standard
		/// HTML5 HREF syntax is used to load the data.
		/// </remarks>
		public string Uri
		{
			get { return mUri; }
			set { mUri = value; }
		}
		//*-----------------------------------------------------------------------*


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	ResourceLiveCollection																									*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Collection of ResourceLiveItem Items.
	/// </summary>
	public class ResourceLiveCollection : List<ResourceLiveItem>
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************


	}
	//*-------------------------------------------------------------------------*

	//*-------------------------------------------------------------------------*
	//*	ResourceLiveItem																												*
	//*-------------------------------------------------------------------------*
	/// <summary>
	/// Live, loaded and unpacked resource.
	/// </summary>
	public class ResourceLiveItem
	{
		//*************************************************************************
		//*	Private																																*
		//*************************************************************************
		//*************************************************************************
		//*	Protected																															*
		//*************************************************************************
		//*************************************************************************
		//*	Public																																*
		//*************************************************************************

		//*-----------------------------------------------------------------------*
		//*	AbsoluteFilename																											*
		//*-----------------------------------------------------------------------*
		private string mAbsoluteFilename = "";
		/// <summary>
		/// Get/Set the full path and filename as it was originally loaded into
		/// this file.
		/// </summary>
		public string AbsoluteFilename
		{
			get { return mAbsoluteFilename; }
			set { mAbsoluteFilename = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Data																																	*
		//*-----------------------------------------------------------------------*
		private object mData = null;
		/// <summary>
		/// Get/Set the binary data loaded in this resource.
		/// </summary>
		public object Data
		{
			get { return mData; }
			set { mData = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//* FromResourceItem																											*
		//*-----------------------------------------------------------------------*
		/// <summary>
		/// Return a live, binary usable representation of the resource from the
		/// universal single instance resource.
		/// </summary>
		/// <param name="resource">
		/// Reference to the universal single instance resource to convert.
		/// </param>
		/// <returns>
		/// Newly created live resource item, if a resource was provided.
		/// Otherwise, null;
		/// </returns>
		public static ResourceLiveItem FromResourceItem(ResourceItem resource)
		{
			byte[] buffer = null;
			char[] chars = null;
			int count = 0;
			FileInfo file = null;
			int index = 0;
			ResourceLiveItem result = null;

			if(resource != null)
			{
				result = new ResourceLiveItem();
				result.mAbsoluteFilename = resource.AbsoluteFilename;
				result.mRelativeFilename = resource.RelativeFilename;
				if(result.mRelativeFilename?.Length > 0)
				{
					file =
						new FileInfo(Path.Combine(ResourceCollection.BasePath,
						result.mRelativeFilename));
					result.mMimeType = MimeType(file.Extension);
				}
				result.mTicket = resource.Ticket;
				if(resource.ResourceType?.Length > 0 &&
					resource.ResourceType != "MediaLink" &&
					resource.ResourceType != "String")
				{
					if(resource.Uri?.Length > 0)
					{
						index = 0;
						count = 0;
						chars = resource.Uri.ToCharArray();
						if(resource.DataUriHeaderLength > 0)
						{
							//	Header length has been provided.
							index = resource.DataUriHeaderLength;
							count = chars.Length - resource.DataUriHeaderLength;
						}
						else if(resource.Uri.StartsWith("data:"))
						{
							//	This is a binary file with the prefix
							//	data:{MimeType};base64,{Base64Data}
							//	In single character search, look for first comma.
							for(index = 0; index < chars.Length; index ++)
							{
								if(chars[index] == ',')
								{
									index++;
									count = chars.Length - index;
									break;
								}
							}
						}
						else
						{
							//	Otherwise, the data for this resource is located in a
							//	separate file.
							try
							{
								result.mData =
									File.ReadAllBytes(Path.Combine(
										ResourceCollection.BasePath, resource.Uri));
							}
							catch
							{
								//	If the file was not read, return null as indication.
								result = null;
							}
						}
						if(count != 0)
						{
							buffer = Convert.FromBase64CharArray(chars, index, count);
							result.mData = buffer;
						}
					}
				}
				else
				{
					//	String-type resource.
					result.mData = resource.Uri;
				}
			}
			return result;
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	MimeType																															*
		//*-----------------------------------------------------------------------*
		private string mMimeType = "";
		/// <summary>
		/// Get/Set the MIME type of this resource.
		/// </summary>
		public string MimeType
		{
			get { return mMimeType; }
			set { mMimeType = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Properties																														*
		//*-----------------------------------------------------------------------*
		private PropertyCollection mProperties = new PropertyCollection();
		/// <summary>
		/// Get a reference to the collection of abstract properties for this
		/// resource.
		/// </summary>
		public PropertyCollection Properties
		{
			get { return mProperties; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	RelativeFilename																											*
		//*-----------------------------------------------------------------------*
		private string mRelativeFilename = "";
		/// <summary>
		/// Get/Set the relative filename to the loaded data, if feasible.
		/// Otherwise, blank.
		/// </summary>
		public string RelativeFilename
		{
			get { return mRelativeFilename; }
			set { mRelativeFilename = value; }
		}
		//*-----------------------------------------------------------------------*

		//*-----------------------------------------------------------------------*
		//*	Ticket																																*
		//*-----------------------------------------------------------------------*
		private string mTicket = Guid.NewGuid().ToString("D");
		/// <summary>
		/// Get/Set the globally unique identification of this resource.
		/// </summary>
		public string Ticket
		{
			get { return mTicket; }
			set { mTicket = value; }
		}
		//*-----------------------------------------------------------------------*

	}
	//*-------------------------------------------------------------------------*


}
