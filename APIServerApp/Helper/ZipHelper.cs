using System.IO.Compression;

public static class ZipHelper
{
    public static string ExtractFirstFile(string zipPath, string extractFolder)
    {
        if (!System.IO.File.Exists(zipPath))
            throw new FileNotFoundException("Không tìm thấy file zip", zipPath);

        Directory.CreateDirectory(extractFolder);

        using (var archive = System.IO.Compression.ZipFile.OpenRead(zipPath))
        {
            var entry = archive.Entries.FirstOrDefault();
            if (entry == null)
                throw new InvalidOperationException("File zip rỗng");

            string extractedPath = Path.Combine(extractFolder, entry.FullName);

            entry.ExtractToFile(extractedPath, overwrite: true);

            return extractedPath;
        }
    }
}
