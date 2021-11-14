using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace nl.hyperdata.blockchain
{
    public static class BlockExtensions
    {
        public static byte[] GenerateHash(this IBlock block)
        {
            using (SHA512 sha = new SHA512Managed())
            using (MemoryStream stream = new MemoryStream())
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                writer.Write(block.Data);
                writer.Write(block.Nonce);
                writer.Write(block.TimeStamp.ToBinary());
                writer.Write(block.PreviousHash);
                var streamArray = stream.ToArray();
                return sha.ComputeHash(streamArray);
            }
        }

        public static byte[] MineHash(this IBlock block, byte[] difficulty)
        {
            if(difficulty == null)
            {
                throw new ArgumentNullException(nameof(difficulty));
            }

            byte[] hash = new byte[0];
            int d = difficulty.Length;
            byte[] difficultyBGP = new byte[3] { 11,245,78};
            d = 3;
            Console.WriteLine($"Start: {DateTime.Now}");
            while (!hash.Take(d).SequenceEqual(difficultyBGP) && block.Nonce <= int.MaxValue)
            {
                block.Nonce++;
                hash = block.GenerateHash();
            }
            Console.WriteLine($"End: {DateTime.Now}");
            return hash;
        }

        public static bool HasValidHash(this IBlock block)
        {
            var curr = block.GenerateHash();
            return block.Hash.SequenceEqual(curr);
        }

        public static bool HasValidPreviousHash(this IBlock block, IBlock previousblock)
        {
            if (previousblock == null)
            {
                throw new ArgumentException("previousblock is null", "previousblock");
            }
            var prev = previousblock.GenerateHash();
            return previousblock.HasValidHash() && block.PreviousHash.SequenceEqual(prev);
        }


    }
}
