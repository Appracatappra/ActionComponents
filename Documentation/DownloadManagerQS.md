# Action Download Manager Component
## Getting Started with Action Download Manager

To use an **Action Download Manager** in your mobile app include the `ActionComponents.dll` and reference the following using statement in your C# code:

```csharp
using ActionComponents;
```

## Adding Files to the Download Queue

To use **Action Download Manager** add one or more `ACDownloadItems` representing files to be downloaded from the internet to your `ACDownloadManager` class.

###iOS Example:

```csharp
//Specify the directory to download the file to
string directory=Environment.GetFolderPath(Environment.SpecialFolder.Personal);

//Queue up a file to download: Source URL, Directory to download to and optionally renaming the file
_downloadManager.QueueFile("http://appracatappra.com/wp-content/plugins/download-monitor/download.php?id=4",directory,"NDA.pdf");
```

### Android Example:

```csharp
// Create path to hold downloaded files
string directory = System.Environment.GetFolderPath (System.Environment.SpecialFolder.Personal);
... 

// Queue up files to download
downloadManager.QueueFile ("http://appracatappra.com/wp-content/uploads/et_temp/ssh-140751_232x117.jpg",directory);
downloadManager.QueueFile ("http://appracatappra.com/wp-content/uploads/et_temp/4-TD-web-5-662620_960x332.png",directory);
```

## Working with Files on a FTP Server

While **Action Download Manager** is able to download files from an HTTP server, for the best performance it is suggested to store your files on a FTP server instead. This is especially true of large files like video or animations.

Currently **Action Download Manager** is unable to query the FTP server for the structure of the files and folders on the server. However, you can store a known index file on the server in a format such as JSON or XML, read that file from the server first and then use it to specify all of the other files required.

When dealing with files on an FTP server, you might need to provide a user ID and Password. **Action Download Manager** is capable of handling files in this situation by using the following code:

```csharp
// Build a path to the file of the FTP server
var uri = new Uri("ftp://username:password@0.0.0.0/folder/.../filename.ext");

// Queue file
downloadManager.QueueFile (uri.ToString(),directory);
```

Where:

* **username** – is your user name.
* **password** – is your password.
* **0.0.0.0** – is your ftp server’s IP address.
* **/folder/…** – would be any folder structure leading down to the filename.
* **filename.ext** – would be the file that you want to download.

## Handling Download Events

**Action Download Manager** provides several events to provide feedback during the downloading of files such as **FileDownloadProgressPercent**, **DownloadError** and **AllDownloadsCompleted**. The following are examples of how to use **Action Download Manager** events in your mobile app.

### iOS Example:

```csharp
// Wireup progress bar
_downloadManager.FileDownloadProgressPercent+= (percentage) => {
    // Update GUI
    InvokeOnMainThread(delegate {
        downloadProgress.Progress=percentage;
    });
};

// Wireup completion handler
_downloadManager.AllDownloadsCompleted+= delegate() {
    //Update GUI on main thread
    InvokeOnMainThread(delegate{
        //Display Alert Dialog Box
        using(var alert = new UIAlertView("DownloadManager", "All files have been downloaded", null, "OK", null))
        {
            alert.Show();   
        }
    });
};

// Wireup download error event
_downloadManager.DownloadError += (message) => {
    // Update GUI
    InvokeOnMainThread(delegate{
        // Display Alert Dialog Box
        using(var alert = new UIAlertView("Download Error", message, null, "OK", null))
        {
            alert.Show();   
        }
    });
};
```

### Android Example:

```csharp
private string dialogMessage = "";
...

// Wireup progress bar
downloadManager.FileDownloadProgressPercent += (percentage) => {

    // Adjust percentage and display
    RunOnUiThread (() => {
        percentage *= 100f;
        bar.Progress = (int)percentage;
    });
};

// Wireup completion handler
downloadManager.AllDownloadsCompleted += () => {

    // Run on UI Thread
    RunOnUiThread (() => {
        // Inform caller
        dialogMessage="All files have been downloaded";
        ShowDialog (DialogLongMessage);
    });

};

// Wireup error handler
downloadManager.DownloadError += (message) => {

    // Run on UI Thread
    RunOnUiThread (() => {
        // Inform caller
        dialogMessage=message;
        ShowDialog (DialogLongMessage);
    });
};

...
// Display dialog to the user as requested
protected override Dialog OnCreateDialog (int id)
{
    Dialog alert = null;

    base.OnCreateDialog (id);

    // Build requested dialog type
    switch (id){
    case DialogLongMessage:
        var builder = new AlertDialog.Builder(this);
        builder.SetIcon (Android.Resource.Attribute.AlertDialogIcon);
        builder.SetTitle ("DownloadManager");
        builder.SetMessage(dialogMessage);
        builder.SetPositiveButton ("OK", delegate(object sender, DialogClickEventArgs e) {
            // Ignore for now
        });
        alert=builder.Create ();
        break;
    }

    // Return dialog
    return alert;
}
```

## Aborting on Errors

If the `abortOnError` property of the **Action Download Manager** is `true`, the **Action Download Manager** will stop executing a batch of files if any file generates an error during the download process. This is the default setting.

## Starting the Download

After you have set the collection of files to download and wired up the user feedback, use the following code to start your downloads.

### iOS Example:

```csharp
//Start the download process
_downloadManager.StartDownloading();
```

### Android Example:

```csharp
// Start the download process
ThreadPool.QueueUserWorkItem((callback) =>{
    downloadManager.StartDownloading();
});
```

# Trial Version

The trial version of **Action Download Manager** is limited to downloading a batch of no more than five (5) files at a time and then only image files in the `.jpg` format. The full version removes these restrictions.