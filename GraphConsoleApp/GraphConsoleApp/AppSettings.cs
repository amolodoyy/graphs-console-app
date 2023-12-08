using System.Net.Sockets;
using Microsoft.VisualBasic;

namespace GraphConsoleApp;
static class AppSettings
{
    private static char sp = Path.DirectorySeparatorChar;
    /* PROD: */

    //public static string SourceDirectoryPath = $".{sp}Graphs{sp}";

    /* DEV: */

    public static string SourceDirectoryPath = $"..{sp}..{sp}..{sp}Graphs{sp}";
}
