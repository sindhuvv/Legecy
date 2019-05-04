using System.IO;
using E = Aspose.Cells;

namespace Tms.Infrastructure.Export
{
	/// <summary>
	/// Extension class to make working with aspose documents better.
	/// </summary>
	public static class WorkbookExtensions
	{
		/// <summary>
		/// Will get the bytes represented by an aspose excel workbook.
		/// </summary>
		public static byte[] GetBytes(this E.Workbook book)
		{
			using (var stream = new MemoryStream())
			using (var reader = new BinaryReader(stream))
			{
				book.Save(stream, E.SaveFormat.Xlsx);

				stream.Position = 0;

				return reader.ReadBytes((int)stream.Length);
			}
		}

		/// <summary>
		/// Will save the workbook to the output stream.  Typically will be used to write directly to the response.
		/// </summary>
		public static void WriteToStream(this E.Workbook workbook, Stream outputStream)
		{
			var stream = new System.IO.MemoryStream();
			workbook.Save(stream, E.SaveFormat.Xlsx);
			using (stream)
			{
				stream.Position = 0;
				byte[] buffer = new byte[0x1000];
				while (true)
				{
					int count = stream.Read(buffer, 0, 0x1000);
					if (count == 0)
					{
						break;
					}
					outputStream.Write(buffer, 0, count);
				}
			}
		}
	}
}
