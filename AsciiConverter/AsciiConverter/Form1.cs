using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Threading;
using Accord.Video.FFMPEG;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.UI;
using Emgu.CV;
using System.IO;
using System.Drawing.Imaging;
using System.Security.Cryptography;

namespace AsciiConverter
{
    public partial class Form1 : Form
    {
        private bool image_loop = false;
        private readonly string[] _AsciiChars = { "$", "@", "B", "%", "8", "&", "W", "M", "#", "*", "o", "a", "h", "k", "b", "d", "p", "q", "w", "m", "Z", "O", "0", "Q", "L", "C", "J", "U", "Y", "X", "z", "c", "v", "u", "n", "x", "r", "j", "f", "t", "/", "|", "(", ")", "1", "{", "}", "[", "]", "?", "-", "_", "+", "~", "i", "!", "l", "I", ";", ":", ",", "^", "`", ".", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", " " };
        private int size_scale = 0;
        private bool record_enabled = false; // Specifies whether or not to record output to video file
        // private List<Image> text_image_cache = new List<Image>();
        private Dictionary<string, Tuple<int, int>> scale_cache = new Dictionary<string, Tuple<int, int>>();
        private bool has_file = false;  // If a file has been loaded
        private bool convert_video = false; // Specifies if current conversion type is video / live view
        private Font mono_font = null;
        private float _ics = 0;
        private readonly int max_char_len = 205;
        private string record_save_path = "./tmp"; // Default saves frames to local dir
        private VideoFileWriter VFW = new VideoFileWriter(); // Main instance for writing files
        private readonly int width = 1280;
        private readonly int height = 720;
        private readonly int framRate = 30;


        public Form1() { 
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mono_font = new Font(FontFamily.GenericMonospace, converted_output.Font.Size);
            converted_output.Font = mono_font; // Generate Monospaced font for ascii Output
            size_scale = (int)Math.Round((float)converted_output.Width / 2.025);
        }


        private void start_convert_click(object sender, EventArgs e) { new Thread(new ThreadStart(init_convert)).Start(); }

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("X2"));
                }
                return builder.ToString();
            }
        }

        static string Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        private void init_convert() // File Convert
        {
            string file = file_path_text_box.Text.Equals(string.Empty) ? null : file_path_text_box.Text;   

            // Validate File
            if (file is null) { MessageBox.Show("Please Select A File or input its path", "File Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            else if (!File.Exists(file)) { MessageBox.Show("Specified File Does Not Exist", "File Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            Invoke(new Action(() => { start_convert_btn.Enabled = false; }));

            // Check what the file media type is
            switch (file.Split('.').Last())
            {
                case "jpg": case "jpeg": case "png":  // Image File Extension
                    // Run Image Conversion
                    Bitmap image = new Bitmap(file, true);
                    Bitmap r_image = GetReSizedImage(image, image_display.Width);
                    string _image = ConvertToAscii(GetReSizedImage(image, (int)Math.Round((float)((converted_output.Width / 2.05)))));

                    // Scaling Calculations for bitmap image display
                    int x = 0;
                    int w = 0;
                    bool skip_scale = false;

                    // Check if image is in scale cache (if it is scaling can be skipped)
                    string SHA;
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();
                        string base64String = Convert.ToBase64String(imageBytes);
                        SHA = Hash(base64String);
                    }

                    if (scale_cache.Keys.Contains(SHA)) { x = scale_cache[SHA].Item1; w = scale_cache[SHA].Item2; skip_scale = true; }

                    if (!skip_scale)
                    {
                        Console.WriteLine("Auto-Scale");
                        while (true) // Bitmap Scaling
                        {
                            if (r_image.Height > image_display.Height)
                            {
                                // Attempt to scale down by increments of 10
                                r_image = GetReSizedImage(image, image_display.Width - x * 5);
                                x++;
                            }
                            else { break; }
                        }

                        while (true) // ASCII Image Scaling
                        {
                            // Ensure that image is scalled width wise
                            if (_image.Split('\n').First().Length > 200)
                            {
                                _image = ConvertToAscii(GetReSizedImage(image, (int)Math.Round((float)(converted_output.Width / 2.05)) - w * 5));
                                w++;
                            } else { break; }
                        }

                        scale_cache.Add(SHA, new Tuple<int, int>(x, w));
                    }
                    else 
                    {
                        Console.WriteLine("Static Scale");
                        r_image = GetReSizedImage(image, image_display.Width - x * 5);  // Bitmap Static Scaling
                        _image = ConvertToAscii(GetReSizedImage(image, (int)Math.Round((float)(converted_output.Width / 2.05)) - w * 5)); // ASCII Image Scaling
                    } 

                    Invoke(new Action(() => {
                        image_display.Image = r_image;
                        converted_output.Text = _image;
                    })); 

                    break;
                case "mp4": // Video File Extension
                    // Run Video Conversion
                    new Thread(() => { process_video(file); }).Start();
                    
                    break;
                default: // Not in supported file types
                    MessageBox.Show("Error: File not supported", "File Conversion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

            Invoke(new Action(() => { start_convert_btn.Enabled = true; }));
        }


        private Image DrawText(string text, Font font, Color textColor, Color backColor)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 5, 5);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }

        private Bitmap GetReSizedImage(Bitmap inputBitmap, int asciiWidth)
        {
            // Calculate the new Height of the image from its width
            int asciiHeight = (int)Math.Ceiling((double)inputBitmap.Height * asciiWidth / inputBitmap.Width);
            
            // Create a new Bitmap and define its resolution
            Bitmap result = new Bitmap(asciiWidth, asciiHeight);
            Graphics g = Graphics.FromImage(result);

            //The interpolation mode produces high quality images
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(inputBitmap, 0, 0, asciiWidth, asciiHeight);
            g.Dispose();
            return result;
        }

        private string ConvertToAscii(Bitmap image)
        {
            bool toggle = false;
            StringBuilder sb = new StringBuilder();
            for (int h = 0; h < image.Height; h++)
            {
                for (int w = 0; w < image.Width; w++)
                {
                    Color pixelColor = image.GetPixel(w, h);
                    
                    //Average out the RGB components to find the Gray Color
                    // int red = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    // int green = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    // int blue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;

                    int _ = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    Color grayColor = Color.FromArgb(_, _, _);

                    //Use the toggle flag to minimize height-wise stretch
                    if (!toggle) { int index = (grayColor.R * 98) / 255; sb.Append(_AsciiChars[index]); }
                }

                if (!toggle) { sb.Append("\n"); toggle = true; } else toggle = false;
            }
            return sb.ToString();
        }


        private void process_video(string file_path)
        {
            bool record_prompt = false; // check whether the record save path dialog has triggered
            convert_video = true; 
            has_file = true;

            bool _w1 = true;
            bool _w2 = true;

            string _image = "";
            Bitmap image = new Bitmap(1, 1);

            using (var vFReader = new VideoFileReader())
            {
                VFW = new VideoFileWriter(); // Reset global instance
                vFReader.Open(file_path);
                int size_scale = (int)Math.Round((float)converted_output.Width / 2.05);
                
                for (int i = 0; i < vFReader.FrameCount; i++)
                {
                    Bitmap frame = vFReader.ReadVideoFrame();

                    // Reset Processing Locks
                    _w1 = true; // Locks to wait for ASCII Image Scaling
                    _w2 = true; // Locks to wait for Bitmap Image Scaling
                    
                    Bitmap w2 = new Bitmap(frame);

                    // Create Seperate Thread for image processing
                    new Thread(() => { image = GetReSizedImage(w2, image_display.Width); _w2 = false; }).Start();
                    _image = ConvertToAscii(GetReSizedImage(frame, size_scale)); _w1 = false;
                    
                    while (_w2 || _w1) ;

                    Invoke(new Action(() =>
                    {
                        converted_output.Text = _image;
                        image_display.Image = image;
                    }));

                    

                    if (record_enabled)
                    {
                        try
                        {
                            new Thread(() =>
                            {
                                // Get Folder that user wants to save the recording to
                                if (!VFW.IsOpen) VFW.Open($@"{record_save_path}/ASCII_Video.mp4", width, height, framRate, VideoCodec.MPEG4);
                                Bitmap _cimage = (Bitmap)DrawText(_image, mono_font, Color.White, Color.Black);
                                Bitmap bmpReduced = ReduceBitmap(_cimage, width, height); // Resize Image
                                VFW.WriteVideoFrame(bmpReduced);
                                _cimage.Dispose();
                                bmpReduced.Dispose();
                            }).Start();
                        }
                        catch (ArgumentException e) { }

                    }

                    w2.Dispose();
                    image.Dispose();
                    frame.Dispose();
                }
                VFW.Close();
                VFW.Dispose();
                //// vFReader.Close();
            }
        }


        public Bitmap ReduceBitmap(Bitmap original, int reducedWidth, int reducedHeight)
        {
            var reduced = new Bitmap(reducedWidth, reducedHeight);
            using (var dc = Graphics.FromImage(reduced))
            {
                // you might want to change properties like
                dc.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                dc.DrawImage(original, new Rectangle(0, 0, reducedWidth, reducedHeight), new Rectangle(0, 0, original.Width, original.Height), GraphicsUnit.Pixel);
            }
            return reduced;
        }


        //private void ImageToVideo(string file_save_path)
        //{
        //    if (!Directory.Exists(file_save_path)) // Make sure path exists
        //    {
        //        string _res = file_save_path.Length > 55 ? "Specified Path does not exist" : $"{file_save_path} does not exist";
        //        MessageBox.Show($"{_res}", "Path does not exist ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    using (var vFWriter = new VideoFileWriter())
        //    {
        //        // Create new video file
        //        vFWriter.Open($"{file_save_path}/ASCII_Video.mp4", width, height, framRate, VideoCodec.MPEG4);

        //        // Loop throught all images in the collection
        //        foreach (Image imageEntity in text_image_cache)
        //        {
        //            var bmpReduced = ReduceBitmap((Bitmap)imageEntity, width, height); // Resize Image
        //            vFWriter.WriteVideoFrame(bmpReduced);
        //            Thread.Sleep(2);
        //        }
        //        vFWriter.Close();
        //    }
        //}

        private void select_file_click(object sender, EventArgs e)
        {
            switch (file_select_dialog.ShowDialog())
            {
                case DialogResult.OK: // User Has Selected a File
                    file_path_text_box.Text = file_select_dialog.FileName;
                    break;
                case DialogResult.Cancel: // User Canceled file select
                    break;
            }
        }


        private void image_loop_process()  // Live View Process
        {
            has_file = true;

            Invoke(new Action(() => { 
                select_file_btn.Enabled = false;
                start_convert_btn.Enabled = false;
                clr_btn.Enabled = false;
                image_display.Image = null;
                converted_output.Clear();
            }));
            
            Capture capture = new Capture(); // Create a camera captue
            using (VFW)
            {
                int width = 1280;
                int height = 720;
                var framRate = 30;
                
                while (image_loop)  // Image Capture Loop
                {
                    Bitmap image = capture.QueryFrame().Bitmap;

                    // Keeps execution calculations on same thread to avoid executing on ownership thread (very slow)
                    string _image = ConvertToAscii(GetReSizedImage(image, size_scale));
                    Bitmap r_image = new Bitmap(GetReSizedImage(image, converted_output.Width));

                    Invoke(new Action(() => {
                        converted_output.Text = _image;
                        image_display.Image = r_image;
                    }));

                    // Check if recording is enabled
                    //if (record_enabled) new Thread(() => { text_image_cache.Add(DrawText(_image, mono_font, Color.White, Color.Black)); });



                    if (record_enabled)
                        new Thread(() =>
                        {
                            if (!VFW.IsOpen) VFW.Open($@"{record_save_path}/ASCII_Video.mp4", width, height, framRate, VideoCodec.MPEG4);
                            Image _cimage = DrawText(_image, mono_font, Color.White, Color.Black);
                            var bmpReduced = ReduceBitmap((Bitmap)_cimage, width, height); // Resize Image
                            VFW.WriteVideoFrame(bmpReduced);
                        }).Start();
                    

                    image.Dispose(); // Free Image From Memory(ish)
                }
                VFW.Close();
            }
            

            // Clean Up Outputs & re-enable buttons
            capture.Dispose();
            Invoke(new Action(() => { 
                image_display.Image = null;
                converted_output.Clear();
                select_file_btn.Enabled = true;
                clr_btn.Enabled = true;
                start_convert_btn.Enabled = true;
            }));            
        }

        private void clr_btn_click(object sender, EventArgs e)
        {
            // Clear Outputs
            converted_output.Clear(); // Clear Text Output
            if (image_display.Image != null)
                image_display.Image.Dispose();
            image_display.Image = null; // Remove Displayed original file
            has_file = false; // Clear File Status
        }

        private void record_btn_click(object sender, EventArgs e)
        {
            // Make sure there is images to be converted
            // if (!has_file && text_image_cache.Count == 0) { MessageBox.Show("No Converted Cache to Read", "No Cache Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            // Check to see if either live view is on or if a video is loaded to be converted
            string[] video_types = { "mp4", "avi" };  // Supported video types
            if (!(image_loop || video_types.Contains(file_path_text_box.Text.Split('.').Last()))) { MessageBox.Show("Please Load a video file type or use the view function to use recording", "Invalid Use", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

            record_enabled = !record_enabled;  // Invert Bool
            record_button.BackColor = record_enabled ? Color.Green : Color.Red; // Toggle Colour Green / Red

            if (record_enabled) 
            {
                // Recording Has Started
                if (folder_select_dialog.ShowDialog() == DialogResult.OK)
                {
                    record_save_path = folder_select_dialog.SelectedPath;
                    Invoke(new Action(() =>
                    {
                        select_file_btn.Enabled = false; // User cannot load a new file during a recording
                        live_view_btn.Enabled = false; // User is not allowed to stop live view during recording
                        clr_btn.Enabled = false; // User is not allowed to clear a frame as it will result in a dropped frame in the final video
                    }));
                }
            }
            else
                Invoke(new Action(() => // Clean Up
                {
                    select_file_btn.Enabled = true;
                    live_view_btn.Enabled = true;
                    clr_btn.Enabled = true;
                }));
        }

        private void frame_cut_btn_click(object sender, EventArgs e) // Pull Frame from converted output and save as single image
        {
            try
            {
                // Prompt for file save location
                if (folder_select_dialog.ShowDialog() == DialogResult.OK)
                {
                    Console.WriteLine(folder_select_dialog.SelectedPath);
                    if (!Directory.Exists(folder_select_dialog.SelectedPath)) { MessageBox.Show("Specifed Directory Could Not Be Found", "Directory Does not Exist", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }

                    // Pull Frame And Save it in specified location location
                    Image _img = DrawText(converted_output.Text, mono_font, Color.White, Color.Black);

                    // Choose file name [Avoid Duplicate File Names]
                    string _name = "ASCII_Image.jpg";
                    for (int i = 0; i < 999; i++)
                    {
                        if (!File.Exists($@"{folder_select_dialog.SelectedPath}\ASCII_Image{i}.jpg"))
                        {
                            _name = $"ASCII_Image{i}.jpg";
                            break;
                        } 
                    }

                    _img.Save($@"{folder_select_dialog.SelectedPath}\{_name}", ImageFormat.Jpeg);
                    MessageBox.Show($@"File Saved to '{folder_select_dialog.SelectedPath}\{_name}'", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } catch
            {
                MessageBox.Show("An error occur while trying to print the image. Please try a different image", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void live_view_btn_Click(object sender, EventArgs e)
        {
            image_loop = !image_loop; // Invert Bool
            live_view_btn.BackColor = image_loop ? Color.LimeGreen : Color.Red; // Select Color
            if (image_loop) new Thread(new ThreadStart(image_loop_process)).Start(); // Run Image Loop Seperately
        }
    }
}
