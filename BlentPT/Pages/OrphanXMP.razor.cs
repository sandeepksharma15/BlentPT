namespace BlentPT.Pages;

public partial class OrphanXMP
{
    private string scanFolder = string.Empty;
    private int progressBarWidth = 50;
    private readonly List<string> orphanFiles = [];
    private bool includeSubFolders = true;
    private string scannedFiles = string.Empty;

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

    private Task ScanOrphans(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
    {
        throw new NotImplementedException();
    }

    private Task DeleteOrphanFiles(Microsoft.AspNetCore.Components.Web.MouseEventArgs e)
    {
        throw new NotImplementedException();
    }
}