using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FrameLib
{
    public class CEffectFramePack
    {
        public struct Effect
        {
            /// <summary>
            /// List of directions in this effect
            /// </summary>
            public List<Direction> Directions;
        }

        public struct Direction
        {
            /// <summary>
            /// List of frames in this direction
            /// </summary>
            public CEffectFrame[] Frames;
        }

        /// <summary>
        /// List of effects in this instance of CEffectFramePack
        /// </summary>
        public List<Effect> Effects;

        /// <summary>
        /// Initializes a new instance of CFramePack from a valid FileStream.
        /// </summary>
        /// <param name="file">The FileStream as it would be read by DarkEden.</param>
        public CEffectFramePack(ref FileStream file)
        {
            if (file.Length == 0) return;

            this.Effects = new List<Effect>();

            byte[] _efc = new byte[2];
            file.Read(_efc, 0, 2);
            UInt16 effectcount = BitConverter.ToUInt16(_efc, 0);

            for (int i = 0; i < effectcount; i++)
            {
                Effect ef;

                byte directioncount = (byte)file.ReadByte();
                //byte framecount = (byte)file.ReadByte();

                byte[] _frc = new byte[2];
                file.Read(_frc, 0, 2);
                UInt16 framecount = BitConverter.ToUInt16(_frc, 0);

                ef.Directions = new List<Direction>();

                for (int d = 0; d < directioncount; i++)
                {
                    Direction di;

                    di.Frames = new CEffectFrame[framecount];//new List<CEffectFrame>();

                    for (int f = 0; f < framecount; f++)
                    {
                        di.Frames[f] = new CEffectFrame(ref file);
                    }

                    ef.Directions.Add(di);
                }

                this.Effects.Add(ef);
            }
        }

        /// <summary>
        /// Initializes a new instance of CEffectFramePack from a valid directory.
        /// </summary>
        /// <param name="filedir">The directory of the .efpk.</param>
        public CEffectFramePack(string filedir)
        {
            FileStream file = File.Open(filedir, FileMode.Open);

            if (file.Length == 0) return;

            this.Effects = new List<Effect>();

            byte[] _efc = new byte[2];
            file.Read(_efc, 0, 2);
            UInt16 effectcount = BitConverter.ToUInt16(_efc, 0);

            this.Effects = new List<Effect>();

            for (int i = 0; i < effectcount; i++)
            {
                Effect ef;

                byte directioncount = (byte)file.ReadByte();
                //byte framecount = (byte)file.ReadByte();

                ef.Directions = new List<Direction>();

                for (int d = 0; d < directioncount; d++)
                {
                    Direction di;

                    byte[] _frc = new byte[2];
                    file.Read(_frc, 0, 2);
                    UInt16 framecount = BitConverter.ToUInt16(_frc, 0);

                    di.Frames = new CEffectFrame[framecount];//new List<CEffectFrame>();

                    for (int f = 0; f < framecount; f++)
                    {
                        di.Frames[f] = new CEffectFrame(ref file);
                    }

                    ef.Directions.Add(di);
                }

                this.Effects.Add(ef);
            }
        }
    }
}
