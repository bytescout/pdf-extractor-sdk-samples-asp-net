//*****************************************************************************************//
//                                                                                         //
// Download offline evaluation version (DLL): https://bytescout.com/download/web-installer //
//                                                                                         //
// Signup Web API free trial: https://secure.bytescout.com/users/sign_up                   //
//                                                                                         //
// Copyright © 2017-2018 ByteScout Inc. All rights reserved.                               //
// http://www.bytescout.com                                                                //
//                                                                                         //
//*****************************************************************************************//


using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using Bytescout.PDFExtractor;

namespace ExtractImages
{
	public partial class _Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			String inputFile = Server.MapPath(@".\bin\sample1.pdf");

			// Create Bytescout.PDFExtractor.ImageExtractor instance
			ImageExtractor extractor = new ImageExtractor();
			extractor.RegistrationName = "demo";
			extractor.RegistrationKey = "demo";
			
			// Load sample PDF document
			extractor.LoadDocumentFromFile(inputFile);

			// Array to keep extracted images
			List<byte[]> extractedImages = new List<byte[]>();

			// Initialize image enumeration
			if (extractor.GetFirstImage())
			{
				do
				{
					// Extract image to memory
					using (MemoryStream memoryStream = new MemoryStream())
					{
						extractor.SaveCurrentImageToStream(memoryStream, ImageFormat.Png);
						// Keep image as byte array
						extractedImages.Add(memoryStream.ToArray());
					}
				} 
				while (extractor.GetNextImage()); // Advance image enumeration
			}
			
			// Write first image to the output stream

			Response.Clear();

			Response.ContentType = "image/png";
			Response.AddHeader("Content-Disposition", "inline;filename=image.png");

			Response.BinaryWrite(extractedImages[0]);
			
			Response.End();
		}
	}
}
