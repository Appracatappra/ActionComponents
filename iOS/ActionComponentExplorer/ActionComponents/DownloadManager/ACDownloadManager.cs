using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Threading;

namespace ActionComponents
{
	/// <summary>
	/// This object handles the downloading of open of more files from the internet.
	/// It provides events for handling progress, suspension and restart, and error
	/// hanlding routines.
	/// </summary>
	public class ACDownloadManager
	{
		//Private properties
		private WebClient _client;
		private Queue<ACDownloadItem> _downloadItems;
		private bool _abortOnError=true;
		
		//Private download properties
		private ACDownloadItem _downloading;
		private bool _suspended=false;
		private ACDownloadItem _suspendedDownload;
		private int _initialFileCount;
		
		#region Computed Properties
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ActionComponentsr.ACDownloadManager"/> aborts
		/// all active downloads on error.
		/// </summary>
		/// <value><c>true</c> if abort on error; otherwise, <c>false</c>.</value>
		public bool abortOnError{
			get{ return _abortOnError;}
			set{ _abortOnError = value;}
		}

		/// <summary>
		/// Gets the number of files in queue.
		/// </summary>
		/// <value>
		/// The current file count
		/// </value>
		public int filesInQueue {
			get {return _downloadItems.Count;}
		}
		
		/// <summary>
		/// Returns the URL of the file currently being downloaded
		/// </summary>
		/// <value>
		/// The URL of the file being downloaded or empty string ("") if no
		/// active download
		/// </value>
		public string downloadingURL{
			get{
				if (_downloading==null) {
					return "";
				} else {
					return _downloading.URL;
				}
			}
		}
		
		/// <summary>
		/// Returns the filename of the URL currently being downloaded
		/// </summary>
		/// <value>
		/// The filename currently being downloaded or empty string ("") if no
		/// active download
		/// </value>
		public string downloadingFilename{
			get{
				if (_downloading==null) {
					return "";
				} else {
					return _downloading.filename;
				}
			}
		}
		
		/// <summary>
		/// Gets a value indicating whether this <see cref="ActionComponentsr"/> is busy.
		/// </summary>
		/// <value>
		/// <c>true</c> if is busy; otherwise, <c>false</c>.
		/// </value>
		public bool isBusy{
			get{return _client.IsBusy;}
		}
		
		/// <summary>
		/// Gets a value indicating whether this <see cref="ActionComponentsr"/> is suspended.
		/// </summary>
		/// <value>
		/// <c>true</c> if is suspended; otherwise, <c>false</c>.
		/// </value>
		public bool isSuspended {
			get{return _suspended;}
		}
		#endregion
		
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionComponentsr"/> class.
		/// </summary>
		public ACDownloadManager ()
		{
			//Initialize
			Initialize();
		}
		
		/// <summary>
		/// Initialize this instance.
		/// </summary>
		private void Initialize(){
			//Create storage space
			_downloadItems=new Queue<ACDownloadItem>();
			
			//Create web client to handle downloads and attach
			//listeners
			_client=new WebClient();
			_client.DownloadFileCompleted+= delegate(object sender, System.ComponentModel.AsyncCompletedEventArgs e) {
				
				//Take action based on the state of the current downloaded file
				if (e.Error!=null) {
					string message= "";

					if (_downloading==null) {
						message=e.Error.Message;
					} else {
						message=String.Format("{0} downloading {1}",e.Error.Message,_downloading.filename);
					}
					
					//Inform caller of download file error
					if (this.DownloadError!=null) this.DownloadError(message);
					Console.WriteLine(message);

					//Is there an error event for this item?
					if (_downloading !=null) _downloading.RaiseDownloadError(message);

					//Should we terminate downloads on error?
					if (abortOnError) {
						//Yes
						ClearQueue();
						return;
					}
				} else if (e.Cancelled) {
					//Inform caller that the downloading of the current file
					//was canceled
					if (this.DownloadCanceled!=null) this.DownloadCanceled(_downloading.filename);
					Console.WriteLine(String.Format("Canceled downloading {0}",_downloading.filename));

					//Is there a cancel event for this item?
					if (_downloading !=null) _downloading.RaiseDownloadCanceled(_downloading.filename);

					//Return to caller
					return;
				} else {
					//Inform the caller that the file was downloaded successfully
					if (this.DownloadFileCompleted!=null) this.DownloadFileCompleted(_downloading.filename);
					Console.WriteLine(String.Format("Downloaded {0}",_downloading.filename));

					//Is there a completed event for this item?
					if (_downloading !=null) _downloading.RaiseDownloadFileCompeted(_downloading.filename);
				}
				
				//Attempt to download the next file in the queue
				if (!_suspended) DownloadNextFileInQueue();
			};
			_client.DownloadProgressChanged+= delegate(object sender, DownloadProgressChangedEventArgs e) {
				//Inform caller of percentage change
				if (this.FileDownloadProgressPercent!=null) this.FileDownloadProgressPercent(e.ProgressPercentage*0.01f);
			};
			
		}
		#endregion 
		
		#region Public Methods
		/// <summary>
		/// Clears the queue.
		/// </summary>
		public void ClearQueue(){
			
			//Empty current queue
			_downloading=null;
			_downloadItems.Clear();
		}
		
		/// <summary>
		/// Adds the given download url and directory to the queue of files to download
		/// </summary>
		/// <param name='url'>
		/// The URL of the file to download
		/// </param>
		/// <param name="downloadDirectory">
		/// The directory to download the file to
		/// </param>
		public void QueueFile(string url,string downloadDirectory){
			_downloadItems.Enqueue(new ACDownloadItem(url,downloadDirectory));
		}

		/// <summary>
		/// Adds the given download url and directory to the queue of files to download
		/// </summary>
		/// <param name='url'>
		/// The URL of the file to download
		/// </param>
		/// <param name="downloadDirectory">
		/// The directory to download the file to
		/// </param>
		/// <param name="destinationFilename">Specifies a new name for the file when it is downloaded</param>
		public void QueueFile(string url,string downloadDirectory,string destinationFilename){
			_downloadItems.Enqueue(new ACDownloadItem(url,downloadDirectory,destinationFilename));
		}

		/// <summary>
		/// Adds the given download item to the queue of files to download
		/// </summary>
		/// <param name="item">The download item to add</param>
		public void QueueFile(ACDownloadItem item){
			_downloadItems.Enqueue (item);
		}
		
		/// <summary>
		/// Aborts the download.
		/// </summary>
		public void AbortDownload(){
			
			//Are we currently downloading a file?
			if (_client.IsBusy) _client.CancelAsync ();
			
			//Clear any queued files
			ClearQueue();
			
		}

		/// <summary>
		/// Start downloading all files currently in the queue
		/// </summary>
		public void StartDownloading()
		{

			//Anything to do?
			if (!_downloadItems.Any()) return;

			//Clear any suspended downloads
			_suspendedDownload = null;
			_suspended = false;

			//Save the initial number of files
			_initialFileCount = filesInQueue;

			//Kickoff the download process
			DownloadNextFileInQueue();

#if TRIAL
			ACToast.MakeText("DownloadManager by Appracatappra, LLC.", ACToastLength.Long, ACToastGravity.Center).Show();
#else
			AppracatappraLicenseManager.ValidateLicense();
#endif
		}
		
		/// <summary>
		/// Suspends the download of any currently active file
		/// </summary>
		public void SuspendDownload(){
			
			//Is a download currently running?
			if (!_client.IsBusy) return;
			
			//Mark suspended and remember suspended URL
			_suspended=true;
			_suspendedDownload=_downloading;
			Console.WriteLine(String.Format("Suspending downloading of {0}",_downloading.filename));
			
			//Terminate current download
			_client.CancelAsync();
		}
		
		/// <summary>
		/// Resumes the downloading of any suspended file download
		/// </summary>
		public void ResumeDownloading(){
			string fullPath="";
			
			//Was a download suspended?
			if (!_suspended) return;
			
			//Reset download state
			_downloading=_suspendedDownload;
			_suspendedDownload=null;
			_suspended=false;
			Console.WriteLine(String.Format("Resume downloading of {0}",_downloading.filename));
			
			//Kick-off downloading of suspended file
			//Form a path to the file
			fullPath=Path.Combine(_downloading.downloadDirectory,_downloading.filename);

			//Inform caller of percentage change
			if (this.FileDownloadProgressPercent!=null) this.FileDownloadProgressPercent(0.0f);
			
			//Start downloading of the file
			_client.DownloadFileAsync (new Uri(_downloading.URL),fullPath);

			//Inform caller of percentage complete
			ReportOverallPercentComplete ();

			//Is there a start event attached to this item?
			_downloading.RaiseDownloadFileStarted(_downloading.filename);
		}
		#endregion 
		
		#region Private Methods
		/// <summary>
		/// For the trial version of DownloadManage limit to only 5 files in a batch
		/// and only .jpg files. This function tests those conditions and informs the
		/// user if the file cannot be added to the queue
		/// </summary>
		/// <returns><c>true</c>, if the file meets trial limit conditions, <c>false</c> otherwise.</returns>
		/// <param name="URL">The source URL for the file to download</param>
		private bool PassesTrialLimits(string URL) {
			bool good = true;

			//Trial version is limited to 5 items in the queue
			if (filesInQueue >= 5)
				good = false;

			//Trial version can only download .jpg images
			if (!URL.ToLower ().Contains (".jpg"))
				good = false;

			//If this file is not good for the trial,
			//inform user
			if (!good) {
				//Build error message
				string message=String.Format("Trial Warning on '{0}': The trail version of DownloadManager only supports up to 5 files in a batch and jpg images.",_downloading.filename);

				//Inform caller of download file error
				if (this.DownloadError!=null) this.DownloadError(message);
				Console.WriteLine(message);
				
				//Is there an error event for this item?
				_downloading.RaiseDownloadError(message);
			}

			//Else the file is good for the trial limit
			return good;
		}

		/// <summary>
		/// Reports the overall percent complete.
		/// </summary>
		private void ReportOverallPercentComplete(){

			//Calculate percent complete
			float percentage = ((float)filesInQueue) / ((float)_initialFileCount);

			//Inform caller of the overall percentage completed
			if (this.OverallDownloadProgressPercent != null)
				this.OverallDownloadProgressPercent (percentage);
		}

		/// <summary>
		/// Downloads the next file in queue.
		/// </summary>
		private void DownloadNextFileInQueue(){
			string fullPath="";
			
			//Any files in queue?
			if (_downloadItems.Any ()){
				//Grab next file to download
				_downloading=_downloadItems.Dequeue ();
				
				//Form a path to the file
				fullPath=Path.Combine(_downloading.downloadDirectory,_downloading.filename);
				
				//Start downloading of the file
				_client.DownloadFileAsync (new Uri(_downloading.URL),fullPath);

				//Inform caller of percentage complete
				ReportOverallPercentComplete ();

				//Inform call that a new file has started downloading
				if (this.DownloadFileStarted!=null) this.DownloadFileStarted(_downloading.filename);

				//Is there a start event attached to this item?
				_downloading.RaiseDownloadFileStarted(_downloading.filename);

			} else {
				//Clear current file
				_downloading=null;
				
				//Inform caller that all downloads have been completed
				if (this.AllDownloadsCompleted!=null) this.AllDownloadsCompleted();
			}
			
		}
		#endregion
		
		#region Events
		/// <summary>
		/// Occurs when all downloads completed.
		/// </summary>
		public delegate void AllDownloadsCompletedDelegate();
		public event AllDownloadsCompletedDelegate AllDownloadsCompleted;
		
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
		
		/// <summary>
		/// Occurs when file download progress percent is updated.
		/// </summary>
		/// <param name="percentage">
		/// The decimal percentage from 0 to 1 that the process has completed
		/// </param>
		public delegate void FileDownloadProgressPercentDelegate(float percentage);
		public event FileDownloadProgressPercentDelegate FileDownloadProgressPercent;

		/// <summary>
		/// Occurs when overall download progress percent is updated.
		/// </summary>
		/// <param name="percentage">
		/// The decimal percentage from 0 to 1 that the process has completed
		/// </param>
		public delegate void OverallDownloadProgressPercentDelegate(float percentage);
		public event OverallDownloadProgressPercentDelegate OverallDownloadProgressPercent;
		#endregion
	}
}

