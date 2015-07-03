using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace CabinetAutomation.Cix
{
	class FinderUpdater
	{
		public readonly String directory;

		public FinderUpdater(String directory)
		{
			this.directory = directory;
		}

		public void FindAndUpdate()
		{
			String[] list = Directory.GetFiles(this.directory, "*.cix", SearchOption.AllDirectories);

			foreach (String file in list)
			{
				new Updater(file).DoMagic();
			}

			Process.Start(this.directory);
		}
	}
}
