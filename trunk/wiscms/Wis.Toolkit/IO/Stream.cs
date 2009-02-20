//------------------------------------------------------------------------------
// <copyright file="Stream.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.IO
{
	/// <summary>
	/// Stream 的摘要说明。
	/// </summary>
	public class Stream
	{
		private Stream()
		{
		}

		public static byte[] ConvertToBytes( System.IO.Stream stream ) 
		{ 
			byte[] bytes = null; 
			int iLength = 0; 
                    
			try 
			{ 
				iLength = System.Convert.ToInt32( stream.Length ); 
				bytes = new byte[ iLength ]; 
                        
				stream.Read( bytes, 0, iLength ); 
				stream.Close(); 
                        
			} 
			catch ( System.Exception ex ) 
			{ 
				Kernel.ExceptionAppender.Append(ex);
			}
                    
			return bytes; 
                    
		} 

	}
}
