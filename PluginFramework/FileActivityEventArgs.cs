using System;


namespace TurnScrew.Wiki.PluginFramework
{

    /// <summary>
    /// Contains arguments for the File Activity event.
    /// </summary>
    public class FileActivityEventArgs : EventArgs
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="T:FileActivityEventArgs" /> class.
        /// </summary>
        /// <param name="file">The file that changed, if any (full path).</param>
        /// <param name="oldFileName">The old name of the file, if any (full path).</param>
        /// <param name="directory">The directory that changed, if any (full path).</param>
        /// <param name="oldDirectoryName">The old name of the directory, if any (full path).</param>
        /// <param name="page">The page owning the attachment, if any.</param>
        /// <param name="activity">The activity.</param>
        public FileActivityEventArgs(StFileInfo file, string oldFileName,
            StDirectoryInfo directory, string oldDirectoryName,
            PageInfo page, FileActivity activity)
        {
            File = file;
            OldFileName = oldFileName;
            Directory = directory;
            OldDirectoryName = oldDirectoryName;
            Page = page;
            Activity = activity;
        }

        /// <summary>
        /// Gets the provider.
        /// </summary>
        public IFilesStorageProviderV30 Provider
        {
            get
            {
                if (File != null) return File.Provider;
                else if (Directory != null) return Directory.Provider;
                else return null;
            }
        }

        /// <summary>
        /// Gets the file that changed, if any.
        /// </summary>
        public StFileInfo File { get; private set; }

        /// <summary>
        /// Gets the old name of the file, if any.
        /// </summary>
        public string OldFileName { get; private set; }

        /// <summary>
        /// Gets the directory that changed, if any.
        /// </summary>
        public StDirectoryInfo Directory { get; private set; }

        /// <summary>
        /// Gets the old name of the directory, if any.
        /// </summary>
        public string OldDirectoryName { get; private set; }

        /// <summary>
        /// Gets the page owning the attachment, if any.
        /// </summary>
        public PageInfo Page { get; private set; }

        /// <summary>
        /// Gets the activity.
        /// </summary>
        public FileActivity Activity { get; private set; }

    }

    /// <summary>
    /// Lists legal file activities.
    /// </summary>
    public enum FileActivity
    {
        /// <summary>
        /// A file has been uploaded.
        /// </summary>
        FileUploaded,
        /// <summary>
        /// A file has been renamed.
        /// </summary>
        FileRenamed,
        /// <summary>
        /// A file has been deleted.
        /// </summary>
        FileDeleted,
        /// <summary>
        /// A directory has been created.
        /// </summary>
        DirectoryCreated,
        /// <summary>
        /// A directory has been renamed.
        /// </summary>
        DirectoryRenamed,
        /// <summary>
        /// A directory (and all its contents) has been deleted.
        /// </summary>
        DirectoryDeleted,
        /// <summary>
        /// An attachment has been uploaded.
        /// </summary>
        AttachmentUploaded,
        /// <summary>
        /// An attachment has been renamed.
        /// </summary>
        AttachmentRenamed,
        /// <summary>
        /// An attachment has been deleted.
        /// </summary>
        AttachmentDeleted
    }

}
