using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace RmiterUwp
{
    // Since the WebView control needs too much nasty workarounds to bind raw HTML string,
    //  so I prefer save the HTML string to a text file in temporary directory and then 
    //  pass the path to the HTML file to render the content into UI.

    // By the way we need to notice that the HTML file must be storaged into a sub-path, 
    //   otherwise WebView control cannot read it. 
    // See: http://stackoverflow.com/questions/42417997/display-local-html-file-content-in-uwp-webview

    public class AnnouncementHandler
    {
        public static async Task<string> SaveHtmlStringToFileAsync(string htmlStr)
        {
            // Select the temporary folder
            var tempFolder = await ApplicationData.Current.TemporaryFolder.CreateFolderAsync("annoucement-html", CreationCollisionOption.OpenIfExists);

            // Create a new HTML file name with UUID and ".htm" prefix
            string fileName = Guid.NewGuid().ToString() + ".htm";

            // Prepare a CSS to improve the damn default serif fonts (Time News Roman again???)
            string cssFonts = "<style>\n" + 
                                "p {\n" +
                                    "font-family: Arial, Helvetica, sans-serif;\n" +
                                    "font-size: 20px;\n"+ 
                                "}\n" +
                              "</style>\n";

            // The announcement html string contains some strange Unicode symbol, 
            // including some new-line symbols ("↵") and double quotation marks, which need to be removed. 
            string htmlFinalStr = htmlStr.Insert(0, "<!DOCTYPE html>\n" + cssFonts).Replace(@"↵", "").Replace("\"", "");

            // Create the file and write HTML content into it.
            var htmlFile = await tempFolder.CreateFileAsync(fileName);
            await FileIO.WriteTextAsync(htmlFile, htmlFinalStr);
            return fileName;
        }

        public static async Task DeleteHtmlFileAsync(string fileName)
        {
            // Select the temporary folder
            var tempFolder = await ApplicationData.Current.TemporaryFolder.GetFolderAsync("annoucement-html");

            // Select the file
            var htmlFile = await tempFolder.GetFileAsync(fileName);

            // Remove the file
            await htmlFile.DeleteAsync();
        }
    }
}
