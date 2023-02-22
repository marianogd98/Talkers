using ConexApiRio.Model;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Configuration;
using System.Windows;

namespace ConexApiRio.Service
{
    public class PrintSvc
    {
        string Data;
        public PrintSvc()
        {            
            //this.Data = "^XA" +
            //            "^A@N,30,7,B:BROADWAY.FNT" + "\n" +
            //            "^A@N,30,8^FO410,180^FDRioSupermarket^FS" + "\n" +
            //            "^FO210,10^A0N,30,25^FD|D1018|^FS" + "\n" +
            //            "^FO255,70^A0N,70,60^FD|D1006|^FS" + "\n" +
            //            "^FO211,160^BY1^B3N,2N,40,Y1,N^FD|D1001|^FS" + "\n" +
            //            "^FO440,160^A0N,15,15^FD|D1998|^FS" + "\n" +
            //            "^XZ";
            this.Data = "^XA" + "\n" +
                        "^ A@N,30,7,B: BROADWAY.FNT" + "\n" +
                        "^ A@N,30,8 ^ FO45,135 ^ FDRioSupermarket ^ FS" + "\n" +
                        "^ FO210,10 ^ A0N,30,25 ^ FD | D1018 | ^FS" + "\n" +
                        "^ FO260,45 ^ A0N,40,35 ^ FDPMVP ^ FS" + "\n" +
                        "^ FO354,45 ^ A0N,40,35 ^ FD | D1006 | ^FS" + "\n" +
                        "^ FO260,90 ^ A0N,40,35 ^ FDIVA ^ FS" + "\n" +
                        "^ FO354,90 ^ A0N,40,35 ^ FD | D1007 | ^FS" + "\n" +
                        "^ FO260,130 ^ A0N,45,35 ^ FDTOTAL ^ FS" + "\n" +
                        "^ FO354,130 ^ A0N,45,35 ^ FD | D1008 | ^FS" + "\n" +
                        "^  " + "\n" +
                        "^ FO100,165 ^ A0N,15,15 ^ FD | D1998 | ^FS" + "\n" +
                        "^ XZ";
        }

        public void CreateFileRtf(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    Impresion imp = new JsonPlaceHolderApi().GetImpresion();
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {

                        string[] partes = this.Data.Split(new char[] { '\n' });

                        foreach (string item in partes)
                        {
                            sw.WriteLine(item);
                        }
                    }
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);   
            }
        }

        public bool SendCodeBarraToPrinter(Habladores producto)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string printerName = appSettings["Impresora"] ?? "Not Found";
            string szFileName = appSettings["FilePrintBarra"] ?? "Not Found";

            if (printerName.Equals("Not Found") || szFileName.Equals("Not Found"))
            {
                MessageBox.Show("Error: Revisar archivo de configuracón", "Error de Conexión", MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }
            else
            {
                string path = Directory.GetCurrentDirectory() + "\\" + szFileName;
                CreateFileRtf(path);

                var sb = new StringBuilder();
                try
                {
                    using (var sr = new StreamReader(path, Encoding.Default))
                    {
                        while (!sr.EndOfStream)
                        {
                            string linea = sr.ReadLine();
                            linea = linea.Replace("|D1001|", producto.CodigoSap);
                            sb.AppendLine(linea);
                        }
                    }
                    /* Ojo con esto verificar qie sino manda imprimir de falso */
                    return RawPrinterHelper.SendStringToPrinter(printerName, sb.ToString());
                }
                catch (Exception e)
                {
                    string m = e.ToString();
                    return false;
                }

            }
        }

        public bool SendCodeBarraToPrinterCardProduct(Producto producto)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string printerName = appSettings["Impresora"] ?? "Not Found";
            string szFileName;

            szFileName = appSettings["FilePrintBarraCardProduct"] ?? "Not Found";

            if (printerName.Equals("Not Found") || szFileName.Equals("Not Found"))
            {
                MessageBox.Show("Error: Revisar archivo de configuracón", "Error de Conexión", MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }
            else
            {
                string path = Directory.GetCurrentDirectory() + "\\" + szFileName;
                CreateFileRtf(path);

                var sb = new StringBuilder();
                try
                {
                    using (var sr = new StreamReader(path, Encoding.Default))
                    {
                        while (!sr.EndOfStream)
                        {
                            string linea = sr.ReadLine();
                            linea = linea.Replace("|D1001|", producto.barra);
                            sb.AppendLine(linea);
                        }
                    }
                    /* Ojo con esto verificar qie sino manda imprimir de falso */
                    return RawPrinterHelper.SendStringToPrinter(printerName, sb.ToString());
                }
                catch (Exception e)
                {
                    string m = e.ToString();
                    return false;
                }

            }
        }





        public bool SendTextFileToPrinter(Habladores hablador)
        {          
            var appSettings = ConfigurationManager.AppSettings;
            string printerName = appSettings["Impresora"] ?? "Not Found";
            string szFileName = appSettings["FilePrint"] ?? "Not Found";
            string ofertaRef = "OFERTA ";

            if(printerName.Equals("Not Found") || szFileName.Equals("Not Found"))
            {
                MessageBox.Show("Error: Revisar archivo de configuracón", "Error de Conexión", MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }
            else { 
                
                try
                {
                    //string path = Directory.GetCurrentDirectory() + "\\" + szFileName;
                    var src =  AppDomain.CurrentDomain.BaseDirectory ;
                    string path = Path.Combine(src , szFileName);
                    CreateFileRtf(path);

                    var sb = new StringBuilder();

                    if (hablador.GetOferta().Equals(1)){
                        using (var sr = new StreamReader(path, Encoding.Default))
                        {
                            while (!sr.EndOfStream)
                            {
                                string linea = sr.ReadLine();
                                linea = linea.Replace("|D1018|", hablador.GetDescripcion());
                                linea = linea.Replace("|D1006|", hablador.GetPrecio());
                                linea = linea.Replace("|D1007|", hablador.GetIva());
                                linea = linea.Replace("|D1008|", ofertaRef + hablador.GetPrecioo());
                                linea = linea.Replace("|D1998|", hablador.GetFecha());
                                linea = linea.Replace("|D1001|", hablador.GetCodigo_barra());
                                sb.AppendLine(linea);
                            }
                        }

                    }
                    else
                    {
                        using (var sr = new StreamReader(path, Encoding.Default))
                        {
                            while (!sr.EndOfStream)
                            {
                                string linea = sr.ReadLine();
                                linea = linea.Replace("|D1018|", hablador.GetDescripcion());
                                linea = linea.Replace("|D1006|", hablador.GetPrecio());
                                linea = linea.Replace("|D1007|", hablador.GetIva());
                                linea = linea.Replace("|D1008|", hablador.GetTotal());
                                linea = linea.Replace("|D1998|", hablador.GetFecha());
                                linea = linea.Replace("|D1001|", hablador.GetCodigo_barra());
                                sb.AppendLine(linea);
                            }
                        }
                    }
                    /* Ojo con esto verificar qie sino manda imprimir de falso */
                    return RawPrinterHelper.SendStringToPrinter(printerName, sb.ToString());
                }
                catch (Exception e)
                {
                    string m = e.ToString();
                    MessageBox.Show(m);
                    return false;
                }
                
            }
        }
    }



    public class RawPrinterHelper
    {
        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)] public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)] public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)] public string pDataType;
        }
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, out Int32 dwWritten);

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array
        // of bytes, the function sends those bytes to the print queue.
        // Returns true on success, false on failure.
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            Int32 dwError = 0, dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false; // Assume failure unless you specifically succeed.

            di.pDocName = "My C#.NET RAW Document";
            di.pDataType = "RAW";

            // Open the printer.
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                // Start a document.
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    // Start a page.
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your bytes.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }

        public static bool SendFileToPrinter(string szPrinterName, string szFileName)
        {
            // Open the file.
            FileStream fs = new FileStream(szFileName, FileMode.Open);
            // Create a BinaryReader on the file.
            BinaryReader br = new BinaryReader(fs);
            // Dim an array of bytes big enough to hold the file's contents.
            Byte[] bytes = new Byte[fs.Length];
            bool bSuccess = false;
            // Your unmanaged pointer.
            IntPtr pUnmanagedBytes = new IntPtr(0);
            int nLength;

            nLength = Convert.ToInt32(fs.Length);
            // Read the contents of the file into the array.
            bytes = br.ReadBytes(nLength);
            // Allocate some unmanaged memory for those bytes.
            pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
            // Copy the managed byte array into the unmanaged array.
            Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
            // Send the unmanaged bytes to the printer.
            bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
            // Free the unmanaged memory that you allocated earlier.
            Marshal.FreeCoTaskMem(pUnmanagedBytes);
            return bSuccess;
        }
        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes;
            Int32 dwCount;
            // How many characters are in the string?
            dwCount = szString.Length;
            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            // Send the converted ANSI string to the printer.
            SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }
    }
}
