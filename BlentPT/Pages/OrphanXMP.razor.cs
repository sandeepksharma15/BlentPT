using System;
using System.IO;
using System.Windows.Forms;

namespace BlentPT.Pages;

public partial class OrphanXMP
{
    private string scanFolder = string.Empty;

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

}