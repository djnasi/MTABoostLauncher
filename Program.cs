using System.Diagnostics;
using System.IO;
using System.Threading;

Console.WriteLine("IP gir (127.0.0.1:22003):");
string ip = Console.ReadLine();

if (string.IsNullOrWhiteSpace(ip))
{
    Console.WriteLine("IP boş olamaz.");
    return;
}

string configPath = Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
    @"MTA San Andreas 1.6\coreconfig.xml");

if (File.Exists(configPath))
{
    string config = File.ReadAllText(configPath);
    config = config.Replace("<vsync>true</vsync>", "<vsync>false</vsync>");
    config = config.Replace("<antialiasing>2</antialiasing>", "<antialiasing>0</antialiasing>");
    File.WriteAllText(configPath, config);
}

Process.Start("mtasa://" + ip);
Thread.Sleep(3000);

foreach (var p in Process.GetProcessesByName("gta_sa"))
{
    try { p.PriorityClass = ProcessPriorityClass.High; }
    catch { }
}
