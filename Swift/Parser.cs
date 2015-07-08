namespace Swift
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    public class Parser
    {
        private readonly IValidator validator;

        public Parser()
            : this(new Validator())
        {
        }

        public Parser(IValidator validator)
        {
            if (validator == null)
            {
                throw new ArgumentNullException("validator");
            }

            this.validator = validator;
        }

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Reviewed.")]
        public ICollection<Message> Parse(FileInfo fileInfo)
        {
            if (fileInfo == null)
            {
                throw new ArgumentNullException("fileInfo");
            }

            try
            {
                using (var stream = File.Open(fileInfo.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    return this.Parse(stream);
                }
            }
            catch (IOException e)
            {
                throw new ParserException(Invariant.Format("Unable to read {0} at {1}.", fileInfo.Name, fileInfo.DirectoryName), e);
            }
        }

        public ICollection<Message> Parse(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }

            using (var reader = new StreamReader(stream, true))
            {
                return this.Parse(reader);
            }
        }

        public ICollection<Message> Parse(TextReader reader)
        {
            if (reader == null)
            {
                throw new ArgumentNullException("reader");
            }

            var list = new List<Message>();

            // Looks for possible multiple messages.
            var iterator = new MessageIterator(reader);
            foreach (var message in iterator)
            {
                try
                {
                    list.Add(this.Parse(message.Value));
                }
                catch (ParserException e)
                {
                    throw new ParserException(Invariant.Format("Unable to parse message at offset {0}.", message.Offset), e);
                }
            }

            if (list.Count == 0)
            {
                throw new ParserException("No messages found.");
            }

            return list.AsReadOnly();
        }

        public Message Parse(string message)
        {
            // Validate if data has a valid message structure.
            if (!this.validator.Validate(message))
            {
                var exception = new ParserException("Invalid message.");
                exception.Data["Swift.Validator.Errors"] = string.Join(Environment.NewLine, this.validator.Errors);

                throw exception;
            }

            BasicHeader basicHeader = null;
            ApplicationHeader applicationHeader = null;
            UserHeader userHeader = null;
            TextBlock text = null;
            Trailer trailer = null;

            // Iterate through all blocks.
            // Instantiate the specific block when available.
            // Each instance will self validate.
            var iterator = new BlockIterator(message);
            foreach (var block in iterator)
            {
                switch (block.BlockId)
                {
                    case "1":
                        basicHeader = new BasicHeader(block.Value);
                        break;
                    case "2":
                        applicationHeader = new ApplicationHeader(block.Value);
                        break;
                    case "3":
                        userHeader = new UserHeader(block.Value);
                        break;
                    case "4":
                        text = new TextBlock(block.Value);
                        break;
                    case "5":
                        trailer = new Trailer(block.Value);
                        break;
                }
            }

            // A message is composed of these blocks.
            return new Message(basicHeader, applicationHeader, userHeader, text, trailer);
        }
    }
}
