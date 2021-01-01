using Cave.IO;
using System;
using System.IO;
using System.Windows.Forms;

namespace SlideView
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var args = Arguments.FromEnvironment();
            if (args.IsHelpOptionFound() || (args.Parameters.Count == 0))
            {
                Console.WriteLine("SlideShow");
                Console.WriteLine("shows images in a directory or listed in a textfile");
                Console.WriteLine("usage:");
                Console.WriteLine("slideshow drive://somedir");
                Console.WriteLine("slideshow somefile.ext");
                Console.WriteLine("options: -r randomize images list");
                Console.WriteLine("options: -s include subdirs (recursive)");
                return;
            }

            if (args.IsOptionPresent("f"))
            {

            }
            string[] images = null;
            string param1 = args.Parameters[0];
            try
            {
                if (Directory.Exists(param1))
                {
                    SearchOption so = args.IsOptionPresent("s") ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
                    images = Directory.GetFiles(param1, "*.*", so);


                }
                if (File.Exists(param1))
                {
                }

                if ((images != null) && (images.Length > 0))
                {
                    if (args.IsOptionPresent("r"))
                    {
                        int n = images.Length;
                        Random rnd = new Random();
                        while (n > 1)
                        {
                            n--;
                            int r = rnd.Next(n + 1);
                            string tmp = images[r];
                            images[r] = images[n];
                            images[n] = tmp;
                        }
                    }

                    Application.Run(new FSlideView(images));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

        }
    }
}
