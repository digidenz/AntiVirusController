namespace AntiVirusWebApi.Scanners
{
	using Models;

	public interface IScanner
	{
		Detection ScanByFilePath(string filePath);
	}
}
