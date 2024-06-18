using System.IO;

namespace BlentPT.Pages;

public partial class OrphanXMP
{
    private string scanFolder = string.Empty;
    private int progressBarWidth = 0;
    private readonly List<string> orphanFiles = [];
    private bool includeSubFolders = true;
    private string scannedFiles = string.Empty;
    private string progressMessage = string.Empty;

    private void SelectFolder(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
    {
        using (var dialog = new FolderBrowserDialog())
        {
            dialog.Description = "Select a folder";
            dialog.ShowNewFolderButton = true;

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                scanFolder = dialog.SelectedPath;
            }
        }
    }

    private void ScanOrphans(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
    {
        // Reset
        progressBarWidth = 0;
        progressMessage = "Scanning...";
        orphanFiles.Clear();
        scannedFiles = string.Empty;

        // Scan
        var files = Directory.GetFiles(scanFolder, "*.xmp", includeSubFolders
            ? SearchOption.AllDirectories
            : SearchOption.TopDirectoryOnly);
        var fileCount = files.Length;
        var i = 0;

        foreach (var file in files)
        {
            // Update Progress Bar
            progressBarWidth = (int)((double)i++ / fileCount * 100);
            progressMessage = $"Scanning {i} of {fileCount} files...";

            // Check that there is no other file with the same name but different extension, in the same folder
            var fileName = Path.GetFileNameWithoutExtension(file);
            var folder = Path.GetDirectoryName(file);
            var otherFiles = Directory.GetFiles(folder, $"{fileName}.*");

            if (otherFiles.Length == 1)
            {
                orphanFiles.Add(file);
                scannedFiles += $"{file}\n";
            }

            StateHasChanged();
        }

        progressBarWidth = 100;
        progressMessage = orphanFiles.Count == 0
            ? "No orphan files found."
            : $"{orphanFiles.Count} orphan files found.";

        StateHasChanged();
    }

    private void DeleteOrphanFiles(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
    {
        // Reset
        progressBarWidth = 0;
        progressMessage = "Deleting...";
        var i = 0;

        foreach (var file in orphanFiles)
        {
            // Update Progress Bar
            progressBarWidth = (int)((double)i++ / orphanFiles.Count * 100);
            progressMessage = $"Deleting {i} of {orphanFiles.Count} files...";

            try
            {
                // Delete
                File.Delete(file);
            }
            catch (Exception ex)
            {
                // Display the exception message in message box
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            StateHasChanged();
        }

        progressBarWidth = 100;
        progressMessage = "Orphan files deleted.";
        orphanFiles.Clear();
        scannedFiles = string.Empty;
        StateHasChanged();
    }
}