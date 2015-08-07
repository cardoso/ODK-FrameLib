using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FrameLib
{
    public class CCreatureFramePack
    {
        public struct Creature
        {
            /// <summary>
            /// List of directions in this effect
            /// </summary>
            public List<Action> Actions;
        }

        public struct Action
        {
            /// <summary>
            /// List of frames in this direction
            /// </summary>
            public List<Direction> Directions;
        }

        public struct Direction
        {
            /// <summary>
            /// List of frames in this direction
            /// </summary>
            public CCreatureFrame[] Frames;
        }

        /// <summary>
        /// List of effects in this instance of CCreatureFramePack
        /// </summary>
        public List<Creature> Creatures;

        /// <summary>
        /// Initializes a new instance of CFramePack from a valid FileStream.
        /// </summary>
        /// <param name="file">The FileStream as it would be read by DarkEden.</param>
        public CCreatureFramePack(ref FileStream file)
        {
            if (file.Length == 0) return;

            this.Creatures = new List<Creature>();

            byte[] _crc = new byte[2];
            file.Read(_crc, 0, 2);
            UInt16 creaturecount = BitConverter.ToUInt16(_crc, 0);

            for (int i = 0; i < creaturecount; i++)
            {
                Creature cr;

                byte actioncount = (byte)file.ReadByte();

                cr.Actions = new List<Action>();

                for (int a = 0; a < actioncount; a++)
                {
                    Action ac;

                    byte dircount = (byte)file.ReadByte();

                    ac.Directions = new List<Direction>();

                    for (int d = 0; d < dircount; d++ )
                    {
                        Direction di;

                        byte[] _frc = new byte[2];
                        file.Read(_frc, 0, 2);
                        UInt16 framecount = BitConverter.ToUInt16(_frc, 0);

                        di.Frames = new CCreatureFrame[framecount];

                        for (int f = 0; f < framecount; f++)
                        {
                            di.Frames[f] = new CCreatureFrame(ref file);
                        }

                        ac.Directions.Add(di);
                    }

                    cr.Actions.Add(ac);
                }

                this.Creatures.Add(cr);
            }
        }

        /// <summary>
        /// Initializes a new instance of CCreatureFramePack from a valid directory.
        /// </summary>
        /// <param name="filedir">The directory of the .cfpk.</param>
        public CCreatureFramePack(string filedir)
        {
            FileStream file = File.Open(filedir, FileMode.Open);

            if (file.Length == 0) return;

            this.Creatures = new List<Creature>();

            byte[] _crc = new byte[2];
            file.Read(_crc, 0, 2);
            UInt16 creaturecount = BitConverter.ToUInt16(_crc, 0);

            for (int i = 0; i < creaturecount; i++)
            {
                Creature cr;

                byte actioncount = (byte)file.ReadByte();

                cr.Actions = new List<Action>();

                for (int a = 0; a < actioncount; a++)
                {
                    Action ac;

                    byte dircount = (byte)file.ReadByte();

                    ac.Directions = new List<Direction>();

                    for (int d = 0; d < dircount; d++ )
                    {
                        Direction di;

                        byte[] _frc = new byte[2];
                        file.Read(_frc, 0, 2);
                        UInt16 framecount = BitConverter.ToUInt16(_frc, 0);

                        di.Frames = new CCreatureFrame[framecount];

                        for (int f = 0; f < framecount; f++)
                        {
                            di.Frames[f] = new CCreatureFrame(ref file);
                        }

                        ac.Directions.Add(di);
                    }

                    cr.Actions.Add(ac);
                }

                this.Creatures.Add(cr);
            }
        }
    }
}
