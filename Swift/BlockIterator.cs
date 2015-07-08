namespace Swift
{
    using System.Collections;
    using System.Collections.Generic;

    internal class BlockIterator : IEnumerable<BlockIterator.Block>
    {
        private readonly string data;

        public BlockIterator(string data)
        {
            this.data = data ?? string.Empty;
        }

        public IEnumerator<Block> GetEnumerator()
        {
            var startIndex = 0;

            // Look for a Block Identifier. 
            var match = Expressions.Block.Match(this.data);
            while (match.Success)
            {
                var blockId = match.Groups["BlockID"].Value;

                // Look for the next available Block Identifier.
                match = match.NextMatch();

                var nextIndex = match.Index;
                var length = nextIndex > 0 ? nextIndex - startIndex : this.data.Length - startIndex;

                // The block is the characters from the current block identifier to the next.
                var block = this.data.Substring(startIndex, length);

                startIndex = nextIndex;

                yield return new Block(blockId, block);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        internal class Block
        {
            public Block(string blockId, string value)
            {
                this.BlockId = blockId;
                this.Value = value;
            }

            public string BlockId { get; private set; }

            public string Value { get; private set; }
        }
    }
}
