using System;

namespace Rainbow.Setup
{
	/// <summary>
	/// IInstaller inteface is used by installable modules
	/// </summary>
	public interface IInstaller
	{
		void Install(System.Collections.IDictionary stateSaver);
		void Uninstall(System.Collections.IDictionary stateSaver);
		void Commit(System.Collections.IDictionary stateSaver);
		void Rollback(System.Collections.IDictionary stateSaver);
	}
}