using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FrameLib
{
    public class CImageFramePack
    {
        /// <summary>
        /// The collection of frames which belong to this instance of CFramePack
        /// </summary>
        public List<CImageFrame> Frames { get; private set; }

        /// <summary>
        /// Initializes a new instance of CFramePack from a valid FileStream.
        /// </summary>
        /// <param name="file">The FileStream as it would be read by DarkEden.</param>
        public CImageFramePack(ref FileStream file)
        {
            if (file.Length == 0) return;

            this.Frames = new List<CImageFrame>();

            byte[] _spc = new byte[2];
            file.Read(_spc, 0, 2);
            UInt16 spritecount = BitConverter.ToUInt16(_spc, 0);

            for (int i = 0; i < spritecount; i++)
            {
                CImageFrame imgf = new CImageFrame(ref file);

                this.Frames.Add(imgf);
            }
        }

        /// <summary>
        /// Initializes a new instance of CImageFramePack from a valid directory.
        /// </summary>
        /// <param name="filedir">The directory of the .ifpk.</param>
        public CImageFramePack(string filedir)
        {
            FileStream file = File.Open(filedir, FileMode.Open);

            if (file.Length == 0) return;

            this.Frames = new List<CImageFrame>();

            byte[] _spc = new byte[2];
            file.Read(_spc, 0, 2);
            UInt16 spritecount = BitConverter.ToUInt16(_spc, 0);

            for (int i = 0; i < spritecount; i++)
            {
                CImageFrame imgf = new CImageFrame(ref file);

                this.Frames.Add(imgf);
            }
        }

        public CImageFramePack() { }
    }
}
