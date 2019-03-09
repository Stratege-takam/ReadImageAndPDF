using IronOcr;
using IronOcr.Languages;
using QRCoder;
using ReadImageAndPDF.ViewModel;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReadImageAndPDF.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			var serverpath = Server.MapPath("~/doc/code c.PNG");
			var Ocr = new AdvancedOcr()
			{
				Language = IronOcr.Languages.French.OcrLanguagePack,
				ColorSpace = AdvancedOcr.OcrColorSpace.GrayScale,
				EnhanceResolution = true,
				EnhanceContrast = true,
				CleanBackgroundNoise = true,
				ColorDepth = 4,
				RotateAndStraighten = false,
				DetectWhiteTextOnDarkBackgrounds = false,
				ReadBarCodes = true,
				Strategy = AdvancedOcr.OcrStrategy.Fast,
				InputImageType = AdvancedOcr.InputTypes.Document
			};
			var Result = Ocr.Read(serverpath);
			ViewBag.message = Result.Text;

			serverpath = Server.MapPath("~/doc/carte-danick_takam.pdf");
			Result = Ocr.ReadPdf(serverpath);
			var Barcodes = Result.Barcodes;
			ViewBag.pdf = Result.Text;

			serverpath = Server.MapPath("~/doc/carte-danick_takam.PNG");
			Result = Ocr.Read(serverpath);
			ViewBag.carteImg = Result.Text;

			serverpath = Server.MapPath("~/doc/Diplome d'etude collegiale niveau BAC +2 (CCNB-CANADA).pdf");
			Result = Ocr.ReadPdf(serverpath);
		    Barcodes = Result.Barcodes;
			ViewBag.diplomepdf = Result.Text;
			//ViewBag.diplomepdf = "";
			//foreach (var page in results.Pages)
			//{
			//	// page object
			//	int page_number = page.PageNumber;
			//	String page_text = page.Text;
			//	int page_wordcount = page.WordCount;
			//	List<OcrResult.OcrBarcode> barcodes = page.Barcodes;
			//	System.Drawing.Image page_image = page.Image;
			//	int page_width_px = page.Width;
			//	int page_height_px = page.Height;
			//	foreach (var paragraph in page.Paragraphs)
			//	{
			//		// pages -> paragraphs
			//		int paragraph_number = paragraph.ParagraphNumber;
			//		String paragraph_text = paragraph.Text;
			//		System.Drawing.Image paragraph_image = paragraph.Image;
			//		int paragraph_x_location = paragraph.X;
			//		int paragraph_y_location = paragraph.Y;
			//		int paragraph_width = paragraph.Width;
			//		int paragraph_height = paragraph.Height;
			//		double paragraph_ocr_accuracy = paragraph.Confidence;
			//		string paragraph_font_name = paragraph.FontName;
			//		double paragraph_font_size = paragraph.FontSize;
			//		OcrResult.TextFlow paragrapth_text_direction = paragraph.TextDirection;
			//		double paragrapth_rotation_degrees = paragraph.TextOrientation;
			//		foreach (var line in paragraph.Lines)
			//		{
			//			// pages -> paragraphs -> lines
			//			int line_number = line.LineNumber;
			//			String line_text = line.Text;
			//			ViewBag.diplomepdf += line_text + "\n";
			//			//System.Drawing.Image line_image = line.Image;
			//			//int line_x_location = line.X;
			//			//int line_y_location = line.Y;
			//			//int line_width = line.Width;
			//			//int line_height = line.Height;
			//			//double line_ocr_accuracy = line.Confidence;
			//			//double line_skew = line.BaselineAngle;
			//			//double line_offset = line.BaselineOffset;
			//			//foreach (var word in line.Words)
			//			//{
			//			//	// pages -> paragraphs -> lines -> words
			//			//	int word_number = word.WordNumber;
			//			//	String word_text = word.Text;
			//			//	System.Drawing.Image word_image = word.Image;
			//			//	int word_x_location = word.X;
			//			//	int word_y_location = word.Y;
			//			//	int word_width = word.Width;
			//			//	int word_height = word.Height;
			//			//	double word_ocr_accuracy = word.Confidence;
			//			//	String word_font_name = word.FontName;
			//			//	double word_font_size = word.FontSize;
			//			//	bool word_is_bold = word.FontIsBold;
			//			//	bool word_is_fixed_width_font = word.FontIsFixedWidth;
			//			//	bool word_is_italic = word.FontIsItalic;
			//			//	bool word_is_serif_font = word.FontIsSerif;
			//			//	bool word_is_underlined = word.FontIsUnderlined;
			//			//	foreach (var character in word.Characters)
			//			//	{
			//			//		// pages -> paragraphs -> lines -> words -> characters
			//			//		int character_number = character.CharacterNumber;
			//			//		String character_text = character.Text;
			//			//		System.Drawing.Image character_image = character.Image;
			//			//		int character_x_location = character.X;
			//			//		int character_y_location = character.Y;
			//			//		int character_width = character.Width;
			//			//		int character_height = character.Height;
			//			//		double character_ocr_accuracy = character.Confidence;
			//			//	}
			//			//}
			//		}
			//	}
			//}

			serverpath = Server.MapPath("~/doc/Diplome d'etude collegiale niveau BAC +2 (CCNB-CANADA).PNG");
			Result = Ocr.Read(serverpath);
			ViewBag.diplomeImg = Result.Text;

			serverpath = Server.MapPath("~/doc/permis-danick_takam.pdf");
			Result = Ocr.ReadPdf(serverpath);
			Barcodes = Result.Barcodes;
			ViewBag.permispdf = Result.Text;

			serverpath = Server.MapPath("~/doc/permis-danick_takam.PNG");
			Result = Ocr.Read(serverpath);
			ViewBag.permisImg = Result.Text;

			serverpath = Server.MapPath("~/doc/195710a19f414b6cbf6da3dc58e6e4a4.PNG");
			Result = Ocr.Read(serverpath);
		    var Barcode = Result.Barcodes.Select(b => b.Value);
			ViewBag.qrcode = string.Format("Texte : {0} Barcodes: {1}", Result.Text, String.Join(",", Barcode));

			return View();
		}

		public ActionResult CodeQr()
		{
			InformationViewModel informationViewModel = new InformationViewModel();
			return View(informationViewModel);
		}

		[HttpPost]
		public ActionResult CodeQr(string txtQRCode)
		{
			ViewBag.txtQRCode = txtQRCode;
			QRCodeGenerator qrGenerator = new QRCodeGenerator();
			QRCodeData qrCodeData = qrGenerator.CreateQrCode(txtQRCode, QRCodeGenerator.ECCLevel.Q);
			QRCode qrCode = new QRCode(qrCodeData);
			System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
			imgBarCode.Height = 50;
			imgBarCode.Width = 50;
			string uniqueId = Guid.NewGuid().ToString("N");
			string filename = string.Format("{0}.{1}",uniqueId , "png");
			var serverpath = Server.MapPath(string.Format("{0}{1}", "~/doc/", filename));

			using (Bitmap bitMap = qrCode.GetGraphic(20))
			{
				using (MemoryStream ms = new MemoryStream())
				{
					using (FileStream fs = new FileStream(serverpath, FileMode.Create, FileAccess.ReadWrite))
					{
						bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
						byte[] bytes = ms.ToArray();
						fs.Write(bytes, 0, bytes.Length);
						ViewBag.imageBytes = bytes;
					}
					//imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
				}
			}
			var Ocr = new AdvancedOcr()
			{
				CleanBackgroundNoise = true,
				EnhanceContrast = true,
				EnhanceResolution = true,
				Language = IronOcr.Languages.English.OcrLanguagePack,
				Strategy = IronOcr.AdvancedOcr.OcrStrategy.Advanced,
				ColorSpace = AdvancedOcr.OcrColorSpace.Color,
				DetectWhiteTextOnDarkBackgrounds = true,
				InputImageType = AdvancedOcr.InputTypes.AutoDetect,
				RotateAndStraighten = true,
				ReadBarCodes = true,
				ColorDepth = 4
			};
			var testImage = serverpath;
			var Results = Ocr.Read(testImage);
			var Barcodes = Results.Barcodes.Select(b => b.Value);
			ViewBag.qrcode = string.Format("Texte : {0} Barcodes: {1}", Results.Text, String.Join(",", Barcodes));
			InformationViewModel informationViewModel = new InformationViewModel()
			{
				CodeQr = uniqueId,
				Name = "Stratège Takam",
				CodeQrPath = filename,
				Title = "Informations de retrait de la carte national d'idendité",
				Description = "Pour retirer votre coli, vous vous servirai soit du code QR soit du numéro du code "
			};
			return View(informationViewModel);
		}


		public ActionResult RenderPdf(InformationViewModel informationViewModel)
		{
			return View(informationViewModel);
		}

		public ActionResult PrintDoc(InformationViewModel informationViewModel)
		{
			
			// instantiate a html to pdf converter object 
			HtmlToPdf converter = new HtmlToPdf();
			string url = Url.Action(actionName: "RenderPdf",controllerName: "Home", routeValues: informationViewModel,protocol: Request.Url.Scheme);
			// create a new pdf document converting an url 
			PdfDocument doc = converter.ConvertUrl(url);

			// save pdf document 
			doc.Save(Server.MapPath(string.Format("{0}{1}{2}", "~/doc/", informationViewModel.CodeQr,".pdf")));

			// close pdf document 
			doc.Close();

			return RedirectToAction("CodeQr");
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}

	
}