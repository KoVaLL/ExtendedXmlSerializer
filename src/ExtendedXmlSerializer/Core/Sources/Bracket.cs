// MIT License
// 
// Copyright (c) 2016 Wojciech Nag�rski
//                    Michael DeMond
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ExtendedXmlSerializer.Core.Sources
{
	class Bracket<T> : Items<T> where T : class
	{
		readonly static TypeComparer<T> Typed = TypeComparer<T>.Default;
		readonly static IComparer<T> Comparer = new TypedSortComparer<T>(new TypedSortOrder());

		public Bracket(IStart<T> start, IEnumerable<T> body, IFinish<T> finish) : this(start, body, finish, Comparer) {}

		public Bracket(IStart<T> start, IEnumerable<T> body, IFinish<T> finish, IComparer<T> comparer)
			: base(
			       start.Concat(body.Except(start.Union(finish, Typed), Typed)
			                        .OrderBy(x => x, comparer))
			            .Concat(finish)
			            .ToImmutableArray()) {}
	}
}