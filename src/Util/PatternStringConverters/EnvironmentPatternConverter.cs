#region Copyright & License
//
// Copyright 2001-2004 The Apache Software Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System;
using System.Text;
using System.IO;

using log4net.Util;
using log4net.DateFormatter;
using log4net.Core;

namespace log4net.Util.PatternStringConverters
{
	/// <summary>
	/// Environment pattern converter expamds environment variables
	/// </summary>
	/// <author>Nicko Cadell</author>
	internal sealed class EnvironmentPatternConverter : PatternConverter
	{
		/// <summary>
		/// Convert the pattern into the rendered message
		/// </summary>
		/// <param name="writer">the writer to write to</param>
		/// <param name="state">null, state is not set</param>
		override protected void Convert(TextWriter writer, object state) 
		{
			try 
			{
				if (this.Option != null && this.Option.Length > 0)
				{
					// Lookup the environement vairable
					string envValue = Environment.GetEnvironmentVariable(this.Option);
					if (envValue != null && envValue.Length > 0)
					{
						writer.Write(envValue);
					}
				}
			}
			catch(System.Security.SecurityException secEx)
			{
				// This security exception will occur if the caller does not have 
				// unrestricted environment permission. If this occurs the expansion 
				// will be skipped with the following warning message.
				LogLog.Debug("DOMConfigurator: Security exception while trying to expand environment variables. Error Ignored. No Expansion.", secEx);
			}
			catch (Exception ex) 
			{
				LogLog.Error("EnvironmentPatternConverter: Error occurred while converting environment variable.", ex);
			}
		}
	}
}
