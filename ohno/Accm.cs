using System;
using System.Threading;
using OpenTK;
using System.IO;

namespace ohno
{
	public class Accm
	{
		Timer poller;

		public Vector3 Data;
		public Vector3 NullData;
		public Vector3 Raw;
		public bool nulled = false;
	
		public Accm ()
		{
			poller = new Timer (poll, null, 100, 100);

		}

		private void poll(object a) {
			string data;
			string[] coords;

			using(StreamReader reader = new StreamReader("/sys/devices/platform/lis3lv02d/position")) {
				data = reader.ReadToEnd ();
			}

			coords = data.Trim().TrimStart ('(').TrimEnd (')').Split (',');

			if (coords.Length == 3) {
				Raw = new Vector3 (
					float.Parse (coords [0]),
					float.Parse (coords [1]),
					float.Parse (coords [2]));
				if (nulled) {
					NullData = Raw;
					nulled = false;
				} else {
					Data = Raw - NullData;
				}
			}
		}
	}
}

