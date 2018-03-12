using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Collections;
using Rhino.Commands;
using Rhino.Display;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;

namespace HugePointCloudConduitTester
{

	public class HPCConduit : Rhino.Display.DisplayConduit
	{
		protected override void PostDrawObjects(DrawEventArgs e)
		{
			if(ThePc != null) e.Display.DrawPointCloud(ThePc, 0.01f);
		}
		public PointCloud ThePc { get; set; } = null;
	}
	public class HugePointCloudConduitTesterCommand : Command
	{
		public HugePointCloudConduitTesterCommand()
		{
			// Rhino only creates one instance of each command class defined in a
			// plug-in, so it is safe to store a refence in a static property.
			Instance = this;
			cnd.Enabled = false;
		}

		///<summary>The only instance of this command.</summary>
		public static HugePointCloudConduitTesterCommand Instance
		{
			get; private set;
		}

		///<returns>The command name as it appears on the Rhino command line.</returns>
		public override string EnglishName
		{
			get { return "HugePointCloudConduitTester"; }
		}

		HPCConduit cnd = new HPCConduit();
		PointCloud pc = new PointCloud();

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
				pc = new PointCloud();
				for (int i = 0; i < nr; i++)
				{
					var d = (double)i;
					pc.Add(
						new Point3d(r.NextDouble() * edge, r.NextDouble() * edge, r.NextDouble() * edge),
						System.Drawing.Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255))
					);
				}
				cnd.ThePc = pc;
				cnd.Enabled = true;
			}

			doc.Views.Redraw();


			return Result.Success;
		}
	}
}
