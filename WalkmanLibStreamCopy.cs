using System;
using System.ComponentModel;
using System.IO;

public partial class WalkmanLib {
    // disable if NoOokii is defined (default for tests project)
#if !NoOokii

    /// <summary>
    /// Asynchronously Copy a stream with a progress dialog. Uses <see cref="Ookii.Dialogs.ProgressDialog"/> for the dialog.
    /// <br />NOTE: As this function exits when the copy process starts, the streams must NOT be closed e.g. by a <see langword="using"/> statement.
    /// Streams are always disposed if this function succeeds in starting the dialog.
    /// </summary>
    /// <param name="source">Stream to copy from. Must support Reading</param>
    /// <param name="target">Stream to copy to. Must support Writing</param>
    /// <param name="description">Optional description to display in the ProgressDialog.</param>
    /// <param name="title">Optional title to use for the ProgressDialog.</param>
    /// <param name="onComplete">Optional event handler to run when the Asnyc operation is complete</param>
    public static void StreamCopy(Stream source, Stream target, string description = " ", string title = "Copying...",
                                  RunWorkerCompletedEventHandler onComplete = null) {
        if (source == null) throw new ArgumentNullException("source");
        if (target == null) throw new ArgumentNullException("target");

        if (!source.CanRead || !target.CanWrite) {
            throw new InvalidOperationException("Either Read from Source or Write to Target isn't possible!");
        } else if (source.Length == 0) {
            target.Write(new byte[0], 0, 0);
            target.SetLength(0);

            source.Dispose();
            target.Dispose();
            return;
        }

        var progressDialog = new Ookii.Dialogs.ProgressDialog {
            Text = " ",
            Description = description,
            WindowTitle = title,
            CancellationText = "Cancelling...",
            ShowTimeRemaining = true
        };

        progressDialog.DoWork += doStreamCopy;
        progressDialog.RunWorkerCompleted += onComplete;

        progressDialog.Show(new object[] {progressDialog, source, target});
    }

    private static void doStreamCopy(object sender, DoWorkEventArgs e) {
        object[] inputs = (object[])e.Argument;

        var progressDialog = (Ookii.Dialogs.ProgressDialog)inputs[0];
        var sourceStream = (Stream)inputs[1];
        var targetStream = (Stream)inputs[2];

        try { // double-buffer code/idea thanks to https://stackoverflow.com/a/26556205/2999220
            int bufferSize = 1024 * 1024;
            byte[] buffer = new byte[bufferSize];
            byte[] buffer2 = new byte[bufferSize];
            //byte[] buffer = new byte[bufferSize + 1];
            //byte[] buffer2 = new byte[bufferSize + 1];
            bool swap = false;
            int oldPercent = 0;
            int newPercent = 0;
            int bytesRead = 0;

            long len = sourceStream.Length;
            float flen = len;
            System.Threading.Tasks.Task writer = null;
            long size = 0;

            while (size < len) {
                if (progressDialog.CancellationPending)
                    throw new OperationCanceledException("Operation was canceled by the user");

                newPercent = (int)(size / flen * 100);
                if (newPercent != oldPercent) {
                    progressDialog.ReportProgress(newPercent, "Progress: " + newPercent + "%", null);
                    oldPercent = newPercent;
                }

                bytesRead = sourceStream.Read(swap ? buffer : buffer2, 0, bufferSize);
                if (writer != null) writer.Wait();
                writer = targetStream.WriteAsync(swap ? buffer : buffer2, 0, bytesRead);
                swap = !swap;
                size += bytesRead;

                if (progressDialog.CancellationPending)
                    throw new OperationCanceledException("Operation was canceled by the user");
            }
            if (writer != null) writer.Wait();
        } finally {
            progressDialog.ReportProgress(100, "Flushing data to disk...", null);
            if (sourceStream != null) sourceStream.Dispose();
            if (targetStream != null) targetStream.Dispose();
        }
    }

#endif
}
