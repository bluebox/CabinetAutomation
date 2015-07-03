using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CabinetAutomation.Cix
{
	public class Tokenizer : IDisposable
	{
		private readonly String file;
		private readonly StreamReader reader;

		public Tokenizer(String file)
		{
			this.file = file;
			this.reader = new StreamReader(file);
		}

		public object Next()
		{
			StringBuilder sb = new StringBuilder();

			while (!this.reader.EndOfStream)
			{
				int c1 = reader.Peek();

				if (c1 == -1)
				{
					break;
				}

				char c = (char)c1;

				if (sb.Length > 0)
				{
					if (sb[0] == '"')
					{
						// Quoted string
						sb.Append((char)reader.Read());

						if (c == '"')
						{
							return sb.ToString();
						}

						continue;
					}

					if (sb[0] == '\'')
					{
						if (c == '\n' || c == '\r')
						{
							return sb.ToString();
						}

						sb.Append((char)reader.Read());

						continue;
					}
				}
				else
				{
					if (c == '\'' || c == '"')
					{
						sb.Append((char)this.reader.Read());

						continue;
					}
				}

				if (Char.IsSymbol(c) || Char.IsPunctuation(c))
				{
					if (sb.Length == 0)
					{
						return (char)this.reader.Read();
					}

					// Do not consume
					return sb.ToString();
				}

				if (sb.Length > 0)
				{
					char start = sb[0];

					if (Char.IsWhiteSpace(start) != Char.IsWhiteSpace(c))
					{
						// Do not consume
						return sb.ToString();
					}
				}

				sb.Append((char)this.reader.Read());

				continue;
			}

			if (sb.Length > 0)
			{
				return sb.ToString();
			}

			return null;
		}

		#region IDisposable Members

		public void Dispose()
		{
			if (this.reader != null)
				reader.Dispose();
		}

		#endregion
	}
}
