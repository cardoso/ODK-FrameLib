using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FrameLib
{
    public class CEffectFrame
    {
        //The following fields are, in order, an accurate representation of a DarkEden Effect Frame

        /// <summary>
        /// The ID of the sprite associated with this frame.
        /// </summary>
        public UInt16 SpriteID;

        /// <summary>
        /// The X offset to be applied
        /// </summary>
        public Int16 X;

        /// <summary>
        /// The Y offset to be applied
        /// </summary>
        public Int16 Y;

        /// <summary>
        /// The amount of light in this effect
        /// </summary>
        public byte Light;

        /// <summary>
        /// Whether or not this effect should be drawn on the background.
        /// </summary>
        public bool isBackground;

        /// <summary>
        /// Initializes a new instance of CImageFrame from a valid FileStream.
        /// </summary>
        /// <param name="file">The FileStream as it would be read by DarkEden.</param>
        public CEffectFrame(ref FileStream file)
        {
            byte[] _spriteid = new byte[2];
            byte[] _x = new byte[2];
            byte[] _y = new byte[2];

            file.Read(_spriteid, 0, 2);
            file.Read(_x, 0, 2);
            file.Read(_y, 0, 2);

            this.SpriteID = BitConverter.ToUInt16(_spriteid, 0);
            this.X = BitConverter.ToInt16(_x, 0);
            this.Y = BitConverter.ToInt16(_y, 0);

            this.Light = (byte)file.ReadByte(); // Light and background boolean are stored in same byte. -> BLLLLLLL
            //file.ReadByte();

            if ((this.Light & 0x80) > 0)
            {
                this.isBackground = true;// BitConverter.ToBoolean(new byte[] { (byte)((bgl & 0x01) >> 7) }, 0);
                this.Light &= 0x7F; //(byte)((bgl & 0x7F) << 1);
            }
            else
                this.isBackground = false;
        }
    }
}
