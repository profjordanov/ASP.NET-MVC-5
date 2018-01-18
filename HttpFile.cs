using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Library.Web
{
    [Serializable]
    public class HttpFile
    {
        // --------------------------------------------------------------------------
        // Constants
        // --------------------------------------------------------------------------
        private const int FILE_START_INDEX = 0;

        // --------------------------------------------------------------------------
        // Properties
        // --------------------------------------------------------------------------

        public string FileName          { get; set; }
        public string ContentType   { get; set; }

        public int DataLength       { get; set; }
        public byte[] Data          { get; set; }


        // --------------------------------------------------------------------------
        // Constructors
        // --------------------------------------------------------------------------
        public static implicit operator HttpFile( HttpPostedFileBase httpPostedFileBase )
        {
            return new HttpFile( httpPostedFileBase );
        }

        private HttpFile( HttpPostedFileBase httpPostedFileBase )
        {
            FileName = httpPostedFileBase.FileName;
            ContentType = httpPostedFileBase.ContentType;
            DataLength = httpPostedFileBase.ContentLength;

            Data = new byte[DataLength];
            // Set the position if was read before
            httpPostedFileBase.InputStream.Position = FILE_START_INDEX; 

            httpPostedFileBase.InputStream.Read( Data, FILE_START_INDEX, DataLength );
        }

        public HttpFile( byte[] data, string fileName, string contentType = Defines.EmptyString )
        {
            Data = data;
            FileName = fileName;
            DataLength = data.Length;
            ContentType = contentType;
        }

        // --------------------------------------------------------------------------
        // Methods
        // --------------------------------------------------------------------------

        public static IList<HttpFile> GetList( IList<HttpPostedFileBase> list )
        {
            return list.Select( x => (HttpFile) x ).ToList();
        }

        public bool SaveData(string fileName)
        {
            BinaryWriter writer = null;

            try
            {
                writer = new BinaryWriter( File.OpenWrite( fileName ) );
                writer.Write( Data );

                writer.Flush();
                writer.Close();
            }
            catch(Exception e)
            {
                if( writer != null )
                {
                    writer.Flush();
                    writer.Close();
                }

                return false;
            }

            return true;
        }

        public MemoryStream GetDataAsStream()
        {
            return new MemoryStream( Data );
        }

    }
}
