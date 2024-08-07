using Microsoft.JSInterop;
using System.IO;
using System.Threading.Tasks;
namespace StaffMgtBlazorApp.Utilities
{
    public static class FileUtil
    {
        public static async Task SaveAs(string fileName, MemoryStream stream, IJSRuntime jsRuntime)
        {
            var fileContent = stream.ToArray();
            var fileUrl = "data:application/octet-stream;base64," + Convert.ToBase64String(fileContent);
            await jsRuntime.InvokeVoidAsync("downloadFile", fileName, fileUrl);
        }
    }
}
