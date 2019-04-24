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

namespace HugePointCloudTester
{
	///<summary>
	/// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
	/// class. DO NOT create instances of this class yourself. It is the
	/// responsibility of Rhino to create an instance of this class.</para>
	/// <para>To complete plug-in information, please also see all PlugInDescription
	/// attributes in AssemblyInfo.cs (you might need to click "Project" ->
	/// "Show All Files" to see it in the "Solution Explorer" window).</para>
	///</summary>
	public class HugePointCloudTesterPlugIn : Rhino.PlugIns.PlugIn

	{
		public HugePointCloudTesterPlugIn()
		{
			Instance = this;
		}

		///<summary>Gets the only instance of the HugePointCloudTesterPlugIn plug-in.</summary>
		public static HugePointCloudTesterPlugIn Instance
		{
			get; private set;
		}

		// You can override methods here to change the plug-in behavior on
		// loading and shut down, add options pages to the Rhino _Option command
		// and maintain plug-in wide options in a document.
	}
}
