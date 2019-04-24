/*
 
 Copyright 2019 Nathan Letwory

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
limitations under the License.

*/

using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Collections;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;

namespace HugePointCloudTester
{
	public class HugePointCloudTesterCommand : Command
	{
		public HugePointCloudTesterCommand()
		{
			// Rhino only creates one instance of each command class defined in a
			// plug-in, so it is safe to store a refence in a static property.
			Instance = this;
		}

		///<summary>The only instance of this command.</summary>
		public static HugePointCloudTesterCommand Instance
		{
			get; private set;
		}

		///<returns>The command name as it appears on the Rhino command line.</returns>
		public override string EnglishName
		{
			get { return "HugePointCloudTester"; }
		}

		protected override Result RunCommand(RhinoDoc doc, RunMode mode)
		{
			Random r = new Random(13);
			var plcnt = 1000000;
			var edge = 10;

			GetNumber gn = new GetNumber();
			gn.SetCommandPrompt("How many pointsies");
			gn.SetDefaultInteger(plcnt);
			gn.SetUpperLimit(500000001, true);
			gn.SetLowerLimit(1, true);
			gn.AcceptNothing(true);

			var gnrc = gn.Get();
			if (gnrc == GetResult.Nothing || gnrc == GetResult.Number)
			{
				var nr = (int)gn.Number();
				if (nr > 500000000)
				{
					RhinoApp.WriteLine("More than 500.000.000 points");
					return Result.Cancel;
				}
				PointCloud pc = new PointCloud();
				for (int i = 0; i < nr; i++)
				{
					var d = (double)i;
					pc.Add(
						new Point3d(r.NextDouble() * edge, r.NextDouble() * edge, r.NextDouble() * edge),
						System.Drawing.Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255))
					);
				}

				doc.Objects.AddPointCloud(pc);

				doc.Views.Redraw();
			}

			return Result.Success;
		}
	}
}
