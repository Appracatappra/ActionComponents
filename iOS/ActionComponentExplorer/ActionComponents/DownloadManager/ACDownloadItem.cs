using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Threading;

namespace ActionComponents
{
	/// <summary>
	/// Holds all information about a given file to be downloaded such as the URL and
	/// the destination directory. Events can be raised when the individual file has
	/// finished downloading for completed, canceled or on download error.
	/// </summary>
	public class ACDownloadItem
	{
		//Public properties
		/// <summary>
		/// The URL to download the file from
		/// </summary>
		public string URL="";
		/// <summary>
		/// The directory to download the file to
		/// </summary>
		public string downloadDirectory="";
		/// <summary>
		/// OPTIONAL: Specifies a new name for the file when it is downloaded
		/// </summary>
		public string destinationFilename="";

		#region Computed Properties
		/// <summary>
		/// Returns the filename of the URL currently being downloaded
		/// </summary>
		/// <value>
		/// The filename currently being downloaded or empty string ("") if no
		/// active download
		/// </value>
		public string filename{
			get{
				if (destinationFilename!="") {
					return destinationFilename;
				} else if (URL=="") {
					return "";
				} else {
					return URL.Substring(URL.LastIndexOf ("/")+1,
					                    (URL.Length-URL.LastIndexOf ("/")-1));
				}
			}
		}
		#endregion 

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACDownloadItem"/> class.
		/// </summary>
		public ACDownloadItem ()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACDownloadItem"/> class and sets
		/// the default values
		/// </summary>
		/// <param name="URL">The URL of the file to download</param>
		/// <param name="downloadDirectory">The directory that the file will be downloaded to</param>
		public ACDownloadItem(string URL, string downloadDirectory) {
			//Initialize
			this.URL = URL;
			this.downloadDirectory = downloadDirectory;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponents.ACDownloadItem"/> class and sets
		/// the default values
		/// </summary>
		/// <param name="URL">The URL of the file to download</param>
		/// <param name="downloadDirectory">The directory that the file will be downloaded to</param>
		/// <param name="destinationFilename">Specifies a new name for the file when it is downloaded</param>
		public ACDownloadItem(string URL, string downloadDirectory, string destinationFilename) {
			//Initialize
			this.URL = URL;
			this.downloadDirectory = downloadDirectory;
			this.destinationFilename = destinationFilename;
		}
		#endregion

		#region Event Handlers
		/// <summary>
		/// Raises the download error.
		/// </summary>
		/// <param name="message">The error message to report</param>
		internal void RaiseDownloadError(string message){
			//Inform caller of error
			if (this.DownloadError != null)
				this.DownloadError (message);
		}

		/// <summary>
		/// Raises the download canceled.
		/// </summary>
		/// <param name="filename">The name of the file that the download is being canceled on</param>
		internal void RaiseDownloadCanceled(string filename){
			//Inform caller
			if (this.DownloadCanceled != null)
				this.DownloadCanceled (filename);
		}

		/// <summary>
		/// Raises the download file started.
		/// </summary>
		/// <param name="filename">The name of the file that started downloading</param>
		internal void RaiseDownloadFileStarted(string filename){
			//Inform caller
			if (this.DownloadFileStarted != null)
				this.DownloadFileStarted (filename);
		}

		/// <summary>
		/// Raises the download file competed.
		/// </summary>
		/// <param name="filename">The name of the file that completed downloading</param>
		internal void RaiseDownloadFileCompeted(string filename){
			//Inform caller
			if (this.DownloadFileCompleted != null)
				this.DownloadFileCompleted (filename);
		}
		#endregion 

		#region Events
		/// <summary>
		/// Occurs when download error.
		/// </summary>
		/// <param name="message">
		/// Contains the error message that occurred while downloading this file
		/// </param>
		public delegate void DownloadErrorDelegate(string message);
		public event DownloadErrorDelegate DownloadError;
		
		/// <summary>
		/// Occurs when download canceled.
		/// </summary>
		/// <param name="filename">
		/// The name of the file that was currently being processed
		/// </param>
		public delegate void DowloadCanceledDelegate(string filename);
		public event DowloadCanceledDelegate DownloadCanceled;
		
		/// <summary>
		/// Occurs when download file started.
		/// </summary>
		/// <param name="filename">
		/// The name of the file that was currently being processed
		/// </param>
		public delegate void DownloadFileStartedDelegate(string filename);
		public event DownloadFileStartedDelegate DownloadFileStarted;
		
		/// <summary>
		/// Occurs when download file completed.
		/// </summary>
		/// <param name="filename">
		/// The name of the file that was currently being processed
		/// </param>
		public delegate void DownloadFileCompletedDelegate(string filename);
		public event DownloadFileCompletedDelegate DownloadFileCompleted;
		#endregion
	}
}

