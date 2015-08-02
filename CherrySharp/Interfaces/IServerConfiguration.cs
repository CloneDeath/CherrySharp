namespace CherrySharp.Interfaces{
	public interface IServerConfiguration{
		string Protocol { get; }
		int Port { get; }
	}
}